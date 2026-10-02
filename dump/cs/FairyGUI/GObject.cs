using System;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GObject : EventDispatcher
{
	public string name;

	public object data;

	public int sourceWidth;

	public int sourceHeight;

	public int initWidth;

	public int initHeight;

	public int minWidth;

	public int maxWidth;

	public int minHeight;

	public int maxHeight;

	public Rect? dragBounds;

	public PackageItem packageItem;

	private float _x;

	private float _y;

	private float _z;

	private float _pivotX;

	private float _pivotY;

	private bool _pivotAsAnchor;

	private float _alpha;

	private float _rotation;

	private float _rotationX;

	private float _rotationY;

	private bool _visible;

	private bool _internalVisible;

	private bool _handlingController;

	private bool _touchable;

	private bool _grayed;

	private bool _draggable;

	private float _scaleX;

	private float _scaleY;

	private int _sortingOrder;

	private string _tooltips;

	private GGroup _group;

	private GearBase[] _gears;

	private EventListener _onClick;

	private EventListener _onRightClick;

	private EventListener _onTouchBegin;

	private EventListener _onTouchMove;

	private EventListener _onTouchEnd;

	private EventListener _onRollOver;

	private EventListener _onRollOut;

	private EventListener _onAddedToStage;

	private EventListener _onRemovedFromStage;

	private EventListener _onKeyDown;

	private EventListener _onClickLink;

	private EventListener _onPositionChanged;

	private EventListener _onSizeChanged;

	private EventListener _onDragStart;

	private EventListener _onDragMove;

	private EventListener _onDragEnd;

	private EventListener _onGearStop;

	private EventListener _onFocusIn;

	private EventListener _onFocusOut;

	protected internal bool underConstruct;

	internal float _width;

	internal float _height;

	internal float _rawWidth;

	internal float _rawHeight;

	internal bool _gearLocked;

	internal float _sizePercentInGroup;

	internal bool _disposed;

	internal GTreeNode _treeNode;

	internal static uint _gInstanceCounter;

	private Vector2 _dragTouchStartPos;

	private bool _dragTesting;

	private static Vector2 sGlobalDragStart;

	private static Rect sGlobalRect;

	private static bool sUpdateInDragging;

	public string id { get; private set; }

	public Relations relations { get; private set; }

	public GComponent parent { get; private set; }

	public DisplayObject displayObject { get; protected set; }

	public static GObject draggingObject { get; private set; }

	public EventListener onClick => _onClick ?? (_onClick = new EventListener(this, "onClick"));

	public EventListener onRightClick => _onRightClick ?? (_onRightClick = new EventListener(this, "onRightClick"));

	public EventListener onTouchBegin => _onTouchBegin ?? (_onTouchBegin = new EventListener(this, "onTouchBegin"));

	public EventListener onTouchMove => _onTouchMove ?? (_onTouchMove = new EventListener(this, "onTouchMove"));

	public EventListener onTouchEnd => _onTouchEnd ?? (_onTouchEnd = new EventListener(this, "onTouchEnd"));

	public EventListener onRollOver => _onRollOver ?? (_onRollOver = new EventListener(this, "onRollOver"));

	public EventListener onRollOut => _onRollOut ?? (_onRollOut = new EventListener(this, "onRollOut"));

	public EventListener onAddedToStage => _onAddedToStage ?? (_onAddedToStage = new EventListener(this, "onAddedToStage"));

	public EventListener onRemovedFromStage => _onRemovedFromStage ?? (_onRemovedFromStage = new EventListener(this, "onRemovedFromStage"));

	public EventListener onKeyDown => _onKeyDown ?? (_onKeyDown = new EventListener(this, "onKeyDown"));

	public EventListener onClickLink => _onClickLink ?? (_onClickLink = new EventListener(this, "onClickLink"));

	public EventListener onPositionChanged => _onPositionChanged ?? (_onPositionChanged = new EventListener(this, "onPositionChanged"));

	public EventListener onSizeChanged => _onSizeChanged ?? (_onSizeChanged = new EventListener(this, "onSizeChanged"));

	public EventListener onDragStart => _onDragStart ?? (_onDragStart = new EventListener(this, "onDragStart"));

	public EventListener onDragMove => _onDragMove ?? (_onDragMove = new EventListener(this, "onDragMove"));

	public EventListener onDragEnd => _onDragEnd ?? (_onDragEnd = new EventListener(this, "onDragEnd"));

	public EventListener onGearStop => _onGearStop ?? (_onGearStop = new EventListener(this, "onGearStop"));

	public EventListener onFocusIn => _onFocusIn ?? (_onFocusIn = new EventListener(this, "onFocusIn"));

	public EventListener onFocusOut => _onFocusOut ?? (_onFocusOut = new EventListener(this, "onFocusOut"));

	public float x
	{
		get
		{
			return _x;
		}
		set
		{
			SetPosition(value, _y, _z);
		}
	}

	public float y
	{
		get
		{
			return _y;
		}
		set
		{
			SetPosition(_x, value, _z);
		}
	}

	public float z
	{
		get
		{
			return _z;
		}
		set
		{
			SetPosition(_x, _y, value);
		}
	}

	public Vector2 xy
	{
		get
		{
			return new Vector2(_x, _y);
		}
		set
		{
			SetPosition(value.x, value.y, _z);
		}
	}

	public Vector3 position
	{
		get
		{
			return new Vector3(_x, _y, _z);
		}
		set
		{
			SetPosition(value.x, value.y, value.z);
		}
	}

	[Obsolete("Use UIConfig.makePixelPerfect or DisplayObject.pixelPerfect")]
	public bool pixelSnapping
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public float width
	{
		get
		{
			return _width;
		}
		set
		{
			SetSize(value, _rawHeight);
		}
	}

	public float height
	{
		get
		{
			return _height;
		}
		set
		{
			SetSize(_rawWidth, value);
		}
	}

	public Vector2 size
	{
		get
		{
			return new Vector2(width, height);
		}
		set
		{
			SetSize(value.x, value.y);
		}
	}

	public float actualWidth => width * _scaleX;

	public float actualHeight => height * _scaleY;

	public float xMin
	{
		get
		{
			if (!_pivotAsAnchor)
			{
				return _x;
			}
			return _x - _width * _pivotX;
		}
		set
		{
			if (_pivotAsAnchor)
			{
				SetPosition(value + _width * _pivotX, _y, _z);
			}
			else
			{
				SetPosition(value, _y, _z);
			}
		}
	}

	public float yMin
	{
		get
		{
			if (!_pivotAsAnchor)
			{
				return _y;
			}
			return _y - _height * _pivotY;
		}
		set
		{
			if (_pivotAsAnchor)
			{
				SetPosition(_x, value + _height * _pivotY, _z);
			}
			else
			{
				SetPosition(_x, value, _z);
			}
		}
	}

	public float scaleX
	{
		get
		{
			return _scaleX;
		}
		set
		{
			SetScale(value, _scaleY);
		}
	}

	public float scaleY
	{
		get
		{
			return _scaleY;
		}
		set
		{
			SetScale(_scaleX, value);
		}
	}

	public Vector2 scale
	{
		get
		{
			return new Vector2(_scaleX, _scaleY);
		}
		set
		{
			SetScale(value.x, value.y);
		}
	}

	public Vector2 skew
	{
		get
		{
			if (displayObject != null)
			{
				return displayObject.skew;
			}
			return Vector2.zero;
		}
		set
		{
			if (displayObject != null)
			{
				displayObject.skew = value;
			}
		}
	}

	public float pivotX
	{
		get
		{
			return _pivotX;
		}
		set
		{
			SetPivot(value, _pivotY, _pivotAsAnchor);
		}
	}

	public float pivotY
	{
		get
		{
			return _pivotY;
		}
		set
		{
			SetPivot(_pivotX, value, _pivotAsAnchor);
		}
	}

	public Vector2 pivot
	{
		get
		{
			return new Vector2(_pivotX, _pivotY);
		}
		set
		{
			SetPivot(value.x, value.y, _pivotAsAnchor);
		}
	}

	public bool pivotAsAnchor
	{
		get
		{
			return _pivotAsAnchor;
		}
		set
		{
			SetPivot(_pivotX, _pivotY, value);
		}
	}

	public bool touchable
	{
		get
		{
			return _touchable;
		}
		set
		{
			if (_touchable != value)
			{
				_touchable = value;
				UpdateGear(3);
				if (displayObject != null)
				{
					displayObject.touchable = _touchable;
				}
			}
		}
	}

	public bool grayed
	{
		get
		{
			return _grayed;
		}
		set
		{
			if (_grayed != value)
			{
				_grayed = value;
				HandleGrayedChanged();
				UpdateGear(3);
			}
		}
	}

	public bool enabled
	{
		get
		{
			if (!_grayed)
			{
				return _touchable;
			}
			return false;
		}
		set
		{
			grayed = !value;
			touchable = value;
		}
	}

	public float rotation
	{
		get
		{
			return _rotation;
		}
		set
		{
			_rotation = value;
			if (displayObject != null)
			{
				displayObject.rotation = _rotation;
			}
			UpdateGear(3);
		}
	}

	public float rotationX
	{
		get
		{
			return _rotationX;
		}
		set
		{
			_rotationX = value;
			if (displayObject != null)
			{
				displayObject.rotationX = _rotationX;
			}
		}
	}

	public float rotationY
	{
		get
		{
			return _rotationY;
		}
		set
		{
			_rotationY = value;
			if (displayObject != null)
			{
				displayObject.rotationY = _rotationY;
			}
		}
	}

	public float alpha
	{
		get
		{
			return _alpha;
		}
		set
		{
			_alpha = value;
			HandleAlphaChanged();
			UpdateGear(3);
		}
	}

	public bool visible
	{
		get
		{
			return _visible;
		}
		set
		{
			if (_visible != value)
			{
				_visible = value;
				HandleVisibleChanged();
				if (parent != null)
				{
					parent.SetBoundsChangedFlag();
				}
				if (_group != null && _group.excludeInvisibles)
				{
					_group.SetBoundsChangedFlag();
				}
			}
		}
	}

	internal bool internalVisible
	{
		get
		{
			if (_internalVisible)
			{
				if (group != null)
				{
					return group.internalVisible;
				}
				return true;
			}
			return false;
		}
	}

	internal bool internalVisible2
	{
		get
		{
			if (_visible)
			{
				if (group != null)
				{
					return group.internalVisible2;
				}
				return true;
			}
			return false;
		}
	}

	internal bool internalVisible3
	{
		get
		{
			if (_visible)
			{
				return _internalVisible;
			}
			return false;
		}
	}

	public int sortingOrder
	{
		get
		{
			return _sortingOrder;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (_sortingOrder != value)
			{
				int oldValue = _sortingOrder;
				_sortingOrder = value;
				if (parent != null)
				{
					parent.ChildSortingOrderChanged(this, oldValue, _sortingOrder);
				}
			}
		}
	}

	public bool focusable
	{
		get
		{
			if (displayObject != null)
			{
				return displayObject.focusable;
			}
			return false;
		}
		set
		{
			if (displayObject != null)
			{
				displayObject.focusable = value;
			}
		}
	}

	public bool tabStop
	{
		get
		{
			if (displayObject != null)
			{
				return displayObject.tabStop;
			}
			return false;
		}
		set
		{
			if (displayObject != null)
			{
				displayObject.tabStop = value;
			}
		}
	}

	public bool focused
	{
		get
		{
			if (displayObject != null)
			{
				return displayObject.focused;
			}
			return false;
		}
	}

	public string tooltips
	{
		get
		{
			return _tooltips;
		}
		set
		{
			if (!string.IsNullOrEmpty(_tooltips))
			{
				onRollOver.Remove(__rollOver);
				onRollOut.Remove(__rollOut);
			}
			_tooltips = value;
			if (!string.IsNullOrEmpty(_tooltips))
			{
				onRollOver.Add(__rollOver);
				onRollOut.Add(__rollOut);
			}
		}
	}

	public string cursor
	{
		get
		{
			if (displayObject == null)
			{
				return null;
			}
			return displayObject.cursor;
		}
		set
		{
			if (displayObject != null)
			{
				displayObject.cursor = value;
			}
		}
	}

	public virtual IFilter filter
	{
		get
		{
			if (displayObject == null)
			{
				return null;
			}
			return displayObject.filter;
		}
		set
		{
			if (displayObject != null)
			{
				displayObject.filter = value;
			}
		}
	}

	public virtual BlendMode blendMode
	{
		get
		{
			if (displayObject == null)
			{
				return BlendMode.None;
			}
			return displayObject.blendMode;
		}
		set
		{
			if (displayObject != null)
			{
				displayObject.blendMode = value;
			}
		}
	}

	public string gameObjectName
	{
		get
		{
			if (displayObject != null)
			{
				return displayObject.gameObject.name;
			}
			return null;
		}
		set
		{
			if (displayObject != null)
			{
				displayObject.gameObject.name = value;
			}
		}
	}

	public bool inContainer
	{
		get
		{
			if (displayObject != null)
			{
				return displayObject.parent != null;
			}
			return false;
		}
	}

	public bool onStage
	{
		get
		{
			if (displayObject != null)
			{
				return displayObject.stage != null;
			}
			return false;
		}
	}

	public string resourceURL
	{
		get
		{
			if (packageItem != null)
			{
				return "ui://" + packageItem.owner.id + packageItem.id;
			}
			return null;
		}
	}

	public GearXY gearXY => (GearXY)GetGear(1);

	public GearSize gearSize => (GearSize)GetGear(2);

	public GearLook gearLook => (GearLook)GetGear(3);

	public GGroup group
	{
		get
		{
			return _group;
		}
		set
		{
			if (_group != value)
			{
				if (_group != null)
				{
					_group.SetBoundsChangedFlag();
				}
				_group = value;
				if (_group != null)
				{
					_group.SetBoundsChangedFlag();
				}
				HandleVisibleChanged();
				if (parent != null)
				{
					parent.ChildStateChanged(this);
				}
			}
		}
	}

	public GRoot root
	{
		get
		{
			GObject gObject = this;
			while (gObject.parent != null)
			{
				gObject = gObject.parent;
			}
			if (gObject is GRoot)
			{
				return (GRoot)gObject;
			}
			if (gObject.displayObject != null && gObject.displayObject.parent != null)
			{
				DisplayObject child = gObject.displayObject.parent.GetChild("GRoot");
				if (child != null && child.gOwner is GRoot)
				{
					return (GRoot)child.gOwner;
				}
			}
			return GRoot.inst;
		}
	}

	public virtual string text
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public virtual string icon
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool draggable
	{
		get
		{
			return _draggable;
		}
		set
		{
			if (_draggable != value)
			{
				_draggable = value;
				InitDrag();
			}
		}
	}

	public bool dragging => draggingObject == this;

	public bool isDisposed => _disposed;

	public GImage asImage => this as GImage;

	public GComponent asCom => this as GComponent;

	public GButton asButton => this as GButton;

	public GLabel asLabel => this as GLabel;

	public GProgressBar asProgress => this as GProgressBar;

	public GSlider asSlider => this as GSlider;

	public GComboBox asComboBox => this as GComboBox;

	public GTextField asTextField => this as GTextField;

	public GRichTextField asRichTextField => this as GRichTextField;

	public GTextInput asTextInput => this as GTextInput;

	public GLoader asLoader => this as GLoader;

	public GLoader3D asLoader3D => this as GLoader3D;

	public GList asList => this as GList;

	public GGraph asGraph => this as GGraph;

	public GGroup asGroup => this as GGroup;

	public GMovieClip asMovieClip => this as GMovieClip;

	public GTree asTree => this as GTree;

	public GTreeNode treeNode => _treeNode;

	public GObject()
	{
		_width = 0f;
		_height = 0f;
		_alpha = 1f;
		_visible = true;
		_touchable = true;
		_scaleX = 1f;
		_scaleY = 1f;
		_internalVisible = true;
		id = "_n" + _gInstanceCounter++;
		name = string.Empty;
		CreateDisplayObject();
		relations = new Relations(this);
		_gears = new GearBase[10];
	}

	public void SetXY(float xv, float yv)
	{
		SetPosition(xv, yv, _z);
	}

	public void SetXY(float xv, float yv, bool topLeftValue)
	{
		if (_pivotAsAnchor)
		{
			SetPosition(xv + _pivotX * _width, yv + _pivotY * _height, _z);
		}
		else
		{
			SetPosition(xv, yv, _z);
		}
	}

	public void SetPosition(float xv, float yv, float zv)
	{
		if (_x == xv && _y == yv && _z == zv)
		{
			return;
		}
		float dx = xv - _x;
		float dy = yv - _y;
		_x = xv;
		_y = yv;
		_z = zv;
		HandlePositionChanged();
		if (this is GGroup)
		{
			((GGroup)this).MoveChildren(dx, dy);
		}
		UpdateGear(1);
		if (parent != null && !(parent is GList))
		{
			parent.SetBoundsChangedFlag();
			if (_group != null)
			{
				_group.SetBoundsChangedFlag(positionChangedOnly: true);
			}
			DispatchEvent("onPositionChanged", null);
		}
		if (draggingObject == this && !sUpdateInDragging)
		{
			sGlobalRect = LocalToGlobal(new Rect(0f, 0f, width, height));
		}
	}

	public void Center()
	{
		Center(restraint: false);
	}

	public void Center(bool restraint)
	{
		GComponent gComponent = ((parent == null) ? root : parent);
		SetXY((int)((gComponent.width - width) / 2f), (int)((gComponent.height - height) / 2f), topLeftValue: true);
		if (restraint)
		{
			AddRelation(gComponent, RelationType.Center_Center);
			AddRelation(gComponent, RelationType.Middle_Middle);
		}
	}

	public void MakeFullScreen()
	{
		SetSize(GRoot.inst.width, GRoot.inst.height);
	}

	public void SetSize(float wv, float hv)
	{
		SetSize(wv, hv, ignorePivot: false);
	}

	public void SetSize(float wv, float hv, bool ignorePivot)
	{
		if (_rawWidth == wv && _rawHeight == hv)
		{
			return;
		}
		_rawWidth = wv;
		_rawHeight = hv;
		if (wv < (float)minWidth)
		{
			wv = minWidth;
		}
		else if (maxWidth > 0 && wv > (float)maxWidth)
		{
			wv = maxWidth;
		}
		if (hv < (float)minHeight)
		{
			hv = minHeight;
		}
		else if (maxHeight > 0 && hv > (float)maxHeight)
		{
			hv = maxHeight;
		}
		float num = wv - _width;
		float num2 = hv - _height;
		_width = wv;
		_height = hv;
		HandleSizeChanged();
		if (_pivotX != 0f || _pivotY != 0f)
		{
			if (!_pivotAsAnchor)
			{
				if (!ignorePivot)
				{
					SetXY(_x - _pivotX * num, _y - _pivotY * num2);
				}
				else
				{
					HandlePositionChanged();
				}
			}
			else
			{
				HandlePositionChanged();
			}
		}
		if (this is GGroup)
		{
			((GGroup)this).ResizeChildren(num, num2);
		}
		UpdateGear(2);
		if (parent != null)
		{
			relations.OnOwnerSizeChanged(num, num2, _pivotAsAnchor || !ignorePivot);
			parent.SetBoundsChangedFlag();
			if (_group != null)
			{
				_group.SetBoundsChangedFlag();
			}
		}
		DispatchEvent("onSizeChanged", null);
	}

	protected void SetSizeDirectly(float wv, float hv)
	{
		_rawWidth = wv;
		_rawHeight = hv;
		if (wv < 0f)
		{
			wv = 0f;
		}
		if (hv < 0f)
		{
			hv = 0f;
		}
		_width = wv;
		_height = hv;
	}

	public void SetScale(float wv, float hv)
	{
		if (_scaleX != wv || _scaleY != hv)
		{
			_scaleX = wv;
			_scaleY = hv;
			HandleScaleChanged();
			UpdateGear(2);
		}
	}

	public void SetPivot(float xv, float yv)
	{
		SetPivot(xv, yv, asAnchor: false);
	}

	public void SetPivot(float xv, float yv, bool asAnchor)
	{
		if (_pivotX != xv || _pivotY != yv || _pivotAsAnchor != asAnchor)
		{
			_pivotX = xv;
			_pivotY = yv;
			_pivotAsAnchor = asAnchor;
			if (displayObject != null)
			{
				displayObject.pivot = new Vector2(_pivotX, _pivotY);
			}
			HandlePositionChanged();
		}
	}

	public void RequestFocus()
	{
		if (displayObject != null)
		{
			Stage.inst.SetFocus(displayObject);
		}
	}

	public void RequestFocus(bool byKey)
	{
		if (displayObject != null)
		{
			Stage.inst.SetFocus(displayObject, byKey);
		}
	}

	private void __rollOver()
	{
		root.ShowTooltips(tooltips);
	}

	private void __rollOut()
	{
		root.HideTooltips();
	}

	public void SetHome(GObject obj)
	{
		if (obj != null && displayObject != null && obj.displayObject != null)
		{
			displayObject.home = obj.displayObject.cachedTransform;
		}
	}

	public GearBase GetGear(int index)
	{
		GearBase gearBase = _gears[index];
		if (gearBase == null)
		{
			gearBase = index switch
			{
				0 => new GearDisplay(this), 
				1 => new GearXY(this), 
				2 => new GearSize(this), 
				3 => new GearLook(this), 
				4 => new GearColor(this), 
				5 => new GearAnimation(this), 
				6 => new GearText(this), 
				7 => new GearIcon(this), 
				8 => new GearDisplay2(this), 
				9 => new GearFontSize(this), 
				_ => throw new Exception("FairyGUI: invalid gear index!"), 
			};
			_gears[index] = gearBase;
		}
		return gearBase;
	}

	protected void UpdateGear(int index)
	{
		if (!underConstruct && !_gearLocked)
		{
			GearBase gearBase = _gears[index];
			if (gearBase != null && gearBase.controller != null)
			{
				gearBase.UpdateState();
			}
		}
	}

	internal bool CheckGearController(int index, Controller c)
	{
		if (_gears[index] != null)
		{
			return _gears[index].controller == c;
		}
		return false;
	}

	internal void UpdateGearFromRelations(int index, float dx, float dy)
	{
		if (_gears[index] != null)
		{
			_gears[index].UpdateFromRelations(dx, dy);
		}
	}

	internal uint AddDisplayLock()
	{
		GearDisplay gearDisplay = (GearDisplay)_gears[0];
		if (gearDisplay != null && gearDisplay.controller != null)
		{
			uint result = gearDisplay.AddLock();
			CheckGearDisplay();
			return result;
		}
		return 0u;
	}

	internal void ReleaseDisplayLock(uint token)
	{
		GearDisplay gearDisplay = (GearDisplay)_gears[0];
		if (gearDisplay != null && gearDisplay.controller != null)
		{
			gearDisplay.ReleaseLock(token);
			CheckGearDisplay();
		}
	}

	private void CheckGearDisplay()
	{
		if (_handlingController)
		{
			return;
		}
		bool flag = _gears[0] == null || ((GearDisplay)_gears[0]).connected;
		if (_gears[8] != null)
		{
			flag = ((GearDisplay2)_gears[8]).Evaluate(flag);
		}
		if (flag != _internalVisible)
		{
			_internalVisible = flag;
			if (parent != null)
			{
				parent.ChildStateChanged(this);
			}
			if (_group != null && _group.excludeInvisibles)
			{
				_group.SetBoundsChangedFlag();
			}
		}
	}

	public void InvalidateBatchingState()
	{
		if (displayObject != null)
		{
			displayObject.InvalidateBatchingState();
		}
		else if (this is GGroup && parent != null)
		{
			parent.container.InvalidateBatchingState(childrenChanged: true);
		}
	}

	public virtual void HandleControllerChanged(Controller c)
	{
		_handlingController = true;
		for (int i = 0; i < 10; i++)
		{
			GearBase gearBase = _gears[i];
			if (gearBase != null && gearBase.controller == c)
			{
				gearBase.Apply();
			}
		}
		_handlingController = false;
		CheckGearDisplay();
	}

	public void AddRelation(GObject target, RelationType relationType)
	{
		AddRelation(target, relationType, usePercent: false);
	}

	public void AddRelation(GObject target, RelationType relationType, bool usePercent)
	{
		relations.Add(target, relationType, usePercent);
	}

	public void RemoveRelation(GObject target, RelationType relationType)
	{
		relations.Remove(target, relationType);
	}

	public void RemoveFromParent()
	{
		if (parent != null)
		{
			parent.RemoveChild(this);
		}
	}

	public void StartDrag()
	{
		StartDrag(-1);
	}

	public void StartDrag(int touchId)
	{
		if (displayObject.stage != null)
		{
			DragBegin(touchId);
		}
	}

	public void StopDrag()
	{
		DragEnd();
	}

	public Vector2 LocalToGlobal(Vector2 pt)
	{
		if (_pivotAsAnchor)
		{
			pt.x += _width * _pivotX;
			pt.y += _height * _pivotY;
		}
		return displayObject.LocalToGlobal(pt);
	}

	public Vector2 GlobalToLocal(Vector2 pt)
	{
		pt = displayObject.GlobalToLocal(pt);
		if (_pivotAsAnchor)
		{
			pt.x -= _width * _pivotX;
			pt.y -= _height * _pivotY;
		}
		return pt;
	}

	public Rect LocalToGlobal(Rect rect)
	{
		Rect result = default(Rect);
		Vector2 vector = LocalToGlobal(new Vector2(rect.xMin, rect.yMin));
		result.xMin = vector.x;
		result.yMin = vector.y;
		vector = LocalToGlobal(new Vector2(rect.xMax, rect.yMax));
		result.xMax = vector.x;
		result.yMax = vector.y;
		return result;
	}

	public Rect GlobalToLocal(Rect rect)
	{
		Rect result = default(Rect);
		Vector2 vector = GlobalToLocal(new Vector2(rect.xMin, rect.yMin));
		result.xMin = vector.x;
		result.yMin = vector.y;
		vector = GlobalToLocal(new Vector2(rect.xMax, rect.yMax));
		result.xMax = vector.x;
		result.yMax = vector.y;
		return result;
	}

	public Vector2 LocalToRoot(Vector2 pt, GRoot r)
	{
		pt = LocalToGlobal(pt);
		if (r == null || r == GRoot.inst)
		{
			pt.x /= UIContentScaler.scaleFactor;
			pt.y /= UIContentScaler.scaleFactor;
			return pt;
		}
		return r.GlobalToLocal(pt);
	}

	public Vector2 RootToLocal(Vector2 pt, GRoot r)
	{
		if (r == null || r == GRoot.inst)
		{
			pt.x *= UIContentScaler.scaleFactor;
			pt.y *= UIContentScaler.scaleFactor;
		}
		else
		{
			pt = r.LocalToGlobal(pt);
		}
		return GlobalToLocal(pt);
	}

	public Vector2 WorldToLocal(Vector3 pt)
	{
		return WorldToLocal(pt, HitTestContext.cachedMainCamera);
	}

	public Vector2 WorldToLocal(Vector3 pt, Camera camera)
	{
		Vector3 vector = camera.WorldToScreenPoint(pt);
		vector.y = (float)Screen.height - vector.y;
		vector.z = 0f;
		return GlobalToLocal(vector);
	}

	public Vector2 TransformPoint(Vector2 pt, GObject targetSpace)
	{
		if (_pivotAsAnchor)
		{
			pt.x += _width * _pivotX;
			pt.y += _height * _pivotY;
		}
		return displayObject.TransformPoint(pt, (targetSpace != null) ? targetSpace.displayObject : Stage.inst);
	}

	public Rect TransformRect(Rect rect, GObject targetSpace)
	{
		if (_pivotAsAnchor)
		{
			rect.x += _width * _pivotX;
			rect.y += _height * _pivotY;
		}
		return displayObject.TransformRect(rect, (targetSpace != null) ? targetSpace.displayObject : Stage.inst);
	}

	public virtual void Dispose()
	{
		if (!_disposed)
		{
			_disposed = true;
			RemoveFromParent();
			RemoveEventListeners();
			relations.Dispose();
			relations = null;
			for (int i = 0; i < 10; i++)
			{
				_gears[i]?.Dispose();
			}
			if (displayObject != null)
			{
				displayObject.gOwner = null;
				displayObject.Dispose();
			}
			data = null;
		}
	}

	protected virtual void CreateDisplayObject()
	{
	}

	internal void InternalSetParent(GComponent value)
	{
		parent = value;
	}

	protected virtual void HandlePositionChanged()
	{
		if (displayObject != null)
		{
			float num = _x;
			float num2 = _y;
			if (!_pivotAsAnchor)
			{
				num += _width * _pivotX;
				num2 += _height * _pivotY;
			}
			displayObject.location = new Vector3(num, num2, _z);
		}
	}

	protected virtual void HandleSizeChanged()
	{
		if (displayObject != null)
		{
			displayObject.SetSize(_width, _height);
		}
	}

	protected virtual void HandleScaleChanged()
	{
		if (displayObject != null)
		{
			displayObject.SetScale(_scaleX, _scaleY);
		}
	}

	protected virtual void HandleGrayedChanged()
	{
		if (displayObject != null)
		{
			displayObject.grayed = _grayed;
		}
	}

	protected virtual void HandleAlphaChanged()
	{
		if (displayObject != null)
		{
			displayObject.alpha = _alpha;
		}
	}

	protected internal virtual void HandleVisibleChanged()
	{
		if (displayObject != null)
		{
			displayObject.visible = internalVisible2;
		}
	}

	public virtual void ConstructFromResource()
	{
	}

	public virtual void Setup_BeforeAdd(ByteBuffer buffer, int beginPos)
	{
		buffer.Seek(beginPos, 0);
		buffer.Skip(5);
		id = buffer.ReadS();
		name = buffer.ReadS();
		float xv = buffer.ReadInt();
		float yv = buffer.ReadInt();
		SetXY(xv, yv);
		if (buffer.ReadBool())
		{
			initWidth = buffer.ReadInt();
			initHeight = buffer.ReadInt();
			SetSize(initWidth, initHeight, ignorePivot: true);
		}
		if (buffer.ReadBool())
		{
			minWidth = buffer.ReadInt();
			maxWidth = buffer.ReadInt();
			minHeight = buffer.ReadInt();
			maxHeight = buffer.ReadInt();
		}
		if (buffer.ReadBool())
		{
			xv = buffer.ReadFloat();
			yv = buffer.ReadFloat();
			SetScale(xv, yv);
		}
		if (buffer.ReadBool())
		{
			xv = buffer.ReadFloat();
			yv = buffer.ReadFloat();
			skew = new Vector2(xv, yv);
		}
		if (buffer.ReadBool())
		{
			xv = buffer.ReadFloat();
			yv = buffer.ReadFloat();
			SetPivot(xv, yv, buffer.ReadBool());
		}
		xv = buffer.ReadFloat();
		if (xv != 1f)
		{
			alpha = xv;
		}
		xv = buffer.ReadFloat();
		if (xv != 0f)
		{
			rotation = xv;
		}
		if (!buffer.ReadBool())
		{
			visible = false;
		}
		if (!buffer.ReadBool())
		{
			touchable = false;
		}
		if (buffer.ReadBool())
		{
			grayed = true;
		}
		blendMode = (BlendMode)buffer.ReadByte();
		if (buffer.ReadByte() == 1)
		{
			ColorFilter colorFilter = (ColorFilter)(this.filter = new ColorFilter());
			colorFilter.AdjustBrightness(buffer.ReadFloat());
			colorFilter.AdjustContrast(buffer.ReadFloat());
			colorFilter.AdjustSaturation(buffer.ReadFloat());
			colorFilter.AdjustHue(buffer.ReadFloat());
		}
		string text = buffer.ReadS();
		if (text != null)
		{
			data = text;
		}
	}

	public virtual void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		buffer.Seek(beginPos, 1);
		string text = buffer.ReadS();
		if (text != null)
		{
			tooltips = text;
		}
		int num = buffer.ReadShort();
		if (num >= 0)
		{
			group = parent.GetChildAt(num) as GGroup;
		}
		buffer.Seek(beginPos, 2);
		int num2 = buffer.ReadShort();
		for (int i = 0; i < num2; i++)
		{
			int num3 = buffer.ReadShort();
			num3 += buffer.position;
			GetGear(buffer.ReadByte()).Setup(buffer);
			buffer.position = num3;
		}
	}

	private void InitDrag()
	{
		if (_draggable)
		{
			onTouchBegin.Add(__touchBegin);
			onTouchMove.Add(__touchMove);
			onTouchEnd.Add(__touchEnd);
		}
		else
		{
			onTouchBegin.Remove(__touchBegin);
			onTouchMove.Remove(__touchMove);
			onTouchEnd.Remove(__touchEnd);
		}
	}

	private void DragBegin(int touchId)
	{
		if (!DispatchEvent("onDragStart", touchId))
		{
			if (draggingObject != null)
			{
				GObject gObject = draggingObject;
				draggingObject.StopDrag();
				draggingObject = null;
				gObject.DispatchEvent("onDragEnd", null);
			}
			onTouchMove.Add(__touchMove);
			onTouchEnd.Add(__touchEnd);
			sGlobalDragStart = Stage.inst.GetTouchPosition(touchId);
			sGlobalRect = LocalToGlobal(new Rect(0f, 0f, width, height));
			_dragTesting = false;
			draggingObject = this;
			Stage.inst.AddTouchMonitor(touchId, this);
		}
	}

	private void DragEnd()
	{
		if (draggingObject == this)
		{
			_dragTesting = false;
			draggingObject = null;
		}
	}

	private void __touchBegin(EventContext context)
	{
		if (Stage.inst.focus is InputTextField && ((InputTextField)Stage.inst.focus).editable)
		{
			_dragTesting = false;
			return;
		}
		InputEvent inputEvent = context.inputEvent;
		_dragTouchStartPos = inputEvent.position;
		_dragTesting = true;
		context.CaptureTouch();
	}

	private void __touchMove(EventContext context)
	{
		InputEvent inputEvent = context.inputEvent;
		if (_dragTesting && draggingObject != this)
		{
			int num = ((!Stage.touchScreen) ? UIConfig.clickDragSensitivity : UIConfig.touchDragSensitivity);
			if (Mathf.Abs(_dragTouchStartPos.x - inputEvent.x) < (float)num && Mathf.Abs(_dragTouchStartPos.y - inputEvent.y) < (float)num)
			{
				return;
			}
			_dragTesting = false;
			DragBegin(inputEvent.touchId);
		}
		if (draggingObject != this)
		{
			return;
		}
		float num2 = inputEvent.x - sGlobalDragStart.x + sGlobalRect.x;
		float num3 = inputEvent.y - sGlobalDragStart.y + sGlobalRect.y;
		if (dragBounds.HasValue)
		{
			Rect rect = GRoot.inst.LocalToGlobal(dragBounds.Value);
			if (num2 < rect.x)
			{
				num2 = rect.x;
			}
			else if (num2 + sGlobalRect.width > rect.xMax)
			{
				num2 = rect.xMax - sGlobalRect.width;
				if (num2 < rect.x)
				{
					num2 = rect.x;
				}
			}
			if (num3 < rect.y)
			{
				num3 = rect.y;
			}
			else if (num3 + sGlobalRect.height > rect.yMax)
			{
				num3 = rect.yMax - sGlobalRect.height;
				if (num3 < rect.y)
				{
					num3 = rect.y;
				}
			}
		}
		Vector2 vector = parent.GlobalToLocal(new Vector2(num2, num3));
		if (!float.IsNaN(vector.x))
		{
			sUpdateInDragging = true;
			SetXY(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
			sUpdateInDragging = false;
			DispatchEvent("onDragMove", null);
		}
	}

	private void __touchEnd(EventContext context)
	{
		if (draggingObject == this)
		{
			draggingObject = null;
			DispatchEvent("onDragEnd", null);
		}
	}

	public GTweener TweenMove(Vector2 endValue, float duration)
	{
		return GTween.To(xy, endValue, duration).SetTarget(this, TweenPropType.XY);
	}

	public GTweener TweenMoveX(float endValue, float duration)
	{
		return GTween.To(_x, endValue, duration).SetTarget(this, TweenPropType.X);
	}

	public GTweener TweenMoveY(float endValue, float duration)
	{
		return GTween.To(_y, endValue, duration).SetTarget(this, TweenPropType.Y);
	}

	public GTweener TweenScale(Vector2 endValue, float duration)
	{
		return GTween.To(scale, endValue, duration).SetTarget(this, TweenPropType.Scale);
	}

	public GTweener TweenScaleX(float endValue, float duration)
	{
		return GTween.To(_scaleX, endValue, duration).SetTarget(this, TweenPropType.ScaleX);
	}

	public GTweener TweenScaleY(float endValue, float duration)
	{
		return GTween.To(_scaleY, endValue, duration).SetTarget(this, TweenPropType.ScaleY);
	}

	public GTweener TweenResize(Vector2 endValue, float duration)
	{
		return GTween.To(size, endValue, duration).SetTarget(this, TweenPropType.Size);
	}

	public GTweener TweenFade(float endValue, float duration)
	{
		return GTween.To(_alpha, endValue, duration).SetTarget(this, TweenPropType.Alpha);
	}

	public GTweener TweenRotate(float endValue, float duration)
	{
		return GTween.To(_rotation, endValue, duration).SetTarget(this, TweenPropType.Rotation);
	}
}
