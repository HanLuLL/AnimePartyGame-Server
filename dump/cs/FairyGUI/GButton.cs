using System;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GButton : GComponent, IColorGear
{
	private GTweener _btnTween;

	private LongPressGesture _longPressGesture;

	private EventCallback0 _onBegin;

	private EventCallback0 _onEnd;

	private EventCallback0 _longPressEvent;

	public int sound;

	public float soundVolumeScale;

	public bool changeStateOnClick;

	public GObject linkedPopup;

	protected GObject _titleObject;

	protected GObject _iconObject;

	protected Controller _relatedController;

	protected string _relatedPageId;

	private ButtonMode _mode;

	private bool _selected;

	private string _title;

	private string _icon;

	private string _selectedTitle;

	private string _selectedIcon;

	private Controller _buttonController;

	private int _downEffect;

	private float _downEffectValue;

	private bool _downScaled;

	private bool _down;

	private bool _over;

	private EventListener _onChanged;

	public const string UP = "up";

	public const string DOWN = "down";

	public const string OVER = "over";

	public const string SELECTED_OVER = "selectedOver";

	public const string DISABLED = "disabled";

	public const string SELECTED_DISABLED = "selectedDisabled";

	public EventListener onChanged => _onChanged ?? (_onChanged = new EventListener(this, "onChanged"));

	public override string icon
	{
		get
		{
			return _icon;
		}
		set
		{
			_icon = value;
			value = ((_selected && _selectedIcon != null) ? _selectedIcon : _icon);
			if (_iconObject != null)
			{
				_iconObject.icon = value;
			}
			UpdateGear(7);
		}
	}

	public string title
	{
		get
		{
			return _title;
		}
		set
		{
			_title = value;
			if (_titleObject != null)
			{
				_titleObject.text = ((_selected && _selectedTitle != null) ? _selectedTitle : _title);
			}
			UpdateGear(6);
		}
	}

	public override string text
	{
		get
		{
			return title;
		}
		set
		{
			title = value;
		}
	}

	public string selectedIcon
	{
		get
		{
			return _selectedIcon;
		}
		set
		{
			_selectedIcon = value;
			value = ((_selected && _selectedIcon != null) ? _selectedIcon : _icon);
			if (_iconObject != null)
			{
				_iconObject.icon = value;
			}
		}
	}

	public string selectedTitle
	{
		get
		{
			return _selectedTitle;
		}
		set
		{
			_selectedTitle = value;
			if (_titleObject != null)
			{
				_titleObject.text = ((_selected && _selectedTitle != null) ? _selectedTitle : _title);
			}
		}
	}

	public Color titleColor
	{
		get
		{
			return GetTextField()?.color ?? Color.black;
		}
		set
		{
			GTextField textField = GetTextField();
			if (textField != null)
			{
				textField.color = value;
				UpdateGear(4);
			}
		}
	}

	public Color color
	{
		get
		{
			return titleColor;
		}
		set
		{
			titleColor = value;
		}
	}

	public int titleFontSize
	{
		get
		{
			return GetTextField()?.textFormat.size ?? 0;
		}
		set
		{
			GTextField textField = GetTextField();
			if (textField != null)
			{
				TextFormat textFormat = textField.textFormat;
				textFormat.size = value;
				textField.textFormat = textFormat;
			}
		}
	}

	public bool selected
	{
		get
		{
			return _selected;
		}
		set
		{
			if (_mode == ButtonMode.Common || _selected == value)
			{
				return;
			}
			_selected = value;
			SetCurrentState();
			if (_selectedTitle != null && _titleObject != null)
			{
				_titleObject.text = (_selected ? _selectedTitle : _title);
			}
			if (_selectedIcon != null)
			{
				string text = (_selected ? _selectedIcon : _icon);
				if (_iconObject != null)
				{
					_iconObject.icon = text;
				}
			}
			if (_relatedController == null || base.parent == null || base.parent._buildingDisplayList)
			{
				return;
			}
			if (_selected)
			{
				_relatedController.selectedPageId = _relatedPageId;
				if (_relatedController.autoRadioGroupDepth)
				{
					base.parent.AdjustRadioGroupDepth(this, _relatedController);
				}
			}
			else if (_mode == ButtonMode.Check && _relatedController.selectedPageId == _relatedPageId)
			{
				_relatedController.oppositePageId = _relatedPageId;
			}
		}
	}

	public ButtonMode mode
	{
		get
		{
			return _mode;
		}
		set
		{
			if (_mode != value)
			{
				if (value == ButtonMode.Common)
				{
					selected = false;
				}
				_mode = value;
			}
		}
	}

	public Controller relatedController
	{
		get
		{
			return _relatedController;
		}
		set
		{
			if (value != _relatedController)
			{
				_relatedController = value;
				_relatedPageId = null;
			}
		}
	}

	public string relatedPageId
	{
		get
		{
			return _relatedPageId;
		}
		set
		{
			_relatedPageId = value;
		}
	}

	private void SetTweenScale(bool _isDownScaled)
	{
		_btnTween?.Kill(complete: true);
		Vector2 endValue = (_isDownScaled ? new Vector2(base.scaleX * _downEffectValue, base.scaleY * _downEffectValue) : new Vector2(base.scaleX / _downEffectValue, base.scaleY / _downEffectValue));
		_btnTween = TweenScale(endValue, 0.1f);
	}

	public void RegisterLongPressEvent(EventCallback0 onBegin, EventCallback0 onEnd, EventCallback0 longPressEvent, float trigger, float interval, bool once = true)
	{
		if (_longPressGesture == null)
		{
			_longPressGesture = new LongPressGesture(this);
		}
		_longPressGesture.trigger = 0f;
		_longPressGesture.Cancel();
		_longPressGesture.once = once;
		_longPressGesture.trigger = trigger;
		_longPressGesture.interval = interval;
		_onBegin = onBegin;
		_onEnd = onEnd;
		_longPressEvent = longPressEvent;
		if (onBegin != null)
		{
			_longPressGesture.onBegin.Add(_onBegin);
		}
		if (onEnd != null)
		{
			_longPressGesture.onEnd.Add(_onEnd);
		}
		if (longPressEvent != null)
		{
			_longPressGesture.onAction.Add(_longPressEvent);
		}
	}

	public void UnRegisterLongPressEvent()
	{
		if (_longPressGesture != null)
		{
			if (_onBegin != null)
			{
				_longPressGesture.onBegin.Remove(_onBegin);
			}
			if (_onEnd != null)
			{
				_longPressGesture.onEnd.Remove(_onEnd);
			}
			if (_longPressEvent != null)
			{
				_longPressGesture.onAction.Remove(_longPressEvent);
			}
		}
	}

	public GButton()
	{
		soundVolumeScale = UIConfig.buttonSoundVolumeScale;
		changeStateOnClick = true;
		_downEffectValue = 0.8f;
		_title = string.Empty;
	}

	public void FireClick(bool downEffect, bool clickCall = false)
	{
		if (downEffect && _mode == ButtonMode.Common)
		{
			SetState("over");
			Timers.inst.Add(0.1f, 1, delegate
			{
				SetState("down");
			});
			Timers.inst.Add(0.2f, 1, delegate
			{
				SetState("up");
				if (clickCall)
				{
					base.onClick.Call();
				}
			});
		}
		else if (clickCall)
		{
			base.onClick.Call();
		}
		__click();
	}

	public GTextField GetTextField()
	{
		if (_titleObject is GTextField)
		{
			return (GTextField)_titleObject;
		}
		if (_titleObject is GLabel)
		{
			return ((GLabel)_titleObject).GetTextField();
		}
		if (_titleObject is GButton)
		{
			return ((GButton)_titleObject).GetTextField();
		}
		return null;
	}

	protected void SetState(string val)
	{
		if (_buttonController != null)
		{
			_buttonController.selectedPage = val;
		}
		if (_downEffect == 1)
		{
			int num = base.numChildren;
			switch (val)
			{
			case "down":
			case "selectedOver":
			case "selectedDisabled":
			{
				Color color = new Color(_downEffectValue, _downEffectValue, _downEffectValue);
				for (int i = 0; i < num; i++)
				{
					GObject childAt = GetChildAt(i);
					if (childAt is IColorGear && !(childAt is GTextField))
					{
						((IColorGear)childAt).color = color;
					}
				}
				return;
			}
			}
			for (int j = 0; j < num; j++)
			{
				GObject childAt2 = GetChildAt(j);
				if (childAt2 is IColorGear && !(childAt2 is GTextField))
				{
					((IColorGear)childAt2).color = Color.white;
				}
			}
		}
		else
		{
			if (_downEffect != 2)
			{
				return;
			}
			switch (val)
			{
			case "down":
			case "selectedOver":
			case "selectedDisabled":
				if (!_downScaled)
				{
					_downScaled = true;
					SetScale(base.scaleX * _downEffectValue, base.scaleY * _downEffectValue);
				}
				break;
			default:
				if (_downScaled)
				{
					_downScaled = false;
					SetScale(base.scaleX / _downEffectValue, base.scaleY / _downEffectValue);
				}
				break;
			}
		}
	}

	protected void SetCurrentState()
	{
		if (base.grayed && _buttonController != null && _buttonController.HasPage("disabled"))
		{
			if (_selected)
			{
				SetState("selectedDisabled");
			}
			else
			{
				SetState("disabled");
			}
		}
		else if (_selected)
		{
			SetState(_over ? "selectedOver" : "down");
		}
		else
		{
			SetState(_over ? "over" : "up");
		}
	}

	public override void HandleControllerChanged(Controller c)
	{
		base.HandleControllerChanged(c);
		if (_relatedController == c)
		{
			selected = _relatedPageId == c.selectedPageId;
		}
	}

	protected override void HandleGrayedChanged()
	{
		if (_buttonController != null && _buttonController.HasPage("disabled"))
		{
			if (base.grayed)
			{
				if (_selected)
				{
					SetState("selectedDisabled");
				}
				else
				{
					SetState("disabled");
				}
			}
			else if (_selected)
			{
				SetState("down");
			}
			else
			{
				SetState("up");
			}
		}
		else
		{
			base.HandleGrayedChanged();
		}
	}

	protected override void ConstructExtension(ByteBuffer buffer)
	{
		buffer.Seek(0, 6);
		_mode = (ButtonMode)buffer.ReadByte();
		string text = buffer.ReadS();
		if (text != null)
		{
			sound = Convert.ToInt32(text);
		}
		soundVolumeScale = buffer.ReadFloat();
		_downEffect = buffer.ReadByte();
		_downEffectValue = buffer.ReadFloat();
		if (_downEffect == 2)
		{
			SetPivot(0.5f, 0.5f, base.pivotAsAnchor);
		}
		_buttonController = GetController("button");
		_titleObject = GetChild("title");
		_iconObject = GetChild("icon");
		if (_titleObject != null)
		{
			_title = _titleObject.text;
		}
		if (_iconObject != null)
		{
			_icon = _iconObject.icon;
		}
		if (_mode == ButtonMode.Common)
		{
			SetState("up");
		}
		base.displayObject.onRollOver.Add(__rollover);
		base.displayObject.onRollOut.Add(__rollout);
		base.displayObject.onTouchBegin.Add(__touchBegin);
		base.displayObject.onTouchEnd.Add(__touchEnd);
		base.displayObject.onRemovedFromStage.Add(__removedFromStage);
		base.displayObject.onClick.Add(__click);
	}

	public override void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_AfterAdd(buffer, beginPos);
		if (buffer.Seek(beginPos, 6) && (ObjectType)buffer.ReadByte() == packageItem.objectType)
		{
			string text = buffer.ReadS();
			if (text != null)
			{
				title = text;
			}
			text = buffer.ReadS();
			if (text != null)
			{
				selectedTitle = text;
			}
			text = buffer.ReadS();
			if (text != null)
			{
				icon = text;
			}
			text = buffer.ReadS();
			if (text != null)
			{
				selectedIcon = text;
			}
			if (buffer.ReadBool())
			{
				titleColor = buffer.ReadColor();
			}
			int num = buffer.ReadInt();
			if (num != 0)
			{
				titleFontSize = num;
			}
			num = buffer.ReadShort();
			if (num >= 0)
			{
				_relatedController = base.parent.GetControllerAt(num);
			}
			_relatedPageId = buffer.ReadS();
			text = buffer.ReadS();
			if (text != null)
			{
				sound = Convert.ToInt32(text);
			}
			if (buffer.ReadBool())
			{
				soundVolumeScale = buffer.ReadFloat();
			}
			selected = buffer.ReadBool();
		}
	}

	private void __rollover()
	{
		if (_buttonController != null && _buttonController.HasPage("over"))
		{
			_over = true;
			if (!_down && (!base.grayed || !_buttonController.HasPage("disabled")))
			{
				SetState(_selected ? "selectedOver" : "over");
			}
		}
	}

	private void __rollout()
	{
		if (_buttonController != null && _buttonController.HasPage("over"))
		{
			_over = false;
			if (!_down && (!base.grayed || !_buttonController.HasPage("disabled")))
			{
				SetState(_selected ? "down" : "up");
			}
		}
	}

	private void __touchBegin(EventContext context)
	{
		if (context.inputEvent.button != 0 && context.inputEvent.button != 1)
		{
			return;
		}
		_down = true;
		context.CaptureTouch();
		if (_mode == ButtonMode.Common)
		{
			if (base.grayed && _buttonController != null && _buttonController.HasPage("disabled"))
			{
				SetState("selectedDisabled");
			}
			else
			{
				SetState("down");
			}
		}
		if (linkedPopup != null)
		{
			if (linkedPopup is Window)
			{
				((Window)linkedPopup).ToggleStatus();
			}
			else
			{
				base.root.TogglePopup(linkedPopup, this);
			}
		}
	}

	private void __touchEnd()
	{
		if (!_down)
		{
			return;
		}
		_down = false;
		if (_mode == ButtonMode.Common)
		{
			if (base.grayed && _buttonController != null && _buttonController.HasPage("disabled"))
			{
				SetState("disabled");
			}
			else if (_over)
			{
				SetState("over");
			}
			else
			{
				SetState("up");
			}
		}
		else if (!_over && _buttonController != null && (_buttonController.selectedPage == "over" || _buttonController.selectedPage == "selectedOver"))
		{
			SetCurrentState();
		}
	}

	private void __removedFromStage()
	{
		if (_over)
		{
			__rollout();
		}
	}

	private void __click()
	{
		if ((long)sound > 0L)
		{
			Stage.inst.PlayOneShotSound(sound);
		}
		if (_mode == ButtonMode.Check)
		{
			if (changeStateOnClick)
			{
				selected = !_selected;
				DispatchEvent("onChanged", null);
			}
		}
		else if (_mode == ButtonMode.Radio)
		{
			if (changeStateOnClick && !_selected)
			{
				selected = true;
				DispatchEvent("onChanged", null);
			}
		}
		else if (_relatedController != null)
		{
			_relatedController.selectedPageId = _relatedPageId;
		}
	}
}
