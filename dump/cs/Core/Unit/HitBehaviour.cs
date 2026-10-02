using System;
using Core.Camera;
using Core.Scene;
using Core.Sprite;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class HitBehaviour : PlayableBehaviour
{
	[SerializeField]
	private int hitEffectId;

	[SerializeField]
	private Vector3 effectScale = Vector3.one;

	[SerializeField]
	private Vector3 effectOffset;

	[SerializeField]
	private Vector3 effectRotation;

	[SerializeField]
	private Vector3 effectRandomRotation;

	[SerializeField]
	private int AudioId;

	[SerializeField]
	private CinemachineImpulseAsset impulseAsset;

	[SerializeField]
	private SpriteShakeAsset shakeAsset;

	[SerializeField]
	private float shakeDuration;

	[SerializeField]
	private OffsetCurveAsset _asset;

	[SerializeField]
	private float attackFrameDuration;

	[SerializeField]
	private float attackFrameMultiplier;

	[SerializeField]
	private float victimDuration;

	[SerializeField]
	private float victimFrameMultiplier;

	[SerializeField]
	private int hitEffectId_Heavy;

	[SerializeField]
	private Vector3 effectScale_Heavy = Vector3.one;

	[SerializeField]
	private Vector3 effectOffset_Heavy;

	[SerializeField]
	private Vector3 effectRotation_Heavy;

	[SerializeField]
	private Vector3 effectRandomRotation_Heavy;

	[SerializeField]
	private int AudioId_Heavy;

	[SerializeField]
	private CinemachineImpulseAsset impulseAsset_Heavy;

	[SerializeField]
	private SpriteShakeAsset shakeAsset_Heavy;

	[SerializeField]
	private float shakeDuration_Heavy;

	[SerializeField]
	private OffsetCurveAsset _asset_Heavy;

	[SerializeField]
	private float attackFrameDuration_Heavy;

	[SerializeField]
	private float attackFrameMultiplier_Heavy;

	[SerializeField]
	private float victimDuration_Heavy;

	[SerializeField]
	private float victimFrameMultiplier_Heavy;

	[SerializeField]
	private bool EnableShowHit = true;

	private PlayableDirector _director;

	[NonSerialized]
	public double StartTime;

	private bool _alreadyPlayed;

	private double _triggerTime;

	private double _lastDirectorTime;

	private BattleShowDirector _battleShowDirector => BattleSceneController.inst.directorManager;

	public override void OnPlayableCreate(Playable playable)
	{
		base.OnPlayableCreate(playable);
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetCustomComponent<PlayableDirector>();
		}
		_triggerTime = StartTime;
		_alreadyPlayed = false;
		_lastDirectorTime = _director.time;
	}

	public override void OnGraphStop(Playable playable)
	{
		if (!_alreadyPlayed && EnableShowHit)
		{
			Play(playable);
		}
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		Play(playable);
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetCustomComponent<PlayableDirector>();
		}
		double time = _director.time;
		double lastDirectorTime = _lastDirectorTime;
		if (!_alreadyPlayed && lastDirectorTime < _triggerTime && time >= _triggerTime)
		{
			Play(playable);
		}
		_lastDirectorTime = time;
	}

	private void ActiveImpulse(CinemachineImpulseAsset asset)
	{
		if ((object)asset != null)
		{
			_battleShowDirector.GenerateImpulse(asset);
		}
	}

	private async UniTask PlayHitEffect(int _effectID, Vector3 _scale, Vector3 _offset, Vector3 _rotation, Vector3 _randomRotation)
	{
		if (_effectID != 0 && !((UnityEngine.Object)(object)_director == null))
		{
			Vector3 position = ((Component)(object)_director).transform.position + _offset;
			Vector3 euler = _rotation + RandomRange(_randomRotation);
			Effect effect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(_effectID, position, Quaternion.Euler(euler));
			if (!(effect == null))
			{
				effect.gameObject.transform.localScale = _scale;
			}
		}
	}

	private Vector3 RandomRange(Vector3 range)
	{
		return new Vector3(UnityEngine.Random.Range(0f - Mathf.Abs(range.x), Mathf.Abs(range.x)), UnityEngine.Random.Range(0f - Mathf.Abs(range.y), Mathf.Abs(range.y)), UnityEngine.Random.Range(0f - Mathf.Abs(range.z), Mathf.Abs(range.z)));
	}

	private void PlayVictimAction()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.fight != null)
		{
			_battleShowDirector.victim.PlayHit().Forget();
		}
	}

	private void Play(Playable playable)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.fight == null)
		{
			return;
		}
		_alreadyPlayed = true;
		if (!_battleShowDirector.IsHit())
		{
			return;
		}
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetCustomComponent<PlayableDirector>();
		}
		BattleActor battleActor = ((Component)(object)_director).GetComponent<BattleActor>();
		if ((object)battleActor == null)
		{
			battleActor = _battleShowDirector.attacker;
		}
		if (Mathf.Abs(_battleShowDirector.VictimIncHp()) < StaticGlobalData.GAME_FIGHT_DAMAGE_THRESHOLD)
		{
			battleActor.PlayStopped(attackFrameDuration, attackFrameMultiplier);
			ActiveImpulse(impulseAsset);
			PlayHitEffect(hitEffectId, effectScale, effectOffset, effectRotation, effectRandomRotation).Forget();
			PlayVictimAction();
			if (EnableShowHit)
			{
				_battleShowDirector.victim._UI.ShowHitValue(_battleShowDirector.VictimIncHp());
			}
			_battleShowDirector.victim.PlayStopped(victimDuration, victimFrameMultiplier);
			Stage.inst.PlayOneShotSound(AudioId);
			_battleShowDirector.victim.SetBattleShowAsVictim(_asset, shakeAsset, shakeDuration, victimDuration, victimFrameMultiplier, _battleShowDirector.VictimDead());
			if (EnableShowHit)
			{
				_battleShowDirector.PlayExHit(victimDuration).Forget();
			}
		}
		else
		{
			battleActor.PlayStopped(attackFrameDuration_Heavy, attackFrameMultiplier_Heavy);
			ActiveImpulse(impulseAsset_Heavy);
			PlayHitEffect(hitEffectId_Heavy, effectScale_Heavy, effectOffset_Heavy, effectRotation_Heavy, effectRandomRotation_Heavy).Forget();
			PlayVictimAction();
			_battleShowDirector.victim.PlayStopped(victimDuration_Heavy, victimFrameMultiplier_Heavy);
			if (EnableShowHit)
			{
				_battleShowDirector.victim._UI.ShowHitValue(_battleShowDirector.VictimIncHp());
			}
			Stage.inst.PlayOneShotSound(AudioId_Heavy);
			_battleShowDirector.victim.SetBattleShowAsVictim(_asset_Heavy, shakeAsset_Heavy, shakeDuration_Heavy, victimDuration_Heavy, victimFrameMultiplier_Heavy, _battleShowDirector.VictimDead());
			if (EnableShowHit)
			{
				_battleShowDirector.PlayExHit(victimDuration_Heavy).Forget();
			}
		}
		if (EnableShowHit)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.performTriggerLogic.signal.characterFightHitSignal.Dispatch(_battleShowDirector.defendeId, _battleShowDirector.attackerId, _battleShowDirector.VictimIncHp(), !_battleShowDirector.VictimDead());
		}
	}
}
