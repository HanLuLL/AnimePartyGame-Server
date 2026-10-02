using System;
using Core.Scene;
using Cysharp.Threading.Tasks;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class DodgeBehaviour : PlayableBehaviour
{
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		PlayVictimAction();
	}

	private void PlayVictimAction()
	{
		if ((object)BattleSceneController.inst != null && !BattleSceneController.inst.directorManager.IsHit())
		{
			BattleSceneController.inst.directorManager.victim.PlayDodge().Forget();
		}
	}
}
