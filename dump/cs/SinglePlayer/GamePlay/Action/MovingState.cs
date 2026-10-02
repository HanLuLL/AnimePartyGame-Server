using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Map;
using UnityEngine;

namespace SinglePlayer.GamePlay.Action;

public class MovingState : PlayerActionState
{
	public MovingState(PlayerActionType type, PlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		BoardCharacterManager characterManager = Game.GetSystem<BoardManager>().characterManager;
		if (characterManager.CanMove())
		{
			Queue<Land> queue = characterManager.GenerateMovePath();
			if (queue == null || queue.Count == 0)
			{
				Debug.Log("当前节点显示可以移动，但是发生意外，导致无法获取正确的路线");
				return;
			}
			await characterManager.Hero.DoMove(queue);
			await _fsm.SwitchState(PlayerActionType.MoveStop);
		}
		else
		{
			await _fsm.SwitchState(PlayerActionType.MoveStop);
		}
	}
}
