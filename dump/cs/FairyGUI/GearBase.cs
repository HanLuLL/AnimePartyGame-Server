using FairyGUI.Utils;

namespace FairyGUI;

public abstract class GearBase
{
	public static bool disableAllTweenEffect;

	protected GObject _owner;

	protected Controller _controller;

	protected GearTweenConfig _tweenConfig;

	public Controller controller
	{
		get
		{
			return _controller;
		}
		set
		{
			if (value != _controller)
			{
				_controller = value;
				if (_controller != null)
				{
					Init();
				}
			}
		}
	}

	public GearTweenConfig tweenConfig
	{
		get
		{
			if (_tweenConfig == null)
			{
				_tweenConfig = new GearTweenConfig();
			}
			return _tweenConfig;
		}
	}

	public GearBase(GObject owner)
	{
		_owner = owner;
	}

	public void Dispose()
	{
		if (_tweenConfig != null && _tweenConfig._tweener != null)
		{
			_tweenConfig._tweener.Kill();
			_tweenConfig._tweener = null;
		}
	}

	public void Setup(ByteBuffer buffer)
	{
		_controller = _owner.parent.GetControllerAt(buffer.ReadShort());
		Init();
		int num = buffer.ReadShort();
		if (this is GearDisplay)
		{
			((GearDisplay)this).pages = buffer.ReadSArray(num);
		}
		else if (this is GearDisplay2)
		{
			((GearDisplay2)this).pages = buffer.ReadSArray(num);
		}
		else
		{
			for (int i = 0; i < num; i++)
			{
				string text = buffer.ReadS();
				if (text != null)
				{
					AddStatus(text, buffer);
				}
			}
			if (buffer.ReadBool())
			{
				AddStatus(null, buffer);
			}
		}
		if (buffer.ReadBool())
		{
			_tweenConfig = new GearTweenConfig();
			_tweenConfig.easeType = (EaseType)buffer.ReadByte();
			_tweenConfig.duration = buffer.ReadFloat();
			_tweenConfig.delay = buffer.ReadFloat();
		}
		if (buffer.version >= 2)
		{
			if (this is GearXY)
			{
				if (buffer.ReadBool())
				{
					((GearXY)this).positionsInPercent = true;
					for (int j = 0; j < num; j++)
					{
						string text2 = buffer.ReadS();
						if (text2 != null)
						{
							((GearXY)this).AddExtStatus(text2, buffer);
						}
					}
					if (buffer.ReadBool())
					{
						((GearXY)this).AddExtStatus(null, buffer);
					}
				}
			}
			else if (this is GearDisplay2)
			{
				((GearDisplay2)this).condition = buffer.ReadByte();
			}
		}
		if (buffer.version >= 4 && _tweenConfig != null && _tweenConfig.easeType == EaseType.Custom)
		{
			_tweenConfig.customEase = new CustomEase();
			_tweenConfig.customEase.Create(buffer.ReadPath());
		}
	}

	public virtual void UpdateFromRelations(float dx, float dy)
	{
	}

	protected abstract void AddStatus(string pageId, ByteBuffer buffer);

	protected abstract void Init();

	public abstract void Apply();

	public abstract void UpdateState();
}
