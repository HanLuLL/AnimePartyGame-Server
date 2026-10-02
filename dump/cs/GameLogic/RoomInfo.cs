using System.Collections.Generic;
using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace GameLogic;

public class RoomInfo
{
	public Room info;

	public Room.Types.State State;

	private int _MapIndex;

	private int _MapId;

	private int _MapDifficultyId;

	private long _MasterId;

	public readonly RoomBattleBGM battleBgm = new RoomBattleBGM();

	private int _Round;

	private readonly Dictionary<int, int> _gameLuckyStarDict = new Dictionary<int, int>();

	public RepeatedField<int> RoomTerms = new RepeatedField<int>();

	public int StoryId;

	public readonly Dictionary<long, RoomPlayer> RoomActorDict = new Dictionary<long, RoomPlayer>();

	public readonly List<RoomPlayer> Players = new List<RoomPlayer>();

	public readonly List<RoomPlayer> Monsters = new List<RoomPlayer>();

	public int WatchCount;

	public long StartTime;

	private int _Difficulty;

	private readonly Dictionary<int, MapMissionData> _mapMissionDict = new Dictionary<int, MapMissionData>();

	private QuickCardStack _CardStack;

	private Gamble _Hall;

	public readonly List<long> _finishSn = new List<long>();

	public long Id => info.Id;

	public string Name => info.Name;

	public string Pwd => info.Pwd;

	public int MapIndex => _MapIndex;

	public int MapId => _MapId;

	public int MapDifficultyId => _MapDifficultyId;

	public MapSceneConfigure SceneConfig
	{
		get
		{
			if (!StaticConfigure.Map.InfoDict.TryGetValue(_MapId, out var value))
			{
				Debug.LogError($"无法通过{_MapId} 加载当前地图");
				return null;
			}
			if (value.Mids.Count <= MapIndex)
			{
				return null;
			}
			if (!StaticConfigure.Map.SceneDict.TryGetValue(value.Mids[MapIndex], out var value2))
			{
				Debug.LogError($"无法通过{_MapId} 加载当前地图");
				return null;
			}
			return value2;
		}
	}

	public int MapType
	{
		get
		{
			if (info.MapType == 0)
			{
				info.MapType = 1;
			}
			return info.MapType;
		}
	}

	public long CreateTime => info.CreateTime;

	public long UpdateTime => info.UpdateTime;

	public int UpgradePlan => info.UpgradePlan;

	public int TimePlan => info.TimePlan;

	public int speedType => 2;

	public long MasterId => _MasterId;

	public bool IsMatchRoom => info.IsMatchRoom;

	public HeroBarBox Box => info.Box;

	public int Round => Mathf.Max(_Round, 1);

	public Dictionary<int, int> GameLuckyStarDict => _gameLuckyStarDict;

	public bool IsTerms
	{
		get
		{
			if (info?.RoomTerms != null)
			{
				return info.RoomTerms.Count > 0;
			}
			return false;
		}
	}

	public int RoomPlayerMaxCount => GetRoomPlayerMaxCount(MapType);

	public string watchCode => info?.WatchCode;

	public RepeatedField<Player> WatchPlayers => info?.WatchPlayers;

	public int Difficulty => _Difficulty;

	public int GameMaxProgress { get; private set; }

	public int GameProgress { get; private set; }

	public Dictionary<int, MapMissionData> MapMissionDict => _mapMissionDict;

	public Dictionary<int, ClueMissionData> ClueMissions { get; } = new Dictionary<int, ClueMissionData>();

	public int ClueNum { get; private set; }

	public QuickCardStack CardStack => _CardStack;

	public Gamble Hall => _Hall;

	public RoomInfo(Room _info)
	{
		UpdateBaseInfo(_info);
		UpdateRoomPlayer(_info.Players);
		UpdateRoomMonster(_info.Monsters);
		UpdateGameModeData(_info);
	}

	public void UpdateBaseInfo(Room _info)
	{
		info = _info;
		State = _info.State;
		UpdateMapId(info.MapId, info.MapIndex);
		UpdatePVEData();
		UpdateMasterId(_info.MasterId);
		battleBgm.ResetBGM();
		UpdateRound(info.Round);
		UpdateRoomTerms(info?.RoomTerms);
		UpdateClueMissionData(info?.MapClue, info?.ClueNum ?? 0);
		StoryId = 0;
	}

