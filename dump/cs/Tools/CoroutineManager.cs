using System;
using System.Collections;
using UnityEngine;

namespace Tools;

public class CoroutineManager : MonoSingletonProvider<CoroutineManager>
{
	public class CoroutineState : CustomYieldInstruction
	{
		private bool _running;

		private bool _paused;

		private bool _stopped;

		public readonly Signal<bool> stopSignal;

		private MonoBehaviour _target;

		private bool _isFollowGameObject;

		private FollowTargetMode _followTargetMode;

		private readonly IEnumerator _coroutine;

		public bool Running => _running;

		public bool Paused => _paused;

		public override bool keepWaiting => _running;

		public CoroutineState(IEnumerator c)
		{
			_coroutine = c;
			stopSignal = new Signal<bool>();
		}

		public void Start()
		{
			if (!_running)
			{
				_running = true;
				_paused = false;
				MonoSingletonProvider<CoroutineManager>.inst.StartCoroutine(CallWrapper());
			}
		}

		public void Pause()
		{
			if (_running && !_paused)
			{
				_paused = true;
			}
		}

		public override void Reset()
		{
			if (_running || _paused)
			{
				Stop();
			}
			_coroutine.Reset();
			base.Reset();
		}

		public void Unpause()
		{
			if (_running && _paused)
			{
				_paused = false;
			}
		}

		public void Stop()
		{
			_stopped = true;
			_running = false;
			_paused = false;
		}

		public CoroutineState DoFollow(MonoBehaviour target, FollowTargetMode followMode = FollowTargetMode.TargetDisactiveDoStop)
		{
			if (target == null)
			{
				return this;
			}
			_target = target;
			_isFollowGameObject = true;
			_followTargetMode = followMode;
			return this;
		}

		private IEnumerator CallWrapper()
		{
			yield return null;
			IEnumerator e = _coroutine;
			while (_running)
			{
				if (_isFollowGameObject)
				{
					if (!(_target != null))
					{
						Stop();
						yield return null;
						continue;
					}
					if ((!_target.enabled || !_target.gameObject.activeSelf) && _followTargetMode != FollowTargetMode.TargetDisactiveDoNothing)
					{
						if (_followTargetMode == FollowTargetMode.TargetDisactiveDoPause)
						{
							yield return null;
							continue;
						}
						if (_followTargetMode == FollowTargetMode.TargetDisactiveDoStop)
						{
							Stop();
						}
					}
				}
				if (_paused)
				{
					yield return null;
				}
				else if (e != null && e.MoveNext())
				{
					yield return e.Current;
				}
				else
				{
					_running = false;
				}
			}
			stopSignal.Dispatch(_stopped);
		}
	}

	public CoroutineState CreateCoroutine(IEnumerator coroutine)
	{
		return new CoroutineState(coroutine);
	}

	public CoroutineState CreateInvoke(float delayTime, Action action)
	{
		return CreateCoroutine(InvokeCoroutine(delayTime, action));
	}

	public CoroutineState CreateInvoke(int delayFrame, Action action)
	{
		return CreateCoroutine(InvokeCoroutine(delayFrame, action));
	}

	public CoroutineState CreateResetableInvoke(int delayFrame, Action action)
	{
		return CreateCoroutine(new DelayFrameCoroutine(delayFrame, action));
	}

	private IEnumerator InvokeCoroutine(float delayTime, Action action)
	{
		if (action != null)
		{
			if (delayTime > 0f)
			{
				yield return new WaitForSeconds(delayTime);
			}
			action();
		}
	}

	private IEnumerator InvokeCoroutine(int delayFrame, Action action)
	{
		if (action == null)
		{
			yield break;
		}
		if (delayFrame != 0)
		{
			int frameCount = delayFrame;
			while (frameCount > 0)
			{
				yield return delayFrame;
				int num = frameCount - 1;
				frameCount = num;
			}
		}
		action();
	}

	public virtual void DestroyCoroutineInst()
	{
		base.DestroyInst();
	}
}
