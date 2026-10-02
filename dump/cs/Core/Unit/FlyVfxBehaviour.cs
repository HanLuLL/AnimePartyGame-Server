using System;
using Core.Scene;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class FlyVfxBehaviour : PlayableBehaviour
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
				PlayFlyVfx();
			}
		}
	}

	private async void PlayFlyVfx()
	{
		long attackerId = BattleSceneController.inst.directorManager.attackerId;
		FashionEffectConfigure fashionEffectConfigure = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.OverKillResultConfig(attackerId);
		BattleActor victim = BattleSceneController.inst.directorManager.victim;
		(await SimpleSingletonProvider<EffectManager>.inst.PlayById(fashionEffectConfigure.FlyVfx, Vector3.zero, Quaternion.identity, victim.transform)).transform.localPosition = new Vector3(0f, 0f, 0f);
	}
}
