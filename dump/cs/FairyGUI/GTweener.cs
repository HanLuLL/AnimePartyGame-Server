using System;
using UnityEngine;

namespace FairyGUI;

public class GTweener
{
	internal object _target;

	internal TweenPropType _propType;

	internal bool _killed;

	internal bool _paused;

	private float _delay;

	private float _duration;

	private float _breakpoint;

	private EaseType _easeType;

	private float _easeOvershootOrAmplitude;

	private float _easePeriod;

	private int _repeat;

	private bool _yoyo;

	private float _timeScale;

	private bool _ignoreEngineTimeScale;

	private bool _snapping;

	private object _userData;

	private GPath _path;

	private CustomEase _customEase;

	private GTweenCallback _onUpdate;

	private GTweenCallback _onStart;

	private GTweenCallback _onComplete;

	private GTweenCallback1 _onUpdate1;

	private GTweenCallback1 _onStart1;

	private GTweenCallback1 _onComplete1;

	private ITweenListener _listener;

	private TweenValue _startValue;

	private TweenValue _endValue;

	private TweenValue _value;

	private TweenValue _deltaValue;

	private int _valueSize;

	private bool _started;

	private int _ended;

	private float _elapsedTime;

	private float _normalizedTime;

	private int _smoothStart;

	public float delay => _delay;

	public float duration => _duration;

	public int repeat => _repeat;

	public object target => _target;

	public object userData => _userData;

	public TweenValue startValue => _startValue;

	public TweenValue endValue => _endValue;

	public TweenValue value => _value;

	public TweenValue deltaValue => _deltaValue;

	public float normalizedTime => _normalizedTime;

	public bool completed => _ended != 0;

	public bool allCompleted => _ended == 1;

	public GTweener()
	{
		_startValue = new TweenValue();
		_endValue = new TweenValue();
		_value = new TweenValue();
		_deltaValue = new TweenValue();
	}

	public GTweener SetDelay(float value)
	{
		_delay = value;
		return this;
	}

	public GTweener SetDuration(float value)
	{
		_duration = value;
		return this;
	}

	public GTweener SetBreakpoint(float value)
	{
		_breakpoint = value;
		return this;
	}

	public GTweener SetEase(EaseType value)
	{
		_easeType = value;
		return this;
	}

	public GTweener SetEase(EaseType value, CustomEase customEase)
	{
		_easeType = value;
		_customEase = customEase;
		return this;
	}

	public GTweener SetEasePeriod(float value)
	{
		_easePeriod = value;
		return this;
	}

	public GTweener SetEaseOvershootOrAmplitude(float value)
	{
		_easeOvershootOrAmplitude = value;
		return this;
	}

	public GTweener SetRepeat(int times, bool yoyo = false)
	{
		_repeat = times;
		_yoyo = yoyo;
		return this;
	}

	public GTweener SetTimeScale(float value)
	{
		_timeScale = value;
		return this;
	}

	public GTweener SetIgnoreEngineTimeScale(bool value)
	{
		_ignoreEngineTimeScale = value;
		return this;
	}

	public GTweener SetSnapping(bool value)
	{
		_snapping = value;
		return this;
	}

	public GTweener SetPath(GPath value)
	{
		_path = value;
		return this;
	}

	public GTweener SetTarget(object value)
	{
		_target = value;
		_propType = TweenPropType.None;
		return this;
	}

	public GTweener SetTarget(object value, TweenPropType propType)
	{
		_target = value;
		_propType = propType;
		return this;
	}

	public GTweener SetUserData(object value)
	{
		_userData = value;
		return this;
	}

	public GTweener OnUpdate(GTweenCallback callback)
	{
		_onUpdate = callback;
		return this;
	}

	public GTweener OnStart(GTweenCallback callback)
	{
		_onStart = callback;
		return this;
	}

	public GTweener OnComplete(GTweenCallback callback)
	{
		_onComplete = callback;
		return this;
	}

	public GTweener OnUpdate(GTweenCallback1 callback)
	{
		_onUpdate1 = callback;
		return this;
	}

	public GTweener OnStart(GTweenCallback1 callback)
	{
		_onStart1 = callback;
		return this;
	}

	public GTweener OnComplete(GTweenCallback1 callback)
	{
		_onComplete1 = callback;
		return this;
	}

	public GTweener SetListener(ITweenListener value)
	{
		_listener = value;
		return this;
	}

	public GTweener SetPaused(bool paused)
	{
		_paused = paused;
		if (_paused)
		{
			_smoothStart = 0;
		}
		return this;
	}

	public void Seek(float time)
	{
		if (_killed)
		{
			return;
		}
		_elapsedTime = time;
		if (_elapsedTime < _delay)
		{
			if (!_started)
			{
				return;
			}
			_elapsedTime = _delay;
		}
		Update();
	}

