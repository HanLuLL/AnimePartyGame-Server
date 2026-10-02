using UnityEngine;

namespace FairyGUI.Utils;

public class HtmlSelect : IHtmlObject
{
	public const string CHANGED_EVENT = "OnHtmlSelectChanged";

	public static string resource;

	private RichTextField _owner;

	private HtmlElement _element;

	private EventCallback0 _changeHandler;

	public GComboBox comboBox { get; private set; }

	public DisplayObject displayObject => comboBox.displayObject;

	public HtmlElement element => _element;

	public float width
	{
		get
		{
			if (comboBox == null)
			{
				return 0f;
			}
			return comboBox.width;
		}
	}

	public float height
	{
		get
		{
			if (comboBox == null)
			{
				return 0f;
			}
			return comboBox.height;
		}
	}

	public HtmlSelect()
	{
		if (resource != null)
		{
			comboBox = UIPackage.CreateObjectFromURL(resource).asComboBox;
			_changeHandler = delegate
			{
				_owner.DispatchEvent("OnHtmlSelectChanged", null, this);
			};
		}
		else
		{
			Debug.LogWarning("FairyGUI: Set HtmlSelect.resource first");
		}
	}

	public void Create(RichTextField owner, HtmlElement element)
	{
		_owner = owner;
		_element = element;
		if (comboBox != null)
		{
			comboBox.onChanged.Add(_changeHandler);
			int num = element.GetInt("width", comboBox.sourceWidth);
			int num2 = element.GetInt("height", comboBox.sourceHeight);
			comboBox.SetSize(num, num2);
			comboBox.items = (string[])element.Get("items");
			comboBox.values = (string[])element.Get("values");
			comboBox.value = element.GetString("value");
		}
	}

	public void SetPosition(float x, float y)
	{
		if (comboBox != null)
		{
			comboBox.SetXY(x, y);
		}
	}

	public void Add()
	{
		if (comboBox != null)
		{
			_owner.AddChild(comboBox.displayObject);
		}
	}

	public void Remove()
	{
		if (comboBox != null && comboBox.displayObject.parent != null)
		{
			_owner.RemoveChild(comboBox.displayObject);
		}
	}

	public void Release()
	{
		if (comboBox != null)
		{
			comboBox.RemoveEventListeners();
		}
		_owner = null;
		_element = null;
	}

	public void Dispose()
	{
		if (comboBox != null)
		{
			comboBox.Dispose();
		}
	}
}
