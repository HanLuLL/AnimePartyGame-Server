using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GScrollBar : GComponent
{
	private GObject _grip;

	private GObject _arrowButton1;

	private GObject _arrowButton2;

	private GObject _bar;

	private ScrollPane _target;

	private bool _vertical;

	private float _scrollPerc;

	private bool _fixedGripSize;

	private bool _gripDragging;

	private Vector2 _dragOffset;

	public float minSize
	{
		get
		{
			if (_vertical)
			{
				return ((_arrowButton1 != null) ? _arrowButton1.height : 0f) + ((_arrowButton2 != null) ? _arrowButton2.height : 0f);
			}
			return ((_arrowButton1 != null) ? _arrowButton1.width : 0f) + ((_arrowButton2 != null) ? _arrowButton2.width : 0f);
		}
	}

	public bool gripDragging => _gripDragging;

	public GScrollBar()
	{
		_scrollPerc = 0f;
	}

	public void SetScrollPane(ScrollPane target, bool vertical)
	{
		_target = target;
		_vertical = vertical;
	}

	public void SetDisplayPerc(float value)
	{
		if (_vertical)
		{
			if (!_fixedGripSize)
			{
				_grip.height = Mathf.FloorToInt(value * _bar.height);
			}
			_grip.y = Mathf.RoundToInt(_bar.y + (_bar.height - _grip.height) * _scrollPerc);
		}
		else
		{
			if (!_fixedGripSize)
			{
				_grip.width = Mathf.FloorToInt(value * _bar.width);
			}
			_grip.x = Mathf.RoundToInt(_bar.x + (_bar.width - _grip.width) * _scrollPerc);
		}
		_grip.visible = value != 0f && value != 1f;
	}

	public void setScrollPerc(float value)
	{
		_scrollPerc = value;
		if (_vertical)
		{
			_grip.y = Mathf.RoundToInt(_bar.y + (_bar.height - _grip.height) * _scrollPerc);
		}
		else
		{
			_grip.x = Mathf.RoundToInt(_bar.x + (_bar.width - _grip.width) * _scrollPerc);
		}
	}

	protected override void ConstructExtension(ByteBuffer buffer)
	{
		buffer.Seek(0, 6);
		_fixedGripSize = buffer.ReadBool();
		_grip = GetChild("grip");
		if (_grip == null)
		{
			Debug.LogWarning("FairyGUI: " + base.resourceURL + " should define grip");
			return;
		}
		_bar = GetChild("bar");
		if (_bar == null)
		{
			Debug.LogWarning("FairyGUI: " + base.resourceURL + " should define bar");
			return;
		}
		_arrowButton1 = GetChild("arrow1");
		_arrowButton2 = GetChild("arrow2");
		_grip.onTouchBegin.Add(__gripTouchBegin);
		_grip.onTouchMove.Add(__gripTouchMove);
		_grip.onTouchEnd.Add(__gripTouchEnd);
		base.onTouchBegin.Add(__touchBegin);
		if (_arrowButton1 != null)
		{
			_arrowButton1.onTouchBegin.Add(__arrowButton1Click);
		}
		if (_arrowButton2 != null)
		{
			_arrowButton2.onTouchBegin.Add(__arrowButton2Click);
		}
	}

	private void __gripTouchBegin(EventContext context)
	{
		if (_bar != null)
		{
			context.StopPropagation();
			InputEvent inputEvent = context.inputEvent;
			if (inputEvent.button == 0)
			{
				context.CaptureTouch();
				_gripDragging = true;
				_target.UpdateScrollBarVisible();
				_dragOffset = GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y)) - _grip.xy;
			}
		}
	}

	private void __gripTouchMove(EventContext context)
	{
		InputEvent inputEvent = context.inputEvent;
		Vector2 vector = GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
		if (float.IsNaN(vector.x))
		{
			return;
		}
		if (_vertical)
		{
			float num = vector.y - _dragOffset.y;
			float num2 = _bar.height - _grip.height;
			if (num2 == 0f)
			{
				_target.percY = 0f;
			}
			else
			{
				_target.percY = (num - _bar.y) / num2;
			}
		}
		else
		{
			float num3 = vector.x - _dragOffset.x;
			float num4 = _bar.width - _grip.width;
			if (num4 == 0f)
			{
				_target.percX = 0f;
			}
			else
			{
				_target.percX = (num3 - _bar.x) / num4;
			}
		}
	}

	private void __gripTouchEnd(EventContext context)
	{
		_gripDragging = false;
		_target.UpdateScrollBarVisible();
	}

	private void __arrowButton1Click(EventContext context)
	{
		context.StopPropagation();
		if (_vertical)
		{
			_target.ScrollUp();
		}
		else
		{
			_target.ScrollLeft();
		}
	}

	private void __arrowButton2Click(EventContext context)
	{
		context.StopPropagation();
		if (_vertical)
		{
			_target.ScrollDown();
		}
		else
		{
			_target.ScrollRight();
		}
	}

	private void __touchBegin(EventContext context)
	{
		context.StopPropagation();
		InputEvent inputEvent = context.inputEvent;
		Vector2 vector = _grip.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
		if (_vertical)
		{
			if (vector.y < 0f)
			{
				_target.ScrollUp(4f, ani: false);
			}
			else
			{
				_target.ScrollDown(4f, ani: false);
			}
		}
		else if (vector.x < 0f)
		{
			_target.ScrollLeft(4f, ani: false);
		}
		else
		{
			_target.ScrollRight(4f, ani: false);
		}
	}
}
