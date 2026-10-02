using System.Collections.Generic;
using System.Linq;
using Core.Net;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core.Tutorial;

public class TutorialThrowDiceState : TutorialPlayerActionState
{
	public TutorialThrowDiceState(PlayerActionType type, TutorialPlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		_fsm.TryClosePlayerHandle();
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_fsm.PlayerId))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.handCard?.ChangeSuggestMove(suggest: false);
		}
		TutorialBoardGameManager gameManager = TutorialGame.GetSystem<TutorialBoardManager>().gameManager;
		int dicePoint = gameManager.GenerateDicePoint(_fsm.PlayerId);
		await TutorialGame.GetSystem<TutorialBoardManager>().buffManager.OnThrowDice(_fsm.PlayerId);
		int num = dicePoint;
		MovePointBuffS2C movePointBuffS2C = null;
		if (gameManager.moveAdditionAttribute.Count > 0)
		{
			int num2 = gameManager.moveAdditionAttribute.Select((AdditionAttribute x) => x.MovePoint).Sum();
			num += num2;
			movePointBuffS2C = new MovePointBuffS2C
			{
				PlayerId = _fsm.PlayerId,
				Attrs = { (IEnumerable<AdditionAttribute>)gameManager.moveAdditionAttribute }
			};
			gameManager.moveAdditionAttribute.Clear();
		}
		gameManager.UpdateMovePoint(num);
		ThrowDiceS2C model = new ThrowDiceS2C
		{
			PlayerId = _fsm.PlayerId,
			MovePoint = num,
			Vals = { Mathf.Min(10, dicePoint) }
		};
		await MonoSingletonProvider<NetManager>.inst.RPC.ThrowDiceS2C.OnThrowDiceS2CServerCallBackAsync(model, 0, isDispatch: true);
		if (movePointBuffS2C != null)
		{
			await MonoSingletonProvider<NetManager>.inst.RPC.MovePointBuffS2C.OnMovePointBuffS2CServerCallBackAsync(movePointBuffS2C, 0, isDispatch: true);
		}
		_fsm.OnActionFinished();
	}
}
