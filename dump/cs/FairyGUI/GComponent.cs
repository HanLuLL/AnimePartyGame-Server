using System;
using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GComponent : GObject
{
	private bool _touchableAll = true;

	private bool _touchableChildes = true;

	internal List<GObject> _children;

	internal List<Controller> _controllers;

	internal List<Transition> _transitions;

	internal bool _buildingDisplayList;

	protected Margin _margin;

	protected bool _trackBounds;

	protected bool _boundsChanged;

	protected ChildrenRenderOrder _childrenRenderOrder;

	protected int _apexIndex;

	internal Vector2 _alignOffset;

	private Vector2 _clipSoftness;

	private int _sortingChildCount;

	private Action _buildDelegate;

	private Controller _applyingController;

	private EventListener _onDrop;

	public bool touchableAll
	{
		get
		{
			return _touchableAll;
		}
		set
		{
			if (_touchableAll != value)
			{
				_touchableAll = value;
				base.touchable = value;
				touchableChildes = value;
			}
		}
	}

	public bool touchableChildes
	{
		get
		{
			return _touchableChildes;
		}
		set
		{
			if (_touchableChildes == value)
			{
				return;
			}
			_touchableChildes = value;
			foreach (GObject child in _children)
			{
				child.touchable = value;
			}
		}
	}

	public Container rootContainer { get; private set; }

	public Container container { get; protected set; }

	public ScrollPane scrollPane { get; private set; }

	public EventListener onDrop => _onDrop ?? (_onDrop = new EventListener(this, "onDrop"));

	public bool fairyBatching
	{
		get
		{
			return rootContainer.fairyBatching;
		}
		set
		{
			rootContainer.fairyBatching = value;
		}
	}

	public bool opaque
	{
		get
		{
			return rootContainer.opaque;
		}
		set
		{
			rootContainer.opaque = value;
		}
	}

	public Margin margin
	{
		get
		{
			return _margin;
		}
		set
		{
			_margin = value;
			if (rootContainer.clipRect.HasValue && scrollPane == null)
			{
				container.SetXY((float)_margin.left + _alignOffset.x, (float)_margin.top + _alignOffset.y);
			}
			HandleSizeChanged();
		}
	}

	public ChildrenRenderOrder childrenRenderOrder
	{
		get
		{
			return _childrenRenderOrder;
		}
		set
		{
			if (_childrenRenderOrder != value)
			{
				_childrenRenderOrder = value;
				BuildNativeDisplayList();
			}
		}
	}

	public int apexIndex
	{
		get
		{
			return _apexIndex;
		}
		set
		{
			if (_apexIndex != value)
			{
				_apexIndex = value;
				if (_childrenRenderOrder == ChildrenRenderOrder.Arch)
				{
					BuildNativeDisplayList();
				}
			}
		}
	}

	public bool tabStopChildren
	{
		get
		{
			return rootContainer.tabStopChildren;
		}
		set
		{
			rootContainer.tabStopChildren = value;
		}
	}

	public int numChildren => _children.Count;

	public List<Controller> Controllers => _controllers;

	public Vector2 clipSoftness
	{
		get
		{
			return _clipSoftness;
		}
		set
		{
			_clipSoftness = value;
			if (scrollPane != null)
			{
				scrollPane.UpdateClipSoft();
			}
			else if (_clipSoftness.x > 0f || _clipSoftness.y > 0f)
			{
				rootContainer.clipSoftness = new Vector4(value.x, value.y, value.x, value.y);
			}
			else
			{
				rootContainer.clipSoftness = null;
			}
		}
	}

	public DisplayObject mask
	{
		get
		{
			return container.mask;
		}
		set
		{
			container.mask = value;
			if (value != null && value.parent != container)
			{
				container.AddChild(value);
			}
		}
	}

	public bool reversedMask
	{
		get
		{
			return container.reversedMask;
		}
		set
		{
			container.reversedMask = value;
		}
	}

	public string baseUserData
	{
		get
		{
			ByteBuffer rawData = packageItem.rawData;
			rawData.Seek(0, 4);
			return rawData.ReadS();
		}
	}

	public float viewWidth
	{
		get
		{
			if (scrollPane != null)
			{
				return scrollPane.viewWidth;
			}
			return base.width - (float)_margin.left - (float)_margin.right;
		}
		set
		{
			if (scrollPane != null)
			{
				scrollPane.viewWidth = value;
			}
			else
			{
				base.width = value + (float)_margin.left + (float)_margin.right;
			}
		}
	}

	public float viewHeight
	{
		get
		{
			if (scrollPane != null)
			{
				return scrollPane.viewHeight;
			}
			return base.height - (float)_margin.top - (float)_margin.bottom;
		}
		set
		{
			if (scrollPane != null)
			{
				scrollPane.viewHeight = value;
			}
			else
			{
				base.height = value + (float)_margin.top + (float)_margin.bottom;
			}
		}
	}

	public GComponent()
	{
		_children = new List<GObject>();
		_controllers = new List<Controller>();
		_transitions = new List<Transition>();
		_margin = default(Margin);
		_buildDelegate = BuildNativeDisplayList;
	}

	protected override void CreateDisplayObject()
	{
		rootContainer = new Container("GComponent");
		rootContainer.gOwner = this;
		rootContainer.onUpdate += OnUpdate;
		container = rootContainer;
		base.displayObject = rootContainer;
	}

	public override void Dispose()
	{
		int count = _transitions.Count;
		for (int i = 0; i < count; i++)
		{
			_transitions[i].Dispose();
		}
		count = _controllers.Count;
		for (int j = 0; j < count; j++)
		{
			_controllers[j].Dispose();
		}
		if (scrollPane != null)
		{
			scrollPane.Dispose();
		}
		base.Dispose();
		count = _children.Count;
		for (int num = count - 1; num >= 0; num--)
		{
			GObject gObject = _children[num];
			gObject.InternalSetParent(null);
			gObject.Dispose();
		}
	}

	public void InvalidateBatchingState(bool childChanged)
	{
		if (childChanged)
		{
			container.InvalidateBatchingState(childChanged);
		}
		else
		{
			rootContainer.InvalidateBatchingState();
		}
	}

	public GObject AddChild(GObject child)
	{
		AddChildAt(child, _children.Count);
		return child;
	}

	public virtual GObject AddChildAt(GObject child, int index)
	{
		if (index >= 0 && index <= _children.Count)
		{
			if (child.parent == this)
			{
				SetChildIndex(child, index);
			}
			else
			{
				child.RemoveFromParent();
				child.InternalSetParent(this);
				int count = _children.Count;
				if (child.sortingOrder != 0)
				{
					_sortingChildCount++;
					index = GetInsertPosForSortingChild(child);
				}
				else if (_sortingChildCount > 0 && index > count - _sortingChildCount)
				{
					index = count - _sortingChildCount;
				}
				if (index == count)
				{
					_children.Add(child);
				}
				else
				{
					_children.Insert(index, child);
				}
				ChildStateChanged(child);
				SetBoundsChangedFlag();
			}
			return child;
		}
		throw new Exception("Invalid child index: " + index + ">" + _children.Count);
	}

	private int GetInsertPosForSortingChild(GObject target)
	{
		int count = _children.Count;
		int i;
		for (i = 0; i < count; i++)
		{
			GObject gObject = _children[i];
			if (gObject != target && target.sortingOrder < gObject.sortingOrder)
			{
				break;
			}
		}
		return i;
	}

	public GObject RemoveChild(GObject child)
	{
		return RemoveChild(child, dispose: false);
	}

	public GObject RemoveChild(GObject child, bool dispose)
	{
		int num = _children.IndexOf(child);
		if (num != -1)
		{
			RemoveChildAt(num, dispose);
		}
		return child;
	}

	public GObject RemoveChildAt(int index)
	{
		return RemoveChildAt(index, dispose: false);
	}

	public virtual GObject RemoveChildAt(int index, bool dispose)
	{
		if (index >= 0 && index < numChildren)
		{
			GObject gObject = _children[index];
			gObject.InternalSetParent(null);
			if (gObject.sortingOrder != 0)
			{
				_sortingChildCount--;
			}
			_children.RemoveAt(index);
			gObject.group = null;
			if (gObject.inContainer)
			{
				container.RemoveChild(gObject.displayObject);
				if (_childrenRenderOrder == ChildrenRenderOrder.Arch)
				{
					UpdateContext.OnBegin -= _buildDelegate;
					UpdateContext.OnBegin += _buildDelegate;
				}
			}
			if (dispose)
			{
				gObject.Dispose();
			}
			SetBoundsChangedFlag();
			return gObject;
		}
		throw new Exception("Invalid child index: " + index + ">" + numChildren);
	}

	public void RemoveChildren()
	{
		RemoveChildren(0, -1, dispose: false);
	}

	public void RemoveChildren(int beginIndex, int endIndex, bool dispose)
	{
		if (endIndex < 0 || endIndex >= numChildren)
		{
			endIndex = numChildren - 1;
		}
		for (int i = beginIndex; i <= endIndex; i++)
		{
			RemoveChildAt(beginIndex, dispose);
		}
	}

	public GObject GetChildAt(int index)
	{
		if (index >= 0 && index < numChildren)
		{
			return _children[index];
		}
		throw new Exception("Invalid child index: " + index + ">" + numChildren);
	}

	public GObject GetChild(string name)
	{
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			if (_children[i].name == name)
			{
				return _children[i];
			}
		}
		return null;
	}

	public GObject GetChildByPath(string path)
	{
		string[] array = path.Split('.');
		int num = array.Length;
		GComponent gComponent = this;
		GObject gObject = null;
		for (int i = 0; i < num; i++)
		{
			gObject = gComponent.GetChild(array[i]);
			if (gObject == null)
			{
				break;
			}
			if (i != num - 1)
			{
				if (!(gObject is GComponent))
				{
					gObject = null;
					break;
				}
				gComponent = (GComponent)gObject;
			}
		}
		return gObject;
	}

	public GObject GetVisibleChild(string name)
	{
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			GObject gObject = _children[i];
			if (gObject.internalVisible && gObject.internalVisible2 && gObject.name == name)
			{
				return gObject;
			}
		}
		return null;
	}

	public GObject GetChildInGroup(GGroup group, string name)
	{
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			GObject gObject = _children[i];
			if (gObject.group == group && gObject.name == name)
			{
				return gObject;
			}
		}
		return null;
	}

	internal GObject GetChildById(string id)
	{
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			if (_children[i].id == id)
			{
				return _children[i];
			}
		}
		return null;
	}

	public GObject[] GetChildren()
	{
		return _children.ToArray();
	}

	public int GetChildIndex(GObject child)
	{
		return _children.IndexOf(child);
	}

	public void SetChildIndex(GObject child, int index)
	{
		int num = _children.IndexOf(child);
		if (num == -1)
		{
			throw new ArgumentException("Not a child of this container");
		}
		if (child.sortingOrder != 0)
		{
			return;
		}
		if (_sortingChildCount > 0)
		{
			int count = _children.Count;
			if (index > count - _sortingChildCount - 1)
			{
				index = count - _sortingChildCount - 1;
			}
		}
		_SetChildIndex(child, num, index);
	}

	public int SetChildIndexBefore(GObject child, int index)
	{
		int num = _children.IndexOf(child);
		if (num == -1)
		{
			throw new ArgumentException("Not a child of this container");
		}
		if (child.sortingOrder != 0)
		{
			return num;
		}
		int count = _children.Count;
		if (_sortingChildCount > 0 && index > count - _sortingChildCount - 1)
		{
			index = count - _sortingChildCount - 1;
		}
		if (num < index)
		{
			return _SetChildIndex(child, num, index - 1);
		}
		return _SetChildIndex(child, num, index);
	}

	private int _SetChildIndex(GObject child, int oldIndex, int index)
	{
		int count = _children.Count;
		if (index > count)
		{
			index = count;
		}
		if (oldIndex == index)
		{
			return oldIndex;
		}
		_children.RemoveAt(oldIndex);
		if (index >= count)
		{
			_children.Add(child);
		}
		else
		{
			_children.Insert(index, child);
		}
		if (child.inContainer)
		{
			int num = 0;
			if (_childrenRenderOrder == ChildrenRenderOrder.Ascent)
			{
				for (int i = 0; i < index; i++)
				{
					if (_children[i].inContainer)
					{
						num++;
					}
				}
				container.SetChildIndex(child.displayObject, num);
			}
			else if (_childrenRenderOrder == ChildrenRenderOrder.Descent)
			{
				for (int num2 = count - 1; num2 > index; num2--)
				{
					if (_children[num2].inContainer)
					{
						num++;
					}
				}
				container.SetChildIndex(child.displayObject, num);
			}
			else
			{
				UpdateContext.OnBegin -= _buildDelegate;
				UpdateContext.OnBegin += _buildDelegate;
			}
			SetBoundsChangedFlag();
		}
		return index;
	}

	public void SwapChildren(GObject child1, GObject child2)
	{
		int num = _children.IndexOf(child1);
		int num2 = _children.IndexOf(child2);
		if (num == -1 || num2 == -1)
		{
			throw new Exception("Not a child of this container");
		}
		SwapChildrenAt(num, num2);
	}

	public void SwapChildrenAt(int index1, int index2)
	{
		GObject child = _children[index1];
		GObject child2 = _children[index2];
		SetChildIndex(child, index2);
		SetChildIndex(child2, index1);
	}

	public bool IsAncestorOf(GObject obj)
	{
		if (obj == null)
		{
			return false;
		}
		for (GComponent gComponent = obj.parent; gComponent != null; gComponent = gComponent.parent)
		{
			if (gComponent == this)
			{
				return true;
			}
		}
		return false;
	}

	public void ChangeChildrenOrder(IList<GObject> objs)
	{
		int count = objs.Count;
		for (int i = 0; i < count; i++)
		{
			GObject gObject = objs[i];
			if (gObject.parent != this)
			{
				throw new Exception("Not a child of this container");
			}
			_children[i] = gObject;
		}
		BuildNativeDisplayList();
		SetBoundsChangedFlag();
	}

	public void AddController(Controller controller)
	{
		_controllers.Add(controller);
		controller.parent = this;
		ApplyController(controller);
	}

	public Controller GetControllerAt(int index)
	{
		return _controllers[index];
	}

	public Controller GetController(string name)
	{
		int count = _controllers.Count;
		for (int i = 0; i < count; i++)
		{
			Controller controller = _controllers[i];
			if (controller.name == name)
			{
				return controller;
			}
		}
		return null;
	}

	public void RemoveController(Controller c)
	{
		int num = _controllers.IndexOf(c);
		if (num == -1)
		{
			throw new Exception("controller not exists: " + c.name);
		}
		c.parent = null;
		_controllers.RemoveAt(num);
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			_children[i].HandleControllerChanged(c);
		}
	}

	public Transition GetTransitionAt(int index)
	{
		return _transitions[index];
	}

	public Transition GetTransition(string name)
	{
		int count = _transitions.Count;
		for (int i = 0; i < count; i++)
		{
			Transition transition = _transitions[i];
			if (transition.name == name)
			{
				return transition;
			}
		}
		return null;
	}

	internal void ChildStateChanged(GObject child)
	{
		if (_buildingDisplayList)
		{
			return;
		}
		int count = _children.Count;
		if (child is GGroup)
		{
			for (int i = 0; i < count; i++)
			{
				GObject gObject = _children[i];
				if (gObject.group == child)
				{
					ChildStateChanged(gObject);
				}
			}
		}
		else
		{
			if (child.displayObject == null)
			{
				return;
			}
			if (child.internalVisible)
			{
				if (child.displayObject.parent != null)
				{
					return;
				}
				if (_childrenRenderOrder == ChildrenRenderOrder.Ascent)
				{
					int num = 0;
					for (int j = 0; j < count; j++)
					{
						GObject gObject2 = _children[j];
						if (gObject2 == child)
						{
							break;
						}
						if (gObject2.displayObject != null && gObject2.displayObject.parent != null)
						{
							num++;
						}
					}
					container.AddChildAt(child.displayObject, num);
				}
				else if (_childrenRenderOrder == ChildrenRenderOrder.Descent)
				{
					int num2 = 0;
					for (int num3 = count - 1; num3 >= 0; num3--)
					{
						GObject gObject3 = _children[num3];
						if (gObject3 == child)
						{
							break;
						}
						if (gObject3.displayObject != null && gObject3.displayObject.parent != null)
						{
							num2++;
						}
					}
					container.AddChildAt(child.displayObject, num2);
				}
				else
				{
					container.AddChild(child.displayObject);
					UpdateContext.OnBegin -= _buildDelegate;
					UpdateContext.OnBegin += _buildDelegate;
				}
			}
			else if (child.displayObject.parent != null)
			{
				container.RemoveChild(child.displayObject);
				if (_childrenRenderOrder == ChildrenRenderOrder.Arch)
				{
					UpdateContext.OnBegin -= _buildDelegate;
					UpdateContext.OnBegin += _buildDelegate;
				}
			}
		}
	}

	private void BuildNativeDisplayList()
	{
		if (base.displayObject == null || base.displayObject.isDisposed)
		{
			return;
		}
		int count = _children.Count;
		if (count == 0)
		{
			return;
		}
		switch (_childrenRenderOrder)
		{
		case ChildrenRenderOrder.Ascent:
		{
			for (int j = 0; j < count; j++)
			{
				GObject gObject3 = _children[j];
				if (gObject3.displayObject != null && gObject3.internalVisible)
				{
					container.AddChild(gObject3.displayObject);
				}
			}
			break;
		}
		case ChildrenRenderOrder.Descent:
		{
			for (int num3 = count - 1; num3 >= 0; num3--)
			{
				GObject gObject4 = _children[num3];
				if (gObject4.displayObject != null && gObject4.internalVisible)
				{
					container.AddChild(gObject4.displayObject);
				}
			}
			break;
		}
		case ChildrenRenderOrder.Arch:
		{
			int num = Mathf.Clamp(_apexIndex, 0, count);
			for (int i = 0; i < num; i++)
			{
				GObject gObject = _children[i];
				if (gObject.displayObject != null && gObject.internalVisible)
				{
					container.AddChild(gObject.displayObject);
				}
			}
			for (int num2 = count - 1; num2 >= num; num2--)
			{
				GObject gObject2 = _children[num2];
				if (gObject2.displayObject != null && gObject2.internalVisible)
				{
					container.AddChild(gObject2.displayObject);
				}
			}
			break;
		}
		}
	}

	internal void ApplyController(Controller c)
	{
		_applyingController = c;
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			_children[i].HandleControllerChanged(c);
		}
		_applyingController = null;
		c.RunActions();
	}

	private void ApplyAllControllers()
	{
		int count = _controllers.Count;
		for (int i = 0; i < count; i++)
		{
			Controller c = _controllers[i];
			ApplyController(c);
		}
	}

	internal void AdjustRadioGroupDepth(GObject obj, Controller c)
	{
		int count = _children.Count;
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < count; i++)
		{
			GObject gObject = _children[i];
			if (gObject == obj)
			{
				num = i;
			}
			else if (gObject is GButton && ((GButton)gObject).relatedController == c && i > num2)
			{
				num2 = i;
			}
		}
		if (num < num2)
		{
			if (_applyingController != null)
			{
				_children[num2].HandleControllerChanged(_applyingController);
			}
			SwapChildrenAt(num, num2);
		}
	}

	public bool IsChildInView(GObject child)
	{
		if (scrollPane != null)
		{
			return scrollPane.IsChildInView(child);
		}
		if (rootContainer.clipRect.HasValue)
		{
			if (child.x + child.width >= 0f && child.x <= base.width && child.y + child.height >= 0f)
			{
				return child.y <= base.height;
			}
			return false;
		}
		return true;
	}

	public virtual int GetFirstChildInView()
	{
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			GObject child = _children[i];
			if (IsChildInView(child))
			{
				return i;
			}
		}
		return -1;
	}

	protected void SetupScroll(ByteBuffer buffer)
	{
		if (rootContainer == container)
		{
			container = new Container();
			rootContainer.AddChild(container);
		}
		scrollPane = new ScrollPane(this);
		scrollPane.Setup(buffer);
	}

	protected void SetupOverflow(OverflowType overflow)
	{
		if (overflow == OverflowType.Hidden)
		{
			if (rootContainer == container)
			{
				container = new Container();
				rootContainer.AddChild(container);
			}
			UpdateClipRect();
			container.SetXY(_margin.left, _margin.top);
		}
		else if (_margin.left != 0 || _margin.top != 0)
		{
			if (rootContainer == container)
			{
				container = new Container();
				rootContainer.AddChild(container);
			}
			container.SetXY(_margin.left, _margin.top);
		}
	}

	private void UpdateClipRect()
	{
		if (scrollPane == null)
		{
			float num = base.width - (float)(_margin.left + _margin.right);
			float num2 = base.height - (float)(_margin.top + _margin.bottom);
			rootContainer.clipRect = new Rect(_margin.left, _margin.top, num, num2);
		}
		else
		{
			rootContainer.clipRect = new Rect(0f, 0f, base.width, base.height);
		}
	}

	protected override void HandleSizeChanged()
	{
		base.HandleSizeChanged();
		if (scrollPane != null)
		{
			scrollPane.OnOwnerSizeChanged();
		}
		if (rootContainer.clipRect.HasValue)
		{
			UpdateClipRect();
		}
	}

	protected override void HandleGrayedChanged()
	{
		Controller controller = GetController("grayed");
		if (controller != null)
		{
			controller.selectedIndex = (base.grayed ? 1 : 0);
		}
		else
		{
			base.HandleGrayedChanged();
		}
	}

	public override void HandleControllerChanged(Controller c)
	{
		base.HandleControllerChanged(c);
		if (scrollPane != null)
		{
			scrollPane.HandleControllerChanged(c);
		}
	}

	public void SetBoundsChangedFlag()
	{
		if (scrollPane != null || _trackBounds)
		{
			_boundsChanged = true;
		}
	}

	public void EnsureBoundsCorrect()
	{
		if (_boundsChanged)
		{
			UpdateBounds();
		}
	}

	protected virtual void UpdateBounds()
	{
		float num;
		float num2;
		float aw;
		float ah;
		if (_children.Count > 0)
		{
			num = 2.1474836E+09f;
			num2 = 2.1474836E+09f;
			float num3 = -2.1474836E+09f;
			float num4 = -2.1474836E+09f;
			int count = _children.Count;
			for (int i = 0; i < count; i++)
			{
				GObject gObject = _children[i];
				float num5 = gObject.x;
				if (num5 < num)
				{
					num = num5;
				}
				num5 = gObject.y;
				if (num5 < num2)
				{
					num2 = num5;
				}
				num5 = gObject.x + gObject.actualWidth;
				if (num5 > num3)
				{
					num3 = num5;
				}
				num5 = gObject.y + gObject.actualHeight;
				if (num5 > num4)
				{
					num4 = num5;
				}
			}
			aw = num3 - num;
			ah = num4 - num2;
		}
		else
		{
			num = 0f;
			num2 = 0f;
			aw = 0f;
			ah = 0f;
		}
		SetBounds(num, num2, aw, ah);
	}

	protected void SetBounds(float ax, float ay, float aw, float ah)
	{
		_boundsChanged = false;
		if (scrollPane != null)
		{
			scrollPane.SetContentSize(Mathf.RoundToInt(ax + aw), Mathf.RoundToInt(ay + ah));
		}
	}

	protected internal virtual void GetSnappingPosition(ref float xValue, ref float yValue)
	{
		int count = _children.Count;
		if (count == 0)
		{
			return;
		}
		EnsureBoundsCorrect();
		GObject gObject = null;
		int i = 0;
		if (yValue != 0f)
		{
			for (; i < count; i++)
			{
				gObject = _children[i];
				if (!(yValue < gObject.y))
				{
					continue;
				}
				if (i == 0)
				{
					yValue = 0f;
					break;
				}
				GObject gObject2 = _children[i - 1];
				if (yValue < gObject2.y + gObject2.height / 2f)
				{
					yValue = gObject2.y;
				}
				else
				{
					yValue = gObject.y;
				}
				break;
			}
			if (i == count)
			{
				yValue = gObject.y;
			}
		}
		if (xValue == 0f)
		{
			return;
		}
		if (i > 0)
		{
			i--;
		}
		for (; i < count; i++)
		{
			gObject = _children[i];
			if (!(xValue < gObject.x))
			{
				continue;
			}
			if (i == 0)
			{
				xValue = 0f;
				break;
			}
			GObject gObject3 = _children[i - 1];
			if (xValue < gObject3.x + gObject3.width / 2f)
			{
				xValue = gObject3.x;
			}
			else
			{
				xValue = gObject.x;
			}
			break;
		}
		if (i == count)
		{
			xValue = gObject.x;
		}
	}

	internal void ChildSortingOrderChanged(GObject child, int oldValue, int newValue)
	{
		if (newValue == 0)
		{
			_sortingChildCount--;
			SetChildIndex(child, _children.Count);
			return;
		}
		if (oldValue == 0)
		{
			_sortingChildCount++;
		}
		int num = _children.IndexOf(child);
		int insertPosForSortingChild = GetInsertPosForSortingChild(child);
		if (num < insertPosForSortingChild)
		{
			_SetChildIndex(child, num, insertPosForSortingChild - 1);
		}
		else
		{
			_SetChildIndex(child, num, insertPosForSortingChild);
		}
	}

	protected virtual void OnUpdate()
	{
		if (_boundsChanged)
		{
			UpdateBounds();
		}
	}

	public override void ConstructFromResource()
	{
		ConstructFromResource(null, 0);
	}

	internal void ConstructFromResource(List<GObject> objectPool, int poolIndex)
	{
		base.gameObjectName = base.packageItem.name;
		PackageItem branch = base.packageItem.getBranch();
		if (!branch.translated)
		{
			branch.translated = true;
			TranslationHelper.TranslateComponent(branch);
		}
		ByteBuffer rawData = branch.rawData;
		rawData.Seek(0, 0);
		underConstruct = true;
		sourceWidth = rawData.ReadInt();
		sourceHeight = rawData.ReadInt();
		initWidth = sourceWidth;
		initHeight = sourceHeight;
		SetSize(sourceWidth, sourceHeight);
		if (rawData.ReadBool())
		{
			minWidth = rawData.ReadInt();
			maxWidth = rawData.ReadInt();
			minHeight = rawData.ReadInt();
			maxHeight = rawData.ReadInt();
		}
		if (rawData.ReadBool())
		{
			float xv = rawData.ReadFloat();
			float yv = rawData.ReadFloat();
			SetPivot(xv, yv, rawData.ReadBool());
		}
		if (rawData.ReadBool())
		{
			_margin.top = rawData.ReadInt();
			_margin.bottom = rawData.ReadInt();
			_margin.left = rawData.ReadInt();
			_margin.right = rawData.ReadInt();
		}
		OverflowType overflowType = (OverflowType)rawData.ReadByte();
		if (overflowType == OverflowType.Scroll)
		{
			int num = rawData.position;
			rawData.Seek(0, 7);
			SetupScroll(rawData);
			rawData.position = num;
		}
		else
		{
			SetupOverflow(overflowType);
		}
		if (rawData.ReadBool())
		{
			int num2 = rawData.ReadInt();
			int num3 = rawData.ReadInt();
			clipSoftness = new Vector2(num2, num3);
		}
		_buildingDisplayList = true;
		rawData.Seek(0, 1);
		int num4 = rawData.ReadShort();
		for (int i = 0; i < num4; i++)
		{
			int num5 = rawData.ReadShort();
			num5 += rawData.position;
			Controller controller = new Controller();
			_controllers.Add(controller);
			controller.parent = this;
			controller.Setup(rawData);
			rawData.position = num5;
		}
		rawData.Seek(0, 2);
		int num6 = rawData.ReadShort();
		for (int j = 0; j < num6; j++)
		{
			int num7 = rawData.ReadShort();
			int num8 = rawData.position;
			GObject gObject;
			if (objectPool != null)
			{
				gObject = objectPool[poolIndex + j];
			}
			else
			{
				rawData.Seek(num8, 0);
				ObjectType type = (ObjectType)rawData.ReadByte();
				string text = rawData.ReadS();
				string text2 = rawData.ReadS();
				PackageItem packageItem = null;
				if (text != null)
				{
					packageItem = ((text2 == null) ? branch.owner : UIPackage.GetById(text2))?.GetItem(text);
				}
				if (packageItem != null)
				{
					gObject = UIObjectFactory.NewObject(packageItem);
					gObject.ConstructFromResource();
				}
				else
				{
					gObject = UIObjectFactory.NewObject(type);
				}
			}
			gObject.underConstruct = true;
			gObject.Setup_BeforeAdd(rawData, num8);
			gObject.InternalSetParent(this);
			_children.Add(gObject);
			rawData.position = num8 + num7;
		}
		rawData.Seek(0, 3);
		base.relations.Setup(rawData, parentToChild: true);
		rawData.Seek(0, 2);
		rawData.Skip(2);
		for (int k = 0; k < num6; k++)
		{
			int num9 = rawData.ReadShort();
			num9 += rawData.position;
			rawData.Seek(rawData.position, 3);
			_children[k].relations.Setup(rawData, parentToChild: false);
			rawData.position = num9;
		}
		rawData.Seek(0, 2);
		rawData.Skip(2);
		for (int l = 0; l < num6; l++)
		{
			int num10 = rawData.ReadShort();
			num10 += rawData.position;
			GObject gObject = _children[l];
			gObject.Setup_AfterAdd(rawData, rawData.position);
			gObject.underConstruct = false;
			if (gObject.displayObject != null)
			{
				gObject.displayObject.cachedTransform.SetParent(base.displayObject.cachedTransform, worldPositionStays: false);
			}
			rawData.position = num10;
		}
		rawData.Seek(0, 4);
		rawData.Skip(2);
		opaque = rawData.ReadBool();
		int num11 = rawData.ReadShort();
		if (num11 != -1)
		{
			container.mask = GetChildAt(num11).displayObject;
			if (rawData.ReadBool())
			{
				container.reversedMask = true;
			}
		}
		string text3 = rawData.ReadS();
		int num12 = rawData.ReadInt();
		int num13 = rawData.ReadInt();
		if (text3 != null)
		{
			PackageItem item = branch.owner.GetItem(text3);
			if (item != null && item.pixelHitTestData != null)
			{
				rootContainer.hitArea = new PixelHitTest(item.pixelHitTestData, num12, num13, sourceWidth, sourceHeight);
			}
		}
		else if (num12 != 0 && num13 != -1)
		{
			rootContainer.hitArea = new ShapeHitTest(GetChildAt(num13).displayObject);
		}
		if (rawData.version >= 5)
		{
			string str = rawData.ReadS();
			if (!string.IsNullOrEmpty(str))
			{
				base.onAddedToStage.Add((EventCallback0)delegate
				{
					__playSound(str, 1f);
				});
			}
			string str2 = rawData.ReadS();
			if (!string.IsNullOrEmpty(str2))
			{
				base.onRemovedFromStage.Add((EventCallback0)delegate
				{
					__playSound(str2, 1f);
				});
			}
		}
		rawData.Seek(0, 5);
		int num14 = rawData.ReadShort();
		for (int num15 = 0; num15 < num14; num15++)
		{
			int num16 = rawData.ReadShort();
			num16 += rawData.position;
			Transition transition = new Transition(this);
			transition.Setup(rawData);
			_transitions.Add(transition);
			rawData.position = num16;
		}
		if (_transitions.Count > 0)
		{
			base.onAddedToStage.Add(__addedToStage);
			base.onRemovedFromStage.Add(__removedFromStage);
		}
		ApplyAllControllers();
		_buildingDisplayList = false;
		underConstruct = false;
		BuildNativeDisplayList();
		SetBoundsChangedFlag();
		if (branch.objectType != ObjectType.Component)
		{
			ConstructExtension(rawData);
		}
		ConstructFromXML(null);
	}

	protected virtual void ConstructExtension(ByteBuffer buffer)
	{
	}

	public virtual void ConstructFromXML(XML xml)
	{
	}

	public override void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_AfterAdd(buffer, beginPos);
		buffer.Seek(beginPos, 4);
		int num = buffer.ReadShort();
		if (num != -1 && scrollPane != null && scrollPane.pageMode)
		{
			scrollPane.pageController = base.parent.GetControllerAt(num);
		}
		int num2 = buffer.ReadShort();
		for (int i = 0; i < num2; i++)
		{
			Controller controller = GetController(buffer.ReadS());
			string selectedPageId = buffer.ReadS();
			if (controller != null)
			{
				controller.selectedPageId = selectedPageId;
			}
		}
		if (buffer.version < 2)
		{
			return;
		}
		num2 = buffer.ReadShort();
		for (int j = 0; j < num2; j++)
		{
			string path = buffer.ReadS();
			int num3 = buffer.ReadShort();
			string text = buffer.ReadS();
			GObject childByPath = GetChildByPath(path);
			if (childByPath != null)
			{
				switch (num3)
				{
				case 0:
					childByPath.text = text;
					break;
				case 1:
					childByPath.icon = text;
					break;
				}
			}
		}
	}

	private void __playSound(string soundRes, float volumeScale)
	{
		int num = Convert.ToInt32(soundRes);
		if ((long)num > 0L)
		{
			Stage.inst.PlayOneShotSound(num);
		}
	}

	private void __addedToStage()
	{
		int count = _transitions.Count;
		for (int i = 0; i < count; i++)
		{
			_transitions[i].OnOwnerAddedToStage();
		}
	}

	private void __removedFromStage()
	{
		int count = _transitions.Count;
		for (int i = 0; i < count; i++)
		{
			_transitions[i].OnOwnerRemovedFromStage();
		}
	}
}
