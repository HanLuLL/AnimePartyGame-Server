using System.Collections.Generic;
using Core.Tutorial.Tools;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace Core.Tutorial;

public class TutorialBoardMissionManager
{
	private readonly Dictionary<int, BaseMapEvent> _mapEvents = new Dictionary<int, BaseMapEvent>();

	private readonly Dictionary<int, BaseMapMission> _mapMissions = new Dictionary<int, BaseMapMission>();

	public void Initialize()
	{
		InitMapEventData();
		InitMapMissionData();
	}

	public void Dispose()
	{
	}

	public async UniTask DealDeadEvent(long player)
	{
		foreach (KeyValuePair<int, BaseMapMission> mapMission in _mapMissions)
		{
			if (mapMission.Value is IKillMonsterMission killMonsterMission)
			{
				await killMonsterMission.TryUpdateKillMonsterProgress(player);
			}
		}
	}

	public async UniTask OnRoundStart()
	{
		await TryActiveMapEvent();
		await TryActiveMapMission();
	}

	public async UniTask OnRoundEnd()
	{
		foreach (KeyValuePair<int, BaseMapMission> mapMission in _mapMissions)
		{
			await mapMission.Value.GrantRewards();
		}
	}

	private void InitMapEventData()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo == null)
		{
			return;
		}
		List<MapEventInfoConfigure> roomMapEvents = SimpleSingletonProvider<GameLogicManager>.inst.gameModePlay.GetRoomMapEvents(roomInfo.MapId, roomInfo.MapDifficultyId, roomInfo.Difficulty);
		if (roomMapEvents == null || roomMapEvents.Count == 0)
		{
			return;
		}
		foreach (MapEventInfoConfigure item in roomMapEvents)
		{
			BaseMapEvent classInstance = Core.Tutorial.Tools.ReflectionHelper.GetClassInstance<BaseMapEvent>($"Core.Tutorial.MapEvent_{item.Id}");
			if (classInstance != null)
			{
				_mapEvents.Add(item.Id, classInstance);
			}
		}
	}

	private async UniTask TryActiveMapEvent()
	{
		if (_mapEvents.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, BaseMapEvent> mapEvent in _mapEvents)
		{
			await mapEvent.Value.TryActiveEvent();
		}
	}

	private void InitMapMissionData()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo == null)
		{
			return;
		}
		RepeatedField<int> repeatedField = null;
		RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = roomInfo.MapDifficultyId.GetMapGameDifficultyItems();
		for (int i = 0; i < mapGameDifficultyItems.Count; i++)
		{
			if (mapGameDifficultyItems[i].Index == roomInfo.Difficulty)
			{
				repeatedField = mapGameDifficultyItems[i].PveMissions;
				break;
			}
		}
		if (repeatedField == null || repeatedField.Count == 0)
		{
			return;
		}
		foreach (int item in repeatedField)
		{
			BaseMapMission classInstance = Core.Tutorial.Tools.ReflectionHelper.GetClassInstance<BaseMapMission>($"Core.Tutorial.MapMission_{item}");
			if (classInstance != null)
			{
				_mapMissions.Add(item, classInstance);
			}
		}
	}

	private async UniTask TryActiveMapMission()
	{
		if (_mapMissions.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, BaseMapMission> mapMission in _mapMissions)
		{
			await mapMission.Value.TryActiveMapMission();
		}
	}
}
