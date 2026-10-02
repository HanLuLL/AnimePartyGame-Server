namespace FairyGUI;

public class DragDropManager
{
	private GLoader _agent;

	private object _sourceData;

	private GObject _source;

	private static DragDropManager _inst;

	public static DragDropManager inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new DragDropManager();
			}
			return _inst;
		}
	}

	public GLoader dragAgent => _agent;

	public bool dragging => _agent.parent != null;

	public DragDropManager()
	{
		_agent = (GLoader)UIObjectFactory.NewObject(ObjectType.Loader);
		_agent.gameObjectName = "DragDropAgent";
		_agent.SetHome(GRoot.inst);
		_agent.touchable = false;
		_agent.draggable = true;
		_agent.SetSize(100f, 100f);
		_agent.SetPivot(0.5f, 0.5f, asAnchor: true);
		_agent.align = AlignType.Center;
		_agent.verticalAlign = VertAlignType.Middle;
		_agent.sortingOrder = int.MaxValue;
		_agent.onDragEnd.Add(__dragEnd);
	}

	public void StartDrag(GObject source, string icon, object sourceData, int touchPointID = -1)
	{
		if (_agent.parent == null)
		{
			_sourceData = sourceData;
			_source = source;
			_agent.url = icon;
			GRoot.inst.AddChild(_agent);
			_agent.xy = GRoot.inst.GlobalToLocal(Stage.inst.GetTouchPosition(touchPointID));
			_agent.StartDrag(touchPointID);
		}
	}

	public void Cancel()
	{
		if (_agent.parent != null)
		{
			_agent.StopDrag();
			GRoot.inst.RemoveChild(_agent);
			_sourceData = null;
		}
	}

	private void __dragEnd(EventContext evt)
	{
		if (_agent.parent == null)
		{
			return;
		}
		GRoot.inst.RemoveChild(_agent);
		object sourceData = _sourceData;
		GObject source = _source;
		_sourceData = null;
		_source = null;
		for (GObject gObject = GRoot.inst.touchTarget; gObject != null; gObject = gObject.parent)
		{
			if (gObject.hasEventListeners("onDrop"))
			{
				gObject.RequestFocus();
				gObject.DispatchEvent("onDrop", sourceData, source);
				break;
			}
		}
	}
}
