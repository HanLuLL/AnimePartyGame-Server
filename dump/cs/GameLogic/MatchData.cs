using System;
using System.Collections.Generic;
using Core.Net;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;

namespace GameLogic;

public class MatchData
{
	private readonly List<(int, bool)> _mapIds = new List<(int, bool)>();

	public readonly List<int> DefaultDifficultyConfigTypes = new List<int> { 0, 1, 2, 3 };

	private List<RoomPlayer> _teamPlayers = new List<RoomPlayer>(4);

	private int _matchMapId;

	private long _teamId;

	private MatchMode _matchMode;

	private MatchTeamInfo.Types.State _matchStatus;

	private int _matchDifficulty;

	private long _matchLeaderId;

	private long _matchStartTime;

	public int readyTime => StaticConfigure.Match.Parmss[0].ReadyTime;

	public int afkLimit => StaticConfigure.Match.Parmss[0].AfkLimit;

	public bool InTeam
	{
		get
		{
			if (_teamId != 0L)
			{
				return _teamPlayers.Count > 0;
			}
			return false;
		}
	}

	public List<(int, bool)> MapIds => _mapIds;

	public List<RoomPlayer> TeamPlayers => _teamPlayers;

	public int MatchMapId => _matchMapId;

	public long TeamId
	{
		get
		{
			return _teamId;
		}
		set
		{
			if (!value.Equals(_teamId))
			{
				_teamId = value;
			}
		}
	}

	public MatchMode MatchMode => _matchMode;

	public MatchTeamInfo.Types.State MatchStatus
	{
		get
		{
			return _matchStatus;
		}
		set
		{
			if (!value.Equals(_matchStatus))
			{
				_matchStatus = value;
			}
		}
	}

	public int MatchDifficulty => _matchDifficulty;

	public long MatchLeaderId => _matchLeaderId;

	public long MatchStartTime => _matchStartTime;

	public void UpdateMapInfo(int mapModeType)
	{
		_mapIds.Clear();
		RepeatedField<int> mapID = mapModeType.GetGameModeInfoConfigure().MapID;
		RepeatedField<MapInfoConfigure> mapInfoConfigs = StaticConfigure.Map.GetMapInfoConfigs();
		for (int i = 0; i < mapID.Count; i++)
		{
			for (int j = 0; j < mapInfoConfigs.Count; j++)
			{
				MapInfoConfigure mapInfoConfigure = mapInfoConfigs[j];
				if (mapInfoConfigure.Id != mapID[i])
				{
					continue;
				}
				if (!(mapInfoConfigure.BeginTime != null) || !(mapInfoConfigure.EndTime != null) || !TimeHelper.ValidityTime(mapInfoConfigure.BeginTime, mapInfoConfigure.EndTime))
				{
					break;
				}
				if (BattleConfig.IsPVE(mapModeType))
				{
					RepeatedField<MapMapLevelConfigureItem> mapLevelConfigureItems = mapInfoConfigure.Id.GetMapLevelConfigureItems();
					if (mapLevelConfigureItems.Count > 4 && TimeHelper.ValidityTime(mapLevelConfigureItems[4].BeginTime, mapLevelConfigureItems[4].EndTime))
					{
						_mapIds.Insert(0, (mapInfoConfigure.Id, true));
					}
				}
				_mapIds.Add((mapInfoConfigure.Id, false));
				break;
			}
		}
		if (mapModeType != 7 && mapModeType != 11)
		{
			_mapIds.Add((0, false));
		}
	}

	public MatchInfoConfigure GetTodayRandomMatchInfo()
	{
		List<MatchInfoConfigure> mapInfos = GetMapInfos();
		if (mapInfos.Count == 0)
		{
			return null;
		}
		return MonoSingletonProvider<NetManager>.inst.ServerTime.AddHours(-4.0).DayOfWeek switch
		{
			DayOfWeek.Monday => mapInfos[0], 
			DayOfWeek.Tuesday => mapInfos[1], 
			DayOfWeek.Wednesday => mapInfos[2], 
			DayOfWeek.Thursday => mapInfos[3], 
			DayOfWeek.Friday => mapInfos[4], 
			DayOfWeek.Saturday => mapInfos[5], 
			DayOfWeek.Sunday => mapInfos[6], 
			_ => mapInfos[0], 
		};
	}

	private List<MatchInfoConfigure> GetMapInfos()
	{
		int curMapMode = (int)GetCurMapMode();
		List<MatchInfoConfigure> list = new List<MatchInfoConfigure>();
		RepeatedField<MatchInfoConfigure> infos = StaticConfigure.Match.Infos;
		for (int i = 0; i < infos.Count; i++)
		{
			if (infos[i].Id / 100 == curMapMode)
			{
				list.Add(infos[i]);
			}
		}
		return list;
	}

	public List<GameModeInfoConfigure> GetPVPGameMode()
	{
		List<GameModeInfoConfigure> list = new List<GameModeInfoConfigure>();
		List<GameModeInfoConfigure> gameModeInfos = SimpleSingletonProvider<GameLogicManager>.inst.roomList.gameModeInfos;
		for (int i = 0; i < gameModeInfos.Count; i++)
		{
			if (gameModeInfos[i].MapModeType != MapModeType.Pve && gameModeInfos[i].IsMatch && TimeHelper.ValidityTime(gameModeInfos[i].BeginTime, gameModeInfos[i].EndTime))
			{
				list.Add(gameModeInfos[i]);
			}
		}
		return list;
	}

