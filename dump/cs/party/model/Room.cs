using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Room : IMessage<Room>, IMessage, IEquatable<Room>, IDeepCloneable<Room>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum State
		{
			[OriginalName("none")]
			None = 0,
			[OriginalName("wait")]
			Wait = 1,
			[OriginalName("choice_hero")]
			ChoiceHero = 10,
			[OriginalName("choice_skin")]
			ChoiceSkin = 15,
			[OriginalName("ready1")]
			Ready1 = 20,
			[OriginalName("running")]
			Running = 25,
			[OriginalName("finish")]
			Finish = 30
		}
	}

	private static readonly MessageParser<Room> _parser = new MessageParser<Room>(() => new Room());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private long id_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int PwdFieldNumber = 3;

	private string pwd_ = "";

	public const int MaxTimeFieldNumber = 4;

	private int maxTime_;

	public const int MapIdFieldNumber = 5;

	private int mapId_;

	public const int MapDataIdFieldNumber = 15;

	private int mapDataId_;

	public const int MasterIdFieldNumber = 6;

	private long masterId_;

	public const int CreateTimeFieldNumber = 7;

	private long createTime_;

	public const int UpdateTimeFieldNumber = 8;

	private long updateTime_;

	public const int PlayersFieldNumber = 9;

	private static readonly FieldCodec<Player> _repeated_players_codec = FieldCodec.ForMessage(74u, Player.Parser);

	private readonly RepeatedField<Player> players_ = new RepeatedField<Player>();

	public const int MonstersFieldNumber = 10;

	private static readonly FieldCodec<Player> _repeated_monsters_codec = FieldCodec.ForMessage(82u, Player.Parser);

	private readonly RepeatedField<Player> monsters_ = new RepeatedField<Player>();

	public const int StateFieldNumber = 11;

	private Types.State state_;

	public const int LandsFieldNumber = 12;

	private static readonly MapField<int, BaseLand>.Codec _map_lands_codec = new MapField<int, BaseLand>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BaseLand.Parser), 98u);

	private readonly MapField<int, BaseLand> lands_ = new MapField<int, BaseLand>();

	public const int PredictsFieldNumber = 13;

	private static readonly FieldCodec<Action> _repeated_predicts_codec = FieldCodec.ForMessage(106u, Action.Parser);

	private readonly RepeatedField<Action> predicts_ = new RepeatedField<Action>();

	public const int RoundFieldNumber = 14;

	private int round_;

	public const int CombatCardsFieldNumber = 16;

	private static readonly FieldCodec<int> _repeated_combatCards_codec = FieldCodec.ForSFixed32(130u);

	private readonly RepeatedField<int> combatCards_ = new RepeatedField<int>();

	public const int EffectCardsFieldNumber = 17;

	private static readonly FieldCodec<int> _repeated_effectCards_codec = FieldCodec.ForSFixed32(138u);

	private readonly RepeatedField<int> effectCards_ = new RepeatedField<int>();

	public const int LastAccessTimeFieldNumber = 18;

	private long lastAccessTime_;

	public const int PlayerIdxFieldNumber = 19;

	private int playerIdx_;

	public const int BuffsFieldNumber = 20;

	private static readonly MapField<long, Buff>.Codec _map_buffs_codec = new MapField<long, Buff>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, Buff.Parser), 162u);

	private readonly MapField<long, Buff> buffs_ = new MapField<long, Buff>();

	public const int BattleFieldNumber = 21;

	private Battle battle_;

	public const int ActionGroupFieldNumber = 22;

	private ActionGroup actionGroup_;

	public const int LandNoHandleFieldNumber = 23;

	private bool landNoHandle_;

	public const int LotteryAwardFieldNumber = 24;

	private int lotteryAward_;

	public const int LandBuffsFieldNumber = 25;

	private static readonly MapField<int, BuffArray>.Codec _map_landBuffs_codec = new MapField<int, BuffArray>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BuffArray.Parser), 202u);

	private readonly MapField<int, BuffArray> landBuffs_ = new MapField<int, BuffArray>();

	public const int CardStackFieldNumber = 26;

	private QuickCardStack cardStack_;

	public const int HallFieldNumber = 28;

	private Gamble hall_;

	public const int BoxFieldNumber = 29;

	private HeroBarBox box_;

	public const int StartTimeFieldNumber = 30;

	private long startTime_;

	public const int UpgradePlanFieldNumber = 31;

	private int upgradePlan_;

	public const int TimePlanFieldNumber = 32;

	private int timePlan_;

	public const int BossTargetGoldFieldNumber = 35;

	private int bossTargetGold_;

	public const int MapTypeFieldNumber = 36;

	private int mapType_;

	public const int SteamLobbyIdFieldNumber = 37;

	private ulong steamLobbyId_;

	public const int MapEventIdsFieldNumber = 38;

	private static readonly FieldCodec<int> _repeated_mapEventIds_codec = FieldCodec.ForSFixed32(306u);

	private readonly RepeatedField<int> mapEventIds_ = new RepeatedField<int>();

	public const int WaitTimeFieldNumber = 39;

	private long waitTime_;

	public const int SpeedTypeFieldNumber = 40;

	private int speedType_;

	public const int MapDelayParamFieldNumber = 41;

	private static readonly MapField<int, MapDelayParam>.Codec _map_mapDelayParam_codec = new MapField<int, MapDelayParam>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.MapDelayParam.Parser), 330u);

	private readonly MapField<int, MapDelayParam> mapDelayParam_ = new MapField<int, MapDelayParam>();

	public const int WatchCodeFieldNumber = 42;

	private string watchCode_ = "";

	public const int WatchPlayersFieldNumber = 43;

	private static readonly FieldCodec<Player> _repeated_watchPlayers_codec = FieldCodec.ForMessage(346u, Player.Parser);

	private readonly RepeatedField<Player> watchPlayers_ = new RepeatedField<Player>();

	public const int TrainCountFieldNumber = 44;

	private int trainCount_;

	public const int ActionTimeOutFieldNumber = 45;

	private long actionTimeOut_;

	public const int GameProgressFieldNumber = 46;

	private int gameProgress_;

	public const int GameMaxProgressFieldNumber = 47;

	private int gameMaxProgress_;

	public const int DifficultyFieldNumber = 48;

	private int difficulty_;

	public const int PveBossActiveFieldNumber = 49;

	private bool pveBossActive_;

	public const int MapMissionsFieldNumber = 50;

	private static readonly FieldCodec<MapMission> _repeated_mapMissions_codec = FieldCodec.ForMessage(402u, MapMission.Parser);

	private readonly RepeatedField<MapMission> mapMissions_ = new RepeatedField<MapMission>();

	public const int MonsterAtkAddFieldNumber = 51;

	private int monsterAtkAdd_;

	public const int MonsterDefAddFieldNumber = 52;

	private int monsterDefAdd_;

	public const int MonsterIndexFieldNumber = 55;

	private static readonly MapField<int, int>.Codec _map_monsterIndex_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 442u);

	private readonly MapField<int, int> monsterIndex_ = new MapField<int, int>();

	public const int PlayerTotalDieFieldNumber = 56;

	private int playerTotalDie_;

	public const int IsMatchRoomFieldNumber = 57;

	private bool isMatchRoom_;

	public const int LevelIdFieldNumber = 58;

	private int levelId_;

	public const int VictoryConditionFieldNumber = 59;

	private VictoryCondition victoryCondition_;

	public const int SpecialScoreFieldNumber = 60;

	private int specialScore_;

	public const int MapStatusFieldNumber = 61;

	private static readonly MapField<int, int>.Codec _map_mapStatus_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 490u);

	private readonly MapField<int, int> mapStatus_ = new MapField<int, int>();

	public const int MapIndexFieldNumber = 62;

	private int mapIndex_;

	public const int MapDifficultyIdFieldNumber = 63;

	private int mapDifficultyId_;

	public const int WaitExecEventIdsFieldNumber = 64;

	private static readonly FieldCodec<int> _repeated_waitExecEventIds_codec = FieldCodec.ForSFixed32(514u);

	private readonly RepeatedField<int> waitExecEventIds_ = new RepeatedField<int>();

	public const int VoteInfoFieldNumber = 65;

	private static readonly FieldCodec<PlayerVoteInfo> _repeated_voteInfo_codec = FieldCodec.ForMessage(522u, PlayerVoteInfo.Parser);

	private readonly RepeatedField<PlayerVoteInfo> voteInfo_ = new RepeatedField<PlayerVoteInfo>();

	public const int SkipStoryFieldNumber = 66;

	private bool skipStory_;

	public const int CampIdFieldNumber = 67;

	private int campId_;

	public const int StoryFinishFieldNumber = 68;

	private static readonly MapField<int, bool>.Codec _map_storyFinish_codec = new MapField<int, bool>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForBool(16u, defaultValue: false), 546u);

	private readonly MapField<int, bool> storyFinish_ = new MapField<int, bool>();

	public const int StoryStateFieldNumber = 69;

	private StoryState storyState_;

	public const int ResetWeightFieldNumber = 70;

	private bool resetWeight_;

	public const int RoomLabelFieldNumber = 71;

	private int roomLabel_;

	public const int MatchTeamIdsFieldNumber = 72;

	private static readonly FieldCodec<long> _repeated_matchTeamIds_codec = FieldCodec.ForSFixed64(578u);

	private readonly RepeatedField<long> matchTeamIds_ = new RepeatedField<long>();

	public const int DelayProgressMapEventFieldNumber = 73;

	private static readonly MapField<int, int>.Codec _map_delayProgressMapEvent_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 586u);

	private readonly MapField<int, int> delayProgressMapEvent_ = new MapField<int, int>();

	public const int ChangeSlotApplyListFieldNumber = 74;

	private static readonly FieldCodec<ChangeSlotApplyList> _repeated_changeSlotApplyList_codec = FieldCodec.ForMessage(594u, party.model.ChangeSlotApplyList.Parser);

	private readonly RepeatedField<ChangeSlotApplyList> changeSlotApplyList_ = new RepeatedField<ChangeSlotApplyList>();

	public const int SummonBuffIndexFieldNumber = 75;

	private static readonly MapField<int, int>.Codec _map_summonBuffIndex_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 602u);

	private readonly MapField<int, int> summonBuffIndex_ = new MapField<int, int>();

	public const int VoteIdsFieldNumber = 76;

	private static readonly FieldCodec<int> _repeated_voteIds_codec = FieldCodec.ForSFixed32(610u);

	private readonly RepeatedField<int> voteIds_ = new RepeatedField<int>();

	public const int GameLuckyStarFieldNumber = 77;

	private static readonly MapField<int, int>.Codec _map_gameLuckyStar_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 618u);

	private readonly MapField<int, int> gameLuckyStar_ = new MapField<int, int>();

	public const int RoomTermsFieldNumber = 78;

	private static readonly FieldCodec<int> _repeated_roomTerms_codec = FieldCodec.ForSFixed32(626u);

	private readonly RepeatedField<int> roomTerms_ = new RepeatedField<int>();

	public const int RoomTermEventFieldNumber = 79;

	private static readonly FieldCodec<int> _repeated_roomTermEvent_codec = FieldCodec.ForSFixed32(634u);

	private readonly RepeatedField<int> roomTermEvent_ = new RepeatedField<int>();

	public const int RoomTermConvertNodesFieldNumber = 80;

	private static readonly FieldCodec<int> _repeated_roomTermConvertNodes_codec = FieldCodec.ForSFixed32(642u);

	private readonly RepeatedField<int> roomTermConvertNodes_ = new RepeatedField<int>();

	public const int RoomTermMonsterPoolFieldNumber = 81;

	private static readonly MapField<int, int>.Codec _map_roomTermMonsterPool_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 650u);

	private readonly MapField<int, int> roomTermMonsterPool_ = new MapField<int, int>();

	public const int LuckyStarMissionInfoFieldNumber = 82;

	private static readonly MapField<int, LuckyStarMissionInfo>.Codec _map_luckyStarMissionInfo_codec = new MapField<int, LuckyStarMissionInfo>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.LuckyStarMissionInfo.Parser), 658u);

	private readonly MapField<int, LuckyStarMissionInfo> luckyStarMissionInfo_ = new MapField<int, LuckyStarMissionInfo>();

	public const int ChoiceStartTimeFieldNumber = 83;

	private long choiceStartTime_;

	public const int RoundDispatchedTermsFieldNumber = 84;

	private static readonly FieldCodec<int> _repeated_roundDispatchedTerms_codec = FieldCodec.ForSFixed32(674u);

	private readonly RepeatedField<int> roundDispatchedTerms_ = new RepeatedField<int>();

	public const int HeroPendingTermsFieldNumber = 86;

	private static readonly MapField<long, PendingTermIds>.Codec _map_heroPendingTerms_codec = new MapField<long, PendingTermIds>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, PendingTermIds.Parser), 690u);

	private readonly MapField<long, PendingTermIds> heroPendingTerms_ = new MapField<long, PendingTermIds>();

	public const int GmSetTermFieldNumber = 87;

	private bool gmSetTerm_;

	public const int MapClueFieldNumber = 88;

	private static readonly FieldCodec<Clue> _repeated_mapClue_codec = FieldCodec.ForMessage(706u, Clue.Parser);

	private readonly RepeatedField<Clue> mapClue_ = new RepeatedField<Clue>();

	public const int ClueNumFieldNumber = 89;

	private int clueNum_;

	public const int RoomServerIdFieldNumber = 1001;

	private int roomServerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Room> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[70];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Pwd
	{
		get
		{
			return pwd_;
		}
		set
		{
			pwd_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxTime
	{
		get
		{
			return maxTime_;
		}
		set
		{
			maxTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapId
	{
		get
		{
			return mapId_;
		}
		set
		{
			mapId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapDataId
	{
		get
		{
			return mapDataId_;
		}
		set
		{
			mapDataId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long MasterId
	{
		get
		{
			return masterId_;
		}
		set
		{
			masterId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long CreateTime
	{
		get
		{
			return createTime_;
		}
		set
		{
			createTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long UpdateTime
	{
		get
		{
			return updateTime_;
		}
		set
		{
			updateTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Player> Players => players_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Player> Monsters => monsters_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.State State
	{
		get
		{
			return state_;
		}
		set
		{
			state_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BaseLand> Lands => lands_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Action> Predicts => predicts_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Round
	{
		get
		{
			return round_;
		}
		set
		{
			round_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CombatCards => combatCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> EffectCards => effectCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastAccessTime
	{
		get
		{
			return lastAccessTime_;
		}
		set
		{
			lastAccessTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PlayerIdx
	{
		get
		{
			return playerIdx_;
		}
		set
		{
			playerIdx_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, Buff> Buffs => buffs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Battle Battle
	{
		get
		{
			return battle_;
		}
		set
		{
			battle_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionGroup ActionGroup
	{
		get
		{
			return actionGroup_;
		}
		set
		{
			actionGroup_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool LandNoHandle
	{
		get
		{
			return landNoHandle_;
		}
		set
		{
			landNoHandle_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LotteryAward
	{
		get
		{
			return lotteryAward_;
		}
		set
		{
			lotteryAward_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BuffArray> LandBuffs => landBuffs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickCardStack CardStack
	{
		get
		{
			return cardStack_;
		}
		set
		{
			cardStack_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Gamble Hall
	{
		get
		{
			return hall_;
		}
		set
		{
			hall_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroBarBox Box
	{
		get
		{
			return box_;
		}
		set
		{
			box_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long StartTime
	{
		get
		{
			return startTime_;
		}
		set
		{
			startTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UpgradePlan
	{
		get
		{
			return upgradePlan_;
		}
		set
		{
			upgradePlan_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TimePlan
	{
		get
		{
			return timePlan_;
		}
		set
		{
			timePlan_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BossTargetGold
	{
		get
		{
			return bossTargetGold_;
		}
		set
		{
			bossTargetGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapType
	{
		get
		{
			return mapType_;
		}
		set
		{
			mapType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ulong SteamLobbyId
	{
		get
		{
			return steamLobbyId_;
		}
		set
		{
			steamLobbyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapEventIds => mapEventIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long WaitTime
	{
		get
		{
			return waitTime_;
		}
		set
		{
			waitTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SpeedType
	{
		get
		{
			return speedType_;
		}
		set
		{
			speedType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapDelayParam> MapDelayParam => mapDelayParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string WatchCode
	{
		get
		{
			return watchCode_;
		}
		set
		{
			watchCode_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Player> WatchPlayers => watchPlayers_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TrainCount
	{
		get
		{
			return trainCount_;
		}
		set
		{
			trainCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ActionTimeOut
	{
		get
		{
			return actionTimeOut_;
		}
		set
		{
			actionTimeOut_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GameProgress
	{
		get
		{
			return gameProgress_;
		}
		set
		{
			gameProgress_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GameMaxProgress
	{
		get
		{
			return gameMaxProgress_;
		}
		set
		{
			gameMaxProgress_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Difficulty
	{
		get
		{
			return difficulty_;
		}
		set
		{
			difficulty_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool PveBossActive
	{
		get
		{
			return pveBossActive_;
		}
		set
		{
			pveBossActive_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapMission> MapMissions => mapMissions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MonsterAtkAdd
	{
		get
		{
			return monsterAtkAdd_;
		}
		set
		{
			monsterAtkAdd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MonsterDefAdd
	{
		get
		{
			return monsterDefAdd_;
		}
		set
		{
			monsterDefAdd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> MonsterIndex => monsterIndex_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PlayerTotalDie
	{
		get
		{
			return playerTotalDie_;
		}
		set
		{
			playerTotalDie_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsMatchRoom
	{
		get
		{
			return isMatchRoom_;
		}
		set
		{
			isMatchRoom_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LevelId
	{
		get
		{
			return levelId_;
		}
		set
		{
			levelId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VictoryCondition VictoryCondition
	{
		get
		{
			return victoryCondition_;
		}
		set
		{
			victoryCondition_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SpecialScore
	{
		get
		{
			return specialScore_;
		}
		set
		{
			specialScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> MapStatus => mapStatus_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapIndex
	{
		get
		{
			return mapIndex_;
		}
		set
		{
			mapIndex_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapDifficultyId
	{
		get
		{
			return mapDifficultyId_;
		}
		set
		{
			mapDifficultyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> WaitExecEventIds => waitExecEventIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PlayerVoteInfo> VoteInfo => voteInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool SkipStory
	{
		get
		{
			return skipStory_;
		}
		set
		{
			skipStory_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CampId
	{
		get
		{
			return campId_;
		}
		set
		{
			campId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, bool> StoryFinish => storyFinish_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StoryState StoryState
	{
		get
		{
			return storyState_;
		}
		set
		{
			storyState_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool ResetWeight
	{
		get
		{
			return resetWeight_;
		}
		set
		{
			resetWeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoomLabel
	{
		get
		{
			return roomLabel_;
		}
		set
		{
			roomLabel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> MatchTeamIds => matchTeamIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> DelayProgressMapEvent => delayProgressMapEvent_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChangeSlotApplyList> ChangeSlotApplyList => changeSlotApplyList_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> SummonBuffIndex => summonBuffIndex_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> VoteIds => voteIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> GameLuckyStar => gameLuckyStar_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RoomTerms => roomTerms_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RoomTermEvent => roomTermEvent_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RoomTermConvertNodes => roomTermConvertNodes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> RoomTermMonsterPool => roomTermMonsterPool_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, LuckyStarMissionInfo> LuckyStarMissionInfo => luckyStarMissionInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ChoiceStartTime
	{
		get
		{
			return choiceStartTime_;
		}
		set
		{
			choiceStartTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RoundDispatchedTerms => roundDispatchedTerms_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, PendingTermIds> HeroPendingTerms => heroPendingTerms_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool GmSetTerm
	{
		get
		{
			return gmSetTerm_;
		}
		set
		{
			gmSetTerm_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Clue> MapClue => mapClue_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ClueNum
	{
		get
		{
			return clueNum_;
		}
		set
		{
			clueNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoomServerId
	{
		get
		{
			return roomServerId_;
		}
		set
		{
			roomServerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Room()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Room(Room other)
		: this()
	{
		id_ = other.id_;
		name_ = other.name_;
		pwd_ = other.pwd_;
		maxTime_ = other.maxTime_;
		mapId_ = other.mapId_;
		mapDataId_ = other.mapDataId_;
		masterId_ = other.masterId_;
		createTime_ = other.createTime_;
		updateTime_ = other.updateTime_;
		players_ = other.players_.Clone();
		monsters_ = other.monsters_.Clone();
		state_ = other.state_;
		lands_ = other.lands_.Clone();
		predicts_ = other.predicts_.Clone();
		round_ = other.round_;
		combatCards_ = other.combatCards_.Clone();
		effectCards_ = other.effectCards_.Clone();
		lastAccessTime_ = other.lastAccessTime_;
		playerIdx_ = other.playerIdx_;
		buffs_ = other.buffs_.Clone();
		battle_ = ((other.battle_ != null) ? other.battle_.Clone() : null);
		actionGroup_ = ((other.actionGroup_ != null) ? other.actionGroup_.Clone() : null);
		landNoHandle_ = other.landNoHandle_;
		lotteryAward_ = other.lotteryAward_;
		landBuffs_ = other.landBuffs_.Clone();
		cardStack_ = ((other.cardStack_ != null) ? other.cardStack_.Clone() : null);
		hall_ = ((other.hall_ != null) ? other.hall_.Clone() : null);
		box_ = ((other.box_ != null) ? other.box_.Clone() : null);
		startTime_ = other.startTime_;
		upgradePlan_ = other.upgradePlan_;
		timePlan_ = other.timePlan_;
		bossTargetGold_ = other.bossTargetGold_;
		mapType_ = other.mapType_;
		steamLobbyId_ = other.steamLobbyId_;
		mapEventIds_ = other.mapEventIds_.Clone();
		waitTime_ = other.waitTime_;
		speedType_ = other.speedType_;
		mapDelayParam_ = other.mapDelayParam_.Clone();
		watchCode_ = other.watchCode_;
		watchPlayers_ = other.watchPlayers_.Clone();
		trainCount_ = other.trainCount_;
		actionTimeOut_ = other.actionTimeOut_;
		gameProgress_ = other.gameProgress_;
		gameMaxProgress_ = other.gameMaxProgress_;
		difficulty_ = other.difficulty_;
		pveBossActive_ = other.pveBossActive_;
		mapMissions_ = other.mapMissions_.Clone();
		monsterAtkAdd_ = other.monsterAtkAdd_;
		monsterDefAdd_ = other.monsterDefAdd_;
		monsterIndex_ = other.monsterIndex_.Clone();
		playerTotalDie_ = other.playerTotalDie_;
		isMatchRoom_ = other.isMatchRoom_;
		levelId_ = other.levelId_;
		victoryCondition_ = ((other.victoryCondition_ != null) ? other.victoryCondition_.Clone() : null);
		specialScore_ = other.specialScore_;
		mapStatus_ = other.mapStatus_.Clone();
		mapIndex_ = other.mapIndex_;
		mapDifficultyId_ = other.mapDifficultyId_;
		waitExecEventIds_ = other.waitExecEventIds_.Clone();
		voteInfo_ = other.voteInfo_.Clone();
		skipStory_ = other.skipStory_;
		campId_ = other.campId_;
		storyFinish_ = other.storyFinish_.Clone();
		storyState_ = other.storyState_;
		resetWeight_ = other.resetWeight_;
		roomLabel_ = other.roomLabel_;
		matchTeamIds_ = other.matchTeamIds_.Clone();
		delayProgressMapEvent_ = other.delayProgressMapEvent_.Clone();
		changeSlotApplyList_ = other.changeSlotApplyList_.Clone();
		summonBuffIndex_ = other.summonBuffIndex_.Clone();
		voteIds_ = other.voteIds_.Clone();
		gameLuckyStar_ = other.gameLuckyStar_.Clone();
		roomTerms_ = other.roomTerms_.Clone();
		roomTermEvent_ = other.roomTermEvent_.Clone();
		roomTermConvertNodes_ = other.roomTermConvertNodes_.Clone();
		roomTermMonsterPool_ = other.roomTermMonsterPool_.Clone();
		luckyStarMissionInfo_ = other.luckyStarMissionInfo_.Clone();
		choiceStartTime_ = other.choiceStartTime_;
		roundDispatchedTerms_ = other.roundDispatchedTerms_.Clone();
		heroPendingTerms_ = other.heroPendingTerms_.Clone();
		gmSetTerm_ = other.gmSetTerm_;
		mapClue_ = other.mapClue_.Clone();
		clueNum_ = other.clueNum_;
		roomServerId_ = other.roomServerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Room Clone()
	{
		return new Room(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Room);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Room other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (Pwd != other.Pwd)
		{
			return false;
		}
		if (MaxTime != other.MaxTime)
		{
			return false;
		}
		if (MapId != other.MapId)
		{
			return false;
		}
		if (MapDataId != other.MapDataId)
		{
			return false;
		}
		if (MasterId != other.MasterId)
		{
			return false;
		}
		if (CreateTime != other.CreateTime)
		{
			return false;
		}
		if (UpdateTime != other.UpdateTime)
		{
			return false;
		}
		if (!players_.Equals(other.players_))
		{
			return false;
		}
		if (!monsters_.Equals(other.monsters_))
		{
			return false;
		}
		if (State != other.State)
		{
			return false;
		}
		if (!Lands.Equals(other.Lands))
		{
			return false;
		}
		if (!predicts_.Equals(other.predicts_))
		{
			return false;
		}
		if (Round != other.Round)
		{
			return false;
		}
		if (!combatCards_.Equals(other.combatCards_))
		{
			return false;
		}
		if (!effectCards_.Equals(other.effectCards_))
		{
			return false;
		}
		if (LastAccessTime != other.LastAccessTime)
		{
			return false;
		}
		if (PlayerIdx != other.PlayerIdx)
		{
			return false;
		}
		if (!Buffs.Equals(other.Buffs))
		{
			return false;
		}
		if (!object.Equals(Battle, other.Battle))
		{
			return false;
		}
		if (!object.Equals(ActionGroup, other.ActionGroup))
		{
			return false;
		}
		if (LandNoHandle != other.LandNoHandle)
		{
			return false;
		}
		if (LotteryAward != other.LotteryAward)
		{
			return false;
		}
		if (!LandBuffs.Equals(other.LandBuffs))
		{
			return false;
		}
		if (!object.Equals(CardStack, other.CardStack))
		{
			return false;
		}
		if (!object.Equals(Hall, other.Hall))
		{
			return false;
		}
		if (!object.Equals(Box, other.Box))
		{
			return false;
		}
		if (StartTime != other.StartTime)
		{
			return false;
		}
		if (UpgradePlan != other.UpgradePlan)
		{
			return false;
		}
		if (TimePlan != other.TimePlan)
		{
			return false;
		}
		if (BossTargetGold != other.BossTargetGold)
		{
			return false;
		}
		if (MapType != other.MapType)
		{
			return false;
		}
		if (SteamLobbyId != other.SteamLobbyId)
		{
			return false;
		}
		if (!mapEventIds_.Equals(other.mapEventIds_))
		{
			return false;
		}
		if (WaitTime != other.WaitTime)
		{
			return false;
		}
		if (SpeedType != other.SpeedType)
		{
			return false;
		}
		if (!MapDelayParam.Equals(other.MapDelayParam))
		{
			return false;
		}
		if (WatchCode != other.WatchCode)
		{
			return false;
		}
		if (!watchPlayers_.Equals(other.watchPlayers_))
		{
			return false;
		}
		if (TrainCount != other.TrainCount)
		{
			return false;
		}
		if (ActionTimeOut != other.ActionTimeOut)
		{
			return false;
		}
		if (GameProgress != other.GameProgress)
		{
			return false;
		}
		if (GameMaxProgress != other.GameMaxProgress)
		{
			return false;
		}
		if (Difficulty != other.Difficulty)
		{
			return false;
		}
		if (PveBossActive != other.PveBossActive)
		{
			return false;
		}
		if (!mapMissions_.Equals(other.mapMissions_))
		{
			return false;
		}
		if (MonsterAtkAdd != other.MonsterAtkAdd)
		{
			return false;
		}
		if (MonsterDefAdd != other.MonsterDefAdd)
		{
			return false;
		}
		if (!MonsterIndex.Equals(other.MonsterIndex))
		{
			return false;
		}
		if (PlayerTotalDie != other.PlayerTotalDie)
		{
			return false;
		}
		if (IsMatchRoom != other.IsMatchRoom)
		{
			return false;
		}
		if (LevelId != other.LevelId)
		{
			return false;
		}
		if (!object.Equals(VictoryCondition, other.VictoryCondition))
		{
			return false;
		}
		if (SpecialScore != other.SpecialScore)
		{
			return false;
		}
		if (!MapStatus.Equals(other.MapStatus))
		{
			return false;
		}
		if (MapIndex != other.MapIndex)
		{
			return false;
		}
		if (MapDifficultyId != other.MapDifficultyId)
		{
			return false;
		}
		if (!waitExecEventIds_.Equals(other.waitExecEventIds_))
		{
			return false;
		}
		if (!voteInfo_.Equals(other.voteInfo_))
		{
			return false;
		}
		if (SkipStory != other.SkipStory)
		{
			return false;
		}
		if (CampId != other.CampId)
		{
			return false;
		}
		if (!StoryFinish.Equals(other.StoryFinish))
		{
			return false;
		}
		if (StoryState != other.StoryState)
		{
			return false;
		}
		if (ResetWeight != other.ResetWeight)
		{
			return false;
		}
		if (RoomLabel != other.RoomLabel)
		{
			return false;
		}
		if (!matchTeamIds_.Equals(other.matchTeamIds_))
		{
			return false;
		}
		if (!DelayProgressMapEvent.Equals(other.DelayProgressMapEvent))
		{
			return false;
		}
		if (!changeSlotApplyList_.Equals(other.changeSlotApplyList_))
		{
			return false;
		}
		if (!SummonBuffIndex.Equals(other.SummonBuffIndex))
		{
			return false;
		}
		if (!voteIds_.Equals(other.voteIds_))
		{
			return false;
		}
		if (!GameLuckyStar.Equals(other.GameLuckyStar))
		{
			return false;
		}
		if (!roomTerms_.Equals(other.roomTerms_))
		{
			return false;
		}
		if (!roomTermEvent_.Equals(other.roomTermEvent_))
		{
			return false;
		}
		if (!roomTermConvertNodes_.Equals(other.roomTermConvertNodes_))
		{
			return false;
		}
		if (!RoomTermMonsterPool.Equals(other.RoomTermMonsterPool))
		{
			return false;
		}
		if (!LuckyStarMissionInfo.Equals(other.LuckyStarMissionInfo))
		{
			return false;
		}
		if (ChoiceStartTime != other.ChoiceStartTime)
		{
			return false;
		}
		if (!roundDispatchedTerms_.Equals(other.roundDispatchedTerms_))
		{
			return false;
		}
		if (!HeroPendingTerms.Equals(other.HeroPendingTerms))
		{
			return false;
		}
		if (GmSetTerm != other.GmSetTerm)
		{
			return false;
		}
		if (!mapClue_.Equals(other.mapClue_))
		{
			return false;
		}
		if (ClueNum != other.ClueNum)
		{
			return false;
		}
		if (RoomServerId != other.RoomServerId)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Id != 0L)
		{
			num ^= Id.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (Pwd.Length != 0)
		{
			num ^= Pwd.GetHashCode();
		}
		if (MaxTime != 0)
		{
			num ^= MaxTime.GetHashCode();
		}
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (MapDataId != 0)
		{
			num ^= MapDataId.GetHashCode();
		}
		if (MasterId != 0L)
		{
			num ^= MasterId.GetHashCode();
		}
		if (CreateTime != 0L)
		{
			num ^= CreateTime.GetHashCode();
		}
		if (UpdateTime != 0L)
		{
			num ^= UpdateTime.GetHashCode();
		}
		num ^= players_.GetHashCode();
		num ^= monsters_.GetHashCode();
		if (State != Types.State.None)
		{
			num ^= State.GetHashCode();
		}
		num ^= Lands.GetHashCode();
		num ^= predicts_.GetHashCode();
		if (Round != 0)
		{
			num ^= Round.GetHashCode();
		}
		num ^= combatCards_.GetHashCode();
		num ^= effectCards_.GetHashCode();
		if (LastAccessTime != 0L)
		{
			num ^= LastAccessTime.GetHashCode();
		}
		if (PlayerIdx != 0)
		{
			num ^= PlayerIdx.GetHashCode();
		}
		num ^= Buffs.GetHashCode();
		if (battle_ != null)
		{
			num ^= Battle.GetHashCode();
		}
		if (actionGroup_ != null)
		{
			num ^= ActionGroup.GetHashCode();
		}
		if (LandNoHandle)
		{
			num ^= LandNoHandle.GetHashCode();
		}
		if (LotteryAward != 0)
		{
			num ^= LotteryAward.GetHashCode();
		}
		num ^= LandBuffs.GetHashCode();
		if (cardStack_ != null)
		{
			num ^= CardStack.GetHashCode();
		}
		if (hall_ != null)
		{
			num ^= Hall.GetHashCode();
		}
		if (box_ != null)
		{
			num ^= Box.GetHashCode();
		}
		if (StartTime != 0L)
		{
			num ^= StartTime.GetHashCode();
		}
		if (UpgradePlan != 0)
		{
			num ^= UpgradePlan.GetHashCode();
		}
		if (TimePlan != 0)
		{
			num ^= TimePlan.GetHashCode();
		}
		if (BossTargetGold != 0)
		{
			num ^= BossTargetGold.GetHashCode();
		}
		if (MapType != 0)
		{
			num ^= MapType.GetHashCode();
		}
		if (SteamLobbyId != 0L)
		{
			num ^= SteamLobbyId.GetHashCode();
		}
		num ^= mapEventIds_.GetHashCode();
		if (WaitTime != 0L)
		{
			num ^= WaitTime.GetHashCode();
		}
		if (SpeedType != 0)
		{
			num ^= SpeedType.GetHashCode();
		}
		num ^= MapDelayParam.GetHashCode();
		if (WatchCode.Length != 0)
		{
			num ^= WatchCode.GetHashCode();
		}
		num ^= watchPlayers_.GetHashCode();
		if (TrainCount != 0)
		{
			num ^= TrainCount.GetHashCode();
		}
		if (ActionTimeOut != 0L)
		{
			num ^= ActionTimeOut.GetHashCode();
		}
		if (GameProgress != 0)
		{
			num ^= GameProgress.GetHashCode();
		}
		if (GameMaxProgress != 0)
		{
			num ^= GameMaxProgress.GetHashCode();
		}
		if (Difficulty != 0)
		{
			num ^= Difficulty.GetHashCode();
		}
		if (PveBossActive)
		{
			num ^= PveBossActive.GetHashCode();
		}
		num ^= mapMissions_.GetHashCode();
		if (MonsterAtkAdd != 0)
		{
			num ^= MonsterAtkAdd.GetHashCode();
		}
		if (MonsterDefAdd != 0)
		{
			num ^= MonsterDefAdd.GetHashCode();
		}
		num ^= MonsterIndex.GetHashCode();
		if (PlayerTotalDie != 0)
		{
			num ^= PlayerTotalDie.GetHashCode();
		}
		if (IsMatchRoom)
		{
			num ^= IsMatchRoom.GetHashCode();
		}
		if (LevelId != 0)
		{
			num ^= LevelId.GetHashCode();
		}
		if (victoryCondition_ != null)
		{
			num ^= VictoryCondition.GetHashCode();
		}
		if (SpecialScore != 0)
		{
			num ^= SpecialScore.GetHashCode();
		}
		num ^= MapStatus.GetHashCode();
		if (MapIndex != 0)
		{
			num ^= MapIndex.GetHashCode();
		}
		if (MapDifficultyId != 0)
		{
			num ^= MapDifficultyId.GetHashCode();
		}
		num ^= waitExecEventIds_.GetHashCode();
		num ^= voteInfo_.GetHashCode();
		if (SkipStory)
		{
			num ^= SkipStory.GetHashCode();
		}
		if (CampId != 0)
		{
			num ^= CampId.GetHashCode();
		}
		num ^= StoryFinish.GetHashCode();
		if (StoryState != StoryState.None)
		{
			num ^= StoryState.GetHashCode();
		}
		if (ResetWeight)
		{
			num ^= ResetWeight.GetHashCode();
		}
		if (RoomLabel != 0)
		{
			num ^= RoomLabel.GetHashCode();
		}
		num ^= matchTeamIds_.GetHashCode();
		num ^= DelayProgressMapEvent.GetHashCode();
		num ^= changeSlotApplyList_.GetHashCode();
		num ^= SummonBuffIndex.GetHashCode();
		num ^= voteIds_.GetHashCode();
		num ^= GameLuckyStar.GetHashCode();
		num ^= roomTerms_.GetHashCode();
		num ^= roomTermEvent_.GetHashCode();
		num ^= roomTermConvertNodes_.GetHashCode();
		num ^= RoomTermMonsterPool.GetHashCode();
		num ^= LuckyStarMissionInfo.GetHashCode();
		if (ChoiceStartTime != 0L)
		{
			num ^= ChoiceStartTime.GetHashCode();
		}
		num ^= roundDispatchedTerms_.GetHashCode();
		num ^= HeroPendingTerms.GetHashCode();
		if (GmSetTerm)
		{
			num ^= GmSetTerm.GetHashCode();
		}
		num ^= mapClue_.GetHashCode();
		if (ClueNum != 0)
		{
			num ^= ClueNum.GetHashCode();
		}
		if (RoomServerId != 0)
		{
			num ^= RoomServerId.GetHashCode();
		}
		if (_unknownFields != null)
		{
			num ^= _unknownFields.GetHashCode();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override string ToString()
	{
		return JsonFormatter.ToDiagnosticString(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void WriteTo(CodedOutputStream output)
	{
		output.WriteRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalWriteTo(ref WriteContext output)
	{
		if (Id != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(Id);
		}
		if (Name.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Name);
		}
		if (Pwd.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Pwd);
		}
		if (MaxTime != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(MaxTime);
		}
		if (MapId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(MapId);
		}
		if (MasterId != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(MasterId);
		}
		if (CreateTime != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(CreateTime);
		}
		if (UpdateTime != 0L)
		{
			output.WriteRawTag(65);
			output.WriteSFixed64(UpdateTime);
		}
		players_.WriteTo(ref output, _repeated_players_codec);
		monsters_.WriteTo(ref output, _repeated_monsters_codec);
		if (State != Types.State.None)
		{
			output.WriteRawTag(88);
			output.WriteEnum((int)State);
		}
		lands_.WriteTo(ref output, _map_lands_codec);
		predicts_.WriteTo(ref output, _repeated_predicts_codec);
		if (Round != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(Round);
		}
		if (MapDataId != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(MapDataId);
		}
		combatCards_.WriteTo(ref output, _repeated_combatCards_codec);
		effectCards_.WriteTo(ref output, _repeated_effectCards_codec);
		if (LastAccessTime != 0L)
		{
			output.WriteRawTag(145, 1);
			output.WriteSFixed64(LastAccessTime);
		}
		if (PlayerIdx != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(PlayerIdx);
		}
		buffs_.WriteTo(ref output, _map_buffs_codec);
		if (battle_ != null)
		{
			output.WriteRawTag(170, 1);
			output.WriteMessage(Battle);
		}
		if (actionGroup_ != null)
		{
			output.WriteRawTag(178, 1);
			output.WriteMessage(ActionGroup);
		}
		if (LandNoHandle)
		{
			output.WriteRawTag(184, 1);
			output.WriteBool(LandNoHandle);
		}
		if (LotteryAward != 0)
		{
			output.WriteRawTag(197, 1);
			output.WriteSFixed32(LotteryAward);
		}
		landBuffs_.WriteTo(ref output, _map_landBuffs_codec);
		if (cardStack_ != null)
		{
			output.WriteRawTag(210, 1);
			output.WriteMessage(CardStack);
		}
		if (hall_ != null)
		{
			output.WriteRawTag(226, 1);
			output.WriteMessage(Hall);
		}
		if (box_ != null)
		{
			output.WriteRawTag(234, 1);
			output.WriteMessage(Box);
		}
		if (StartTime != 0L)
		{
			output.WriteRawTag(241, 1);
			output.WriteSFixed64(StartTime);
		}
		if (UpgradePlan != 0)
		{
			output.WriteRawTag(253, 1);
			output.WriteSFixed32(UpgradePlan);
		}
		if (TimePlan != 0)
		{
			output.WriteRawTag(133, 2);
			output.WriteSFixed32(TimePlan);
		}
		if (BossTargetGold != 0)
		{
			output.WriteRawTag(157, 2);
			output.WriteSFixed32(BossTargetGold);
		}
		if (MapType != 0)
		{
			output.WriteRawTag(165, 2);
			output.WriteSFixed32(MapType);
		}
		if (SteamLobbyId != 0L)
		{
			output.WriteRawTag(169, 2);
			output.WriteFixed64(SteamLobbyId);
		}
		mapEventIds_.WriteTo(ref output, _repeated_mapEventIds_codec);
		if (WaitTime != 0L)
		{
			output.WriteRawTag(185, 2);
			output.WriteSFixed64(WaitTime);
		}
		if (SpeedType != 0)
		{
			output.WriteRawTag(197, 2);
			output.WriteSFixed32(SpeedType);
		}
		mapDelayParam_.WriteTo(ref output, _map_mapDelayParam_codec);
		if (WatchCode.Length != 0)
		{
			output.WriteRawTag(210, 2);
			output.WriteString(WatchCode);
		}
		watchPlayers_.WriteTo(ref output, _repeated_watchPlayers_codec);
		if (TrainCount != 0)
		{
			output.WriteRawTag(229, 2);
			output.WriteSFixed32(TrainCount);
		}
		if (ActionTimeOut != 0L)
		{
			output.WriteRawTag(233, 2);
			output.WriteSFixed64(ActionTimeOut);
		}
		if (GameProgress != 0)
		{
			output.WriteRawTag(245, 2);
			output.WriteSFixed32(GameProgress);
		}
		if (GameMaxProgress != 0)
		{
			output.WriteRawTag(253, 2);
			output.WriteSFixed32(GameMaxProgress);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(133, 3);
			output.WriteSFixed32(Difficulty);
		}
		if (PveBossActive)
		{
			output.WriteRawTag(136, 3);
			output.WriteBool(PveBossActive);
		}
		mapMissions_.WriteTo(ref output, _repeated_mapMissions_codec);
		if (MonsterAtkAdd != 0)
		{
			output.WriteRawTag(157, 3);
			output.WriteSFixed32(MonsterAtkAdd);
		}
		if (MonsterDefAdd != 0)
		{
			output.WriteRawTag(165, 3);
			output.WriteSFixed32(MonsterDefAdd);
		}
		monsterIndex_.WriteTo(ref output, _map_monsterIndex_codec);
		if (PlayerTotalDie != 0)
		{
			output.WriteRawTag(197, 3);
			output.WriteSFixed32(PlayerTotalDie);
		}
		if (IsMatchRoom)
		{
			output.WriteRawTag(200, 3);
			output.WriteBool(IsMatchRoom);
		}
		if (LevelId != 0)
		{
			output.WriteRawTag(213, 3);
			output.WriteSFixed32(LevelId);
		}
		if (victoryCondition_ != null)
		{
			output.WriteRawTag(218, 3);
			output.WriteMessage(VictoryCondition);
		}
		if (SpecialScore != 0)
		{
			output.WriteRawTag(229, 3);
			output.WriteSFixed32(SpecialScore);
		}
		mapStatus_.WriteTo(ref output, _map_mapStatus_codec);
		if (MapIndex != 0)
		{
			output.WriteRawTag(245, 3);
			output.WriteSFixed32(MapIndex);
		}
		if (MapDifficultyId != 0)
		{
			output.WriteRawTag(253, 3);
			output.WriteSFixed32(MapDifficultyId);
		}
		waitExecEventIds_.WriteTo(ref output, _repeated_waitExecEventIds_codec);
		voteInfo_.WriteTo(ref output, _repeated_voteInfo_codec);
		if (SkipStory)
		{
			output.WriteRawTag(144, 4);
			output.WriteBool(SkipStory);
		}
		if (CampId != 0)
		{
			output.WriteRawTag(157, 4);
			output.WriteSFixed32(CampId);
		}
		storyFinish_.WriteTo(ref output, _map_storyFinish_codec);
		if (StoryState != StoryState.None)
		{
			output.WriteRawTag(168, 4);
			output.WriteEnum((int)StoryState);
		}
		if (ResetWeight)
		{
			output.WriteRawTag(176, 4);
			output.WriteBool(ResetWeight);
		}
		if (RoomLabel != 0)
		{
			output.WriteRawTag(189, 4);
			output.WriteSFixed32(RoomLabel);
		}
		matchTeamIds_.WriteTo(ref output, _repeated_matchTeamIds_codec);
		delayProgressMapEvent_.WriteTo(ref output, _map_delayProgressMapEvent_codec);
		changeSlotApplyList_.WriteTo(ref output, _repeated_changeSlotApplyList_codec);
		summonBuffIndex_.WriteTo(ref output, _map_summonBuffIndex_codec);
		voteIds_.WriteTo(ref output, _repeated_voteIds_codec);
		gameLuckyStar_.WriteTo(ref output, _map_gameLuckyStar_codec);
		roomTerms_.WriteTo(ref output, _repeated_roomTerms_codec);
		roomTermEvent_.WriteTo(ref output, _repeated_roomTermEvent_codec);
		roomTermConvertNodes_.WriteTo(ref output, _repeated_roomTermConvertNodes_codec);
		roomTermMonsterPool_.WriteTo(ref output, _map_roomTermMonsterPool_codec);
		luckyStarMissionInfo_.WriteTo(ref output, _map_luckyStarMissionInfo_codec);
		if (ChoiceStartTime != 0L)
		{
			output.WriteRawTag(153, 5);
			output.WriteSFixed64(ChoiceStartTime);
		}
		roundDispatchedTerms_.WriteTo(ref output, _repeated_roundDispatchedTerms_codec);
		heroPendingTerms_.WriteTo(ref output, _map_heroPendingTerms_codec);
		if (GmSetTerm)
		{
			output.WriteRawTag(184, 5);
			output.WriteBool(GmSetTerm);
		}
		mapClue_.WriteTo(ref output, _repeated_mapClue_codec);
		if (ClueNum != 0)
		{
			output.WriteRawTag(205, 5);
			output.WriteSFixed32(ClueNum);
		}
		if (RoomServerId != 0)
		{
			output.WriteRawTag(205, 62);
			output.WriteSFixed32(RoomServerId);
		}
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref output);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (Id != 0L)
		{
			num += 9;
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (Pwd.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Pwd);
		}
		if (MaxTime != 0)
		{
			num += 5;
		}
		if (MapId != 0)
		{
			num += 5;
		}
		if (MapDataId != 0)
		{
			num += 5;
		}
		if (MasterId != 0L)
		{
			num += 9;
		}
		if (CreateTime != 0L)
		{
			num += 9;
		}
		if (UpdateTime != 0L)
		{
			num += 9;
		}
		num += players_.CalculateSize(_repeated_players_codec);
		num += monsters_.CalculateSize(_repeated_monsters_codec);
		if (State != Types.State.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)State);
		}
		num += lands_.CalculateSize(_map_lands_codec);
		num += predicts_.CalculateSize(_repeated_predicts_codec);
		if (Round != 0)
		{
			num += 5;
		}
		num += combatCards_.CalculateSize(_repeated_combatCards_codec);
		num += effectCards_.CalculateSize(_repeated_effectCards_codec);
		if (LastAccessTime != 0L)
		{
			num += 10;
		}
		if (PlayerIdx != 0)
		{
			num += 6;
		}
		num += buffs_.CalculateSize(_map_buffs_codec);
		if (battle_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Battle);
		}
		if (actionGroup_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ActionGroup);
		}
		if (LandNoHandle)
		{
			num += 3;
		}
		if (LotteryAward != 0)
		{
			num += 6;
		}
		num += landBuffs_.CalculateSize(_map_landBuffs_codec);
		if (cardStack_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CardStack);
		}
		if (hall_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Hall);
		}
		if (box_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Box);
		}
		if (StartTime != 0L)
		{
			num += 10;
		}
		if (UpgradePlan != 0)
		{
			num += 6;
		}
		if (TimePlan != 0)
		{
			num += 6;
		}
		if (BossTargetGold != 0)
		{
			num += 6;
		}
		if (MapType != 0)
		{
			num += 6;
		}
		if (SteamLobbyId != 0L)
		{
			num += 10;
		}
		num += mapEventIds_.CalculateSize(_repeated_mapEventIds_codec);
		if (WaitTime != 0L)
		{
			num += 10;
		}
		if (SpeedType != 0)
		{
			num += 6;
		}
		num += mapDelayParam_.CalculateSize(_map_mapDelayParam_codec);
		if (WatchCode.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(WatchCode);
		}
		num += watchPlayers_.CalculateSize(_repeated_watchPlayers_codec);
		if (TrainCount != 0)
		{
			num += 6;
		}
		if (ActionTimeOut != 0L)
		{
			num += 10;
		}
		if (GameProgress != 0)
		{
			num += 6;
		}
		if (GameMaxProgress != 0)
		{
			num += 6;
		}
		if (Difficulty != 0)
		{
			num += 6;
		}
		if (PveBossActive)
		{
			num += 3;
		}
		num += mapMissions_.CalculateSize(_repeated_mapMissions_codec);
		if (MonsterAtkAdd != 0)
		{
			num += 6;
		}
		if (MonsterDefAdd != 0)
		{
			num += 6;
		}
		num += monsterIndex_.CalculateSize(_map_monsterIndex_codec);
		if (PlayerTotalDie != 0)
		{
			num += 6;
		}
		if (IsMatchRoom)
		{
			num += 3;
		}
		if (LevelId != 0)
		{
			num += 6;
		}
		if (victoryCondition_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(VictoryCondition);
		}
		if (SpecialScore != 0)
		{
			num += 6;
		}
		num += mapStatus_.CalculateSize(_map_mapStatus_codec);
		if (MapIndex != 0)
		{
			num += 6;
		}
		if (MapDifficultyId != 0)
		{
			num += 6;
		}
		num += waitExecEventIds_.CalculateSize(_repeated_waitExecEventIds_codec);
		num += voteInfo_.CalculateSize(_repeated_voteInfo_codec);
		if (SkipStory)
		{
			num += 3;
		}
		if (CampId != 0)
		{
			num += 6;
		}
		num += storyFinish_.CalculateSize(_map_storyFinish_codec);
		if (StoryState != StoryState.None)
		{
			num += 2 + CodedOutputStream.ComputeEnumSize((int)StoryState);
		}
		if (ResetWeight)
		{
			num += 3;
		}
		if (RoomLabel != 0)
		{
			num += 6;
		}
		num += matchTeamIds_.CalculateSize(_repeated_matchTeamIds_codec);
		num += delayProgressMapEvent_.CalculateSize(_map_delayProgressMapEvent_codec);
		num += changeSlotApplyList_.CalculateSize(_repeated_changeSlotApplyList_codec);
		num += summonBuffIndex_.CalculateSize(_map_summonBuffIndex_codec);
		num += voteIds_.CalculateSize(_repeated_voteIds_codec);
		num += gameLuckyStar_.CalculateSize(_map_gameLuckyStar_codec);
		num += roomTerms_.CalculateSize(_repeated_roomTerms_codec);
		num += roomTermEvent_.CalculateSize(_repeated_roomTermEvent_codec);
		num += roomTermConvertNodes_.CalculateSize(_repeated_roomTermConvertNodes_codec);
		num += roomTermMonsterPool_.CalculateSize(_map_roomTermMonsterPool_codec);
		num += luckyStarMissionInfo_.CalculateSize(_map_luckyStarMissionInfo_codec);
		if (ChoiceStartTime != 0L)
		{
			num += 10;
		}
		num += roundDispatchedTerms_.CalculateSize(_repeated_roundDispatchedTerms_codec);
		num += heroPendingTerms_.CalculateSize(_map_heroPendingTerms_codec);
		if (GmSetTerm)
		{
			num += 3;
		}
		num += mapClue_.CalculateSize(_repeated_mapClue_codec);
		if (ClueNum != 0)
		{
			num += 6;
		}
		if (RoomServerId != 0)
		{
			num += 6;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(Room other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0L)
		{
			Id = other.Id;
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.Pwd.Length != 0)
		{
			Pwd = other.Pwd;
		}
		if (other.MaxTime != 0)
		{
			MaxTime = other.MaxTime;
		}
		if (other.MapId != 0)
		{
			MapId = other.MapId;
		}
		if (other.MapDataId != 0)
		{
			MapDataId = other.MapDataId;
		}
		if (other.MasterId != 0L)
		{
			MasterId = other.MasterId;
		}
		if (other.CreateTime != 0L)
		{
			CreateTime = other.CreateTime;
		}
		if (other.UpdateTime != 0L)
		{
			UpdateTime = other.UpdateTime;
		}
		players_.Add(other.players_);
		monsters_.Add(other.monsters_);
		if (other.State != Types.State.None)
		{
			State = other.State;
		}
		lands_.MergeFrom(other.lands_);
		predicts_.Add(other.predicts_);
		if (other.Round != 0)
		{
			Round = other.Round;
		}
		combatCards_.Add(other.combatCards_);
		effectCards_.Add(other.effectCards_);
		if (other.LastAccessTime != 0L)
		{
			LastAccessTime = other.LastAccessTime;
		}
		if (other.PlayerIdx != 0)
		{
			PlayerIdx = other.PlayerIdx;
		}
		buffs_.MergeFrom(other.buffs_);
		if (other.battle_ != null)
		{
			if (battle_ == null)
			{
				Battle = new Battle();
			}
			Battle.MergeFrom(other.Battle);
		}
		if (other.actionGroup_ != null)
		{
			if (actionGroup_ == null)
			{
				ActionGroup = new ActionGroup();
			}
			ActionGroup.MergeFrom(other.ActionGroup);
		}
		if (other.LandNoHandle)
		{
			LandNoHandle = other.LandNoHandle;
		}
		if (other.LotteryAward != 0)
		{
			LotteryAward = other.LotteryAward;
		}
		landBuffs_.MergeFrom(other.landBuffs_);
		if (other.cardStack_ != null)
		{
			if (cardStack_ == null)
			{
				CardStack = new QuickCardStack();
			}
			CardStack.MergeFrom(other.CardStack);
		}
		if (other.hall_ != null)
		{
			if (hall_ == null)
			{
				Hall = new Gamble();
			}
			Hall.MergeFrom(other.Hall);
		}
		if (other.box_ != null)
		{
			if (box_ == null)
			{
				Box = new HeroBarBox();
			}
			Box.MergeFrom(other.Box);
		}
		if (other.StartTime != 0L)
		{
			StartTime = other.StartTime;
		}
		if (other.UpgradePlan != 0)
		{
			UpgradePlan = other.UpgradePlan;
		}
		if (other.TimePlan != 0)
		{
			TimePlan = other.TimePlan;
		}
		if (other.BossTargetGold != 0)
		{
			BossTargetGold = other.BossTargetGold;
		}
		if (other.MapType != 0)
		{
			MapType = other.MapType;
		}
		if (other.SteamLobbyId != 0L)
		{
			SteamLobbyId = other.SteamLobbyId;
		}
		mapEventIds_.Add(other.mapEventIds_);
		if (other.WaitTime != 0L)
		{
			WaitTime = other.WaitTime;
		}
		if (other.SpeedType != 0)
		{
			SpeedType = other.SpeedType;
		}
		mapDelayParam_.MergeFrom(other.mapDelayParam_);
		if (other.WatchCode.Length != 0)
		{
			WatchCode = other.WatchCode;
		}
		watchPlayers_.Add(other.watchPlayers_);
		if (other.TrainCount != 0)
		{
			TrainCount = other.TrainCount;
		}
		if (other.ActionTimeOut != 0L)
		{
			ActionTimeOut = other.ActionTimeOut;
		}
		if (other.GameProgress != 0)
		{
			GameProgress = other.GameProgress;
		}
		if (other.GameMaxProgress != 0)
		{
			GameMaxProgress = other.GameMaxProgress;
		}
		if (other.Difficulty != 0)
		{
			Difficulty = other.Difficulty;
		}
		if (other.PveBossActive)
		{
			PveBossActive = other.PveBossActive;
		}
		mapMissions_.Add(other.mapMissions_);
		if (other.MonsterAtkAdd != 0)
		{
			MonsterAtkAdd = other.MonsterAtkAdd;
		}
		if (other.MonsterDefAdd != 0)
		{
			MonsterDefAdd = other.MonsterDefAdd;
		}
		monsterIndex_.MergeFrom(other.monsterIndex_);
		if (other.PlayerTotalDie != 0)
		{
			PlayerTotalDie = other.PlayerTotalDie;
		}
		if (other.IsMatchRoom)
		{
			IsMatchRoom = other.IsMatchRoom;
		}
		if (other.LevelId != 0)
		{
			LevelId = other.LevelId;
		}
		if (other.victoryCondition_ != null)
		{
			if (victoryCondition_ == null)
			{
				VictoryCondition = new VictoryCondition();
			}
			VictoryCondition.MergeFrom(other.VictoryCondition);
		}
		if (other.SpecialScore != 0)
		{
			SpecialScore = other.SpecialScore;
		}
		mapStatus_.MergeFrom(other.mapStatus_);
		if (other.MapIndex != 0)
		{
			MapIndex = other.MapIndex;
		}
		if (other.MapDifficultyId != 0)
		{
			MapDifficultyId = other.MapDifficultyId;
		}
		waitExecEventIds_.Add(other.waitExecEventIds_);
		voteInfo_.Add(other.voteInfo_);
		if (other.SkipStory)
		{
			SkipStory = other.SkipStory;
		}
		if (other.CampId != 0)
		{
			CampId = other.CampId;
		}
		storyFinish_.MergeFrom(other.storyFinish_);
		if (other.StoryState != StoryState.None)
		{
			StoryState = other.StoryState;
		}
		if (other.ResetWeight)
		{
			ResetWeight = other.ResetWeight;
		}
		if (other.RoomLabel != 0)
		{
			RoomLabel = other.RoomLabel;
		}
		matchTeamIds_.Add(other.matchTeamIds_);
		delayProgressMapEvent_.MergeFrom(other.delayProgressMapEvent_);
		changeSlotApplyList_.Add(other.changeSlotApplyList_);
		summonBuffIndex_.MergeFrom(other.summonBuffIndex_);
		voteIds_.Add(other.voteIds_);
		gameLuckyStar_.MergeFrom(other.gameLuckyStar_);
		roomTerms_.Add(other.roomTerms_);
		roomTermEvent_.Add(other.roomTermEvent_);
		roomTermConvertNodes_.Add(other.roomTermConvertNodes_);
		roomTermMonsterPool_.MergeFrom(other.roomTermMonsterPool_);
		luckyStarMissionInfo_.MergeFrom(other.luckyStarMissionInfo_);
		if (other.ChoiceStartTime != 0L)
		{
			ChoiceStartTime = other.ChoiceStartTime;
		}
		roundDispatchedTerms_.Add(other.roundDispatchedTerms_);
		heroPendingTerms_.MergeFrom(other.heroPendingTerms_);
		if (other.GmSetTerm)
		{
			GmSetTerm = other.GmSetTerm;
		}
		mapClue_.Add(other.mapClue_);
		if (other.ClueNum != 0)
		{
			ClueNum = other.ClueNum;
		}
		if (other.RoomServerId != 0)
		{
			RoomServerId = other.RoomServerId;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CodedInputStream input)
	{
		input.ReadRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalMergeFrom(ref ParseContext input)
	{
		uint num;
		while ((num = input.ReadTag()) != 0)
		{
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				break;
			case 9u:
				Id = input.ReadSFixed64();
				break;
			case 18u:
				Name = input.ReadString();
				break;
			case 26u:
				Pwd = input.ReadString();
				break;
			case 37u:
				MaxTime = input.ReadSFixed32();
				break;
			case 45u:
				MapId = input.ReadSFixed32();
				break;
			case 49u:
				MasterId = input.ReadSFixed64();
				break;
			case 57u:
				CreateTime = input.ReadSFixed64();
				break;
			case 65u:
				UpdateTime = input.ReadSFixed64();
				break;
			case 74u:
				players_.AddEntriesFrom(ref input, _repeated_players_codec);
				break;
			case 82u:
				monsters_.AddEntriesFrom(ref input, _repeated_monsters_codec);
				break;
			case 88u:
				State = (Types.State)input.ReadEnum();
				break;
			case 98u:
				lands_.AddEntriesFrom(ref input, _map_lands_codec);
				break;
			case 106u:
				predicts_.AddEntriesFrom(ref input, _repeated_predicts_codec);
				break;
			case 117u:
				Round = input.ReadSFixed32();
				break;
			case 125u:
				MapDataId = input.ReadSFixed32();
				break;
			case 130u:
			case 133u:
				combatCards_.AddEntriesFrom(ref input, _repeated_combatCards_codec);
				break;
			case 138u:
			case 141u:
				effectCards_.AddEntriesFrom(ref input, _repeated_effectCards_codec);
				break;
			case 145u:
				LastAccessTime = input.ReadSFixed64();
				break;
			case 157u:
				PlayerIdx = input.ReadSFixed32();
				break;
			case 162u:
				buffs_.AddEntriesFrom(ref input, _map_buffs_codec);
				break;
			case 170u:
				if (battle_ == null)
				{
					Battle = new Battle();
				}
				input.ReadMessage(Battle);
				break;
			case 178u:
				if (actionGroup_ == null)
				{
					ActionGroup = new ActionGroup();
				}
				input.ReadMessage(ActionGroup);
				break;
			case 184u:
				LandNoHandle = input.ReadBool();
				break;
			case 197u:
				LotteryAward = input.ReadSFixed32();
				break;
			case 202u:
				landBuffs_.AddEntriesFrom(ref input, _map_landBuffs_codec);
				break;
			case 210u:
				if (cardStack_ == null)
				{
					CardStack = new QuickCardStack();
				}
				input.ReadMessage(CardStack);
				break;
			case 226u:
				if (hall_ == null)
				{
					Hall = new Gamble();
				}
				input.ReadMessage(Hall);
				break;
			case 234u:
				if (box_ == null)
				{
					Box = new HeroBarBox();
				}
				input.ReadMessage(Box);
				break;
			case 241u:
				StartTime = input.ReadSFixed64();
				break;
			case 253u:
				UpgradePlan = input.ReadSFixed32();
				break;
			case 261u:
				TimePlan = input.ReadSFixed32();
				break;
			case 285u:
				BossTargetGold = input.ReadSFixed32();
				break;
			case 293u:
				MapType = input.ReadSFixed32();
				break;
			case 297u:
				SteamLobbyId = input.ReadFixed64();
				break;
			case 306u:
			case 309u:
				mapEventIds_.AddEntriesFrom(ref input, _repeated_mapEventIds_codec);
				break;
			case 313u:
				WaitTime = input.ReadSFixed64();
				break;
			case 325u:
				SpeedType = input.ReadSFixed32();
				break;
			case 330u:
				mapDelayParam_.AddEntriesFrom(ref input, _map_mapDelayParam_codec);
				break;
			case 338u:
				WatchCode = input.ReadString();
				break;
			case 346u:
				watchPlayers_.AddEntriesFrom(ref input, _repeated_watchPlayers_codec);
				break;
			case 357u:
				TrainCount = input.ReadSFixed32();
				break;
			case 361u:
				ActionTimeOut = input.ReadSFixed64();
				break;
			case 373u:
				GameProgress = input.ReadSFixed32();
				break;
			case 381u:
				GameMaxProgress = input.ReadSFixed32();
				break;
			case 389u:
				Difficulty = input.ReadSFixed32();
				break;
			case 392u:
				PveBossActive = input.ReadBool();
				break;
			case 402u:
				mapMissions_.AddEntriesFrom(ref input, _repeated_mapMissions_codec);
				break;
			case 413u:
				MonsterAtkAdd = input.ReadSFixed32();
				break;
			case 421u:
				MonsterDefAdd = input.ReadSFixed32();
				break;
			case 442u:
				monsterIndex_.AddEntriesFrom(ref input, _map_monsterIndex_codec);
				break;
			case 453u:
				PlayerTotalDie = input.ReadSFixed32();
				break;
			case 456u:
				IsMatchRoom = input.ReadBool();
				break;
			case 469u:
				LevelId = input.ReadSFixed32();
				break;
			case 474u:
				if (victoryCondition_ == null)
				{
					VictoryCondition = new VictoryCondition();
				}
				input.ReadMessage(VictoryCondition);
				break;
			case 485u:
				SpecialScore = input.ReadSFixed32();
				break;
			case 490u:
				mapStatus_.AddEntriesFrom(ref input, _map_mapStatus_codec);
				break;
			case 501u:
				MapIndex = input.ReadSFixed32();
				break;
			case 509u:
				MapDifficultyId = input.ReadSFixed32();
				break;
			case 514u:
			case 517u:
				waitExecEventIds_.AddEntriesFrom(ref input, _repeated_waitExecEventIds_codec);
				break;
			case 522u:
				voteInfo_.AddEntriesFrom(ref input, _repeated_voteInfo_codec);
				break;
			case 528u:
				SkipStory = input.ReadBool();
				break;
			case 541u:
				CampId = input.ReadSFixed32();
				break;
			case 546u:
				storyFinish_.AddEntriesFrom(ref input, _map_storyFinish_codec);
				break;
			case 552u:
				StoryState = (StoryState)input.ReadEnum();
				break;
			case 560u:
				ResetWeight = input.ReadBool();
				break;
			case 573u:
				RoomLabel = input.ReadSFixed32();
				break;
			case 577u:
			case 578u:
				matchTeamIds_.AddEntriesFrom(ref input, _repeated_matchTeamIds_codec);
				break;
			case 586u:
				delayProgressMapEvent_.AddEntriesFrom(ref input, _map_delayProgressMapEvent_codec);
				break;
			case 594u:
				changeSlotApplyList_.AddEntriesFrom(ref input, _repeated_changeSlotApplyList_codec);
				break;
			case 602u:
				summonBuffIndex_.AddEntriesFrom(ref input, _map_summonBuffIndex_codec);
				break;
			case 610u:
			case 613u:
				voteIds_.AddEntriesFrom(ref input, _repeated_voteIds_codec);
				break;
			case 618u:
				gameLuckyStar_.AddEntriesFrom(ref input, _map_gameLuckyStar_codec);
				break;
			case 626u:
			case 629u:
				roomTerms_.AddEntriesFrom(ref input, _repeated_roomTerms_codec);
				break;
			case 634u:
			case 637u:
				roomTermEvent_.AddEntriesFrom(ref input, _repeated_roomTermEvent_codec);
				break;
			case 642u:
			case 645u:
				roomTermConvertNodes_.AddEntriesFrom(ref input, _repeated_roomTermConvertNodes_codec);
				break;
			case 650u:
				roomTermMonsterPool_.AddEntriesFrom(ref input, _map_roomTermMonsterPool_codec);
				break;
			case 658u:
				luckyStarMissionInfo_.AddEntriesFrom(ref input, _map_luckyStarMissionInfo_codec);
				break;
			case 665u:
				ChoiceStartTime = input.ReadSFixed64();
				break;
			case 674u:
			case 677u:
				roundDispatchedTerms_.AddEntriesFrom(ref input, _repeated_roundDispatchedTerms_codec);
				break;
			case 690u:
				heroPendingTerms_.AddEntriesFrom(ref input, _map_heroPendingTerms_codec);
				break;
			case 696u:
				GmSetTerm = input.ReadBool();
				break;
			case 706u:
				mapClue_.AddEntriesFrom(ref input, _repeated_mapClue_codec);
				break;
			case 717u:
				ClueNum = input.ReadSFixed32();
				break;
			case 8013u:
				RoomServerId = input.ReadSFixed32();
				break;
			}
		}
	}
}