	public void Kill(bool complete = false)
	{
		if (_killed)
		{
			return;
		}
		if (complete)
		{
			if (_ended == 0)
			{
				if (_breakpoint >= 0f)
				{
					_elapsedTime = _delay + _breakpoint;
				}
				else if (_repeat >= 0)
				{
					_elapsedTime = _delay + _duration * (float)(_repeat + 1);
				}
				else
				{
					_elapsedTime = _delay + _duration * 2f;
				}
				Update();
			}
			CallCompleteCallback();
		}
		_killed = true;
	}

	internal GTweener _To(float start, float end, float duration)
	{
		_valueSize = 1;
		_startValue.x = start;
		_endValue.x = end;
		_value.x = start;
		_duration = duration;
		return this;
	}

	internal GTweener _To(Vector2 start, Vector2 end, float duration)
	{
		_valueSize = 2;
		_startValue.vec2 = start;
		_endValue.vec2 = end;
		_value.vec2 = start;
		_duration = duration;
		return this;
	}

	internal GTweener _To(Vector3 start, Vector3 end, float duration)
	{
		_valueSize = 3;
		_startValue.vec3 = start;
		_endValue.vec3 = end;
		_value.vec3 = start;
		_duration = duration;
		return this;
	}

	internal GTweener _To(Vector4 start, Vector4 end, float duration)
	{
		_valueSize = 4;
		_startValue.vec4 = start;
		_endValue.vec4 = end;
		_value.vec4 = start;
		_duration = duration;
		return this;
	}

	internal GTweener _To(Color start, Color end, float duration)
	{
		_valueSize = 4;
		_startValue.color = start;
		_endValue.color = end;
		_value.color = start;
		_duration = duration;
		return this;
	}

	internal GTweener _To(double start, double end, float duration)
	{
		_valueSize = 5;
		_startValue.d = start;
		_endValue.d = end;
		_value.d = start;
		_duration = duration;
		return this;
	}

	internal GTweener _Shake(Vector3 start, float amplitude, float duration)
	{
		_valueSize = 6;
		_startValue.vec3 = start;
		_startValue.w = amplitude;
		_duration = duration;
		_easeType = EaseType.Linear;
		return this;
	}

	internal void _Init()
	{
		_delay = 0f;
		_duration = 0f;
		_breakpoint = -1f;
		_easeType = EaseType.QuadOut;
		_timeScale = 1f;
		_ignoreEngineTimeScale = false;
		_easePeriod = 0f;
		_easeOvershootOrAmplitude = 1.70158f;
		_snapping = false;
		_repeat = 0;
		_yoyo = false;
		_valueSize = 0;
		_started = false;
		_paused = false;
		_killed = false;
		_elapsedTime = 0f;
		_normalizedTime = 0f;
		_ended = 0;
		_path = null;
		_customEase = null;
		_smoothStart = ((Time.frameCount != 1) ? 1 : 3);
	}

	internal void _Reset()
	{
		_target = null;
		_listener = null;
		_userData = null;
		_onStart = (_onUpdate = (_onComplete = null));
		_onStart1 = (_onUpdate1 = (_onComplete1 = null));
	}

	internal void _Update()
	{
		if (_ended != 0)
		{
			CallCompleteCallback();
			_killed = true;
			return;
		}
		float num;
		if (_smoothStart <= 0)
		{
			num = ((!_ignoreEngineTimeScale) ? Time.deltaTime : Time.unscaledDeltaTime);
		}
		else
		{
			_smoothStart--;
			num = Mathf.Clamp(Time.unscaledDeltaTime, 0f, (Application.targetFrameRate > 0) ? (1f / (float)Application.targetFrameRate) : 0.016f);
			if (!_ignoreEngineTimeScale)
			{
				num *= Time.timeScale;
			}
		}
		if (_timeScale != 1f)
		{
			num *= _timeScale;
		}
		if (num != 0f)
		{
			_elapsedTime += num;
			Update();
			if (_ended != 0 && !_killed)
			{
				CallCompleteCallback();
				_killed = true;
			}
		}
	}

