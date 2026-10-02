using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Scripting;
using party.model;
using party.protocol;

namespace Core.Tutorial;

[Preserve]
public class MapMission_330020 : BaseMapMission, IKillMonsterMission
{
	public MapMission_330020()
	{
		int num = 330020;
		if (!StaticConfigure.PVEMission.InfoDict.TryGetValue(num, out _missionInfo))
		{
			Debug.LogError("无法通过ID:330020, 在MapEvent.InfoDict中获取正确的配置");
			return;
		}
		int safeByIndex = _missionInfo.MissionParam.GetSafeByIndex(0);
		_mapMissionNotifyS2C = new MapMissionNotifyS2C
		{
			Mission = new MapMission
			{
				MissionState = MapMission.Types.State.None,
				MissionId = num,
				Targets = 
				{
					new MapMissionTarget
					{
						TargetId = { (IEnumerable<int>)_missionInfo.MissionTarget },
						TargetNum = safeByIndex,
						TargetType = MapMissionTarget.Types.Type.KillMonster
					}
				}
			}
		};
	}

	public override async UniTask TryActiveMapMission()
	{
		if (_missionInfo == null || _mapMissionNotifyS2C == null)
		{
			return;
		}
		if (_missionInfo.PveMissionTriggerType != PVEMissionTriggerType.GameProgress)
		{
			Debug.LogError($"任务：{_missionInfo.Id} 的触发类型是：{_missionInfo.PveMissionTriggerType}，当前类不可用");
			return;
		}
		int safeByIndex = _missionInfo.Triggerparams.GetSafeByIndex(0);
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.GameMaxProgress == safeByIndex)
		{
			_mapMissionNotifyS2C.Mission.MissionState = MapMission.Types.State.Accept;
			await SendMapMissionData();
		}
	}

	public async UniTask TryUpdateKillMonsterProgress(long playerId)
	{
		if (_mapMissionNotifyS2C == null)
		{
			return;
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById == null)
		{
			return;
		}
		int heroId = playerDataById.player.Hero.HeroId;
		foreach (MapMissionTarget target in _mapMissionNotifyS2C.Mission.Targets)
		{
			if (target.TargetId.Contains(heroId))
			{
				target.TargetNum = Mathf.Max(target.TargetNum - 1, 0);
			}
		}
		await SendMapMissionData();
	}

	public override async UniTask GrantRewards()
	{
		if (_mapMissionNotifyS2C == null)
		{
			return;
		}
		MapMission.Types.State missionState = _mapMissionNotifyS2C.Mission.MissionState;
		if (missionState == MapMission.Types.State.None || missionState == MapMission.Types.State.Complete)
		{
			return;
		}
		foreach (MapMissionTarget target in _mapMissionNotifyS2C.Mission.Targets)
		{
			if (target.TargetNum > 0)
			{
				return;
			}
		}
		_mapMissionNotifyS2C.Mission.MissionState = MapMission.Types.State.Complete;
		await SendMapMissionData();
		TutorialBoardRelicManager relicManager = TutorialGame.GetSystem<TutorialBoardManager>().relicManager;
		List<int> relicIds = relicManager.GetSecondMissionReward();
		List<BattlePlayerData> players = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		for (int i = 0; i < players.Count; i++)
		{
			if (players[i].characterType == CharacterType.Hero)
			{
				await relicManager.DealSelectRelic(players[i].player.Id, relicIds);
			}
		}
	}
}
