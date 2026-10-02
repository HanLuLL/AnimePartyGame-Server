using Core.Scene;
using UnityEngine.Playables;

namespace Core.Unit;

public class DiceHUDBehaviour : PlayableBehaviour
{
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		BattleSceneController.inst.directorManager.attacker._UI.com_Attack.showPoint.selectedIndex = 0;
		BattleSceneController.inst.directorManager.victim._UI.com_Defend.showPoint.selectedIndex = 0;
		BattleSceneController.inst.directorManager.attacker._UI.com_Attack.showHp.selectedIndex = 1;
		BattleSceneController.inst.directorManager.victim._UI.com_Defend.showHp.selectedIndex = 1;
		base.OnBehaviourPlay(playable, info);
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		BattleSceneController.inst.directorManager.attacker._UI.com_Attack.showHp.selectedIndex = 0;
		BattleSceneController.inst.directorManager.victim._UI.com_Defend.showHp.selectedIndex = 0;
		base.OnBehaviourPause(playable, info);
	}
}