	public void UpdateGameModeData(Room _info)
	{
		UpdateLuckyStarData(_info.GameLuckyStar, _info.LuckyStarMissionInfo);
	}

	public void UpdateMasterId(long _masterId)
	{
		if (_masterId != 0L)
		{
			_MasterId = _masterId;
		}
	}

	public void UpdateMapId(int mapId, int mapIndex)
	{
		_MapId = mapId;
		_MapIndex = mapIndex;
	}

	public void UpdateRound(int round)
	{
		_Round = round;
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roundChange.Dispatch(_Round);
	}

	public void UpdateRoomPlayer(RepeatedField<Player> _ServerPlayers)
	{
		List<int> list = new List<int>(4);
		foreach (RoomPlayer player in Players)
		{
			if (player.coverNameId > 0)
			{
				list.Add(player.coverNameId);
			}
		}
		foreach (Player _ServerPlayer in _ServerPlayers)
		{
			if (RoomActorDict.TryGetValue(_ServerPlayer.Id, out var value))
			{
				value.UpdateFromServer(_ServerPlayer);
				continue;
			}
			int num = 0;
			if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_ServerPlayer.Id))
			{
				num = PlayerConfigure.GetConverId(list);
				list.Add(num);
			}
			RoomPlayer roomPlayer = new RoomPlayer(_ServerPlayer, num);
			RoomActorDict.Add(_ServerPlayer.Id, roomPlayer);
			Players.Add(roomPlayer);
		}
		for (int i = 0; i < Players.Count; i++)
		{
			long curId = Players[i].Id;
			if (_ServerPlayers.All((Player p) => p.Id != curId))
			{
				RoomActorDict.Remove(curId);
			}
		}
		Players.Clear();
		for (int num2 = 0; num2 < _ServerPlayers.Count; num2++)
		{
			Players.Add(RoomActorDict[_ServerPlayers[num2].Id]);
		}
	}

	public RoomPlayer GetPlayerById(long _id)
	{
		return RoomActorDict.GetValueOrDefault(_id);
	}

	public RoomPlayer GetSelfInfo()
	{
		long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		return GetPlayerById(playerID);
	}

	public RoomPlayer GetPlayerBySlot(int _Slot)
	{
		return Players.Find((RoomPlayer x) => x.Slot == _Slot);
	}

	public void RemovePlayerInfo(long _PlayerId)
	{
		if (State != Room.Types.State.Running && RoomActorDict.ContainsKey(_PlayerId))
		{
			Players.Remove(RoomActorDict[_PlayerId]);
			RoomActorDict.Remove(_PlayerId);
		}
	}

	public void UpdateRoomMonster(RepeatedField<Player> _ServerMonsters)
	{
		foreach (RoomPlayer monster in Monsters)
		{
			RoomActorDict.Remove(monster.Id);
		}
		Monsters.Clear();
		foreach (Player _ServerMonster in _ServerMonsters)
		{
			TryAddMonster(_ServerMonster);
		}
	}

	public RoomPlayer TryAddMonster(Player monster)
	{
		if (RoomActorDict.TryGetValue(monster.Id, out var value))
		{
			value.UpdateFromServer(monster);
		}
		else
		{
			value = new RoomPlayer(monster);
			Monsters.Add(value);
			RoomActorDict.TryAdd(monster.Id, value);
		}
		return value;
	}

	public void UpdateProgress(Room roomInfo)
	{
		for (int i = 0; i < roomInfo.Players.Count; i++)
		{
			RoomActorDict[roomInfo.Players[i].Id].UpdateProgress(roomInfo.Players[i].Progress);
		}
	}

	private void UpdatePVEData()
	{
		_MapDifficultyId = info.MapDifficultyId;
		_Difficulty = info.Difficulty;
		GameProgress = info.GameProgress;
		GameMaxProgress = info.GameMaxProgress;
		_mapMissionDict.Clear();
		if (info.MapMissions == null || info.MapMissions.Count == 0)
		{
			return;
		}
		foreach (MapMission mapMission in info.MapMissions)
		{
			UpdateMapMission(mapMission);
		}
	}

	public async UniTask UpdateMapMission(MapMission _MapMission)
	{
		if (!_mapMissionDict.TryGetValue(_MapMission.MissionId, out var value))
		{
			value = new MapMissionData();
			_mapMissionDict.TryAdd(_MapMission.MissionId, value);
		}
		await value.UpdateMission(_MapMission);
	}

	public void UpdatePveProgress(int progress, int maxProgress)
	{
		GameProgress = progress;
		GameMaxProgress = maxProgress;
	}

	public List<int> GetCurrentConfigMonsterIds()
	{
		List<int> list = null;
		if (StaticConfigure.Map.InfoDict.TryGetValue(MapId, out var value))
		{
			list = value.GetPreloadCharacterIdsInRoom(MapDifficultyId, Difficulty);
		}
		if (list == null)
		{
			Debug.LogWarning($"地图{MapId}没有配置怪物");
			list = new List<int>();
		}
		if (State != Room.Types.State.None && IsTerms)
		{
			foreach (int roomTerm in RoomTerms)
			{
				MutatorInfoConfigure mutatorInfoConfigure = roomTerm.GetMutatorInfoConfigure();
				if (mutatorInfoConfigure == null)
				{
					continue;
				}
				RepeatedField<int> preloadCharacterIds = mutatorInfoConfigure.PreloadCharacterIds;
				if (preloadCharacterIds == null || preloadCharacterIds.Count <= 0)
				{
					continue;
				}
				foreach (int preloadCharacterId in mutatorInfoConfigure.PreloadCharacterIds)
				{
					if (!list.Contains(preloadCharacterId))
					{
						list.Add(preloadCharacterId);
					}
				}
			}
		}
		return list;
	}

	public string HeadURL(long _playerId)
	{
		RoomPlayer playerById = GetPlayerById(_playerId);
		if (playerById == null || _playerId == 0L)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[1].GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
		}
		return playerById.HeadURL();
	}

	public (string, bool) AccountBackgroundURL(long _playerId)
	{
		RoomPlayer playerById = GetPlayerById(_playerId);
		if (playerById == null || _playerId == 0L)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[2].GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
		}
		return playerById.AccountBackgroundURL();
	}

	public FashionEffectConfigure OverKillResultConfig(long _playerId)
	{
		RoomPlayer playerById = GetPlayerById(_playerId);
		if (playerById == null || _playerId == 0L)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[5].GetItemInfoConfigure().SubMeterID.GetFashionFashionEffectConfigure();
		}
		return playerById.OverKillResultConfig();
	}

	public FashionCardBackConfigure CardBackConfig(long _playerId)
	{
		RoomPlayer playerById = GetPlayerById(_playerId);
		if (playerById == null || _playerId == 0L)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[3].GetItemInfoConfigure().SubMeterID.GetFashionCardBackConfigure();
		}
		return playerById.CardBackConfig();
	}

	public FashionDiceConfigure PlayerDiceConfig(long _playerId)
	{
		RoomPlayer playerById = GetPlayerById(_playerId);
		if (playerById == null || _playerId == 0L)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[4].GetItemInfoConfigure().SubMeterID.GetFashionDiceConfigure();
		}
		return playerById.PlayerDiceConfig();
	}

	public void UpdateBattleCache(Room _info)
	{
		_CardStack = _info.CardStack;
		_Hall = _info.Hall;
	}

	public void ClearClientCardStack()
	{
		_CardStack = null;
	}

	public void ClearClientHallInfo()
	{
		_Hall = null;
	}

	public RepeatedField<int> GetForbiddenHeroIds()
	{
		return MapType.GetGameModeInfoConfigure().BanCharacters;
	}

	public async UniTask UpdatePredicts(RepeatedField<Action> _predicts)
	{
		foreach (Action _predict in _predicts)
		{
			await SimpleSingletonProvider<ActionListener>.inst.EnqueueActionsList(_predict);
		}
	}

	public void RecordFinishSn(long _Sn)
	{
		_finishSn.Add(_Sn);
		if (_finishSn.Count > 50)
		{
			_finishSn.RemoveRange(0, 25);
		}
	}

	public bool IsCampaign()
	{
		return BattleConfig.IsCampaign(MapType);
	}

	public bool IsPractice()
	{
		return BattleConfig.IsPractice(MapType);
	}

	public bool IsSingleGameModel()
	{
		return BattleConfig.IsSingleGameModel(MapType);
	}

	public bool IsPVE()
	{
		return BattleConfig.IsPVE(MapType);
	}

	public bool IsPVP()
	{
		return BattleConfig.IsPVP(MapType);
	}

	public bool IsAsymmetricalBattle()
	{
		return BattleConfig.IsAsymmetricalBattle(MapType);
	}

	public bool IsLuckyStarBattle()
	{
		return BattleConfig.IsLuckyStarBattle(MapType);
	}

	public bool IsNovice()
	{
		return BattleConfig.IsNovice(MapType);
	}

	public bool IsMutatorPve()
	{
		return BattleConfig.IsMutatorPve(MapType);
	}

	public bool IsClue()
	{
		if (!IsPVE())
		{
			return false;
		}
		RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = MapDifficultyId.GetMapGameDifficultyItems();
		if (mapGameDifficultyItems != null && mapGameDifficultyItems.Count > 0)
		{
			return mapGameDifficultyItems[0].ExtraModes.Contains(PVEExtraModeType.Clue);
		}
		return false;
	}

	public void Dispose()
	{
		if (battleBgm != null)
		{
			battleBgm.ContinueBGMAfterFight();
		}
	}

	public void UpdateHeroBox(HeroBarBox heroBox)
	{
		if (info != null)
		{
			info.Box = heroBox;
		}
	}

	public HeroBar GetHeroBarById(long playerId)
	{
		if (Box?.Box == null)
		{
			return null;
		}
		Box.Box.TryGetValue(playerId, out var value);
		return value;
	}

	private void UpdateLuckyStarData(MapField<int, int> gameLuckyStar, MapField<int, LuckyStarMissionInfo> luckyStarMissionInfo)
	{
		if (!IsLuckyStarBattle())
		{
			return;
		}
		if (gameLuckyStar != null)
		{
			_gameLuckyStarDict.Clear();
			foreach (var (teamId, star) in gameLuckyStar)
			{
				UpdateLuckyStar(teamId, star);
			}
		}
		if (luckyStarMissionInfo == null)
		{
			return;
		}
		foreach (RoomPlayer player in Players)
		{
			player.InitLuckyStarMissions(luckyStarMissionInfo);
		}
	}

	public void UpdateLuckyStar(int teamId, int star)
	{
		_gameLuckyStarDict[teamId] = star;
	}

	private void UpdateRoomTerms(RepeatedField<int> _roomTerms)
	{
		RoomTerms.Clear();
		if (_roomTerms != null)
		{
			RoomTerms.AddRange(_roomTerms);
		}
	}

	public void TryAddRoomTerms(RepeatedField<int> _roomTerms)
	{
		if (_roomTerms != null)
		{
			RoomTerms.AddRange(_roomTerms);
		}
	}

	public static int GetRoomPlayerMaxCount(MapModeType mapModeType)
	{
		if (mapModeType == MapModeType.MutatorPve)
		{
			return 3;
		}
		return 4;
	}

	public static int GetRoomPlayerMaxCount(int mapModeType)
	{
		return GetRoomPlayerMaxCount((MapModeType)mapModeType);
	}

	public void UpdateClueMissionData(RepeatedField<Clue> infoMapClue, int clueNum)
	{
		ClueMissions.Clear();
		ClueNum = clueNum;
		if (infoMapClue == null)
		{
			return;
		}
		foreach (Clue item in infoMapClue)
		{
			UpdateClueMission(item);
		}
	}

	public ClueMissionData UpdateClueMissionData(Clue clue)
	{
		if (clue == null)
		{
			return null;
		}
		ClueMissionData result = UpdateClueMission(clue);
		ClueNum = ClueMissions.Values.Count((ClueMissionData clueData) => clueData.IsCompleted);
		return result;
	}

	private ClueMissionData UpdateClueMission(Clue clue)
	{
		if (!ClueMissions.TryGetValue(clue.ClueId, out var value))
		{
			value = new ClueMissionData();
			ClueMissions.Add(clue.ClueId, value);
		}
		value.UpdateClue(clue);
		return value;
	}
}
