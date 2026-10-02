using System;
using Core.Camera;
using Core.Scene;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class KillShakeBehaviour : PlayableBehaviour
{
	private PlayableDirector _director;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if ((object)BattleSceneController.inst != null && BattleSceneController.inst.directorManager.IsHit())
		{
			if ((UnityEngine.Object)(object)_director == null)
			{
				_director = playable.GetCustomComponent<PlayableDirector>();
			}
			if (BattleSceneController.inst.directorManager.VictimDead())
			{
				PlayKillShake();
			}
		}
	}

	private async void PlayKillShake()
	{
		long attackerId = BattleSceneController.inst.directorManager.attackerId;
		FashionEffectConfigure fashionEffectConfigure = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.OverKillResultConfig(attackerId);
		CinemachineImpulseAsset cinemachineImpulseAsset = await SimpleSingletonProvider<InternalAssetManager>.inst.GetImpulseAsset(fashionEffectConfigure.KillCameraShake);
		if ((object)cinemachineImpulseAsset != null)
		{
			BattleSceneController.inst.directorManager.GenerateImpulse(cinemachineImpulseAsset);
		}
	}
}
