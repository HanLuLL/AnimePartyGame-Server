using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class GearAnimation : GearBase
{
	private Dictionary<string, GearAnimationValue> _storage;

	private GearAnimationValue _default;

	public GearAnimation(GObject owner)
		: base(owner)
	{
	}

	protected override void Init()
	{
		_default = new GearAnimationValue(((IAnimationGear)_owner).playing, ((IAnimationGear)_owner).frame);
		_storage = new Dictionary<string, GearAnimationValue>();
	}

	protected override void AddStatus(string pageId, ByteBuffer buffer)
	{
		GearAnimationValue gearAnimationValue;
		if (pageId == null)
		{
			gearAnimationValue = _default;
		}
		else
		{
			gearAnimationValue = new GearAnimationValue(playing: false, 0);
			_storage[pageId] = gearAnimationValue;
		}
		gearAnimationValue.playing = buffer.ReadBool();
		gearAnimationValue.frame = buffer.ReadInt();
	}

	public override void Apply()
	{
		_owner._gearLocked = true;
		if (!_storage.TryGetValue(_controller.selectedPageId, out var value))
		{
			value = _default;
		}
		IAnimationGear obj = (IAnimationGear)_owner;
		obj.frame = value.frame;
		obj.playing = value.playing;
		_owner._gearLocked = false;
	}

	public override void UpdateState()
	{
		IAnimationGear animationGear = (IAnimationGear)_owner;
		if (!_storage.TryGetValue(_controller.selectedPageId, out var value))
		{
			_storage[_controller.selectedPageId] = new GearAnimationValue(animationGear.playing, animationGear.frame);
			return;
		}
		value.playing = animationGear.playing;
		value.frame = animationGear.frame;
	}
}
