using System.Collections.Generic;
using Core.Net;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using party.protocol;

namespace Core.Tutorial;

public class TutorialMovingState : TutorialPlayerActionState
{
	public TutorialMovingState(PlayerActionType type, TutorialPlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		TutorialBoardGameManager gameManager = TutorialGame.GetSystem<TutorialBoardManager>().gameManager;
		if (gameManager.CanMove(_fsm.PlayerId))
		{
			BattlePlayerData actionPlayer = _fsm.GetActionPlayerData();
			if (actionPlayer.characterType == CharacterType.Hero && actionPlayer.CharacterInst.canStep > 0)
			{
				if (gameManager.Round <= 2 && SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmickManager_Tutorial)
				{
					mapGimmickManager_Tutorial.DealGuideRoad(_fsm.PlayerId, disable: true);
				}
				if (!(SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001) || gameManager.Round != 4 || !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_fsm.PlayerId))
				{
					await SimpleSingletonProvider<RoadLineManager>.inst.GeneratePath(actionPlayer.CharacterInst, actionPlayer.CharacterInst.canStep);
				}
				else
				{
					await SimpleSingletonProvider<RoadLineManager>.inst.GeneratePath(actionPlayer.CharacterInst, 5);
				}
				if (gameManager.Round <= 2 && SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmickManager_Tutorial2)
				{
					mapGimmickManager_Tutorial2.DealGuideRoad(_fsm.PlayerId, disable: false);
				}
			}
			if (gameManager.IsChooseDir(actionPlayer.CharacterInst.standLand, actionPlayer.CharacterInst.fromLandId))
			{
				await _fsm.SwitchState(PlayerActionType.ChooseDir);
				return;
			}
			List<int> list = gameManager.GenerateMovePath(_fsm.PlayerId);
			if (list == null || list.Count == 0)
			{
				Debug.LogError("当前节点显示可以移动，但是发生意外，导致无法获取正确的路线");
				return;
			}
			await MonoSingletonProvider<NetManager>.inst.RPC.MoveS2C.OnMoveS2CServerCallBackAsync(new MoveS2C
			{
				End = (gameManager.MovePoint <= 0),
				PlayerId = _fsm.PlayerId,
				NodeIds = { (IEnumerable<int>)list }
			}, 0, isDispatch: true);
			await _fsm.SwitchState(PlayerActionType.MoveStop);
		}
		else
		{
			await _fsm.SwitchState(PlayerActionType.MoveStop);
		}
	}
}
