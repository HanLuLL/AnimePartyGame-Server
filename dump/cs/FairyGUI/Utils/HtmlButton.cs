using UnityEngine;

namespace FairyGUI.Utils;

public class HtmlButton : IHtmlObject
{
	public const string CLICK_EVENT = "OnHtmlButtonClick";

	public static string resource;

	private RichTextField _owner;

	private HtmlElement _element;

	private EventCallback1 _clickHandler;

	public GComponent button { get; private set; }

	public DisplayObject displayObject
	{
		get
		{
			if (button == null)
			{
				return null;
			}
			return button.displayObject;
		}
	}

	public HtmlElement element => _element;

	public float width
	{
		get
		{
			if (button == null)
			{
				return 0f;
			}
			return button.width;
		}
	}

	public float height
	{
		get
		{
			if (button == null)
			{
				return 0f;
			}
			return button.height;
		}
	}

	public HtmlButton()
	{
		if (resource != null)
		{
			button = UIPackage.CreateObjectFromURL(resource).asCom;
			_clickHandler = delegate(EventContext context)
			{
				_owner.DispatchEvent("OnHtmlButtonClick", context.data, this);
			};
		}
		else
		{
			Debug.LogWarning("FairyGUI: Set HtmlButton.resource first");
		}
	}

	public void Create(RichTextField owner, HtmlElement element)
	{
		_owner = owner;
		_element = element;
		if (button != null)
		{
			button.onClick.Add(_clickHandler);
			int num = element.GetInt("width", button.sourceWidth);
			int num2 = element.GetInt("height", button.sourceHeight);
			button.SetSize(num, num2);
			button.text = element.GetString("value");
		}
	}

	public void SetPosition(float x, float y)
	{
		if (button != null)
		{
			button.SetXY(x, y);
		}
	}

	public void Add()
	{
		if (button != null)
		{
			_owner.AddChild(button.displayObject);
		}
	}

	public void Remove()
	{
		if (button != null && button.displayObject.parent != null)
		{
			_owner.RemoveChild(button.displayObject);
		}
	}

	public void Release()
	{
		if (button != null)
		{
			button.RemoveEventListeners();
		}
		_owner = null;
		_element = null;
	}

	public void Dispose()
	{
		if (button != null)
		{
			button.Dispose();
		}
	}
}
