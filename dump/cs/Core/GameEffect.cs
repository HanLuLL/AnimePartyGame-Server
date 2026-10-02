using System;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityTimer;

namespace Core;

public class GameEffect : Effect
{
	[SerializeField]
	public bool isLoop;

	[SerializeField]
	public float duration;

	[SerializeField]
	public float audioDelayTime;

	[SerializeField]
	public int audioEventID;

	[SerializeField]
	protected PlayableDirector _director;

	private Timer _effectTimer;

	public override void Play(string _effectPoolKey, Action action)
	{
		effectPoolKey = _effectPoolKey;
		onCompletePlay = action;
		if ((UnityEngine.Object)(object)_director != null)
		{
			PlayTimeline();
		}
		else
		{
			PlayAllParticles(base.transform);
			if (!isLoop && !string.IsNullOrEmpty(effectPoolKey))
			{
				_effectTimer = TimerExtensions.AttachTimer(base.gameObject, 0f, duration, (Action)ReleaseEffect, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, false, (Action)null, -1f, false);
			}
		}
		PlayEffectSound();
	}

	private void PlayTimeline()
	{
		_director.Play();
		if (_director.playableGraph.IsValid())
		{
			Playable rootPlayable = _director.playableGraph.GetRootPlayable(0);
			if (rootPlayable.IsValid())
			{
				rootPlayable.SetSpeed(base.effectSpeed);
			}
		}
		if (!isLoop)
		{
			_effectTimer = TimerExtensions.AttachTimer(base.gameObject, 0f, (float)_director.duration, (Action)OnStopped, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, false, (Action)null, -1f, false);
		}
	}

	protected async void PlayEffectSound()
	{
		bool flag = await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(audioDelayTime));
		if (audioEventID != 0 && !flag)
		{
			Stage.inst.PlayOneShotSound(audioEventID);
		}
	}

	public override async UniTask WaitFinishPlay()
	{
		if (!isLoop)
		{
			if ((UnityEngine.Object)(object)_director != null)
			{
				duration = (float)_director.duration;
			}
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(duration));
		}
	}

	private void OnStopped()
	{
		onCompletePlay?.Invoke();
		Dispose();
	}

	public override bool ResetEffectTimer()
	{
		if (!base.IsActive)
		{
			return false;
		}
		if (_effectTimer == null)
		{
			return false;
		}
		if (!_effectTimer.isDone)
		{
			_effectTimer.ReStart(true);
			PlayShowEffect();
			return true;
		}
		return false;
	}

	public override void ReleaseEffect()
	{
		if ((UnityEngine.Object)(object)_director != null)
		{
			OnStopped();
		}
		else if (base.gameObject != null)
		{
			StopAllParticles(base.transform);
			onCompletePlay?.Invoke();
			Dispose();
		}
	}

	public void PlayShowEffect()
	{
		if ((UnityEngine.Object)(object)_director != null)
		{
			_director.Play();
		}
		else
		{
			PlayAllParticles(base.transform);
		}
	}
}
