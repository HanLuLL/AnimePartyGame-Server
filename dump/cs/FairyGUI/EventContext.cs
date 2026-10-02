using System.Collections.Generic;

namespace FairyGUI;

public class EventContext
{
	public string type;

	public object data;

	internal bool _defaultPrevented;

	internal bool _stopsPropagation;

	internal bool _touchCapture;

	internal List<EventBridge> callChain = new List<EventBridge>();

	private static Stack<EventContext> pool = new Stack<EventContext>();

	public EventDispatcher sender { get; internal set; }

	public object initiator { get; internal set; }

	public InputEvent inputEvent { get; internal set; }

	public bool isDefaultPrevented => _defaultPrevented;

	public void StopPropagation()
	{
		_stopsPropagation = true;
	}

	public void PreventDefault()
	{
		_defaultPrevented = true;
	}

	public void CaptureTouch()
	{
		_touchCapture = true;
	}

	internal static EventContext Get()
	{
		if (pool.Count > 0)
		{
			EventContext eventContext = pool.Pop();
			eventContext._stopsPropagation = false;
			eventContext._defaultPrevented = false;
			eventContext._touchCapture = false;
			return eventContext;
		}
		return new EventContext();
	}

	internal static void Return(EventContext value)
	{
		pool.Push(value);
	}
}
