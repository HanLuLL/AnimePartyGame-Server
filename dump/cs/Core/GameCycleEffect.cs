using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityTimer;

namespace Core;

[Serializable]
public class GameCycleEffect : Effect
{
	[Space(8f)]
	[Header("周期特效")]
	[SerializeField]
	[Tooltip("if StartDuration = 0, BeginEffect dont play")]
	public float StartDuration;

	[SerializeField]
	public bool KeppStartEffect;

	public Transform StartEffect;

	public Transform LoopEffect;

	[SerializeField]
	[Tooltip("0-始终循环")]
	public float LoopDuration;

	[SerializeField]
	[Tooltip("if EndDuration = 0, EndEffect dont play")]
	public float EndDuration;

	public Transform EndEffect;

	[SerializeField]
	public float audioDelayTime;

	[SerializeField]
	public int StartAudioEventID;

	private Timer _timer;

	public override void Play(string _effectPoolKey, Action action)
	{
		Timer timer = _timer;
		if (timer != null)
		{
			timer.Cancel();
		}
		effectPoolKey = _effectPoolKey;
		onCompletePlay = action;
		if (StartEffect != null)
		{
			StartEffect.gameObject.SetActive(value: false);
		}
		if (LoopEffect != null)
		{
			LoopEffect.gameObject.SetActive(value: false);
		}
		if (EndEffect != null)
		{
			EndEffect.gameObject.SetActive(value: false);
		}
		if (StartDuration != 0f && StartEffect != null)
		{
			StartEffect.gameObject.SetActive(value: true);
			PlayAllParticles(StartEffect);
			_timer = TimerExtensions.AttachTimer(base.gameObject, 0f, StartDuration / base.effectSpeed, (Action)PlayLoopEffect, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, false, (Action)null, -1f, false);
		}
		else
		{
			PlayLoopEffect();
		}
		PlayEffectSound();
	}

	protected async void PlayEffectSound()
	{
		bool flag = await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(audioDelayTime));
		if (StartAudioEventID != 0 && !flag)
		{
			if (GetComponents<Component>().Any((Component x) => x?.GetType().Name == "AkGameObj"))
			{
				SimpleSingletonProvider<AudioManager>.inst.SendEvent(StartAudioEventID, base.gameObject);
			}
			else
			{
				Stage.inst.PlayOneShotSound(StartAudioEventID);
			}
		}
	}

	private void PlayLoopEffect()
	{
		Timer timer = _timer;
		if (timer != null)
		{
			timer.Cancel();
		}
		if (LoopEffect != null)
		{
			LoopEffect.gameObject.SetActive(value: true);
			PlayAllParticles(LoopEffect);
			if (LoopDuration != 0f)
			{
				_timer = TimerExtensions.AttachTimer(base.gameObject, 0f, LoopDuration / base.effectSpeed, (Action)PlayEndEffect, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, false, (Action)null, -1f, false);
			}
		}
		if (!KeppStartEffect && StartEffect != null)
		{
			StartEffect.gameObject.SetActive(value: false);
		}
	}

	private void PlayEndEffect()
	{
		Timer timer = _timer;
		if (timer != null)
		{
			timer.Cancel();
		}
		if (EndDuration != 0f && EndEffect != null)
		{
			EndEffect.gameObject.SetActive(value: true);
			PlayAllParticles(EndEffect);
			if (LoopEffect != null)
			{
				LoopEffect.gameObject.SetActive(value: false);
			}
			if (StartEffect != null)
			{
				StartEffect.gameObject.SetActive(value: false);
			}
			_timer = TimerExtensions.AttachTimer(base.gameObject, 0f, EndDuration / base.effectSpeed, (Action)ReleaseCycleEffect, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, false, (Action)null, -1f, false);
		}
		else
		{
			ReleaseCycleEffect();
		}
	}

	private void ReleaseCycleEffect()
	{
		if (base.gameObject != null)
		{
			StopAllParticles(StartEffect);
			StopAllParticles(LoopEffect);
			StopAllParticles(EndEffect);
			onCompletePlay?.Invoke();
			Dispose();
		}
	}

	public override void ReleaseEffect()
	{
		PlayEndEffect();
	}

	public override async UniTask WaitFinishPlay()
	{
		await UniTask.CompletedTask;
	}

	protected override void Dispose()
	{
		Timer timer = _timer;
		if (timer != null)
		{
			timer.Cancel();
		}
		_timer = null;
		base.Dispose();
	}

	private void OnDestroy()
	{
		Timer timer = _timer;
		if (timer != null)
		{
			timer.Cancel();
		}
		_timer = null;
	}

	public override bool ResetEffectTimer()
	{
		if (!base.IsActive)
		{
			return false;
		}
		if (_timer == null)
		{
			return false;
		}
		PlayLoopEffect();
		return true;
	}
}
