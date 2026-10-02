using System;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class ScrollPane : EventDispatcher
{
	private ScrollType _scrollType;

	private float _scrollStep;

	private float _decelerationRate;

	private Margin _scrollBarMargin;

	private bool _bouncebackEffect;

	private bool _touchEffect;

	private bool _scrollBarDisplayAuto;

	private bool _vScrollNone;

	private bool _hScrollNone;

	private bool _needRefresh;

	private int _refreshBarAxis;

	private bool _displayOnLeft;

	private bool _snapToItem;

	internal bool _displayInDemand;

	private bool _mouseWheelEnabled;

	private bool _softnessOnTopOrLeftSide;

	private bool _pageMode;

	private Vector2 _pageSize;

	private bool _inertiaDisabled;

	private bool _maskDisabled;

	private bool _floating;

	private bool _dontClipMargin;

	private float _xPos;

	private float _yPos;

	private Vector2 _viewSize;

	private Vector2 _contentSize;

	private Vector2 _overlapSize;

	private Vector2 _containerPos;

	private Vector2 _beginTouchPos;

	private Vector2 _lastTouchPos;

	private Vector2 _lastTouchGlobalPos;

	private Vector2 _velocity;

	private float _velocityScale;

	private float _lastMoveTime;

	private bool _dragged;

	private bool _isHoldAreaDone;

	private int _aniFlag;

	internal int _loop;

	private int _headerLockedSize;

	private int _footerLockedSize;

	private bool _hover;

	private int _tweening;

	private Vector2 _tweenStart;

	private Vector2 _tweenChange;

	private Vector2 _tweenTime;

	private Vector2 _tweenDuration;

	private Action _refreshDelegate;

	private TimerCallback _tweenUpdateDelegate;

	private GTweenCallback1 _hideScrollBarDelegate;

	private GComponent _owner;

	private Container _maskContainer;

	private Container _container;

	private GScrollBar _hzScrollBar;

	private GScrollBar _vtScrollBar;

	private GComponent _header;

	private GComponent _footer;

	private Controller _pageController;

	private EventListener _onScroll;

	private EventListener _onScrollEnd;

	private EventListener _onPullDownRelease;

	private EventListener _onPullUpRelease;

	private static int _gestureFlag;

	public static float TWEEN_TIME_GO = 0.3f;

	public static float TWEEN_TIME_DEFAULT = 0.3f;

	public static float PULL_RATIO = 0.5f;

	private bool _MouseWheelTrigger;

	public static ScrollPane draggingPane { get; private set; }

	public EventListener onScroll => _onScroll ?? (_onScroll = new EventListener(this, "onScroll"));

	public EventListener onScrollEnd => _onScrollEnd ?? (_onScrollEnd = new EventListener(this, "onScrollEnd"));

	public EventListener onPullDownRelease => _onPullDownRelease ?? (_onPullDownRelease = new EventListener(this, "onPullDownRelease"));

	public EventListener onPullUpRelease => _onPullUpRelease ?? (_onPullUpRelease = new EventListener(this, "onPullUpRelease"));

	public GComponent owner => _owner;

	public GScrollBar hzScrollBar => _hzScrollBar;

	public GScrollBar vtScrollBar => _vtScrollBar;

	public GComponent header => _header;

	public GComponent footer => _footer;

	public bool bouncebackEffect
	{
		get
		{
			return _bouncebackEffect;
		}
		set
		{
			_bouncebackEffect = value;
		}
	}

	public bool touchEffect
	{
		get
		{
			return _touchEffect;
		}
		set
		{
			_touchEffect = value;
		}
	}

	public bool inertiaDisabled
	{
		get
		{
			return _inertiaDisabled;
		}
		set
		{
			_inertiaDisabled = value;
		}
	}

	public bool softnessOnTopOrLeftSide
	{
		get
		{
			return _softnessOnTopOrLeftSide;
		}
		set
		{
			_softnessOnTopOrLeftSide = value;
		}
	}

	public float scrollStep
	{
		get
		{
			return _scrollStep;
		}
		set
		{
			_scrollStep = value;
			if (_scrollStep == 0f)
			{
				_scrollStep = UIConfig.defaultScrollStep;
			}
		}
	}

	public bool snapToItem
	{
		get
		{
			return _snapToItem;
		}
		set
		{
			_snapToItem = value;
		}
	}

	public bool pageMode
	{
		get
		{
			return _pageMode;
		}
		set
		{
			_pageMode = value;
		}
	}

	public Controller pageController
	{
		get
		{
			return _pageController;
		}
		set
		{
			_pageController = value;
		}
	}

	public bool mouseWheelEnabled
	{
		get
		{
			return _mouseWheelEnabled;
		}
		set
		{
			_mouseWheelEnabled = value;
		}
	}

	public float decelerationRate
	{
		get
		{
			return _decelerationRate;
		}
		set
		{
			_decelerationRate = value;
		}
	}

	public bool isDragged => _dragged;

	public float percX
	{
		get
		{
			if (_overlapSize.x != 0f)
			{
				return _xPos / _overlapSize.x;
			}
			return 0f;
		}
		set
		{
			SetPercX(value, ani: false);
		}
	}

	public float percY
	{
		get
		{
			if (_overlapSize.y != 0f)
			{
				return _yPos / _overlapSize.y;
			}
			return 0f;
		}
		set
		{
			SetPercY(value, ani: false);
		}
	}

	public float posX
	{
		get
		{
			return _xPos;
		}
		set
		{
			SetPosX(value, ani: false);
		}
	}

	public float posY
	{
		get
		{
			return _yPos;
		}
		set
		{
			SetPosY(value, ani: false);
		}
	}

	public bool isBottomMost
	{
		get
		{
			if (_yPos != _overlapSize.y)
			{
				return _overlapSize.y == 0f;
			}
			return true;
		}
	}

	public bool isRightMost
	{
		get
		{
			if (_xPos != _overlapSize.x)
			{
				return _overlapSize.x == 0f;
			}
			return true;
		}
	}

	public int currentPageX
	{
		get
		{
			if (!_pageMode)
			{
				return 0;
			}
			int num = Mathf.FloorToInt(_xPos / _pageSize.x);
			if (_xPos - (float)num * _pageSize.x > _pageSize.x * 0.5f)
			{
				num++;
			}
			return num;
		}
		set
		{
			if (_pageMode)
			{
				_owner.EnsureBoundsCorrect();
				if (_overlapSize.x > 0f)
				{
					SetPosX((float)value * _pageSize.x, ani: false);
				}
			}
		}
	}

	public int currentPageY
	{
		get
		{
			if (!_pageMode)
			{
				return 0;
			}
			int num = Mathf.FloorToInt(_yPos / _pageSize.y);
			if (_yPos - (float)num * _pageSize.y > _pageSize.y * 0.5f)
			{
				num++;
			}
			return num;
		}
		set
		{
			if (_pageMode)
			{
				_owner.EnsureBoundsCorrect();
				if (_overlapSize.y > 0f)
				{
					SetPosY((float)value * _pageSize.y, ani: false);
				}
			}
		}
	}

	public float scrollingPosX => Mathf.Clamp(0f - _container.x, 0f, _overlapSize.x);

	public float scrollingPosY => Mathf.Clamp(0f - _container.y, 0f, _overlapSize.y);

	public float contentWidth => _contentSize.x;

	public float contentHeight => _contentSize.y;

	public float viewWidth
	{
		get
		{
			return _viewSize.x;
		}
		set
		{
			value = value + (float)_owner.margin.left + (float)_owner.margin.right;
			if (_vtScrollBar != null && !_floating)
			{
				value += _vtScrollBar.width;
			}
			_owner.width = value;
		}
	}

	public float viewHeight
	{
		get
		{
			return _viewSize.y;
		}
		set
		{
			value = value + (float)_owner.margin.top + (float)_owner.margin.bottom;
			if (_hzScrollBar != null && !_floating)
			{
				value += _hzScrollBar.height;
			}
			_owner.height = value;
		}
	}

	public ScrollPane(GComponent owner)
	{
		_onScroll = new EventListener(this, "onScroll");
		_onScrollEnd = new EventListener(this, "onScrollEnd");
		_scrollStep = UIConfig.defaultScrollStep;
		_softnessOnTopOrLeftSide = UIConfig.allowSoftnessOnTopOrLeftSide;
		_decelerationRate = UIConfig.defaultScrollDecelerationRate;
		_touchEffect = UIConfig.defaultScrollTouchEffect;
		_bouncebackEffect = UIConfig.defaultScrollBounceEffect;
		_mouseWheelEnabled = true;
		_pageSize = Vector2.one;
		_refreshDelegate = Refresh;
		_tweenUpdateDelegate = TweenUpdate;
		_hideScrollBarDelegate = __barTweenComplete;
		_owner = owner;
		_maskContainer = new Container();
		_owner.rootContainer.AddChild(_maskContainer);
		_container = _owner.container;
		_container.SetXY(0f, 0f);
		_maskContainer.AddChild(_container);
		_owner.rootContainer.onMouseWheel.Add(__mouseWheel);
		_owner.rootContainer.onTouchBegin.Add(__touchBegin);
		_owner.rootContainer.onTouchMove.Add(__touchMove);
		_owner.rootContainer.onTouchEnd.Add(__touchEnd);
	}

	public void Setup(ByteBuffer buffer)
	{
		_scrollType = (ScrollType)buffer.ReadByte();
		ScrollBarDisplayType scrollBarDisplayType = (ScrollBarDisplayType)buffer.ReadByte();
		int num = buffer.ReadInt();
		if (buffer.ReadBool())
		{
			_scrollBarMargin.top = buffer.ReadInt();
			_scrollBarMargin.bottom = buffer.ReadInt();
			_scrollBarMargin.left = buffer.ReadInt();
			_scrollBarMargin.right = buffer.ReadInt();
		}
		string text = buffer.ReadS();
		string text2 = buffer.ReadS();
		string text3 = buffer.ReadS();
		string text4 = buffer.ReadS();
		_displayOnLeft = (num & 1) != 0;
		_snapToItem = (num & 2) != 0;
		_displayInDemand = (num & 4) != 0;
		_pageMode = (num & 8) != 0;
		if ((num & 0x10) != 0)
		{
			_touchEffect = true;
		}
		else if ((num & 0x20) != 0)
		{
			_touchEffect = false;
		}
		if ((num & 0x40) != 0)
		{
			_bouncebackEffect = true;
		}
		else if ((num & 0x80) != 0)
		{
			_bouncebackEffect = false;
		}
		_inertiaDisabled = (num & 0x100) != 0;
		_maskDisabled = (num & 0x200) != 0;
		_floating = (num & 0x400) != 0;
		_dontClipMargin = (num & 0x800) != 0;
		if (scrollBarDisplayType == ScrollBarDisplayType.Default)
		{
			scrollBarDisplayType = ((!Application.isMobilePlatform) ? UIConfig.defaultScrollBarDisplay : ScrollBarDisplayType.Auto);
		}
		if (scrollBarDisplayType != ScrollBarDisplayType.Hidden)
		{
			if (_scrollType == ScrollType.Both || _scrollType == ScrollType.Vertical)
			{
				string text5 = ((text != null) ? text : UIConfig.verticalScrollBar);
				if (!string.IsNullOrEmpty(text5))
				{
					_vtScrollBar = UIPackage.CreateObjectFromURL(text5) as GScrollBar;
					if (_vtScrollBar == null)
					{
						Debug.LogWarning("FairyGUI: cannot create scrollbar from " + text5);
					}
					else
					{
						_vtScrollBar.SetScrollPane(this, vertical: true);
						_owner.rootContainer.AddChild(_vtScrollBar.displayObject);
					}
				}
			}
			if (_scrollType == ScrollType.Both || _scrollType == ScrollType.Horizontal)
			{
				string text6 = ((text2 != null) ? text2 : UIConfig.horizontalScrollBar);
				if (!string.IsNullOrEmpty(text6))
				{
					_hzScrollBar = UIPackage.CreateObjectFromURL(text6) as GScrollBar;
					if (_hzScrollBar == null)
					{
						Debug.LogWarning("FairyGUI: cannot create scrollbar from " + text6);
					}
					else
					{
						_hzScrollBar.SetScrollPane(this, vertical: false);
						_owner.rootContainer.AddChild(_hzScrollBar.displayObject);
					}
				}
			}
			_scrollBarDisplayAuto = scrollBarDisplayType == ScrollBarDisplayType.Auto;
			if (_scrollBarDisplayAuto)
			{
				if (_vtScrollBar != null)
				{
					_vtScrollBar.displayObject.visible = false;
				}
				if (_hzScrollBar != null)
				{
					_hzScrollBar.displayObject.visible = false;
				}
				_owner.rootContainer.onRollOver.Add(__rollOver);
				_owner.rootContainer.onRollOut.Add(__rollOut);
			}
		}
		else
		{
			_mouseWheelEnabled = false;
		}
		if (Application.isPlaying)
		{
			if (text3 != null)
			{
				_header = (GComponent)UIPackage.CreateObjectFromURL(text3);
				if (_header == null)
				{
					Debug.LogWarning("FairyGUI: cannot create scrollPane header from " + text3);
				}
			}
			if (text4 != null)
			{
				_footer = (GComponent)UIPackage.CreateObjectFromURL(text4);
				if (_footer == null)
				{
					Debug.LogWarning("FairyGUI: cannot create scrollPane footer from " + text4);
				}
			}
			if (_header != null || _footer != null)
			{
				_refreshBarAxis = ((_scrollType == ScrollType.Both || _scrollType == ScrollType.Vertical) ? 1 : 0);
			}
		}
		SetSize(owner.width, owner.height);
	}

	public void Dispose()
	{
		RemoveEventListeners();
		if (_tweening != 0)
		{
			Timers.inst.Remove(_tweenUpdateDelegate);
		}
		if (draggingPane == this)
		{
			draggingPane = null;
		}
		_pageController = null;
		if (_hzScrollBar != null)
		{
			_hzScrollBar.Dispose();
		}
		if (_vtScrollBar != null)
		{
			_vtScrollBar.Dispose();
		}
		if (_header != null)
		{
			_header.Dispose();
		}
		if (_footer != null)
		{
			_footer.Dispose();
		}
	}

	public void SetPercX(float value, bool ani)
	{
		_owner.EnsureBoundsCorrect();
		SetPosX(_overlapSize.x * Mathf.Clamp01(value), ani);
	}

	public void SetPercY(float value, bool ani)
	{
		_owner.EnsureBoundsCorrect();
		SetPosY(_overlapSize.y * Mathf.Clamp01(value), ani);
	}

	public void SetPosX(float value, bool ani)
	{
		_owner.EnsureBoundsCorrect();
		if (_loop == 1)
		{
			LoopCheckingNewPos(ref value, 0);
		}
		value = Mathf.Clamp(value, 0f, _overlapSize.x);
		if (value != _xPos)
		{
			_xPos = value;
			PosChanged(ani);
		}
	}

	public void SetPosY(float value, bool ani)
	{
		_owner.EnsureBoundsCorrect();
		if (_loop == 2)
		{
			LoopCheckingNewPos(ref value, 1);
		}
		value = Mathf.Clamp(value, 0f, _overlapSize.y);
		if (value != _yPos)
		{
			_yPos = value;
			PosChanged(ani);
		}
	}

	public void SetCurrentPageX(int value, bool ani)
	{
		if (_pageMode)
		{
			_owner.EnsureBoundsCorrect();
			if (_overlapSize.x > 0f)
			{
				SetPosX((float)value * _pageSize.x, ani);
			}
		}
	}

	public void SetCurrentPageY(int value, bool ani)
	{
		if (_pageMode)
		{
			_owner.EnsureBoundsCorrect();
			if (_overlapSize.y > 0f)
			{
				SetPosY((float)value * _pageSize.y, ani);
			}
		}
	}

	public void ScrollTop()
	{
		ScrollTop(ani: false);
	}

	public void ScrollTop(bool ani)
	{
		SetPercY(0f, ani);
	}

	public void ScrollBottom()
	{
		ScrollBottom(ani: false);
	}

	public void ScrollBottom(bool ani)
	{
		SetPercY(1f, ani);
	}

	public void ScrollUp()
	{
		ScrollUp(1f, ani: false);
	}

	public void ScrollUp(float ratio, bool ani)
	{
		if (_pageMode)
		{
			SetPosY(_yPos - _pageSize.y * ratio, ani);
		}
		else
		{
			SetPosY(_yPos - _scrollStep * ratio, ani);
		}
	}

	public void ScrollDown()
	{
		ScrollDown(1f, ani: false);
	}

	public void ScrollDown(float ratio, bool ani)
	{
		if (_pageMode)
		{
			SetPosY(_yPos + _pageSize.y * ratio, ani);
		}
		else
		{
			SetPosY(_yPos + _scrollStep * ratio, ani);
		}
	}

	public void ScrollLeft()
	{
		ScrollLeft(1f, ani: false);
	}

	public void ScrollLeft(float ratio, bool ani)
	{
		if (_pageMode)
		{
			SetPosX(_xPos - _pageSize.x * ratio, ani);
		}
		else
		{
			SetPosX(_xPos - _scrollStep * ratio, ani);
		}
	}

	public void ScrollRight()
	{
		ScrollRight(1f, ani: false);
	}

	public void ScrollRight(float ratio, bool ani)
	{
		if (_pageMode)
		{
			SetPosX(_xPos + _pageSize.x * ratio, ani);
		}
		else
		{
			SetPosX(_xPos + _scrollStep * ratio, ani);
		}
	}

	public void ScrollToView(GObject obj)
	{
		ScrollToView(obj, ani: false);
	}

	public void ScrollToView(GObject obj, bool ani)
	{
		ScrollToView(obj, ani, setFirst: false);
	}

	public void ScrollToView(GObject obj, bool ani, bool setFirst)
	{
		_owner.EnsureBoundsCorrect();
		if (_needRefresh)
		{
			Refresh();
		}
		Rect rect = new Rect(obj.x, obj.y, obj.width, obj.height);
		if (obj.parent != _owner)
		{
			rect = obj.parent.TransformRect(rect, _owner);
		}
		ScrollToView(rect, ani, setFirst);
	}

	public void ScrollToView(Rect rect, bool ani, bool setFirst)
	{
		_owner.EnsureBoundsCorrect();
		if (_needRefresh)
		{
			Refresh();
		}
		if (_overlapSize.y > 0f)
		{
			float num = _yPos + _viewSize.y;
			if (setFirst || rect.y <= _yPos || rect.height >= _viewSize.y)
			{
				if (_pageMode)
				{
					SetPosY(Mathf.Floor(rect.y / _pageSize.y) * _pageSize.y, ani);
				}
				else
				{
					SetPosY(rect.y, ani);
				}
			}
			else if (rect.y + rect.height > num)
			{
				if (_pageMode)
				{
					SetPosY(Mathf.Floor(rect.y / _pageSize.y) * _pageSize.y, ani);
				}
				else if (rect.height <= _viewSize.y / 2f)
				{
					SetPosY(rect.y + rect.height * 2f - _viewSize.y, ani);
				}
				else
				{
					SetPosY(rect.y + rect.height - _viewSize.y, ani);
				}
			}
		}
		if (_overlapSize.x > 0f)
		{
			float num2 = _xPos + _viewSize.x;
			if (setFirst || rect.x <= _xPos || rect.width >= _viewSize.x)
			{
				if (_pageMode)
				{
					SetPosX(Mathf.Floor(rect.x / _pageSize.x) * _pageSize.x, ani);
				}
				SetPosX(rect.x, ani);
			}
			else if (rect.x + rect.width > num2)
			{
				if (_pageMode)
				{
					SetPosX(Mathf.Floor(rect.x / _pageSize.x) * _pageSize.x, ani);
				}
				else if (rect.width <= _viewSize.x / 2f)
				{
					SetPosX(rect.x + rect.width * 2f - _viewSize.x, ani);
				}
				else
				{
					SetPosX(rect.x + rect.width - _viewSize.x, ani);
				}
			}
		}
		if (!ani && _needRefresh)
		{
			Refresh();
		}
	}

	public bool IsChildInView(GObject obj)
	{
		if (_overlapSize.y > 0f)
		{
			float num = obj.y + _container.y;
			if (num <= 0f - obj.height || num >= _viewSize.y)
			{
				return false;
			}
		}
		if (_overlapSize.x > 0f)
		{
			float num2 = obj.x + _container.x;
			if (num2 <= 0f - obj.width || num2 >= _viewSize.x)
			{
				return false;
			}
		}
		return true;
	}

	public void CancelDragging()
	{
		Stage.inst.RemoveTouchMonitor(_owner.rootContainer);
		if (draggingPane == this)
		{
			draggingPane = null;
		}
		_gestureFlag = 0;
		_dragged = false;
	}

	public void LockHeader(int size)
	{
		if (_headerLockedSize != size)
		{
			_headerLockedSize = size;
			if (!isDispatching("onPullDownRelease") && _container.xy[_refreshBarAxis] >= 0f)
			{
				_tweenStart = _container.xy;
				_tweenChange = Vector2.zero;
				_tweenChange[_refreshBarAxis] = (float)_headerLockedSize - _tweenStart[_refreshBarAxis];
				_tweenDuration = new Vector2(TWEEN_TIME_DEFAULT, TWEEN_TIME_DEFAULT);
				StartTween(2);
			}
		}
	}

	public void LockFooter(int size)
	{
		if (_footerLockedSize != size)
		{
			_footerLockedSize = size;
			if (!isDispatching("onPullUpRelease") && _container.xy[_refreshBarAxis] <= 0f - _overlapSize[_refreshBarAxis])
			{
				_tweenStart = _container.xy;
				_tweenChange = Vector2.zero;
				float num = _overlapSize[_refreshBarAxis];
				num = ((num != 0f) ? (num + (float)_footerLockedSize) : Mathf.Max(_contentSize[_refreshBarAxis] + (float)_footerLockedSize - _viewSize[_refreshBarAxis], 0f));
				_tweenChange[_refreshBarAxis] = 0f - num - _tweenStart[_refreshBarAxis];
				_tweenDuration = new Vector2(TWEEN_TIME_DEFAULT, TWEEN_TIME_DEFAULT);
				StartTween(2);
			}
		}
	}

	internal void OnOwnerSizeChanged()
	{
		SetSize(_owner.width, _owner.height);
		PosChanged(ani: false);
	}

	internal void HandleControllerChanged(Controller c)
	{
		if (_pageController == c)
		{
			if (_scrollType == ScrollType.Horizontal)
			{
				SetCurrentPageX(c.selectedIndex, ani: true);
			}
			else
			{
				SetCurrentPageY(c.selectedIndex, ani: true);
			}
		}
	}

	private void UpdatePageController()
	{
		if (_pageController != null && !_pageController.changing)
		{
			int num = ((_scrollType != ScrollType.Horizontal) ? currentPageY : currentPageX);
			if (num < _pageController.pageCount)
			{
				Controller controller = _pageController;
				_pageController = null;
				controller.selectedIndex = num;
				_pageController = controller;
			}
		}
	}

	internal void AdjustMaskContainer()
	{
		float num = ((!_displayOnLeft || _vtScrollBar == null || _floating) ? ((float)_owner.margin.left) : ((float)Mathf.FloorToInt((float)_owner.margin.left + _vtScrollBar.width)));
		float num2 = _owner.margin.top;
		num += _owner._alignOffset.x;
		num2 += _owner._alignOffset.y;
		_maskContainer.SetXY(num, num2);
	}

	private void SetSize(float aWidth, float aHeight)
	{
		AdjustMaskContainer();
		if (_hzScrollBar != null)
		{
			_hzScrollBar.y = aHeight - _hzScrollBar.height;
			if (_vtScrollBar != null)
			{
				_hzScrollBar.width = aWidth - _vtScrollBar.width - (float)_scrollBarMargin.left - (float)_scrollBarMargin.right;
				if (_displayOnLeft)
				{
					_hzScrollBar.x = (float)_scrollBarMargin.left + _vtScrollBar.width;
				}
				else
				{
					_hzScrollBar.x = _scrollBarMargin.left;
				}
			}
			else
			{
				_hzScrollBar.width = aWidth - (float)_scrollBarMargin.left - (float)_scrollBarMargin.right;
				_hzScrollBar.x = _scrollBarMargin.left;
			}
		}
		if (_vtScrollBar != null)
		{
			if (!_displayOnLeft)
			{
				_vtScrollBar.x = aWidth - _vtScrollBar.width;
			}
			if (_hzScrollBar != null)
			{
				_vtScrollBar.height = aHeight - _hzScrollBar.height - (float)_scrollBarMargin.top - (float)_scrollBarMargin.bottom;
			}
			else
			{
				_vtScrollBar.height = aHeight - (float)_scrollBarMargin.top - (float)_scrollBarMargin.bottom;
			}
			_vtScrollBar.y = _scrollBarMargin.top;
		}
		_viewSize.x = aWidth;
		_viewSize.y = aHeight;
		if (_hzScrollBar != null && !_floating)
		{
			_viewSize.y -= _hzScrollBar.height;
		}
		if (_vtScrollBar != null && !_floating)
		{
			_viewSize.x -= _vtScrollBar.width;
		}
		_viewSize.x -= _owner.margin.left + _owner.margin.right;
		_viewSize.y -= _owner.margin.top + _owner.margin.bottom;
		_viewSize.x = Mathf.Max(1f, _viewSize.x);
		_viewSize.y = Mathf.Max(1f, _viewSize.y);
		_pageSize.x = _viewSize.x;
		_pageSize.y = _viewSize.y;
		HandleSizeChanged();
	}

	internal void SetContentSize(float aWidth, float aHeight)
	{
		if (!Mathf.Approximately(_contentSize.x, aWidth) || !Mathf.Approximately(_contentSize.y, aHeight))
		{
			_contentSize.x = aWidth;
			_contentSize.y = aHeight;
			HandleSizeChanged();
		}
	}

	internal void ChangeContentSizeOnScrolling(float deltaWidth, float deltaHeight, float deltaPosX, float deltaPosY)
	{
		bool flag = _xPos == _overlapSize.x;
		bool flag2 = _yPos == _overlapSize.y;
		_contentSize.x += deltaWidth;
		_contentSize.y += deltaHeight;
		HandleSizeChanged();
		if (_tweening == 1)
		{
			if (deltaWidth != 0f && flag && _tweenChange.x < 0f)
			{
				_xPos = _overlapSize.x;
				_tweenChange.x = 0f - _xPos - _tweenStart.x;
			}
			if (deltaHeight != 0f && flag2 && _tweenChange.y < 0f)
			{
				_yPos = _overlapSize.y;
				_tweenChange.y = 0f - _yPos - _tweenStart.y;
			}
		}
		else if (_tweening == 2)
		{
			if (deltaPosX != 0f)
			{
				_container.x -= deltaPosX;
				_tweenStart.x -= deltaPosX;
				_xPos = 0f - _container.x;
			}
			if (deltaPosY != 0f)
			{
				_container.y -= deltaPosY;
				_tweenStart.y -= deltaPosY;
				_yPos = 0f - _container.y;
			}
		}
		else if (_dragged)
		{
			if (deltaPosX != 0f)
			{
				_container.x -= deltaPosX;
				_containerPos.x -= deltaPosX;
				_xPos = 0f - _container.x;
			}
			if (deltaPosY != 0f)
			{
				_container.y -= deltaPosY;
				_containerPos.y -= deltaPosY;
				_yPos = 0f - _container.y;
			}
		}
		else
		{
			if (deltaWidth != 0f && flag)
			{
				_xPos = _overlapSize.x;
				_container.x = 0f - _xPos;
			}
			if (deltaHeight != 0f && flag2)
			{
				_yPos = _overlapSize.y;
				_container.y = 0f - _yPos;
			}
		}
		if (_pageMode)
		{
			UpdatePageController();
		}
	}

	private void HandleSizeChanged()
	{
		if (_displayInDemand)
		{
			_vScrollNone = _contentSize.y <= _viewSize.y;
			_hScrollNone = _contentSize.x <= _viewSize.x;
		}
		if (_vtScrollBar != null)
		{
			if (_contentSize.y == 0f)
			{
				_vtScrollBar.SetDisplayPerc(0f);
			}
			else
			{
				_vtScrollBar.SetDisplayPerc(Mathf.Min(1f, _viewSize.y / _contentSize.y));
			}
		}
		if (_hzScrollBar != null)
		{
			if (_contentSize.x == 0f)
			{
				_hzScrollBar.SetDisplayPerc(0f);
			}
			else
			{
				_hzScrollBar.SetDisplayPerc(Mathf.Min(1f, _viewSize.x / _contentSize.x));
			}
		}
		UpdateScrollBarVisible();
		if (!_maskDisabled)
		{
			Rect value = new Rect(0f - _owner._alignOffset.x, 0f - _owner._alignOffset.y, _viewSize.x, _viewSize.y);
			if (_vScrollNone && _vtScrollBar != null)
			{
				value.width += _vtScrollBar.width;
			}
			if (_hScrollNone && _hzScrollBar != null)
			{
				value.height += _hzScrollBar.height;
			}
			if (_dontClipMargin)
			{
				value.x -= _owner.margin.left;
				value.width += _owner.margin.left + _owner.margin.right;
				value.y -= _owner.margin.top;
				value.height += _owner.margin.top + _owner.margin.bottom;
			}
			_maskContainer.clipRect = value;
		}
		if (_scrollType == ScrollType.Horizontal || _scrollType == ScrollType.Both)
		{
			_overlapSize.x = Mathf.CeilToInt(Math.Max(0f, _contentSize.x - _viewSize.x));
		}
		else
		{
			_overlapSize.x = 0f;
		}
		if (_scrollType == ScrollType.Vertical || _scrollType == ScrollType.Both)
		{
			_overlapSize.y = Mathf.CeilToInt(Math.Max(0f, _contentSize.y - _viewSize.y));
		}
		else
		{
			_overlapSize.y = 0f;
		}
		_xPos = Mathf.Clamp(_xPos, 0f, _overlapSize.x);
		_yPos = Mathf.Clamp(_yPos, 0f, _overlapSize.y);
		float num = _overlapSize[_refreshBarAxis];
		num = ((num != 0f) ? (num + (float)_footerLockedSize) : Mathf.Max(_contentSize[_refreshBarAxis] + (float)_footerLockedSize - _viewSize[_refreshBarAxis], 0f));
		if (_refreshBarAxis == 0)
		{
			_container.SetXY(Mathf.Clamp(_container.x, 0f - num, _headerLockedSize), Mathf.Clamp(_container.y, 0f - _overlapSize.y, 0f));
		}
		else
		{
			_container.SetXY(Mathf.Clamp(_container.x, 0f - _overlapSize.x, 0f), Mathf.Clamp(_container.y, 0f - num, _headerLockedSize));
		}
		if (_header != null)
		{
			if (_refreshBarAxis == 0)
			{
				_header.height = _viewSize.y;
			}
			else
			{
				_header.width = _viewSize.x;
			}
		}
		if (_footer != null)
		{
			if (_refreshBarAxis == 0)
			{
				_footer.height = _viewSize.y;
			}
			else
			{
				_footer.width = _viewSize.x;
			}
		}
		UpdateScrollBarPos();
		if (_pageMode)
		{
			UpdatePageController();
		}
	}

	private void PosChanged(bool ani)
	{
		if (_aniFlag == 0)
		{
			_aniFlag = (ani ? 1 : (-1));
		}
		else if (_aniFlag == 1 && !ani)
		{
			_aniFlag = -1;
		}
		_needRefresh = true;
		UpdateContext.OnBegin -= _refreshDelegate;
		UpdateContext.OnBegin += _refreshDelegate;
	}

	private void Refresh()
	{
		_needRefresh = false;
		UpdateContext.OnBegin -= _refreshDelegate;
		if (_owner.displayObject != null && !_owner.displayObject.isDisposed)
		{
			if (_pageMode || _snapToItem)
			{
				Vector2 pos = new Vector2(0f - _xPos, 0f - _yPos);
				AlignPosition(ref pos, inertialScrolling: false);
				_xPos = 0f - pos.x;
				_yPos = 0f - pos.y;
			}
			Refresh2();
			_onScroll.Call();
			if (_needRefresh)
			{
				_needRefresh = false;
				UpdateContext.OnBegin -= _refreshDelegate;
				Refresh2();
			}
			UpdateScrollBarPos();
			_aniFlag = 0;
		}
	}

	private void Refresh2()
	{
		if (_aniFlag == 1 && !_dragged)
		{
			Vector2 vector = default(Vector2);
			if (_overlapSize.x > 0f)
			{
				vector.x = -(int)_xPos;
			}
			else
			{
				if (_container.x != 0f)
				{
					_container.x = 0f;
				}
				vector.x = 0f;
			}
			if (_overlapSize.y > 0f)
			{
				vector.y = -(int)_yPos;
			}
			else
			{
				if (_container.y != 0f)
				{
					_container.y = 0f;
				}
				vector.y = 0f;
			}
			if (vector.x != _container.x || vector.y != _container.y)
			{
				_tweenDuration = new Vector2(TWEEN_TIME_GO, TWEEN_TIME_GO);
				_tweenStart = _container.xy;
				_tweenChange = vector - _tweenStart;
				StartTween(1);
			}
			else if (_tweening != 0)
			{
				KillTween();
			}
		}
		else
		{
			if (_tweening != 0)
			{
				KillTween();
			}
			_container.SetXY((int)(0f - _xPos), (int)(0f - _yPos));
			LoopCheckingCurrent();
		}
		if (_pageMode)
		{
			UpdatePageController();
		}
	}

	private void __touchBegin(EventContext context)
	{
		if (!_touchEffect)
		{
			return;
		}
		InputEvent inputEvent = context.inputEvent;
		if (inputEvent.button == 0)
		{
			context.CaptureTouch();
			Vector2 lastTouchPos = _owner.GlobalToLocal(inputEvent.position);
			if (_tweening != 0)
			{
				KillTween();
				Stage.inst.CancelClick(inputEvent.touchId);
				_dragged = true;
			}
			else
			{
				_dragged = false;
			}
			_containerPos = _container.xy;
			_beginTouchPos = (_lastTouchPos = lastTouchPos);
			_lastTouchGlobalPos = inputEvent.position;
			_isHoldAreaDone = false;
			_velocity = Vector2.zero;
			_velocityScale = 1f;
			_lastMoveTime = Time.unscaledTime;
		}
	}

	private void __touchMove(EventContext context)
	{
		if (!_touchEffect || (draggingPane != null && draggingPane != this) || GObject.draggingObject != null)
		{
			return;
		}
		InputEvent inputEvent = context.inputEvent;
		Vector2 vector = _owner.GlobalToLocal(inputEvent.position);
		if (float.IsNaN(vector.x))
		{
			return;
		}
		int num = ((!Stage.touchScreen) ? 20 : UIConfig.touchScrollSensitivity);
		bool flag = false;
		bool flag2 = false;
		if (_scrollType == ScrollType.Vertical)
		{
			if (!_isHoldAreaDone)
			{
				_gestureFlag |= 1;
				float num2 = Mathf.Abs(_beginTouchPos.y - vector.y);
				if (num2 < (float)num)
				{
					return;
				}
				if ((_gestureFlag & 2) != 0)
				{
					float num3 = Mathf.Abs(_beginTouchPos.x - vector.x);
					if (num2 < num3)
					{
						return;
					}
				}
			}
			flag = true;
		}
		else if (_scrollType == ScrollType.Horizontal)
		{
			if (!_isHoldAreaDone)
			{
				_gestureFlag |= 2;
				float num2 = Mathf.Abs(_beginTouchPos.x - vector.x);
				if (num2 < (float)num)
				{
					return;
				}
				if ((_gestureFlag & 1) != 0)
				{
					float num4 = Mathf.Abs(_beginTouchPos.y - vector.y);
					if (num2 < num4)
					{
						return;
					}
				}
			}
			flag2 = true;
		}
		else
		{
			_gestureFlag = 3;
			if (!_isHoldAreaDone)
			{
				float num2 = Mathf.Abs(_beginTouchPos.y - vector.y);
				if (num2 < (float)num)
				{
					num2 = Mathf.Abs(_beginTouchPos.x - vector.x);
					if (num2 < (float)num)
					{
						return;
					}
				}
			}
			flag = (flag2 = true);
		}
		Vector2 vector2 = _containerPos + vector - _beginTouchPos;
		vector2.x = (int)vector2.x;
		vector2.y = (int)vector2.y;
		if (flag)
		{
			if (vector2.y > 0f)
			{
				if (!_bouncebackEffect)
				{
					_container.y = 0f;
				}
				else if (_header != null && _header.maxHeight != 0)
				{
					_container.y = (int)Mathf.Min(vector2.y * 0.5f, _header.maxHeight);
				}
				else
				{
					_container.y = (int)Mathf.Min(vector2.y * 0.5f, _viewSize.y * PULL_RATIO);
				}
			}
			else if (vector2.y < 0f - _overlapSize.y)
			{
				if (!_bouncebackEffect)
				{
					_container.y = 0f - _overlapSize.y;
				}
				else if (_footer != null && _footer.maxHeight > 0)
				{
					_container.y = (float)(int)Mathf.Max((vector2.y + _overlapSize.y) * 0.5f, -_footer.maxHeight) - _overlapSize.y;
				}
				else
				{
					_container.y = (float)(int)Mathf.Max((vector2.y + _overlapSize.y) * 0.5f, (0f - _viewSize.y) * PULL_RATIO) - _overlapSize.y;
				}
			}
			else
			{
				_container.y = vector2.y;
			}
		}
		if (flag2)
		{
			if (vector2.x > 0f)
			{
				if (!_bouncebackEffect)
				{
					_container.x = 0f;
				}
				else if (_header != null && _header.maxWidth != 0)
				{
					_container.x = (int)Mathf.Min(vector2.x * 0.5f, _header.maxWidth);
				}
				else
				{
					_container.x = (int)Mathf.Min(vector2.x * 0.5f, _viewSize.x * PULL_RATIO);
				}
			}
			else if (vector2.x < 0f - _overlapSize.x)
			{
				if (!_bouncebackEffect)
				{
					_container.x = 0f - _overlapSize.x;
				}
				else if (_footer != null && _footer.maxWidth > 0)
				{
					_container.x = (float)(int)Mathf.Max((vector2.x + _overlapSize.x) * 0.5f, -_footer.maxWidth) - _overlapSize.x;
				}
				else
				{
					_container.x = (float)(int)Mathf.Max((vector2.x + _overlapSize.x) * 0.5f, (0f - _viewSize.x) * PULL_RATIO) - _overlapSize.x;
				}
			}
			else
			{
				_container.x = vector2.x;
			}
		}
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		float num5 = (Time.unscaledTime - _lastMoveTime) * 60f - 1f;
		if (num5 > 1f)
		{
			_velocity *= Mathf.Pow(0.833f, num5);
		}
		Vector2 vector3 = vector - _lastTouchPos;
		if (!flag2)
		{
			vector3.x = 0f;
		}
		if (!flag)
		{
			vector3.y = 0f;
		}
		_velocity = Vector2.Lerp(_velocity, vector3 / unscaledDeltaTime, unscaledDeltaTime * 10f);
		Vector2 vector4 = _lastTouchGlobalPos - inputEvent.position;
		if (vector3.x != 0f)
		{
			_velocityScale = Mathf.Abs(vector4.x / vector3.x);
		}
		else if (vector3.y != 0f)
		{
			_velocityScale = Mathf.Abs(vector4.y / vector3.y);
		}
		_lastTouchPos = vector;
		_lastTouchGlobalPos = inputEvent.position;
		_lastMoveTime = Time.unscaledTime;
		if (_overlapSize.x > 0f)
		{
			_xPos = Mathf.Clamp(0f - _container.x, 0f, _overlapSize.x);
		}
		if (_overlapSize.y > 0f)
		{
			_yPos = Mathf.Clamp(0f - _container.y, 0f, _overlapSize.y);
		}
		if (_loop != 0)
		{
			vector2 = _container.xy;
			if (LoopCheckingCurrent())
			{
				_containerPos += _container.xy - vector2;
			}
		}
		draggingPane = this;
		_isHoldAreaDone = true;
		_dragged = true;
		UpdateScrollBarPos();
		UpdateScrollBarVisible();
		if (_pageMode)
		{
			UpdatePageController();
		}
		_onScroll.Call();
	}

	private void __touchEnd(EventContext context)
	{
		if (draggingPane == this)
		{
			draggingPane = null;
		}
		_gestureFlag = 0;
		if (!_dragged || !_touchEffect)
		{
			_dragged = false;
			return;
		}
		_dragged = false;
		_tweenStart = _container.xy;
		Vector2 endPos = _tweenStart;
		bool flag = false;
		if (_container.x > 0f)
		{
			endPos.x = 0f;
			flag = true;
		}
		else if (_container.x < 0f - _overlapSize.x)
		{
			endPos.x = 0f - _overlapSize.x;
			flag = true;
		}
		if (_container.y > 0f)
		{
			endPos.y = 0f;
			flag = true;
		}
		else if (_container.y < 0f - _overlapSize.y)
		{
			endPos.y = 0f - _overlapSize.y;
			flag = true;
		}
		if (flag)
		{
			_tweenChange = endPos - _tweenStart;
			if (_tweenChange.x < (float)(-UIConfig.touchDragSensitivity) || _tweenChange.y < (float)(-UIConfig.touchDragSensitivity))
			{
				DispatchEvent("onPullDownRelease", null);
			}
			else if (_tweenChange.x > (float)UIConfig.touchDragSensitivity || _tweenChange.y > (float)UIConfig.touchDragSensitivity)
			{
				DispatchEvent("onPullUpRelease", null);
			}
			if (_headerLockedSize > 0 && endPos[_refreshBarAxis] == 0f)
			{
				endPos[_refreshBarAxis] = _headerLockedSize;
				_tweenChange = endPos - _tweenStart;
			}
			else if (_footerLockedSize > 0 && endPos[_refreshBarAxis] == 0f - _overlapSize[_refreshBarAxis])
			{
				float num = _overlapSize[_refreshBarAxis];
				num = ((num != 0f) ? (num + (float)_footerLockedSize) : Mathf.Max(_contentSize[_refreshBarAxis] + (float)_footerLockedSize - _viewSize[_refreshBarAxis], 0f));
				endPos[_refreshBarAxis] = 0f - num;
				_tweenChange = endPos - _tweenStart;
			}
			_tweenDuration.Set(TWEEN_TIME_DEFAULT, TWEEN_TIME_DEFAULT);
		}
		else
		{
			if (!_inertiaDisabled)
			{
				float num2 = (Time.unscaledTime - _lastMoveTime) * 60f - 1f;
				if (num2 > 1f)
				{
					_velocity *= Mathf.Pow(0.833f, num2);
				}
				endPos = UpdateTargetAndDuration(_tweenStart);
			}
			else
			{
				_tweenDuration.Set(TWEEN_TIME_DEFAULT, TWEEN_TIME_DEFAULT);
			}
			Vector2 vector = endPos - _tweenStart;
			LoopCheckingTarget(ref endPos);
			if (_pageMode || _snapToItem)
			{
				AlignPosition(ref endPos, inertialScrolling: true);
			}
			_tweenChange = endPos - _tweenStart;
			if (_tweenChange.x == 0f && _tweenChange.y == 0f)
			{
				UpdateScrollBarVisible();
				return;
			}
			if (_pageMode || _snapToItem)
			{
				FixDuration(0, vector.x);
				FixDuration(1, vector.y);
			}
		}
		StartTween(2);
	}

	private void UpdateMouseWheelTrigger()
	{
		_MouseWheelTrigger = false;
	}

	private void __mouseWheel(EventContext context)
	{
		if (!_touchEffect || !_mouseWheelEnabled)
		{
			return;
		}
		float num = context.inputEvent.mouseWheelDelta / Stage.devicePixelRatio;
		if (_snapToItem && Mathf.Abs(num) < 1f)
		{
			num = Mathf.Sign(num);
		}
		_tweenStart = _container.xy;
		Vector2 endPos = _tweenStart;
		if (_overlapSize.x > 0f && _overlapSize.y == 0f)
		{
			float num2 = (_pageMode ? _pageSize.x : _scrollStep);
			float num3 = _xPos + num2 * num;
			endPos.x = 0f - num3;
		}
		else
		{
			float num4 = (_pageMode ? _pageSize.y : _scrollStep);
			float num5 = _yPos + num4 * num;
			endPos.y = 0f - num5;
		}
		if (!_MouseWheelTrigger)
		{
			bool flag = endPos.y > 0f;
			bool flag2 = endPos.y < 0f - _overlapSize.y;
			bool flag3 = endPos.x > 0f;
			bool flag4 = endPos.x < 0f - _overlapSize.x;
			if (flag || flag3)
			{
				if ((flag ? endPos.y : endPos.x) > (float)UIConfig.touchDragSensitivity)
				{
					DispatchEvent("onPullDownRelease", null);
				}
			}
			else if ((flag2 || flag4) && (flag2 ? (0f - _overlapSize.y - endPos.y) : (0f - _overlapSize.x - endPos.x)) > (float)UIConfig.touchDragSensitivity)
			{
				DispatchEvent("onPullUpRelease", null);
			}
			_MouseWheelTrigger = true;
		}
		LoopCheckingTarget(ref endPos);
		if (_pageMode || _snapToItem)
		{
			AlignPosition(ref endPos, inertialScrolling: true);
		}
		_tweenChange = endPos - _tweenStart;
		_tweenDuration.Set(TWEEN_TIME_DEFAULT, TWEEN_TIME_DEFAULT);
		StartTween(2);
	}

	private void __rollOver()
	{
		_hover = true;
		UpdateScrollBarVisible();
	}

	private void __rollOut()
	{
		_hover = false;
		UpdateScrollBarVisible();
	}

	internal void UpdateClipSoft()
	{
		Vector2 clipSoftness = _owner.clipSoftness;
		if (clipSoftness.x != 0f || clipSoftness.y != 0f)
		{
			_maskContainer.clipSoftness = new Vector4((_container.x >= 0f || !_softnessOnTopOrLeftSide) ? 0f : clipSoftness.x, (_container.y >= 0f || !_softnessOnTopOrLeftSide) ? 0f : clipSoftness.y, (0f - _container.x - _overlapSize.x >= 0f) ? 0f : clipSoftness.x, (0f - _container.y - _overlapSize.y >= 0f) ? 0f : clipSoftness.y);
		}
		else
		{
			_maskContainer.clipSoftness = null;
		}
	}

	private void UpdateScrollBarPos()
	{
		if (_vtScrollBar != null)
		{
			_vtScrollBar.setScrollPerc((_overlapSize.y == 0f) ? 0f : (Mathf.Clamp(0f - _container.y, 0f, _overlapSize.y) / _overlapSize.y));
		}
		if (_hzScrollBar != null)
		{
			_hzScrollBar.setScrollPerc((_overlapSize.x == 0f) ? 0f : (Mathf.Clamp(0f - _container.x, 0f, _overlapSize.x) / _overlapSize.x));
		}
		UpdateClipSoft();
		CheckRefreshBar();
	}

	public void UpdateScrollBarVisible()
	{
		if (_vtScrollBar != null)
		{
			if (_viewSize.y <= _vtScrollBar.minSize || _vScrollNone)
			{
				_vtScrollBar.displayObject.visible = false;
			}
			else
			{
				UpdateScrollBarVisible2(_vtScrollBar);
			}
		}
		if (_hzScrollBar != null)
		{
			if (_viewSize.x <= _hzScrollBar.minSize || _hScrollNone)
			{
				_hzScrollBar.displayObject.visible = false;
			}
			else
			{
				UpdateScrollBarVisible2(_hzScrollBar);
			}
		}
	}

	private void UpdateScrollBarVisible2(GScrollBar bar)
	{
		if (_scrollBarDisplayAuto)
		{
			GTween.Kill(bar, TweenPropType.Alpha, complete: false);
		}
		if (_scrollBarDisplayAuto && !_hover && _tweening == 0 && !_dragged && !bar.gripDragging)
		{
			if (bar.displayObject.visible)
			{
				GTween.To(1f, 0f, 0.5f).SetDelay(0.5f).OnComplete(_hideScrollBarDelegate)
					.SetTarget(bar, TweenPropType.Alpha);
			}
		}
		else
		{
			bar.alpha = 1f;
			bar.displayObject.visible = true;
		}
	}

	private void __barTweenComplete(GTweener tweener)
	{
		GObject obj = (GObject)tweener.target;
		obj.alpha = 1f;
		obj.displayObject.visible = false;
	}

	private float GetLoopPartSize(float division, int axis)
	{
		return (_contentSize[axis] + (float)((axis == 0) ? ((GList)_owner).columnGap : ((GList)_owner).lineGap)) / division;
	}

	private bool LoopCheckingCurrent()
	{
		bool flag = false;
		if (_loop == 1 && _overlapSize.x > 0f)
		{
			if (_xPos < 0.001f)
			{
				_xPos += GetLoopPartSize(2f, 0);
				flag = true;
			}
			else if (_xPos >= _overlapSize.x)
			{
				_xPos -= GetLoopPartSize(2f, 0);
				flag = true;
			}
		}
		else if (_loop == 2 && _overlapSize.y > 0f)
		{
			if (_yPos < 0.001f)
			{
				_yPos += GetLoopPartSize(2f, 1);
				flag = true;
			}
			else if (_yPos >= _overlapSize.y)
			{
				_yPos -= GetLoopPartSize(2f, 1);
				flag = true;
			}
		}
		if (flag)
		{
			_container.SetXY((int)(0f - _xPos), (int)(0f - _yPos));
		}
		return flag;
	}

	private void LoopCheckingTarget(ref Vector2 endPos)
	{
		if (_loop == 1)
		{
			LoopCheckingTarget(ref endPos, 0);
		}
		if (_loop == 2)
		{
			LoopCheckingTarget(ref endPos, 1);
		}
	}

	private void LoopCheckingTarget(ref Vector2 endPos, int axis)
	{
		if (endPos[axis] > 0f)
		{
			float loopPartSize = GetLoopPartSize(2f, axis);
			float num = _tweenStart[axis] - loopPartSize;
			if (num <= 0f && num >= 0f - _overlapSize[axis])
			{
				endPos[axis] -= loopPartSize;
				_tweenStart[axis] = num;
			}
		}
		else if (endPos[axis] < 0f - _overlapSize[axis])
		{
			float loopPartSize2 = GetLoopPartSize(2f, axis);
			float num2 = _tweenStart[axis] + loopPartSize2;
			if (num2 <= 0f && num2 >= 0f - _overlapSize[axis])
			{
				endPos[axis] += loopPartSize2;
				_tweenStart[axis] = num2;
			}
		}
	}

	private void LoopCheckingNewPos(ref float value, int axis)
	{
		if (_overlapSize[axis] == 0f)
		{
			return;
		}
		float num = ((axis == 0) ? _xPos : _yPos);
		bool flag = false;
		if (value < 0.001f)
		{
			value += GetLoopPartSize(2f, axis);
			if (value > num)
			{
				float loopPartSize = GetLoopPartSize(6f, axis);
				loopPartSize = (float)Mathf.CeilToInt((value - num) / loopPartSize) * loopPartSize;
				num = Mathf.Clamp(num + loopPartSize, 0f, _overlapSize[axis]);
				flag = true;
			}
		}
		else if (value >= _overlapSize[axis])
		{
			value -= GetLoopPartSize(2f, axis);
			if (value < num)
			{
				float loopPartSize2 = GetLoopPartSize(6f, axis);
				loopPartSize2 = (float)Mathf.CeilToInt((num - value) / loopPartSize2) * loopPartSize2;
				num = Mathf.Clamp(num - loopPartSize2, 0f, _overlapSize[axis]);
				flag = true;
			}
		}
		if (flag)
		{
			if (axis == 0)
			{
				_container.x = -(int)num;
			}
			else
			{
				_container.y = -(int)num;
			}
		}
	}

	private void AlignPosition(ref Vector2 pos, bool inertialScrolling)
	{
		if (_pageMode)
		{
			pos.x = AlignByPage(pos.x, 0, inertialScrolling);
			pos.y = AlignByPage(pos.y, 1, inertialScrolling);
		}
		else if (_snapToItem)
		{
			float xValue = 0f - pos.x;
			float yValue = 0f - pos.y;
			_owner.GetSnappingPosition(ref xValue, ref yValue);
			if (pos.x < 0f && pos.x > 0f - _overlapSize.x)
			{
				pos.x = 0f - xValue;
			}
			if (pos.y < 0f && pos.y > 0f - _overlapSize.y)
			{
				pos.y = 0f - yValue;
			}
		}
	}

	private float AlignByPage(float pos, int axis, bool inertialScrolling)
	{
		int num;
		if (pos > 0f)
		{
			num = 0;
		}
		else if (pos < 0f - _overlapSize[axis])
		{
			num = Mathf.CeilToInt(_contentSize[axis] / _pageSize[axis]) - 1;
		}
		else
		{
			num = Mathf.FloorToInt((0f - pos) / _pageSize[axis]);
			float num2 = (inertialScrolling ? (pos - _containerPos[axis]) : (pos - _container.xy[axis]));
			float num3 = Mathf.Min(_pageSize[axis], _contentSize[axis] - (float)(num + 1) * _pageSize[axis]);
			float num4 = 0f - pos - (float)num * _pageSize[axis];
			if (Mathf.Abs(num2) > _pageSize[axis])
			{
				if (num4 > num3 * 0.5f)
				{
					num++;
				}
			}
			else if (num4 > num3 * ((num2 < 0f) ? 0.3f : 0.7f))
			{
				num++;
			}
			pos = (float)(-num) * _pageSize[axis];
			if (pos < 0f - _overlapSize[axis])
			{
				pos = 0f - _overlapSize[axis];
			}
		}
		if (inertialScrolling)
		{
			float num5 = _tweenStart[axis];
			int num6 = ((!(num5 > 0f)) ? ((!(num5 < 0f - _overlapSize[axis])) ? Mathf.FloorToInt((0f - num5) / _pageSize[axis]) : (Mathf.CeilToInt(_contentSize[axis] / _pageSize[axis]) - 1)) : 0);
			int num7 = Mathf.FloorToInt((0f - _containerPos[axis]) / _pageSize[axis]);
			if (Mathf.Abs(num - num7) > 1 && Mathf.Abs(num6 - num7) <= 1)
			{
				num = ((num <= num7) ? (num7 - 1) : (num7 + 1));
				pos = (float)(-num) * _pageSize[axis];
			}
		}
		return pos;
	}

	private Vector2 UpdateTargetAndDuration(Vector2 orignPos)
	{
		Vector2 zero = Vector2.zero;
		zero.x = UpdateTargetAndDuration(orignPos.x, 0);
		zero.y = UpdateTargetAndDuration(orignPos.y, 1);
		return zero;
	}

	private float UpdateTargetAndDuration(float pos, int axis)
	{
		float num = _velocity[axis];
		float num2 = 0f;
		if (pos > 0f)
		{
			pos = 0f;
		}
		else if (pos < 0f - _overlapSize[axis])
		{
			pos = 0f - _overlapSize[axis];
		}
		else
		{
			float num3 = Mathf.Abs(num) * _velocityScale;
			if (Stage.touchScreen)
			{
				num3 *= 1136f / (float)Mathf.Max(Screen.width, Screen.height);
			}
			float num4 = 0f;
			if (_pageMode || !Stage.touchScreen)
			{
				if (num3 > 500f)
				{
					num4 = Mathf.Pow((num3 - 500f) / 500f, 2f);
				}
			}
			else if (num3 > 1000f)
			{
				num4 = Mathf.Pow((num3 - 1000f) / 1000f, 2f);
			}
			if (num4 != 0f)
			{
				if (num4 > 1f)
				{
					num4 = 1f;
				}
				num3 *= num4;
				num *= num4;
				_velocity[axis] = num;
				num2 = Mathf.Log(60f / num3, _decelerationRate) / 60f;
				float num5 = (int)(num * num2 * 0.4f);
				pos += num5;
			}
		}
		if (num2 < TWEEN_TIME_DEFAULT)
		{
			num2 = TWEEN_TIME_DEFAULT;
		}
		_tweenDuration[axis] = num2;
		return pos;
	}

	private void FixDuration(int axis, float oldChange)
	{
		if (_tweenChange[axis] != 0f && !(Mathf.Abs(_tweenChange[axis]) >= Mathf.Abs(oldChange)))
		{
			float num = Mathf.Abs(_tweenChange[axis] / oldChange) * _tweenDuration[axis];
			if (num < TWEEN_TIME_DEFAULT)
			{
				num = TWEEN_TIME_DEFAULT;
			}
			_tweenDuration[axis] = num;
		}
	}

	private void StartTween(int type)
	{
		_tweenTime.Set(0f, 0f);
		_tweening = type;
		Timers.inst.AddUpdate(_tweenUpdateDelegate);
		UpdateScrollBarVisible();
	}

	private void KillTween()
	{
		if (_tweening == 1)
		{
			_container.xy = _tweenStart + _tweenChange;
			_onScroll.Call();
		}
		_tweening = 0;
		Timers.inst.Remove(_tweenUpdateDelegate);
		UpdateScrollBarVisible();
		_onScrollEnd.Call();
	}

	private void CheckRefreshBar()
	{
		if (_header == null && _footer == null)
		{
			return;
		}
		float num = _container.xy[_refreshBarAxis];
		if (_header != null)
		{
			if (num > 0f)
			{
				if (_header.displayObject.parent == null)
				{
					_maskContainer.AddChildAt(_header.displayObject, 0);
				}
				Vector2 size = _header.size;
				size[_refreshBarAxis] = num;
				_header.size = size;
			}
			else if (_header.displayObject.parent != null)
			{
				_maskContainer.RemoveChild(_header.displayObject);
			}
		}
		if (_footer == null)
		{
			return;
		}
		float num2 = _overlapSize[_refreshBarAxis];
		if (num < 0f - num2 || (num2 == 0f && _footerLockedSize > 0))
		{
			if (_footer.displayObject.parent == null)
			{
				_maskContainer.AddChildAt(_footer.displayObject, 0);
			}
			Vector2 xy = _footer.xy;
			if (num2 > 0f)
			{
				xy[_refreshBarAxis] = num + _contentSize[_refreshBarAxis];
			}
			else
			{
				xy[_refreshBarAxis] = Mathf.Max(Mathf.Min(num + _viewSize[_refreshBarAxis], _viewSize[_refreshBarAxis] - (float)_footerLockedSize), _viewSize[_refreshBarAxis] - _contentSize[_refreshBarAxis]);
			}
			_footer.xy = xy;
			xy = _footer.size;
			if (num2 > 0f)
			{
				xy[_refreshBarAxis] = 0f - num2 - num;
			}
			else
			{
				xy[_refreshBarAxis] = _viewSize[_refreshBarAxis] - _footer.xy[_refreshBarAxis];
			}
			_footer.size = xy;
		}
		else if (_footer.displayObject.parent != null)
		{
			_maskContainer.RemoveChild(_footer.displayObject);
		}
	}

	private void TweenUpdate(object param)
	{
		if (_owner.displayObject == null || _owner.displayObject.isDisposed)
		{
			Timers.inst.Remove(_tweenUpdateDelegate);
			return;
		}
		float num = RunTween(0);
		float num2 = RunTween(1);
		_container.SetXY(num, num2);
		if (_tweening == 2)
		{
			if (_overlapSize.x > 0f)
			{
				_xPos = Mathf.Clamp(0f - num, 0f, _overlapSize.x);
			}
			if (_overlapSize.y > 0f)
			{
				_yPos = Mathf.Clamp(0f - num2, 0f, _overlapSize.y);
			}
			if (_pageMode)
			{
				UpdatePageController();
			}
		}
		if (_tweenChange.x == 0f && _tweenChange.y == 0f)
		{
			_tweening = 0;
			Timers.inst.Remove(_tweenUpdateDelegate);
			LoopCheckingCurrent();
			UpdateScrollBarPos();
			UpdateScrollBarVisible();
			_onScroll.Call();
			_onScrollEnd.Call();
			UpdateMouseWheelTrigger();
		}
		else
		{
			UpdateScrollBarPos();
			_onScroll.Call();
		}
	}

	private float RunTween(int axis)
	{
		float num;
		if (_tweenChange[axis] != 0f)
		{
			_tweenTime[axis] += Time.unscaledDeltaTime;
			if (_tweenTime[axis] >= _tweenDuration[axis])
			{
				num = _tweenStart[axis] + _tweenChange[axis];
				_tweenChange[axis] = 0f;
			}
			else
			{
				float num2 = EaseFunc(_tweenTime[axis], _tweenDuration[axis]);
				num = _tweenStart[axis] + (float)(int)(_tweenChange[axis] * num2);
			}
			float num3 = 0f;
			float num4 = 0f - _overlapSize[axis];
			if (_headerLockedSize > 0 && _refreshBarAxis == axis)
			{
				num3 = _headerLockedSize;
			}
			if (_footerLockedSize > 0 && _refreshBarAxis == axis)
			{
				float num5 = _overlapSize[_refreshBarAxis];
				num5 = ((num5 != 0f) ? (num5 + (float)_footerLockedSize) : Mathf.Max(_contentSize[_refreshBarAxis] + (float)_footerLockedSize - _viewSize[_refreshBarAxis], 0f));
				num4 = 0f - num5;
			}
			if (_tweening == 2 && _bouncebackEffect)
			{
				if ((num > 20f + num3 && _tweenChange[axis] > 0f) || (num > num3 && _tweenChange[axis] == 0f))
				{
					_tweenTime[axis] = 0f;
					_tweenDuration[axis] = TWEEN_TIME_DEFAULT;
					_tweenChange[axis] = 0f - num + num3;
					_tweenStart[axis] = num;
				}
				else if ((num < num4 - 20f && _tweenChange[axis] < 0f) || (num < num4 && _tweenChange[axis] == 0f))
				{
					_tweenTime[axis] = 0f;
					_tweenDuration[axis] = TWEEN_TIME_DEFAULT;
					_tweenChange[axis] = num4 - num;
					_tweenStart[axis] = num;
				}
			}
			else if (num > num3)
			{
				num = num3;
				_tweenChange[axis] = 0f;
			}
			else if (num < num4)
			{
				num = num4;
				_tweenChange[axis] = 0f;
			}
		}
		else
		{
			num = _container.xy[axis];
		}
		return num;
	}

	private static float EaseFunc(float t, float d)
	{
		return (t = t / d - 1f) * t * t + 1f;
	}
}
