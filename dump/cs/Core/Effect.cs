using System;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core;

public abstract class Effect : MonoBehaviour
{
	protected Action onCompletePlay;

	[HideInInspector]
	public string effectPoolKey;

	protected float effectSpeed => BattleConfig.EffectSpeed;

	public bool IsActive => effectPoolKey != null;

	public abstract void Play(string _effectPoolKey, Action action);

	public abstract void ReleaseEffect();

	public abstract UniTask WaitFinishPlay();

	protected void PlayAllParticles(Transform parentTransform)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (!(parentTransform == null))
		{
			ParticleSystem component = parentTransform.GetComponent<ParticleSystem>();
			if ((UnityEngine.Object)(object)component != null)
			{
				MainModule main = component.main;
				((MainModule)(ref main)).simulationSpeed = effectSpeed;
				component.Stop(true, (ParticleSystemStopBehavior)0);
				component.Play(true);
			}
			for (int i = 0; i < parentTransform.childCount; i++)
			{
				Transform child = parentTransform.GetChild(i);
				PlayAllParticles(child);
			}
		}
	}

	protected void StopAllParticles(Transform parentTransform)
	{
		if (!(parentTransform == null))
		{
			ParticleSystem component = parentTransform.GetComponent<ParticleSystem>();
			if ((UnityEngine.Object)(object)component != null)
			{
				component.Stop();
			}
			for (int i = 0; i < parentTransform.childCount; i++)
			{
				Transform child = parentTransform.GetChild(i);
				StopAllParticles(child);
			}
		}
	}

	protected virtual void Dispose()
	{
		if (!string.IsNullOrEmpty(effectPoolKey))
		{
			SimpleSingletonProvider<EffectManager>.inst.Stop(effectPoolKey, base.gameObject);
			effectPoolKey = null;
		}
	}

	public virtual void RefreshSpeed()
	{
		RefreshParticleSpeeds(base.transform);
	}

	private void RefreshParticleSpeeds(Transform parentTransform)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (!(parentTransform == null))
		{
			ParticleSystem component = parentTransform.GetComponent<ParticleSystem>();
			if ((UnityEngine.Object)(object)component != null)
			{
				MainModule main = component.main;
				((MainModule)(ref main)).simulationSpeed = effectSpeed;
			}
			for (int i = 0; i < parentTransform.childCount; i++)
			{
				RefreshParticleSpeeds(parentTransform.GetChild(i));
			}
		}
	}

	protected virtual void OnEnable()
	{
		(SimpleSingletonProvider<GameLogicManager>.inst?.replay?.Session?.Signal.speedChanged)?.AddListener(RefreshSpeed);
	}

	protected virtual void OnDisable()
	{
		(SimpleSingletonProvider<GameLogicManager>.inst?.replay?.Session?.Signal.speedChanged)?.RemoveListener(RefreshSpeed);
	}

	public virtual void SetColor(Color color)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			MainModule main = componentsInChildren[i].main;
			((MainModule)(ref main)).startColor = MinMaxGradient.op_Implicit(color);
		}
	}

	public virtual bool ResetEffectTimer()
	{
		if (!IsActive)
		{
			return false;
		}
		return true;
	}
}
