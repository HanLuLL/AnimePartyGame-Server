using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FairyGUI;

public class Stage : Container
{
	private class CursorDef
	{
		public Texture2D texture;

		public Vector2 hotspot;
	}

	public Action<int> playSoundCallback;

	private DisplayObject _touchTarget;

	private DisplayObject _focused;

	private InputTextField _lastInput;

	private bool _IMEComposite;

	private UpdateContext _updateContext;

	private List<DisplayObject> _rollOutChain;

	private List<DisplayObject> _rollOverChain;

	private TouchInfo[] _touches;

	private int _touchCount;

	private Vector2 _touchPosition;

	private int _frameGotHitTarget;

	private int _frameGotTouchPosition;

	private bool _customInput;

	private Vector2 _customInputPos;

	private bool _customInputButtonDown;

	private List<NTexture> _toCollectTextures = new List<NTexture>();

	private EventListener _onStageResized;

	private List<DisplayObject> _focusOutChain;

	private List<DisplayObject> _focusInChain;

	private List<Container> _focusHistory;

	private Container _nextFocus;

	private Dictionary<string, CursorDef> _cursors;

	private string _currentCursor;

	private static bool _touchScreen;

	internal static int _clickTestThreshold;

	private static IKeyboard _keyboard;

	private static Stage _inst;

	private static List<DisplayObject> sTempList1;

	private static List<int> sTempList2;

	private static Dictionary<uint, int> sTempDict;

	[Obsolete("Use size.y")]
	public int stageHeight => (int)_contentRect.height;

	[Obsolete("Use size.x")]
	public int stageWidth => (int)_contentRect.width;

	public float soundVolume { get; set; }

	public static Stage inst
	{
		get
		{
			if (_inst == null)
			{
				Instantiate();
			}
			return _inst;
		}
	}

	public static bool touchScreen
	{
		get
		{
			return _touchScreen;
		}
		set
		{
			_touchScreen = value;
			if (_touchScreen)
			{
				_keyboard = new TouchScreenKeyboard();
				keyboardInput = true;
				_clickTestThreshold = 50;
			}
			else
			{
				_keyboard = null;
				keyboardInput = false;
				inst.ResetInputState();
				_clickTestThreshold = 10;
			}
		}
	}

	public static bool keyboardInput { get; set; }

	public static bool isTouchOnUI
	{
		get
		{
			if (_inst != null)
			{
				return _inst.touchTarget != null;
			}
			return false;
		}
	}

	public static float devicePixelRatio { get; set; }

	public EventListener onStageResized => _onStageResized ?? (_onStageResized = new EventListener(this, "onStageResized"));

	public DisplayObject touchTarget
	{
		get
		{
			if (_frameGotHitTarget != Time.frameCount)
			{
				GetHitTarget();
			}
			if (_touchTarget == this)
			{
				return null;
			}
			return _touchTarget;
		}
	}

	public DisplayObject focus
	{
		get
		{
			if (_focused != null && _focused.isDisposed)
			{
				_focused = null;
			}
			return _focused;
		}
		set
		{
			SetFocus(value);
		}
	}

	public Vector2 touchPosition
	{
		get
		{
			UpdateTouchPosition();
			return _touchPosition;
		}
	}

	public int touchCount => _touchCount;

	public IKeyboard keyboard
	{
		get
		{
			return _keyboard;
		}
		set
		{
			_keyboard = value;
		}
	}

	public string activeCursor => _currentCursor;

	public event Action beforeUpdate;

	public event Action afterUpdate;

	public void PlayOneShotSound(int audioEventID)
	{
		playSoundCallback?.Invoke(audioEventID);
	}

	public static void Instantiate()
	{
		if (_inst == null)
		{
			_inst = new Stage();
			GRoot._inst = new GRoot();
			GRoot._inst.ApplyContentScaleFactor();
			_inst.AddChild(GRoot._inst.displayObject);
			StageCamera.CheckMainCamera();
		}
	}

