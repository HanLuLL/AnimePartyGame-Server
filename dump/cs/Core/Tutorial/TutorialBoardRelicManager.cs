using System.Collections.Generic;
using Core.Net;
using Core.Tutorial.Tools;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core.Tutorial;

public class TutorialBoardRelicManager
{
	private CtsInfo _selectRelicTsc;

	public void Initialize()
	{
	}

	public void Dispose()
	{
	}

	public List<int> GetLVRewardLv(int lv)
	{
		return lv switch
		{
			1 => new List<int> { 50007, 50018, 50002 }, 
			2 => new List<int> { 50040, 50022, 50008 }, 
			3 => new List<int> { 50010, 50020, 50006 }, 
			_ => null, 
		};
	}

	public List<int> GetFirstMissionReward()
	{
		return new List<int> { 50028, 50031, 50003 };
	}

	public List<int> GetSecondMissionReward()
	{
		return new List<int> { 50019, 50005, 50011 };
	}

	public async UniTask DealSelectRelic(long playerId, List<int> relicIds)
	{
		if (!(SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002) || relicIds == null || relicIds.Count == 0)
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			_selectRelicTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
			Action action = new Action
			{
				Sn = UIDGenerator.NextUID(),
				PlayerId = playerId
			};
			SimpleSingletonProvider<UIManager>.inst.relic.ShowRelicData(action, new SelectRelicC2S
			{
				Relics = { (IEnumerable<int>)relicIds }
			});
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId) && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round == 1)
			{
				await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowSystemInfo(130);
			}
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_selectRelicTsc);
		}
		else
		{
			int selectIndex = UnityEngine.Random.Range(0, relicIds.Count);
			await SelectRelic(playerId, relicIds, selectIndex);
		}
	}

	public async UniTask SelectRelic(long playerId, List<int> relicIds, int selectIndex)
	{
		int relicId = relicIds.GetSafeByIndex(selectIndex);
		await AddRelic(playerId, relicId);
		SelectRelicS2C model = new SelectRelicS2C
		{
			PlayerId = playerId,
			RelicId = relicId
		};
		await MonoSingletonProvider<NetManager>.inst.RPC.SelectRelicS2C.OnSelectRelicS2CServerCallBackAsync(model, 0, isDispatch: true);
		_selectRelicTsc?.Cancel();
	}

	public async UniTask AddRelic(long playerId, int relicId)
	{
		List<int> relicIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).GetRelicIds();
		RelicInfoConfigure value;
		if (relicIds != null && relicIds.Contains(relicId))
		{
			Debug.LogError($"当前玩家：{playerId} 已经有 筹码：{relicId}");
		}
		else if (StaticConfigure.Relic.InfoDict.TryGetValue(relicId, out value))
		{
			party.model.Buff buff = new party.model.Buff
			{
				UniqueId = UIDGenerator.NextUID(),
				BuffId = value.BuffId,
				Source = new buff_source
				{
					S = buff_source.Types.source.Relic,
					Id = relicId
				}
			};
			buff.InitBuffData(playerId);
			await TutorialGame.GetSystem<TutorialBoardManager>().buffManager.AddBuff(playerId, buff);
		}
	}
}
