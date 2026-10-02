using System;
using System.Collections.Generic;

namespace FairyGUI;

public class EventDispatcher : IEventDispatcher
{
	private Dictionary<string, EventBridge> _dic;

	private static InputEvent sCurrentInputEvent = new InputEvent();

	public void AddEventListener(string strType, EventCallback1 callback)
	{
		GetBridge(strType).Add(callback);
	}

	public void AddEventListener(string strType, EventCallback0 callback)
	{
		GetBridge(strType).Add(callback);
	}

	public void RemoveEventListener(string strType, EventCallback1 callback)
	{
		if (_dic != null)
		{
			EventBridge value = null;
			if (_dic.TryGetValue(strType, out value))
			{
				value.Remove(callback);
			}
		}
	}

	public void RemoveEventListener(string strType, EventCallback0 callback)
	{
		if (_dic != null)
		{
			EventBridge value = null;
			if (_dic.TryGetValue(strType, out value))
			{
				value.Remove(callback);
			}
		}
	}

	public void AddCapture(string strType, EventCallback1 callback)
	{
		GetBridge(strType).AddCapture(callback);
	}

	public void RemoveCapture(string strType, EventCallback1 callback)
	{
		if (_dic != null)
		{
			EventBridge value = null;
			if (_dic.TryGetValue(strType, out value))
			{
				value.RemoveCapture(callback);
			}
		}
	}

	public void RemoveEventListeners()
	{
		RemoveEventListeners(null);
	}

	public void RemoveEventListeners(string strType)
	{
		if (_dic == null)
		{
			return;
		}
		if (strType != null)
		{
			if (_dic.TryGetValue(strType, out var value))
			{
				value.Clear();
			}
			return;
		}
		foreach (KeyValuePair<string, EventBridge> item in _dic)
		{
			item.Value.Clear();
		}
	}

	public bool hasEventListeners(string strType)
	{
		EventBridge eventBridge = TryGetEventBridge(strType);
		if (eventBridge == null)
		{
			return false;
		}
		return !eventBridge.isEmpty;
	}

	public bool isDispatching(string strType)
	{
		return TryGetEventBridge(strType)?._dispatching ?? false;
	}

	internal EventBridge TryGetEventBridge(string strType)
	{
		if (_dic == null)
		{
			return null;
		}
		EventBridge value = null;
		_dic.TryGetValue(strType, out value);
		return value;
	}

	internal EventBridge GetEventBridge(string strType)
	{
		if (_dic == null)
		{
			_dic = new Dictionary<string, EventBridge>();
		}
		EventBridge value = null;
		if (!_dic.TryGetValue(strType, out value))
		{
			value = new EventBridge(this);
			_dic[strType] = value;
		}
		return value;
	}

	public bool DispatchEvent(string strType)
	{
		return DispatchEvent(strType, null);
	}

	public bool DispatchEvent(string strType, object data)
	{
		return InternalDispatchEvent(strType, null, data, null);
	}

	public bool DispatchEvent(string strType, object data, object initiator)
	{
		return InternalDispatchEvent(strType, null, data, initiator);
	}

	internal bool InternalDispatchEvent(string strType, EventBridge bridge, object data, object initiator)
	{
		if (bridge == null)
		{
			bridge = TryGetEventBridge(strType);
		}
		EventBridge eventBridge = null;
		if (this is DisplayObject && ((DisplayObject)this).gOwner != null)
		{
			eventBridge = ((DisplayObject)this).gOwner.TryGetEventBridge(strType);
		}
		bool flag = bridge != null && !bridge.isEmpty;
		bool flag2 = eventBridge != null && !eventBridge.isEmpty;
		if (flag || flag2)
		{
			EventContext eventContext = EventContext.Get();
			eventContext.initiator = ((initiator != null) ? initiator : this);
			eventContext.type = strType;
			eventContext.data = data;
			if (data is InputEvent)
			{
				sCurrentInputEvent = (InputEvent)data;
			}
			eventContext.inputEvent = sCurrentInputEvent;
			if (flag)
			{
				bridge.CallCaptureInternal(eventContext);
				bridge.CallInternal(eventContext);
			}
			if (flag2)
			{
				eventBridge.CallCaptureInternal(eventContext);
				eventBridge.CallInternal(eventContext);
			}
			EventContext.Return(eventContext);
			eventContext.initiator = null;
			eventContext.sender = null;
			eventContext.data = null;
			return eventContext._defaultPrevented;
		}
		return false;
	}

