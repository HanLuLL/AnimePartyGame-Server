using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GearColor : GearBase, ITweenListener
{
	private Dictionary<string, GearColorValue> _storage;

	private GearColorValue _default;

	public GearColor(GObject owner)
		: base(owner)
	{
	}

	protected override void Init()
	{
		_default = new GearColorValue();
		_default.color = ((IColorGear)_owner).color;
		if (_owner is ITextColorGear)
		{
			_default.strokeColor = ((ITextColorGear)_owner).strokeColor;
		}
		_storage = new Dictionary<string, GearColorValue>();
	}

	protected override void AddStatus(string pageId, ByteBuffer buffer)
	{
		GearColorValue gearColorValue;
		if (pageId == null)
		{
			gearColorValue = _default;
		}
		else
		{
			gearColorValue = new GearColorValue(Color.black, Color.black);
			_storage[pageId] = gearColorValue;
		}
		gearColorValue.color = buffer.ReadColor();
		gearColorValue.strokeColor = buffer.ReadColor();
	}

	public override void Apply()
	{
		if (!_storage.TryGetValue(_controller.selectedPageId, out var value))
		{
			value = _default;
		}
		if (_tweenConfig != null && _tweenConfig.tween && UIPackage._constructing == 0 && !GearBase.disableAllTweenEffect)
		{
			if (_owner is ITextColorGear && value.strokeColor.a > 0f)
			{
				_owner._gearLocked = true;
				((ITextColorGear)_owner).strokeColor = value.strokeColor;
				_owner._gearLocked = false;
			}
			if (_tweenConfig._tweener != null)
			{
				if (!(_tweenConfig._tweener.endValue.color != value.color))
				{
					return;
				}
				_tweenConfig._tweener.Kill(complete: true);
				_tweenConfig._tweener = null;
			}
			if (((IColorGear)_owner).color != value.color)
			{
				if (_owner.CheckGearController(0, _controller))
				{
					_tweenConfig._displayLockToken = _owner.AddDisplayLock();
				}
				_tweenConfig._tweener = GTween.To(((IColorGear)_owner).color, value.color, _tweenConfig.duration).SetDelay(_tweenConfig.delay).SetEase(_tweenConfig.easeType, _tweenConfig.customEase)
					.SetTarget(this)
					.SetListener(this);
			}
		}
		else
		{
			_owner._gearLocked = true;
			((IColorGear)_owner).color = value.color;
			if (_owner is ITextColorGear && value.strokeColor.a > 0f)
			{
				((ITextColorGear)_owner).strokeColor = value.strokeColor;
			}
			_owner._gearLocked = false;
		}
	}

	public void OnTweenStart(GTweener tweener)
	{
	}

	public void OnTweenUpdate(GTweener tweener)
	{
		_owner._gearLocked = true;
		((IColorGear)_owner).color = tweener.value.color;
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
			value = (_storage[_controller.selectedPageId] = new GearColorValue());
		}
		value.color = ((IColorGear)_owner).color;
		if (_owner is ITextColorGear)
		{
			value.strokeColor = ((ITextColorGear)_owner).strokeColor;
		}
	}
}
