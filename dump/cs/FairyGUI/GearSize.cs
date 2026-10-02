using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GearSize : GearBase, ITweenListener
{
	private Dictionary<string, GearSizeValue> _storage;

	private GearSizeValue _default;

	public GearSize(GObject owner)
		: base(owner)
	{
	}

	protected override void Init()
	{
		_default = new GearSizeValue(_owner.width, _owner.height, _owner.scaleX, _owner.scaleY);
		_storage = new Dictionary<string, GearSizeValue>();
	}

	protected override void AddStatus(string pageId, ByteBuffer buffer)
	{
		GearSizeValue gearSizeValue;
		if (pageId == null)
		{
			gearSizeValue = _default;
		}
		else
		{
			gearSizeValue = new GearSizeValue(0f, 0f, 1f, 1f);
			_storage[pageId] = gearSizeValue;
		}
		gearSizeValue.width = buffer.ReadInt();
		gearSizeValue.height = buffer.ReadInt();
		gearSizeValue.scaleX = buffer.ReadFloat();
		gearSizeValue.scaleY = buffer.ReadFloat();
	}

	public override void Apply()
	{
		if (!_storage.TryGetValue(_controller.selectedPageId, out var value))
		{
			value = _default;
		}
		if (_tweenConfig != null && _tweenConfig.tween && UIPackage._constructing == 0 && !GearBase.disableAllTweenEffect)
		{
			if (_tweenConfig._tweener != null)
			{
				if (_tweenConfig._tweener.endValue.x == value.width && _tweenConfig._tweener.endValue.y == value.height && _tweenConfig._tweener.endValue.z == value.scaleX && _tweenConfig._tweener.endValue.w == value.scaleY)
				{
					return;
				}
				_tweenConfig._tweener.Kill(complete: true);
				_tweenConfig._tweener = null;
			}
			bool flag = value.width != _owner.width || value.height != _owner.height;
			bool flag2 = value.scaleX != _owner.scaleX || value.scaleY != _owner.scaleY;
			if (flag || flag2)
			{
				if (_owner.CheckGearController(0, _controller))
				{
					_tweenConfig._displayLockToken = _owner.AddDisplayLock();
				}
				_tweenConfig._tweener = GTween.To(new Vector4(_owner.width, _owner.height, _owner.scaleX, _owner.scaleY), new Vector4(value.width, value.height, value.scaleX, value.scaleY), _tweenConfig.duration).SetDelay(_tweenConfig.delay).SetEase(_tweenConfig.easeType, _tweenConfig.customEase)
					.SetUserData((flag ? 1 : 0) + (flag2 ? 2 : 0))
					.SetTarget(this)
					.SetListener(this);
			}
		}
		else
		{
			_owner._gearLocked = true;
			_owner.SetSize(value.width, value.height, _owner.CheckGearController(1, _controller));
			_owner.SetScale(value.scaleX, value.scaleY);
			_owner._gearLocked = false;
		}
	}

	public void OnTweenStart(GTweener tweener)
	{
	}

	public void OnTweenUpdate(GTweener tweener)
	{
		_owner._gearLocked = true;
		int num = (int)tweener.userData;
		if ((num & 1) != 0)
		{
			_owner.SetSize(tweener.value.x, tweener.value.y, _owner.CheckGearController(1, _controller));
		}
		if ((num & 2) != 0)
		{
			_owner.SetScale(tweener.value.z, tweener.value.w);
		}
		_owner._gearLocked = false;
		_owner.InvalidateBatchingState();
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
			_storage[_controller.selectedPageId] = new GearSizeValue(_owner.width, _owner.height, _owner.scaleX, _owner.scaleY);
			return;
		}
		value.width = _owner.width;
		value.height = _owner.height;
		value.scaleX = _owner.scaleX;
		value.scaleY = _owner.scaleY;
	}

	public override void UpdateFromRelations(float dx, float dy)
	{
		if (_controller == null || _storage == null)
		{
			return;
		}
		foreach (GearSizeValue value in _storage.Values)
		{
			value.width += dx;
			value.height += dy;
		}
		_default.width += dx;
		_default.height += dy;
		UpdateState();
	}
}
