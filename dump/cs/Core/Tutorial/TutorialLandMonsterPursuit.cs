using System.Collections.Generic;
using Core.Net;
using Core.Tutorial.Tools;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine.Scripting;
using party.model;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandMonsterPursuit : BaseTutorialLand
{
	private CtsInfo _cts;

	public override UniTask Pass(int landId, long playerId)
	{
		return UniTask.CompletedTask;
	}

	public override async UniTask Stay(int landId, long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			_cts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
			await SimpleSingletonProvider<GameLogicManager>.inst.land.DealMonsterPursuit(new Action
			{
				PlayerId = playerId,
				Sn = UIDGenerator.NextUID()
			});
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_cts);
		}
	}

	public async UniTask RequestMonsterPursuitC2S(long playerId, long monsterId)
	{
		MonsterPursuitS2C model;
		if (monsterId != 0L)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(monsterId);
			UnitLand standLand = playerDataById.CharacterInst.standLand;
			int fromLandId = playerDataById.CharacterInst.fromLandId;
			List<int> adjacencyLandIds = standLand.AdjacencyLandIds;
			adjacencyLandIds.Remove(fromLandId);
			model = new MonsterPursuitS2C
			{
				PlayerId = playerId,
				Exit = false,
				NodeId = standLand.Id,
				BackId = fromLandId,
				FrontIds = { (IEnumerable<int>)adjacencyLandIds }
			};
		}
		else
		{
			model = new MonsterPursuitS2C
			{
				PlayerId = playerId,
				Exit = true
			};
		}
		await MonoSingletonProvider<NetManager>.inst.RPC.MonsterPursuitS2C.OnMonsterPursuitS2CServerCallBackAsync(model, 0, isDispatch: true);
		if (monsterId != 0L)
		{
			await TutorialGame.GetSystem<TutorialBoardManager>().gameManager.StartPK(playerId, monsterId);
		}
		_cts?.Cancel();
	}
}
