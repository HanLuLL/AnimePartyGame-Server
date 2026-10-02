using System.Collections.Generic;
using Core.Net;
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
public class MapEvent_33002 : BaseMapEvent
{
	private readonly MapEventInfoConfigure _mapInfo;

	public MapEvent_33002()
	{
		if (!StaticConfigure.MapEvent.InfoDict.TryGetValue(33002, out _mapInfo))
		{
			Debug.LogError("无法通过ID:33002, 在MapEvent.InfoDict中获取正确的配置");
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
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		int num = 0;
		for (int i = 0; i < playerDatas.Count; i++)
		{
			if (playerDatas[i].player.Hero.HeroId == 1005)
			{
				num++;
			}
		}
		Player monster = mapGimmick.TutorialSceneConfig.BuildMonsterServerPlayer1005(num + 1, 16, 17);
		await MonoSingletonProvider<NetManager>.inst.RPC.MonsterRefreshS2C.OnMonsterRefreshS2CServerCallBackAsync(new MonsterRefreshS2C
		{
			Monster = monster
		}, 0, isDispatch: true);
	}
}
