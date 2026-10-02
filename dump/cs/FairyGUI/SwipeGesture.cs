using UnityEngine;

namespace FairyGUI;

public class SwipeGesture : EventDispatcher
{
	public Vector2 velocity;

	public Vector2 position;

	public Vector2 delta;

	public int actionDistance;

	public bool snapping;

	private Vector2 _startPoint;

	private Vector2 _lastPoint;

	private float _time;

	private bool _started;

	private bool _touchBegan;

	public static int ACTION_DISTANCE = 200;

	public Vector2 point;

	public GObject host { get; private set; }

	public EventListener onBegin { get; private set; }

	public EventListener onEnd { get; private set; }

	public EventListener onMove { get; private set; }

	public EventListener onAction { get; private set; }

	public SwipeGesture(GObject host)
	{
		this.host = host;
		actionDistance = ACTION_DISTANCE;
		snapping = true;
		Enable(value: true);
		onBegin = new EventListener(this, "onSwipeBegin");
		onEnd = new EventListener(this, "onSwipeEnd");
		onMove = new EventListener(this, "onSwipeMove");
		onAction = new EventListener(this, "onnSwipeAction");
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
		if (Stage.inst.touchCount > 1)
		{
			_touchBegan = false;
			if (_started)
			{
				_started = false;
				onEnd.Call(context.inputEvent);
			}
			return;
		}
		InputEvent inputEvent = context.inputEvent;
		_startPoint = (_lastPoint = host.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y)));
		point = _startPoint;
		_lastPoint = _startPoint;
		_time = Time.unscaledTime;
		_started = false;
		velocity = Vector2.zero;
		position = Vector2.zero;
		_touchBegan = true;
		context.CaptureTouch();
	}

	private void __touchMove(EventContext context)
	{
		if (!_touchBegan || Stage.inst.touchCount > 1)
		{
			return;
		}
		InputEvent inputEvent = context.inputEvent;
		Vector2 vector = (point = host.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y)));
		delta = vector - _lastPoint;
		if (snapping)
		{
			delta.x = Mathf.Round(delta.x);
			delta.y = Mathf.Round(delta.y);
			if (delta.x == 0f && delta.y == 0f)
			{
				return;
			}
		}
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		float num = (Time.unscaledTime - _time) * 60f - 1f;
		if (num > 1f)
		{
			velocity *= Mathf.Pow(0.833f, num);
		}
		velocity = Vector3.Lerp(velocity, delta / unscaledDeltaTime, unscaledDeltaTime * 10f);
		_time = Time.unscaledTime;
		position += delta;
		_lastPoint = vector;
		if (!_started)
		{
			int num2 = ((!Stage.touchScreen) ? 5 : UIConfig.touchDragSensitivity);
			if (Mathf.Abs(delta.x) < (float)num2 && Mathf.Abs(delta.y) < (float)num2)
			{
				return;
			}
			_started = true;
			onBegin.Call(inputEvent);
		}
		onMove.Call(inputEvent);
	}

	private void __touchEnd(EventContext context)
	{
		_touchBegan = false;
		if (_started)
		{
			_started = false;
			InputEvent inputEvent = context.inputEvent;
			Vector2 vector = host.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
			delta = vector - _lastPoint;
			if (snapping)
			{
				delta.x = Mathf.Round(delta.x);
				delta.y = Mathf.Round(delta.y);
			}
			position += delta;
			float num = (Time.unscaledTime - _time) * 60f - 1f;
			if (num > 1f)
			{
				velocity *= Mathf.Pow(0.833f, num);
			}
			if (snapping)
			{
				velocity.x = Mathf.Round(velocity.x);
				velocity.y = Mathf.Round(velocity.y);
			}
			onEnd.Call(inputEvent);
			vector -= _startPoint;
			if (Mathf.Abs(vector.x) > (float)actionDistance || Mathf.Abs(vector.y) > (float)actionDistance)
			{
				onAction.Call(inputEvent);
			}
		}
	}
}
