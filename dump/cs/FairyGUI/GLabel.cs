using System;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GLabel : GComponent, IColorGear
{
	protected GObject _titleObject;

	protected GObject _iconObject;

	public override string icon
	{
		get
		{
			if (_iconObject != null)
			{
				return _iconObject.icon;
			}
			return null;
		}
		set
		{
			if (_iconObject != null)
			{
				_iconObject.icon = value;
			}
			UpdateGear(7);
		}
	}

	public string title
	{
		get
		{
			if (_titleObject != null)
			{
				return _titleObject.text;
			}
			return null;
		}
		set
		{
			if (_titleObject != null)
			{
				_titleObject.text = value;
			}
			UpdateGear(6);
		}
	}

	public override string text
	{
		get
		{
			return title;
		}
		set
		{
			title = value;
		}
	}

	public bool editable
	{
		get
		{
			if (_titleObject is GTextInput)
			{
				return _titleObject.asTextInput.editable;
			}
			return false;
		}
		set
		{
			if (_titleObject is GTextInput)
			{
				_titleObject.asTextInput.editable = value;
			}
		}
	}

	public Color titleColor
	{
		get
		{
			return GetTextField()?.color ?? Color.black;
		}
		set
		{
			GTextField textField = GetTextField();
			if (textField != null)
			{
				textField.color = value;
				UpdateGear(4);
			}
		}
	}

	public int titleFontSize
	{
		get
		{
			return GetTextField()?.textFormat.size ?? 0;
		}
		set
		{
			GTextField textField = GetTextField();
			if (textField != null)
			{
				TextFormat textFormat = textField.textFormat;
				textFormat.size = value;
				textField.textFormat = textFormat;
			}
		}
	}

	public Color color
	{
		get
		{
			return titleColor;
		}
		set
		{
			titleColor = value;
		}
	}

	public GTextField GetTextField()
	{
		if (_titleObject is GTextField)
		{
			return (GTextField)_titleObject;
		}
		if (_titleObject is GLabel)
		{
			return ((GLabel)_titleObject).GetTextField();
		}
		if (_titleObject is GButton)
		{
			return ((GButton)_titleObject).GetTextField();
		}
		return null;
	}

	protected override void ConstructExtension(ByteBuffer buffer)
	{
		_titleObject = GetChild("title");
		_iconObject = GetChild("icon");
	}

	public override void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_AfterAdd(buffer, beginPos);
		if (!buffer.Seek(beginPos, 6) || (ObjectType)buffer.ReadByte() != packageItem.objectType)
		{
			return;
		}
		string text = buffer.ReadS();
		if (text != null)
		{
			title = text;
		}
		text = buffer.ReadS();
		if (text != null)
		{
			icon = text;
		}
		if (buffer.ReadBool())
		{
			titleColor = buffer.ReadColor();
		}
		int num = buffer.ReadInt();
		if (num != 0)
		{
			titleFontSize = num;
		}
		if (buffer.ReadBool())
		{
			if (GetTextField() is GTextInput gTextInput)
			{
				text = buffer.ReadS();
				if (text != null)
				{
					gTextInput.promptText = text;
				}
				text = buffer.ReadS();
				if (text != null)
				{
					gTextInput.restrict = text;
				}
				num = buffer.ReadInt();
				if (num != 0)
				{
					gTextInput.maxLength = num;
				}
				num = buffer.ReadInt();
				if (num != 0)
				{
					gTextInput.keyboardType = num;
				}
				if (buffer.ReadBool())
				{
					gTextInput.displayAsPassword = true;
				}
			}
			else
			{
				buffer.Skip(13);
			}
		}
		if (buffer.version < 5)
		{
			return;
		}
		string value = buffer.ReadS();
		if (!string.IsNullOrEmpty(value))
		{
			buffer.ReadFloat();
			int soundID = Convert.ToInt32(value);
			base.displayObject.onClick.Add((EventCallback0)delegate
			{
				if ((long)soundID > 0L)
				{
					Stage.inst.PlayOneShotSound(soundID);
				}
			});
		}
		else
		{
			buffer.Skip(4);
		}
	}
}
