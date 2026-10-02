using System;
using System.Text;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class DisplayObject : EventDispatcher
{
	protected internal class PaintingInfo
	{
		public Action captureDelegate;

		public Margin extend;

		public float scale;

		public int flag;
	}

	[Flags]
	protected internal enum Flags
	{
		Disposed = 1,
		UserGameObject = 2,
		TouchDisabled = 4,
		OutlineChanged = 8,
		UpdatingSize = 0x10,
		WidthChanged = 0x20,
		HeightChanged = 0x40,
		PixelPerfect = 0x80,
		LayerSet = 0x100,
		LayerFromParent = 0x200,
		NotFocusable = 0x400,
		TabStop = 0x800,
		TabStopChildren = 0x1000,
		FairyBatching = 0x2000,
		BatchingRequested = 0x4000,
		BatchingRoot = 0x8000,
		SkipBatching = 0x10000,
		CacheAsBitmap = 0x20000,
		GameObjectDisposed = 0x40000,
		DisposedWarning = 0x80000
	}

	public string name;

	public GObject gOwner;

	public uint id;

	private bool _visible;

	private bool _touchable;

	private Vector2 _pivot;

	private Vector3 _pivotOffset;

	private Vector3 _rotation;

	private Vector2 _skew;

	private int _renderingOrder;

	private float _alpha;

	private bool _grayed;

	private BlendMode _blendMode;

	private IFilter _filter;

	private Transform _home;

	private string _cursor;

	private bool _perspective;

	private int _focalLength;

	private Vector3 _pixelPerfectAdjustment;

	private int _checkPixelPerfect;

	private EventListener _onClick;

	private EventListener _onRightClick;

	private EventListener _onTouchBegin;

	private EventListener _onTouchMove;

	private EventListener _onTouchEnd;

	private EventListener _onRollOver;

	private EventListener _onRollOut;

	private EventListener _onMouseWheel;

	private EventListener _onAddedToStage;

	private EventListener _onRemovedFromStage;

	private EventListener _onKeyDown;

	private EventListener _onClickLink;

	private EventListener _onFocusIn;

	private EventListener _onFocusOut;

	protected internal int _paintingMode;

	protected internal PaintingInfo _paintingInfo;

	protected Rect _contentRect;

	protected NGraphics.VertexMatrix _vertexMatrix;

	protected internal Flags _flags;

	protected internal float[] _batchingBounds;

	internal static uint _gInstanceCounter;

	internal static HideFlags hideFlags;

	public Container parent { get; private set; }

	public GameObject gameObject { get; protected set; }

	public Transform cachedTransform { get; protected set; }

	public NGraphics graphics { get; protected set; }

	public NGraphics paintingGraphics { get; protected set; }

	public EventListener onClick => _onClick ?? (_onClick = new EventListener(this, "onClick"));

	public EventListener onRightClick => _onRightClick ?? (_onRightClick = new EventListener(this, "onRightClick"));

	public EventListener onTouchBegin => _onTouchBegin ?? (_onTouchBegin = new EventListener(this, "onTouchBegin"));

	public EventListener onTouchMove => _onTouchMove ?? (_onTouchMove = new EventListener(this, "onTouchMove"));

	public EventListener onTouchEnd => _onTouchEnd ?? (_onTouchEnd = new EventListener(this, "onTouchEnd"));

	public EventListener onRollOver => _onRollOver ?? (_onRollOver = new EventListener(this, "onRollOver"));

	public EventListener onRollOut => _onRollOut ?? (_onRollOut = new EventListener(this, "onRollOut"));

	public EventListener onMouseWheel => _onMouseWheel ?? (_onMouseWheel = new EventListener(this, "onMouseWheel"));

	public EventListener onAddedToStage => _onAddedToStage ?? (_onAddedToStage = new EventListener(this, "onAddedToStage"));

	public EventListener onRemovedFromStage => _onRemovedFromStage ?? (_onRemovedFromStage = new EventListener(this, "onRemovedFromStage"));

	public EventListener onKeyDown => _onKeyDown ?? (_onKeyDown = new EventListener(this, "onKeyDown"));

	public EventListener onClickLink => _onClickLink ?? (_onClickLink = new EventListener(this, "onClickLink"));

	public EventListener onFocusIn => _onFocusIn ?? (_onFocusIn = new EventListener(this, "onFocusIn"));

	public EventListener onFocusOut => _onFocusOut ?? (_onFocusOut = new EventListener(this, "onFocusOut"));

	public float alpha
	{
		get
		{
			return _alpha;
		}
		set
		{
			_alpha = value;
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
			_grayed = value;
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
			if (_visible == value)
			{
				return;
			}
			_visible = value;
			_flags |= Flags.OutlineChanged;
			if (parent != null && _visible)
			{
				gameObject.SetActive(value: true);
				InvalidateBatchingState();
				if (this is Container)
				{
					((Container)this).InvalidateBatchingState(childrenChanged: true);
				}
			}
			else
			{
				gameObject.SetActive(value: false);
			}
		}
	}

	public float x
	{
		get
		{
			return cachedTransform.localPosition.x;
		}
		set
		{
			SetPosition(value, 0f - cachedTransform.localPosition.y, cachedTransform.localPosition.z);
		}
	}

	public float y
	{
		get
		{
			return 0f - cachedTransform.localPosition.y;
		}
		set
		{
			SetPosition(cachedTransform.localPosition.x, value, cachedTransform.localPosition.z);
		}
	}

	public float z
	{
		get
		{
			return cachedTransform.localPosition.z;
		}
		set
		{
			SetPosition(cachedTransform.localPosition.x, 0f - cachedTransform.localPosition.y, value);
		}
	}

	public Vector2 xy
	{
		get
		{
			return new Vector2(x, y);
		}
		set
		{
			SetPosition(value.x, value.y, cachedTransform.localPosition.z);
		}
	}

	public Vector3 position
	{
		get
		{
			return new Vector3(x, y, z);
		}
		set
		{
			SetPosition(value.x, value.y, value.z);
		}
	}

	public bool pixelPerfect
	{
		get
		{
			return (_flags & Flags.PixelPerfect) != 0;
		}
		set
		{
			if (value)
			{
				_flags |= Flags.PixelPerfect;
			}
			else
			{
				_flags &= ~Flags.PixelPerfect;
			}
		}
	}

	public float width
	{
		get
		{
			EnsureSizeCorrect();
			return _contentRect.width;
		}
		set
		{
			if (!Mathf.Approximately(value, _contentRect.width))
			{
				_contentRect.width = value;
				_flags |= Flags.WidthChanged;
				_flags &= ~Flags.HeightChanged;
				OnSizeChanged();
			}
		}
	}

	public float height
	{
		get
		{
			EnsureSizeCorrect();
			return _contentRect.height;
		}
		set
		{
			if (!Mathf.Approximately(value, _contentRect.height))
			{
				_contentRect.height = value;
				_flags &= ~Flags.WidthChanged;
				_flags |= Flags.HeightChanged;
				OnSizeChanged();
			}
		}
	}

	public Vector2 size
	{
		get
		{
			EnsureSizeCorrect();
			return _contentRect.size;
		}
		set
		{
			SetSize(value.x, value.y);
		}
	}

	public float scaleX
	{
		get
		{
			return cachedTransform.localScale.x;
		}
		set
		{
			Vector3 localScale = cachedTransform.localScale;
			localScale.x = (localScale.z = ValidateScale(value));
			cachedTransform.localScale = localScale;
			_flags |= Flags.OutlineChanged;
			ApplyPivot();
		}
	}

	public float scaleY
	{
		get
		{
			return cachedTransform.localScale.y;
		}
		set
		{
			Vector3 localScale = cachedTransform.localScale;
			localScale.y = ValidateScale(value);
			cachedTransform.localScale = localScale;
			_flags |= Flags.OutlineChanged;
			ApplyPivot();
		}
	}

	public Vector2 scale
	{
		get
		{
			return cachedTransform.localScale;
		}
		set
		{
			SetScale(value.x, value.y);
		}
	}

	public float rotation
	{
		get
		{
			return 0f - _rotation.z;
		}
		set
		{
			_rotation.z = 0f - value;
			_flags |= Flags.OutlineChanged;
			if (_perspective)
			{
				UpdateTransformMatrix();
				return;
			}
			cachedTransform.localEulerAngles = _rotation;
			ApplyPivot();
		}
	}

	public float rotationX
	{
		get
		{
			return _rotation.x;
		}
		set
		{
			_rotation.x = value;
			_flags |= Flags.OutlineChanged;
			if (_perspective)
			{
				UpdateTransformMatrix();
				return;
			}
			cachedTransform.localEulerAngles = _rotation;
			ApplyPivot();
		}
	}

	public float rotationY
	{
		get
		{
			return _rotation.y;
		}
		set
		{
			_rotation.y = value;
			_flags |= Flags.OutlineChanged;
			if (_perspective)
			{
				UpdateTransformMatrix();
				return;
			}
			cachedTransform.localEulerAngles = _rotation;
			ApplyPivot();
		}
	}

	public Vector2 skew
	{
		get
		{
			return _skew;
		}
		set
		{
			_skew = value;
			_flags |= Flags.OutlineChanged;
			if (Application.isPlaying)
			{
				UpdateTransformMatrix();
			}
		}
	}

	public bool perspective
	{
		get
		{
			return _perspective;
		}
		set
		{
			if (_perspective != value)
			{
				_perspective = value;
				if (_perspective)
				{
					cachedTransform.localEulerAngles = Vector3.zero;
				}
				else
				{
					cachedTransform.localEulerAngles = _rotation;
				}
				ApplyPivot();
				UpdateTransformMatrix();
			}
		}
	}

	public int focalLength
	{
		get
		{
			return _focalLength;
		}
		set
		{
			if (value <= 0)
			{
				value = 1;
			}
			_focalLength = value;
			if (_vertexMatrix != null)
			{
				UpdateTransformMatrix();
			}
		}
	}

	public Vector2 pivot
	{
		get
		{
			return _pivot;
		}
		set
		{
			Vector3 vector = new Vector2((value.x - _pivot.x) * _contentRect.width, (_pivot.y - value.y) * _contentRect.height);
			Vector3 pivotOffset = _pivotOffset;
			_pivot = value;
			UpdatePivotOffset();
			Vector3 localPosition = cachedTransform.localPosition;
			localPosition += pivotOffset - _pivotOffset + vector;
			cachedTransform.localPosition = localPosition;
			_flags |= Flags.OutlineChanged;
		}
	}

	public Vector3 location
	{
		get
		{
			Vector3 result = position;
			result.x += _pivotOffset.x;
			result.y -= _pivotOffset.y;
			result.z += _pivotOffset.z;
			return result;
		}
		set
		{
			SetPosition(value.x - _pivotOffset.x, value.y + _pivotOffset.y, value.z - _pivotOffset.z);
		}
	}

	public virtual Material material
	{
		get
		{
			if (graphics != null)
			{
				return graphics.material;
			}
			return null;
		}
		set
		{
			if (graphics != null)
			{
				graphics.material = value;
			}
		}
	}

	public virtual string shader
	{
		get
		{
			if (graphics != null)
			{
				return graphics.shader;
			}
			return null;
		}
		set
		{
			if (graphics != null)
			{
				graphics.shader = value;
			}
		}
	}

	public virtual int renderingOrder
	{
		get
		{
			return _renderingOrder;
		}
		set
		{
			if ((_flags & Flags.GameObjectDisposed) != 0)
			{
				DisplayDisposedWarning();
				return;
			}
			_renderingOrder = value;
			if (graphics != null)
			{
				graphics.sortingOrder = value;
			}
			if (_paintingMode > 0)
			{
				paintingGraphics.sortingOrder = value;
			}
		}
	}

	public int layer
	{
		get
		{
			if (_paintingMode > 0)
			{
				return paintingGraphics.gameObject.layer;
			}
			return gameObject.layer;
		}
		set
		{
			SetLayer(value, fromParent: false);
		}
	}

	public bool focusable
	{
		get
		{
			return (_flags & Flags.NotFocusable) == 0;
		}
		set
		{
			if (value)
			{
				_flags &= ~Flags.NotFocusable;
			}
			else
			{
				_flags |= Flags.NotFocusable;
			}
		}
	}

	public bool tabStop
	{
		get
		{
			return (_flags & Flags.TabStop) != 0;
		}
		set
		{
			if (value)
			{
				_flags |= Flags.TabStop;
			}
			else
			{
				_flags &= ~Flags.TabStop;
			}
		}
	}

	public bool focused
	{
		get
		{
			if (Stage.inst.focus != this)
			{
				if (this is Container)
				{
					return ((Container)this).IsAncestorOf(Stage.inst.focus);
				}
				return false;
			}
			return true;
		}
	}

	public string cursor
	{
		get
		{
			return _cursor;
		}
		set
		{
			_cursor = value;
			if (Application.isPlaying && (this == Stage.inst.touchTarget || (this is Container && ((Container)this).IsAncestorOf(Stage.inst.touchTarget))))
			{
				Stage.inst._ChangeCursor(_cursor);
			}
		}
	}

	public bool isDisposed
	{
		get
		{
			if ((_flags & Flags.Disposed) == 0)
			{
				return gameObject == null;
			}
			return true;
		}
	}

	public Container topmost
	{
		get
		{
			DisplayObject displayObject = this;
			while (displayObject.parent != null)
			{
				displayObject = displayObject.parent;
			}
			return displayObject as Container;
		}
	}

	public Stage stage => topmost as Stage;

	public Container worldSpaceContainer
	{
		get
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Invalid comparison between Unknown and I4
			Container result = null;
			DisplayObject displayObject = this;
			while (displayObject.parent != null)
			{
				if (displayObject is Container && (int)((Container)displayObject).renderMode == 2)
				{
					result = (Container)displayObject;
					break;
				}
				displayObject = displayObject.parent;
			}
			return result;
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
				if (this is Container && ((Container)this).hitArea is ColliderHitTest colliderHitTest)
				{
					colliderHitTest.collider.enabled = value;
				}
			}
		}
	}

	public bool touchDisabled => (_flags & Flags.TouchDisabled) != 0;

	public bool paintingMode => _paintingMode > 0;

	public bool cacheAsBitmap
	{
		get
		{
			return (_flags & Flags.CacheAsBitmap) != 0;
		}
		set
		{
			if (value)
			{
				_flags |= Flags.CacheAsBitmap;
				EnterPaintingMode(8, null, UIContentScaler.scaleFactor);
			}
			else
			{
				_flags &= ~Flags.CacheAsBitmap;
				LeavePaintingMode(8);
			}
		}
	}

	public IFilter filter
	{
		get
		{
			return _filter;
		}
		set
		{
			if (Application.isPlaying && value != _filter)
			{
				if (_filter != null)
				{
					_filter.Dispose();
				}
				if (value != null && value.target != null)
				{
					value.target.filter = null;
				}
				_filter = value;
				if (_filter != null)
				{
					_filter.target = this;
				}
			}
		}
	}

	public BlendMode blendMode
	{
		get
		{
			return _blendMode;
		}
		set
		{
			_blendMode = value;
			InvalidateBatchingState();
			if (graphics == null)
			{
				if (_blendMode != BlendMode.Normal)
				{
					if (Application.isPlaying)
					{
						EnterPaintingMode(2, null);
						paintingGraphics.blendMode = _blendMode;
					}
				}
				else
				{
					LeavePaintingMode(2);
				}
			}
			else
			{
				graphics.blendMode = _blendMode;
			}
		}
	}

	public Transform home
	{
		get
		{
			return _home;
		}
		set
		{
			_home = value;
			if (value != null && cachedTransform.parent == null)
			{
				cachedTransform.SetParent(value, worldPositionStays: false);
			}
		}
	}

	public event Action onPaint;

	public DisplayObject()
	{
		id = _gInstanceCounter++;
		_alpha = 1f;
		_visible = true;
		_touchable = true;
		_blendMode = BlendMode.Normal;
		_focalLength = 2000;
		_flags |= Flags.OutlineChanged;
		if (UIConfig.makePixelPerfect)
		{
			_flags |= Flags.PixelPerfect;
		}
	}

	protected void CreateGameObject(string gameObjectName)
	{
		gameObject = new GameObject(gameObjectName);
		cachedTransform = gameObject.transform;
		if (Application.isPlaying)
		{
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.AddComponent<DisplayObjectInfo>().displayObject = this;
		}
		gameObject.hideFlags = hideFlags;
		gameObject.SetActive(value: false);
	}

	protected void SetGameObject(GameObject gameObject)
	{
		this.gameObject = gameObject;
		cachedTransform = gameObject.transform;
		_rotation = cachedTransform.localEulerAngles;
		_flags |= Flags.UserGameObject;
	}

	protected void DestroyGameObject()
	{
		if ((_flags & Flags.UserGameObject) == 0 && gameObject != null)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
			gameObject = null;
			cachedTransform = null;
		}
	}

	public void SetXY(float xv, float yv)
	{
		SetPosition(xv, yv, cachedTransform.localPosition.z);
	}

	public void SetPosition(float xv, float yv, float zv)
	{
		Vector3 vector = new Vector3
		{
			x = xv,
			y = 0f - yv,
			z = zv
		};
		if (vector != cachedTransform.localPosition)
		{
			cachedTransform.localPosition = vector;
			_flags |= Flags.OutlineChanged;
			if ((_flags & Flags.PixelPerfect) != 0)
			{
				_checkPixelPerfect = Time.frameCount;
				_pixelPerfectAdjustment = Vector3.zero;
			}
		}
	}

	public void SetSize(float wv, float hv)
	{
		if (!Mathf.Approximately(wv, _contentRect.width))
		{
			_flags |= Flags.WidthChanged;
		}
		else
		{
			_flags &= ~Flags.WidthChanged;
		}
		if (!Mathf.Approximately(hv, _contentRect.height))
		{
			_flags |= Flags.HeightChanged;
		}
		else
		{
			_flags &= ~Flags.HeightChanged;
		}
		if ((_flags & Flags.WidthChanged) != 0 || (_flags & Flags.HeightChanged) != 0)
		{
			_contentRect.width = wv;
			_contentRect.height = hv;
			OnSizeChanged();
		}
	}

	public virtual void EnsureSizeCorrect()
	{
	}

	protected virtual void OnSizeChanged()
	{
		ApplyPivot();
		if (_paintingInfo != null)
		{
			_paintingInfo.flag = 1;
		}
		if (graphics != null)
		{
			graphics.contentRect = _contentRect;
		}
		_flags |= Flags.OutlineChanged;
	}

	public void SetScale(float xv, float yv)
	{
		Vector3 localScale = default(Vector3);
		localScale.x = (localScale.z = ValidateScale(xv));
		localScale.y = ValidateScale(yv);
		cachedTransform.localScale = localScale;
		_flags |= Flags.OutlineChanged;
		ApplyPivot();
	}

	private float ValidateScale(float value)
	{
		if (value >= 0f && value < 0.001f)
		{
			value = 0.001f;
		}
		else if (value < 0f && value > -0.001f)
		{
			value = -0.001f;
		}
		return value;
	}

	private void UpdateTransformMatrix()
	{
		Matrix4x4 matrix = Matrix4x4.identity;
		if (_skew.x != 0f || _skew.y != 0f)
		{
			ToolSet.SkewMatrix(ref matrix, _skew.x, _skew.y);
		}
		if (_perspective)
		{
			matrix *= Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(_rotation), Vector3.one);
		}
		if (matrix.isIdentity)
		{
			_vertexMatrix = null;
		}
		else if (_vertexMatrix == null)
		{
			_vertexMatrix = new NGraphics.VertexMatrix();
		}
		if (_vertexMatrix != null)
		{
			_vertexMatrix.matrix = matrix;
			_vertexMatrix.cameraPos = new Vector3(_pivot.x * _contentRect.width, (0f - _pivot.y) * _contentRect.height, _focalLength);
			if (graphics == null)
			{
				EnterPaintingMode(4, null);
			}
		}
		else if (graphics == null)
		{
			LeavePaintingMode(4);
		}
		if (_paintingMode > 0)
		{
			paintingGraphics.vertexMatrix = _vertexMatrix;
			_paintingInfo.flag = 1;
		}
		else if (graphics != null)
		{
			graphics.vertexMatrix = _vertexMatrix;
		}
		_flags |= Flags.OutlineChanged;
	}

	private void UpdatePivotOffset()
	{
		float num = _pivot.x * _contentRect.width;
		float num2 = _pivot.y * _contentRect.height;
		_pivotOffset = Matrix4x4.TRS(Vector3.zero, cachedTransform.localRotation, cachedTransform.localScale).MultiplyPoint(new Vector3(num, 0f - num2, 0f));
		if (_vertexMatrix != null)
		{
			_vertexMatrix.cameraPos = new Vector3(_pivot.x * _contentRect.width, (0f - _pivot.y) * _contentRect.height, _focalLength);
		}
	}

	private void ApplyPivot()
	{
		if (_pivot.x != 0f || _pivot.y != 0f)
		{
			Vector3 pivotOffset = _pivotOffset;
			UpdatePivotOffset();
			Vector3 localPosition = cachedTransform.localPosition;
			if ((_flags & Flags.PixelPerfect) != 0)
			{
				localPosition -= _pixelPerfectAdjustment;
				_checkPixelPerfect = Time.frameCount;
				_pixelPerfectAdjustment = Vector3.zero;
			}
			localPosition += pivotOffset - _pivotOffset;
			cachedTransform.localPosition = localPosition;
			_flags |= Flags.OutlineChanged;
		}
	}

	internal bool _AcceptTab()
	{
		if (_touchable && _visible && ((_flags & Flags.TabStop) != 0 || (_flags & Flags.TabStopChildren) != 0) && (_flags & Flags.NotFocusable) == 0)
		{
			Stage.inst.SetFocus(this, byKey: true);
			return true;
		}
		return false;
	}

	internal void InternalSetParent(Container value)
	{
		if (parent != value)
		{
			if (value == null && (parent._flags & Flags.Disposed) != 0)
			{
				parent = value;
			}
			else
			{
				parent = value;
				UpdateHierarchy();
			}
			_flags |= Flags.OutlineChanged;
		}
	}

	public void EnterPaintingMode()
	{
		EnterPaintingMode(16384, null, 1f);
	}

	public void EnterPaintingMode(int requestorId, Margin? extend)
	{
		EnterPaintingMode(requestorId, extend, 1f);
	}

	public void EnterPaintingMode(int requestorId, Margin? extend, float scale)
	{
		bool num = _paintingMode == 0;
		_paintingMode |= requestorId;
		if (num)
		{
			if (_paintingInfo == null)
			{
				_paintingInfo = new PaintingInfo
				{
					captureDelegate = Capture,
					scale = 1f
				};
			}
			if (paintingGraphics == null)
			{
				if (graphics == null)
				{
					paintingGraphics = new NGraphics(this.gameObject);
				}
				else
				{
					GameObject gameObject = new GameObject(this.gameObject.name + " (Painter)");
					gameObject.layer = this.gameObject.layer;
					gameObject.transform.SetParent(cachedTransform, worldPositionStays: false);
					gameObject.hideFlags = hideFlags;
					paintingGraphics = new NGraphics(gameObject);
				}
			}
			else
			{
				paintingGraphics.enabled = true;
			}
			paintingGraphics.vertexMatrix = null;
			if (this is Container)
			{
				((Container)this).SetChildrenLayer(CaptureCamera.hiddenLayer);
				((Container)this).UpdateBatchingFlags();
			}
			else
			{
				InvalidateBatchingState();
			}
			if (graphics != null)
			{
				this.gameObject.layer = CaptureCamera.hiddenLayer;
			}
		}
		if (extend.HasValue)
		{
			_paintingInfo.extend = extend.Value;
		}
		_paintingInfo.scale = scale;
		_paintingInfo.flag = 1;
	}

	public void LeavePaintingMode(int requestorId)
	{
		if (_paintingMode == 0 || (_flags & Flags.Disposed) != 0)
		{
			return;
		}
		_paintingMode ^= requestorId;
		if (_paintingMode == 0)
		{
			paintingGraphics.enabled = false;
			if (this is Container)
			{
				((Container)this).SetChildrenLayer(layer);
				((Container)this).UpdateBatchingFlags();
			}
			else
			{
				InvalidateBatchingState();
			}
			if (graphics != null)
			{
				gameObject.layer = paintingGraphics.gameObject.layer;
			}
		}
	}

	public Texture2D GetScreenShot(Margin? extend, float scale)
	{
		EnterPaintingMode(8, null, scale);
		UpdatePainting();
		Capture();
		Texture2D texture2D;
		if (paintingGraphics.texture == null)
		{
			texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, mipChain: false, linear: true);
		}
		else
		{
			RenderTexture renderTexture = (RenderTexture)paintingGraphics.texture.nativeTexture;
			texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, mipChain: false, linear: true);
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = renderTexture;
			texture2D.ReadPixels(new Rect(0f, 0f, renderTexture.width, renderTexture.height), 0, 0);
			texture2D.Apply();
			RenderTexture.active = active;
		}
		LeavePaintingMode(8);
		return texture2D;
	}

	public virtual Rect GetBounds(DisplayObject targetSpace)
	{
		EnsureSizeCorrect();
		if (targetSpace == this)
		{
			return _contentRect;
		}
		if (targetSpace == parent && _rotation.z == 0f)
		{
			return new Rect(cachedTransform.localPosition.x, 0f - cachedTransform.localPosition.y, _contentRect.width * cachedTransform.localScale.x, _contentRect.height * cachedTransform.localScale.y);
		}
		return TransformRect(_contentRect, targetSpace);
	}

	internal DisplayObject InternalHitTest()
	{
		if (_visible && (!HitTestContext.forTouch || _touchable))
		{
			return HitTest();
		}
		return null;
	}

	internal DisplayObject InternalHitTestMask()
	{
		if (_visible)
		{
			return HitTest();
		}
		return null;
	}

	protected virtual DisplayObject HitTest()
	{
		Rect bounds = GetBounds(this);
		if (bounds.width == 0f || bounds.height == 0f)
		{
			return null;
		}
		Vector2 point = WorldToLocal(HitTestContext.worldPoint, HitTestContext.direction);
		if (bounds.Contains(point))
		{
			return this;
		}
		return null;
	}

	public Vector2 GlobalToLocal(Vector2 point)
	{
		Container container = worldSpaceContainer;
		if (container != null)
		{
			Camera renderCamera = container.GetRenderCamera();
			Vector3 pos = new Vector3
			{
				x = point.x,
				y = (float)Screen.height - point.y
			};
			Vector3 worldPoint;
			Vector3 direction;
			if (container.hitArea is MeshColliderHitTest)
			{
				Ray ray = renderCamera.ScreenPointToRay(pos);
				RaycastHit val = default(RaycastHit);
				if (!((MeshColliderHitTest)container.hitArea).collider.Raycast(ray, ref val, 100f))
				{
					return new Vector2(float.NaN, float.NaN);
				}
				point = new Vector2(((RaycastHit)(ref val)).textureCoord.x * _contentRect.width, (1f - ((RaycastHit)(ref val)).textureCoord.y) * _contentRect.height);
				worldPoint = Stage.inst.cachedTransform.TransformPoint(point.x, 0f - point.y, 0f);
				direction = Vector3.back;
			}
			else
			{
				pos.z = renderCamera.WorldToScreenPoint(cachedTransform.position).z;
				worldPoint = renderCamera.ScreenToWorldPoint(pos);
				Ray ray2 = renderCamera.ScreenPointToRay(pos);
				direction = Vector3.zero - ray2.direction;
			}
			return WorldToLocal(worldPoint, direction);
		}
		Vector3 worldPoint2 = Stage.inst.cachedTransform.TransformPoint(point.x, 0f - point.y, 0f);
		return WorldToLocal(worldPoint2, Vector3.back);
	}

	public Vector2 LocalToGlobal(Vector2 point)
	{
		Container container = worldSpaceContainer;
		Vector3 vector = cachedTransform.TransformPoint(point.x, 0f - point.y, 0f);
		if (container != null)
		{
			if (container.hitArea is MeshColliderHitTest)
			{
				return new Vector2(float.NaN, float.NaN);
			}
			Vector3 vector2 = container.GetRenderCamera().WorldToScreenPoint(vector);
			return new Vector2(vector2.x, Stage.inst.size.y - vector2.y);
		}
		point = Stage.inst.cachedTransform.InverseTransformPoint(vector);
		point.y = 0f - point.y;
		return point;
	}

	public Vector3 WorldToLocal(Vector3 worldPoint, Vector3 direction)
	{
		Vector3 vector = cachedTransform.InverseTransformPoint(worldPoint);
		if (vector.z != 0f)
		{
			direction = cachedTransform.InverseTransformDirection(direction);
			float num = Vector3.Dot(Vector3.zero - vector, Vector3.forward) / Vector3.Dot(direction, Vector3.forward);
			if (float.IsInfinity(num))
			{
				return Vector2.zero;
			}
			vector += direction * num;
		}
		else if (_vertexMatrix != null)
		{
			Vector3 cameraPos = _vertexMatrix.cameraPos;
			cameraPos.z = 0f;
			cameraPos -= _vertexMatrix.matrix.MultiplyPoint(cameraPos);
			Matrix4x4 inverse = _vertexMatrix.matrix.inverse;
			vector -= cameraPos;
			vector = inverse.MultiplyPoint(vector);
			Vector3 vector2 = inverse.MultiplyPoint(_vertexMatrix.cameraPos);
			Vector3 vector3 = vector - vector2;
			float num2 = (0f - vector2.z) / vector3.z;
			vector = vector2 + num2 * vector3;
			vector.z = 0f;
		}
		vector.y = 0f - vector.y;
		return vector;
	}

	public Vector3 LocalToWorld(Vector3 localPoint)
	{
		localPoint.y = 0f - localPoint.y;
		if (_vertexMatrix != null)
		{
			Vector3 cameraPos = _vertexMatrix.cameraPos;
			cameraPos.z = 0f;
			cameraPos -= _vertexMatrix.matrix.MultiplyPoint(cameraPos);
			localPoint = _vertexMatrix.matrix.MultiplyPoint(localPoint);
			localPoint += cameraPos;
			Vector3 cameraPos2 = _vertexMatrix.cameraPos;
			Vector3 vector = localPoint - cameraPos2;
			float num = (0f - cameraPos2.z) / vector.z;
			localPoint = cameraPos2 + num * vector;
			localPoint.z = 0f;
		}
		return cachedTransform.TransformPoint(localPoint);
	}

	public Vector2 TransformPoint(Vector2 point, DisplayObject targetSpace)
	{
		if (targetSpace == this)
		{
			return point;
		}
		point = LocalToWorld(point);
		if (targetSpace != null)
		{
			point = targetSpace.WorldToLocal(point, Vector3.back);
		}
		return point;
	}

	public Rect TransformRect(Rect rect, DisplayObject targetSpace)
	{
		if (targetSpace == this)
		{
			return rect;
		}
		if (targetSpace == parent && _rotation.z == 0f)
		{
			Vector3 localScale = cachedTransform.localScale;
			return new Rect((x + rect.x) * localScale.x, (y + rect.y) * localScale.y, rect.width * localScale.x, rect.height * localScale.y);
		}
		Vector4 vec = new Vector4(float.MaxValue, float.MaxValue, float.MinValue, float.MinValue);
		TransformRectPoint(rect.xMin, rect.yMin, targetSpace, ref vec);
		TransformRectPoint(rect.xMax, rect.yMin, targetSpace, ref vec);
		TransformRectPoint(rect.xMin, rect.yMax, targetSpace, ref vec);
		TransformRectPoint(rect.xMax, rect.yMax, targetSpace, ref vec);
		return Rect.MinMaxRect(vec.x, vec.y, vec.z, vec.w);
	}

	protected void TransformRectPoint(float px, float py, DisplayObject targetSpace, ref Vector4 vec4)
	{
		Vector2 vector = TransformPoint(new Vector2(px, py), targetSpace);
		if (vec4.x > vector.x)
		{
			vec4.x = vector.x;
		}
		if (vec4.z < vector.x)
		{
			vec4.z = vector.x;
		}
		if (vec4.y > vector.y)
		{
			vec4.y = vector.y;
		}
		if (vec4.w < vector.y)
		{
			vec4.w = vector.y;
		}
	}

	public void RemoveFromParent()
	{
		if (parent != null)
		{
			parent.RemoveChild(this);
		}
	}

	public void InvalidateBatchingState()
	{
		if (parent != null)
		{
			parent.InvalidateBatchingState(childrenChanged: true);
		}
	}

	public virtual void Update(UpdateContext context)
	{
		if (_checkPixelPerfect != 0)
		{
			if (_rotation == Vector3.zero)
			{
				Vector3 localPosition = cachedTransform.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				_pixelPerfectAdjustment = localPosition - cachedTransform.localPosition;
				if (_pixelPerfectAdjustment != Vector3.zero)
				{
					cachedTransform.localPosition = localPosition;
				}
			}
			_checkPixelPerfect = 0;
		}
		if (graphics != null)
		{
			graphics.Update(context, context.alpha * _alpha, context.grayed | _grayed);
		}
		if (_paintingMode != 0)
		{
			UpdatePainting();
			if (!(this is Container) && ((_flags & Flags.CacheAsBitmap) == 0 || _paintingInfo.flag != 2))
			{
				UpdateContext.OnEnd += _paintingInfo.captureDelegate;
			}
			paintingGraphics.Update(context, 1f, grayed: false);
		}
		if (_filter != null)
		{
			_filter.Update();
		}
		Stats.ObjectCount++;
	}

	private void UpdatePainting()
	{
		NTexture nTexture = paintingGraphics.texture;
		if (nTexture != null && nTexture.disposed)
		{
			nTexture = null;
			_paintingInfo.flag = 1;
		}
		if (_paintingInfo.flag == 1)
		{
			_paintingInfo.flag = 0;
			Margin extend = _paintingInfo.extend;
			paintingGraphics.contentRect = new Rect(-extend.left, -extend.top, _contentRect.width + (float)extend.left + (float)extend.right, _contentRect.height + (float)extend.top + (float)extend.bottom);
			int num = Mathf.RoundToInt(paintingGraphics.contentRect.width * _paintingInfo.scale);
			int num2 = Mathf.RoundToInt(paintingGraphics.contentRect.height * _paintingInfo.scale);
			if (nTexture == null || nTexture.width != num || nTexture.height != num2)
			{
				nTexture?.Dispose();
				if (num > 0 && num2 > 0)
				{
					nTexture = new NTexture(CaptureCamera.CreateRenderTexture(num, num2, UIConfig.depthSupportForPaintingMode));
					Stage.inst.MonitorTexture(nTexture);
				}
				else
				{
					nTexture = null;
				}
				paintingGraphics.texture = nTexture;
			}
		}
		if (nTexture != null)
		{
			nTexture.lastActive = Time.time;
		}
	}

	private void Capture()
	{
		if (paintingGraphics.texture != null)
		{
			CaptureCamera.Capture(offset: new Vector2(_paintingInfo.extend.left, _paintingInfo.extend.top), target: this, texture: (RenderTexture)paintingGraphics.texture.nativeTexture, contentHeight: paintingGraphics.contentRect.height);
			_paintingInfo.flag = 2;
			if (this.onPaint != null)
			{
				this.onPaint();
			}
		}
	}

	private void UpdateHierarchy()
	{
		if ((_flags & Flags.GameObjectDisposed) != 0)
		{
			return;
		}
		if ((_flags & Flags.UserGameObject) != 0)
		{
			if (gameObject != null)
			{
				if (parent != null && visible)
				{
					gameObject.SetActive(value: true);
				}
				else
				{
					gameObject.SetActive(value: false);
				}
			}
		}
		else if (parent != null)
		{
			cachedTransform.SetParent(parent.cachedTransform, worldPositionStays: false);
			if (_visible)
			{
				gameObject.SetActive(value: true);
			}
			int hiddenLayer = parent.gameObject.layer;
			if (parent._paintingMode != 0)
			{
				hiddenLayer = CaptureCamera.hiddenLayer;
			}
			SetLayer(hiddenLayer, fromParent: true);
		}
		else
		{
			if ((_flags & Flags.Disposed) != 0 || !(gameObject != null) || StageEngine.beingQuit)
			{
				return;
			}
			if (Application.isPlaying && (gOwner == null || gOwner.parent == null))
			{
				cachedTransform.SetParent(_home, worldPositionStays: false);
				if (_home == null)
				{
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
				}
			}
			gameObject.SetActive(value: false);
		}
	}

	protected virtual bool SetLayer(int value, bool fromParent)
	{
		if ((_flags & Flags.LayerSet) != 0)
		{
			if (fromParent)
			{
				return false;
			}
		}
		else if ((_flags & Flags.LayerFromParent) != 0)
		{
			if (!fromParent)
			{
				_flags |= Flags.LayerSet;
			}
		}
		else if (fromParent)
		{
			_flags |= Flags.LayerFromParent;
		}
		else
		{
			_flags |= Flags.LayerSet;
		}
		if (_paintingMode > 0)
		{
			paintingGraphics.gameObject.layer = value;
		}
		else if (gameObject.layer != value)
		{
			gameObject.layer = value;
			if (this is Container)
			{
				int numChildren = ((Container)this).numChildren;
				for (int i = 0; i < numChildren; i++)
				{
					((Container)this).GetChildAt(i).SetLayer(value, fromParent: true);
				}
			}
		}
		return true;
	}

	internal void _SetLayerDirect(int value)
	{
		if (_paintingMode > 0)
		{
			paintingGraphics.gameObject.layer = value;
		}
		else
		{
			gameObject.layer = value;
		}
	}

	public virtual void Dispose()
	{
		if ((_flags & Flags.Disposed) != 0)
		{
			return;
		}
		_flags |= Flags.Disposed;
		RemoveFromParent();
		RemoveEventListeners();
		if (graphics != null)
		{
			graphics.Dispose();
		}
		if (_filter != null)
		{
			_filter.Dispose();
		}
		if (paintingGraphics != null)
		{
			if (paintingGraphics.texture != null)
			{
				paintingGraphics.texture.Dispose();
			}
			paintingGraphics.Dispose();
			if (paintingGraphics.gameObject != gameObject)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(paintingGraphics.gameObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(paintingGraphics.gameObject);
				}
			}
		}
		DestroyGameObject();
	}

	internal void DisplayDisposedWarning()
	{
		if ((_flags & Flags.DisposedWarning) != 0)
		{
			return;
		}
		_flags |= Flags.DisposedWarning;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("DisplayObject is still in use but GameObject was disposed. (");
		if (gOwner != null)
		{
			stringBuilder.Append("type=").Append(gOwner.GetType().Name).Append(", x=")
				.Append(gOwner.x)
				.Append(", y=")
				.Append(gOwner.y)
				.Append(", name=")
				.Append(gOwner.name);
			if (gOwner.packageItem != null)
			{
				stringBuilder.Append(", res=" + gOwner.packageItem.name);
			}
		}
		else
		{
			stringBuilder.Append("id=").Append(id).Append(", type=")
				.Append(GetType().Name)
				.Append(", name=")
				.Append(name);
		}
		stringBuilder.Append(")");
		Debug.LogError(stringBuilder.ToString());
	}
}
