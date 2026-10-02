using System;
using System.Collections.Generic;
using Core.Camera;
using Core.MapEvents;
using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace Core;

public class GameModePlayLogic : IRPCSync
{
	public readonly GameModePlaySignal signal = new GameModePlaySignal();

	public List<MapEventInfoConfigure> GetRoomMapEvents(int mapId, int mapDifficultyId, int difficulty)
	{
		if (StaticConfigure.Map.InfoDict.TryGetValue(mapId, out var value))
		{
			List<MapEventInfoConfigure> result = new List<MapEventInfoConfigure>();
			if (value.DifficultyIds.Count == 0)
			{
				result = value.MapEventInfoConfigures;
			}
			else
			{
				RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = mapDifficultyId.GetMapGameDifficultyItems();
				for (int i = 0; i < mapGameDifficultyItems.Count; i++)
				{
					if (mapGameDifficultyItems[i].Index == difficulty)
					{
						result = mapGameDifficultyItems[i].MapEventInfoConfigures;
					}
				}
			}
			return result;
		}
		return null;
	}

	public void TriggerMonsterShow()
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (!room.IsInRoom)
		{
			return;
		}
		int mapId = room.curRoomInfo.MapId;
		int mapDifficultyId = room.curRoomInfo.MapDifficultyId;
		int difficulty = room.curRoomInfo.Difficulty;
		List<MapEventInfoConfigure> roomMapEvents = GetRoomMapEvents(mapId, mapDifficultyId, difficulty);
		if (roomMapEvents == null || roomMapEvents.Count <= 0)
		{
			return;
		}
		foreach (MapEventInfoConfigure item in roomMapEvents)
		{
			Type type = Type.GetType($"Core.MapEvent_{item.Id}");
			if (type != null)
			{
				(Activator.CreateInstance(type) as MapEvent)?.MonsterShow();
			}
		}
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.MapEventS2C.OnMapEventS2CServerCallBackAsync = OnMapEventS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MapEventTrainS2C.OnMapEventTrainS2CServerCallBackAsync = OnMapEventTrainS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GameScoreChangeS2C.OnGameScoreChangeS2CServerCallBackAsync = OnGameScoreChangeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MapEventCrabS2C.OnMapEventCrabS2CServerCallBackAsync = OnMapEventCrabS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.LuckyStarMissionChangeS2C.OnLuckyStarMissionChangeS2CServerCallBackAsync = OnLuckyStarMissionChangeS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.MapEventS2C.OnMapEventS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MapEventTrainS2C.OnMapEventTrainS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GameScoreChangeS2C.OnGameScoreChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MapEventCrabS2C.OnMapEventCrabS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.LuckyStarMissionChangeS2C.OnLuckyStarMissionChangeS2CServerCallBackAsync = null;
	}

	private async UniTask OnMapEventCrabS2CServerCallBackAsync(MapEventCrabS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0 && SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager015 mapGimmickManager)
		{
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(model.Path[model.Path.Count / 2]);
			SimpleSingletonProvider<CameraManager>.inst.EnableShowCamera(landById.transform.position);
			await mapGimmickManager.ActiveRoadLine(model.Path);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
		}
	}

	private async UniTask OnMapEventS2CServerCallBack(MapEventS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			Type type = Type.GetType($"Core.MapEvent_{model.EventId}");
			MapEvent instance = ((type != null) ? (Activator.CreateInstance(type) as MapEvent) : null);
			if (instance != null)
			{
				await instance.MapEventShowByEvent_Front();
			}
			if (StaticConfigure.MapEvent.InfoDict.TryGetValue(model.EventId, out var _config) && !_config.IsHide)
			{
				await (await SimpleSingletonProvider<UIManager>.inst.landEvent.ShowLand()).RefreshMapEventData(_config.CardID);
				SimpleSingletonProvider<GameLogicManager>.inst.campaign.campaignData?.TriggerMapEvent(model.EventId);
			}
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager016 mapGimmickManager)
			{
				mapGimmickManager.PlayEnvironmentShowByMapEvent();
			}
			if (instance != null)
			{
				await instance.MapEventShowByEvent();
			}
		}
	}

	private async UniTask OnMapEventTrainS2CServerCallBack(MapEventTrainS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(model.Path[model.Path.Count / 2]);
			SimpleSingletonProvider<CameraManager>.inst.EnableShowCamera(landById.transform.position);
			await TrainManager.inst.ActiveRoadLine(model.Path);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
		}
	}

	private async UniTask OnGameScoreChangeS2CServerCallBack(GameScoreChangeS2C model, int errid, bool isdispatch)
	{
		if (errid != 0 || SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING)
		{
			return;
		}
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null)
		{
			if (curRoomInfo.IsAsymmetricalBattle())
			{
				signal.score.Dispatch(model.SpecialScore);
			}
			else if (curRoomInfo.IsLuckyStarBattle())
			{
				curRoomInfo.UpdateLuckyStar(model.TeamId, model.SpecialScore);
				signal.luckyStar.Dispatch(model.TeamId, t2: true);
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnLuckyStarMissionChangeS2CServerCallBackAsync(LuckyStarMissionChangeS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING)
		{
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById == null)
		{
			Debug.LogError($"通过服务器下发的玩家id: {model.PlayerId} 无法获取对应玩家数据");
			return;
		}
		await playerDataById.player.UpdateLuckyStarMission(model.LuckyStarMission);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
		{
			signal.luckyStarMission.Dispatch(model.LuckyStarMission.DefId);
		}
	}
}
