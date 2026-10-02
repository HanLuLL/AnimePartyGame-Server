using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GTextInput : GTextField
{
	private EventListener _onChanged;

	private EventListener _onSubmit;

	public InputTextField inputTextField { get; private set; }

	public EventListener onChanged => _onChanged ?? (_onChanged = new EventListener(this, "onChanged"));

	public EventListener onSubmit => _onSubmit ?? (_onSubmit = new EventListener(this, "onSubmit"));

	public bool SubmitOnEnter
	{
		get
		{
			return inputTextField.submitOnEnter;
		}
		set
		{
			inputTextField.submitOnEnter = value;
		}
	}

	public bool editable
	{
		get
		{
			return inputTextField.editable;
		}
		set
		{
			inputTextField.editable = value;
		}
	}

	public bool hideInput
	{
		get
		{
			return inputTextField.hideInput;
		}
		set
		{
			inputTextField.hideInput = value;
		}
	}

	public int maxLength
	{
		get
		{
			return inputTextField.maxLength;
		}
		set
		{
			inputTextField.maxLength = value;
		}
	}

	public string restrict
	{
		get
		{
			return inputTextField.restrict;
		}
		set
		{
			inputTextField.restrict = value;
		}
	}

	public bool displayAsPassword
	{
		get
		{
			return inputTextField.displayAsPassword;
		}
		set
		{
			inputTextField.displayAsPassword = value;
		}
	}

	public int caretPosition
	{
		get
		{
			return inputTextField.caretPosition;
		}
		set
		{
			inputTextField.caretPosition = value;
		}
	}

	public string promptText
	{
		get
		{
			return inputTextField.promptText;
		}
		set
		{
			inputTextField.promptText = value;
		}
	}

	public bool keyboardInput
	{
		get
		{
			return inputTextField.keyboardInput;
		}
		set
		{
			inputTextField.keyboardInput = value;
		}
	}

	public int keyboardType
	{
		get
		{
			return inputTextField.keyboardType;
		}
		set
		{
			inputTextField.keyboardType = value;
		}
	}

	public bool disableIME
	{
		get
		{
			return inputTextField.disableIME;
		}
		set
		{
			inputTextField.disableIME = value;
		}
	}

	public Dictionary<uint, Emoji> emojies
	{
		get
		{
			return inputTextField.emojies;
		}
		set
		{
			inputTextField.emojies = value;
		}
	}

	public int border
	{
		get
		{
			return inputTextField.border;
		}
		set
		{
			inputTextField.border = value;
		}
	}

	public int corner
	{
		get
		{
			return inputTextField.corner;
		}
		set
		{
			inputTextField.corner = value;
		}
	}

	public Color borderColor
	{
		get
		{
			return inputTextField.borderColor;
		}
		set
		{
			inputTextField.borderColor = value;
		}
	}

	public Color backgroundColor
	{
		get
		{
			return inputTextField.backgroundColor;
		}
		set
		{
			inputTextField.backgroundColor = value;
		}
	}

	public bool mouseWheelEnabled
	{
		get
		{
			return inputTextField.mouseWheelEnabled;
		}
		set
		{
			inputTextField.mouseWheelEnabled = value;
		}
	}

	public GTextInput()
	{
		_textField.autoSize = AutoSizeType.None;
		_textField.wordWrap = false;
	}

	public void SetSelection(int start, int length)
	{
		inputTextField.SetSelection(start, length);
	}

	public void ReplaceSelection(string value)
	{
		inputTextField.ReplaceSelection(value);
	}

	protected override void SetTextFieldText()
	{
		inputTextField.text = _text;
	}

	protected override void CreateDisplayObject()
	{
		inputTextField = new InputTextField();
		inputTextField.gOwner = this;
		base.displayObject = inputTextField;
		_textField = inputTextField.textField;
	}

	public override void Setup_BeforeAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_BeforeAdd(buffer, beginPos);
		buffer.Seek(beginPos, 4);
		string text = buffer.ReadS();
		if (text != null)
		{
			inputTextField.promptText = text;
		}
		text = buffer.ReadS();
		if (text != null)
		{
			inputTextField.restrict = text;
		}
		int num = buffer.ReadInt();
		if (num != 0)
		{
			inputTextField.maxLength = num;
		}
		num = buffer.ReadInt();
		if (num != 0)
		{
			inputTextField.keyboardType = num;
		}
		if (buffer.ReadBool())
		{
			inputTextField.displayAsPassword = true;
		}
	}
}
