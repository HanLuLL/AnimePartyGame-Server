using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;

namespace Core.Tutorial;

public class TutorialMoveStopState : TutorialPlayerActionState
{
	public TutorialMoveStopState(PlayerActionType type, TutorialPlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		BattlePlayerData actionPlayerData = _fsm.GetActionPlayerData();
		TutorialBoardGameManager gameManager = TutorialGame.GetSystem<TutorialBoardManager>().gameManager;
		if (actionPlayerData.player.Property.HP.Value == 0)
		{
			_fsm.OnActionFinished();
			return;
		}
		List<long> pKTargetIds = gameManager.PKTargetIds;
		if (pKTargetIds != null && pKTargetIds.Count > 0)
		{
			await _fsm.SwitchState(PlayerActionType.Encounter);
		}
		else if (gameManager.CanMove(actionPlayerData.player.Id))
		{
			int beBornNodeId = actionPlayerData.player.serverPlayer.Hero.BeBornNodeId;
			UnitLand standLand = actionPlayerData.CharacterInst.standLand;
			if (gameManager.SuggestStop(beBornNodeId, standLand, gameManager.MovePoint))
			{
				await TutorialGame.GetSystem<TutorialBoardManager>().landManager.GetLandByType(standLand.LandType).Pass(standLand.Id, actionPlayerData.player.Id);
			}
			await _fsm.SwitchState(PlayerActionType.Moving);
		}
		else
		{
			_fsm.OnActionFinished();
		}
	}
}
