using UnityEngine;

namespace FairyGUI;

public class RotationGesture : EventDispatcher
{
	public float rotation;

	public float delta;

	public bool snapping;

	private Vector2 _startVector;

	private float _lastRotation;

	private int[] _touches;

	private bool _started;

	private bool _touchBegan;

	public GObject host { get; private set; }

	public EventListener onBegin { get; private set; }

	public EventListener onEnd { get; private set; }

	public EventListener onAction { get; private set; }

	public RotationGesture(GObject host)
	{
		this.host = host;
		Enable(value: true);
		_touches = new int[2];
		snapping = true;
		onBegin = new EventListener(this, "onRotationBegin");
		onEnd = new EventListener(this, "onRotationEnd");
		onAction = new EventListener(this, "onRotationAction");
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
			Vector2 vector = host.GlobalToLocal(Stage.inst.GetTouchPosition(_touches[0]));
			Vector2 vector2 = host.GlobalToLocal(Stage.inst.GetTouchPosition(_touches[1]));
			_startVector = vector - vector2;
			context.CaptureTouch();
		}
	}

	private void __touchMove(EventContext context)
	{
		if (!_touchBegan || Stage.inst.touchCount != 2)
		{
			return;
		}
		InputEvent inputEvent = context.inputEvent;
		Vector2 vector = host.GlobalToLocal(Stage.inst.GetTouchPosition(_touches[0]));
		Vector2 vector2 = host.GlobalToLocal(Stage.inst.GetTouchPosition(_touches[1]));
		Vector2 vector3 = vector - vector2;
		float num = 57.29578f * (Mathf.Atan2(vector3.y, vector3.x) - Mathf.Atan2(_startVector.y, _startVector.x));
		if (snapping)
		{
			num = Mathf.Round(num);
			if (num == 0f)
			{
				return;
			}
		}
		if (!_started && num > 5f)
		{
			_started = true;
			rotation = 0f;
			_lastRotation = 0f;
			onBegin.Call(inputEvent);
		}
		if (_started)
		{
			delta = num - _lastRotation;
			_lastRotation = num;
			rotation += delta;
			onAction.Call(inputEvent);
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
