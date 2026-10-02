using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

internal class TouchInfo
{
	public float x;

	public float y;

	public int touchId;

	public int clickCount;

	public KeyCode keyCode;

	public char character;

	public EventModifiers modifiers;

	public float mouseWheelDelta;

	public int button;

	public float downX;

	public float downY;

	public float downTime;

	public int downFrame;

	public bool began;

	public bool clickCancelled;

	public float lastClickTime;

	public float lastClickX;

	public float lastClickY;

	public int lastClickButton;

	public float holdTime;

	public DisplayObject target;

	public List<DisplayObject> downTargets;

	public DisplayObject lastRollOver;

	public List<EventDispatcher> touchMonitors;

	public InputEvent evt;

	private static List<EventBridge> sHelperChain = new List<EventBridge>();

	public TouchInfo()
	{
		evt = new InputEvent();
		downTargets = new List<DisplayObject>();
		touchMonitors = new List<EventDispatcher>();
		Reset();
	}

	public void Reset()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		touchId = -1;
		x = 0f;
		y = 0f;
		clickCount = 0;
		button = -1;
		keyCode = KeyCode.None;
		character = '\0';
		modifiers = (EventModifiers)0;
		mouseWheelDelta = 0f;
		lastClickTime = 0f;
		began = false;
		target = null;
		downTargets.Clear();
		lastRollOver = null;
		clickCancelled = false;
		touchMonitors.Clear();
	}

	public void UpdateEvent()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		evt.touchId = touchId;
		evt.x = x;
		evt.y = y;
		evt.clickCount = clickCount;
		evt.keyCode = keyCode;
		evt.character = character;
		evt.modifiers = modifiers;
		evt.mouseWheelDelta = mouseWheelDelta;
		evt.button = button;
		evt.holdTime = holdTime;
	}

	public void Begin()
	{
		began = true;
		clickCancelled = false;
		downX = x;
		downY = y;
		downTime = Time.unscaledTime;
		downFrame = Time.frameCount;
		holdTime = 0f;
		downTargets.Clear();
		if (target != null)
		{
			downTargets.Add(target);
			for (DisplayObject parent = target; parent != null; parent = parent.parent)
			{
				downTargets.Add(parent);
			}
		}
	}

	public void Move()
	{
		if (began)
		{
			holdTime = ((Time.frameCount - downFrame == 1) ? (1f / (float)Application.targetFrameRate) : (Time.unscaledTime - downTime));
		}
		UpdateEvent();
		if (Mathf.Abs(x - downX) > 50f || Mathf.Abs(y - downY) > 50f)
		{
			clickCancelled = true;
		}
		if (touchMonitors.Count > 0)
		{
			int count = touchMonitors.Count;
			for (int i = 0; i < count; i++)
			{
				EventDispatcher eventDispatcher = touchMonitors[i];
				if (eventDispatcher != null && (!(eventDispatcher is DisplayObject) || ((DisplayObject)eventDispatcher).stage != null) && (!(eventDispatcher is GObject) || ((GObject)eventDispatcher).onStage))
				{
					eventDispatcher.GetChainBridges("onTouchMove", sHelperChain, bubble: false);
				}
			}
			Stage.inst.BubbleEvent("onTouchMove", evt, sHelperChain);
			sHelperChain.Clear();
		}
		else
		{
			Stage.inst.DispatchEvent("onTouchMove", evt);
		}
	}

	public void End()
	{
		began = false;
		if (downTargets.Count == 0 || clickCancelled || Mathf.Abs(x - downX) > (float)Stage._clickTestThreshold || Mathf.Abs(y - downY) > (float)Stage._clickTestThreshold)
		{
			clickCancelled = true;
			lastClickTime = 0f;
			clickCount = 1;
		}
		else
		{
			if (Time.unscaledTime - lastClickTime < 0.35f && Mathf.Abs(x - lastClickX) < (float)Stage._clickTestThreshold && Mathf.Abs(y - lastClickY) < (float)Stage._clickTestThreshold && lastClickButton == button)
			{
				if (clickCount == 2)
				{
					clickCount = 1;
				}
				else
				{
					clickCount++;
				}
			}
			else
			{
				clickCount = 1;
			}
			lastClickTime = Time.unscaledTime;
			lastClickX = x;
			lastClickY = y;
			lastClickButton = button;
		}
		holdTime = ((Time.frameCount - downFrame == 1) ? (1f / (float)Application.targetFrameRate) : (Time.unscaledTime - downTime));
		UpdateEvent();
		if (touchMonitors.Count > 0)
		{
			int count = touchMonitors.Count;
			for (int i = 0; i < count; i++)
			{
				touchMonitors[i]?.GetChainBridges("onTouchEnd", sHelperChain, bubble: false);
			}
			target.BubbleEvent("onTouchEnd", evt, sHelperChain);
			touchMonitors.Clear();
			sHelperChain.Clear();
		}
		else
		{
			target.BubbleEvent("onTouchEnd", evt);
		}
	}

	public DisplayObject ClickTest()
	{
		if (clickCancelled)
		{
			downTargets.Clear();
			return null;
		}
		DisplayObject displayObject = downTargets[0];
		if (displayObject.stage != null)
		{
			downTargets.Clear();
			return displayObject;
		}
		displayObject = target;
		while (displayObject != null && (downTargets.IndexOf(displayObject) == -1 || displayObject.stage == null))
		{
			displayObject = displayObject.parent;
		}
		downTargets.Clear();
		return displayObject;
	}
}
