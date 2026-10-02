using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine.Scripting;

namespace Core.Tutorial;

[Preserve]
public class TutorialMonster_1004 : TutorialBaseMonster
{
	public override bool ActionStatus()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round != 1;
	}

	public override async UniTask StartEncounterEvent(long playerId, long target)
	{
		await TutorialGame.GetSystem<TutorialBoardManager>().gameManager.StartPK(playerId, target);
	}
}
