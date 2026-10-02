using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class Container : DisplayObject
{
	private struct DescendantsEnumerator : IEnumerator<DisplayObject>, IEnumerator, IDisposable
	{
		private Container _root;

		private Container _com;

		private DisplayObject _current;

		private int _index;

		private bool _forward;

		public DisplayObject Current => _current;

		object IEnumerator.Current => _current;

		public DescendantsEnumerator(Container root, bool backward)
		{
			_root = root;
			_com = _root;
			_current = null;
			_forward = !backward;
			if (_forward)
			{
				_index = 0;
			}
			else
			{
				_index = _com._children.Count - 1;
			}
		}

		public bool MoveNext()
		{
			if (_forward)
			{
				if (_index >= _com._children.Count)
				{
					if (_com == _root)
					{
						_current = null;
						return false;
					}
					_current = _com;
					_com = _com.parent;
					_index = _com.GetChildIndex(_current) + 1;
					return true;
				}
				DisplayObject displayObject = _com._children[_index];
				if (displayObject is Container)
				{
					_com = (Container)displayObject;
					_index = 0;
					return MoveNext();
				}
				_index++;
				_current = displayObject;
				return true;
			}
			if (_index < 0)
			{
				if (_com == _root)
				{
					_current = null;
					return false;
				}
				_current = _com;
				_com = _com.parent;
				_index = _com.GetChildIndex(_current) - 1;
				return true;
			}
			DisplayObject displayObject2 = _com._children[_index];
			if (displayObject2 is Container)
			{
				_com = (Container)displayObject2;
				_index = _com._children.Count - 1;
				return MoveNext();
			}
			_index--;
			_current = displayObject2;
			return true;
		}

		public void Reset()
		{
			_com = _root;
			_current = null;
			_index = 0;
		}

		public void Dispose()
		{
		}
	}

	public RenderMode renderMode;

	public Camera renderCamera;

	public bool opaque;

	public Vector4? clipSoftness;

	public IHitTest hitArea;

	public bool touchChildren;

	public bool reversedMask;

	private List<DisplayObject> _children;

	private DisplayObject _mask;

	private Rect? _clipRect;

	private List<DisplayObject> _descendants;

	internal int _panelOrder;

	internal DisplayObject _lastFocus;

	public int numChildren => _children.Count;

	public Rect? clipRect
	{
		get
		{
			return _clipRect;
		}
		set
		{
			if (_clipRect != value)
			{
				_clipRect = value;
				UpdateBatchingFlags();
			}
		}
	}

	public DisplayObject mask
	{
		get
		{
			return _mask;
		}
		set
		{
			if (_mask != value)
			{
				_mask = value;
				UpdateBatchingFlags();
			}
		}
	}

	public bool fairyBatching
	{
		get
		{
			return (_flags & Flags.FairyBatching) != 0;
		}
		set
		{
			if ((_flags & Flags.FairyBatching) != 0 != value)
			{
				if (value)
				{
					_flags |= Flags.FairyBatching;
				}
				else
				{
					_flags &= ~Flags.FairyBatching;
				}
				UpdateBatchingFlags();
			}
		}
	}

	public bool tabStopChildren
	{
		get
		{
			return (_flags & Flags.TabStopChildren) != 0;
		}
		set
		{
			if (value)
			{
				_flags |= Flags.TabStopChildren;
			}
			else
			{
				_flags &= ~Flags.TabStopChildren;
			}
		}
	}

	public event Action onUpdate;

	public Container()
	{
		CreateGameObject("Container");
		Init();
	}

	public Container(string gameObjectName)
	{
		CreateGameObject(gameObjectName);
		Init();
	}

	public Container(GameObject attachTarget)
	{
		SetGameObject(attachTarget);
		Init();
	}

	private void Init()
	{
		_children = new List<DisplayObject>();
		touchChildren = true;
	}

	public DisplayObject AddChild(DisplayObject child)
	{
		AddChildAt(child, _children.Count);
		return child;
	}

	public DisplayObject AddChildAt(DisplayObject child, int index)
	{
		int count = _children.Count;
		if (index >= 0 && index <= count)
		{
			if (child.parent == this)
			{
				SetChildIndex(child, index);
			}
			else
			{
				child.RemoveFromParent();
				if (index == count)
				{
					_children.Add(child);
				}
				else
				{
					_children.Insert(index, child);
				}
				child.InternalSetParent(this);
				if (base.stage != null)
				{
					if (child is Container)
					{
						child.BroadcastEvent("onAddedToStage", null);
					}
					else
					{
						child.DispatchEvent("onAddedToStage", null);
					}
				}
				InvalidateBatchingState(childrenChanged: true);
			}
			return child;
		}
		throw new Exception("Invalid child index");
	}

	public bool Contains(DisplayObject child)
	{
		return _children.Contains(child);
	}

	public DisplayObject GetChildAt(int index)
	{
		return _children[index];
	}

	public DisplayObject GetChild(string name)
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

	public DisplayObject[] GetChildren()
	{
		return _children.ToArray();
	}

	public int GetChildIndex(DisplayObject child)
	{
		return _children.IndexOf(child);
	}

	public DisplayObject RemoveChild(DisplayObject child)
	{
		return RemoveChild(child, dispose: false);
	}

	public DisplayObject RemoveChild(DisplayObject child, bool dispose)
	{
		if (child.parent != this)
		{
			throw new Exception("obj is not a child");
		}
		int num = _children.IndexOf(child);
		if (num >= 0)
		{
			return RemoveChildAt(num, dispose);
		}
		return null;
	}

	public DisplayObject RemoveChildAt(int index)
	{
		return RemoveChildAt(index, dispose: false);
	}

	public DisplayObject RemoveChildAt(int index, bool dispose)
	{
		if (index >= 0 && index < _children.Count)
		{
			DisplayObject displayObject = _children[index];
			if (base.stage != null && (displayObject._flags & Flags.Disposed) == 0)
			{
				if (displayObject is Container)
				{
					displayObject.BroadcastEvent("onRemovedFromStage", null);
					if (displayObject == Stage.inst.focus || ((Container)displayObject).IsAncestorOf(Stage.inst.focus))
					{
						Stage.inst._OnFocusRemoving(this);
					}
				}
				else
				{
					displayObject.DispatchEvent("onRemovedFromStage", null);
					if (displayObject == Stage.inst.focus)
					{
						Stage.inst._OnFocusRemoving(this);
					}
				}
			}
			_children.Remove(displayObject);
			InvalidateBatchingState(childrenChanged: true);
			if (!dispose)
			{
				displayObject.InternalSetParent(null);
			}
			else
			{
				displayObject.Dispose();
			}
			return displayObject;
		}
		throw new Exception("Invalid child index");
	}

	public void RemoveChildren()
	{
		RemoveChildren(0, int.MaxValue, dispose: false);
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

	public void SetChildIndex(DisplayObject child, int index)
	{
		int num = _children.IndexOf(child);
		if (num != index)
		{
			if (num == -1)
			{
				throw new ArgumentException("Not a child of this container");
			}
			_children.RemoveAt(num);
			if (index >= _children.Count)
			{
				_children.Add(child);
			}
			else
			{
				_children.Insert(index, child);
			}
			InvalidateBatchingState(childrenChanged: true);
		}
	}

	public void SwapChildren(DisplayObject child1, DisplayObject child2)
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
		DisplayObject value = _children[index1];
		DisplayObject value2 = _children[index2];
		_children[index1] = value2;
		_children[index2] = value;
		InvalidateBatchingState(childrenChanged: true);
	}

	public void ChangeChildrenOrder(IList<int> indice, IList<DisplayObject> objs)
	{
		int count = objs.Count;
		for (int i = 0; i < count; i++)
		{
			DisplayObject displayObject = objs[i];
			if (displayObject.parent != this)
			{
				throw new Exception("Not a child of this container");
			}
			_children[indice[i]] = displayObject;
		}
		InvalidateBatchingState(childrenChanged: true);
	}

	public IEnumerator<DisplayObject> GetDescendants(bool backward)
	{
		return new DescendantsEnumerator(this, backward);
	}

	public void CreateGraphics()
	{
		if (base.graphics == null)
		{
			base.graphics = new NGraphics(base.gameObject);
			base.graphics.texture = NTexture.Empty;
		}
	}

	public override Rect GetBounds(DisplayObject targetSpace)
	{
		if (_clipRect.HasValue)
		{
			return TransformRect(_clipRect.Value, targetSpace);
		}
		int count = _children.Count;
		switch (count)
		{
		case 0:
		{
			Vector2 vector = TransformPoint(Vector2.zero, targetSpace);
			return Rect.MinMaxRect(vector.x, vector.y, 0f, 0f);
		}
		case 1:
			return _children[0].GetBounds(targetSpace);
		default:
		{
			float num = float.MaxValue;
			float num2 = float.MinValue;
			float num3 = float.MaxValue;
			float num4 = float.MinValue;
			for (int i = 0; i < count; i++)
			{
				Rect bounds = _children[i].GetBounds(targetSpace);
				num = ((num < bounds.xMin) ? num : bounds.xMin);
				num2 = ((num2 > bounds.xMax) ? num2 : bounds.xMax);
				num3 = ((num3 < bounds.yMin) ? num3 : bounds.yMin);
				num4 = ((num4 > bounds.yMax) ? num4 : bounds.yMax);
			}
			return Rect.MinMaxRect(num, num3, num2, num4);
		}
		}
	}

	public Camera GetRenderCamera()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)renderMode == 0)
		{
			return StageCamera.main;
		}
		Camera camera = renderCamera;
		if (camera == null)
		{
			if (HitTestContext.cachedMainCamera != null)
			{
				camera = HitTestContext.cachedMainCamera;
			}
			else
			{
				camera = Camera.main;
				if (camera == null)
				{
					camera = StageCamera.main;
				}
			}
		}
		return camera;
	}

	public DisplayObject HitTest(Vector2 stagePoint, bool forTouch)
	{
		if (StageCamera.main == null)
		{
			if (this is Stage)
			{
				return this;
			}
			return null;
		}
		HitTestContext.screenPoint = new Vector3(stagePoint.x, (float)Screen.height - stagePoint.y, 0f);
		if (Display.displays.Length > 1)
		{
			Vector3 vector = Display.RelativeMouseAt(HitTestContext.screenPoint);
			if (vector != Vector3.zero)
			{
				HitTestContext.screenPoint = vector;
			}
		}
		HitTestContext.worldPoint = StageCamera.main.ScreenToWorldPoint(HitTestContext.screenPoint);
		HitTestContext.direction = Vector3.back;
		HitTestContext.forTouch = forTouch;
		HitTestContext.camera = StageCamera.main;
		DisplayObject displayObject = HitTest();
		if (displayObject != null)
		{
			return displayObject;
		}
		if (this is Stage)
		{
			return this;
		}
		return null;
	}

	protected override DisplayObject HitTest()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Invalid comparison between Unknown and I4
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Invalid comparison between Unknown and I4
		if ((_flags & Flags.UserGameObject) != 0 && !base.gameObject.activeInHierarchy)
		{
			return null;
		}
		if (base.cachedTransform.localScale.x == 0f || base.cachedTransform.localScale.y == 0f)
		{
			return null;
		}
		Camera camera = HitTestContext.camera;
		Vector3 worldPoint = HitTestContext.worldPoint;
		Vector3 direction = HitTestContext.direction;
		if ((int)renderMode != 0 || (_flags & Flags.UserGameObject) != 0)
		{
			Camera camera2 = GetRenderCamera();
			if ((float)camera2.targetDisplay != HitTestContext.screenPoint.z)
			{
				return null;
			}
			HitTestContext.camera = camera2;
			if ((int)renderMode == 2)
			{
				Vector3 pos = HitTestContext.camera.WorldToScreenPoint(base.cachedTransform.position);
				pos.x = HitTestContext.screenPoint.x;
				pos.y = HitTestContext.screenPoint.y;
				HitTestContext.worldPoint = HitTestContext.camera.ScreenToWorldPoint(pos);
				Ray ray = HitTestContext.camera.ScreenPointToRay(pos);
				HitTestContext.direction = Vector3.zero - ray.direction;
			}
			else if ((int)renderMode == 1)
			{
				HitTestContext.worldPoint = HitTestContext.camera.ScreenToWorldPoint(HitTestContext.screenPoint);
			}
		}
		else if ((float)HitTestContext.camera.targetDisplay != HitTestContext.screenPoint.z && !(this is Stage))
		{
			return null;
		}
		DisplayObject result = HitTest_Container();
		HitTestContext.camera = camera;
		HitTestContext.worldPoint = worldPoint;
		HitTestContext.direction = direction;
		return result;
	}

	private DisplayObject HitTest_Container()
	{
		Vector2 vector = WorldToLocal(HitTestContext.worldPoint, HitTestContext.direction);
		if (_vertexMatrix != null)
		{
			HitTestContext.worldPoint = base.cachedTransform.TransformPoint(new Vector2(vector.x, 0f - vector.y));
		}
		if (hitArea != null)
		{
			if (!hitArea.HitTest(_contentRect, vector))
			{
				return null;
			}
			if (hitArea is MeshColliderHitTest)
			{
				vector = ((MeshColliderHitTest)hitArea).lastHit;
			}
		}
		else if (_clipRect.HasValue && !_clipRect.Value.Contains(vector))
		{
			return null;
		}
		if (_mask != null)
		{
			DisplayObject displayObject = _mask.InternalHitTestMask();
			if ((!reversedMask && displayObject == null) || (reversedMask && displayObject != null))
			{
				return null;
			}
		}
		DisplayObject displayObject2 = null;
		if (touchChildren)
		{
			for (int num = _children.Count - 1; num >= 0; num--)
			{
				DisplayObject displayObject3 = _children[num];
				if ((displayObject3._flags & Flags.GameObjectDisposed) != 0)
				{
					displayObject3.DisplayDisposedWarning();
				}
				else if (displayObject3 != _mask && (displayObject3._flags & Flags.TouchDisabled) == 0)
				{
					displayObject2 = displayObject3.InternalHitTest();
					if (displayObject2 != null)
					{
						break;
					}
				}
			}
		}
		if (displayObject2 == null && opaque && (hitArea != null || _contentRect.Contains(vector)))
		{
			displayObject2 = this;
		}
		return displayObject2;
	}

	public bool IsAncestorOf(DisplayObject obj)
	{
		if (obj == null)
		{
			return false;
		}
		for (Container container = obj.parent; container != null; container = container.parent)
		{
			if (container == this)
			{
				return true;
			}
		}
		return false;
	}

	internal void UpdateBatchingFlags()
	{
		bool num = (_flags & Flags.BatchingRoot) != 0;
		bool flag = (_flags & Flags.FairyBatching) != 0 || _clipRect.HasValue || _mask != null || _paintingMode > 0;
		if (flag)
		{
			_flags |= Flags.BatchingRoot;
		}
		else
		{
			_flags &= ~Flags.BatchingRoot;
		}
		if (num != flag)
		{
			if (flag)
			{
				_flags |= Flags.BatchingRequested;
			}
			else if (_descendants != null)
			{
				_descendants.Clear();
			}
			InvalidateBatchingState();
		}
	}

	public void InvalidateBatchingState(bool childrenChanged)
	{
		if (childrenChanged && (_flags & Flags.BatchingRoot) != 0)
		{
			_flags |= Flags.BatchingRequested;
			return;
		}
		for (Container container = base.parent; container != null; container = container.parent)
		{
			if ((container._flags & Flags.BatchingRoot) != 0)
			{
				container._flags |= Flags.BatchingRequested;
				break;
			}
		}
	}

	public void SetChildrenLayer(int value)
	{
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			DisplayObject displayObject = _children[i];
			displayObject._SetLayerDirect(value);
			if (displayObject is Container && displayObject._paintingMode == 0)
			{
				((Container)displayObject).SetChildrenLayer(value);
			}
		}
	}

	public override void Update(UpdateContext context)
	{
		if ((_flags & Flags.UserGameObject) != 0 && !base.gameObject.activeInHierarchy)
		{
			return;
		}
		base.Update(context);
		if (_paintingMode != 0)
		{
			if ((_flags & Flags.CacheAsBitmap) != 0 && _paintingInfo.flag == 2)
			{
				if (this.onUpdate != null)
				{
					this.onUpdate();
				}
				return;
			}
			context.EnterPaintingMode();
		}
		if (_mask != null)
		{
			context.EnterClipping(id, reversedMask);
			if (_mask.graphics != null)
			{
				_mask.graphics._PreUpdateMask(context, _mask.id);
			}
		}
		else if (_clipRect.HasValue)
		{
			context.EnterClipping(id, TransformRect(_clipRect.Value, null), clipSoftness);
		}
		float num = context.alpha;
		context.alpha *= base.alpha;
		bool flag = context.grayed;
		context.grayed = context.grayed || base.grayed;
		if ((_flags & Flags.FairyBatching) != 0)
		{
			context.batchingDepth++;
		}
		if (context.batchingDepth > 0)
		{
			int count = _children.Count;
			for (int i = 0; i < count; i++)
			{
				DisplayObject displayObject = _children[i];
				if ((displayObject._flags & Flags.GameObjectDisposed) != 0)
				{
					displayObject.DisplayDisposedWarning();
				}
				else if (displayObject.visible)
				{
					displayObject.Update(context);
				}
			}
		}
		else
		{
			if (_mask != null)
			{
				_mask.renderingOrder = context.renderingOrder++;
			}
			int count2 = _children.Count;
			for (int j = 0; j < count2; j++)
			{
				DisplayObject displayObject2 = _children[j];
				if ((displayObject2._flags & Flags.GameObjectDisposed) != 0)
				{
					displayObject2.DisplayDisposedWarning();
				}
				else if (displayObject2.visible)
				{
					if (displayObject2.graphics == null || displayObject2.graphics._maskFlag != 1)
					{
						displayObject2.renderingOrder = context.renderingOrder++;
					}
					displayObject2.Update(context);
				}
			}
			if (_mask != null && _mask.graphics != null)
			{
				_mask.graphics._SetStencilEraserOrder(context.renderingOrder++);
			}
		}
		if ((_flags & Flags.FairyBatching) != 0)
		{
			if (context.batchingDepth == 1)
			{
				SetRenderingOrder(context);
			}
			context.batchingDepth--;
		}
		context.alpha = num;
		context.grayed = flag;
		if (_clipRect.HasValue || _mask != null)
		{
			context.LeaveClipping();
		}
		if (_paintingMode != 0)
		{
			context.LeavePaintingMode();
			UpdateContext.OnEnd += _paintingInfo.captureDelegate;
		}
		if (this.onUpdate != null)
		{
			this.onUpdate();
		}
	}

	private void SetRenderingOrder(UpdateContext context)
	{
		if ((_flags & Flags.BatchingRequested) != 0)
		{
			DoFairyBatching();
		}
		if (_mask != null)
		{
			_mask.renderingOrder = context.renderingOrder++;
		}
		int count = _descendants.Count;
		for (int i = 0; i < count; i++)
		{
			DisplayObject displayObject = _descendants[i];
			if (displayObject.graphics == null || displayObject.graphics._maskFlag != 1)
			{
				displayObject.renderingOrder = context.renderingOrder++;
			}
			if ((displayObject._flags & Flags.BatchingRoot) != 0)
			{
				((Container)displayObject).SetRenderingOrder(context);
			}
		}
		if (_mask != null && _mask.graphics != null)
		{
			_mask.graphics._SetStencilEraserOrder(context.renderingOrder++);
		}
	}

	private void DoFairyBatching()
	{
		_flags &= ~Flags.BatchingRequested;
		if (_descendants == null)
		{
			_descendants = new List<DisplayObject>();
		}
		else
		{
			_descendants.Clear();
		}
		CollectChildren(this, outlineChanged: false);
		int count = _descendants.Count;
		for (int i = 0; i < count; i++)
		{
			DisplayObject displayObject = _descendants[i];
			float[] batchingBounds = displayObject._batchingBounds;
			object obj = displayObject.material;
			if (obj == null || (displayObject._flags & Flags.SkipBatching) != 0)
			{
				continue;
			}
			int num = -1;
			object obj2 = null;
			int num2 = i;
			for (int num3 = i - 1; num3 >= 0; num3--)
			{
				DisplayObject displayObject2 = _descendants[num3];
				if ((displayObject2._flags & Flags.SkipBatching) != 0)
				{
					break;
				}
				object obj3 = displayObject2.material;
				if (obj3 != null)
				{
					if (obj2 != obj3)
					{
						obj2 = obj3;
						num2 = num3 + 1;
					}
					if (obj == obj3)
					{
						num = num2;
					}
				}
				if (((batchingBounds[0] > displayObject2._batchingBounds[0]) ? batchingBounds[0] : displayObject2._batchingBounds[0]) <= ((batchingBounds[2] < displayObject2._batchingBounds[2]) ? batchingBounds[2] : displayObject2._batchingBounds[2]) && ((batchingBounds[1] > displayObject2._batchingBounds[1]) ? batchingBounds[1] : displayObject2._batchingBounds[1]) <= ((batchingBounds[3] < displayObject2._batchingBounds[3]) ? batchingBounds[3] : displayObject2._batchingBounds[3]))
				{
					if (num == -1)
					{
						num = num2;
					}
					break;
				}
			}
			if (num != -1 && i != num)
			{
				_descendants.RemoveAt(i);
				_descendants.Insert(num, displayObject);
			}
		}
	}

	private void CollectChildren(Container initiator, bool outlineChanged)
	{
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			DisplayObject displayObject = _children[i];
			if (!displayObject.visible)
			{
				continue;
			}
			if (displayObject._batchingBounds == null)
			{
				displayObject._batchingBounds = new float[4];
			}
			if (displayObject is Container)
			{
				Container container = (Container)displayObject;
				if ((container._flags & Flags.BatchingRoot) != 0)
				{
					initiator._descendants.Add(container);
					if (outlineChanged || (container._flags & Flags.OutlineChanged) != 0)
					{
						Rect bounds = container.GetBounds(initiator);
						container._batchingBounds[0] = bounds.xMin;
						container._batchingBounds[1] = bounds.yMin;
						container._batchingBounds[2] = bounds.xMax;
						container._batchingBounds[3] = bounds.yMax;
					}
					if ((container._flags & Flags.BatchingRequested) != 0)
					{
						container.DoFairyBatching();
					}
				}
				else
				{
					container.CollectChildren(initiator, outlineChanged || (container._flags & Flags.OutlineChanged) != 0);
				}
			}
			else if (displayObject != initiator._mask)
			{
				if (outlineChanged || (displayObject._flags & Flags.OutlineChanged) != 0)
				{
					Rect bounds2 = displayObject.GetBounds(initiator);
					displayObject._batchingBounds[0] = bounds2.xMin;
					displayObject._batchingBounds[1] = bounds2.yMin;
					displayObject._batchingBounds[2] = bounds2.xMax;
					displayObject._batchingBounds[3] = bounds2.yMax;
				}
				initiator._descendants.Add(displayObject);
			}
			displayObject._flags &= ~Flags.OutlineChanged;
		}
	}

	public override void Dispose()
	{
		if ((_flags & Flags.Disposed) == 0)
		{
			base.Dispose();
			for (int num = _children.Count - 1; num >= 0; num--)
			{
				DisplayObject displayObject = _children[num];
				displayObject.InternalSetParent(null);
				displayObject.Dispose();
			}
		}
	}
}
