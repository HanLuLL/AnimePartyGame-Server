using UnityEngine;

namespace FairyGUI;

public class MovieClip : Image
{
	public class Frame
	{
		public NTexture texture;

		public float addDelay;
	}

	public float interval;

	public bool swing;

	public float repeatDelay;

	public float timeScale;

	public bool ignoreEngineTimeScale;

	private Frame[] _frames;

	private int _frameCount;

	private int _frame;

	private bool _playing;

	private int _start;

	private int _end;

	private int _times;

	private int _endAt;

	private int _status;

	private float _frameElapsed;

	private bool _reversed;

	private int _repeatedCount;

	private TimerCallback _timerDelegate;

	private EventListener _onPlayEnd;

	public EventListener onPlayEnd => _onPlayEnd ?? (_onPlayEnd = new EventListener(this, "onPlayEnd"));

	public Frame[] frames
	{
		get
		{
			return _frames;
		}
		set
		{
			_frames = value;
			_scale9Grid = null;
			_scaleByTile = false;
			if (_frames == null)
			{
				_frameCount = 0;
				base.graphics.texture = null;
				CheckTimer();
				return;
			}
			_frameCount = frames.Length;
			if (_end == -1 || _end > _frameCount - 1)
			{
				_end = _frameCount - 1;
			}
			if (_endAt == -1 || _endAt > _frameCount - 1)
			{
				_endAt = _frameCount - 1;
			}
			if (_frame < 0 || _frame > _frameCount - 1)
			{
				_frame = _frameCount - 1;
			}
			InvalidateBatchingState();
			_frameElapsed = 0f;
			_repeatedCount = 0;
			_reversed = false;
			DrawFrame();
			CheckTimer();
		}
	}

	public bool playing
	{
		get
		{
			return _playing;
		}
		set
		{
			if (_playing != value)
			{
				_playing = value;
				CheckTimer();
			}
		}
	}

	public int frame
	{
		get
		{
			return _frame;
		}
		set
		{
			if (_frame != value)
			{
				if (_frames != null && value >= _frameCount)
				{
					value = _frameCount - 1;
				}
				_frame = value;
				_frameElapsed = 0f;
				DrawFrame();
			}
		}
	}

	public MovieClip()
	{
		interval = 0.1f;
		_playing = true;
		_timerDelegate = OnTimer;
		timeScale = 1f;
		ignoreEngineTimeScale = false;
		if (Application.isPlaying)
		{
			base.onAddedToStage.Add(OnAddedToStage);
			base.onRemovedFromStage.Add(OnRemoveFromStage);
		}
		SetPlaySettings();
	}

	public void Rewind()
	{
		_frame = 0;
		_frameElapsed = 0f;
		_reversed = false;
		_repeatedCount = 0;
		DrawFrame();
	}

	public void SyncStatus(MovieClip anotherMc)
	{
		_frame = anotherMc._frame;
		_frameElapsed = anotherMc._frameElapsed;
		_reversed = anotherMc._reversed;
		_repeatedCount = anotherMc._repeatedCount;
		DrawFrame();
	}

	public void Advance(float time)
	{
		int num = _frame;
		bool reversed = _reversed;
		float num2 = time;
		while (true)
		{
			float num3 = interval + _frames[_frame].addDelay;
			if (_frame == 0 && _repeatedCount > 0)
			{
				num3 += repeatDelay;
			}
			if (time < num3)
			{
				break;
			}
			time -= num3;
			if (swing)
			{
				if (_reversed)
				{
					_frame--;
					if (_frame <= 0)
					{
						_frame = 0;
						_repeatedCount++;
						_reversed = !_reversed;
					}
				}
				else
				{
					_frame++;
					if (_frame > _frameCount - 1)
					{
						_frame = Mathf.Max(0, _frameCount - 2);
						_repeatedCount++;
						_reversed = !_reversed;
					}
				}
			}
			else
			{
				_frame++;
				if (_frame > _frameCount - 1)
				{
					_frame = 0;
					_repeatedCount++;
				}
			}
			if (_frame == num && _reversed == reversed)
			{
				float num4 = num2 - time;
				time -= (float)Mathf.FloorToInt(time / num4) * num4;
			}
		}
		_frameElapsed = 0f;
		DrawFrame();
	}

	public void SetPlaySettings()
	{
		SetPlaySettings(0, -1, 0, -1);
	}

	public void SetPlaySettings(int start, int end, int times, int endAt)
	{
		_start = start;
		_end = end;
		if (_end == -1 || _end > _frameCount - 1)
		{
			_end = _frameCount - 1;
		}
		_times = times;
		_endAt = endAt;
		if (_endAt == -1)
		{
			_endAt = _end;
		}
		_status = 0;
		frame = start;
	}

	private void OnAddedToStage()
	{
		if (_playing && _frameCount > 0)
		{
			Timers.inst.AddUpdate(_timerDelegate);
		}
	}

	private void OnRemoveFromStage()
	{
		Timers.inst.Remove(_timerDelegate);
	}

	private void CheckTimer()
	{
		if (Application.isPlaying)
		{
			if (_playing && _frameCount > 0 && base.stage != null)
			{
				Timers.inst.AddUpdate(_timerDelegate);
			}
			else
			{
				Timers.inst.Remove(_timerDelegate);
			}
		}
	}

	private void OnTimer(object param)
	{
		if (!_playing || _frameCount == 0 || _status == 3)
		{
			return;
		}
		float num;
		if (ignoreEngineTimeScale)
		{
			num = Time.unscaledDeltaTime;
			if (num > 0.1f)
			{
				num = 0.1f;
			}
		}
		else
		{
			num = Time.deltaTime;
		}
		if (timeScale != 1f)
		{
			num *= timeScale;
		}
		_frameElapsed += num;
		float num2 = interval + _frames[_frame].addDelay;
		if (_frame == 0 && _repeatedCount > 0)
		{
			num2 += repeatDelay;
		}
		if (_frameElapsed < num2)
		{
			return;
		}
		_frameElapsed -= num2;
		if (_frameElapsed > interval)
		{
			_frameElapsed = interval;
		}
		if (swing)
		{
			if (_reversed)
			{
				_frame--;
				if (_frame <= 0)
				{
					_frame = 0;
					_repeatedCount++;
					_reversed = !_reversed;
				}
			}
			else
			{
				_frame++;
				if (_frame > _frameCount - 1)
				{
					_frame = Mathf.Max(0, _frameCount - 2);
					_repeatedCount++;
					_reversed = !_reversed;
				}
			}
		}
		else
		{
			_frame++;
			if (_frame > _frameCount - 1)
			{
				_frame = 0;
				_repeatedCount++;
			}
		}
		if (_status == 1)
		{
			_frame = _start;
			_frameElapsed = 0f;
			_status = 0;
			DrawFrame();
			return;
		}
		if (_status == 2)
		{
			_frame = _endAt;
			_frameElapsed = 0f;
			_status = 3;
			DrawFrame();
			DispatchEvent("onPlayEnd", null);
			return;
		}
		DrawFrame();
		if (_frame != _end)
		{
			return;
		}
		if (_times > 0)
		{
			_times--;
			if (_times == 0)
			{
				_status = 2;
			}
			else
			{
				_status = 1;
			}
		}
		else if (_start != 0)
		{
			_status = 1;
		}
	}

	private void DrawFrame()
	{
		if (_frameCount > 0)
		{
			Frame frame = _frames[_frame];
			base.graphics.texture = frame.texture;
		}
	}
}
