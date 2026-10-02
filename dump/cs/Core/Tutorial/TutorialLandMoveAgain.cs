using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine.Scripting;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandMoveAgain : BaseTutorialLand
{
	public override UniTask Pass(int landId, long playerId)
	{
		return UniTask.CompletedTask;
	}

	public override async UniTask Stay(int landId, long playerId)
	{
		if (TutorialGame.GetSystem<GamePlayManager>().GameLoop is DefaultGameLoop defaultGameLoop)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			await defaultGameLoop.MoveAgain(playerDataById);
		}
	}
}
