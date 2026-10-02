using System.Collections.Generic;
using Core.Net;
using Core.Tutorial.Tools;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Scripting;
using party.model;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class TutorialLandEvent : BaseTutorialLand
{
	public int SpecifyEventId;

	private readonly int[] _eventIds = new int[4] { 30019, 30008, 30014, 30202 };

	public override UniTask Pass(int landId, long playerId)
	{
		return UniTask.CompletedTask;
	}

	public override async UniTask Stay(int landId, long playerId)
	{
		int eventId = SpecifyEventId;
		if (SpecifyEventId == 0)
		{
			int num = UnityEngine.Random.Range(0, _eventIds.Length);
			eventId = _eventIds[num];
		}
		SpecifyEventId = 0;
		await MonoSingletonProvider<NetManager>.inst.RPC.TriggerEventS2C.OnTriggerEventS2CServerCallBackAsync(new TriggerEventS2C
		{
			PlayerId = playerId,
			EventId = eventId
		}, 0, isDispatch: true);
		if (!StaticConfigure.Event.InfoDict.TryGetValue(eventId, out var info))
		{
			Debug.LogError($"无法通过配置Id:{eventId} 在Event.InfoDict中获取配置");
			return;
		}
		switch (eventId)
		{
		case 30203:
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001 mapGimmickManager_Tutorial)
			{
				await mapGimmickManager_Tutorial.CreateLittleRaccoon();
			}
			break;
		case 30019:
		{
			List<BattlePlayerData> players = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i].characterType == CharacterType.Hero && !(players[i].CharacterInst == null))
				{
					long id3 = players[i].player.Id;
					int safeByIndex3 = info.Params.GetSafeByIndex(0);
					players[i].cardContainer._HandCards.Add(HandCardData.GetTutorialHandCardData(safeByIndex3));
					HeroAttrEffect cardUpdate2 = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCardUpdate(id3, players[i].cardContainer._CardInfos);
					await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
					{
						Cause = new CauseOrigin
						{
							S = CauseOrigin.Types.source.Event,
							Id = eventId
						},
						PlayerId = id3,
						EffectDatas = { cardUpdate2 }
					});
				}
			}
			break;
		}
		case 30008:
		{
			List<BattlePlayerData> players = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i].characterType == CharacterType.Hero && !(players[i].CharacterInst == null))
				{
					long id4 = players[i].player.Id;
					int safeByIndex4 = info.Params.GetSafeByIndex(0);
					HeroAttrEffect hpUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetHpUpdate(id4, safeByIndex4);
					await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
					{
						Cause = new CauseOrigin
						{
							S = CauseOrigin.Types.source.Event,
							Id = eventId
						},
						PlayerId = id4,
						EffectDatas = { hpUpdate }
					});
				}
			}
			break;
		}
		case 30014:
		{
			List<BattlePlayerData> players = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i].characterType != CharacterType.Hero || players[i].CharacterInst == null)
				{
					continue;
				}
				long id2 = players[i].player.Id;
				int safeByIndex = info.Params.GetSafeByIndex(0);
				int safeByIndex2 = info.Params.GetSafeByIndex(1);
				HeroAttrEffect goldUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetGoldUpdate(id2, safeByIndex);
				List<HandCardData> list = TutorialGame.GetSystem<TutorialBoardManager>().cardManager.TryGetCards(safeByIndex2);
				if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1001)
				{
					for (int k = 0; k < list.Count; k++)
					{
						if (list[k].CardId == 21006)
						{
							list[k].CardId = 10003;
						}
					}
				}
				players[i].cardContainer._HandCards.AddRange(list);
				HeroAttrEffect cardUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCardUpdate(id2, players[i].cardContainer._CardInfos);
				await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
				{
					Cause = new CauseOrigin
					{
						S = CauseOrigin.Types.source.Event,
						Id = eventId
					},
					PlayerId = id2,
					EffectDatas = { goldUpdate, cardUpdate }
				});
			}
			break;
		}
		case 30202:
		{
			int i = info.BuffIds.GetSafeByIndex(0);
			List<BattlePlayerData> players = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
			for (int j = 0; j < players.Count; j++)
			{
				if (players[j].characterType == CharacterType.Hero && !(players[j].CharacterInst == null))
				{
					long id = players[j].player.Id;
					party.model.Buff buff = new party.model.Buff
					{
						UniqueId = UIDGenerator.NextUID(),
						BuffId = i,
						Source = new buff_source
						{
							S = buff_source.Types.source.Card,
							Id = eventId
						}
					};
					buff.InitBuffData(id);
					await TutorialGame.GetSystem<TutorialBoardManager>().buffManager.AddBuff(id, buff);
				}
			}
			break;
		}
		}
	}
}
