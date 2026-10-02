using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class TextField : DisplayObject, IMeshFactory
{
	public class LineInfo
	{
		public float width;

		public float height;

		public float baseline;

		public int charIndex;

		public short charCount;

		public float y;

		internal float y2;

		private static Stack<LineInfo> pool = new Stack<LineInfo>();

		public static LineInfo Borrow()
		{
			if (pool.Count > 0)
			{
				LineInfo lineInfo = pool.Pop();
				lineInfo.width = (lineInfo.height = (lineInfo.baseline = 0f));
				lineInfo.y = (lineInfo.y2 = 0f);
				lineInfo.charIndex = (lineInfo.charCount = 0);
				return lineInfo;
			}
			return new LineInfo();
		}

		public static void Return(LineInfo value)
		{
			pool.Push(value);
		}

		public static void Return(List<LineInfo> values)
		{
			int count = values.Count;
			for (int i = 0; i < count; i++)
			{
				pool.Push(values[i]);
			}
			values.Clear();
		}
	}

	public struct LineCharInfo
	{
		public float width;

		public float height;

		public float baseline;
	}

	public struct CharPosition
	{
		public int charIndex;

		public short lineIndex;

		public float offsetX;

		public short vertCount;

		public short width;

		public short imgIndex;
	}

	private VertAlignType _verticalAlign;

	private TextFormat _textFormat;

	private bool _input;

	private string _text;

	private AutoSizeType _autoSize;

	private bool _wordWrap;

	private bool _singleLine;

	private bool _html;

	private RTLSupport.DirectionType _textDirection;

	private int _maxWidth;

	private List<HtmlElement> _elements;

	private List<LineInfo> _lines;

	private List<CharPosition> _charPositions;

	private BaseFont _font;

	private float _textWidth;

	private float _textHeight;

	private bool _textChanged;

	private float _yOffset;

	private float _fontSizeScale;

	private float _renderScale;

	private int _fontVersion;

	private string _parsedText;

	private RichTextField _richTextField;

	private const int GUTTER_X = 2;

	private const int GUTTER_Y = 2;

	private const float IMAGE_BASELINE = 0.8f;

	private static float[] STROKE_OFFSET = new float[16]
	{
		-1f, 0f, 1f, 0f, 0f, -1f, 0f, 1f, -1f, -1f,
		1f, -1f, -1f, 1f, 1f, 1f
	};

	private static List<LineCharInfo> sLineChars = new List<LineCharInfo>();

	private readonly string strRegex = "(\\！|\\？|\\，|\\。|\\《|\\》|\\（|\\）|\\(|\\)|\\：|\\“|\\‘|\\、|\\；|\\+|\\-|\\·|\\#|\\￥|\\；|\\”|\\【|\\】|\\——|\\/)";

	public TextFormat textFormat
	{
		get
		{
			return _textFormat;
		}
		set
		{
			_textFormat = value;
			ApplyFormat();
		}
	}

	public AlignType align
	{
		get
		{
			return _textFormat.align;
		}
		set
		{
			if (_textFormat.align != value)
			{
				_textFormat.align = value;
				if (!string.IsNullOrEmpty(_text))
				{
					_textChanged = true;
				}
			}
		}
	}

	public VertAlignType verticalAlign
	{
		get
		{
			return _verticalAlign;
		}
		set
		{
			if (_verticalAlign != value)
			{
				_verticalAlign = value;
				if (!_textChanged)
				{
					ApplyVertAlign();
				}
			}
		}
	}

	public string text
	{
		get
		{
			return _text;
		}
		set
		{
			if (!(_text == value) || _html)
			{
				_text = value;
				_textChanged = true;
				_html = false;
			}
		}
	}

	public string htmlText
	{
		get
		{
			return _text;
		}
		set
		{
			if (!(_text == value) || !_html)
			{
				_text = value;
				_textChanged = true;
				_html = true;
			}
		}
	}

	public string parsedText => _parsedText;

	public AutoSizeType autoSize
	{
		get
		{
			return _autoSize;
		}
		set
		{
			if (_autoSize != value)
			{
				_autoSize = value;
				_textChanged = true;
			}
		}
	}

	public bool wordWrap
	{
		get
		{
			return _wordWrap;
		}
		set
		{
			if (_wordWrap != value)
			{
				_wordWrap = value;
				_textChanged = true;
			}
		}
	}

	public bool singleLine
	{
		get
		{
			return _singleLine;
		}
		set
		{
			if (_singleLine != value)
			{
				_singleLine = value;
				_textChanged = true;
			}
		}
	}

	public float stroke
	{
		get
		{
			return _textFormat.outline;
		}
		set
		{
			if (_textFormat.outline != value)
			{
				_textFormat.outline = value;
				base.graphics.SetMeshDirty();
			}
		}
	}

	public Color strokeColor
	{
		get
		{
			return _textFormat.outlineColor;
		}
		set
		{
			if (_textFormat.outlineColor != value)
			{
				_textFormat.outlineColor = value;
				base.graphics.SetMeshDirty();
			}
		}
	}

	public Vector2 shadowOffset
	{
		get
		{
			return _textFormat.shadowOffset;
		}
		set
		{
			_textFormat.shadowOffset = value;
			base.graphics.SetMeshDirty();
		}
	}

	public float textWidth
	{
		get
		{
			if (_textChanged)
			{
				BuildLines();
			}
			return _textWidth;
		}
	}

	public float textHeight
	{
		get
		{
			if (_textChanged)
			{
				BuildLines();
			}
			return _textHeight;
		}
	}

	public int maxWidth
	{
		get
		{
			return _maxWidth;
		}
		set
		{
			if (_maxWidth != value)
			{
				_maxWidth = value;
				_textChanged = true;
			}
		}
	}

	public List<HtmlElement> htmlElements
	{
		get
		{
			if (_textChanged)
			{
				BuildLines();
			}
			return _elements;
		}
	}

	public List<LineInfo> lines
	{
		get
		{
			if (_textChanged)
			{
				BuildLines();
			}
			return _lines;
		}
	}

	public List<CharPosition> charPositions
	{
		get
		{
			if (_textChanged)
			{
				BuildLines();
			}
			base.graphics.UpdateMesh();
			return _charPositions;
		}
	}

	public RichTextField richTextField => _richTextField;

	public TextField()
	{
		_flags |= Flags.TouchDisabled;
		_textFormat = new TextFormat();
		_fontSizeScale = 1f;
		_renderScale = UIContentScaler.scaleFactor;
		_wordWrap = false;
		_text = string.Empty;
		_parsedText = string.Empty;
		_elements = new List<HtmlElement>(0);
		_lines = new List<LineInfo>(1);
		CreateGameObject("TextField");
		base.graphics = new NGraphics(base.gameObject);
		base.graphics.meshFactory = this;
	}

	internal void EnableRichSupport(RichTextField richTextField)
	{
		_richTextField = richTextField;
		if (richTextField is InputTextField)
		{
			_input = true;
			EnableCharPositionSupport();
		}
	}

	public void EnableCharPositionSupport()
	{
		if (_charPositions == null)
		{
			_charPositions = new List<CharPosition>();
			_textChanged = true;
		}
	}

	public void ApplyFormat()
	{
		string value = _textFormat.font;
		if (string.IsNullOrEmpty(value))
		{
			value = UIConfig.defaultFont;
		}
		BaseFont font = FontManager.GetFont(value);
		if (_font != font)
		{
			_font = font;
			_fontVersion = _font.version;
			base.graphics.SetShaderAndTexture(_font.shader, _font.mainTexture);
		}
		if (!string.IsNullOrEmpty(_text))
		{
			_textChanged = true;
		}
	}

	public bool Redraw()
	{
		if (_font == null)
		{
			_font = FontManager.GetFont(UIConfig.defaultFont);
			base.graphics.SetShaderAndTexture(_font.shader, _font.mainTexture);
			_fontVersion = _font.version;
			_textChanged = true;
		}
		if (_font.keepCrisp && _renderScale != UIContentScaler.scaleFactor)
		{
			_textChanged = true;
		}
		if (_font.version != _fontVersion)
		{
			_fontVersion = _font.version;
			if (_font.mainTexture != base.graphics.texture)
			{
				base.graphics.SetShaderAndTexture(_font.shader, _font.mainTexture);
				InvalidateBatchingState();
			}
			_textChanged = true;
		}
		if (_textChanged)
		{
			BuildLines();
		}
		return base.graphics.UpdateMesh();
	}

	public bool HasCharacter(char ch)
	{
		return _font.HasCharacter(ch);
	}

	public void GetLinesShape(int startLine, float startCharX, int endLine, float endCharX, bool clipped, List<Rect> resultRects)
	{
		LineInfo lineInfo = _lines[startLine];
		LineInfo lineInfo2 = _lines[endLine];
		bool flag = _textFormat.align == AlignType.Left;
		if (startLine == endLine)
		{
			Rect rect = Rect.MinMaxRect(startCharX, lineInfo.y, endCharX, lineInfo.y + lineInfo.height);
			if (clipped)
			{
				resultRects.Add(ToolSet.Intersection(ref rect, ref _contentRect));
			}
			else
			{
				resultRects.Add(rect);
			}
			return;
		}
		if (startLine == endLine - 1)
		{
			Rect rect2 = Rect.MinMaxRect(startCharX, lineInfo.y, flag ? (2f + lineInfo.width) : _contentRect.xMax, lineInfo.y + lineInfo.height);
			if (clipped)
			{
				resultRects.Add(ToolSet.Intersection(ref rect2, ref _contentRect));
			}
			else
			{
				resultRects.Add(rect2);
			}
			rect2 = Rect.MinMaxRect(2f, lineInfo.y + lineInfo.height, endCharX, lineInfo2.y + lineInfo2.height);
			if (clipped)
			{
				resultRects.Add(ToolSet.Intersection(ref rect2, ref _contentRect));
			}
			else
			{
				resultRects.Add(rect2);
			}
			return;
		}
		Rect rect3 = Rect.MinMaxRect(startCharX, lineInfo.y, flag ? (2f + lineInfo.width) : _contentRect.xMax, lineInfo.y + lineInfo.height);
		if (clipped)
		{
			resultRects.Add(ToolSet.Intersection(ref rect3, ref _contentRect));
		}
		else
		{
			resultRects.Add(rect3);
		}
		for (int i = startLine + 1; i < endLine; i++)
		{
			LineInfo lineInfo3 = _lines[i];
			rect3 = Rect.MinMaxRect(2f, rect3.yMax, flag ? (2f + lineInfo3.width) : _contentRect.xMax, lineInfo3.y + lineInfo3.height);
			if (clipped)
			{
				resultRects.Add(ToolSet.Intersection(ref rect3, ref _contentRect));
			}
			else
			{
				resultRects.Add(rect3);
			}
		}
		rect3 = Rect.MinMaxRect(2f, rect3.yMax, endCharX, lineInfo2.y + lineInfo2.height);
		if (clipped)
		{
			resultRects.Add(ToolSet.Intersection(ref rect3, ref _contentRect));
		}
		else
		{
			resultRects.Add(rect3);
		}
	}

	protected override void OnSizeChanged()
	{
		if ((_flags & Flags.UpdatingSize) == 0)
		{
			if (_autoSize == AutoSizeType.Shrink || (_wordWrap && (_flags & Flags.WidthChanged) != 0))
			{
				_textChanged = true;
			}
			else if (_autoSize != AutoSizeType.None)
			{
				base.graphics.SetMeshDirty();
			}
			if (_verticalAlign != VertAlignType.Top)
			{
				ApplyVertAlign();
			}
		}
		base.OnSizeChanged();
	}

	public override void EnsureSizeCorrect()
	{
		if (_textChanged && _autoSize != AutoSizeType.None)
		{
			BuildLines();
		}
	}

	public override void Update(UpdateContext context)
	{
		if (_richTextField == null)
		{
			Redraw();
		}
		base.Update(context);
	}

	private void RequestText()
	{
		if (!_html)
		{
			_font.SetFormat(_textFormat, _fontSizeScale);
			_font.PrepareCharacters(_parsedText);
			return;
		}
		int count = _elements.Count;
		for (int i = 0; i < count; i++)
		{
			HtmlElement htmlElement = _elements[i];
			if (htmlElement.type == HtmlElementType.Text)
			{
				_font.SetFormat(htmlElement.format, _fontSizeScale);
				_font.PrepareCharacters(htmlElement.text);
			}
		}
	}

	private void BuildLines()
	{
		if (_font == null)
		{
			_font = FontManager.GetFont(UIConfig.defaultFont);
			_fontVersion = _font.version;
			base.graphics.SetShaderAndTexture(_font.shader, _font.mainTexture);
		}
		_textChanged = false;
		base.graphics.SetMeshDirty();
		_renderScale = UIContentScaler.scaleFactor;
		_fontSizeScale = 1f;
		Cleanup();
		if (_text.Length == 0)
		{
			LineInfo lineInfo = LineInfo.Borrow();
			lineInfo.width = 0f;
			lineInfo.height = _font.GetLineHeight(_textFormat.size);
			lineInfo.charIndex = (lineInfo.charCount = 0);
			lineInfo.y = (lineInfo.y2 = 2f);
			_lines.Add(lineInfo);
			_textWidth = (_textHeight = 0f);
		}
		else
		{
			ParseText();
			BuildLines2();
			if (_autoSize == AutoSizeType.Shrink)
			{
				DoShrink();
			}
		}
		if (_autoSize == AutoSizeType.Both)
		{
			_flags |= Flags.UpdatingSize;
			if (_richTextField != null)
			{
				if (_input)
				{
					float wv = Mathf.Max(_textFormat.size, _textWidth);
					float hv = Mathf.Max(_font.GetLineHeight(_textFormat.size) + 4, _textHeight);
					_richTextField.SetSize(wv, hv);
				}
				else
				{
					_richTextField.SetSize(_textWidth, _textHeight);
				}
			}
			else
			{
				SetSize(_textWidth, _textHeight);
			}
			InvalidateBatchingState();
			_flags &= ~Flags.UpdatingSize;
		}
		else if (_autoSize == AutoSizeType.Height)
		{
			_flags |= Flags.UpdatingSize;
			if (_richTextField != null)
			{
				if (_input)
				{
					_richTextField.height = Mathf.Max(_font.GetLineHeight(_textFormat.size) + 4, _textHeight);
				}
				else
				{
					_richTextField.height = _textHeight;
				}
			}
			else
			{
				base.height = _textHeight;
			}
			InvalidateBatchingState();
			_flags &= ~Flags.UpdatingSize;
		}
		_yOffset = 0f;
		ApplyVertAlign();
	}

	private void ParseText()
	{
		if (_html)
		{
			HtmlParser.inst.Parse(_text, _textFormat, _elements, (_richTextField != null) ? _richTextField.htmlParseOptions : null);
			_parsedText = string.Empty;
		}
		else
		{
			_parsedText = _text;
		}
		int count = _elements.Count;
		if (count == 0)
		{
			if (_textDirection != RTLSupport.DirectionType.UNKNOW)
			{
				_parsedText = RTLSupport.DoMapping(_parsedText);
			}
			bool flag = _input || (_richTextField != null && _richTextField.emojies != null);
			if (!flag)
			{
				int length = _parsedText.Length;
				for (int i = 0; i < length; i++)
				{
					char c = _parsedText[i];
					if (c == '\r' || char.IsHighSurrogate(c))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				ParseText(stringBuilder, _parsedText, -1);
				count = _elements.Count;
				_parsedText = stringBuilder.ToString();
			}
			return;
		}
		StringBuilder stringBuilder2 = new StringBuilder();
		for (int j = 0; j < count; j++)
		{
			HtmlElement htmlElement = _elements[j];
			htmlElement.charIndex = stringBuilder2.Length;
			if (htmlElement.type == HtmlElementType.Text)
			{
				if (_textDirection != RTLSupport.DirectionType.UNKNOW)
				{
					htmlElement.text = RTLSupport.DoMapping(htmlElement.text);
				}
				j = ParseText(stringBuilder2, htmlElement.text, j);
				count = _elements.Count;
			}
			else if (htmlElement.isEntity)
			{
				stringBuilder2.Append(' ');
			}
		}
		_parsedText = stringBuilder2.ToString();
	}

	private void BuildLines2()
	{
		float num = (float)_textFormat.letterSpacing * _fontSizeScale;
		float num2 = (float)(_textFormat.lineSpacing - 1) * _fontSizeScale;
		float num3 = _contentRect.width - 4f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		short num7 = 0;
		bool flag = false;
		float num8 = 0f;
		TextFormat format = _textFormat;
		_font.SetFormat(format, _fontSizeScale);
		bool flag2 = _wordWrap && !_singleLine;
		if (_maxWidth > 0)
		{
			flag2 = true;
			num3 = _maxWidth - 4;
		}
		_textWidth = (_textHeight = 0f);
		RequestText();
		int count = _elements.Count;
		int num9 = 0;
		HtmlElement htmlElement = null;
		if (count > 0)
		{
			htmlElement = _elements[num9];
		}
		int length = _parsedText.Length;
		LineInfo lineInfo = LineInfo.Borrow();
		_lines.Add(lineInfo);
		lineInfo.y = (lineInfo.y2 = 2f);
		sLineChars.Clear();
		for (int i = 0; i < length; i++)
		{
			char c = _parsedText[i];
			num4 = (num5 = (num6 = 0f));
			while (htmlElement != null && htmlElement.charIndex == i)
			{
				if (htmlElement.type == HtmlElementType.Text)
				{
					format = htmlElement.format;
					_font.SetFormat(format, _fontSizeScale);
				}
				else
				{
					IHtmlObject htmlObject = htmlElement.htmlObject;
					if (_richTextField != null && htmlObject == null)
					{
						htmlElement.space = (int)(num3 - lineInfo.width - 4f);
						htmlObject = (htmlElement.htmlObject = _richTextField.htmlPageContext.CreateObject(_richTextField, htmlElement));
					}
					if (htmlObject != null)
					{
						num4 = htmlObject.width + 2f;
						num5 = htmlObject.height;
						num6 = num5 * 0.8f;
					}
					if (htmlElement.isEntity)
					{
						c = '\0';
					}
				}
				num9++;
				htmlElement = ((num9 >= count) ? null : _elements[num9]);
			}
			if (c == '\0' || c == '\n')
			{
				flag = false;
			}
			else if (_font.GetGlyph((c == '\t') ? ' ' : c, out num4, out num5, out num6))
			{
				if (c == '\t')
				{
					num4 *= 4f;
				}
				if (flag)
				{
					if (char.IsWhiteSpace(c))
					{
						num7 = 0;
					}
					else if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '.' || c == '"' || c == '\'' || format.specialStyle == TextFormat.SpecialStyle.Subscript || format.specialStyle == TextFormat.SpecialStyle.Superscript || (_textDirection != RTLSupport.DirectionType.UNKNOW && RTLSupport.IsArabicLetter(c)))
					{
						num7++;
					}
					else
					{
						flag = false;
					}
				}
				else if (char.IsWhiteSpace(c))
				{
					num7 = 0;
					flag = true;
				}
				else if (format.specialStyle == TextFormat.SpecialStyle.Subscript || format.specialStyle == TextFormat.SpecialStyle.Superscript)
				{
					if (sLineChars.Count > 0)
					{
						num7 = 2;
						flag = true;
					}
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
			sLineChars.Add(new LineCharInfo
			{
				width = num4,
				height = num5,
				baseline = num6
			});
			if (num4 != 0f)
			{
				if (num8 != 0f)
				{
					num8 += num;
				}
				num8 += num4;
			}
			if (c == '\n' && !_singleLine)
			{
				UpdateLineInfo(lineInfo, num, sLineChars.Count);
				LineInfo lineInfo2 = LineInfo.Borrow();
				_lines.Add(lineInfo2);
				lineInfo2.y = lineInfo.y + (lineInfo.height + num2);
				if (lineInfo2.y < 2f)
				{
					lineInfo2.y = 2f;
				}
				lineInfo2.y2 = lineInfo2.y;
				lineInfo2.charIndex = lineInfo.charIndex + lineInfo.charCount;
				sLineChars.Clear();
				flag = false;
				num8 = 0f;
				lineInfo = lineInfo2;
			}
			else
			{
				if (!flag2 || !(num8 > num3))
				{
					continue;
				}
				int count2 = sLineChars.Count;
				int num10;
				if (flag && num7 < 20 && count2 > 2)
				{
					num10 = num7;
					UpdateLineInfo(lineInfo, num, count2 - (num10 + 1));
					lineInfo.charCount++;
				}
				else
				{
					num10 = ((count2 > 1) ? 1 : 0);
					UpdateLineInfo(lineInfo, num, count2 - num10);
				}
				LineInfo lineInfo3 = LineInfo.Borrow();
				_lines.Add(lineInfo3);
				lineInfo3.y = lineInfo.y + (lineInfo.height + num2);
				if (lineInfo3.y < 2f)
				{
					lineInfo3.y = 2f;
				}
				lineInfo3.y2 = lineInfo3.y;
				lineInfo3.charIndex = lineInfo.charIndex + lineInfo.charCount;
				num8 = 0f;
				if (num10 != 0)
				{
					for (int j = lineInfo.charCount; j < count2; j++)
					{
						LineCharInfo lineCharInfo = sLineChars[j];
						if (num8 != 0f)
						{
							num8 += num;
						}
						num8 += lineCharInfo.width;
					}
					sLineChars.RemoveRange(0, lineInfo.charCount);
				}
				else
				{
					sLineChars.Clear();
				}
				flag = false;
				if (Regex.IsMatch(_parsedText[i].ToString(), strRegex) && i < length)
				{
					lineInfo.charCount--;
					lineInfo3.charIndex--;
					i--;
				}
				lineInfo = lineInfo3;
			}
		}
		UpdateLineInfo(lineInfo, num, sLineChars.Count);
		if (_textWidth > 0f)
		{
			_textWidth += 4f;
		}
		_textHeight = lineInfo.y + lineInfo.height + 2f;
		_textWidth = Mathf.RoundToInt(_textWidth);
		_textHeight = Mathf.RoundToInt(_textHeight);
	}

	private void UpdateLineInfo(LineInfo line, float letterSpacing, int cnt)
	{
		for (int i = 0; i < cnt; i++)
		{
			LineCharInfo lineCharInfo = sLineChars[i];
			if (lineCharInfo.baseline > line.baseline)
			{
				line.height += lineCharInfo.baseline - line.baseline;
				line.baseline = lineCharInfo.baseline;
			}
			if (lineCharInfo.height - lineCharInfo.baseline > line.height - line.baseline)
			{
				line.height += lineCharInfo.height - lineCharInfo.baseline - (line.height - line.baseline);
			}
			if (lineCharInfo.width > 0f)
			{
				if (line.width != 0f)
				{
					line.width += letterSpacing;
				}
				line.width += lineCharInfo.width;
			}
		}
		if (line.height == 0f)
		{
			if (_lines.Count == 1)
			{
				line.height = _textFormat.size;
			}
			else
			{
				line.height = _lines[_lines.Count - 2].height;
			}
		}
		if (line.width > _textWidth)
		{
			_textWidth = line.width;
		}
		line.charCount = (short)cnt;
	}

	private void DoShrink()
	{
		if (_lines.Count > 1 && _textHeight > _contentRect.height)
		{
			int num = 0;
			int num2 = _textFormat.size;
			_fontSizeScale = Mathf.Sqrt(_contentRect.height / _textHeight);
			int num3 = Mathf.FloorToInt(_fontSizeScale * (float)_textFormat.size);
			while (true)
			{
				LineInfo.Return(_lines);
				BuildLines2();
				if (_textWidth > _contentRect.width || _textHeight > _contentRect.height)
				{
					num2 = num3;
				}
				else
				{
					num = num3;
				}
				if (num2 - num > 1 || (num2 != num && num3 == num2))
				{
					num3 = num + (num2 - num) / 2;
					_fontSizeScale = (float)num3 / (float)_textFormat.size;
					continue;
				}
				break;
			}
		}
		else if (_textWidth > _contentRect.width)
		{
			_fontSizeScale = _contentRect.width / _textWidth;
			LineInfo.Return(_lines);
			BuildLines2();
			if (_textWidth > _contentRect.width)
			{
				int num4 = Mathf.FloorToInt((float)_textFormat.size * _fontSizeScale);
				num4--;
				_fontSizeScale = (float)num4 / (float)_textFormat.size;
				LineInfo.Return(_lines);
				BuildLines2();
			}
		}
	}

	private int ParseText(StringBuilder buffer, string source, int elementIndex)
	{
		int length = source.Length;
		int i = 0;
		int num = 0;
		bool flag = _richTextField != null && _richTextField.emojies != null;
		for (; i < length; i++)
		{
			char c = source[i];
			if (c == '\r')
			{
				buffer.Append(source, num, i - num);
				if (i != length - 1 && source[i + 1] == '\n')
				{
					i++;
				}
				num = i + 1;
				buffer.Append('\n');
				continue;
			}
			bool flag2 = char.IsHighSurrogate(c);
			if (flag)
			{
				uint num2 = 0u;
				num2 = (uint)((!flag2) ? c : ((source[i + 1] & 0x3FF) + ((c & 0x3FF) + 64 << 10)));
				if (_richTextField.emojies.TryGetValue(num2, out var value))
				{
					HtmlElement element = HtmlElement.GetElement(HtmlElementType.Image);
					element.Set("src", value.url);
					if (value.width != 0)
					{
						element.Set("width", value.width);
					}
					if (value.height != 0)
					{
						element.Set("height", value.height);
					}
					if (flag2)
					{
						element.text = source.Substring(i, 2);
					}
					else
					{
						element.text = source.Substring(i, 1);
					}
					element.format.align = _textFormat.align;
					_elements.Insert(++elementIndex, element);
					buffer.Append(source, num, i - num);
					num = i;
					element.charIndex = buffer.Length;
				}
			}
			if (flag2)
			{
				buffer.Append(source, num, i - num);
				num = i + 2;
				i++;
				buffer.Append(' ');
			}
		}
		if (num < length)
		{
			buffer.Append(source, num, i - num);
		}
		return elementIndex;
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		if (_textWidth == 0f && _lines.Count == 1)
		{
			if (_charPositions != null)
			{
				_charPositions.Clear();
				_charPositions.Add(default(CharPosition));
			}
			if (_richTextField != null)
			{
				_richTextField.RefreshObjects();
			}
			return;
		}
		float num = (float)_textFormat.letterSpacing * _fontSizeScale;
		TextFormat format = _textFormat;
		_font.SetFormat(format, _fontSizeScale);
		_font.UpdateGraphics(base.graphics);
		float num2 = ((_contentRect.width > 0f) ? (_contentRect.width - 4f) : 0f);
		float num3 = ((_contentRect.height > 0f) ? Mathf.Max(_contentRect.height, _font.GetLineHeight(format.size)) : 0f);
		if (_charPositions != null)
		{
			_charPositions.Clear();
		}
		List<Vector3> vertices = vb.vertices;
		List<Vector2> uvs = vb.uvs;
		List<Vector2> uvs2 = vb.uvs2;
		List<Color32> colors = vb.colors;
		HtmlLink htmlLink = null;
		float startCharX = 0f;
		int startLine = 0;
		float num4 = 0f;
		bool flag = !_input && _autoSize == AutoSizeType.None;
		string text = null;
		int num5 = 0;
		int count = _elements.Count;
		HtmlElement htmlElement = null;
		if (count > 0)
		{
			htmlElement = _elements[num5];
		}
		int count2 = _lines.Count;
		for (int i = 0; i < count2; i++)
		{
			LineInfo lineInfo = _lines[i];
			if (lineInfo.charCount == 0)
			{
				continue;
			}
			bool flag2 = flag && i != 0 && lineInfo.y + lineInfo.height > num3;
			AlignType alignType = format.align;
			alignType = ((htmlElement == null || htmlElement.charIndex != lineInfo.charIndex) ? format.align : htmlElement.format.align);
			float num6;
			if (_textDirection == RTLSupport.DirectionType.RTL)
			{
				num6 = alignType switch
				{
					AlignType.Center => (int)((num2 + lineInfo.width) / 2f), 
					AlignType.Right => num2, 
					_ => lineInfo.width + 4f, 
				};
				if (num6 > num2)
				{
					num6 = num2;
				}
				num4 = num6 - 2f;
			}
			else
			{
				num6 = alignType switch
				{
					AlignType.Center => (int)((num2 - lineInfo.width) / 2f), 
					AlignType.Right => num2 - lineInfo.width, 
					_ => 0f, 
				};
				if (num6 < 0f)
				{
					num6 = 0f;
				}
				num4 = 2f + num6;
			}
			int num7 = lineInfo.charCount;
			float num8 = num4;
			float num9 = num4;
			int num11;
			int num10 = (num11 = format.size);
			if (_textDirection != RTLSupport.DirectionType.UNKNOW)
			{
				text = _parsedText.Substring(lineInfo.charIndex, num7);
				text = ((_textDirection != RTLSupport.DirectionType.RTL) ? RTLSupport.ConvertLineL(text) : RTLSupport.ConvertLineR(text));
				num7 = text.Length;
			}
			short num13;
			for (int j = 0; j < num7; j++)
			{
				int num12 = lineInfo.charIndex + j;
				char c = text?[j] ?? _parsedText[num12];
				while (htmlElement != null && num12 == htmlElement.charIndex)
				{
					if (htmlElement.type == HtmlElementType.Text)
					{
						num13 = 0;
						if (format.underline != htmlElement.format.underline)
						{
							if (format.underline)
							{
								if (!flag2)
								{
									float num14 = ((_textDirection != RTLSupport.DirectionType.UNKNOW) ? (num8 - (flag ? Mathf.Clamp(num4, 2f, 2f + num2) : num4)) : ((flag ? Mathf.Clamp(num4, 2f, 2f + num2) : num4) - num8));
									if (num14 > 0f)
									{
										num13 += (short)_font.DrawLine((num8 < num4) ? num8 : num4, 0f - (lineInfo.y + lineInfo.baseline), num14, num11, 0, vertices, uvs, uvs2, colors);
									}
								}
								num11 = 0;
							}
							else
							{
								num8 = num4;
							}
						}
						if (format.strikethrough != htmlElement.format.strikethrough)
						{
							if (format.strikethrough)
							{
								if (!flag2)
								{
									float num15 = ((_textDirection != RTLSupport.DirectionType.UNKNOW) ? (num9 - (flag ? Mathf.Clamp(num4, 2f, 2f + num2) : num4)) : ((flag ? Mathf.Clamp(num4, 2f, 2f + num2) : num4) - num9));
									if (num15 > 0f)
									{
										num13 += (short)_font.DrawLine((num9 < num4) ? num9 : num4, 0f - (lineInfo.y + lineInfo.baseline), num15, num10, 1, vertices, uvs, uvs2, colors);
									}
								}
								num10 = int.MaxValue;
							}
							else
							{
								num9 = num4;
							}
						}
						if (num13 > 0 && _charPositions != null)
						{
							CharPosition value = _charPositions[_charPositions.Count - 1];
							value.vertCount += num13;
							_charPositions[_charPositions.Count - 1] = value;
						}
						format = htmlElement.format;
						num10 = Math.Min(num10, format.size);
						num11 = Math.Max(num11, format.size);
						_font.SetFormat(format, _fontSizeScale);
					}
					else if (htmlElement.type == HtmlElementType.Link)
					{
						htmlLink = (HtmlLink)htmlElement.htmlObject;
						if (htmlLink != null)
						{
							htmlElement.position = Vector2.zero;
							htmlLink.SetPosition(0f, 0f);
							startCharX = num4;
							startLine = i;
						}
					}
					else if (htmlElement.type == HtmlElementType.LinkEnd)
					{
						if (htmlLink != null)
						{
							htmlLink.SetArea(startLine, startCharX, i, num4);
							htmlLink = null;
						}
					}
					else
					{
						IHtmlObject htmlObject = htmlElement.htmlObject;
						if (htmlObject != null)
						{
							if (_textDirection == RTLSupport.DirectionType.RTL)
							{
								num4 -= htmlObject.width - 2f;
							}
							if (_charPositions != null)
							{
								CharPosition item = new CharPosition
								{
									lineIndex = (short)i,
									charIndex = _charPositions.Count,
									imgIndex = (short)(num5 + 1),
									offsetX = num4,
									width = (short)htmlObject.width
								};
								_charPositions.Add(item);
							}
							if (flag2 || (flag && (num4 < 2f || (num4 > 2f && num4 + htmlObject.width > _contentRect.width - 2f))))
							{
								htmlElement.status |= 1;
							}
							else
							{
								htmlElement.status &= 254;
							}
							htmlElement.position = new Vector2(num4 + 1f, lineInfo.y + lineInfo.baseline - htmlObject.height * 0.8f);
							htmlObject.SetPosition(htmlElement.position.x, htmlElement.position.y);
							num4 = ((_textDirection != RTLSupport.DirectionType.RTL) ? (num4 + (htmlObject.width + num + 2f)) : (num4 - num));
						}
					}
					if (htmlElement.isEntity)
					{
						c = '\0';
					}
					num5++;
					htmlElement = ((num5 >= count) ? null : _elements[num5]);
				}
				if (c == '\0')
				{
					continue;
				}
				if (_font.GetGlyph((c == '\t') ? ' ' : c, out var num16, out var _, out var _))
				{
					if (c == '\t')
					{
						num16 *= 4f;
					}
					if (_textDirection == RTLSupport.DirectionType.RTL)
					{
						if (flag2 || (flag && (num2 < 7f || num4 != num6 - 2f) && num4 < 1.5f))
						{
							num4 -= num + num16;
							continue;
						}
						num4 -= num16;
					}
					else if (flag2 || (flag && (num2 < 7f || num4 != 2f + num6) && num4 + num16 > _contentRect.width - 2f + 0.5f))
					{
						num4 += num + num16;
						continue;
					}
					num13 = (short)_font.DrawGlyph(num4, 0f - (lineInfo.y + lineInfo.baseline), vertices, uvs, uvs2, colors);
					if (_charPositions != null)
					{
						CharPosition item2 = new CharPosition
						{
							lineIndex = (short)i,
							charIndex = _charPositions.Count,
							vertCount = num13,
							offsetX = num4,
							width = (short)num16
						};
						_charPositions.Add(item2);
					}
					num4 = ((_textDirection != RTLSupport.DirectionType.RTL) ? (num4 + (num + num16)) : (num4 - num));
				}
				else
				{
					if (_charPositions != null)
					{
						CharPosition item3 = new CharPosition
						{
							lineIndex = (short)i,
							charIndex = _charPositions.Count,
							offsetX = num4
						};
						_charPositions.Add(item3);
					}
					num4 = ((_textDirection != RTLSupport.DirectionType.RTL) ? (num4 + num) : (num4 - num));
				}
			}
			if (flag2)
			{
				continue;
			}
			num13 = 0;
			if (format.underline)
			{
				float num18 = ((_textDirection != RTLSupport.DirectionType.UNKNOW) ? (num8 - (flag ? Mathf.Clamp(num4, 2f, 2f + num2) : num4)) : ((flag ? Mathf.Clamp(num4, 2f, 2f + num2) : num4) - num8));
				if (num18 > 0f)
				{
					num13 += (short)_font.DrawLine((num8 < num4) ? num8 : num4, 0f - (lineInfo.y + lineInfo.baseline), num18, num11, 0, vertices, uvs, uvs2, colors);
				}
			}
			if (format.strikethrough)
			{
				float num19 = ((_textDirection != RTLSupport.DirectionType.UNKNOW) ? (num9 - (flag ? Mathf.Clamp(num4, 2f, 2f + num2) : num4)) : ((flag ? Mathf.Clamp(num4, 2f, 2f + num2) : num4) - num9));
				if (num19 > 0f)
				{
					num13 += (short)_font.DrawLine((num9 < num4) ? num9 : num4, 0f - (lineInfo.y + lineInfo.baseline), num19, num10, 1, vertices, uvs, uvs2, colors);
				}
			}
			if (num13 > 0 && _charPositions != null)
			{
				CharPosition value2 = _charPositions[_charPositions.Count - 1];
				value2.vertCount += num13;
				_charPositions[_charPositions.Count - 1] = value2;
			}
		}
		if (htmlElement != null && htmlElement.type == HtmlElementType.LinkEnd)
		{
			htmlLink?.SetArea(startLine, startCharX, count2 - 1, num4);
		}
		if (_charPositions != null)
		{
			CharPosition item4 = new CharPosition
			{
				lineIndex = (short)(count2 - 1),
				charIndex = _charPositions.Count,
				offsetX = num4
			};
			_charPositions.Add(item4);
		}
		int num20 = vertices.Count;
		if (num20 > 65000)
		{
			Debug.LogWarning("Text is too large. A mesh may not have more than 65000 vertices.");
			vertices.RemoveRange(65000, num20 - 65000);
			colors.RemoveRange(65000, num20 - 65000);
			uvs.RemoveRange(65000, num20 - 65000);
			if (uvs2.Count > 0)
			{
				uvs2.RemoveRange(65000, num20 - 65000);
			}
			num20 = 65000;
		}
		if (_font.customOutline)
		{
			bool flag3 = _textFormat.shadowOffset.x != 0f || _textFormat.shadowOffset.y != 0f;
			int num21 = num20;
			int num22 = 0;
			if (_textFormat.outline != 0f)
			{
				num22 = (UIConfig.enhancedTextOutlineEffect ? 8 : 4);
				num21 += num20 * num22;
			}
			if (flag3)
			{
				num21 += num20;
			}
			if (num21 > 65000)
			{
				Debug.LogWarning("Text is too large. Outline/shadow effect cannot be completed.");
				num21 = num20;
			}
			if (num21 != num20)
			{
				VertexBuffer vertexBuffer = VertexBuffer.Begin();
				List<Vector3> vertices2 = vertexBuffer.vertices;
				List<Color32> colors2 = vertexBuffer.colors;
				Color32 item5 = _textFormat.outlineColor;
				float outline = _textFormat.outline;
				if (outline != 0f)
				{
					for (int k = 0; k < num22; k++)
					{
						for (int l = 0; l < num20; l++)
						{
							Vector3 vector = vertices[l];
							vertices2.Add(new Vector3(vector.x + STROKE_OFFSET[k * 2] * outline, vector.y + STROKE_OFFSET[k * 2 + 1] * outline, 0f));
							colors2.Add(item5);
						}
						vertexBuffer.uvs.AddRange(uvs);
						if (uvs2.Count > 0)
						{
							vertexBuffer.uvs2.AddRange(uvs2);
						}
					}
				}
				if (flag3)
				{
					item5 = _textFormat.shadowColor;
					Vector2 vector2 = _textFormat.shadowOffset;
					for (int m = 0; m < num20; m++)
					{
						Vector3 vector3 = vertices[m];
						vertices2.Add(new Vector3(vector3.x + vector2.x, vector3.y - vector2.y, 0f));
						colors2.Add(item5);
					}
					vertexBuffer.uvs.AddRange(uvs);
					if (uvs2.Count > 0)
					{
						vertexBuffer.uvs2.AddRange(uvs2);
					}
				}
				vb.Insert(vertexBuffer);
				vertexBuffer.End();
			}
		}
		vb.AddTriangles();
		if (_richTextField != null)
		{
			_richTextField.RefreshObjects();
		}
	}

	private void Cleanup()
	{
		if (_richTextField != null)
		{
			_richTextField.CleanupObjects();
		}
		HtmlElement.ReturnElements(_elements);
		LineInfo.Return(_lines);
		_textWidth = 0f;
		_textHeight = 0f;
		_parsedText = string.Empty;
		_textDirection = RTLSupport.DirectionType.UNKNOW;
		if (_charPositions != null)
		{
			_charPositions.Clear();
		}
	}

	private void ApplyVertAlign()
	{
		float yOffset = _yOffset;
		if (_autoSize == AutoSizeType.Both || _autoSize == AutoSizeType.Height || _verticalAlign == VertAlignType.Top)
		{
			_yOffset = 0f;
		}
		else
		{
			float num = ((_textHeight != 0f || _lines.Count <= 0) ? (_contentRect.height - _textHeight) : (_contentRect.height - _lines[0].height));
			if (num < 0f)
			{
				num = 0f;
			}
			if (_verticalAlign == VertAlignType.Middle)
			{
				_yOffset = (int)(num / 2f);
			}
			else
			{
				_yOffset = num;
			}
		}
		if (yOffset != _yOffset)
		{
			int count = _lines.Count;
			for (int i = 0; i < count; i++)
			{
				_lines[i].y = _lines[i].y2 + _yOffset;
			}
			base.graphics.SetMeshDirty();
		}
	}
}