	public bool DispatchEvent(EventContext context)
	{
		EventBridge eventBridge = TryGetEventBridge(context.type);
		EventBridge eventBridge2 = null;
		if (this is DisplayObject && ((DisplayObject)this).gOwner != null)
		{
			eventBridge2 = ((DisplayObject)this).gOwner.TryGetEventBridge(context.type);
		}
		EventDispatcher sender = context.sender;
		if (eventBridge != null && !eventBridge.isEmpty)
		{
			eventBridge.CallCaptureInternal(context);
			eventBridge.CallInternal(context);
		}
		if (eventBridge2 != null && !eventBridge2.isEmpty)
		{
			eventBridge2.CallCaptureInternal(context);
			eventBridge2.CallInternal(context);
		}
		context.sender = sender;
		return context._defaultPrevented;
	}

	internal bool BubbleEvent(string strType, object data, List<EventBridge> addChain)
	{
		EventContext eventContext = EventContext.Get();
		eventContext.initiator = this;
		eventContext.type = strType;
		eventContext.data = data;
		if (data is InputEvent)
		{
			sCurrentInputEvent = (InputEvent)data;
		}
		eventContext.inputEvent = sCurrentInputEvent;
		List<EventBridge> callChain = eventContext.callChain;
		callChain.Clear();
		GetChainBridges(strType, callChain, bubble: true);
		int count = callChain.Count;
		for (int num = count - 1; num >= 0; num--)
		{
			callChain[num].CallCaptureInternal(eventContext);
			if (eventContext._touchCapture)
			{
				eventContext._touchCapture = false;
				if (strType == "onTouchBegin")
				{
					Stage.inst.AddTouchMonitor(eventContext.inputEvent.touchId, callChain[num].owner);
				}
			}
		}
		if (!eventContext._stopsPropagation)
		{
			for (int i = 0; i < count; i++)
			{
				callChain[i].CallInternal(eventContext);
				if (eventContext._touchCapture)
				{
					eventContext._touchCapture = false;
					if (strType == "onTouchBegin")
					{
						Stage.inst.AddTouchMonitor(eventContext.inputEvent.touchId, callChain[i].owner);
					}
				}
				if (eventContext._stopsPropagation)
				{
					break;
				}
			}
			if (addChain != null)
			{
				count = addChain.Count;
				for (int j = 0; j < count; j++)
				{
					EventBridge eventBridge = addChain[j];
					if (callChain.IndexOf(eventBridge) == -1)
					{
						eventBridge.CallCaptureInternal(eventContext);
						eventBridge.CallInternal(eventContext);
					}
				}
			}
		}
		EventContext.Return(eventContext);
		eventContext.initiator = null;
		eventContext.sender = null;
		eventContext.data = null;
		return eventContext._defaultPrevented;
	}

	public bool BubbleEvent(string strType, object data)
	{
		return BubbleEvent(strType, data, null);
	}

	public bool BroadcastEvent(string strType, object data)
	{
		EventContext eventContext = EventContext.Get();
		eventContext.initiator = this;
		eventContext.type = strType;
		eventContext.data = data;
		if (data is InputEvent)
		{
			sCurrentInputEvent = (InputEvent)data;
		}
		eventContext.inputEvent = sCurrentInputEvent;
		List<EventBridge> callChain = eventContext.callChain;
		callChain.Clear();
		if (this is Container)
		{
			GetChildEventBridges(strType, (Container)this, callChain);
		}
		else if (this is GComponent)
		{
			GetChildEventBridges(strType, (GComponent)this, callChain);
		}
		int count = callChain.Count;
		for (int i = 0; i < count; i++)
		{
			callChain[i].CallInternal(eventContext);
		}
		EventContext.Return(eventContext);
		eventContext.initiator = null;
		eventContext.sender = null;
		eventContext.data = null;
		return eventContext._defaultPrevented;
	}

