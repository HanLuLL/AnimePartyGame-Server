using System;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class KillVfxBehaviour : PlayableBehaviour
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
				PlayKillVfx();
			}
		}
	}

	private void PlayKillVfx()
	{
		BattleShowDirector directorManager = BattleSceneController.inst.directorManager;
		if ((object)directorManager != null)
		{
			FashionEffectConfigure killVfxConfig = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.OverKillResultConfig(directorManager.attackerId);
			directorManager.BattleEffect.TryShowKillVfx(killVfxConfig, _director).Forget();
		}
	}
}
