using System;
using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class Transition : ITweenListener
{
	private Action OnDelayCompletedAction;

	public bool invalidateBatchingEveryFrame;

	private GComponent _owner;

	private TransitionItem[] _items;

	private int _totalTimes;

	private int _totalTasks;

	private bool _playing;

	private bool _paused;

	private float _ownerBaseX;

	private float _ownerBaseY;

	private PlayCompleteCallback _onComplete;

	private int _options;

	private bool _reversed;

	private float _totalDuration;

	private bool _autoPlay;

	private int _autoPlayTimes;

	private float _autoPlayDelay;

	private float _timeScale;

	private bool _ignoreEngineTimeScale;

	private float _startTime;

	private float _endTime;

	private GTweenCallback _delayedCallDelegate;

	private GTweenCallback _checkAllDelegate;

	private GTweenCallback1 _delayedCallDelegate2;

	private const int OPTION_IGNORE_DISPLAY_CONTROLLER = 1;

	private const int OPTION_AUTO_STOP_DISABLED = 2;

	private const int OPTION_AUTO_STOP_AT_END = 4;

	public string name { get; private set; }

	public bool playing => _playing;

	public float timeScale
	{
		get
		{
			return _timeScale;
		}
		set
		{
			if (_timeScale == value)
			{
				return;
			}
			_timeScale = value;
			int num = _items.Length;
			for (int i = 0; i < num; i++)
			{
				TransitionItem transitionItem = _items[i];
				if (transitionItem.tweener != null)
				{
					transitionItem.tweener.SetTimeScale(value);
				}
				else if (transitionItem.type == TransitionActionType.Transition)
				{
					if (((TValue_Transition)transitionItem.value).trans != null)
					{
						((TValue_Transition)transitionItem.value).trans.timeScale = value;
					}
				}
				else if (transitionItem.type == TransitionActionType.Animation && transitionItem.target != null)
				{
					((IAnimationGear)transitionItem.target).timeScale = value;
				}
			}
		}
	}

	public bool ignoreEngineTimeScale
	{
		get
		{
			return _ignoreEngineTimeScale;
		}
		set
		{
			if (_ignoreEngineTimeScale == value)
			{
				return;
			}
			_ignoreEngineTimeScale = value;
			int num = _items.Length;
			for (int i = 0; i < num; i++)
			{
				TransitionItem transitionItem = _items[i];
				if (transitionItem.tweener != null)
				{
					transitionItem.tweener.SetIgnoreEngineTimeScale(value);
				}
				else if (transitionItem.type == TransitionActionType.Transition)
				{
					if (((TValue_Transition)transitionItem.value).trans != null)
					{
						((TValue_Transition)transitionItem.value).trans.ignoreEngineTimeScale = value;
					}
				}
				else if (transitionItem.type == TransitionActionType.Animation && transitionItem.target != null)
				{
					((IAnimationGear)transitionItem.target).ignoreEngineTimeScale = value;
				}
			}
		}
	}

	public TransitionAsyncResult PlayYield()
	{
		TransitionAsyncResult transitionAsyncResult = new TransitionAsyncResult();
		Play(transitionAsyncResult.Complete);
		return transitionAsyncResult;
	}

	public void Play(int times, float delay, Action onDelayCompletedAction, PlayCompleteCallback onComplete)
	{
		OnDelayCompletedAction = onDelayCompletedAction;
		_Play(times, delay, 0f, -1f, onComplete, reverse: false);
	}

	public Transition(GComponent owner)
	{
		_owner = owner;
		_timeScale = 1f;
		_ignoreEngineTimeScale = true;
		_delayedCallDelegate = OnDelayedPlay;
		_delayedCallDelegate2 = OnDelayedPlayItem;
		_checkAllDelegate = CheckAllComplete;
	}

	public void Play()
	{
		_Play(1, 0f, 0f, -1f, null, reverse: false);
	}

	public void Play(PlayCompleteCallback onComplete)
	{
		_Play(1, 0f, 0f, -1f, onComplete, reverse: false);
	}

	public void Play(int times, float delay, PlayCompleteCallback onComplete)
	{
		_Play(times, delay, 0f, -1f, onComplete, reverse: false);
	}

	public void Play(int times, float delay, float startTime, float endTime, PlayCompleteCallback onComplete)
	{
		_Play(times, delay, startTime, endTime, onComplete, reverse: false);
	}

	public void PlayReverse()
	{
		_Play(1, 0f, 0f, -1f, null, reverse: true);
	}

	public void PlayReverse(PlayCompleteCallback onComplete)
	{
		_Play(1, 0f, 0f, -1f, onComplete, reverse: true);
	}

	public void PlayReverse(int times, float delay, PlayCompleteCallback onComplete)
	{
		_Play(times, delay, 0f, -1f, onComplete, reverse: true);
	}

	public void ChangePlayTimes(int value)
	{
		_totalTimes = value;
	}

	public void SetAutoPlay(bool autoPlay, int times, float delay)
	{
		if (_autoPlay == autoPlay)
		{
			return;
		}
		_autoPlay = autoPlay;
		_autoPlayTimes = times;
		_autoPlayDelay = delay;
		if (_autoPlay)
		{
			if (_owner.onStage)
			{
				Play(times, delay, null);
			}
		}
		else if (!_owner.onStage)
		{
			Stop(setToComplete: false, processCallback: true);
		}
	}

	private void _Play(int times, float delay, float startTime, float endTime, PlayCompleteCallback onComplete, bool reverse)
	{
		invalidateBatchingEveryFrame = true;
		Stop(setToComplete: true, processCallback: true);
		_totalTimes = times;
		_reversed = reverse;
		_startTime = startTime;
		_endTime = endTime;
		_playing = true;
		_paused = false;
		_onComplete = onComplete;
		int num = _items.Length;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (transitionItem.target == null)
			{
				if (transitionItem.targetId.Length > 0)
				{
					transitionItem.target = _owner.GetChildById(transitionItem.targetId);
				}
				else
				{
					transitionItem.target = _owner;
				}
			}
			else if (transitionItem.target != _owner && transitionItem.target.parent != _owner)
			{
				transitionItem.target = null;
			}
			if (transitionItem.target == null || transitionItem.type != TransitionActionType.Transition)
			{
				continue;
			}
			TValue_Transition tValue_Transition = (TValue_Transition)transitionItem.value;
			Transition transition = ((GComponent)transitionItem.target).GetTransition(tValue_Transition.transName);
			if (transition == this)
			{
				transition = null;
			}
			if (transition != null)
			{
				if (tValue_Transition.playTimes == 0)
				{
					int num2;
					for (num2 = i - 1; num2 >= 0; num2--)
					{
						TransitionItem transitionItem2 = _items[num2];
						if (transitionItem2.type == TransitionActionType.Transition)
						{
							TValue_Transition tValue_Transition2 = (TValue_Transition)transitionItem2.value;
							if (tValue_Transition2.trans == transition)
							{
								tValue_Transition2.stopTime = transitionItem.time - transitionItem2.time;
								break;
							}
						}
					}
					if (num2 < 0)
					{
						tValue_Transition.stopTime = 0f;
					}
					else
					{
						transition = null;
					}
				}
				else
				{
					tValue_Transition.stopTime = -1f;
				}
			}
			tValue_Transition.trans = transition;
		}
		if (delay == 0f)
		{
			OnDelayedPlay();
		}
		else
		{
			GTween.DelayedCall(delay).SetTarget(this).OnComplete(_delayedCallDelegate);
		}
	}

	public void Stop()
	{
		Stop(setToComplete: true, processCallback: false);
	}

	public void Stop(bool setToComplete, bool processCallback)
	{
		if (!_playing)
		{
			return;
		}
		_playing = false;
		_totalTasks = 0;
		_totalTimes = 0;
		PlayCompleteCallback onComplete = _onComplete;
		_onComplete = null;
		GTween.Kill(this);
		int num = _items.Length;
		if (_reversed)
		{
			for (int num2 = num - 1; num2 >= 0; num2--)
			{
				TransitionItem transitionItem = _items[num2];
				if (transitionItem.target != null)
				{
					StopItem(transitionItem, setToComplete);
				}
			}
		}
		else
		{
			for (int i = 0; i < num; i++)
			{
				TransitionItem transitionItem2 = _items[i];
				if (transitionItem2.target != null)
				{
					StopItem(transitionItem2, setToComplete);
				}
			}
		}
		if (processCallback)
		{
			onComplete?.Invoke();
		}
	}

	private void StopItem(TransitionItem item, bool setToComplete)
	{
		if (item.displayLockToken != 0)
		{
			item.target.ReleaseDisplayLock(item.displayLockToken);
			item.displayLockToken = 0u;
		}
		if (item.tweener != null)
		{
			item.tweener.Kill(setToComplete);
			item.tweener = null;
			if (item.type == TransitionActionType.Shake && !setToComplete)
			{
				item.target._gearLocked = true;
				item.target.SetXY(item.target.x - ((TValue_Shake)item.value).lastOffset.x, item.target.y - ((TValue_Shake)item.value).lastOffset.y);
				item.target._gearLocked = false;
				_owner.InvalidateBatchingState(childChanged: true);
			}
		}
		if (item.type == TransitionActionType.Transition)
		{
			TValue_Transition tValue_Transition = (TValue_Transition)item.value;
			if (tValue_Transition.trans != null)
			{
				tValue_Transition.trans.Stop(setToComplete, processCallback: false);
			}
		}
	}

	public void SetPaused(bool paused)
	{
		if (!_playing || _paused == paused)
		{
			return;
		}
		_paused = paused;
		GTween.GetTween(this)?.SetPaused(paused);
		int num = _items.Length;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (transitionItem.target == null)
			{
				continue;
			}
			if (transitionItem.type == TransitionActionType.Transition)
			{
				if (((TValue_Transition)transitionItem.value).trans != null)
				{
					((TValue_Transition)transitionItem.value).trans.SetPaused(paused);
				}
			}
			else if (transitionItem.type == TransitionActionType.Animation)
			{
				if (paused)
				{
					((TValue_Animation)transitionItem.value).flag = ((IAnimationGear)transitionItem.target).playing;
					((IAnimationGear)transitionItem.target).playing = false;
				}
				else
				{
					((IAnimationGear)transitionItem.target).playing = ((TValue_Animation)transitionItem.value).flag;
				}
			}
			if (transitionItem.tweener != null)
			{
				transitionItem.tweener.SetPaused(paused);
			}
		}
	}

	public void Dispose()
	{
		if (_playing)
		{
			GTween.Kill(this);
		}
		int num = _items.Length;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (transitionItem.tweener != null)
			{
				transitionItem.tweener.Kill();
				transitionItem.tweener = null;
			}
			transitionItem.target = null;
			transitionItem.hook = null;
			if (transitionItem.tweenConfig != null)
			{
				transitionItem.tweenConfig.endHook = null;
			}
		}
		_playing = false;
		_onComplete = null;
	}

	public void SetValue(string label, params object[] aParams)
	{
		int num = _items.Length;
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			object obj;
			if (transitionItem.label == label)
			{
				obj = ((transitionItem.tweenConfig == null) ? transitionItem.value : transitionItem.tweenConfig.startValue);
				flag = true;
			}
			else
			{
				if (transitionItem.tweenConfig == null || !(transitionItem.tweenConfig.endLabel == label))
				{
					continue;
				}
				obj = transitionItem.tweenConfig.endValue;
				flag = true;
			}
			switch (transitionItem.type)
			{
			case TransitionActionType.XY:
			case TransitionActionType.Size:
			case TransitionActionType.Scale:
			case TransitionActionType.Pivot:
			case TransitionActionType.Skew:
			{
				TValue obj3 = (TValue)obj;
				obj3.b1 = true;
				obj3.b2 = true;
				obj3.f1 = Convert.ToSingle(aParams[0]);
				obj3.f2 = Convert.ToSingle(aParams[1]);
				break;
			}
			case TransitionActionType.Alpha:
				((TValue)obj).f1 = Convert.ToSingle(aParams[0]);
				break;
			case TransitionActionType.Rotation:
				((TValue)obj).f1 = Convert.ToSingle(aParams[0]);
				break;
			case TransitionActionType.Color:
				((TValue)obj).color = (Color)aParams[0];
				break;
			case TransitionActionType.Animation:
			{
				TValue_Animation tValue_Animation = (TValue_Animation)obj;
				tValue_Animation.frame = Convert.ToInt32(aParams[0]);
				if (aParams.Length > 1)
				{
					tValue_Animation.playing = Convert.ToBoolean(aParams[1]);
				}
				break;
			}
			case TransitionActionType.Visible:
				((TValue_Visible)obj).visible = Convert.ToBoolean(aParams[0]);
				break;
			case TransitionActionType.Sound:
			{
				TValue_Sound tValue_Sound = (TValue_Sound)obj;
				string value = aParams[0] as string;
				if (!string.IsNullOrEmpty(value))
				{
					tValue_Sound.sound = Convert.ToInt32(value);
				}
				if (aParams.Length > 1)
				{
					tValue_Sound.volume = Convert.ToSingle(aParams[1]);
				}
				break;
			}
			case TransitionActionType.Transition:
			{
				TValue_Transition tValue_Transition = (TValue_Transition)obj;
				tValue_Transition.transName = (string)aParams[0];
				if (aParams.Length > 1)
				{
					tValue_Transition.playTimes = Convert.ToInt32(aParams[1]);
				}
				break;
			}
			case TransitionActionType.Shake:
				((TValue_Shake)obj).amplitude = Convert.ToSingle(aParams[0]);
				if (aParams.Length > 1)
				{
					((TValue_Shake)obj).duration = Convert.ToSingle(aParams[1]);
				}
				break;
			case TransitionActionType.ColorFilter:
			{
				TValue obj2 = (TValue)obj;
				obj2.f1 = Convert.ToSingle(aParams[0]);
				obj2.f2 = Convert.ToSingle(aParams[1]);
				obj2.f3 = Convert.ToSingle(aParams[2]);
				obj2.f4 = Convert.ToSingle(aParams[3]);
				break;
			}
			case TransitionActionType.Text:
			case TransitionActionType.Icon:
				((TValue_Text)obj).text = (string)aParams[0];
				break;
			}
		}
		if (!flag)
		{
			throw new Exception("label not exists");
		}
	}

	public void SetHook(string label, TransitionHook callback)
	{
		int num = _items.Length;
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (transitionItem.label == label)
			{
				transitionItem.hook = callback;
				flag = true;
				break;
			}
			if (transitionItem.tweenConfig != null && transitionItem.tweenConfig.endLabel == label)
			{
				transitionItem.tweenConfig.endHook = callback;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			throw new Exception("label not exists");
		}
	}

	public void ClearHooks()
	{
		int num = _items.Length;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			transitionItem.hook = null;
			if (transitionItem.tweenConfig != null)
			{
				transitionItem.tweenConfig.endHook = null;
			}
		}
	}

	public void SetTarget(string label, GObject newTarget)
	{
		int num = _items.Length;
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (!(transitionItem.label == label))
			{
				continue;
			}
			transitionItem.targetId = ((newTarget == _owner || newTarget == null) ? string.Empty : newTarget.id);
			if (_playing)
			{
				if (transitionItem.targetId.Length > 0)
				{
					transitionItem.target = _owner.GetChildById(transitionItem.targetId);
				}
				else
				{
					transitionItem.target = _owner;
				}
			}
			else
			{
				transitionItem.target = null;
			}
			flag = true;
		}
		if (!flag)
		{
			throw new Exception("label not exists");
		}
	}

	public void SetDuration(string label, float value)
	{
		int num = _items.Length;
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (transitionItem.tweenConfig != null && transitionItem.label == label)
			{
				transitionItem.tweenConfig.duration = value;
				flag = true;
			}
		}
		if (!flag)
		{
			throw new Exception("label not exists or not a tween label");
		}
	}

	public float GetLabelTime(string label)
	{
		int num = _items.Length;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (transitionItem.label == label)
			{
				return transitionItem.time;
			}
			if (transitionItem.tweenConfig != null && transitionItem.tweenConfig.endLabel == label)
			{
				return transitionItem.time + transitionItem.tweenConfig.duration;
			}
		}
		return float.NaN;
	}

	internal void UpdateFromRelations(string targetId, float dx, float dy)
	{
		int num = _items.Length;
		if (num == 0)
		{
			return;
		}
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (transitionItem.type != TransitionActionType.XY || !(transitionItem.targetId == targetId))
			{
				continue;
			}
			if (transitionItem.tweenConfig != null)
			{
				if (!transitionItem.tweenConfig.startValue.b3)
				{
					transitionItem.tweenConfig.startValue.f1 += dx;
					transitionItem.tweenConfig.startValue.f2 += dy;
				}
				if (!transitionItem.tweenConfig.endValue.b3)
				{
					transitionItem.tweenConfig.endValue.f1 += dx;
					transitionItem.tweenConfig.endValue.f2 += dy;
				}
			}
			else if (!((TValue)transitionItem.value).b3)
			{
				((TValue)transitionItem.value).f1 += dx;
				((TValue)transitionItem.value).f2 += dy;
			}
		}
	}

	internal void OnOwnerAddedToStage()
	{
		if (_autoPlay && !_playing)
		{
			Play(_autoPlayTimes, _autoPlayDelay, null);
		}
	}

	internal void OnOwnerRemovedFromStage()
	{
		if ((_options & 2) == 0)
		{
			Stop(((_options & 4) != 0) ? true : false, processCallback: false);
		}
	}

	private void OnDelayedPlay()
	{
		InternalPlay();
		OnDelayCompletedAction?.Invoke();
		OnDelayCompletedAction = null;
		_playing = _totalTasks > 0;
		if (_playing)
		{
			if ((_options & 1) == 0)
			{
				return;
			}
			int num = _items.Length;
			for (int i = 0; i < num; i++)
			{
				TransitionItem transitionItem = _items[i];
				if (transitionItem.target != null && transitionItem.target != _owner)
				{
					transitionItem.displayLockToken = transitionItem.target.AddDisplayLock();
				}
			}
		}
		else if (_onComplete != null)
		{
			PlayCompleteCallback onComplete = _onComplete;
			_onComplete = null;
			onComplete();
		}
	}

	private void InternalPlay()
	{
		_ownerBaseX = _owner.x;
		_ownerBaseY = _owner.y;
		_totalTasks = 1;
		bool flag = false;
		int num = _items.Length;
		if (!_reversed)
		{
			for (int i = 0; i < num; i++)
			{
				TransitionItem transitionItem = _items[i];
				if (transitionItem.target != null)
				{
					if (transitionItem.type == TransitionActionType.Animation && _startTime != 0f && transitionItem.time <= _startTime)
					{
						flag = true;
						((TValue_Animation)transitionItem.value).flag = false;
					}
					else
					{
						PlayItem(transitionItem);
					}
				}
			}
		}
		else
		{
			for (int num2 = num - 1; num2 >= 0; num2--)
			{
				TransitionItem transitionItem2 = _items[num2];
				if (transitionItem2.target != null)
				{
					PlayItem(transitionItem2);
				}
			}
		}
		if (flag)
		{
			SkipAnimations();
		}
		_totalTasks--;
	}

	private void PlayItem(TransitionItem item)
	{
		if (item.tweenConfig != null)
		{
			float num = ((!_reversed) ? item.time : (_totalDuration - item.time - item.tweenConfig.duration));
			if (_endTime == -1f || num <= _endTime)
			{
				TValue tValue;
				TValue tValue2;
				if (_reversed)
				{
					tValue = item.tweenConfig.endValue;
					tValue2 = item.tweenConfig.startValue;
				}
				else
				{
					tValue = item.tweenConfig.startValue;
					tValue2 = item.tweenConfig.endValue;
				}
				((TValue)item.value).b1 = tValue.b1 || tValue2.b1;
				((TValue)item.value).b2 = tValue.b2 || tValue2.b2;
				switch (item.type)
				{
				case TransitionActionType.XY:
				case TransitionActionType.Size:
				case TransitionActionType.Scale:
				case TransitionActionType.Skew:
					item.tweener = GTween.To(tValue.vec2, tValue2.vec2, item.tweenConfig.duration);
					break;
				case TransitionActionType.Alpha:
				case TransitionActionType.Rotation:
					item.tweener = GTween.To(tValue.f1, tValue2.f1, item.tweenConfig.duration);
					break;
				case TransitionActionType.Color:
					item.tweener = GTween.To(tValue.color, tValue2.color, item.tweenConfig.duration);
					break;
				case TransitionActionType.ColorFilter:
					item.tweener = GTween.To(tValue.vec4, tValue2.vec4, item.tweenConfig.duration);
					break;
				}
				item.tweener.SetDelay(num).SetEase(item.tweenConfig.easeType, item.tweenConfig.customEase).SetRepeat(item.tweenConfig.repeat, item.tweenConfig.yoyo)
					.SetTimeScale(_timeScale)
					.SetIgnoreEngineTimeScale(_ignoreEngineTimeScale)
					.SetTarget(item)
					.SetListener(this);
				if (_endTime >= 0f)
				{
					item.tweener.SetBreakpoint(_endTime - num);
				}
				_totalTasks++;
			}
		}
		else if (item.type == TransitionActionType.Shake)
		{
			TValue_Shake tValue_Shake = (TValue_Shake)item.value;
			float num = ((!_reversed) ? item.time : (_totalDuration - item.time - tValue_Shake.duration));
			if (_endTime == -1f || num <= _endTime)
			{
				tValue_Shake.lastOffset.Set(0f, 0f);
				tValue_Shake.offset.Set(0f, 0f);
				item.tweener = GTween.Shake(Vector3.zero, tValue_Shake.amplitude, tValue_Shake.duration).SetDelay(num).SetTimeScale(_timeScale)
					.SetIgnoreEngineTimeScale(_ignoreEngineTimeScale)
					.SetTarget(item)
					.SetListener(this);
				if (_endTime >= 0f)
				{
					item.tweener.SetBreakpoint(_endTime - item.time);
				}
				_totalTasks++;
			}
		}
		else
		{
			float num = ((!_reversed) ? item.time : (_totalDuration - item.time));
			if (num <= _startTime)
			{
				ApplyValue(item);
				CallHook(item, tweenEnd: false);
			}
			else if (_endTime == -1f || num <= _endTime)
			{
				_totalTasks++;
				item.tweener = GTween.DelayedCall(num).SetTimeScale(_timeScale).SetIgnoreEngineTimeScale(_ignoreEngineTimeScale)
					.SetTarget(item)
					.OnComplete(_delayedCallDelegate2);
			}
		}
		if (item.tweener != null)
		{
			item.tweener.Seek(_startTime);
		}
	}

	private void SkipAnimations()
	{
		int num = _items.Length;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (transitionItem.type != TransitionActionType.Animation || transitionItem.time > _startTime)
			{
				continue;
			}
			TValue_Animation tValue_Animation = (TValue_Animation)transitionItem.value;
			if (tValue_Animation.flag)
			{
				continue;
			}
			IAnimationGear animationGear = (IAnimationGear)transitionItem.target;
			int frame = animationGear.frame;
			float num2 = ((!animationGear.playing) ? (-1) : 0);
			float num3 = 0f;
			for (int j = i; j < num; j++)
			{
				transitionItem = _items[j];
				if (transitionItem.type != TransitionActionType.Animation || transitionItem.target != animationGear || transitionItem.time > _startTime)
				{
					continue;
				}
				tValue_Animation = (TValue_Animation)transitionItem.value;
				tValue_Animation.flag = true;
				if (tValue_Animation.frame != -1)
				{
					frame = tValue_Animation.frame;
					num2 = ((!tValue_Animation.playing) ? (-1f) : transitionItem.time);
					num3 = 0f;
				}
				else if (tValue_Animation.playing)
				{
					if (num2 < 0f)
					{
						num2 = transitionItem.time;
					}
				}
				else
				{
					if (num2 >= 0f)
					{
						num3 += transitionItem.time - num2;
					}
					num2 = -1f;
				}
				CallHook(transitionItem, tweenEnd: false);
			}
			if (num2 >= 0f)
			{
				num3 += _startTime - num2;
			}
			animationGear.playing = num2 >= 0f;
			animationGear.frame = frame;
			if (num3 > 0f)
			{
				animationGear.Advance(num3);
			}
		}
	}

	private void OnDelayedPlayItem(GTweener tweener)
	{
		TransitionItem transitionItem = (TransitionItem)tweener.target;
		transitionItem.tweener = null;
		_totalTasks--;
		ApplyValue(transitionItem);
		CallHook(transitionItem, tweenEnd: false);
		CheckAllComplete();
	}

	public void OnTweenStart(GTweener tweener)
	{
		TransitionItem transitionItem = (TransitionItem)tweener.target;
		if (transitionItem.type == TransitionActionType.XY || transitionItem.type == TransitionActionType.Size)
		{
			TValue tValue;
			TValue tValue2;
			if (_reversed)
			{
				tValue = transitionItem.tweenConfig.endValue;
				tValue2 = transitionItem.tweenConfig.startValue;
			}
			else
			{
				tValue = transitionItem.tweenConfig.startValue;
				tValue2 = transitionItem.tweenConfig.endValue;
			}
			if (transitionItem.type == TransitionActionType.XY)
			{
				if (transitionItem.target != _owner)
				{
					if (!tValue.b1)
					{
						tweener.startValue.x = transitionItem.target.x;
					}
					else if (tValue.b3)
					{
						tweener.startValue.x = tValue.f1 * _owner.width;
					}
					if (!tValue.b2)
					{
						tweener.startValue.y = transitionItem.target.y;
					}
					else if (tValue.b3)
					{
						tweener.startValue.y = tValue.f2 * _owner.height;
					}
					if (!tValue2.b1)
					{
						tweener.endValue.x = tweener.startValue.x;
					}
					else if (tValue2.b3)
					{
						tweener.endValue.x = tValue2.f1 * _owner.width;
					}
					if (!tValue2.b2)
					{
						tweener.endValue.y = tweener.startValue.y;
					}
					else if (tValue2.b3)
					{
						tweener.endValue.y = tValue2.f2 * _owner.height;
					}
				}
				else
				{
					if (!tValue.b1)
					{
						tweener.startValue.x = transitionItem.target.x - _ownerBaseX;
					}
					if (!tValue.b2)
					{
						tweener.startValue.y = transitionItem.target.y - _ownerBaseY;
					}
					if (!tValue2.b1)
					{
						tweener.endValue.x = tweener.startValue.x;
					}
					if (!tValue2.b2)
					{
						tweener.endValue.y = tweener.startValue.y;
					}
				}
			}
			else
			{
				if (!tValue.b1)
				{
					tweener.startValue.x = transitionItem.target.width;
				}
				if (!tValue.b2)
				{
					tweener.startValue.y = transitionItem.target.height;
				}
				if (!tValue2.b1)
				{
					tweener.endValue.x = tweener.startValue.x;
				}
				if (!tValue2.b2)
				{
					tweener.endValue.y = tweener.startValue.y;
				}
			}
			if (transitionItem.tweenConfig.path != null)
			{
				((TValue)transitionItem.value).b1 = (((TValue)transitionItem.value).b2 = true);
				tweener.SetPath(transitionItem.tweenConfig.path);
			}
		}
		CallHook(transitionItem, tweenEnd: false);
	}

	public void OnTweenUpdate(GTweener tweener)
	{
		TransitionItem transitionItem = (TransitionItem)tweener.target;
		switch (transitionItem.type)
		{
		case TransitionActionType.XY:
		case TransitionActionType.Size:
		case TransitionActionType.Scale:
		case TransitionActionType.Skew:
			((TValue)transitionItem.value).vec2 = tweener.value.vec2;
			if (transitionItem.tweenConfig.path != null)
			{
				((TValue)transitionItem.value).f1 += tweener.startValue.x;
				((TValue)transitionItem.value).f2 += tweener.startValue.y;
			}
			break;
		case TransitionActionType.Alpha:
		case TransitionActionType.Rotation:
			((TValue)transitionItem.value).f1 = tweener.value.x;
			break;
		case TransitionActionType.Color:
			((TValue)transitionItem.value).color = tweener.value.color;
			break;
		case TransitionActionType.ColorFilter:
			((TValue)transitionItem.value).vec4 = tweener.value.vec4;
			break;
		case TransitionActionType.Shake:
			((TValue_Shake)transitionItem.value).offset = tweener.deltaValue.vec2;
			break;
		}
		ApplyValue(transitionItem);
	}

	public void OnTweenComplete(GTweener tweener)
	{
		TransitionItem transitionItem = (TransitionItem)tweener.target;
		transitionItem.tweener = null;
		_totalTasks--;
		if (tweener.allCompleted)
		{
			CallHook(transitionItem, tweenEnd: true);
		}
		CheckAllComplete();
	}

	private void OnPlayTransCompleted(TransitionItem item)
	{
		_totalTasks--;
		CheckAllComplete();
	}

	private void CallHook(TransitionItem item, bool tweenEnd)
	{
		if (tweenEnd)
		{
			if (item.tweenConfig != null && item.tweenConfig.endHook != null)
			{
				item.tweenConfig.endHook();
			}
		}
		else if (item.time >= _startTime && item.hook != null)
		{
			item.hook();
		}
	}

	private void CheckAllComplete()
	{
		if (!_playing || _totalTasks != 0)
		{
			return;
		}
		if (_totalTimes < 0)
		{
			InternalPlay();
			if (_totalTasks == 0)
			{
				GTween.DelayedCall(0f).SetTarget(this).OnComplete(_checkAllDelegate);
			}
			return;
		}
		_totalTimes--;
		if (_totalTimes > 0)
		{
			InternalPlay();
			if (_totalTasks == 0)
			{
				GTween.DelayedCall(0f).SetTarget(this).OnComplete(_checkAllDelegate);
			}
			return;
		}
		_playing = false;
		int num = _items.Length;
		for (int i = 0; i < num; i++)
		{
			TransitionItem transitionItem = _items[i];
			if (transitionItem.target != null && transitionItem.displayLockToken != 0)
			{
				transitionItem.target.ReleaseDisplayLock(transitionItem.displayLockToken);
				transitionItem.displayLockToken = 0u;
			}
		}
		if (_onComplete != null)
		{
			PlayCompleteCallback onComplete = _onComplete;
			_onComplete = null;
			onComplete();
		}
	}

	private void ApplyValue(TransitionItem item)
	{
		item.target._gearLocked = true;
		switch (item.type)
		{
		case TransitionActionType.XY:
		{
			TValue tValue3 = (TValue)item.value;
			if (item.target == _owner)
			{
				if (tValue3.b1 && tValue3.b2)
				{
					item.target.SetXY(tValue3.f1 + _ownerBaseX, tValue3.f2 + _ownerBaseY);
				}
				else if (tValue3.b1)
				{
					item.target.x = tValue3.f1 + _ownerBaseX;
				}
				else
				{
					item.target.y = tValue3.f2 + _ownerBaseY;
				}
			}
			else if (tValue3.b3)
			{
				if (tValue3.b1 && tValue3.b2)
				{
					item.target.SetXY(tValue3.f1 * _owner.width, tValue3.f2 * _owner.height);
				}
				else if (tValue3.b1)
				{
					item.target.x = tValue3.f1 * _owner.width;
				}
				else if (tValue3.b2)
				{
					item.target.y = tValue3.f2 * _owner.height;
				}
			}
			else if (tValue3.b1 && tValue3.b2)
			{
				item.target.SetXY(tValue3.f1, tValue3.f2);
			}
			else if (tValue3.b1)
			{
				item.target.x = tValue3.f1;
			}
			else if (tValue3.b2)
			{
				item.target.y = tValue3.f2;
			}
			if (invalidateBatchingEveryFrame)
			{
				_owner.InvalidateBatchingState(childChanged: true);
			}
			break;
		}
		case TransitionActionType.Size:
		{
			TValue tValue2 = (TValue)item.value;
			if (!tValue2.b1)
			{
				tValue2.f1 = item.target.width;
			}
			if (!tValue2.b2)
			{
				tValue2.f2 = item.target.height;
			}
			item.target.SetSize(tValue2.f1, tValue2.f2);
			if (invalidateBatchingEveryFrame)
			{
				_owner.InvalidateBatchingState(childChanged: true);
			}
			break;
		}
		case TransitionActionType.Pivot:
			item.target.SetPivot(((TValue)item.value).f1, ((TValue)item.value).f2, item.target.pivotAsAnchor);
			if (invalidateBatchingEveryFrame)
			{
				_owner.InvalidateBatchingState(childChanged: true);
			}
			break;
		case TransitionActionType.Alpha:
			item.target.alpha = ((TValue)item.value).f1;
			break;
		case TransitionActionType.Rotation:
			item.target.rotation = ((TValue)item.value).f1;
			if (invalidateBatchingEveryFrame)
			{
				_owner.InvalidateBatchingState(childChanged: true);
			}
			break;
		case TransitionActionType.Scale:
			item.target.SetScale(((TValue)item.value).f1, ((TValue)item.value).f2);
			if (invalidateBatchingEveryFrame)
			{
				_owner.InvalidateBatchingState(childChanged: true);
			}
			break;
		case TransitionActionType.Skew:
			item.target.skew = ((TValue)item.value).vec2;
			if (invalidateBatchingEveryFrame)
			{
				_owner.InvalidateBatchingState(childChanged: true);
			}
			break;
		case TransitionActionType.Color:
			((IColorGear)item.target).color = ((TValue)item.value).color;
			break;
		case TransitionActionType.Animation:
		{
			TValue_Animation tValue_Animation = (TValue_Animation)item.value;
			if (tValue_Animation.frame >= 0)
			{
				((IAnimationGear)item.target).frame = tValue_Animation.frame;
			}
			((IAnimationGear)item.target).playing = tValue_Animation.playing;
			((IAnimationGear)item.target).timeScale = _timeScale;
			((IAnimationGear)item.target).ignoreEngineTimeScale = _ignoreEngineTimeScale;
			break;
		}
		case TransitionActionType.Visible:
			item.target.visible = ((TValue_Visible)item.value).visible;
			break;
		case TransitionActionType.Shake:
		{
			TValue_Shake tValue_Shake = (TValue_Shake)item.value;
			item.target.SetXY(item.target.x - tValue_Shake.lastOffset.x + tValue_Shake.offset.x, item.target.y - tValue_Shake.lastOffset.y + tValue_Shake.offset.y);
			tValue_Shake.lastOffset = tValue_Shake.offset;
			if (invalidateBatchingEveryFrame)
			{
				_owner.InvalidateBatchingState(childChanged: true);
			}
			break;
		}
		case TransitionActionType.Transition:
		{
			if (!_playing)
			{
				break;
			}
			TValue_Transition tValue_Transition = (TValue_Transition)item.value;
			if (tValue_Transition.trans != null)
			{
				_totalTasks++;
				float startTime = ((_startTime > item.time) ? (_startTime - item.time) : 0f);
				float num = ((_endTime >= 0f) ? (_endTime - item.time) : (-1f));
				if (tValue_Transition.stopTime >= 0f && (num < 0f || num > tValue_Transition.stopTime))
				{
					num = tValue_Transition.stopTime;
				}
				tValue_Transition.trans.timeScale = _timeScale;
				tValue_Transition.trans.ignoreEngineTimeScale = _ignoreEngineTimeScale;
				tValue_Transition.trans._Play(tValue_Transition.playTimes, 0f, startTime, num, tValue_Transition.playCompleteDelegate, _reversed);
			}
			break;
		}
		case TransitionActionType.Sound:
			if (_playing && item.time >= _startTime)
			{
				TValue_Sound tValue_Sound = (TValue_Sound)item.value;
				if ((long)tValue_Sound.sound > 0L)
				{
					Stage.inst.PlayOneShotSound(tValue_Sound.sound);
				}
			}
			break;
		case TransitionActionType.ColorFilter:
		{
			TValue tValue = (TValue)item.value;
			ColorFilter colorFilter = item.target.filter as ColorFilter;
			if (colorFilter == null)
			{
				colorFilter = new ColorFilter();
				item.target.filter = colorFilter;
			}
			else
			{
				colorFilter.Reset();
			}
			colorFilter.AdjustBrightness(tValue.f1);
			colorFilter.AdjustContrast(tValue.f2);
			colorFilter.AdjustSaturation(tValue.f3);
			colorFilter.AdjustHue(tValue.f4);
			break;
		}
		case TransitionActionType.Text:
			item.target.text = ((TValue_Text)item.value).text;
			break;
		case TransitionActionType.Icon:
			item.target.icon = ((TValue_Text)item.value).text;
			break;
		}
		item.target._gearLocked = false;
	}

	public void Setup(ByteBuffer buffer)
	{
		name = buffer.ReadS();
		_options = buffer.ReadInt();
		_autoPlay = buffer.ReadBool();
		_autoPlayTimes = buffer.ReadInt();
		_autoPlayDelay = buffer.ReadFloat();
		int num = buffer.ReadShort();
		_items = new TransitionItem[num];
		for (int i = 0; i < num; i++)
		{
			int num2 = buffer.ReadShort();
			int position = buffer.position;
			buffer.Seek(position, 0);
			TransitionItem transitionItem = new TransitionItem((TransitionActionType)buffer.ReadByte());
			_items[i] = transitionItem;
			transitionItem.time = buffer.ReadFloat();
			int num3 = buffer.ReadShort();
			if (num3 < 0)
			{
				transitionItem.targetId = string.Empty;
			}
			else
			{
				transitionItem.targetId = _owner.GetChildAt(num3).id;
			}
			transitionItem.label = buffer.ReadS();
			if (buffer.ReadBool())
			{
				buffer.Seek(position, 1);
				transitionItem.tweenConfig = new TweenConfig();
				transitionItem.tweenConfig.duration = buffer.ReadFloat();
				if (transitionItem.time + transitionItem.tweenConfig.duration > _totalDuration)
				{
					_totalDuration = transitionItem.time + transitionItem.tweenConfig.duration;
				}
				transitionItem.tweenConfig.easeType = (EaseType)buffer.ReadByte();
				transitionItem.tweenConfig.repeat = buffer.ReadInt();
				transitionItem.tweenConfig.yoyo = buffer.ReadBool();
				transitionItem.tweenConfig.endLabel = buffer.ReadS();
				buffer.Seek(position, 2);
				DecodeValue(transitionItem, buffer, transitionItem.tweenConfig.startValue);
				buffer.Seek(position, 3);
				DecodeValue(transitionItem, buffer, transitionItem.tweenConfig.endValue);
				if (buffer.version >= 2)
				{
					List<GPathPoint> list = buffer.ReadPath();
					if (list.Count > 0)
					{
						transitionItem.tweenConfig.path = new GPath();
						transitionItem.tweenConfig.path.Create(list);
					}
				}
				if (buffer.version >= 4 && transitionItem.tweenConfig.easeType == EaseType.Custom)
				{
					List<GPathPoint> list2 = buffer.ReadPath();
					if (list2.Count > 0)
					{
						transitionItem.tweenConfig.customEase = new CustomEase();
						transitionItem.tweenConfig.customEase.Create(list2);
					}
				}
			}
			else
			{
				if (transitionItem.time > _totalDuration)
				{
					_totalDuration = transitionItem.time;
				}
				buffer.Seek(position, 2);
				DecodeValue(transitionItem, buffer, transitionItem.value);
			}
			buffer.position = position + num2;
		}
	}

	private void DecodeValue(TransitionItem item, ByteBuffer buffer, object value)
	{
		switch (item.type)
		{
		case TransitionActionType.XY:
		case TransitionActionType.Size:
		case TransitionActionType.Pivot:
		case TransitionActionType.Skew:
		{
			TValue tValue = (TValue)value;
			tValue.b1 = buffer.ReadBool();
			tValue.b2 = buffer.ReadBool();
			tValue.f1 = buffer.ReadFloat();
			tValue.f2 = buffer.ReadFloat();
			if (buffer.version >= 2 && item.type == TransitionActionType.XY)
			{
				tValue.b3 = buffer.ReadBool();
			}
			break;
		}
		case TransitionActionType.Alpha:
		case TransitionActionType.Rotation:
			((TValue)value).f1 = buffer.ReadFloat();
			break;
		case TransitionActionType.Scale:
			((TValue)value).f1 = buffer.ReadFloat();
			((TValue)value).f2 = buffer.ReadFloat();
			break;
		case TransitionActionType.Color:
			((TValue)value).color = buffer.ReadColor();
			break;
		case TransitionActionType.Animation:
			((TValue_Animation)value).playing = buffer.ReadBool();
			((TValue_Animation)value).frame = buffer.ReadInt();
			break;
		case TransitionActionType.Visible:
			((TValue_Visible)value).visible = buffer.ReadBool();
			break;
		case TransitionActionType.Sound:
		{
			string value2 = buffer.ReadS();
			if (!string.IsNullOrEmpty(value2))
			{
				((TValue_Sound)value).sound = Convert.ToInt32(value2);
			}
			((TValue_Sound)value).volume = buffer.ReadFloat();
			break;
		}
		case TransitionActionType.Transition:
			((TValue_Transition)value).transName = buffer.ReadS();
			((TValue_Transition)value).playTimes = buffer.ReadInt();
			((TValue_Transition)value).playCompleteDelegate = delegate
			{
				OnPlayTransCompleted(item);
			};
			break;
		case TransitionActionType.Shake:
			((TValue_Shake)value).amplitude = buffer.ReadFloat();
			((TValue_Shake)value).duration = buffer.ReadFloat();
			break;
		case TransitionActionType.ColorFilter:
		{
			TValue obj = (TValue)value;
			obj.f1 = buffer.ReadFloat();
			obj.f2 = buffer.ReadFloat();
			obj.f3 = buffer.ReadFloat();
			obj.f4 = buffer.ReadFloat();
			break;
		}
		case TransitionActionType.Text:
		case TransitionActionType.Icon:
			((TValue_Text)value).text = buffer.ReadS();
			break;
		}
	}
}
