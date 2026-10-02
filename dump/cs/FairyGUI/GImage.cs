using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GImage : GObject, IColorGear
{
	private Image _content;

	public Color color
	{
		get
		{
			return _content.color;
		}
		set
		{
			_content.color = value;
			UpdateGear(4);
		}
	}

	public FlipType flip
	{
		get
		{
			return _content.graphics.flip;
		}
		set
		{
			_content.graphics.flip = value;
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

	public NTexture texture
	{
		get
		{
			return _content.texture;
		}
		set
		{
			if (value != null)
			{
				sourceWidth = value.width;
				sourceHeight = value.height;
			}
			else
			{
				sourceWidth = 0;
				sourceHeight = 0;
			}
			initWidth = sourceWidth;
			initHeight = sourceHeight;
			_content.texture = value;
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

	protected override void CreateDisplayObject()
	{
		_content = new Image();
		_content.gOwner = this;
		base.displayObject = _content;
	}

	public override void ConstructFromResource()
	{
		base.gameObjectName = packageItem.name;
		PackageItem branch = packageItem.getBranch();
		sourceWidth = branch.width;
		sourceHeight = branch.height;
		initWidth = sourceWidth;
		initHeight = sourceHeight;
		branch = branch.getHighResolution();
		branch.Load();
		_content.scale9Grid = branch.scale9Grid;
		_content.scaleByTile = branch.scaleByTile;
		_content.tileGridIndice = branch.tileGridIndice;
		_content.texture = branch.texture;
		_content.textureScale = new Vector2((float)branch.width / (float)sourceWidth, (float)branch.height / (float)sourceHeight);
		SetSize(sourceWidth, sourceHeight);
	}

	public override void Setup_BeforeAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_BeforeAdd(buffer, beginPos);
		buffer.Seek(beginPos, 5);
		if (buffer.ReadBool())
		{
			_content.color = buffer.ReadColor();
		}
		_content.graphics.flip = (FlipType)buffer.ReadByte();
		_content.fillMethod = (FillMethod)buffer.ReadByte();
		if (_content.fillMethod != FillMethod.None)
		{
			_content.fillOrigin = buffer.ReadByte();
			_content.fillClockwise = buffer.ReadBool();
			_content.fillAmount = buffer.ReadFloat();
		}
	}
}