	private EventBridge GetBridge(string strType)
	{
		if (strType == null)
		{
			throw new Exception("event type cant be null");
		}
		if (_dic == null)
		{
			_dic = new Dictionary<string, EventBridge>();
		}
		EventBridge value = null;
		if (!_dic.TryGetValue(strType, out value))
		{
			value = new EventBridge(this);
			_dic[strType] = value;
		}
		return value;
	}

	private static void GetChildEventBridges(string strType, Container container, List<EventBridge> bridges)
	{
		EventBridge eventBridge = container.TryGetEventBridge(strType);
		if (eventBridge != null)
		{
			bridges.Add(eventBridge);
		}
		if (container.gOwner != null)
		{
			eventBridge = container.gOwner.TryGetEventBridge(strType);
			if (eventBridge != null && !eventBridge.isEmpty)
			{
				bridges.Add(eventBridge);
			}
		}
		int numChildren = container.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			DisplayObject childAt = container.GetChildAt(i);
			if (childAt is Container)
			{
				GetChildEventBridges(strType, (Container)childAt, bridges);
				continue;
			}
			eventBridge = childAt.TryGetEventBridge(strType);
			if (eventBridge != null && !eventBridge.isEmpty)
			{
				bridges.Add(eventBridge);
			}
			if (childAt.gOwner != null)
			{
				eventBridge = childAt.gOwner.TryGetEventBridge(strType);
				if (eventBridge != null && !eventBridge.isEmpty)
				{
					bridges.Add(eventBridge);
				}
			}
		}
	}

	private static void GetChildEventBridges(string strType, GComponent container, List<EventBridge> bridges)
	{
		EventBridge eventBridge = container.TryGetEventBridge(strType);
		if (eventBridge != null)
		{
			bridges.Add(eventBridge);
		}
		int numChildren = container.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			GObject childAt = container.GetChildAt(i);
			if (childAt is GComponent)
			{
				GetChildEventBridges(strType, (GComponent)childAt, bridges);
				continue;
			}
			eventBridge = childAt.TryGetEventBridge(strType);
			if (eventBridge != null)
			{
				bridges.Add(eventBridge);
			}
		}
	}

	internal void GetChainBridges(string strType, List<EventBridge> chain, bool bubble)
	{
		EventBridge eventBridge = TryGetEventBridge(strType);
		if (eventBridge != null && !eventBridge.isEmpty)
		{
			chain.Add(eventBridge);
		}
		if (this is DisplayObject && ((DisplayObject)this).gOwner != null)
		{
			eventBridge = ((DisplayObject)this).gOwner.TryGetEventBridge(strType);
			if (eventBridge != null && !eventBridge.isEmpty)
			{
				chain.Add(eventBridge);
			}
		}
		if (!bubble)
		{
			return;
		}
		if (this is DisplayObject)
		{
			DisplayObject displayObject = (DisplayObject)this;
			while ((displayObject = displayObject.parent) != null)
			{
				eventBridge = displayObject.TryGetEventBridge(strType);
				if (eventBridge != null && !eventBridge.isEmpty)
				{
					chain.Add(eventBridge);
				}
				if (displayObject.gOwner != null)
				{
					eventBridge = displayObject.gOwner.TryGetEventBridge(strType);
					if (eventBridge != null && !eventBridge.isEmpty)
					{
						chain.Add(eventBridge);
					}
				}
			}
		}
		else
		{
			if (!(this is GObject))
			{
				return;
			}
			GObject gObject = (GObject)this;
			while ((gObject = gObject.parent) != null)
			{
				eventBridge = gObject.TryGetEventBridge(strType);
				if (eventBridge != null && !eventBridge.isEmpty)
				{
					chain.Add(eventBridge);
				}
			}
		}
	}
}
