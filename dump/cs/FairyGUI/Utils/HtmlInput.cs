using UnityEngine;

namespace FairyGUI.Utils;

public class HtmlInput : IHtmlObject
{
	private RichTextField _owner;

	private HtmlElement _element;

	private bool _hidden;

	public static int defaultBorderSize = 2;

	public static Color defaultBorderColor = ToolSet.ColorFromRGB(11119017);

	public static Color defaultBackgroundColor = Color.clear;

	public GTextInput textInput { get; private set; }

	public DisplayObject displayObject => textInput.displayObject;

	public HtmlElement element => _element;

	public float width
	{
		get
		{
			if (!_hidden)
			{
				return textInput.width;
			}
			return 0f;
		}
	}

	public float height
	{
		get
		{
			if (!_hidden)
			{
				return textInput.height;
			}
			return 0f;
		}
	}

	public HtmlInput()
	{
		textInput = (GTextInput)UIObjectFactory.NewObject(ObjectType.InputText);
		textInput.gameObjectName = "HtmlInput";
		textInput.verticalAlign = VertAlignType.Middle;
	}

	public void Create(RichTextField owner, HtmlElement element)
	{
		_owner = owner;
		_element = element;
		string text = element.GetString("type");
		if (text != null)
		{
			text = text.ToLower();
		}
		_hidden = text == "hidden";
		if (!_hidden)
		{
			int num = element.GetInt("width", 0);
			int num2 = element.GetInt("height", 0);
			int border = element.GetInt("border", defaultBorderSize);
			Color color = element.GetColor("border-color", defaultBorderColor);
			Color color2 = element.GetColor("background-color", defaultBackgroundColor);
			if (num == 0)
			{
				num = element.space;
				if ((float)num > _owner.width / 2f || num < 100)
				{
					num = (int)_owner.width / 2;
				}
			}
			if (num2 == 0)
			{
				num2 = element.format.size + 10;
			}
			textInput.textFormat = element.format;
			textInput.displayAsPassword = text == "password";
			textInput.maxLength = element.GetInt("maxlength", int.MaxValue);
			textInput.border = border;
			textInput.borderColor = color;
			textInput.backgroundColor = color2;
			textInput.SetSize(num, num2);
		}
		textInput.text = element.GetString("value");
	}

	public void SetPosition(float x, float y)
	{
		if (!_hidden)
		{
			textInput.SetXY(x, y);
		}
	}

	public void Add()
	{
		if (!_hidden)
		{
			_owner.AddChild(textInput.displayObject);
		}
	}

	public void Remove()
	{
		if (!_hidden && textInput.displayObject.parent != null)
		{
			_owner.RemoveChild(textInput.displayObject);
		}
	}

	public void Release()
	{
		textInput.RemoveEventListeners();
		textInput.text = null;
		_owner = null;
		_element = null;
	}

	public void Dispose()
	{
		textInput.Dispose();
	}
}
