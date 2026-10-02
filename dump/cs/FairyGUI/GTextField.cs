using System.Collections.Generic;
using System.Text;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GTextField : GObject, ITextColorGear, IColorGear
{
	private AlignType _scrollTextAlign;

	private GTweener tweener;

	protected TextField _textField;

	protected string _text;

	protected bool _ubbEnabled;

	protected bool _updatingSize;

	protected Dictionary<string, string> _templateVars;

	public override string text
	{
		get
		{
			if (this is GTextInput)
			{
				_text = ((GTextInput)this).inputTextField.text;
			}
			return _text;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			_text = value;
			SetTextFieldText();
			UpdateSize();
			UpdateGear(6);
		}
	}

	public Dictionary<string, string> templateVars
	{
		get
		{
			return _templateVars;
		}
		set
		{
			if (_templateVars != null || value != null)
			{
				_templateVars = value;
				FlushVars();
			}
		}
	}

	public TextFormat textFormat
	{
		get
		{
			return _textField.textFormat;
		}
		set
		{
			_textField.textFormat = value;
			if (!underConstruct)
			{
				UpdateSize();
			}
		}
	}

	public Color color
	{
		get
		{
			return _textField.textFormat.color;
		}
		set
		{
			if (_textField.textFormat.color != value)
			{
				TextFormat textFormat = _textField.textFormat;
				textFormat.color = value;
				_textField.textFormat = textFormat;
				UpdateGear(4);
			}
		}
	}

	public AlignType align
	{
		get
		{
			return _textField.align;
		}
		set
		{
			_textField.align = value;
		}
	}

	public VertAlignType verticalAlign
	{
		get
		{
			return _textField.verticalAlign;
		}
		set
		{
			_textField.verticalAlign = value;
		}
	}

	public bool singleLine
	{
		get
		{
			return _textField.singleLine;
		}
		set
		{
			_textField.singleLine = value;
		}
	}

	public float stroke
	{
		get
		{
			return _textField.stroke;
		}
		set
		{
			_textField.stroke = value;
		}
	}

	public Color strokeColor
	{
		get
		{
			return _textField.strokeColor;
		}
		set
		{
			_textField.strokeColor = value;
			UpdateGear(4);
		}
	}

	public Vector2 shadowOffset
	{
		get
		{
			return _textField.shadowOffset;
		}
		set
		{
			_textField.shadowOffset = value;
		}
	}

	public bool UBBEnabled
	{
		get
		{
			return _ubbEnabled;
		}
		set
		{
			_ubbEnabled = value;
		}
	}

	public AutoSizeType autoSize
	{
		get
		{
			return _textField.autoSize;
		}
		set
		{
			_textField.autoSize = value;
			if (value == AutoSizeType.Both)
			{
				_textField.wordWrap = false;
				if (!underConstruct)
				{
					SetSize(_textField.textWidth, _textField.textHeight);
				}
				return;
			}
			_textField.wordWrap = true;
			if (value == AutoSizeType.Height)
			{
				if (!underConstruct)
				{
					base.displayObject.width = base.width;
					base.height = _textField.textHeight;
				}
			}
			else
			{
				base.displayObject.SetSize(base.width, base.height);
			}
		}
	}

	public float textWidth => _textField.textWidth;

	public float textHeight => _textField.textHeight;

	public void TryScrollTextField(string _title, AlignType type = AlignType.Center)
	{
		_scrollTextAlign = type;
		text = _title;
		TryScrollTextField();
	}

	private void TryScrollTextField()
	{
		StopScrollTextField();
		if (base.displayObject.gOwner == null || base.parent == null)
		{
			return;
		}
		SetPivot(0.5f, 0.5f, asAnchor: true);
		if (base.width <= base.parent.width)
		{
			if (_scrollTextAlign == AlignType.Center)
			{
				base.x = base.parent.width * 0.5f;
			}
			else if (_scrollTextAlign == AlignType.Left)
			{
				base.x = base.width * 0.5f;
			}
			else if (_scrollTextAlign == AlignType.Right)
			{
				base.x = base.parent.width - base.width * 0.5f;
			}
		}
		else
		{
			base.x = base.width * 0.5f;
			float endValue = base.parent.width - base.width * 0.5f;
			float duration = (float)text.Length * 0.5f;
			if (base.displayObject.gameObject.activeInHierarchy)
			{
				tweener = TweenMoveX(endValue, duration).OnComplete(TryScrollTextField).SetEase(EaseType.QuintInOut);
			}
		}
	}

	private void StopScrollTextField()
	{
		tweener?.Kill();
		tweener = null;
	}

	public override void Dispose()
	{
		StopScrollTextField();
		base.Dispose();
	}

	public void SetTextAdaptiveMaxWidth(string content, float maxWidth)
	{
		singleLine = true;
		autoSize = AutoSizeType.Both;
		text = content;
		if (base.width > maxWidth)
		{
			autoSize = AutoSizeType.Shrink;
			base.width = maxWidth;
		}
	}

	public void SetTextAdaptiveMinHeight(float _minHeight)
	{
		autoSize = AutoSizeType.Height;
		if (base.height <= _minHeight)
		{
			autoSize = AutoSizeType.None;
			base.height = _minHeight;
		}
		else
		{
			autoSize = AutoSizeType.Height;
		}
	}

	public GTextField()
	{
		TextFormat textFormat = _textField.textFormat;
		textFormat.font = UIConfig.defaultFont;
		textFormat.size = 12;
		textFormat.color = Color.black;
		textFormat.lineSpacing = 3;
		textFormat.letterSpacing = 0;
		_textField.textFormat = textFormat;
		_text = string.Empty;
		_textField.autoSize = AutoSizeType.Both;
		_textField.wordWrap = false;
	}

	protected override void CreateDisplayObject()
	{
		_textField = new TextField();
		_textField.gOwner = this;
		base.displayObject = _textField;
	}

	protected virtual void SetTextFieldText()
	{
		string text = _text;
		if (_templateVars != null)
		{
			text = ParseTemplate(text);
		}
		_textField.maxWidth = maxWidth;
		if (_ubbEnabled)
		{
			_textField.htmlText = UBBParser.inst.Parse(XMLUtils.EncodeString(text));
		}
		else
		{
			_textField.text = text;
		}
	}

	public GTextField SetVar(string name, string value)
	{
		if (_templateVars == null)
		{
			_templateVars = new Dictionary<string, string>();
		}
		_templateVars[name] = value;
		return this;
	}

	public void FlushVars()
	{
		SetTextFieldText();
		UpdateSize();
	}

	public bool HasCharacter(char ch)
	{
		return _textField.HasCharacter(ch);
	}

	protected string ParseTemplate(string template)
	{
		int num = 0;
		int num2 = 0;
		StringBuilder stringBuilder = new StringBuilder();
		while ((num2 = template.IndexOf('{', num)) != -1)
		{
			if (num2 > 0 && template[num2 - 1] == '\\')
			{
				stringBuilder.Append(template, num, num2 - num - 1);
				stringBuilder.Append('{');
				num = num2 + 1;
				continue;
			}
			stringBuilder.Append(template, num, num2 - num);
			num = num2;
			num2 = template.IndexOf('}', num);
			if (num2 == -1)
			{
				break;
			}
			if (num2 == num + 1)
			{
				stringBuilder.Append(template, num, 2);
				num = num2 + 1;
				continue;
			}
			string text = template.Substring(num + 1, num2 - num - 1);
			int num3 = text.IndexOf('=');
			string value;
			if (num3 != -1)
			{
				if (!_templateVars.TryGetValue(text.Substring(0, num3), out value))
				{
					value = text.Substring(num3 + 1);
				}
			}
			else if (!_templateVars.TryGetValue(text, out value))
			{
				value = "";
			}
			stringBuilder.Append(value);
			num = num2 + 1;
		}
		if (num < template.Length)
		{
			stringBuilder.Append(template, num, template.Length - num);
		}
		return stringBuilder.ToString();
	}

	protected void UpdateSize()
	{
		if (!_updatingSize)
		{
			_updatingSize = true;
			if (_textField.autoSize == AutoSizeType.Both)
			{
				base.size = base.displayObject.size;
				InvalidateBatchingState();
			}
			else if (_textField.autoSize == AutoSizeType.Height)
			{
				base.height = base.displayObject.height;
				InvalidateBatchingState();
			}
			_updatingSize = false;
		}
	}

	protected override void HandleSizeChanged()
	{
		if (_updatingSize)
		{
			return;
		}
		if (underConstruct)
		{
			base.displayObject.SetSize(base.width, base.height);
		}
		else
		{
			if (_textField.autoSize == AutoSizeType.Both)
			{
				return;
			}
			if (_textField.autoSize == AutoSizeType.Height)
			{
				base.displayObject.width = base.width;
				if (_text != string.Empty)
				{
					SetSizeDirectly(base.width, base.displayObject.height);
				}
			}
			else
			{
				base.displayObject.SetSize(base.width, base.height);
			}
		}
	}

	public override void Setup_BeforeAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_BeforeAdd(buffer, beginPos);
		buffer.Seek(beginPos, 5);
		TextFormat textFormat = _textField.textFormat;
		textFormat.font = buffer.ReadS();
		textFormat.size = buffer.ReadShort();
		textFormat.color = buffer.ReadColor();
		align = (AlignType)buffer.ReadByte();
		verticalAlign = (VertAlignType)buffer.ReadByte();
		textFormat.lineSpacing = buffer.ReadShort();
		textFormat.letterSpacing = buffer.ReadShort();
		_ubbEnabled = buffer.ReadBool();
		autoSize = (AutoSizeType)buffer.ReadByte();
		textFormat.underline = buffer.ReadBool();
		textFormat.italic = buffer.ReadBool();
		textFormat.bold = buffer.ReadBool();
		singleLine = buffer.ReadBool();
		if (buffer.ReadBool())
		{
			textFormat.outlineColor = buffer.ReadColor();
			textFormat.outline = buffer.ReadFloat();
		}
		if (buffer.ReadBool())
		{
			textFormat.shadowColor = buffer.ReadColor();
			float num = buffer.ReadFloat();
			float num2 = buffer.ReadFloat();
			textFormat.shadowOffset = new Vector2(num, num2);
		}
		if (buffer.ReadBool())
		{
			_templateVars = new Dictionary<string, string>();
		}
		if (buffer.version >= 3)
		{
			textFormat.strikethrough = buffer.ReadBool();
			textFormat.faceDilate = buffer.ReadFloat();
			textFormat.outlineSoftness = buffer.ReadFloat();
			textFormat.underlaySoftness = buffer.ReadFloat();
		}
		_textField.textFormat = textFormat;
	}

	public override void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_AfterAdd(buffer, beginPos);
		buffer.Seek(beginPos, 6);
		string text = buffer.ReadS();
		if (text != null)
		{
			this.text = text;
		}
	}
}
