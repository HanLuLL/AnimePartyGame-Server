using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class InputTextField : RichTextField
{
	public static Action<InputTextField, string> onCopy;

	public static Action<InputTextField> onPaste;

	public static PopupMenu contextMenu;

	private string _text;

	private string _restrict;

	private Regex _restrictPattern;

	private bool _displayAsPassword;

	private string _promptText;

	private string _decodedPromptText;

	private int _border;

	private int _corner;

	private Color _borderColor;

	private Color _backgroundColor;

	private bool _editable;

	private bool _editing;

	private int _caretPosition;

	private int _selectionStart;

	private int _composing;

	private char _highSurrogateChar;

	private string _textBeforeEdit;

	private EventListener _onChanged;

	private EventListener _onSubmit;

	public bool submitOnEnter;

	private Shape _caret;

	private SelectionShape _selectionShape;

	private float _nextBlink;

	private const int GUTTER_X = 2;

	private const int GUTTER_Y = 2;

	public int maxLength { get; set; }

	public bool keyboardInput { get; set; }

	public int keyboardType { get; set; }

	public bool hideInput { get; set; }

	public bool disableIME { get; set; }

	public bool mouseWheelEnabled { get; set; }

	public EventListener onChanged => _onChanged ?? (_onChanged = new EventListener(this, "onChanged"));

	public EventListener onSubmit => _onSubmit ?? (_onSubmit = new EventListener(this, "onSubmit"));

	public override string text
	{
		get
		{
			return _text;
		}
		set
		{
			_text = value;
			ClearSelection();
			UpdateText();
		}
	}

	public override TextFormat textFormat
	{
		get
		{
			return base.textFormat;
		}
		set
		{
			base.textFormat = value;
			if (_editing)
			{
				_caret.height = base.textField.textFormat.size;
				_caret.DrawRect(0f, Color.clear, base.textField.textFormat.color);
			}
		}
	}

	public string restrict
	{
		get
		{
			return _restrict;
		}
		set
		{
			_restrict = value;
			if (string.IsNullOrEmpty(_restrict))
			{
				_restrictPattern = null;
			}
			else
			{
				_restrictPattern = new Regex(value);
			}
		}
	}

	public int caretPosition
	{
		get
		{
			base.textField.Redraw();
			return _caretPosition;
		}
		set
		{
			SetSelection(value, 0);
		}
	}

	public int selectionBeginIndex
	{
		get
		{
			if (_selectionStart >= _caretPosition)
			{
				return _caretPosition;
			}
			return _selectionStart;
		}
	}

	public int selectionEndIndex
	{
		get
		{
			if (_selectionStart >= _caretPosition)
			{
				return _selectionStart;
			}
			return _caretPosition;
		}
	}

	public string promptText
	{
		get
		{
			return _promptText;
		}
		set
		{
			_promptText = value;
			if (!string.IsNullOrEmpty(_promptText))
			{
				_decodedPromptText = UBBParser.inst.Parse(XMLUtils.EncodeString(_promptText));
			}
			else
			{
				_decodedPromptText = null;
			}
			UpdateText();
		}
	}

	public bool displayAsPassword
	{
		get
		{
			return _displayAsPassword;
		}
		set
		{
			if (_displayAsPassword != value)
			{
				_displayAsPassword = value;
				UpdateText();
			}
		}
	}

	public bool editable
	{
		get
		{
			return _editable;
		}
		set
		{
			_editable = value;
			if (_caret != null)
			{
				_caret.visible = _editable;
			}
		}
	}

	public int border
	{
		get
		{
			return _border;
		}
		set
		{
			_border = value;
			UpdateShape();
		}
	}

	public int corner
	{
		get
		{
			return _corner;
		}
		set
		{
			_corner = value;
			UpdateShape();
		}
	}

	public Color borderColor
	{
		get
		{
			return _borderColor;
		}
		set
		{
			_borderColor = value;
			UpdateShape();
		}
	}

	public Color backgroundColor
	{
		get
		{
			return _backgroundColor;
		}
		set
		{
			_backgroundColor = value;
			UpdateShape();
		}
	}

	public InputTextField()
	{
		base.gameObject.name = "InputTextField";
		_text = string.Empty;
		maxLength = 0;
		_editable = true;
		_composing = 0;
		keyboardInput = Stage.keyboardInput;
		_borderColor = Color.black;
		_backgroundColor = Color.clear;
		mouseWheelEnabled = true;
		base.tabStop = true;
		base.cursor = "text-ibeam";
		hitArea = new RectHitTest();
		touchChildren = false;
		base.onFocusIn.Add(__focusIn);
		base.onFocusOut.AddCapture(__focusOut);
		base.onKeyDown.Add(__keydown);
		base.onTouchBegin.AddCapture(__touchBegin);
		base.onTouchMove.AddCapture(__touchMove);
		base.onMouseWheel.Add(__mouseWheel);
		base.onClick.Add(__click);
		base.onRightClick.Add(__rightClick);
	}

	private void UpdateShape()
	{
		if (_border > 0 || _backgroundColor.a > 0f)
		{
			CreateGraphics();
			base.graphics.enabled = true;
			RoundedRectMesh meshFactory = base.graphics.GetMeshFactory<RoundedRectMesh>();
			meshFactory.lineWidth = _border;
			meshFactory.lineColor = _borderColor;
			meshFactory.fillColor = _backgroundColor;
			meshFactory.topLeftRadius = (meshFactory.topRightRadius = (meshFactory.bottomLeftRadius = (meshFactory.bottomRightRadius = corner)));
			base.graphics.SetMeshDirty();
		}
		else if (base.graphics != null)
		{
			base.graphics.enabled = false;
		}
	}

	public void SetSelection(int start, int length)
	{
		if (!_editing)
		{
			Stage.inst.focus = this;
		}
		_selectionStart = start;
		_caretPosition = ((length < 0) ? int.MaxValue : (start + length));
		if (!base.textField.Redraw())
		{
			int count = base.textField.charPositions.Count;
			if (_caretPosition >= count)
			{
				_caretPosition = count - 1;
			}
			if (_selectionStart >= count)
			{
				_selectionStart = count - 1;
			}
			UpdateCaret();
		}
	}

	public void ReplaceSelection(string value)
	{
		if (keyboardInput && Stage.keyboardInput && !Stage.inst.keyboard.supportsCaret)
		{
			this.text = _text + value;
			OnChanged();
			return;
		}
		if (!_editing)
		{
			Stage.inst.focus = this;
		}
		base.textField.Redraw();
		int endIndex;
		int selectionStart;
		if (_selectionStart != _caretPosition)
		{
			if (_selectionStart < _caretPosition)
			{
				endIndex = _selectionStart;
				selectionStart = _caretPosition;
				_caretPosition = _selectionStart;
			}
			else
			{
				endIndex = _caretPosition;
				selectionStart = _selectionStart;
				_selectionStart = _caretPosition;
			}
		}
		else
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			endIndex = (selectionStart = _caretPosition);
		}
		StringBuilder stringBuilder = new StringBuilder();
		GetPartialText(0, endIndex, stringBuilder);
		if (!string.IsNullOrEmpty(value))
		{
			value = ValidateInput(value);
			stringBuilder.Append(value);
			_caretPosition += GetTextlength(value);
		}
		GetPartialText(selectionStart + _composing, -1, stringBuilder);
		string text = stringBuilder.ToString();
		if (maxLength > 0)
		{
			string text2 = TruncateText(text, maxLength);
			if (text2.Length != text.Length)
			{
				_caretPosition += text2.Length - text.Length;
			}
			text = text2;
		}
		this.text = text;
		OnChanged();
	}

	public void ReplaceText(string value)
	{
		if (!(value == _text))
		{
			if (value == null)
			{
				value = string.Empty;
			}
			value = ValidateInput(value);
			if (maxLength > 0)
			{
				value = TruncateText(value, maxLength);
			}
			_caretPosition = value.Length;
			text = value;
			OnChanged();
		}
	}

	private void GetPartialText(int startIndex, int endIndex, StringBuilder buffer)
	{
		int count = base.textField.htmlElements.Count;
		int num = startIndex;
		string text = ((!_displayAsPassword) ? base.textField.parsedText : _text);
		if (endIndex < 0)
		{
			endIndex = text.Length;
		}
		for (int i = 0; i < count; i++)
		{
			HtmlElement htmlElement = base.textField.htmlElements[i];
			if (htmlElement.htmlObject != null && htmlElement.text != null && htmlElement.charIndex >= startIndex && htmlElement.charIndex < endIndex)
			{
				buffer.Append(text.Substring(num, htmlElement.charIndex - num));
				buffer.Append(htmlElement.text);
				num = htmlElement.charIndex + 1;
			}
		}
		if (num < text.Length)
		{
			buffer.Append(text.Substring(num, endIndex - num));
		}
	}

	private int GetTextlength(string value)
	{
		int length = value.Length;
		int num = length;
		for (int i = 0; i < length; i++)
		{
			if (char.IsHighSurrogate(value[i]))
			{
				num--;
			}
		}
		return num;
	}

	private string TruncateText(string value, int length)
	{
		int length2 = value.Length;
		int num = 0;
		int num2 = 0;
		while (num2 < length2)
		{
			if (num == length)
			{
				return value.Substring(0, num2);
			}
			if (char.IsHighSurrogate(value[num2]))
			{
				num2++;
			}
			num2++;
			num++;
		}
		return value;
	}

	private string ValidateInput(string source)
	{
		if (_restrict != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Match match = _restrictPattern.Match(source);
			int num = 0;
			while (match != Match.Empty)
			{
				if (match.Index != num)
				{
					for (int i = num; i < match.Index; i++)
					{
						if (source[i] == '\n' || source[i] == '\t')
						{
							stringBuilder.Append(source[i]);
						}
					}
				}
				string text = match.ToString();
				num = match.Index + text.Length;
				stringBuilder.Append(text);
				match = match.NextMatch();
			}
			for (int j = num; j < source.Length; j++)
			{
				if (source[j] == '\n' || source[j] == '\t')
				{
					stringBuilder.Append(source[j]);
				}
			}
			return stringBuilder.ToString();
		}
		return source;
	}

	private void UpdateText()
	{
		if (!_editing && _text.Length == 0 && !string.IsNullOrEmpty(_decodedPromptText))
		{
			base.textField.htmlText = _decodedPromptText;
			return;
		}
		if (_displayAsPassword)
		{
			base.textField.text = EncodePasswordText(_text);
		}
		else
		{
			base.textField.text = _text;
		}
		_composing = Input.compositionString.Length;
		if (_composing > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			GetPartialText(0, _caretPosition, stringBuilder);
			stringBuilder.Append(Input.compositionString);
			GetPartialText(_caretPosition, -1, stringBuilder);
			base.textField.text = stringBuilder.ToString();
		}
	}

	private string EncodePasswordText(string value)
	{
		int length = value.Length;
		StringBuilder stringBuilder = new StringBuilder(length);
		for (int i = 0; i < length; i++)
		{
			char c = value[i];
			if (c == '\n')
			{
				stringBuilder.Append(c);
				continue;
			}
			if (char.IsHighSurrogate(c))
			{
				i++;
			}
			stringBuilder.Append("*");
		}
		return stringBuilder.ToString();
	}

	private void ClearSelection()
	{
		if (_selectionStart != _caretPosition)
		{
			if (_selectionShape != null)
			{
				_selectionShape.Clear();
			}
			_selectionStart = _caretPosition;
		}
	}

	public string GetSelection()
	{
		if (_selectionStart == _caretPosition)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		if (_selectionStart < _caretPosition)
		{
			GetPartialText(_selectionStart, _caretPosition, stringBuilder);
		}
		else
		{
			GetPartialText(_caretPosition, _selectionStart, stringBuilder);
		}
		return stringBuilder.ToString();
	}

	private void Scroll(int hScroll, int vScroll)
	{
		vScroll = Mathf.Clamp(vScroll, 0, base.textField.lines.Count - 1);
		TextField.LineInfo lineInfo = base.textField.lines[vScroll];
		hScroll = Mathf.Clamp(hScroll, 0, lineInfo.charCount - 1);
		TextField.CharPosition charPosition = GetCharPosition(lineInfo.charIndex + hScroll);
		Vector2 charLocation = GetCharLocation(charPosition);
		MoveContent(new Vector2(2f - charLocation.x, 2f - charLocation.y), forceUpdate: false);
	}

	private void AdjustCaret(TextField.CharPosition cp, bool moveSelectionHeader = false)
	{
		_caretPosition = cp.charIndex;
		if (moveSelectionHeader)
		{
			_selectionStart = _caretPosition;
		}
		UpdateCaret();
	}

	private void UpdateCaret(bool forceUpdate = false)
	{
		TextField.CharPosition cp = ((!_editing) ? GetCharPosition(_caretPosition) : GetCharPosition(_caretPosition + Input.compositionString.Length));
		Vector2 charLocation = GetCharLocation(cp);
		TextField.LineInfo lineInfo = base.textField.lines[cp.lineIndex];
		Vector2 vector = charLocation + base.textField.xy;
		if (vector.x < (float)base.textField.textFormat.size)
		{
			vector.x += Mathf.Min(50f, _contentRect.width * 0.5f);
		}
		else if (vector.x > _contentRect.width - 2f - (float)base.textField.textFormat.size)
		{
			vector.x -= Mathf.Min(50f, _contentRect.width * 0.5f);
		}
		if (vector.x < 2f)
		{
			vector.x = 2f;
		}
		else if (vector.x > _contentRect.width - 2f)
		{
			vector.x = Mathf.Max(2f, _contentRect.width - 2f);
		}
		if (vector.y < 2f)
		{
			vector.y = 2f;
		}
		else if (vector.y + lineInfo.height >= _contentRect.height - 2f)
		{
			vector.y = Mathf.Max(2f, _contentRect.height - lineInfo.height - 2f);
		}
		MoveContent(vector - charLocation, forceUpdate);
		if (!_editing)
		{
			return;
		}
		_caret.position = base.textField.xy + charLocation;
		_caret.height = ((lineInfo.height > 0f) ? lineInfo.height : ((float)base.textField.textFormat.size));
		if (_editable)
		{
			Vector2 vector2 = _caret.LocalToWorld(new Vector2(0f, _caret.height));
			vector2 = StageCamera.main.WorldToScreenPoint(vector2);
			if (Stage.devicePixelRatio == 1f)
			{
				vector2.y = (float)Screen.height - vector2.y;
				vector2 /= Stage.devicePixelRatio;
				Input.compositionCursorPos = vector2 + new Vector2(0f, 20f);
			}
			else
			{
				Input.compositionCursorPos = vector2 - new Vector2(0f, 20f);
			}
		}
		_nextBlink = Time.time + 0.5f;
		_caret.graphics.enabled = true;
		UpdateSelection(cp);
	}

	private void MoveContent(Vector2 pos, bool forceUpdate)
	{
		float num = base.textField.x;
		float num2 = base.textField.y;
		float num3 = pos.x;
		float num4 = pos.y;
		float num5 = _contentRect.width - 1f;
		if (num5 - num3 > base.textField.textWidth)
		{
			num3 = num5 - base.textField.textWidth;
		}
		if (_contentRect.height - num4 > base.textField.textHeight)
		{
			num4 = _contentRect.height - base.textField.textHeight;
		}
		if (num3 > 0f)
		{
			num3 = 0f;
		}
		if (num4 > 0f)
		{
			num4 = 0f;
		}
		num3 = (int)num3;
		num4 = (int)num4;
		if (!(num3 != num || num4 != num2 || forceUpdate))
		{
			return;
		}
		if (_caret != null)
		{
			_caret.SetXY(num3 + _caret.x - num, num4 + _caret.y - num2);
			_selectionShape.SetXY(num3, num4);
		}
		base.textField.SetXY(num3, num4);
		List<HtmlElement> htmlElements = base.textField.htmlElements;
		int count = htmlElements.Count;
		for (int i = 0; i < count; i++)
		{
			HtmlElement htmlElement = htmlElements[i];
			if (htmlElement.htmlObject != null)
			{
				htmlElement.htmlObject.SetPosition(htmlElement.position.x + num3, htmlElement.position.y + num4);
			}
		}
	}

	private void UpdateSelection(TextField.CharPosition cp)
	{
		if (_selectionStart == _caretPosition)
		{
			_selectionShape.Clear();
			return;
		}
		TextField.CharPosition charPosition;
		if (_editing && Input.compositionString.Length > 0)
		{
			if (_selectionStart < _caretPosition)
			{
				cp = GetCharPosition(_caretPosition);
				charPosition = GetCharPosition(_selectionStart);
			}
			else
			{
				charPosition = GetCharPosition(_selectionStart + Input.compositionString.Length);
			}
		}
		else
		{
			charPosition = GetCharPosition(_selectionStart);
		}
		if (charPosition.charIndex > cp.charIndex)
		{
			TextField.CharPosition charPosition2 = charPosition;
			charPosition = cp;
			cp = charPosition2;
		}
		Vector2 charLocation = GetCharLocation(charPosition);
		Vector2 charLocation2 = GetCharLocation(cp);
		_selectionShape.rects.Clear();
		base.textField.GetLinesShape(charPosition.lineIndex, charLocation.x, cp.lineIndex, charLocation2.x, clipped: false, _selectionShape.rects);
		_selectionShape.Refresh();
	}

	private TextField.CharPosition GetCharPosition(int caretIndex)
	{
		if (caretIndex < 0)
		{
			caretIndex = 0;
		}
		else if (caretIndex >= base.textField.charPositions.Count)
		{
			caretIndex = base.textField.charPositions.Count - 1;
		}
		return base.textField.charPositions[caretIndex];
	}

	private TextField.CharPosition GetCharPosition(Vector2 location)
	{
		if (base.textField.charPositions.Count <= 1)
		{
			return base.textField.charPositions[0];
		}
		location.x -= base.textField.x;
		location.y -= base.textField.y;
		List<TextField.LineInfo> lines = base.textField.lines;
		int count = lines.Count;
		int i;
		for (i = 0; i < count; i++)
		{
			TextField.LineInfo lineInfo = lines[i];
			if (lineInfo.y + lineInfo.height > location.y)
			{
				break;
			}
		}
		if (i == count)
		{
			i = count - 1;
		}
		int num = i;
		count = base.textField.charPositions.Count;
		int num2 = -1;
		for (i = 0; i < count; i++)
		{
			TextField.CharPosition result = base.textField.charPositions[i];
			if (result.lineIndex == num)
			{
				if (num2 == -1)
				{
					num2 = i;
				}
				if (result.offsetX + (float)result.width * 0.5f > location.x)
				{
					return result;
				}
			}
			else if (num2 != -1)
			{
				return result;
			}
		}
		return base.textField.charPositions[i - 1];
	}

	private Vector2 GetCharLocation(TextField.CharPosition cp)
	{
		TextField.LineInfo lineInfo = base.textField.lines[cp.lineIndex];
		Vector2 result = default(Vector2);
		if (lineInfo.charCount == 0 || base.textField.charPositions.Count == 0)
		{
			if (base.textField.align == AlignType.Center)
			{
				result.x = (int)(_contentRect.width / 2f);
			}
			else
			{
				result.x = 2f;
			}
		}
		else
		{
			result.x = base.textField.charPositions[Math.Min(cp.charIndex, base.textField.charPositions.Count - 1)].offsetX;
		}
		result.y = lineInfo.y;
		return result;
	}

	internal override void RefreshObjects()
	{
		base.RefreshObjects();
		if (_editing)
		{
			SetChildIndex(_selectionShape, 0);
			SetChildIndex(_caret, base.numChildren - 1);
		}
		int count = base.textField.charPositions.Count;
		if (_caretPosition >= count)
		{
			_caretPosition = count - 1;
		}
		if (_selectionStart >= count)
		{
			_selectionStart = count - 1;
		}
		UpdateCaret(forceUpdate: true);
	}

	protected void OnChanged()
	{
		DispatchEvent("onChanged", null);
		TextInputHistory.inst.MarkChanged(this);
	}

	protected override void OnSizeChanged()
	{
		base.OnSizeChanged();
		Rect contentRect = _contentRect;
		contentRect.x += 2f;
		contentRect.y += 2f;
		contentRect.width -= 4f;
		contentRect.height -= 4f;
		base.clipRect = contentRect;
		((RectHitTest)hitArea).rect = _contentRect;
	}

	public override void Update(UpdateContext context)
	{
		base.Update(context);
		if (_editing && _nextBlink < Time.time)
		{
			_nextBlink = Time.time + 0.5f;
			_caret.graphics.enabled = !_caret.graphics.enabled;
		}
	}

	public override void Dispose()
	{
		if ((_flags & Flags.Disposed) == 0)
		{
			_editing = false;
			if (_caret != null)
			{
				_caret.Dispose();
				_selectionShape.Dispose();
			}
			base.Dispose();
		}
	}

	private void DoCopy(string value)
	{
		if (onCopy != null)
		{
			onCopy(this, value);
		}
	}

	private void DoPaste()
	{
		if (onPaste != null)
		{
			onPaste(this);
		}
	}

	private void CreateCaret()
	{
		_caret = new Shape();
		_caret.gameObject.name = "Caret";
		_caret.touchable = false;
		_caret._flags |= Flags.SkipBatching;
		_caret.xy = base.textField.xy;
		_selectionShape = new SelectionShape();
		_selectionShape.gameObject.name = "Selection";
		_selectionShape.color = UIConfig.inputHighlightColor;
		_selectionShape._flags |= Flags.SkipBatching;
		_selectionShape.touchable = false;
		_selectionShape.xy = base.textField.xy;
	}

	private void __touchBegin(EventContext context)
	{
		if (_editing && base.textField.charPositions.Count > 1 && (!keyboardInput || !Stage.keyboardInput || Stage.inst.keyboard.supportsCaret) && context.inputEvent.button == 0)
		{
			ClearSelection();
			Vector3 vector = Stage.inst.touchPosition;
			vector = GlobalToLocal(vector);
			TextField.CharPosition charPosition = GetCharPosition(vector);
			AdjustCaret(charPosition, moveSelectionHeader: true);
			context.CaptureTouch();
		}
	}

	private void __touchMove(EventContext context)
	{
		if (!_editing)
		{
			return;
		}
		Vector3 vector = Stage.inst.touchPosition;
		vector = GlobalToLocal(vector);
		if (!float.IsNaN(vector.x))
		{
			TextField.CharPosition charPosition = GetCharPosition(vector);
			if (charPosition.charIndex != _caretPosition)
			{
				AdjustCaret(charPosition);
			}
		}
	}

	private void __mouseWheel(EventContext context)
	{
		if (_editing && mouseWheelEnabled)
		{
			context.StopPropagation();
			TextField.CharPosition charPosition = GetCharPosition(new Vector2(2f, 2f));
			int lineIndex = charPosition.lineIndex;
			int hScroll = charPosition.charIndex - base.textField.lines[charPosition.lineIndex].charIndex;
			lineIndex = ((!(context.inputEvent.mouseWheelDelta < 0f)) ? (lineIndex + 1) : (lineIndex - 1));
			Scroll(hScroll, lineIndex);
		}
	}

	private void __focusIn(EventContext context)
	{
		if (!Application.isPlaying)
		{
			return;
		}
		_editing = true;
		_textBeforeEdit = _text;
		if (_caret == null)
		{
			CreateCaret();
		}
		if (!string.IsNullOrEmpty(_promptText))
		{
			UpdateText();
		}
		float wv = ((UIConfig.inputCaretSize != 1f || !(UIContentScaler.scaleFactor < 1f)) ? UIConfig.inputCaretSize : (UIConfig.inputCaretSize / UIContentScaler.scaleFactor));
		_caret.SetSize(wv, base.textField.textFormat.size);
		_caret.DrawRect(0f, Color.clear, base.textField.textFormat.color);
		_caret.visible = _editable;
		AddChild(_caret);
		_selectionShape.Clear();
		AddChildAt(_selectionShape, 0);
		if (!base.textField.Redraw())
		{
			TextField.CharPosition charPosition = GetCharPosition(_caretPosition);
			AdjustCaret(charPosition);
		}
		if (Stage.keyboardInput)
		{
			if (keyboardInput)
			{
				Stage.inst.OpenKeyboard(_text, autocorrection: false, !_displayAsPassword && !base.textField.singleLine, _displayAsPassword, alert: false, null, keyboardType, hideInput);
				SetSelection(0, -1);
			}
			return;
		}
		if (!disableIME && !_displayAsPassword)
		{
			Input.imeCompositionMode = (IMECompositionMode)1;
		}
		else
		{
			Input.imeCompositionMode = (IMECompositionMode)2;
		}
		_composing = 0;
		if ((string)context.data == "key")
		{
			SetSelection(0, -1);
		}
		TextInputHistory.inst.StartRecord(this);
	}

	private void __focusOut(EventContext contxt)
	{
		if (!_editing)
		{
			return;
		}
		_editing = false;
		if (Stage.keyboardInput)
		{
			if (keyboardInput)
			{
				Stage.inst.CloseKeyboard();
			}
		}
		else
		{
			Input.imeCompositionMode = (IMECompositionMode)0;
			TextInputHistory.inst.StopRecord(this);
		}
		if (!string.IsNullOrEmpty(_promptText))
		{
			UpdateText();
		}
		_caret.RemoveFromParent();
		_selectionShape.RemoveFromParent();
		if (contextMenu != null && contextMenu.contentPane.onStage)
		{
			contextMenu.Hide();
		}
	}

	private void __keydown(EventContext context)
	{
		if (_editing && HandleKey(context.inputEvent))
		{
			context.StopPropagation();
		}
	}

	private bool HandleKey(InputEvent evt)
	{
		bool result = true;
		switch (evt.keyCode)
		{
		case KeyCode.Backspace:
			if (evt.command)
			{
				if (_selectionStart == _caretPosition && _caretPosition < base.textField.charPositions.Count - 1)
				{
					_selectionStart = _caretPosition + 1;
				}
			}
			else if (_selectionStart == _caretPosition && _caretPosition > 0)
			{
				_selectionStart = _caretPosition - 1;
			}
			if (_editable)
			{
				ReplaceSelection(null);
			}
			break;
		case KeyCode.Delete:
			if (_selectionStart == _caretPosition && _caretPosition < base.textField.charPositions.Count - 1)
			{
				_selectionStart = _caretPosition + 1;
			}
			if (_editable)
			{
				ReplaceSelection(null);
			}
			break;
		case KeyCode.LeftArrow:
			if (!evt.shift)
			{
				ClearSelection();
			}
			if (_caretPosition > 0)
			{
				if (evt.command)
				{
					TextField.CharPosition charPosition2 = GetCharPosition(_caretPosition);
					TextField.LineInfo lineInfo2 = base.textField.lines[charPosition2.lineIndex];
					charPosition2 = GetCharPosition(new Vector2(-2.1474836E+09f, lineInfo2.y + base.textField.y));
					AdjustCaret(charPosition2, !evt.shift);
				}
				else
				{
					TextField.CharPosition charPosition3 = GetCharPosition(_caretPosition - 1);
					AdjustCaret(charPosition3, !evt.shift);
				}
			}
			break;
		case KeyCode.RightArrow:
			if (!evt.shift)
			{
				ClearSelection();
			}
			if (_caretPosition < base.textField.charPositions.Count - 1)
			{
				if (evt.command)
				{
					TextField.CharPosition charPosition6 = GetCharPosition(_caretPosition);
					TextField.LineInfo lineInfo5 = base.textField.lines[charPosition6.lineIndex];
					charPosition6 = GetCharPosition(new Vector2(2.1474836E+09f, lineInfo5.y + base.textField.y));
					AdjustCaret(charPosition6, !evt.shift);
				}
				else
				{
					TextField.CharPosition charPosition7 = GetCharPosition(_caretPosition + 1);
					AdjustCaret(charPosition7, !evt.shift);
				}
			}
			break;
		case KeyCode.UpArrow:
		{
			if (!evt.shift)
			{
				ClearSelection();
			}
			TextField.CharPosition charPosition4 = GetCharPosition(_caretPosition);
			if (charPosition4.lineIndex > 0)
			{
				TextField.LineInfo lineInfo3 = base.textField.lines[charPosition4.lineIndex - 1];
				charPosition4 = GetCharPosition(new Vector2(_caret.x, lineInfo3.y + base.textField.y));
				AdjustCaret(charPosition4, !evt.shift);
			}
			break;
		}
		case KeyCode.DownArrow:
		{
			if (!evt.shift)
			{
				ClearSelection();
			}
			TextField.CharPosition charPosition8 = GetCharPosition(_caretPosition);
			if (charPosition8.lineIndex == base.textField.lines.Count - 1)
			{
				charPosition8.charIndex = base.textField.charPositions.Count - 1;
			}
			else
			{
				TextField.LineInfo lineInfo6 = base.textField.lines[charPosition8.lineIndex + 1];
				charPosition8 = GetCharPosition(new Vector2(_caret.x, lineInfo6.y + base.textField.y));
			}
			AdjustCaret(charPosition8, !evt.shift);
			break;
		}
		case KeyCode.PageUp:
			ClearSelection();
			break;
		case KeyCode.PageDown:
			ClearSelection();
			break;
		case KeyCode.Home:
		{
			if (!evt.shift)
			{
				ClearSelection();
			}
			TextField.CharPosition charPosition5 = GetCharPosition(_caretPosition);
			TextField.LineInfo lineInfo4 = base.textField.lines[charPosition5.lineIndex];
			charPosition5 = GetCharPosition(new Vector2(-2.1474836E+09f, lineInfo4.y + base.textField.y));
			AdjustCaret(charPosition5, !evt.shift);
			break;
		}
		case KeyCode.End:
		{
			if (!evt.shift)
			{
				ClearSelection();
			}
			TextField.CharPosition charPosition = GetCharPosition(_caretPosition);
			TextField.LineInfo lineInfo = base.textField.lines[charPosition.lineIndex];
			charPosition = GetCharPosition(new Vector2(2.1474836E+09f, lineInfo.y + base.textField.y));
			AdjustCaret(charPosition, !evt.shift);
			break;
		}
		case KeyCode.A:
			if (evt.ctrlOrCmd)
			{
				_selectionStart = 0;
				AdjustCaret(GetCharPosition(int.MaxValue));
			}
			break;
		case KeyCode.C:
			if (evt.ctrlOrCmd && !_displayAsPassword)
			{
				string selection2 = GetSelection();
				if (!string.IsNullOrEmpty(selection2))
				{
					DoCopy(selection2);
				}
			}
			break;
		case KeyCode.V:
			if (evt.ctrlOrCmd && _editable)
			{
				DoPaste();
			}
			break;
		case KeyCode.X:
		{
			if (!evt.ctrlOrCmd || _displayAsPassword)
			{
				break;
			}
			string selection = GetSelection();
			if (!string.IsNullOrEmpty(selection))
			{
				DoCopy(selection);
				if (_editable)
				{
					ReplaceSelection(null);
				}
			}
			break;
		}
		case KeyCode.Z:
			if (evt.ctrlOrCmd && _editable)
			{
				if (evt.shift)
				{
					TextInputHistory.inst.Redo(this);
				}
				else
				{
					TextInputHistory.inst.Undo(this);
				}
			}
			break;
		case KeyCode.Y:
			if (evt.ctrlOrCmd && _editable)
			{
				TextInputHistory.inst.Redo(this);
			}
			break;
		case KeyCode.Return:
		case KeyCode.KeypadEnter:
			if (base.textField.singleLine)
			{
				Stage.inst.focus = base.parent;
				DispatchEvent("onSubmit", null);
				DispatchEvent("onKeyDown", null);
			}
			else if (submitOnEnter && !evt.shift)
			{
				DispatchEvent("onSubmit", null);
			}
			break;
		case KeyCode.Tab:
			if (base.textField.singleLine)
			{
				Stage.inst.DoKeyNavigate(evt.shift);
				result = false;
			}
			break;
		case KeyCode.Escape:
			text = _textBeforeEdit;
			Stage.inst.focus = base.parent;
			break;
		default:
			result = evt.keyCode <= KeyCode.KeypadEquals && !evt.ctrlOrCmd;
			break;
		}
		char c = evt.character;
		if (c != 0)
		{
			if (evt.ctrlOrCmd)
			{
				return true;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (c == '\u0019')
			{
				c = '\t';
			}
			if (c == '\u001b' || (base.textField.singleLine && (c == '\n' || c == '\t')))
			{
				return true;
			}
			if (submitOnEnter && c == '\n' && !evt.shift)
			{
				return true;
			}
			if (char.IsHighSurrogate(c))
			{
				_highSurrogateChar = c;
				return true;
			}
			if (_editable)
			{
				if (char.IsLowSurrogate(c))
				{
					ReplaceSelection(char.ConvertFromUtf32((c & 0x3FF) + ((_highSurrogateChar & 0x3FF) + 64 << 10)));
				}
				else
				{
					ReplaceSelection(c.ToString());
				}
			}
			return true;
		}
		if (Input.compositionString.Length > 0 && _editable)
		{
			int composing = _composing;
			_composing = Input.compositionString.Length;
			StringBuilder stringBuilder = new StringBuilder();
			GetPartialText(0, _caretPosition, stringBuilder);
			stringBuilder.Append(Input.compositionString);
			GetPartialText(_caretPosition + composing, -1, stringBuilder);
			base.textField.text = stringBuilder.ToString();
		}
		return result;
	}

	internal void CheckComposition()
	{
		if (_composing != 0 && Input.compositionString.Length == 0)
		{
			UpdateText();
		}
	}

	private void __click(EventContext context)
	{
		if (_editing && context.inputEvent.isDoubleClick)
		{
			context.StopPropagation();
			_selectionStart = 0;
			AdjustCaret(GetCharPosition(int.MaxValue));
		}
	}

	private void __rightClick(EventContext context)
	{
		if (contextMenu != null)
		{
			context.StopPropagation();
			contextMenu.Show();
		}
	}
}