	public Stage()
	{
		_inst = this;
		soundVolume = 1f;
		_updateContext = new UpdateContext();
		_frameGotHitTarget = -1;
		_touches = new TouchInfo[5];
		for (int i = 0; i < _touches.Length; i++)
		{
			_touches[i] = new TouchInfo();
		}
		bool flag = Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor;
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || flag)
		{
			touchScreen = false;
		}
		else
		{
			touchScreen = Input.touchSupported && SystemInfo.deviceType != DeviceType.Desktop;
		}
		devicePixelRatio = ((!flag || !(Screen.dpi > 96f)) ? 1 : 2);
		_rollOutChain = new List<DisplayObject>();
		_rollOverChain = new List<DisplayObject>();
		_focusOutChain = new List<DisplayObject>();
		_focusInChain = new List<DisplayObject>();
		_focusHistory = new List<Container>();
		_cursors = new Dictionary<string, CursorDef>();
		SetSize(Screen.width, Screen.height);
		base.cachedTransform.localScale = new Vector3(StageCamera.DefaultUnitsPerPixel, StageCamera.DefaultUnitsPerPixel, StageCamera.DefaultUnitsPerPixel);
		StageEngine stageEngine = UnityEngine.Object.FindObjectOfType<StageEngine>();
		if (stageEngine != null)
		{
			UnityEngine.Object.Destroy(stageEngine.gameObject);
		}
		base.gameObject.name = "Stage";
		base.gameObject.layer = LayerMask.NameToLayer("UI");
		base.gameObject.AddComponent<StageEngine>();
		base.gameObject.AddComponent<UIContentScaler>();
		base.gameObject.SetActive(value: true);
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Timers.inst.Add(5f, 0, RunTextureCollector);
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

