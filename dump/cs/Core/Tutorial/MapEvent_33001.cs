using System.Collections.Generic;
using Core.Net;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Scripting;
using party.model;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class MapEvent_33001 : BaseMapEvent
{
	private readonly MapEventInfoConfigure _mapInfo;

	public MapEvent_33001()
	{
		if (!StaticConfigure.MapEvent.InfoDict.TryGetValue(33001, out _mapInfo))
		{
			Debug.LogError("无法通过ID:33001, 在MapEvent.InfoDict中获取正确的配置");
		}
	}

	public override async UniTask TryActiveEvent()
	{
		MapGimmickManager mapGimmickManager = SimpleSingletonProvider<LandManager>.inst.MapGimmickManager;
		if (!(mapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmick))
		{
			return;
		}
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo == null)
		{
			return;
		}
		int gameProgress = roomInfo.GameProgress;
		int safeByIndex = _mapInfo.Triggerparams.GetSafeByIndex(0);
		if (gameProgress != safeByIndex)
		{
			return;
		}
		await MonoSingletonProvider<NetManager>.inst.RPC.MapEventS2C.OnMapEventS2CServerCallBackAsync(new MapEventS2C
		{
			EventId = _mapInfo.Id
		}, 0, isDispatch: true);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowSystemInfo(140);
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < playerDatas.Count; i++)
		{
			if (playerDatas[i].player.Hero.HeroId == 1002)
			{
				num++;
			}
			if (playerDatas[i].player.Hero.HeroId == 1005)
			{
				num2++;
			}
		}
		Player monster = mapGimmick.TutorialSceneConfig.BuildMonsterServerPlayer1002(++num, 8, 14);
		Player monster1002_2 = mapGimmick.TutorialSceneConfig.BuildMonsterServerPlayer1002(num + 1, 14, 15);
		Player monster1005_1 = mapGimmick.TutorialSceneConfig.BuildMonsterServerPlayer1005(num2 + 1, 2, 1);
		await MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync(new MonsterRefreshS2C
		{
			Monster = monster
		}, 0, isDispatch: true);
		await MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync(new MonsterRefreshS2C
		{
			Monster = monster1002_2
		}, 0, isDispatch: true);
		await MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync(new MonsterRefreshS2C
		{
			Monster = monster1005_1
		}, 0, isDispatch: true);
	}
}