	private void Update()
	{
		_ended = 0;
		if (_valueSize == 0)
		{
			if (_elapsedTime >= _delay + _duration)
			{
				_ended = 1;
			}
			return;
		}
		if (!_started)
		{
			if (_elapsedTime < _delay)
			{
				return;
			}
			_started = true;
			CallStartCallback();
			if (_killed)
			{
				return;
			}
		}
		bool flag = false;
		float num = _elapsedTime - _delay;
		if (_breakpoint >= 0f && num >= _breakpoint)
		{
			num = _breakpoint;
			_ended = 2;
		}
		if (_repeat != 0)
		{
			int num2 = Mathf.FloorToInt(num / _duration);
			num -= _duration * (float)num2;
			if (_yoyo)
			{
				flag = num2 % 2 == 1;
			}
			if (_repeat > 0 && _repeat - num2 < 0)
			{
				if (_yoyo)
				{
					flag = _repeat % 2 == 1;
				}
				num = _duration;
				_ended = 1;
			}
		}
		else if (num >= _duration)
		{
			num = _duration;
			_ended = 1;
		}
		_normalizedTime = EaseManager.Evaluate(_easeType, flag ? (_duration - num) : num, _duration, _easeOvershootOrAmplitude, _easePeriod, _customEase);
		_value.SetZero();
		_deltaValue.SetZero();
		if (_valueSize == 5)
		{
			double num3 = _startValue.d + (_endValue.d - _startValue.d) * (double)_normalizedTime;
			if (_snapping)
			{
				num3 = Math.Round(num3);
			}
			_deltaValue.d = num3 - _value.d;
			_value.d = num3;
			_value.x = (float)num3;
		}
		else if (_valueSize == 6)
		{
			if (_ended == 0)
			{
				Vector3 insideUnitSphere = UnityEngine.Random.insideUnitSphere;
				insideUnitSphere.x = ((insideUnitSphere.x > 0f) ? 1 : (-1));
				insideUnitSphere.y = ((insideUnitSphere.y > 0f) ? 1 : (-1));
				insideUnitSphere.z = ((insideUnitSphere.z > 0f) ? 1 : (-1));
				insideUnitSphere *= _startValue.w * (1f - _normalizedTime);
				_deltaValue.vec3 = insideUnitSphere;
				_value.vec3 = _startValue.vec3 + insideUnitSphere;
			}
			else
			{
				_value.vec3 = _startValue.vec3;
			}
		}
		else if (_path != null)
		{
			Vector3 pointAt = _path.GetPointAt(_normalizedTime);
			if (_snapping)
			{
				pointAt.x = Mathf.Round(pointAt.x);
				pointAt.y = Mathf.Round(pointAt.y);
				pointAt.z = Mathf.Round(pointAt.z);
			}
			_deltaValue.vec3 = pointAt - _value.vec3;
			_value.vec3 = pointAt;
		}
		else
		{
			for (int i = 0; i < _valueSize; i++)
			{
				float num4 = _startValue[i];
				float num5 = _endValue[i];
				float num6 = num4 + (num5 - num4) * _normalizedTime;
				if (_snapping)
				{
					num6 = Mathf.Round(num6);
				}
				_deltaValue[i] = num6 - _value[i];
				_value[i] = num6;
			}
			_value.d = _value.x;
		}
		if (_target != null && _propType != TweenPropType.None)
		{
			TweenPropTypeUtils.SetProps(_target, _propType, _value);
		}
		CallUpdateCallback();
	}

	private void CallStartCallback()
	{
		if (GTween.catchCallbackExceptions)
		{
			try
			{
				if (_onStart1 != null)
				{
					_onStart1(this);
				}
				if (_onStart != null)
				{
					_onStart();
				}
				if (_listener != null)
				{
					_listener.OnTweenStart(this);
				}
				return;
			}
			catch (Exception ex)
			{
				Debug.LogWarning("FairyGUI: error in start callback > " + ex.Message);
				return;
			}
		}
		if (_onStart1 != null)
		{
			_onStart1(this);
		}
		if (_onStart != null)
		{
			_onStart();
		}
		if (_listener != null)
		{
			_listener.OnTweenStart(this);
		}
	}

	private void CallUpdateCallback()
	{
		if (GTween.catchCallbackExceptions)
		{
			try
			{
				if (_onUpdate1 != null)
				{
					_onUpdate1(this);
				}
				if (_onUpdate != null)
				{
					_onUpdate();
				}
				if (_listener != null)
				{
					_listener.OnTweenUpdate(this);
				}
				return;
			}
			catch (Exception ex)
			{
				Debug.LogWarning("FairyGUI: error in update callback > " + ex.Message);
				return;
			}
		}
		if (_onUpdate1 != null)
		{
			_onUpdate1(this);
		}
		if (_onUpdate != null)
		{
			_onUpdate();
		}
		if (_listener != null)
		{
			_listener.OnTweenUpdate(this);
		}
	}

	private void CallCompleteCallback()
	{
		if (GTween.catchCallbackExceptions)
		{
			try
			{
				if (_onComplete1 != null)
				{
					_onComplete1(this);
				}
				if (_onComplete != null)
				{
					_onComplete();
				}
				if (_listener != null)
				{
					_listener.OnTweenComplete(this);
				}
				return;
			}
			catch (Exception ex)
			{
				Debug.LogWarning("FairyGUI: error in complete callback > " + ex.Message);
				return;
			}
		}
		if (_onComplete1 != null)
		{
			_onComplete1(this);
		}
		if (_onComplete != null)
		{
			_onComplete();
		}
		if (_listener != null)
		{
			_listener.OnTweenComplete(this);
		}
	}
}
