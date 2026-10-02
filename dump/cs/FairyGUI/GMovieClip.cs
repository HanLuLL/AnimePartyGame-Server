using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GMovieClip : GObject, IAnimationGear, IColorGear
{
	private MovieClip _content;

	private EventListener _onPlayEnd;

	public EventListener onPlayEnd => _onPlayEnd ?? (_onPlayEnd = new EventListener(this, "onPlayEnd"));

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

	protected override void CreateDisplayObject()
	{
		_content = new MovieClip();
		_content.gOwner = this;
		_content.ignoreEngineTimeScale = true;
		base.displayObject = _content;
	}

	public void Rewind()
	{
		_content.Rewind();
	}

	public void SyncStatus(GMovieClip anotherMc)
	{
		_content.SyncStatus(anotherMc._content);
	}

	public void Advance(float time)
	{
		_content.Advance(time);
	}

	public void SetPlaySettings(int start, int end, int times, int endAt)
	{
		((MovieClip)base.displayObject).SetPlaySettings(start, end, times, endAt);
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
		_content.interval = branch.interval;
		_content.swing = branch.swing;
		_content.repeatDelay = branch.repeatDelay;
		_content.frames = branch.frames;
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
		_content.frame = buffer.ReadInt();
		_content.playing = buffer.ReadBool();
	}
}
