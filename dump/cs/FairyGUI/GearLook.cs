using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GearLook : GearBase, ITweenListener
{
	private Dictionary<string, GearLookValue> _storage;

	private GearLookValue _default;

	public GearLook(GObject owner)
		: base(owner)
	{
	}

	protected override void Init()
	{
		_default = new GearLookValue(_owner.alpha, _owner.rotation, _owner.grayed, _owner.touchable);
		_storage = new Dictionary<string, GearLookValue>();
	}

	protected override void AddStatus(string pageId, ByteBuffer buffer)
	{
		GearLookValue gearLookValue;
		if (pageId == null)
		{
			gearLookValue = _default;
		}
		else
		{
			gearLookValue = new GearLookValue(0f, 0f, grayed: false, touchable: false);
			_storage[pageId] = gearLookValue;
		}
		gearLookValue.alpha = buffer.ReadFloat();
		gearLookValue.rotation = buffer.ReadFloat();
		gearLookValue.grayed = buffer.ReadBool();
		gearLookValue.touchable = buffer.ReadBool();
	}

	public override void Apply()
	{
		if (!_storage.TryGetValue(_controller.selectedPageId, out var value))
		{
			value = _default;
		}
		if (_tweenConfig != null && _tweenConfig.tween && UIPackage._constructing == 0 && !GearBase.disableAllTweenEffect)
		{
			_owner._gearLocked = true;
			_owner.grayed = value.grayed;
			_owner.touchable = value.touchable;
			_owner._gearLocked = false;
			if (_tweenConfig._tweener != null)
			{
				if (_tweenConfig._tweener.endValue.x == value.alpha && _tweenConfig._tweener.endValue.y == value.rotation)
				{
					return;
				}
				_tweenConfig._tweener.Kill(complete: true);
				_tweenConfig._tweener = null;
			}
			bool flag = value.alpha != _owner.alpha;
			bool flag2 = value.rotation != _owner.rotation;
			if (flag || flag2)
			{
				if (_owner.CheckGearController(0, _controller))
				{
					_tweenConfig._displayLockToken = _owner.AddDisplayLock();
				}
				_tweenConfig._tweener = GTween.To(new Vector2(_owner.alpha, _owner.rotation), new Vector2(value.alpha, value.rotation), _tweenConfig.duration).SetDelay(_tweenConfig.delay).SetEase(_tweenConfig.easeType, _tweenConfig.customEase)
					.SetUserData((flag ? 1 : 0) + (flag2 ? 2 : 0))
					.SetTarget(this)
					.SetListener(this);
			}
		}
		else
		{
			_owner._gearLocked = true;
			_owner.alpha = value.alpha;
			_owner.rotation = value.rotation;
			_owner.grayed = value.grayed;
			_owner.touchable = value.touchable;
			_owner._gearLocked = false;
		}
	}

	public void OnTweenStart(GTweener tweener)
	{
	}

	public void OnTweenUpdate(GTweener tweener)
	{
		int num = (int)tweener.userData;
		_owner._gearLocked = true;
		if ((num & 1) != 0)
		{
			_owner.alpha = tweener.value.x;
		}
		if ((num & 2) != 0)
		{
			_owner.rotation = tweener.value.y;
			_owner.InvalidateBatchingState();
		}
		_owner._gearLocked = false;
	}

	public void OnTweenComplete(GTweener tweener)
	{
		_tweenConfig._tweener = null;
		if (_tweenConfig._displayLockToken != 0)
		{
			_owner.ReleaseDisplayLock(_tweenConfig._displayLockToken);
			_tweenConfig._displayLockToken = 0u;
		}
		_owner.DispatchEvent("onGearStop", this);
	}

	public override void UpdateState()
	{
		if (!_storage.TryGetValue(_controller.selectedPageId, out var value))
		{
			_storage[_controller.selectedPageId] = new GearLookValue(_owner.alpha, _owner.rotation, _owner.grayed, _owner.touchable);
			return;
		}
		value.alpha = _owner.alpha;
		value.rotation = _owner.rotation;
		value.grayed = _owner.grayed;
		value.touchable = _owner.touchable;
	}
}
