namespace FairyGUI.Utils;

public class HtmlImage : IHtmlObject
{
	private RichTextField _owner;

	private HtmlElement _element;

	private bool _externalTexture;

	public GLoader loader { get; private set; }

	public DisplayObject displayObject => loader.displayObject;

	public HtmlElement element => _element;

	public float width => loader.width;

	public float height => loader.height;

	public HtmlImage()
	{
		loader = (GLoader)UIObjectFactory.NewObject(ObjectType.Loader);
		loader.gameObjectName = "HtmlImage";
		loader.fill = FillType.ScaleFree;
		loader.touchable = false;
	}

	public void Create(RichTextField owner, HtmlElement element)
	{
		_owner = owner;
		_element = element;
		int defValue = 0;
		int defValue2 = 0;
		NTexture imageTexture = owner.htmlPageContext.GetImageTexture(this);
		if (imageTexture != null)
		{
			defValue = imageTexture.width;
			defValue2 = imageTexture.height;
			loader.texture = imageTexture;
			_externalTexture = true;
		}
		else
		{
			string text = element.GetString("src");
			if (text != null)
			{
				PackageItem itemByURL = UIPackage.GetItemByURL(text);
				if (itemByURL != null)
				{
					defValue = itemByURL.width;
					defValue2 = itemByURL.height;
				}
			}
			loader.url = text;
			_externalTexture = false;
		}
		int num = element.GetInt("width", defValue);
		int num2 = element.GetInt("height", defValue2);
		if (num == 0)
		{
			num = 5;
		}
		if (num2 == 0)
		{
			num2 = 10;
		}
		loader.SetSize(num, num2);
	}

	public void SetPosition(float x, float y)
	{
		loader.SetXY(x, y);
	}

	public void Add()
	{
		_owner.AddChild(loader.displayObject);
	}

	public void Remove()
	{
		if (loader.displayObject.parent != null)
		{
			_owner.RemoveChild(loader.displayObject);
		}
	}

	public void Release()
	{
		loader.RemoveEventListeners();
		if (_externalTexture)
		{
			_owner.htmlPageContext.FreeImageTexture(this, loader.texture);
			_externalTexture = false;
		}
		loader.url = null;
		_owner = null;
		_element = null;
	}

	public void Dispose()
	{
		if (_externalTexture)
		{
			_owner.htmlPageContext.FreeImageTexture(this, loader.texture);
		}
		loader.Dispose();
	}
}
