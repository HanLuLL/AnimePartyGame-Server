using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GLoader3D : GObject, IAnimationGear, IColorGear
{
	private string _url;

	private AlignType _align;

	private VertAlignType _verticalAlign;

	private bool _autoSize;

	private FillType _fill;

	private bool _shrinkOnly;

	private string _animationName;

	private string _skinName;

	private bool _playing;

	private int _frame;

	private bool _loop;

	private bool _updatingLayout;

	private Color _color;

	protected PackageItem _contentItem;

	protected GoWrapper _content;

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
			return _playing;
		}
		set
		{
			if (_playing != value)
			{
				_playing = value;
				OnChange("playing");
				UpdateGear(5);
			}
		}
	}

	public int frame
	{
		get
		{
			return _frame;
		}
		set
		{
			if (_frame != value)
			{
				_frame = value;
				OnChange("frame");
				UpdateGear(5);
			}
		}
	}

	public float timeScale { get; set; }

	public bool ignoreEngineTimeScale { get; set; }

	public bool loop
	{
		get
		{
			return _loop;
		}
		set
		{
			if (_loop != value)
			{
				_loop = value;
				OnChange("loop");
			}
		}
	}

	public string animationName
	{
		get
		{
			return _animationName;
		}
		set
		{
			_animationName = value;
			OnChange("animationName");
		}
	}

	public string skinName
	{
		get
		{
			return _skinName;
		}
		set
		{
			_skinName = value;
			OnChange("skinName");
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
			return _color;
		}
		set
		{
			if (_color != value)
			{
				_color = value;
				UpdateGear(4);
				OnChange("color");
			}
		}
	}

	public GameObject wrapTarget => _content.wrapTarget;

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

	public GLoader3D()
	{
		_url = string.Empty;
		_align = AlignType.Left;
		_verticalAlign = VertAlignType.Top;
		_playing = true;
		_color = Color.white;
	}

	protected override void CreateDisplayObject()
	{
		base.displayObject = new Container("GLoader3D");
		base.displayObject.gOwner = this;
		_content = new GoWrapper();
		_content.onUpdate += OnUpdateContent;
		((Container)base.displayObject).AddChild(_content);
		((Container)base.displayObject).opaque = true;
	}

	public override void Dispose()
	{
		_content.Dispose();
		base.Dispose();
	}

	public void Advance(float time)
	{
	}

	public void SetWrapTarget(GameObject gameObject, bool cloneMaterial, int width, int height)
	{
		_content.SetWrapTarget(gameObject, cloneMaterial);
		_content.SetSize(width, height);
		sourceWidth = width;
		sourceHeight = height;
		UpdateLayout();
	}

	protected void LoadContent()
	{
		ClearContent();
		if (string.IsNullOrEmpty(_url))
		{
			return;
		}
		_contentItem = UIPackage.GetItemByURL(_url);
		if (_contentItem != null)
		{
			_contentItem = _contentItem.getBranch();
			_contentItem = _contentItem.getHighResolution();
			_contentItem.Load();
			if (_contentItem.type != PackageItemType.Spine)
			{
				_ = _contentItem.type;
				_ = 10;
			}
		}
		else
		{
			LoadExternal();
		}
	}

	protected virtual void OnChange(string propertyName)
	{
		if (_contentItem != null && _contentItem.type != PackageItemType.Spine)
		{
			_ = _contentItem.type;
			_ = 10;
		}
	}

	protected virtual void LoadExternal()
	{
	}

	protected virtual void FreeExternal()
	{
		Object.DestroyImmediate(_content.wrapTarget);
	}

	protected void UpdateLayout()
	{
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
				_content.SetXY(0f, 0f);
				_content.SetScale(1f, 1f);
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
		_content.SetScale(num3, num4);
		float xv = ((_align == AlignType.Center) ? ((base.width - num) / 2f) : ((_align != AlignType.Right) ? 0f : (base.width - num)));
		float yv = ((_verticalAlign == VertAlignType.Middle) ? ((base.height - num2) / 2f) : ((_verticalAlign != VertAlignType.Bottom) ? 0f : (base.height - num2)));
		_content.SetXY(xv, yv);
		InvalidateBatchingState();
	}

	protected void ClearContent()
	{
		if (_content.wrapTarget != null)
		{
			if (_contentItem != null)
			{
				if (_contentItem.type != PackageItemType.Spine && _contentItem.type != PackageItemType.DragoneBones)
				{
				}
			}
			else
			{
				FreeExternal();
			}
		}
		_content.wrapTarget = null;
		_contentItem = null;
	}

	protected void OnUpdateContent(UpdateContext context)
	{
		if (_contentItem != null && _contentItem.type != PackageItemType.Spine)
		{
			_ = _contentItem.type;
			_ = 10;
		}
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
		_animationName = buffer.ReadS();
		_skinName = buffer.ReadS();
		_playing = buffer.ReadBool();
		_frame = buffer.ReadInt();
		_loop = buffer.ReadBool();
		if (buffer.ReadBool())
		{
			color = buffer.ReadColor();
		}
		if (!string.IsNullOrEmpty(_url))
		{
			LoadContent();
		}
	}
}
