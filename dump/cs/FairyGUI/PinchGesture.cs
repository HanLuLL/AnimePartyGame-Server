using UnityEngine;

namespace FairyGUI;

public class PinchGesture : EventDispatcher
{
	public float scale;

	public float delta;

	private float _startDistance;

	private float _lastScale;

	private int[] _touches;

	private bool _started;

	private bool _touchBegan;

	public GObject host { get; private set; }

	public EventListener onBegin { get; private set; }

	public EventListener onEnd { get; private set; }

	public EventListener onAction { get; private set; }

	public PinchGesture(GObject host)
	{
		this.host = host;
		Enable(value: true);
		_touches = new int[2];
		onBegin = new EventListener(this, "onPinchBegin");
		onEnd = new EventListener(this, "onPinchEnd");
		onAction = new EventListener(this, "onPinchAction");
	}

	public void Dispose()
	{
		Enable(value: false);
		host = null;
	}

	public void Enable(bool value)
	{
		if (value)
		{
			if (host == GRoot.inst)
			{
				Stage.inst.onTouchBegin.Add(__touchBegin);
				Stage.inst.onTouchMove.Add(__touchMove);
				Stage.inst.onTouchEnd.Add(__touchEnd);
			}
			else
			{
				host.onTouchBegin.Add(__touchBegin);
				host.onTouchMove.Add(__touchMove);
				host.onTouchEnd.Add(__touchEnd);
			}
			return;
		}
		_started = false;
		_touchBegan = false;
		if (host == GRoot.inst)
		{
			Stage.inst.onTouchBegin.Remove(__touchBegin);
			Stage.inst.onTouchMove.Remove(__touchMove);
			Stage.inst.onTouchEnd.Remove(__touchEnd);
		}
		else
		{
			host.onTouchBegin.Remove(__touchBegin);
			host.onTouchMove.Remove(__touchMove);
			host.onTouchEnd.Remove(__touchEnd);
		}
	}

	private void __touchBegin(EventContext context)
	{
		if (Stage.inst.touchCount == 2 && !_started && !_touchBegan)
		{
			_touchBegan = true;
			Stage.inst.GetAllTouch(_touches);
			Vector2 a = host.GlobalToLocal(Stage.inst.GetTouchPosition(_touches[0]));
			Vector2 b = host.GlobalToLocal(Stage.inst.GetTouchPosition(_touches[1]));
			_startDistance = Vector2.Distance(a, b);
			context.CaptureTouch();
		}
	}

	private void __touchMove(EventContext context)
	{
		if (_touchBegan && Stage.inst.touchCount == 2)
		{
			InputEvent inputEvent = context.inputEvent;
			Vector2 a = host.GlobalToLocal(Stage.inst.GetTouchPosition(_touches[0]));
			Vector2 b = host.GlobalToLocal(Stage.inst.GetTouchPosition(_touches[1]));
			float num = Vector2.Distance(a, b);
			if (!_started && Mathf.Abs(num - _startDistance) > (float)UIConfig.touchDragSensitivity)
			{
				_started = true;
				scale = 1f;
				_lastScale = 1f;
				onBegin.Call(inputEvent);
			}
			if (_started)
			{
				float num2 = num / _startDistance;
				delta = num2 - _lastScale;
				_lastScale = num2;
				scale += delta;
				onAction.Call(inputEvent);
			}
		}
	}

	private void __touchEnd(EventContext context)
	{
		_touchBegan = false;
		if (_started)
		{
			_started = false;
			onEnd.Call(context.inputEvent);
		}
	}
}
