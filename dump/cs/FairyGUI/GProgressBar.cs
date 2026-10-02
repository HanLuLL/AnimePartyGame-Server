using System;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GProgressBar : GComponent
{
	private GObject _preBarObjectH;

	private GObject _preBarObjectV;

	private float _preBarMaxWidth;

	private float _preBarMaxHeight;

	private float _preBarMaxWidthDelta;

	private float _preBarMaxHeightDelta;

	private float _preBarStartX;

	private float _preBarStartY;

	private double _preValue;

	private double _min;

	private double _max;

	private double _value;

	private ProgressTitleType _titleType;

	private bool _reverse;

	private GObject _titleObject;

	private GMovieClip _aniObject;

	private GObject _barObjectH;

	private GObject _barObjectV;

	private float _barMaxWidth;

	private float _barMaxHeight;

	private float _barMaxWidthDelta;

	private float _barMaxHeightDelta;

	private float _barStartX;

	private float _barStartY;

	public double preValue
	{
		get
		{
			return _preValue;
		}
		set
		{
			if (_preValue != value)
			{
				_preValue = value;
				Update(_value, _preValue);
			}
		}
	}

	public float BarStartX => _barStartX;

	public float BarStartY => _barStartY;

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
				Update(_value, _preValue);
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
				Update(_value, _preValue);
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
				Update(_value, _preValue);
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
				GTween.Kill(this, TweenPropType.Progress, complete: false);
				_value = value;
				Update(_value, _preValue);
			}
		}
	}

	public bool reverse
	{
		get
		{
			return _reverse;
		}
		set
		{
			_reverse = value;
		}
	}

	public GProgressBar()
	{
		_value = 50.0;
		_max = 100.0;
		_preValue = 0.0;
	}

	public GTweener TweenValue(double value, float duration)
	{
		GTweener tween = GTween.GetTween(this, TweenPropType.Progress);
		double d;
		if (tween != null)
		{
			d = tween.value.d;
			tween.Kill();
		}
		else
		{
			d = _value;
		}
		_value = value;
		return GTween.ToDouble(d, _value, duration).SetEase(EaseType.Linear).SetTarget(this, TweenPropType.Progress);
	}

	public void Update(double newValue, double newPreValue)
	{
		float num = Mathf.Clamp01((float)((newValue - _min) / (_max - _min)));
		float num2 = Mathf.Clamp01((float)((newPreValue - _min) / (_max - _min)));
		if (_titleObject != null)
		{
			switch (_titleType)
			{
			case ProgressTitleType.Percent:
				if (RTLSupport.BaseDirection == RTLSupport.DirectionType.RTL)
				{
					_titleObject.text = "%" + Mathf.FloorToInt(num * 100f);
				}
				else
				{
					_titleObject.text = Mathf.FloorToInt(num * 100f) + "%";
				}
				break;
			case ProgressTitleType.ValueAndMax:
				if (RTLSupport.BaseDirection == RTLSupport.DirectionType.RTL)
				{
					_titleObject.text = Math.Round(max) + "/" + Math.Round(newValue);
				}
				else
				{
					_titleObject.text = Math.Round(newValue) + "/" + Math.Round(max);
				}
				break;
			case ProgressTitleType.Value:
				_titleObject.text = Math.Round(newValue).ToString() ?? "";
				break;
			case ProgressTitleType.Max:
				_titleObject.text = Math.Round(_max).ToString() ?? "";
				break;
			}
		}
		float num3 = base.width - _barMaxWidthDelta;
		float num4 = base.height - _barMaxHeightDelta;
		if (!_reverse)
		{
			if (_barObjectH != null && !SetFillAmount(_barObjectH, num))
			{
				_barObjectH.width = Mathf.RoundToInt(num3 * num);
			}
			if (_barObjectV != null && !SetFillAmount(_barObjectV, num))
			{
				_barObjectV.height = Mathf.RoundToInt(num4 * num);
			}
			if (_preBarObjectH != null && !SetFillAmount(_preBarObjectH, num2))
			{
				_preBarObjectH.width = Mathf.RoundToInt(num3 * num2);
			}
			if (_preBarObjectV != null && !SetFillAmount(_preBarObjectV, num2))
			{
				_preBarObjectV.height = Mathf.RoundToInt(num4 * num2);
			}
		}
		else
		{
			if (_barObjectH != null && !SetFillAmount(_barObjectH, 1f - num))
			{
				_barObjectH.width = Mathf.RoundToInt(num3 * num);
				_barObjectH.x = _barStartX + (num3 - _barObjectH.width);
			}
			if (_barObjectV != null && !SetFillAmount(_barObjectV, 1f - num))
			{
				_barObjectV.height = Mathf.RoundToInt(num4 * num);
				_barObjectV.y = _barStartY + (num4 - _barObjectV.height);
			}
			if (_preBarObjectH != null && !SetFillAmount(_preBarObjectH, 1f - num2))
			{
				_preBarObjectH.width = Mathf.RoundToInt(num3 * num2);
				_preBarObjectH.x = _preBarStartX + (num3 - _preBarObjectH.width);
			}
			if (_preBarObjectV != null && !SetFillAmount(_preBarObjectV, 1f - num2))
			{
				_preBarObjectV.height = Mathf.RoundToInt(num4 * num);
				_preBarObjectV.y = _preBarStartY + (num4 - _preBarObjectV.height);
			}
		}
		if (_aniObject != null)
		{
			_aniObject.frame = Mathf.RoundToInt(num * 100f);
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
		_titleObject = GetChild("title");
		_barObjectH = GetChild("bar");
		_preBarObjectH = GetChild("bar_pre");
		_barObjectV = GetChild("bar_v");
		_preBarObjectV = GetChild("bar_v_pre");
		_aniObject = GetChild("ani") as GMovieClip;
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
		if (_preBarObjectH != null)
		{
			_preBarMaxWidth = _preBarObjectH.width;
			_preBarMaxWidthDelta = base.width - _preBarMaxWidth;
			_preBarStartX = _preBarObjectH.x;
		}
		if (_preBarObjectV != null)
		{
			_preBarMaxHeight = _preBarObjectV.height;
			_preBarMaxHeightDelta = base.height - _preBarMaxHeight;
			_preBarStartY = _preBarObjectV.y;
		}
	}

	public override void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_AfterAdd(buffer, beginPos);
		if (!buffer.Seek(beginPos, 6))
		{
			Update(_value, _preValue);
			return;
		}
		if ((ObjectType)buffer.ReadByte() != packageItem.objectType)
		{
			Update(_value, _preValue);
			return;
		}
		_value = buffer.ReadInt();
		_max = buffer.ReadInt();
		if (buffer.version >= 2)
		{
			_min = buffer.ReadInt();
		}
		if (buffer.version >= 5)
		{
			string text = buffer.ReadS();
			if (!string.IsNullOrEmpty(text))
			{
				buffer.ReadFloat();
				int soundID = Convert.ToInt32(text);
				base.displayObject.onClick.Add((EventCallback0)delegate
				{
					if ((long)soundID > 0L)
					{
						Stage.inst.PlayOneShotSound(soundID);
					}
				});
			}
			else
			{
				buffer.Skip(4);
			}
		}
		Update(_value, _preValue);
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
		if (_preBarObjectH != null)
		{
			_preBarMaxWidth = base.width - _preBarMaxWidthDelta;
		}
		if (_preBarObjectV != null)
		{
			_preBarMaxHeight = base.height - _preBarMaxHeightDelta;
		}
		if (!underConstruct)
		{
			Update(_value, _preValue);
		}
	}
}
