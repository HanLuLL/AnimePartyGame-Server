using UnityEngine;

namespace FairyGUI;

public class LongPressGesture : EventDispatcher
{
	public float trigger;

	public float interval;

	public bool once;

	public int holdRangeRadius;

	private Vector2 _startPoint;

	private bool _started;

	private int _touchId;

	public static float TRIGGER = 1.5f;

	public static float INTERVAL = 1f;

	public GObject host { get; private set; }

	public EventListener onBegin { get; private set; }

	public EventListener onEnd { get; private set; }

	public EventListener onAction { get; private set; }

	public LongPressGesture(GObject host)
	{
		this.host = host;
		trigger = TRIGGER;
		interval = INTERVAL;
		holdRangeRadius = 50;
		Enable(value: true);
		onBegin = new EventListener(this, "onLongPressBegin");
		onEnd = new EventListener(this, "onLongPressEnd");
		onAction = new EventListener(this, "onLongPressAction");
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
				Stage.inst.onTouchEnd.Add(__touchEnd);
			}
			else
			{
				host.onTouchBegin.Add(__touchBegin);
				host.onTouchEnd.Add(__touchEnd);
			}
			return;
		}
		if (host == GRoot.inst)
		{
			Stage.inst.onTouchBegin.Remove(__touchBegin);
			Stage.inst.onTouchEnd.Remove(__touchEnd);
		}
		else
		{
			host.onTouchBegin.Remove(__touchBegin);
			host.onTouchEnd.Remove(__touchEnd);
		}
		Timers.inst.Remove(__timer);
	}

	public void Cancel()
	{
		Timers.inst.Remove(__timer);
		_started = false;
	}

	private void __touchBegin(EventContext context)
	{
		InputEvent inputEvent = context.inputEvent;
		_startPoint = host.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
		_started = false;
		_touchId = inputEvent.touchId;
		Timers.inst.Add(trigger, 1, __timer);
		context.CaptureTouch();
	}

	private void __timer(object param)
	{
		Vector2 vector = host.GlobalToLocal(Stage.inst.GetTouchPosition(_touchId));
		if (Mathf.Pow(vector.x - _startPoint.x, 2f) + Mathf.Pow(vector.y - _startPoint.y, 2f) > Mathf.Pow(holdRangeRadius, 2f))
		{
			Timers.inst.Remove(__timer);
		}
		else if (!_started)
		{
			_started = true;
			onBegin.Call();
			Timers.inst.Add(interval, once ? 1 : 0, __timer);
		}
		else
		{
			onAction.Call();
		}
	}

	private void __touchEnd(EventContext context)
	{
		Timers.inst.Remove(__timer);
		_started = false;
		onEnd.Call();
	}
}
