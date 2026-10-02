using System;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GLoader : GObject, IAnimationGear, IColorGear
{
	public Vector2 customOffset = Vector2.zero;

	public Vector2 customScale = Vector2.one;

	public bool showErrorSign;

	private string _url;

	private AlignType _align;

	private VertAlignType _verticalAlign;

	private bool _autoSize;

	private FillType _fill;

	private bool _shrinkOnly;

	private bool _updatingLayout;

	private PackageItem _contentItem;

	private Action<NTexture> _reloadDelegate;

	private MovieClip _content;

	private GObject _errorSign;

	private GComponent _content2;

	public Action loadCompleted;

	public string url
	{
		get
		{
			return _url;
		}
		set
		{
			if (!(_url == value))
			{
				ClearContent();
				_url = value;
				LoadContent();
				UpdateGear(7);
			}
		}
	}

	public override string icon
	{
		get
		{
			return _url;
		}
		set
		{
			url = value;
		}
	}

	public AlignType align
	{
		get
		{
			return _align;
		}
		set
		{
			if (_align != value)
			{
				_align = value;
				UpdateLayout();
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
				UpdateLayout();
			}
		}
	}

	public FillType fill
	{
		get
		{
			return _fill;
		}
		set
		{
			if (_fill != value)
			{
				_fill = value;
				UpdateLayout();
			}
		}
	}

	public bool shrinkOnly
	{
		get
		{
			return _shrinkOnly;
		}
		set
		{
			if (_shrinkOnly != value)
			{
				_shrinkOnly = value;
				UpdateLayout();
			}
		}
	}

	public bool autoSize
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
				UpdateLayout();
			}
		}
	}

	public bool playing
	{
		get
		{
			return _content.playing;
		}
		set
		{
			_content.playing = value;
			UpdateGear(5);
		}
	}

	public int frame
	{
		get
		{
			return _content.frame;
		}
		set
		{
			_content.frame = value;
			UpdateGear(5);
		}
	}

	public float timeScale
	{
		get
		{
			return _content.timeScale;
		}
		set
		{
			_content.timeScale = value;
		}
	}

	public bool ignoreEngineTimeScale
	{
		get
		{
			return _content.ignoreEngineTimeScale;
		}
		set
		{
			_content.ignoreEngineTimeScale = value;
		}
	}

	public Material material
	{
		get
		{
			return _content.material;
		}
		set
		{
			_content.material = value;
		}
	}

	public string shader
	{
		get
		{
			return _content.shader;
		}
		set
		{
			_content.shader = value;
		}
	}

	public Color color
	{
		get
		{
			return _content.color;
		}
		set
		{
			if (_content.color != value)
			{
				_content.color = value;
				UpdateGear(4);
			}
		}
	}

	public FillMethod fillMethod
	{
		get
		{
			return _content.fillMethod;
		}
		set
		{
			_content.fillMethod = value;
		}
	}

	public int fillOrigin
	{
		get
		{
			return _content.fillOrigin;
		}
		set
		{
			_content.fillOrigin = value;
		}
	}

	public bool fillClockwise
	{
		get
		{
			return _content.fillClockwise;
		}
		set
		{
			_content.fillClockwise = value;
		}
	}

	public float fillAmount
	{
		get
		{
			return _content.fillAmount;
		}
		set
		{
			_content.fillAmount = value;
		}
	}

	public Image image => _content;

	public MovieClip movieClip => _content;

	public GComponent component => _content2;

	public NTexture texture
	{
		get
		{
			return _content.texture;
		}
		set
		{
			url = null;
			_content.texture = value;
			if (value != null)
			{
				sourceWidth = value.width;
				sourceHeight = value.height;
			}
			else
			{
				sourceWidth = (sourceHeight = 0);
			}
			UpdateLayout();
		}
	}

	public override IFilter filter
	{
		get
		{
			return _content.filter;
		}
		set
		{
			_content.filter = value;
		}
	}

	public override BlendMode blendMode
	{
		get
		{
			return _content.blendMode;
		}
		set
		{
			_content.blendMode = value;
		}
	}

	public void Background(string bgURL)
	{
		MallScreen();
		url = bgURL;
	}

	public void Background(NTexture nTexture)
	{
		MallScreen();
		texture = nTexture;
	}

	public void MallScreen()
	{
		GComponent target = base.parent ?? GRoot.inst;
		AddRelation(target, RelationType.Center_Center);
		SetSize(GRoot.inst.width, GRoot.inst.height);
	}

	public GLoader()
	{
		_url = string.Empty;
		_align = AlignType.Left;
		_verticalAlign = VertAlignType.Top;
		showErrorSign = true;
		_reloadDelegate = OnExternalReload;
	}

	protected override void CreateDisplayObject()
	{
		base.displayObject = new Container("GLoader");
		base.displayObject.gOwner = this;
		_content = new MovieClip();
		((Container)base.displayObject).AddChild(_content);
		((Container)base.displayObject).opaque = true;
	}

	public override void Dispose()
	{
		if (_content.texture != null && _contentItem == null)
		{
			_content.texture.onSizeChanged -= _reloadDelegate;
			try
			{
				FreeExternal(_content.texture);
			}
			catch (Exception message)
			{
				Debug.LogWarning(message);
			}
		}
		if (_errorSign != null)
		{
			_errorSign.Dispose();
		}
		if (_content2 != null)
		{
			_content2.Dispose();
		}
		_content.Dispose();
		base.Dispose();
	}

	public void Advance(float time)
	{
		_content.Advance(time);
	}

	protected void LoadContent()
	{
		ClearContent();
		if (!string.IsNullOrEmpty(_url))
		{
			if (_url.StartsWith("ui://"))
			{
				LoadFromPackage(_url);
			}
			else
			{
				LoadExternal();
			}
		}
	}

	protected void LoadFromPackage(string itemURL)
	{
		_contentItem = UIPackage.GetItemByURL(itemURL);
		if (_contentItem != null)
		{
			_contentItem = _contentItem.getBranch();
			sourceWidth = _contentItem.width;
			sourceHeight = _contentItem.height;
			_contentItem = _contentItem.getHighResolution();
			_contentItem.Load();
			if (_contentItem.type == PackageItemType.Image)
			{
				_content.texture = _contentItem.texture;
				_content.textureScale = new Vector2((float)_contentItem.width / (float)sourceWidth, (float)_contentItem.height / (float)sourceHeight);
				_content.scale9Grid = _contentItem.scale9Grid;
				_content.scaleByTile = _contentItem.scaleByTile;
				_content.tileGridIndice = _contentItem.tileGridIndice;
				UpdateLayout();
			}
			else if (_contentItem.type == PackageItemType.MovieClip)
			{
				_content.interval = _contentItem.interval;
				_content.swing = _contentItem.swing;
				_content.repeatDelay = _contentItem.repeatDelay;
				_content.frames = _contentItem.frames;
				UpdateLayout();
			}
			else if (_contentItem.type == PackageItemType.Component)
			{
				GObject gObject = UIPackage.CreateObjectFromURL(itemURL);
				if (gObject == null)
				{
					SetErrorState();
				}
				else if (!(gObject is GComponent))
				{
					gObject.Dispose();
					SetErrorState();
				}
				else
				{
					_content2 = (GComponent)gObject;
					((Container)base.displayObject).AddChild(_content2.displayObject);
					UpdateLayout();
				}
			}
			else
			{
				if (_autoSize)
				{
					SetSize(_contentItem.width, _contentItem.height);
				}
				SetErrorState();
				Debug.LogWarning("Unsupported type of GLoader: " + _contentItem.type);
			}
		}
		else
		{
			SetErrorState();
		}
	}

	protected virtual void LoadExternal()
	{
		Texture2D texture2D = (Texture2D)Resources.Load(_url, typeof(Texture2D));
		if (texture2D != null)
		{
			onExternalLoadSuccess(new NTexture(texture2D));
		}
		else
		{
			onExternalLoadFailed();
		}
	}

	protected virtual void FreeExternal(NTexture texture)
	{
	}

	protected void onExternalLoadSuccess(NTexture texture)
	{
		if (!(texture.nativeTexture != null) || texture.nativeTexture.name.Equals(url))
		{
			_content.texture = texture;
			sourceWidth = texture.width;
			sourceHeight = texture.height;
			_content.scale9Grid = null;
			_content.scaleByTile = false;
			texture.onSizeChanged += _reloadDelegate;
			UpdateLayout();
			loadCompleted?.Invoke();
		}
	}

	protected void onExternalLoadFailed()
	{
		SetErrorState();
	}

	private void OnExternalReload(NTexture texture)
	{
		sourceWidth = texture.width;
		sourceHeight = texture.height;
		UpdateLayout();
	}

	private void SetErrorState()
	{
		if (!showErrorSign || !Application.isPlaying)
		{
			return;
		}
		if (_errorSign == null)
		{
			if (UIConfig.loaderErrorSign == null)
			{
				return;
			}
			_errorSign = UIPackage.CreateObjectFromURL(UIConfig.loaderErrorSign);
		}
		if (_errorSign != null)
		{
			_errorSign.SetSize(base.width, base.height);
			((Container)base.displayObject).AddChild(_errorSign.displayObject);
		}
	}

	protected void ClearErrorState()
	{
		if (_errorSign != null && _errorSign.displayObject.parent != null)
		{
			((Container)base.displayObject).RemoveChild(_errorSign.displayObject);
		}
	}

	protected void UpdateLayout()
	{
		if (_content2 == null && _content.texture == null && _content.frames == null)
		{
			if (_autoSize)
			{
				_updatingLayout = true;
				SetSize(50f, 30f);
				_updatingLayout = false;
			}
			return;
		}
		float num = sourceWidth;
		float num2 = sourceHeight;
		if (_autoSize)
		{
			_updatingLayout = true;
			if (num == 0f)
			{
				num = 50f;
			}
			if (num2 == 0f)
			{
				num2 = 30f;
			}
			SetSize(num, num2);
			_updatingLayout = false;
			if (_width == num && _height == num2)
			{
				if (_content2 != null)
				{
					_content2.SetXY(0f, 0f);
					_content2.SetScale(1f, 1f);
				}
				else
				{
					_content.SetXY(0f, 0f);
					_content.SetSize(num, num2);
				}
				InvalidateBatchingState();
				return;
			}
		}
		float num3 = 1f;
		float num4 = 1f;
		if (_fill != FillType.None)
		{
			num3 = base.width / (float)sourceWidth;
			num4 = base.height / (float)sourceHeight;
			if (num3 != 1f || num4 != 1f)
			{
				if (_fill == FillType.ScaleMatchHeight)
				{
					num3 = num4;
				}
				else if (_fill == FillType.ScaleMatchWidth)
				{
					num4 = num3;
				}
				else if (_fill == FillType.Scale)
				{
					if (num3 > num4)
					{
						num3 = num4;
					}
					else
					{
						num4 = num3;
					}
				}
				else if (_fill == FillType.ScaleNoBorder)
				{
					if (num3 > num4)
					{
						num4 = num3;
					}
					else
					{
						num3 = num4;
					}
				}
				if (_shrinkOnly)
				{
					if (num3 > 1f)
					{
						num3 = 1f;
					}
					if (num4 > 1f)
					{
						num4 = 1f;
					}
				}
				num = (float)sourceWidth * num3;
				num2 = (float)sourceHeight * num4;
			}
		}
		if (_content2 != null)
		{
			_content2.SetScale(num3, num4);
		}
		else
		{
			_content.size = new Vector2(num, num2);
		}
		float xv = ((_align == AlignType.Center) ? ((base.width - num) / 2f) : ((_align != AlignType.Right) ? 0f : (base.width - num)));
		float yv = ((_verticalAlign == VertAlignType.Middle) ? ((base.height - num2) / 2f) : ((_verticalAlign != VertAlignType.Bottom) ? 0f : (base.height - num2)));
		if (_content2 != null)
		{
			_content2.SetXY(xv, yv);
		}
		else
		{
			_content.SetXY(xv, yv);
		}
		if (customOffset != Vector2.zero)
		{
			_content.SetXY(customOffset.x, 0f - customOffset.y);
			_content.SetScale(customScale.x, customScale.y);
		}
		InvalidateBatchingState();
	}

	public void ClearContent()
	{
		ClearErrorState();
		if (_content.texture != null)
		{
			if (_contentItem == null)
			{
				_content.texture.onSizeChanged -= _reloadDelegate;
				FreeExternal(_content.texture);
			}
			_content.texture = null;
		}
		_content.frames = null;
		if (_content2 != null)
		{
			_content2.Dispose();
			_content2 = null;
		}
		_contentItem = null;
	}

	protected override void HandleSizeChanged()
	{
		base.HandleSizeChanged();
		if (!_updatingLayout)
		{
			UpdateLayout();
		}
	}

	public override void Setup_BeforeAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_BeforeAdd(buffer, beginPos);
		buffer.Seek(beginPos, 5);
		_url = buffer.ReadS();
		_align = (AlignType)buffer.ReadByte();
		_verticalAlign = (VertAlignType)buffer.ReadByte();
		_fill = (FillType)buffer.ReadByte();
		_shrinkOnly = buffer.ReadBool();
		_autoSize = buffer.ReadBool();
		showErrorSign = buffer.ReadBool();
		_content.playing = buffer.ReadBool();
		_content.frame = buffer.ReadInt();
		if (buffer.ReadBool())
		{
			_content.color = buffer.ReadColor();
		}
		_content.fillMethod = (FillMethod)buffer.ReadByte();
		if (_content.fillMethod != FillMethod.None)
		{
			_content.fillOrigin = buffer.ReadByte();
			_content.fillClockwise = buffer.ReadBool();
			_content.fillAmount = buffer.ReadFloat();
		}
		if (!string.IsNullOrEmpty(_url))
		{
			LoadContent();
		}
	}
}