	public void UpdateMatchInfo(MatchTeamInfo matchInfo)
	{
		UpdateMatchTeamData(matchInfo);
		_teamId = matchInfo.Id;
		_matchMapId = matchInfo.MapId;
		_matchMode = matchInfo.Mode;
		_matchStatus = matchInfo.State;
		_matchDifficulty = matchInfo.Difficulty;
		_matchLeaderId = matchInfo.LeaderId;
		_matchStartTime = matchInfo.MatchTime;
	}

	public MatchMode GetMatchMode(MapModeType mapMode)
	{
		return mapMode switch
		{
			MapModeType.Ultra => MatchMode.Ultra, 
			MapModeType.Standard => MatchMode.Standard, 
			MapModeType.Pve => MatchMode.Pve, 
			MapModeType.AsymmetricalBattle => MatchMode.AsymmetricalBattle, 
			MapModeType.LuckyStarBattle => MatchMode.LuckyStarBattle, 
			MapModeType.MutatorPve => MatchMode.MutatorPve, 
			_ => MatchMode.None, 
		};
	}

	public MapModeType GetCurMapMode()
	{
		return _matchMode switch
		{
			MatchMode.Ultra => MapModeType.Ultra, 
			MatchMode.Standard => MapModeType.Standard, 
			MatchMode.Pve => MapModeType.Pve, 
			MatchMode.AsymmetricalBattle => MapModeType.AsymmetricalBattle, 
			MatchMode.LuckyStarBattle => MapModeType.LuckyStarBattle, 
			MatchMode.MutatorPve => MapModeType.MutatorPve, 
			_ => MapModeType.None, 
		};
	}

	private void UpdateMatchTeamData(MatchTeamInfo matchInfo)
	{
		if (matchInfo == null)
		{
			return;
		}
		RepeatedField<Player> players = matchInfo.Players;
		List<RoomPlayer> list = new List<RoomPlayer>(4);
		List<int> list2 = new List<int>(4);
		foreach (RoomPlayer teamPlayer in _teamPlayers)
		{
			if (teamPlayer.coverNameId > 0)
			{
				list2.Add(teamPlayer.coverNameId);
			}
		}
		foreach (Player serverPlayer in players)
		{
			RoomPlayer roomPlayer = _teamPlayers?.Find((RoomPlayer x) => x.Id == serverPlayer.Id);
			if (roomPlayer != null)
			{
				roomPlayer.UpdateFromServer(serverPlayer);
				list.Add(roomPlayer);
			}
			else
			{
				int num = 0;
				if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(serverPlayer.Id))
				{
					num = PlayerConfigure.GetConverId(list2);
					list2.Add(num);
				}
				roomPlayer = new RoomPlayer(serverPlayer, num);
				list.Add(roomPlayer);
			}
			if (matchInfo.PlayerReady.TryGetValue(roomPlayer.Id, out var value))
			{
				roomPlayer.RoomReady = value;
			}
		}
		_teamPlayers = list;
	}

	public void RemoveTeamPlayer(long exitPlayerId)
	{
		if (_teamPlayers != null)
		{
			RoomPlayer roomPlayer = _teamPlayers.Find((RoomPlayer x) => x.Id == exitPlayerId);
			if (roomPlayer != null)
			{
				_teamPlayers.Remove(roomPlayer);
			}
		}
	}

	public void Dispose()
	{
		_teamId = 0L;
		_teamPlayers.Clear();
	}

	public bool IsVailOperate()
	{
		return _matchStatus == MatchTeamInfo.Types.State.Waiting;
	}

	public (int, int) GetDefaultMapInfo(int mapModeType)
	{
		int item = 0;
		int item2 = 0;
		UpdateMapInfo(mapModeType);
		if (BattleConfig.IsPVE(mapModeType))
		{
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.match.IsVailDifficulty(4);
			foreach (var (num, flag2) in _mapIds)
			{
				if (num != 0)
				{
					if (flag2 && flag)
					{
						item = num;
						item2 = 4;
						break;
					}
					if (!flag2)
					{
						item = num;
						item2 = DefaultDifficultyConfigTypes.GetSafeByIndex(0);
						break;
					}
				}
			}
		}
		else
		{
			item = _mapIds.GetSafeByIndex(0).Item1;
			item2 = 0;
		}
		return (item, item2);
	}

	public void UpdatePlayerReady(long playerId, bool isReady)
	{
		RoomPlayer roomPlayer = _teamPlayers.Find((RoomPlayer x) => x.Id == playerId);
		if (roomPlayer != null)
		{
			roomPlayer.RoomReady = isReady;
		}
	}

	public void ClearReadyStatus()
	{
		for (int i = 0; i < _teamPlayers.Count; i++)
		{
			_teamPlayers[i].RoomReady = false;
		}
	}

	public int GetMaxTeammateCountByMode(MapModeType _mapMode)
	{
		if (_mapMode == MapModeType.LuckyStarBattle)
		{
			return 2;
		}
		if (!BattleConfig.IsPVE((int)_mapMode))
		{
			return 1;
		}
		return 4;
	}
}
