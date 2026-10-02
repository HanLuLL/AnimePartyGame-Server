using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GearXY : GearBase, ITweenListener
{
	public bool positionsInPercent;

	private Dictionary<string, GearXYValue> _storage;

	private GearXYValue _default;

	public GearXY(GObject owner)
		: base(owner)
	{
	}

	protected override void Init()
	{
		_default = new GearXYValue(_owner.x, _owner.y, _owner.x / _owner.parent.width, _owner.y / _owner.parent.height);
		_storage = new Dictionary<string, GearXYValue>();
	}

	protected override void AddStatus(string pageId, ByteBuffer buffer)
	{
		GearXYValue gearXYValue;
		if (pageId == null)
		{
			gearXYValue = _default;
		}
		else
		{
			gearXYValue = new GearXYValue();
			_storage[pageId] = gearXYValue;
		}
		gearXYValue.x = buffer.ReadInt();
		gearXYValue.y = buffer.ReadInt();
	}

	public void AddExtStatus(string pageId, ByteBuffer buffer)
	{
		GearXYValue gearXYValue = ((pageId != null) ? _storage[pageId] : _default);
		gearXYValue.px = buffer.ReadFloat();
		gearXYValue.py = buffer.ReadFloat();
	}

	public override void Apply()
	{
		if (!_storage.TryGetValue(_controller.selectedPageId, out var value))
		{
			value = _default;
		}
		Vector2 vector = default(Vector2);
		if (positionsInPercent && _owner.parent != null)
		{
			vector.x = value.px * _owner.parent.width;
			vector.y = value.py * _owner.parent.height;
		}
		else
		{
			vector.x = value.x;
			vector.y = value.y;
		}
		if (_tweenConfig != null && _tweenConfig.tween && UIPackage._constructing == 0 && !GearBase.disableAllTweenEffect)
		{
			if (_tweenConfig._tweener != null)
			{
				if (_tweenConfig._tweener.endValue.x == vector.x && _tweenConfig._tweener.endValue.y == vector.y)
				{
					return;
				}
				_tweenConfig._tweener.Kill(complete: true);
				_tweenConfig._tweener = null;
			}
			Vector2 xy = _owner.xy;
			if (vector != xy)
			{
				if (_owner.CheckGearController(0, _controller))
				{
					_tweenConfig._displayLockToken = _owner.AddDisplayLock();
				}
				_tweenConfig._tweener = GTween.To(xy, vector, _tweenConfig.duration).SetDelay(_tweenConfig.delay).SetEase(_tweenConfig.easeType, _tweenConfig.customEase)
					.SetTarget(this)
					.SetListener(this);
			}
		}
		else
		{
			_owner._gearLocked = true;
			_owner.SetXY(vector.x, vector.y);
			_owner._gearLocked = false;
		}
	}

	public void OnTweenStart(GTweener tweener)
	{
	}

	public void OnTweenUpdate(GTweener tweener)
	{
		_owner._gearLocked = true;
		_owner.SetXY(tweener.value.x, tweener.value.y);
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
			value = (_storage[_controller.selectedPageId] = new GearXYValue());
		}
		value.x = _owner.x;
		value.y = _owner.y;
		value.px = _owner.x / _owner.parent.width;
		value.py = _owner.y / _owner.parent.height;
	}

	public override void UpdateFromRelations(float dx, float dy)
	{
		if (_controller == null || _storage == null || positionsInPercent)
		{
			return;
		}
		foreach (GearXYValue value in _storage.Values)
		{
			value.x += dx;
			value.y += dy;
		}
		_default.x += dx;
		_default.y += dy;
		UpdateState();
	}
}