	private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!(scene.name == "Empty"))
		{
			StageCamera.CheckMainCamera();
		}
	}

	public override void Dispose()
	{
		base.Dispose();
		Timers.inst.Remove(RunTextureCollector);
		SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
	}

	public void SetFocus(DisplayObject newFocus, bool byKey = false)
	{
		if (newFocus == this)
		{
			newFocus = null;
		}
		_nextFocus = null;
		if (_focused == newFocus)
		{
			return;
		}
		Container container = null;
		for (DisplayObject displayObject = newFocus; displayObject != null; displayObject = displayObject.parent)
		{
			if (!displayObject.focusable)
			{
				return;
			}
			if (displayObject is Container && ((Container)displayObject).tabStopChildren && container == null)
			{
				container = displayObject as Container;
			}
		}
		DisplayObject displayObject2 = _focused;
		_focused = newFocus;
		if (container != null)
		{
			container._lastFocus = _focused;
			int num = _focusHistory.IndexOf(container);
			if (num != -1)
			{
				if (num < _focusHistory.Count - 1)
				{
					_focusHistory.RemoveRange(num + 1, _focusHistory.Count - num - 1);
				}
			}
			else
			{
				_focusHistory.Add(container);
				if (_focusHistory.Count > 10)
				{
					_focusHistory.RemoveAt(0);
				}
			}
		}
		_focusInChain.Clear();
		_focusOutChain.Clear();
		for (DisplayObject displayObject = displayObject2; displayObject != null; displayObject = displayObject.parent)
		{
			if (displayObject.focusable)
			{
				_focusOutChain.Add(displayObject);
			}
		}
		for (DisplayObject displayObject = _focused; displayObject != null; displayObject = displayObject.parent)
		{
			int num2 = _focusOutChain.IndexOf(displayObject);
			if (num2 != -1)
			{
				_focusOutChain.RemoveRange(num2, _focusOutChain.Count - num2);
				break;
			}
			if (displayObject.focusable)
			{
				_focusInChain.Add(displayObject);
			}
		}
		int count = _focusOutChain.Count;
		if (count > 0)
		{
			for (int num2 = 0; num2 < count; num2++)
			{
				DisplayObject displayObject = _focusOutChain[num2];
				if (displayObject.stage != null)
				{
					displayObject.DispatchEvent("onFocusOut", null);
					if (_focused != newFocus)
					{
						return;
					}
				}
			}
			_focusOutChain.Clear();
		}
		count = _focusInChain.Count;
		if (count > 0)
		{
			for (int num2 = 0; num2 < count; num2++)
			{
				DisplayObject displayObject = _focusInChain[num2];
				if (displayObject.stage != null)
				{
					displayObject.DispatchEvent("onFocusIn", byKey ? "key" : null);
					if (_focused != newFocus)
					{
						return;
					}
				}
			}
			_focusInChain.Clear();
		}
		if (_focused is InputTextField)
		{
			_lastInput = (InputTextField)_focused;
		}
	}

	internal void _OnFocusRemoving(Container sender)
	{
		_nextFocus = sender;
		if (_focusHistory.Count > 0)
		{
			int num = _focusHistory.Count - 1;
			DisplayObject displayObject = _focusHistory[num];
			DisplayObject displayObject2 = _focused;
			while (displayObject2 != null && displayObject2 != sender)
			{
				if (displayObject2 is Container && ((Container)displayObject2).tabStopChildren && displayObject2 == displayObject)
				{
					num--;
					if (num < 0)
					{
						break;
					}
					displayObject = _focusHistory[num];
				}
				displayObject2 = displayObject2.parent;
			}
			if (num != _focusHistory.Count - 1)
			{
				_focusHistory.RemoveRange(num + 1, _focusHistory.Count - num - 1);
				if (_focusHistory.Count > 0)
				{
					_nextFocus = _focusHistory[_focusHistory.Count - 1];
				}
			}
		}
		if (_focused is InputTextField)
		{
			_lastInput = null;
		}
		_focused = null;
	}

	public void DoKeyNavigate(bool backward)
	{
		Container container = null;
		for (DisplayObject displayObject = _focused; displayObject != null; displayObject = displayObject.parent)
		{
			if (displayObject is Container && ((Container)displayObject).tabStopChildren)
			{
				container = displayObject as Container;
				break;
			}
		}
		if (container == null)
		{
			container = this;
		}
		IEnumerator<DisplayObject> descendants = container.GetDescendants(backward);
		bool flag = _focused == null;
		DisplayObject displayObject2 = ((_focused != null) ? _focused.parent : null);
		while (descendants.MoveNext())
		{
			DisplayObject current = descendants.Current;
			if (flag)
			{
				if (current == displayObject2)
				{
					displayObject2 = displayObject2.parent;
				}
				else if (current._AcceptTab())
				{
					return;
				}
			}
			else if (current == _focused)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			return;
		}
		descendants.Reset();
		while (descendants.MoveNext())
		{
			DisplayObject current2 = descendants.Current;
			if (current2 != _focused)
			{
				if (current2 == displayObject2)
				{
					displayObject2 = displayObject2.parent;
				}
				else if (current2._AcceptTab())
				{
					break;
				}
				continue;
			}
			break;
		}
	}

	public Vector2 GetTouchPosition(int touchId)
	{
		UpdateTouchPosition();
		if (touchId < 0)
		{
			return _touchPosition;
		}
		for (int i = 0; i < 5; i++)
		{
			TouchInfo touchInfo = _touches[i];
			if (touchInfo.touchId == touchId)
			{
				return new Vector2(touchInfo.x, touchInfo.y);
			}
		}
		return _touchPosition;
	}

	public DisplayObject GetTouchTarget(int touchId)
	{
		if (_frameGotHitTarget != Time.frameCount)
		{
			GetHitTarget();
		}
		for (int i = 0; i < 5; i++)
		{
			TouchInfo touchInfo = _touches[i];
			if (touchInfo.touchId == touchId)
			{
				if (touchInfo.target == this)
				{
					return null;
				}
				return touchInfo.target;
			}
		}
		return null;
	}

	public int[] GetAllTouch(int[] result)
	{
		if (result == null)
		{
			result = new int[_touchCount];
		}
		int num = 0;
		for (int i = 0; i < 5; i++)
		{
			TouchInfo touchInfo = _touches[i];
			if (touchInfo.touchId != -1)
			{
				result[num++] = touchInfo.touchId;
				if (num >= result.Length)
				{
					break;
				}
			}
		}
		return result;
	}

	public void ResetInputState()
	{
		for (int i = 0; i < 5; i++)
		{
			_touches[i].Reset();
		}
		if (!touchScreen)
		{
			_touches[0].touchId = 0;
		}
		_touchCount = 0;
	}

	public void CancelClick(int touchId)
	{
		for (int i = 0; i < 5; i++)
		{
			TouchInfo touchInfo = _touches[i];
			if (touchInfo.touchId == touchId)
			{
				touchInfo.clickCancelled = true;
			}
		}
	}

	public void OpenKeyboard(string text, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder, int keyboardType, bool hideInput)
	{
		if (_keyboard != null)
		{
			_keyboard.Open(text, autocorrection, multiline, secure, alert, textPlaceholder, keyboardType, hideInput);
		}
	}

	public void CloseKeyboard()
	{
		if (_keyboard != null)
		{
			_keyboard.Close();
		}
	}

	public void InputString(string value)
	{
		if (_lastInput != null)
		{
			_lastInput.ReplaceSelection(value);
		}
	}

	public void SetCustomInput(Vector2 screenPos, bool buttonDown)
	{
		_customInput = true;
		_customInputButtonDown = buttonDown;
		_customInputPos = screenPos;
		_frameGotHitTarget = 0;
	}

	public void SetCustomInput(Vector2 screenPos, bool buttonDown, bool buttonUp)
	{
		_customInput = true;
		if (buttonDown)
		{
			_customInputButtonDown = true;
		}
		else if (buttonUp)
		{
			_customInputButtonDown = false;
		}
		_customInputPos = screenPos;
		_frameGotHitTarget = 0;
	}

	public void SetCustomInput(ref RaycastHit hit, bool buttonDown)
	{
		Vector2 screenPos = HitTestContext.cachedMainCamera.WorldToScreenPoint(((RaycastHit)(ref hit)).point);
		HitTestContext.CacheRaycastHit(HitTestContext.cachedMainCamera, ref hit);
		SetCustomInput(screenPos, buttonDown);
	}

	public void SetCustomInput(ref RaycastHit hit, bool buttonDown, bool buttonUp)
	{
		Vector2 screenPos = HitTestContext.cachedMainCamera.WorldToScreenPoint(((RaycastHit)(ref hit)).point);
		HitTestContext.CacheRaycastHit(HitTestContext.cachedMainCamera, ref hit);
		SetCustomInput(screenPos, buttonDown, buttonUp);
	}

	public void ForceUpdate()
	{
		_updateContext.Begin();
		Update(_updateContext);
		_updateContext.End();
	}

	internal void InternalUpdate()
	{
		HandleEvents();
		if (_nextFocus != null)
		{
			if (_nextFocus.stage != null)
			{
				if (_nextFocus.tabStopChildren)
				{
					if (_nextFocus._lastFocus != null && _nextFocus.IsAncestorOf(_nextFocus._lastFocus))
					{
						SetFocus(_nextFocus._lastFocus);
					}
					else
					{
						SetFocus(_nextFocus);
					}
				}
				else
				{
					SetFocus(_nextFocus);
				}
			}
			_nextFocus = null;
		}
		if (this.beforeUpdate != null)
		{
			this.beforeUpdate();
		}
		_updateContext.Begin();
		Update(_updateContext);
		_updateContext.End();
		if (BaseFont.textRebuildFlag)
		{
			_updateContext.Begin();
			Update(_updateContext);
			_updateContext.End();
			BaseFont.textRebuildFlag = false;
		}
		if (this.afterUpdate != null)
		{
			this.afterUpdate();
		}
	}

	private void GetHitTarget()
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Invalid comparison between Unknown and I4
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		if (_frameGotHitTarget == Time.frameCount)
		{
			return;
		}
		_frameGotHitTarget = Time.frameCount;
		if (_customInput)
		{
			Vector2 customInputPos = _customInputPos;
			customInputPos.y = _contentRect.height - customInputPos.y;
			TouchInfo obj = _touches[0];
			_touchTarget = HitTest(customInputPos, forTouch: true);
			obj.target = _touchTarget;
		}
		else if (touchScreen)
		{
			_touchTarget = null;
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				Vector2 stagePoint = ((Touch)(ref touch)).position;
				stagePoint.y = _contentRect.height - stagePoint.y;
				TouchInfo touchInfo = null;
				TouchInfo touchInfo2 = null;
				for (int j = 0; j < 5; j++)
				{
					if (_touches[j].touchId == ((Touch)(ref touch)).fingerId)
					{
						touchInfo = _touches[j];
						break;
					}
					if (_touches[j].touchId == -1)
					{
						touchInfo2 = _touches[j];
					}
				}
				if (touchInfo == null)
				{
					touchInfo = touchInfo2;
					if (touchInfo == null || (int)((Touch)(ref touch)).phase != 0)
					{
						continue;
					}
					touchInfo.touchId = ((Touch)(ref touch)).fingerId;
				}
				if ((int)((Touch)(ref touch)).phase == 2)
				{
					_touchTarget = touchInfo.target;
					continue;
				}
				_touchTarget = HitTest(stagePoint, forTouch: true);
				touchInfo.target = _touchTarget;
			}
		}
		else
		{
			Vector2 stagePoint2 = Input.mousePosition;
			stagePoint2.y = (float)Screen.height - stagePoint2.y;
			TouchInfo obj2 = _touches[0];
			if (stagePoint2.x < 0f || stagePoint2.y < 0f)
			{
				_touchTarget = this;
			}
			else
			{
				_touchTarget = HitTest(stagePoint2, forTouch: true);
			}
			obj2.target = _touchTarget;
		}
		HitTestContext.ClearRaycastHitCache();
	}

	internal void HandleScreenSizeChanged(int screenWidth, int screenHeight, float unitsPerPixel)
	{
		SetSize(screenWidth, screenHeight);
		base.cachedTransform.localScale = new Vector3(unitsPerPixel, unitsPerPixel, unitsPerPixel);
		if (!DispatchEvent("onStageResized", null))
		{
			base.gameObject.GetComponent<UIContentScaler>().ApplyChange();
			GRoot.inst.ApplyContentScaleFactor();
		}
	}

	internal void HandleGUIEvents(Event evt)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Invalid comparison between Unknown and I4
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Invalid comparison between Unknown and I4
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if ((int)evt.rawType == 4)
		{
			if (_IMEComposite && Input.compositionString.Length == 0)
			{
				_IMEComposite = false;
				if (evt.keyCode != KeyCode.None)
				{
					return;
				}
			}
			TouchInfo touchInfo = _touches[0];
			touchInfo.keyCode = evt.keyCode;
			touchInfo.modifiers = evt.modifiers;
			touchInfo.character = evt.character;
			touchInfo.UpdateEvent();
			DisplayObject displayObject = focus;
			if (displayObject != null)
			{
				displayObject.BubbleEvent("onKeyDown", touchInfo.evt);
			}
			else
			{
				DispatchEvent("onKeyDown", touchInfo.evt);
			}
		}
		else if ((int)evt.rawType == 5)
		{
			TouchInfo touchInfo2 = _touches[0];
			touchInfo2.keyCode = evt.keyCode;
			touchInfo2.modifiers = evt.modifiers;
			touchInfo2.character = evt.character;
			touchInfo2.UpdateEvent();
			DisplayObject displayObject2 = focus;
			if (displayObject2 != null)
			{
				displayObject2.BubbleEvent("onKeyUp", touchInfo2.evt);
			}
			else
			{
				DispatchEvent("onKeyUp", touchInfo2.evt);
			}
		}
		else if ((int)evt.type == 6 && _touchTarget != null)
		{
			TouchInfo touchInfo3 = _touches[0];
			touchInfo3.mouseWheelDelta = evt.delta.y;
			touchInfo3.UpdateEvent();
			_touchTarget.BubbleEvent("onMouseWheel", touchInfo3.evt);
			touchInfo3.mouseWheelDelta = 0f;
		}
	}

	private void HandleEvents()
	{
		GetHitTarget();
		UpdateTouchPosition();
		if (_customInput)
		{
			HandleCustomInput();
			_customInput = false;
		}
		else if (touchScreen)
		{
			HandleTouchEvents();
		}
		else
		{
			HandleMouseEvents();
		}
		if (_focused is InputTextField)
		{
			HandleTextInput();
		}
	}

	private void UpdateTouchPosition()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (_frameGotTouchPosition == Time.frameCount)
		{
			return;
		}
		_frameGotTouchPosition = Time.frameCount;
		if (_customInput)
		{
			_touchPosition = _customInputPos;
			_touchPosition.y = _contentRect.height - _touchPosition.y;
		}
		else if (touchScreen)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				_touchPosition = ((Touch)(ref touch)).position;
				_touchPosition.y = _contentRect.height - _touchPosition.y;
			}
		}
		else
		{
			Vector2 vector = Input.mousePosition;
			if (vector.x >= 0f && vector.y >= 0f)
			{
				vector.y = _contentRect.height - vector.y;
				_touchPosition = vector;
			}
		}
	}

	private void HandleTextInput()
	{
		_IMEComposite = Input.compositionString.Length > 0;
		InputTextField inputTextField = (InputTextField)_focused;
		if (!inputTextField.editable)
		{
			return;
		}
		if (keyboardInput)
		{
			if (!inputTextField.keyboardInput || _keyboard == null)
			{
				return;
			}
			string input = _keyboard.GetInput();
			if (input != null)
			{
				if (_keyboard.supportsCaret)
				{
					inputTextField.ReplaceSelection(input);
				}
				else
				{
					inputTextField.ReplaceText(input);
				}
			}
			if (_keyboard.done)
			{
				SetFocus(null);
			}
		}
		else
		{
			inputTextField.CheckComposition();
		}
	}

	private void HandleCustomInput()
	{
		Vector2 customInputPos = _customInputPos;
		customInputPos.y = _contentRect.height - customInputPos.y;
		TouchInfo touchInfo = _touches[0];
		if (touchInfo.x != customInputPos.x || touchInfo.y != customInputPos.y)
		{
			touchInfo.x = customInputPos.x;
			touchInfo.y = customInputPos.y;
			touchInfo.Move();
		}
		if (touchInfo.lastRollOver != touchInfo.target)
		{
			HandleRollOver(touchInfo);
		}
		if (_customInputButtonDown)
		{
			if (!touchInfo.began)
			{
				_touchCount = 1;
				touchInfo.Begin();
				touchInfo.button = 0;
				SetFocus(touchInfo.target);
				touchInfo.UpdateEvent();
				touchInfo.target.BubbleEvent("onTouchBegin", touchInfo.evt);
			}
		}
		else if (touchInfo.began)
		{
			_touchCount = 0;
			touchInfo.End();
			DisplayObject displayObject = touchInfo.ClickTest();
			if (displayObject != null)
			{
				touchInfo.UpdateEvent();
				displayObject.BubbleEvent("onClick", touchInfo.evt);
			}
			touchInfo.button = -1;
		}
	}

	private void HandleMouseEvents()
	{
		TouchInfo touchInfo = _touches[0];
		if (touchInfo.x != _touchPosition.x || touchInfo.y != _touchPosition.y)
		{
			touchInfo.x = _touchPosition.x;
			touchInfo.y = _touchPosition.y;
			touchInfo.Move();
		}
		if (touchInfo.lastRollOver != touchInfo.target)
		{
			HandleRollOver(touchInfo);
		}
		if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && !touchInfo.began)
		{
			_touchCount = 1;
			touchInfo.Begin();
			touchInfo.button = (Input.GetMouseButtonDown(2) ? 2 : (Input.GetMouseButtonDown(1) ? 1 : 0));
			SetFocus(touchInfo.target);
			touchInfo.UpdateEvent();
			touchInfo.target.BubbleEvent("onTouchBegin", touchInfo.evt);
		}
		if ((!Input.GetMouseButtonUp(0) && !Input.GetMouseButtonUp(1) && !Input.GetMouseButtonUp(2)) || !touchInfo.began)
		{
			return;
		}
		_touchCount = 0;
		touchInfo.End();
		DisplayObject displayObject = touchInfo.ClickTest();
		if (displayObject != null)
		{
			touchInfo.UpdateEvent();
			if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
			{
				displayObject.BubbleEvent("onRightClick", touchInfo.evt);
			}
			else
			{
				displayObject.BubbleEvent("onClick", touchInfo.evt);
			}
		}
		touchInfo.button = -1;
	}

	private void HandleTouchEvents()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Invalid comparison between Unknown and I4
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Invalid comparison between Unknown and I4
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Invalid comparison between Unknown and I4
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Invalid comparison between Unknown and I4
		int num = Input.touchCount;
		for (int i = 0; i < num; i++)
		{
			Touch touch = Input.GetTouch(i);
			if ((int)((Touch)(ref touch)).phase == 2)
			{
				continue;
			}
			Vector2 vector = ((Touch)(ref touch)).position;
			vector.y = _contentRect.height - vector.y;
			TouchInfo touchInfo = null;
			for (int j = 0; j < 5; j++)
			{
				if (_touches[j].touchId == ((Touch)(ref touch)).fingerId)
				{
					touchInfo = _touches[j];
					break;
				}
			}
			if (touchInfo == null)
			{
				continue;
			}
			if (touchInfo.x != vector.x || touchInfo.y != vector.y)
			{
				touchInfo.x = vector.x;
				touchInfo.y = vector.y;
				if (touchInfo.began)
				{
					touchInfo.Move();
				}
			}
			if (touchInfo.lastRollOver != touchInfo.target)
			{
				HandleRollOver(touchInfo);
			}
			if ((int)((Touch)(ref touch)).phase == 0)
			{
				if (!touchInfo.began)
				{
					_touchCount++;
					touchInfo.Begin();
					touchInfo.button = 0;
					SetFocus(touchInfo.target);
					touchInfo.UpdateEvent();
					touchInfo.target.BubbleEvent("onTouchBegin", touchInfo.evt);
				}
			}
			else
			{
				if (((int)((Touch)(ref touch)).phase != 4 && (int)((Touch)(ref touch)).phase != 3) || !touchInfo.began)
				{
					continue;
				}
				_touchCount--;
				touchInfo.End();
				if ((int)((Touch)(ref touch)).phase != 4)
				{
					DisplayObject displayObject = touchInfo.ClickTest();
					if (displayObject != null)
					{
						touchInfo.clickCount = ((Touch)(ref touch)).tapCount;
						touchInfo.UpdateEvent();
						displayObject.BubbleEvent("onClick", touchInfo.evt);
					}
				}
				touchInfo.target = null;
				HandleRollOver(touchInfo);
				touchInfo.touchId = -1;
			}
		}
	}

	private void HandleRollOver(TouchInfo touch)
	{
		_rollOverChain.Clear();
		_rollOutChain.Clear();
		for (DisplayObject lastRollOver = touch.lastRollOver; lastRollOver != null; lastRollOver = lastRollOver.parent)
		{
			_rollOutChain.Add(lastRollOver);
		}
		touch.lastRollOver = touch.target;
		string text = base.cursor;
		if (text == null)
		{
			for (DisplayObject lastRollOver = touch.target; lastRollOver != null; lastRollOver = lastRollOver.parent)
			{
				if (lastRollOver.cursor != null && text == null)
				{
					text = lastRollOver.cursor;
				}
			}
		}
		for (DisplayObject lastRollOver = touch.target; lastRollOver != null; lastRollOver = lastRollOver.parent)
		{
			int num = _rollOutChain.IndexOf(lastRollOver);
			if (num != -1)
			{
				_rollOutChain.RemoveRange(num, _rollOutChain.Count - num);
				break;
			}
			_rollOverChain.Add(lastRollOver);
		}
		int count = _rollOutChain.Count;
		if (count > 0)
		{
			for (int num = 0; num < count; num++)
			{
				DisplayObject lastRollOver = _rollOutChain[num];
				if (lastRollOver.stage != null)
				{
					lastRollOver.DispatchEvent("onRollOut", null);
				}
			}
			_rollOutChain.Clear();
		}
		count = _rollOverChain.Count;
		if (count <= 0)
		{
			return;
		}
		for (int num = 0; num < count; num++)
		{
			DisplayObject lastRollOver = _rollOverChain[num];
			if (lastRollOver.stage != null)
			{
				lastRollOver.DispatchEvent("onRollOver", null);
			}
		}
		_rollOverChain.Clear();
	}

	public void ApplyPanelOrder(Container target)
	{
		int panelOrder = target._panelOrder;
		int num = base.numChildren;
		int i = 0;
		int num2 = -1;
		for (; i < num; i++)
		{
			DisplayObject childAt = GetChildAt(i);
			if (childAt == target)
			{
				num2 = i;
				continue;
			}
			int num3;
			if (childAt == GRoot.inst.displayObject)
			{
				num3 = 1000;
			}
			else
			{
				if (!(childAt is Container))
				{
					continue;
				}
				num3 = ((Container)childAt)._panelOrder;
			}
			if (panelOrder > num3)
			{
				continue;
			}
			if (num2 != -1)
			{
				AddChildAt(target, i - 1);
			}
			else
			{
				AddChildAt(target, i);
			}
			break;
		}
		if (i == num)
		{
			AddChild(target);
		}
	}

	public void SortWorldSpacePanelsByZOrder(int panelSortingOrder)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Invalid comparison between Unknown and I4
		if (sTempList1 == null)
		{
			sTempList1 = new List<DisplayObject>();
			sTempList2 = new List<int>();
			sTempDict = new Dictionary<uint, int>();
		}
		int num = base.numChildren;
		for (int i = 0; i < num; i++)
		{
			if (GetChildAt(i) is Container container && (int)container.renderMode == 2 && container._panelOrder == panelSortingOrder)
			{
				sTempDict[container.id] = i;
				sTempList1.Add(container);
				sTempList2.Add(i);
			}
		}
		sTempList1.Sort(delegate(DisplayObject c1, DisplayObject c2)
		{
			int num2 = c2.cachedTransform.position.z.CompareTo(c1.cachedTransform.position.z);
			return (num2 == 0) ? sTempDict[c1.id].CompareTo(sTempDict[c2.id]) : num2;
		});
		ChangeChildrenOrder(sTempList2, sTempList1);
		sTempList1.Clear();
		sTempList2.Clear();
		sTempDict.Clear();
	}

	public void MonitorTexture(NTexture texture)
	{
		if (_toCollectTextures.IndexOf(texture) == -1)
		{
			_toCollectTextures.Add(texture);
		}
	}

	private void RunTextureCollector(object param)
	{
		int num = _toCollectTextures.Count;
		float time = Time.time;
		int num2 = 0;
		while (num2 < num)
		{
			NTexture nTexture = _toCollectTextures[num2];
			if (nTexture.disposed)
			{
				_toCollectTextures.RemoveAt(num2);
				num--;
			}
			else if (time - nTexture.lastActive > 5f)
			{
				nTexture.Dispose();
				_toCollectTextures.RemoveAt(num2);
				num--;
			}
			else
			{
				num2++;
			}
		}
	}

	public void AddTouchMonitor(int touchId, EventDispatcher target)
	{
		TouchInfo touchInfo = null;
		for (int i = 0; i < 5; i++)
		{
			touchInfo = _touches[i];
			if ((touchId == -1 && touchInfo.touchId != -1) || (touchId != -1 && touchInfo.touchId == touchId))
			{
				break;
			}
		}
		if (touchInfo.touchMonitors.IndexOf(target) == -1)
		{
			touchInfo.touchMonitors.Add(target);
		}
	}

	public void RemoveTouchMonitor(EventDispatcher target)
	{
		for (int i = 0; i < 5; i++)
		{
			TouchInfo touchInfo = _touches[i];
			int num = touchInfo.touchMonitors.IndexOf(target);
			if (num != -1)
			{
				touchInfo.touchMonitors[num] = null;
			}
		}
	}

	public bool IsTouchMonitoring(EventDispatcher target)
	{
		for (int i = 0; i < 5; i++)
		{
			if (_touches[i].touchMonitors.IndexOf(target) != -1)
			{
				return true;
			}
		}
		return false;
	}

	internal Transform CreatePoolManager(string name)
	{
		GameObject obj = new GameObject("[" + name + "]");
		obj.SetActive(value: false);
		Transform transform = obj.transform;
		transform.SetParent(base.cachedTransform, worldPositionStays: false);
		return transform;
	}

	public void RegisterCursor(string cursorName, Texture2D texture, Vector2 hotspot)
	{
		_cursors[cursorName] = new CursorDef
		{
			texture = texture,
			hotspot = hotspot
		};
	}

	public void ActiveCurCursor()
	{
		_ChangeCursor(activeCursor);
	}

	internal void _ChangeCursor(string cursorName)
	{
		if (!GameSettings.IsRunningOnSteamDeck)
		{
			if (cursorName != null && GetCursorDef(cursorName, out var cursorDef))
			{
				_currentCursor = cursorName;
				Cursor.SetCursor(cursorDef.texture, cursorDef.hotspot, CursorMode.Auto);
			}
			else if (GetCursorDef("onMouseUp", out cursorDef))
			{
				Cursor.SetCursor(cursorDef.texture, cursorDef.hotspot, CursorMode.Auto);
			}
			else
			{
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			}
		}
	}

	private bool GetCursorDef(string key, out CursorDef cursorDef)
	{
		if (CursorConfig.cursorType == CursorType.CrossHair)
		{
			cursorDef = null;
			Cursor.visible = false;
			return false;
		}
		Cursor.visible = true;
		return _cursors.TryGetValue(key, out cursorDef);
	}
}
