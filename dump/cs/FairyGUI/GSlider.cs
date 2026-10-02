using System;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GSlider : GComponent
{
	private double _min;

	private double _max;

	private double _value;

	private ProgressTitleType _titleType;

	private bool _reverse;

	private bool _wholeNumbers;

	private GObject _titleObject;

	private GObject _barObjectH;

	private GObject _barObjectV;

	private float _barMaxWidth;

	private float _barMaxHeight;

	private float _barMaxWidthDelta;

	private float _barMaxHeightDelta;

	private GObject _gripObject;

	private Vector2 _clickPos;

	private float _clickPercent;

	private float _barStartX;

	private float _barStartY;

	private EventListener _onChanged;

	private EventListener _onGripTouchEnd;

	public bool changeOnClick;

	public bool canDrag;

	public Vector2 gripPos
	{
		get
		{
			if (_gripObject != null)
			{
				return _gripObject.position;
			}
			return Vector2.zero;
		}
	}

	public EventListener onChanged => _onChanged ?? (_onChanged = new EventListener(this, "onChanged"));

	public EventListener onGripTouchEnd => _onGripTouchEnd ?? (_onGripTouchEnd = new EventListener(this, "onGripTouchEnd"));

	public ProgressTitleType titleType
	{
		get
		{
			return _titleType;
		}
		set
		{
			if (_titleType != value)
			{
				_titleType = value;
				Update();
			}
		}
	}

	public double min
	{
		get
		{
			return _min;
		}
		set
		{
			if (_min != value)
			{
				_min = value;
				Update();
			}
		}
	}

	public double max
	{
		get
		{
			return _max;
		}
		set
		{
			if (_max != value)
			{
				_max = value;
				Update();
			}
		}
	}

	public double value
	{
		get
		{
			return _value;
		}
		set
		{
			if (_value != value)
			{
				_value = value;
				Update();
			}
		}
	}

	public bool wholeNumbers
	{
		get
		{
			return _wholeNumbers;
		}
		set
		{
			if (_wholeNumbers != value)
			{
				_wholeNumbers = value;
				Update();
			}
		}
	}

	public void UpdateValueAndChange(double v)
	{
		if (Math.Abs(_value - v) > 0.0)
		{
			UpdateWithPercent((float)((v - _min) / (_max - _min)), manual: true);
		}
	}

	public GSlider()
	{
		_value = 50.0;
		_max = 100.0;
		changeOnClick = true;
		canDrag = true;
	}

	private void Update()
	{
		UpdateWithPercent((float)((_value - _min) / (_max - _min)), manual: false);
	}

	private void UpdateWithPercent(float percent, bool manual)
	{
		percent = Mathf.Clamp01(percent);
		if (manual)
		{
			double num = _min + (_max - _min) * (double)percent;
			if (num < _min)
			{
				num = _min;
			}
			if (num > _max)
			{
				num = _max;
			}
			if (_wholeNumbers)
			{
				num = Math.Round(num);
				percent = Mathf.Clamp01((float)((num - _min) / (_max - _min)));
			}
			if (num != _value)
			{
				_value = num;
				if (DispatchEvent("onChanged", null))
				{
					return;
				}
			}
		}
		if (_titleObject != null)
		{
			switch (_titleType)
			{
			case ProgressTitleType.Percent:
				_titleObject.text = Mathf.FloorToInt(percent * 100f) + "%";
				break;
			case ProgressTitleType.ValueAndMax:
				_titleObject.text = Math.Round(_value) + "/" + Math.Round(max);
				break;
			case ProgressTitleType.Value:
				_titleObject.text = Math.Round(_value).ToString() ?? "";
				break;
			case ProgressTitleType.Max:
				_titleObject.text = Math.Round(_max).ToString() ?? "";
				break;
			}
		}
		float num2 = base.width - _barMaxWidthDelta;
		float num3 = base.height - _barMaxHeightDelta;
		if (!_reverse)
		{
			if (_barObjectH != null && !SetFillAmount(_barObjectH, percent))
			{
				_barObjectH.width = Mathf.RoundToInt(num2 * percent);
			}
			if (_barObjectV != null && !SetFillAmount(_barObjectV, percent))
			{
				_barObjectV.height = Mathf.RoundToInt(num3 * percent);
			}
		}
		else
		{
			if (_barObjectH != null && !SetFillAmount(_barObjectH, 1f - percent))
			{
				_barObjectH.width = Mathf.RoundToInt(num2 * percent);
				_barObjectH.x = _barStartX + (num2 - _barObjectH.width);
			}
			if (_barObjectV != null && !SetFillAmount(_barObjectV, 1f - percent))
			{
				_barObjectV.height = Mathf.RoundToInt(num3 * percent);
				_barObjectV.y = _barStartY + (num3 - _barObjectV.height);
			}
		}
		InvalidateBatchingState(childChanged: true);
	}

	private bool SetFillAmount(GObject bar, float amount)
	{
		if (bar is GImage && ((GImage)bar).fillMethod != FillMethod.None)
		{
			((GImage)bar).fillAmount = amount;
		}
		else
		{
			if (!(bar is GLoader) || ((GLoader)bar).fillMethod == FillMethod.None)
			{
				return false;
			}
			((GLoader)bar).fillAmount = amount;
		}
		return true;
	}

	protected override void ConstructExtension(ByteBuffer buffer)
	{
		buffer.Seek(0, 6);
		_titleType = (ProgressTitleType)buffer.ReadByte();
		_reverse = buffer.ReadBool();
		if (buffer.version >= 2)
		{
			_wholeNumbers = buffer.ReadBool();
			changeOnClick = buffer.ReadBool();
		}
		_titleObject = GetChild("title");
		_barObjectH = GetChild("bar");
		_barObjectV = GetChild("bar_v");
		_gripObject = GetChild("grip");
		if (_barObjectH != null)
		{
			_barMaxWidth = _barObjectH.width;
			_barMaxWidthDelta = base.width - _barMaxWidth;
			_barStartX = _barObjectH.x;
		}
		if (_barObjectV != null)
		{
			_barMaxHeight = _barObjectV.height;
			_barMaxHeightDelta = base.height - _barMaxHeight;
			_barStartY = _barObjectV.y;
		}
		if (_gripObject != null)
		{
			_gripObject.onTouchBegin.Add(__gripTouchBegin);
			_gripObject.onTouchMove.Add(__gripTouchMove);
			_gripObject.onTouchEnd.Add(__gripTouchEnd);
		}
		base.onTouchBegin.Add(__barTouchBegin);
	}

	public override void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_AfterAdd(buffer, beginPos);
		if (!buffer.Seek(beginPos, 6))
		{
			Update();
			return;
		}
		if ((ObjectType)buffer.ReadByte() != packageItem.objectType)
		{
			Update();
			return;
		}
		_value = buffer.ReadInt();
		_max = buffer.ReadInt();
		if (buffer.version >= 2)
		{
			_min = buffer.ReadInt();
		}
		Update();
	}

	protected override void HandleSizeChanged()
	{
		base.HandleSizeChanged();
		if (_barObjectH != null)
		{
			_barMaxWidth = base.width - _barMaxWidthDelta;
		}
		if (_barObjectV != null)
		{
			_barMaxHeight = base.height - _barMaxHeightDelta;
		}
		if (!underConstruct)
		{
			Update();
		}
	}

	private void __gripTouchBegin(EventContext context)
	{
		canDrag = true;
		context.StopPropagation();
		InputEvent inputEvent = context.inputEvent;
		if (inputEvent.button == 0)
		{
			context.CaptureTouch();
			_clickPos = GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
			_clickPercent = Mathf.Clamp01((float)((_value - _min) / (_max - _min)));
		}
	}

	private void __gripTouchMove(EventContext context)
	{
		if (!canDrag)
		{
			return;
		}
		InputEvent inputEvent = context.inputEvent;
		Vector2 vector = GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
		if (!float.IsNaN(vector.x))
		{
			float num = vector.x - _clickPos.x;
			float num2 = vector.y - _clickPos.y;
			if (_reverse)
			{
				num = 0f - num;
				num2 = 0f - num2;
			}
			float percent = ((_barObjectH == null) ? (_clickPercent + num2 / _barMaxHeight) : (_clickPercent + num / _barMaxWidth));
			UpdateWithPercent(percent, manual: true);
		}
	}

	private void __gripTouchEnd(EventContext context)
	{
		DispatchEvent("onGripTouchEnd", null);
	}

	private void __barTouchBegin(EventContext context)
	{
		if (changeOnClick)
		{
			InputEvent inputEvent = context.inputEvent;
			Vector2 vector = _gripObject.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
			float num = Mathf.Clamp01((float)((_value - _min) / (_max - _min)));
			float num2 = 0f;
			if (_barObjectH != null)
			{
				num2 = (vector.x - _gripObject.width / 2f) / _barMaxWidth;
			}
			if (_barObjectV != null)
			{
				num2 = (vector.y - _gripObject.height / 2f) / _barMaxHeight;
			}
			num = ((!_reverse) ? (num + num2) : (num - num2));
			UpdateWithPercent(num, manual: true);
		}
	}
}
