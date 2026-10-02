using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Player : IMessage<Player>, IMessage, IEquatable<Player>, IDeepCloneable<Player>, IBufferMessage
{
	private static readonly MessageParser<Player> _parser = new MessageParser<Player>(() => new Player());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private long id_;

	public const int NickFieldNumber = 2;

	private string nick_ = "";

	public const int WaitOffLineFieldNumber = 3;

	private bool waitOffLine_;

	public const int OffLineFieldNumber = 4;

	private bool offLine_;

	public const int RoomIdFieldNumber = 5;

	private long roomId_;

	public const int SlotFieldNumber = 6;

	private int slot_;

	public const int ChangeSlotFieldNumber = 7;

	private int changeSlot_;

	public const int ProgressFieldNumber = 8;

	private int progress_;

	public const int TokenFieldNumber = 9;

	private string token_ = "";

	public const int HeroFieldNumber = 10;

	private Hero hero_;

	public const int BagItemsFieldNumber = 11;

	private static readonly FieldCodec<ItemEtc> _repeated_bagItems_codec = FieldCodec.ForMessage(90u, ItemEtc.Parser);

	private readonly RepeatedField<ItemEtc> bagItems_ = new RepeatedField<ItemEtc>();

	public const int ShopInfoFieldNumber = 12;

	private PlayerShopInfo shopInfo_;

	public const int NextOverDayTimeFieldNumber = 13;

	private long nextOverDayTime_;

	public const int FashionPlanFieldNumber = 14;

	private static readonly FieldCodec<FashionPlan> _repeated_fashionPlan_codec = FieldCodec.ForMessage(114u, party.model.FashionPlan.Parser);

	private readonly RepeatedField<FashionPlan> fashionPlan_ = new RepeatedField<FashionPlan>();

	public const int UsePlanFieldNumber = 15;

	private int usePlan_;

	public const int GachaCountFieldNumber = 16;

	private static readonly MapField<int, GachaCount>.Codec _map_gachaCount_codec = new MapField<int, GachaCount>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.GachaCount.Parser), 130u);

	private readonly MapField<int, GachaCount> gachaCount_ = new MapField<int, GachaCount>();

	public const int NextOverMonthTimeFieldNumber = 17;

	private long nextOverMonthTime_;

	public const int NextOverWeekTimeFieldNumber = 18;

	private long nextOverWeekTime_;

	public const int RoleCardFieldNumber = 19;

	private static readonly MapField<int, RoleCard>.Codec _map_roleCard_codec = new MapField<int, RoleCard>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.RoleCard.Parser), 154u);

	private readonly MapField<int, RoleCard> roleCard_ = new MapField<int, RoleCard>();

	public const int IsBotFieldNumber = 20;

	private bool isBot_;

	public const int TaskFieldNumber = 21;

	private TaskInfo task_;

	public const int IsUpPlayerFieldNumber = 22;

	private bool isUpPlayer_;

	public const int SaveTimeFieldNumber = 23;

	private long saveTime_;

	public const int RoomReadyFieldNumber = 24;

	private bool roomReady_;

	public const int LevelFieldNumber = 25;

	private int level_;

	public const int ExpFieldNumber = 26;

	private int exp_;

	public const int RechargeFieldNumber = 27;

	private static readonly MapField<string, RechargeInfo>.Codec _map_recharge_codec = new MapField<string, RechargeInfo>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForMessage(18u, RechargeInfo.Parser), 218u);

	private readonly MapField<string, RechargeInfo> recharge_ = new MapField<string, RechargeInfo>();

	public const int CreateTimeFieldNumber = 28;

	private long createTime_;

	public const int CdkGiftFieldNumber = 29;

	private static readonly FieldCodec<int> _repeated_cdkGift_codec = FieldCodec.ForSFixed32(234u);

	private readonly RepeatedField<int> cdkGift_ = new RepeatedField<int>();

	public const int MailsFieldNumber = 30;

	private static readonly MapField<int, MailData>.Codec _map_mails_codec = new MapField<int, MailData>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MailData.Parser), 242u);

	private readonly MapField<int, MailData> mails_ = new MapField<int, MailData>();

	public const int MailGenIdFieldNumber = 31;

	private int mailGenId_;

	public const int LastServerAllMailFieldNumber = 32;

	private long lastServerAllMail_;

	public const int ActivityTaskFieldNumber = 34;

	private static readonly FieldCodec<ActivityInfo> _repeated_activityTask_codec = FieldCodec.ForMessage(274u, ActivityInfo.Parser);

	private readonly RepeatedField<ActivityInfo> activityTask_ = new RepeatedField<ActivityInfo>();

	public const int IsDayPlayGameFieldNumber = 35;

	private bool isDayPlayGame_;

	public const int IsDayLoginFieldNumber = 36;

	private bool isDayLogin_;

	public const int ShowPlayerFieldNumber = 37;

	private ShowPlayerInfo showPlayer_;

	public const int GachaRecordsFieldNumber = 38;

	private static readonly FieldCodec<GachaRecordList> _repeated_gachaRecords_codec = FieldCodec.ForMessage(306u, GachaRecordList.Parser);

	private readonly RepeatedField<GachaRecordList> gachaRecords_ = new RepeatedField<GachaRecordList>();

	public const int Day7FieldNumber = 39;

	private static readonly MapField<int, Day7Reward>.Codec _map_day7_codec = new MapField<int, Day7Reward>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, Day7Reward.Parser), 314u);

	private readonly MapField<int, Day7Reward> day7_ = new MapField<int, Day7Reward>();

	public const int CanPraiseMapFieldNumber = 40;

	private static readonly MapField<long, int>.Codec _map_canPraiseMap_codec = new MapField<long, int>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForSFixed32(21u, 0), 322u);

	private readonly MapField<long, int> canPraiseMap_ = new MapField<long, int>();

	public const int MonthlyCardRemDaysFieldNumber = 41;

	private int monthlyCardRemDays_;

	public const int ClientDataFieldNumber = 42;

	private ClientData clientData_;

	public const int FriendsFieldNumber = 43;

	private FriendList friends_;

	public const int OfflineTimeFieldNumber = 44;

	private long offlineTime_;

	public const int RoomInviteFieldNumber = 45;

	private static readonly FieldCodec<FriendInvite> _repeated_roomInvite_codec = FieldCodec.ForMessage(362u, FriendInvite.Parser);

	private readonly RepeatedField<FriendInvite> roomInvite_ = new RepeatedField<FriendInvite>();

	public const int LoginCheckFieldNumber = 46;

	private bool loginCheck_;

	public const int LoginTimeFieldNumber = 47;

	private long loginTime_;

	public const int ScratchCardFieldNumber = 48;

	private static readonly MapField<int, ScratchCardRecord>.Codec _map_scratchCard_codec = new MapField<int, ScratchCardRecord>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ScratchCardRecord.Parser), 386u);

	private readonly MapField<int, ScratchCardRecord> scratchCard_ = new MapField<int, ScratchCardRecord>();

	public const int WatchRoomIdFieldNumber = 49;

	private long watchRoomId_;

	public const int BattlePassFieldNumber = 50;

	private BattlePass battlePass_;

	public const int MsgInfosFieldNumber = 51;

	private static readonly MapField<long, FriendChatMsgInfo>.Codec _map_msgInfos_codec = new MapField<long, FriendChatMsgInfo>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, FriendChatMsgInfo.Parser), 410u);

	private readonly MapField<long, FriendChatMsgInfo> msgInfos_ = new MapField<long, FriendChatMsgInfo>();

	public const int IsDayPlayPVEFieldNumber = 52;

	private bool isDayPlayPVE_;

	public const int WeeklyLimitsFieldNumber = 53;

	private static readonly MapField<int, int>.Codec _map_weeklyLimits_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 426u);

	private readonly MapField<int, int> weeklyLimits_ = new MapField<int, int>();

	public const int MatchingModeFieldNumber = 54;

	private int matchingMode_;

	public const int PunishmentTimeFieldNumber = 55;

	private long punishmentTime_;

	public const int CampaignPassFieldNumber = 56;

	private static readonly MapField<int, bool>.Codec _map_campaignPass_codec = new MapField<int, bool>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForBool(16u, defaultValue: false), 450u);

	private readonly MapField<int, bool> campaignPass_ = new MapField<int, bool>();

	public const int SignInRewardFieldNumber = 57;

	private static readonly MapField<int, SignInReward>.Codec _map_signInReward_codec = new MapField<int, SignInReward>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.SignInReward.Parser), 458u);

	private readonly MapField<int, SignInReward> signInReward_ = new MapField<int, SignInReward>();

	public const int PayAmountInfoFieldNumber = 58;

	private PayAmountInfo payAmountInfo_;

	public const int ServerIdFieldNumber = 59;

	private string serverId_ = "";

	public const int IsDeleteFieldNumber = 60;

	private bool isDelete_;

	public const int LightGiftFieldNumber = 61;

	private static readonly MapField<int, LightGift>.Codec _map_lightGift_codec = new MapField<int, LightGift>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.LightGift.Parser), 490u);

	private readonly MapField<int, LightGift> lightGift_ = new MapField<int, LightGift>();

	public const int InviteInfoFieldNumber = 62;

	private InviteInfo inviteInfo_;

	public const int ChannelTypeFieldNumber = 63;

	private int channelType_;

	public const int PayStarDiscFieldNumber = 64;

	private static readonly MapField<string, int>.Codec _map_payStarDisc_codec = new MapField<string, int>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForSFixed32(21u, 0), 514u);

	private readonly MapField<string, int> payStarDisc_ = new MapField<string, int>();

	public const int RechargeSumFieldNumber = 65;

	private int rechargeSum_;

	public const int WinCountFieldNumber = 66;

	private int winCount_;

	public const int NextChangeNameTimeFieldNumber = 67;

	private long nextChangeNameTime_;

	public const int MissionModFieldNumber = 68;

	private MissionMod missionMod_;

	public const int DeviceIdFieldNumber = 69;

	private string deviceId_ = "";

	public const int CampScoreFieldNumber = 70;

	private static readonly MapField<int, int>.Codec _map_campScore_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 562u);

	private readonly MapField<int, int> campScore_ = new MapField<int, int>();

	public const int UnLockDifficultyFieldNumber = 71;

	private int unLockDifficulty_;

	public const int ActivityTasksFieldNumber = 72;

	private static readonly FieldCodec<TaskDSO> _repeated_activityTasks_codec = FieldCodec.ForMessage(578u, TaskDSO.Parser);

	private readonly RepeatedField<TaskDSO> activityTasks_ = new RepeatedField<TaskDSO>();

	public const int RecoupBagItemsFieldNumber = 73;

	private static readonly FieldCodec<ItemEtc> _repeated_recoupBagItems_codec = FieldCodec.ForMessage(586u, ItemEtc.Parser);

	private readonly RepeatedField<ItemEtc> recoupBagItems_ = new RepeatedField<ItemEtc>();

	public const int IsDayPlayPVPFieldNumber = 74;

	private bool isDayPlayPVP_;

	public const int DailyPraiseCountFieldNumber = 75;

	private int dailyPraiseCount_;

	public const int MapModeCountFieldNumber = 79;

	private static readonly MapField<int, int>.Codec _map_mapModeCount_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 634u);

	private readonly MapField<int, int> mapModeCount_ = new MapField<int, int>();

	public const int WinMapFieldNumber = 80;

	private static readonly MapField<int, int>.Codec _map_winMap_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 642u);

	private readonly MapField<int, int> winMap_ = new MapField<int, int>();

	public const int MatchTeamIdFieldNumber = 81;

	private long matchTeamId_;

	public const int SteamIdFieldNumber = 82;

	private string steamId_ = "";

	public const int MergeFlagFieldNumber = 83;

	private int mergeFlag_;

	public const int PlatFieldNumber = 84;

	private string plat_ = "";

	public const int SingleInfoFieldNumber = 85;

	private SingleInfo singleInfo_;

	public const int ActivityPassFieldNumber = 86;

	private static readonly MapField<int, ActivityPass>.Codec _map_activityPass_codec = new MapField<int, ActivityPass>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.ActivityPass.Parser), 690u);

	private readonly MapField<int, ActivityPass> activityPass_ = new MapField<int, ActivityPass>();

	public const int FriendsCdTimeFieldNumber = 88;

	private long friendsCdTime_;

	public const int NearFriendsCdTimeFieldNumber = 89;

	private long nearFriendsCdTime_;

	public const int FriendBlackCdTimeFieldNumber = 90;

	private long friendBlackCdTime_;

	public const int FriendInviteCdTimeFieldNumber = 91;

	private long friendInviteCdTime_;

	public const int LastLaborDicePointFieldNumber = 93;

	private int lastLaborDicePoint_;

	public const int MapModeWinCountFieldNumber = 94;

	private static readonly MapField<int, int>.Codec _map_mapModeWinCount_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 754u);

	private readonly MapField<int, int> mapModeWinCount_ = new MapField<int, int>();

	public const int OnlineStatusFieldNumber = 95;

	private int onlineStatus_;

	public const int IsDayPlayLuckyStarFieldNumber = 96;

	private bool isDayPlayLuckyStar_;

	public const int IsHarmonyFieldNumber = 97;

	private bool isHarmony_;

	public const int AltArtCardsFieldNumber = 98;

	private static readonly MapField<int, AltArtCardInfo>.Codec _map_altArtCards_codec = new MapField<int, AltArtCardInfo>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, AltArtCardInfo.Parser), 786u);

	private readonly MapField<int, AltArtCardInfo> altArtCards_ = new MapField<int, AltArtCardInfo>();

	public const int CreditInfoFieldNumber = 99;

	private CreditInfo creditInfo_;

	public const int SportsMeetInfoFieldNumber = 100;

	private SportsMeetInfo sportsMeetInfo_;

	public const int ReturnInfoFieldNumber = 101;

	private ReturnInfo returnInfo_;

	public const int UpdateTimeFieldNumber = 102;

	private long updateTime_;

	public const int MuteTimeFieldNumber = 103;

	private long muteTime_;

	public const int FlipCardFieldNumber = 104;

	private static readonly MapField<int, FlipCardActivity>.Codec _map_flipCard_codec = new MapField<int, FlipCardActivity>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FlipCardActivity.Parser), 834u);

	private readonly MapField<int, FlipCardActivity> flipCard_ = new MapField<int, FlipCardActivity>();

	public const int HarmonyTypeFieldNumber = 105;

	private int harmonyType_;

	public const int QuestionInfoFieldNumber = 106;

	private static readonly MapField<int, QuestionModel>.Codec _map_questionInfo_codec = new MapField<int, QuestionModel>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, QuestionModel.Parser), 850u);

	private readonly MapField<int, QuestionModel> questionInfo_ = new MapField<int, QuestionModel>();

	public const int GuildInfoFieldNumber = 201;

	private PlayerGuildInfo guildInfo_;

	public const int GameServerIdFieldNumber = 1001;

	private int gameServerId_;

	public const int RoomServerIdFieldNumber = 1002;

	private int roomServerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Player> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[0];

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
	public string Nick
	{
		get
		{
			return nick_;
		}
		set
		{
			nick_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool WaitOffLine
	{
		get
		{
			return waitOffLine_;
		}
		set
		{
			waitOffLine_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool OffLine
	{
		get
		{
			return offLine_;
		}
		set
		{
			offLine_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long RoomId
	{
		get
		{
			return roomId_;
		}
		set
		{
			roomId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Slot
	{
		get
		{
			return slot_;
		}
		set
		{
			slot_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ChangeSlot
	{
		get
		{
			return changeSlot_;
		}
		set
		{
			changeSlot_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Progress
	{
		get
		{
			return progress_;
		}
		set
		{
			progress_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Token
	{
		get
		{
			return token_;
		}
		set
		{
			token_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Hero Hero
	{
		get
		{
			return hero_;
		}
		set
		{
			hero_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemEtc> BagItems => bagItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopInfo ShopInfo
	{
		get
		{
			return shopInfo_;
		}
		set
		{
			shopInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NextOverDayTime
	{
		get
		{
			return nextOverDayTime_;
		}
		set
		{
			nextOverDayTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FashionPlan> FashionPlan => fashionPlan_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UsePlan
	{
		get
		{
			return usePlan_;
		}
		set
		{
			usePlan_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GachaCount> GachaCount => gachaCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NextOverMonthTime
	{
		get
		{
			return nextOverMonthTime_;
		}
		set
		{
			nextOverMonthTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NextOverWeekTime
	{
		get
		{
			return nextOverWeekTime_;
		}
		set
		{
			nextOverWeekTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RoleCard> RoleCard => roleCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsBot
	{
		get
		{
			return isBot_;
		}
		set
		{
			isBot_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskInfo Task
	{
		get
		{
			return task_;
		}
		set
		{
			task_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsUpPlayer
	{
		get
		{
			return isUpPlayer_;
		}
		set
		{
			isUpPlayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long SaveTime
	{
		get
		{
			return saveTime_;
		}
		set
		{
			saveTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool RoomReady
	{
		get
		{
			return roomReady_;
		}
		set
		{
			roomReady_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Level
	{
		get
		{
			return level_;
		}
		set
		{
			level_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Exp
	{
		get
		{
			return exp_;
		}
		set
		{
			exp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<string, RechargeInfo> Recharge => recharge_;

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
	public RepeatedField<int> CdkGift => cdkGift_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MailData> Mails => mails_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MailGenId
	{
		get
		{
			return mailGenId_;
		}
		set
		{
			mailGenId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastServerAllMail
	{
		get
		{
			return lastServerAllMail_;
		}
		set
		{
			lastServerAllMail_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityInfo> ActivityTask => activityTask_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDayPlayGame
	{
		get
		{
			return isDayPlayGame_;
		}
		set
		{
			isDayPlayGame_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDayLogin
	{
		get
		{
			return isDayLogin_;
		}
		set
		{
			isDayLogin_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerInfo ShowPlayer
	{
		get
		{
			return showPlayer_;
		}
		set
		{
			showPlayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaRecordList> GachaRecords => gachaRecords_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, Day7Reward> Day7 => day7_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, int> CanPraiseMap => canPraiseMap_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MonthlyCardRemDays
	{
		get
		{
			return monthlyCardRemDays_;
		}
		set
		{
			monthlyCardRemDays_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientData ClientData
	{
		get
		{
			return clientData_;
		}
		set
		{
			clientData_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendList Friends
	{
		get
		{
			return friends_;
		}
		set
		{
			friends_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long OfflineTime
	{
		get
		{
			return offlineTime_;
		}
		set
		{
			offlineTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FriendInvite> RoomInvite => roomInvite_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool LoginCheck
	{
		get
		{
			return loginCheck_;
		}
		set
		{
			loginCheck_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LoginTime
	{
		get
		{
			return loginTime_;
		}
		set
		{
			loginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ScratchCardRecord> ScratchCard => scratchCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long WatchRoomId
	{
		get
		{
			return watchRoomId_;
		}
		set
		{
			watchRoomId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePass BattlePass
	{
		get
		{
			return battlePass_;
		}
		set
		{
			battlePass_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, FriendChatMsgInfo> MsgInfos => msgInfos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDayPlayPVE
	{
		get
		{
			return isDayPlayPVE_;
		}
		set
		{
			isDayPlayPVE_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> WeeklyLimits => weeklyLimits_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MatchingMode
	{
		get
		{
			return matchingMode_;
		}
		set
		{
			matchingMode_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PunishmentTime
	{
		get
		{
			return punishmentTime_;
		}
		set
		{
			punishmentTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, bool> CampaignPass => campaignPass_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SignInReward> SignInReward => signInReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayAmountInfo PayAmountInfo
	{
		get
		{
			return payAmountInfo_;
		}
		set
		{
			payAmountInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ServerId
	{
		get
		{
			return serverId_;
		}
		set
		{
			serverId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDelete
	{
		get
		{
			return isDelete_;
		}
		set
		{
			isDelete_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, LightGift> LightGift => lightGift_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public InviteInfo InviteInfo
	{
		get
		{
			return inviteInfo_;
		}
		set
		{
			inviteInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ChannelType
	{
		get
		{
			return channelType_;
		}
		set
		{
			channelType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<string, int> PayStarDisc => payStarDisc_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RechargeSum
	{
		get
		{
			return rechargeSum_;
		}
		set
		{
			rechargeSum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WinCount
	{
		get
		{
			return winCount_;
		}
		set
		{
			winCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NextChangeNameTime
	{
		get
		{
			return nextChangeNameTime_;
		}
		set
		{
			nextChangeNameTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MissionMod MissionMod
	{
		get
		{
			return missionMod_;
		}
		set
		{
			missionMod_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string DeviceId
	{
		get
		{
			return deviceId_;
		}
		set
		{
			deviceId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> CampScore => campScore_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UnLockDifficulty
	{
		get
		{
			return unLockDifficulty_;
		}
		set
		{
			unLockDifficulty_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TaskDSO> ActivityTasks => activityTasks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemEtc> RecoupBagItems => recoupBagItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDayPlayPVP
	{
		get
		{
			return isDayPlayPVP_;
		}
		set
		{
			isDayPlayPVP_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DailyPraiseCount
	{
		get
		{
			return dailyPraiseCount_;
		}
		set
		{
			dailyPraiseCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> MapModeCount => mapModeCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> WinMap => winMap_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long MatchTeamId
	{
		get
		{
			return matchTeamId_;
		}
		set
		{
			matchTeamId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SteamId
	{
		get
		{
			return steamId_;
		}
		set
		{
			steamId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MergeFlag
	{
		get
		{
			return mergeFlag_;
		}
		set
		{
			mergeFlag_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Plat
	{
		get
		{
			return plat_;
		}
		set
		{
			plat_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleInfo SingleInfo
	{
		get
		{
			return singleInfo_;
		}
		set
		{
			singleInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityPass> ActivityPass => activityPass_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long FriendsCdTime
	{
		get
		{
			return friendsCdTime_;
		}
		set
		{
			friendsCdTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NearFriendsCdTime
	{
		get
		{
			return nearFriendsCdTime_;
		}
		set
		{
			nearFriendsCdTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long FriendBlackCdTime
	{
		get
		{
			return friendBlackCdTime_;
		}
		set
		{
			friendBlackCdTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long FriendInviteCdTime
	{
		get
		{
			return friendInviteCdTime_;
		}
		set
		{
			friendInviteCdTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LastLaborDicePoint
	{
		get
		{
			return lastLaborDicePoint_;
		}
		set
		{
			lastLaborDicePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> MapModeWinCount => mapModeWinCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OnlineStatus
	{
		get
		{
			return onlineStatus_;
		}
		set
		{
			onlineStatus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDayPlayLuckyStar
	{
		get
		{
			return isDayPlayLuckyStar_;
		}
		set
		{
			isDayPlayLuckyStar_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsHarmony
	{
		get
		{
			return isHarmony_;
		}
		set
		{
			isHarmony_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, AltArtCardInfo> AltArtCards => altArtCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreditInfo CreditInfo
	{
		get
		{
			return creditInfo_;
		}
		set
		{
			creditInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SportsMeetInfo SportsMeetInfo
	{
		get
		{
			return sportsMeetInfo_;
		}
		set
		{
			sportsMeetInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnInfo ReturnInfo
	{
		get
		{
			return returnInfo_;
		}
		set
		{
			returnInfo_ = value;
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
	public long MuteTime
	{
		get
		{
			return muteTime_;
		}
		set
		{
			muteTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FlipCardActivity> FlipCard => flipCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HarmonyType
	{
		get
		{
			return harmonyType_;
		}
		set
		{
			harmonyType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, QuestionModel> QuestionInfo => questionInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerGuildInfo GuildInfo
	{
		get
		{
			return guildInfo_;
		}
		set
		{
			guildInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GameServerId
	{
		get
		{
			return gameServerId_;
		}
		set
		{
			gameServerId_ = value;
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
	public Player()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Player(Player other)
		: this()
	{
		id_ = other.id_;
		nick_ = other.nick_;
		waitOffLine_ = other.waitOffLine_;
		offLine_ = other.offLine_;
		roomId_ = other.roomId_;
		slot_ = other.slot_;
		changeSlot_ = other.changeSlot_;
		progress_ = other.progress_;
		token_ = other.token_;
		hero_ = ((other.hero_ != null) ? other.hero_.Clone() : null);
		bagItems_ = other.bagItems_.Clone();
		shopInfo_ = ((other.shopInfo_ != null) ? other.shopInfo_.Clone() : null);
		nextOverDayTime_ = other.nextOverDayTime_;
		fashionPlan_ = other.fashionPlan_.Clone();
		usePlan_ = other.usePlan_;
		gachaCount_ = other.gachaCount_.Clone();
		nextOverMonthTime_ = other.nextOverMonthTime_;
		nextOverWeekTime_ = other.nextOverWeekTime_;
		roleCard_ = other.roleCard_.Clone();
		isBot_ = other.isBot_;
		task_ = ((other.task_ != null) ? other.task_.Clone() : null);
		isUpPlayer_ = other.isUpPlayer_;
		saveTime_ = other.saveTime_;
		roomReady_ = other.roomReady_;
		level_ = other.level_;
		exp_ = other.exp_;
		recharge_ = other.recharge_.Clone();
		createTime_ = other.createTime_;
		cdkGift_ = other.cdkGift_.Clone();
		mails_ = other.mails_.Clone();
		mailGenId_ = other.mailGenId_;
		lastServerAllMail_ = other.lastServerAllMail_;
		activityTask_ = other.activityTask_.Clone();
		isDayPlayGame_ = other.isDayPlayGame_;
		isDayLogin_ = other.isDayLogin_;
		showPlayer_ = ((other.showPlayer_ != null) ? other.showPlayer_.Clone() : null);
		gachaRecords_ = other.gachaRecords_.Clone();
		day7_ = other.day7_.Clone();
		canPraiseMap_ = other.canPraiseMap_.Clone();
		monthlyCardRemDays_ = other.monthlyCardRemDays_;
		clientData_ = ((other.clientData_ != null) ? other.clientData_.Clone() : null);
		friends_ = ((other.friends_ != null) ? other.friends_.Clone() : null);
		offlineTime_ = other.offlineTime_;
		roomInvite_ = other.roomInvite_.Clone();
		loginCheck_ = other.loginCheck_;
		loginTime_ = other.loginTime_;
		scratchCard_ = other.scratchCard_.Clone();
		watchRoomId_ = other.watchRoomId_;
		battlePass_ = ((other.battlePass_ != null) ? other.battlePass_.Clone() : null);
		msgInfos_ = other.msgInfos_.Clone();
		isDayPlayPVE_ = other.isDayPlayPVE_;
		weeklyLimits_ = other.weeklyLimits_.Clone();
		matchingMode_ = other.matchingMode_;
		punishmentTime_ = other.punishmentTime_;
		campaignPass_ = other.campaignPass_.Clone();
		signInReward_ = other.signInReward_.Clone();
		payAmountInfo_ = ((other.payAmountInfo_ != null) ? other.payAmountInfo_.Clone() : null);
		serverId_ = other.serverId_;
		isDelete_ = other.isDelete_;
		lightGift_ = other.lightGift_.Clone();
		inviteInfo_ = ((other.inviteInfo_ != null) ? other.inviteInfo_.Clone() : null);
		channelType_ = other.channelType_;
		payStarDisc_ = other.payStarDisc_.Clone();
		rechargeSum_ = other.rechargeSum_;
		winCount_ = other.winCount_;
		nextChangeNameTime_ = other.nextChangeNameTime_;
		missionMod_ = ((other.missionMod_ != null) ? other.missionMod_.Clone() : null);
		deviceId_ = other.deviceId_;
		campScore_ = other.campScore_.Clone();
		unLockDifficulty_ = other.unLockDifficulty_;
		activityTasks_ = other.activityTasks_.Clone();
		recoupBagItems_ = other.recoupBagItems_.Clone();
		isDayPlayPVP_ = other.isDayPlayPVP_;
		dailyPraiseCount_ = other.dailyPraiseCount_;
		mapModeCount_ = other.mapModeCount_.Clone();
		winMap_ = other.winMap_.Clone();
		matchTeamId_ = other.matchTeamId_;
		steamId_ = other.steamId_;
		mergeFlag_ = other.mergeFlag_;
		plat_ = other.plat_;
		singleInfo_ = ((other.singleInfo_ != null) ? other.singleInfo_.Clone() : null);
		activityPass_ = other.activityPass_.Clone();
		friendsCdTime_ = other.friendsCdTime_;
		nearFriendsCdTime_ = other.nearFriendsCdTime_;
		friendBlackCdTime_ = other.friendBlackCdTime_;
		friendInviteCdTime_ = other.friendInviteCdTime_;
		lastLaborDicePoint_ = other.lastLaborDicePoint_;
		mapModeWinCount_ = other.mapModeWinCount_.Clone();
		onlineStatus_ = other.onlineStatus_;
		isDayPlayLuckyStar_ = other.isDayPlayLuckyStar_;
		isHarmony_ = other.isHarmony_;
		altArtCards_ = other.altArtCards_.Clone();
		creditInfo_ = ((other.creditInfo_ != null) ? other.creditInfo_.Clone() : null);
		sportsMeetInfo_ = ((other.sportsMeetInfo_ != null) ? other.sportsMeetInfo_.Clone() : null);
		returnInfo_ = ((other.returnInfo_ != null) ? other.returnInfo_.Clone() : null);
		updateTime_ = other.updateTime_;
		muteTime_ = other.muteTime_;
		flipCard_ = other.flipCard_.Clone();
		harmonyType_ = other.harmonyType_;
		questionInfo_ = other.questionInfo_.Clone();
		guildInfo_ = ((other.guildInfo_ != null) ? other.guildInfo_.Clone() : null);
		gameServerId_ = other.gameServerId_;
		roomServerId_ = other.roomServerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Player Clone()
	{
		return new Player(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Player);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Player other)
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
		if (Nick != other.Nick)
		{
			return false;
		}
		if (WaitOffLine != other.WaitOffLine)
		{
			return false;
		}
		if (OffLine != other.OffLine)
		{
			return false;
		}
		if (RoomId != other.RoomId)
		{
			return false;
		}
		if (Slot != other.Slot)
		{
			return false;
		}
		if (ChangeSlot != other.ChangeSlot)
		{
			return false;
		}
		if (Progress != other.Progress)
		{
			return false;
		}
		if (Token != other.Token)
		{
			return false;
		}
		if (!object.Equals(Hero, other.Hero))
		{
			return false;
		}
		if (!bagItems_.Equals(other.bagItems_))
		{
			return false;
		}
		if (!object.Equals(ShopInfo, other.ShopInfo))
		{
			return false;
		}
		if (NextOverDayTime != other.NextOverDayTime)
		{
			return false;
		}
		if (!fashionPlan_.Equals(other.fashionPlan_))
		{
			return false;
		}
		if (UsePlan != other.UsePlan)
		{
			return false;
		}
		if (!GachaCount.Equals(other.GachaCount))
		{
			return false;
		}
		if (NextOverMonthTime != other.NextOverMonthTime)
		{
			return false;
		}
		if (NextOverWeekTime != other.NextOverWeekTime)
		{
			return false;
		}
		if (!RoleCard.Equals(other.RoleCard))
		{
			return false;
		}
		if (IsBot != other.IsBot)
		{
			return false;
		}
		if (!object.Equals(Task, other.Task))
		{
			return false;
		}
		if (IsUpPlayer != other.IsUpPlayer)
		{
			return false;
		}
		if (SaveTime != other.SaveTime)
		{
			return false;
		}
		if (RoomReady != other.RoomReady)
		{
			return false;
		}
		if (Level != other.Level)
		{
			return false;
		}
		if (Exp != other.Exp)
		{
			return false;
		}
		if (!Recharge.Equals(other.Recharge))
		{
			return false;
		}
		if (CreateTime != other.CreateTime)
		{
			return false;
		}
		if (!cdkGift_.Equals(other.cdkGift_))
		{
			return false;
		}
		if (!Mails.Equals(other.Mails))
		{
			return false;
		}
		if (MailGenId != other.MailGenId)
		{
			return false;
		}
		if (LastServerAllMail != other.LastServerAllMail)
		{
			return false;
		}
		if (!activityTask_.Equals(other.activityTask_))
		{
			return false;
		}
		if (IsDayPlayGame != other.IsDayPlayGame)
		{
			return false;
		}
		if (IsDayLogin != other.IsDayLogin)
		{
			return false;
		}
		if (!object.Equals(ShowPlayer, other.ShowPlayer))
		{
			return false;
		}
		if (!gachaRecords_.Equals(other.gachaRecords_))
		{
			return false;
		}
		if (!Day7.Equals(other.Day7))
		{
			return false;
		}
		if (!CanPraiseMap.Equals(other.CanPraiseMap))
		{
			return false;
		}
		if (MonthlyCardRemDays != other.MonthlyCardRemDays)
		{
			return false;
		}
		if (!object.Equals(ClientData, other.ClientData))
		{
			return false;
		}
		if (!object.Equals(Friends, other.Friends))
		{
			return false;
		}
		if (OfflineTime != other.OfflineTime)
		{
			return false;
		}
		if (!roomInvite_.Equals(other.roomInvite_))
		{
			return false;
		}
		if (LoginCheck != other.LoginCheck)
		{
			return false;
		}
		if (LoginTime != other.LoginTime)
		{
			return false;
		}
		if (!ScratchCard.Equals(other.ScratchCard))
		{
			return false;
		}
		if (WatchRoomId != other.WatchRoomId)
		{
			return false;
		}
		if (!object.Equals(BattlePass, other.BattlePass))
		{
			return false;
		}
		if (!MsgInfos.Equals(other.MsgInfos))
		{
			return false;
		}
		if (IsDayPlayPVE != other.IsDayPlayPVE)
		{
			return false;
		}
		if (!WeeklyLimits.Equals(other.WeeklyLimits))
		{
			return false;
		}
		if (MatchingMode != other.MatchingMode)
		{
			return false;
		}
		if (PunishmentTime != other.PunishmentTime)
		{
			return false;
		}
		if (!CampaignPass.Equals(other.CampaignPass))
		{
			return false;
		}
		if (!SignInReward.Equals(other.SignInReward))
		{
			return false;
		}
		if (!object.Equals(PayAmountInfo, other.PayAmountInfo))
		{
			return false;
		}
		if (ServerId != other.ServerId)
		{
			return false;
		}
		if (IsDelete != other.IsDelete)
		{
			return false;
		}
		if (!LightGift.Equals(other.LightGift))
		{
			return false;
		}
		if (!object.Equals(InviteInfo, other.InviteInfo))
		{
			return false;
		}
		if (ChannelType != other.ChannelType)
		{
			return false;
		}
		if (!PayStarDisc.Equals(other.PayStarDisc))
		{
			return false;
		}
		if (RechargeSum != other.RechargeSum)
		{
			return false;
		}
		if (WinCount != other.WinCount)
		{
			return false;
		}
		if (NextChangeNameTime != other.NextChangeNameTime)
		{
			return false;
		}
		if (!object.Equals(MissionMod, other.MissionMod))
		{
			return false;
		}
		if (DeviceId != other.DeviceId)
		{
			return false;
		}
		if (!CampScore.Equals(other.CampScore))
		{
			return false;
		}
		if (UnLockDifficulty != other.UnLockDifficulty)
		{
			return false;
		}
		if (!activityTasks_.Equals(other.activityTasks_))
		{
			return false;
		}
		if (!recoupBagItems_.Equals(other.recoupBagItems_))
		{
			return false;
		}
		if (IsDayPlayPVP != other.IsDayPlayPVP)
		{
			return false;
		}
		if (DailyPraiseCount != other.DailyPraiseCount)
		{
			return false;
		}
		if (!MapModeCount.Equals(other.MapModeCount))
		{
			return false;
		}
		if (!WinMap.Equals(other.WinMap))
		{
			return false;
		}
		if (MatchTeamId != other.MatchTeamId)
		{
			return false;
		}
		if (SteamId != other.SteamId)
		{
			return false;
		}
		if (MergeFlag != other.MergeFlag)
		{
			return false;
		}
		if (Plat != other.Plat)
		{
			return false;
		}
		if (!object.Equals(SingleInfo, other.SingleInfo))
		{
			return false;
		}
		if (!ActivityPass.Equals(other.ActivityPass))
		{
			return false;
		}
		if (FriendsCdTime != other.FriendsCdTime)
		{
			return false;
		}
		if (NearFriendsCdTime != other.NearFriendsCdTime)
		{
			return false;
		}
		if (FriendBlackCdTime != other.FriendBlackCdTime)
		{
			return false;
		}
		if (FriendInviteCdTime != other.FriendInviteCdTime)
		{
			return false;
		}
		if (LastLaborDicePoint != other.LastLaborDicePoint)
		{
			return false;
		}
		if (!MapModeWinCount.Equals(other.MapModeWinCount))
		{
			return false;
		}
		if (OnlineStatus != other.OnlineStatus)
		{
			return false;
		}
		if (IsDayPlayLuckyStar != other.IsDayPlayLuckyStar)
		{
			return false;
		}
		if (IsHarmony != other.IsHarmony)
		{
			return false;
		}
		if (!AltArtCards.Equals(other.AltArtCards))
		{
			return false;
		}
		if (!object.Equals(CreditInfo, other.CreditInfo))
		{
			return false;
		}
		if (!object.Equals(SportsMeetInfo, other.SportsMeetInfo))
		{
			return false;
		}
		if (!object.Equals(ReturnInfo, other.ReturnInfo))
		{
			return false;
		}
		if (UpdateTime != other.UpdateTime)
		{
			return false;
		}
		if (MuteTime != other.MuteTime)
		{
			return false;
		}
		if (!FlipCard.Equals(other.FlipCard))
		{
			return false;
		}
		if (HarmonyType != other.HarmonyType)
		{
			return false;
		}
		if (!QuestionInfo.Equals(other.QuestionInfo))
		{
			return false;
		}
		if (!object.Equals(GuildInfo, other.GuildInfo))
		{
			return false;
		}
		if (GameServerId != other.GameServerId)
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
		if (Nick.Length != 0)
		{
			num ^= Nick.GetHashCode();
		}
		if (WaitOffLine)
		{
			num ^= WaitOffLine.GetHashCode();
		}
		if (OffLine)
		{
			num ^= OffLine.GetHashCode();
		}
		if (RoomId != 0L)
		{
			num ^= RoomId.GetHashCode();
		}
		if (Slot != 0)
		{
			num ^= Slot.GetHashCode();
		}
		if (ChangeSlot != 0)
		{
			num ^= ChangeSlot.GetHashCode();
		}
		if (Progress != 0)
		{
			num ^= Progress.GetHashCode();
		}
		if (Token.Length != 0)
		{
			num ^= Token.GetHashCode();
		}
		if (hero_ != null)
		{
			num ^= Hero.GetHashCode();
		}
		num ^= bagItems_.GetHashCode();
		if (shopInfo_ != null)
		{
			num ^= ShopInfo.GetHashCode();
		}
		if (NextOverDayTime != 0L)
		{
			num ^= NextOverDayTime.GetHashCode();
		}
		num ^= fashionPlan_.GetHashCode();
		if (UsePlan != 0)
		{
			num ^= UsePlan.GetHashCode();
		}
		num ^= GachaCount.GetHashCode();
		if (NextOverMonthTime != 0L)
		{
			num ^= NextOverMonthTime.GetHashCode();
		}
		if (NextOverWeekTime != 0L)
		{
			num ^= NextOverWeekTime.GetHashCode();
		}
		num ^= RoleCard.GetHashCode();
		if (IsBot)
		{
			num ^= IsBot.GetHashCode();
		}
		if (task_ != null)
		{
			num ^= Task.GetHashCode();
		}
		if (IsUpPlayer)
		{
			num ^= IsUpPlayer.GetHashCode();
		}
		if (SaveTime != 0L)
		{
			num ^= SaveTime.GetHashCode();
		}
		if (RoomReady)
		{
			num ^= RoomReady.GetHashCode();
		}
		if (Level != 0)
		{
			num ^= Level.GetHashCode();
		}
		if (Exp != 0)
		{
			num ^= Exp.GetHashCode();
		}
		num ^= Recharge.GetHashCode();
		if (CreateTime != 0L)
		{
			num ^= CreateTime.GetHashCode();
		}
		num ^= cdkGift_.GetHashCode();
		num ^= Mails.GetHashCode();
		if (MailGenId != 0)
		{
			num ^= MailGenId.GetHashCode();
		}
		if (LastServerAllMail != 0L)
		{
			num ^= LastServerAllMail.GetHashCode();
		}
		num ^= activityTask_.GetHashCode();
		if (IsDayPlayGame)
		{
			num ^= IsDayPlayGame.GetHashCode();
		}
		if (IsDayLogin)
		{
			num ^= IsDayLogin.GetHashCode();
		}
		if (showPlayer_ != null)
		{
			num ^= ShowPlayer.GetHashCode();
		}
		num ^= gachaRecords_.GetHashCode();
		num ^= Day7.GetHashCode();
		num ^= CanPraiseMap.GetHashCode();
		if (MonthlyCardRemDays != 0)
		{
			num ^= MonthlyCardRemDays.GetHashCode();
		}
		if (clientData_ != null)
		{
			num ^= ClientData.GetHashCode();
		}
		if (friends_ != null)
		{
			num ^= Friends.GetHashCode();
		}
		if (OfflineTime != 0L)
		{
			num ^= OfflineTime.GetHashCode();
		}
		num ^= roomInvite_.GetHashCode();
		if (LoginCheck)
		{
			num ^= LoginCheck.GetHashCode();
		}
		if (LoginTime != 0L)
		{
			num ^= LoginTime.GetHashCode();
		}
		num ^= ScratchCard.GetHashCode();
		if (WatchRoomId != 0L)
		{
			num ^= WatchRoomId.GetHashCode();
		}
		if (battlePass_ != null)
		{
			num ^= BattlePass.GetHashCode();
		}
		num ^= MsgInfos.GetHashCode();
		if (IsDayPlayPVE)
		{
			num ^= IsDayPlayPVE.GetHashCode();
		}
		num ^= WeeklyLimits.GetHashCode();
		if (MatchingMode != 0)
		{
			num ^= MatchingMode.GetHashCode();
		}
		if (PunishmentTime != 0L)
		{
			num ^= PunishmentTime.GetHashCode();
		}
		num ^= CampaignPass.GetHashCode();
		num ^= SignInReward.GetHashCode();
		if (payAmountInfo_ != null)
		{
			num ^= PayAmountInfo.GetHashCode();
		}
		if (ServerId.Length != 0)
		{
			num ^= ServerId.GetHashCode();
		}
		if (IsDelete)
		{
			num ^= IsDelete.GetHashCode();
		}
		num ^= LightGift.GetHashCode();
		if (inviteInfo_ != null)
		{
			num ^= InviteInfo.GetHashCode();
		}
		if (ChannelType != 0)
		{
			num ^= ChannelType.GetHashCode();
		}
		num ^= PayStarDisc.GetHashCode();
		if (RechargeSum != 0)
		{
			num ^= RechargeSum.GetHashCode();
		}
		if (WinCount != 0)
		{
			num ^= WinCount.GetHashCode();
		}
		if (NextChangeNameTime != 0L)
		{
			num ^= NextChangeNameTime.GetHashCode();
		}
		if (missionMod_ != null)
		{
			num ^= MissionMod.GetHashCode();
		}
		if (DeviceId.Length != 0)
		{
			num ^= DeviceId.GetHashCode();
		}
		num ^= CampScore.GetHashCode();
		if (UnLockDifficulty != 0)
		{
			num ^= UnLockDifficulty.GetHashCode();
		}
		num ^= activityTasks_.GetHashCode();
		num ^= recoupBagItems_.GetHashCode();
		if (IsDayPlayPVP)
		{
			num ^= IsDayPlayPVP.GetHashCode();
		}
		if (DailyPraiseCount != 0)
		{
			num ^= DailyPraiseCount.GetHashCode();
		}
		num ^= MapModeCount.GetHashCode();
		num ^= WinMap.GetHashCode();
		if (MatchTeamId != 0L)
		{
			num ^= MatchTeamId.GetHashCode();
		}
		if (SteamId.Length != 0)
		{
			num ^= SteamId.GetHashCode();
		}
		if (MergeFlag != 0)
		{
			num ^= MergeFlag.GetHashCode();
		}
		if (Plat.Length != 0)
		{
			num ^= Plat.GetHashCode();
		}
		if (singleInfo_ != null)
		{
			num ^= SingleInfo.GetHashCode();
		}
		num ^= ActivityPass.GetHashCode();
		if (FriendsCdTime != 0L)
		{
			num ^= FriendsCdTime.GetHashCode();
		}
		if (NearFriendsCdTime != 0L)
		{
			num ^= NearFriendsCdTime.GetHashCode();
		}
		if (FriendBlackCdTime != 0L)
		{
			num ^= FriendBlackCdTime.GetHashCode();
		}
		if (FriendInviteCdTime != 0L)
		{
			num ^= FriendInviteCdTime.GetHashCode();
		}
		if (LastLaborDicePoint != 0)
		{
			num ^= LastLaborDicePoint.GetHashCode();
		}
		num ^= MapModeWinCount.GetHashCode();
		if (OnlineStatus != 0)
		{
			num ^= OnlineStatus.GetHashCode();
		}
		if (IsDayPlayLuckyStar)
		{
			num ^= IsDayPlayLuckyStar.GetHashCode();
		}
		if (IsHarmony)
		{
			num ^= IsHarmony.GetHashCode();
		}
		num ^= AltArtCards.GetHashCode();
		if (creditInfo_ != null)
		{
			num ^= CreditInfo.GetHashCode();
		}
		if (sportsMeetInfo_ != null)
		{
			num ^= SportsMeetInfo.GetHashCode();
		}
		if (returnInfo_ != null)
		{
			num ^= ReturnInfo.GetHashCode();
		}
		if (UpdateTime != 0L)
		{
			num ^= UpdateTime.GetHashCode();
		}
		if (MuteTime != 0L)
		{
			num ^= MuteTime.GetHashCode();
		}
		num ^= FlipCard.GetHashCode();
		if (HarmonyType != 0)
		{
			num ^= HarmonyType.GetHashCode();
		}
		num ^= QuestionInfo.GetHashCode();
		if (guildInfo_ != null)
		{
			num ^= GuildInfo.GetHashCode();
		}
		if (GameServerId != 0)
		{
			num ^= GameServerId.GetHashCode();
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
		if (Nick.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Nick);
		}
		if (WaitOffLine)
		{
			output.WriteRawTag(24);
			output.WriteBool(WaitOffLine);
		}
		if (OffLine)
		{
			output.WriteRawTag(32);
			output.WriteBool(OffLine);
		}
		if (RoomId != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(RoomId);
		}
		if (Slot != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Slot);
		}
		if (ChangeSlot != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(ChangeSlot);
		}
		if (Progress != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Progress);
		}
		if (Token.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(Token);
		}
		if (hero_ != null)
		{
			output.WriteRawTag(82);
			output.WriteMessage(Hero);
		}
		bagItems_.WriteTo(ref output, _repeated_bagItems_codec);
		if (shopInfo_ != null)
		{
			output.WriteRawTag(98);
			output.WriteMessage(ShopInfo);
		}
		if (NextOverDayTime != 0L)
		{
			output.WriteRawTag(105);
			output.WriteSFixed64(NextOverDayTime);
		}
		fashionPlan_.WriteTo(ref output, _repeated_fashionPlan_codec);
		if (UsePlan != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(UsePlan);
		}
		gachaCount_.WriteTo(ref output, _map_gachaCount_codec);
		if (NextOverMonthTime != 0L)
		{
			output.WriteRawTag(137, 1);
			output.WriteSFixed64(NextOverMonthTime);
		}
		if (NextOverWeekTime != 0L)
		{
			output.WriteRawTag(145, 1);
			output.WriteSFixed64(NextOverWeekTime);
		}
		roleCard_.WriteTo(ref output, _map_roleCard_codec);
		if (IsBot)
		{
			output.WriteRawTag(160, 1);
			output.WriteBool(IsBot);
		}
		if (task_ != null)
		{
			output.WriteRawTag(170, 1);
			output.WriteMessage(Task);
		}
		if (IsUpPlayer)
		{
			output.WriteRawTag(176, 1);
			output.WriteBool(IsUpPlayer);
		}
		if (SaveTime != 0L)
		{
			output.WriteRawTag(184, 1);
			output.WriteSInt64(SaveTime);
		}
		if (RoomReady)
		{
			output.WriteRawTag(192, 1);
			output.WriteBool(RoomReady);
		}
		if (Level != 0)
		{
			output.WriteRawTag(205, 1);
			output.WriteSFixed32(Level);
		}
		if (Exp != 0)
		{
			output.WriteRawTag(213, 1);
			output.WriteSFixed32(Exp);
		}
		recharge_.WriteTo(ref output, _map_recharge_codec);
		if (CreateTime != 0L)
		{
			output.WriteRawTag(224, 1);
			output.WriteSInt64(CreateTime);
		}
		cdkGift_.WriteTo(ref output, _repeated_cdkGift_codec);
		mails_.WriteTo(ref output, _map_mails_codec);
		if (MailGenId != 0)
		{
			output.WriteRawTag(253, 1);
			output.WriteSFixed32(MailGenId);
		}
		if (LastServerAllMail != 0L)
		{
			output.WriteRawTag(129, 2);
			output.WriteSFixed64(LastServerAllMail);
		}
		activityTask_.WriteTo(ref output, _repeated_activityTask_codec);
		if (IsDayPlayGame)
		{
			output.WriteRawTag(152, 2);
			output.WriteBool(IsDayPlayGame);
		}
		if (IsDayLogin)
		{
			output.WriteRawTag(160, 2);
			output.WriteBool(IsDayLogin);
		}
		if (showPlayer_ != null)
		{
			output.WriteRawTag(170, 2);
			output.WriteMessage(ShowPlayer);
		}
		gachaRecords_.WriteTo(ref output, _repeated_gachaRecords_codec);
		day7_.WriteTo(ref output, _map_day7_codec);
		canPraiseMap_.WriteTo(ref output, _map_canPraiseMap_codec);
		if (MonthlyCardRemDays != 0)
		{
			output.WriteRawTag(205, 2);
			output.WriteSFixed32(MonthlyCardRemDays);
		}
		if (clientData_ != null)
		{
			output.WriteRawTag(210, 2);
			output.WriteMessage(ClientData);
		}
		if (friends_ != null)
		{
			output.WriteRawTag(218, 2);
			output.WriteMessage(Friends);
		}
		if (OfflineTime != 0L)
		{
			output.WriteRawTag(225, 2);
			output.WriteSFixed64(OfflineTime);
		}
		roomInvite_.WriteTo(ref output, _repeated_roomInvite_codec);
		if (LoginCheck)
		{
			output.WriteRawTag(240, 2);
			output.WriteBool(LoginCheck);
		}
		if (LoginTime != 0L)
		{
			output.WriteRawTag(249, 2);
			output.WriteSFixed64(LoginTime);
		}
		scratchCard_.WriteTo(ref output, _map_scratchCard_codec);
		if (WatchRoomId != 0L)
		{
			output.WriteRawTag(137, 3);
			output.WriteSFixed64(WatchRoomId);
		}
		if (battlePass_ != null)
		{
			output.WriteRawTag(146, 3);
			output.WriteMessage(BattlePass);
		}
		msgInfos_.WriteTo(ref output, _map_msgInfos_codec);
		if (IsDayPlayPVE)
		{
			output.WriteRawTag(160, 3);
			output.WriteBool(IsDayPlayPVE);
		}
		weeklyLimits_.WriteTo(ref output, _map_weeklyLimits_codec);
		if (MatchingMode != 0)
		{
			output.WriteRawTag(181, 3);
			output.WriteSFixed32(MatchingMode);
		}
		if (PunishmentTime != 0L)
		{
			output.WriteRawTag(185, 3);
			output.WriteSFixed64(PunishmentTime);
		}
		campaignPass_.WriteTo(ref output, _map_campaignPass_codec);
		signInReward_.WriteTo(ref output, _map_signInReward_codec);
		if (payAmountInfo_ != null)
		{
			output.WriteRawTag(210, 3);
			output.WriteMessage(PayAmountInfo);
		}
		if (ServerId.Length != 0)
		{
			output.WriteRawTag(218, 3);
			output.WriteString(ServerId);
		}
		if (IsDelete)
		{
			output.WriteRawTag(224, 3);
			output.WriteBool(IsDelete);
		}
		lightGift_.WriteTo(ref output, _map_lightGift_codec);
		if (inviteInfo_ != null)
		{
			output.WriteRawTag(242, 3);
			output.WriteMessage(InviteInfo);
		}
		if (ChannelType != 0)
		{
			output.WriteRawTag(253, 3);
			output.WriteSFixed32(ChannelType);
		}
		payStarDisc_.WriteTo(ref output, _map_payStarDisc_codec);
		if (RechargeSum != 0)
		{
			output.WriteRawTag(141, 4);
			output.WriteSFixed32(RechargeSum);
		}
		if (WinCount != 0)
		{
			output.WriteRawTag(149, 4);
			output.WriteSFixed32(WinCount);
		}
		if (NextChangeNameTime != 0L)
		{
			output.WriteRawTag(153, 4);
			output.WriteSFixed64(NextChangeNameTime);
		}
		if (missionMod_ != null)
		{
			output.WriteRawTag(162, 4);
			output.WriteMessage(MissionMod);
		}
		if (DeviceId.Length != 0)
		{
			output.WriteRawTag(170, 4);
			output.WriteString(DeviceId);
		}
		campScore_.WriteTo(ref output, _map_campScore_codec);
		if (UnLockDifficulty != 0)
		{
			output.WriteRawTag(189, 4);
			output.WriteSFixed32(UnLockDifficulty);
		}
		activityTasks_.WriteTo(ref output, _repeated_activityTasks_codec);
		recoupBagItems_.WriteTo(ref output, _repeated_recoupBagItems_codec);
		if (IsDayPlayPVP)
		{
			output.WriteRawTag(208, 4);
			output.WriteBool(IsDayPlayPVP);
		}
		if (DailyPraiseCount != 0)
		{
			output.WriteRawTag(221, 4);
			output.WriteSFixed32(DailyPraiseCount);
		}
		mapModeCount_.WriteTo(ref output, _map_mapModeCount_codec);
		winMap_.WriteTo(ref output, _map_winMap_codec);
		if (MatchTeamId != 0L)
		{
			output.WriteRawTag(137, 5);
			output.WriteSFixed64(MatchTeamId);
		}
		if (SteamId.Length != 0)
		{
			output.WriteRawTag(146, 5);
			output.WriteString(SteamId);
		}
		if (MergeFlag != 0)
		{
			output.WriteRawTag(157, 5);
			output.WriteSFixed32(MergeFlag);
		}
		if (Plat.Length != 0)
		{
			output.WriteRawTag(162, 5);
			output.WriteString(Plat);
		}
		if (singleInfo_ != null)
		{
			output.WriteRawTag(170, 5);
			output.WriteMessage(SingleInfo);
		}
		activityPass_.WriteTo(ref output, _map_activityPass_codec);
		if (FriendsCdTime != 0L)
		{
			output.WriteRawTag(193, 5);
			output.WriteSFixed64(FriendsCdTime);
		}
		if (NearFriendsCdTime != 0L)
		{
			output.WriteRawTag(201, 5);
			output.WriteSFixed64(NearFriendsCdTime);
		}
		if (FriendBlackCdTime != 0L)
		{
			output.WriteRawTag(209, 5);
			output.WriteSFixed64(FriendBlackCdTime);
		}
		if (FriendInviteCdTime != 0L)
		{
			output.WriteRawTag(217, 5);
			output.WriteSFixed64(FriendInviteCdTime);
		}
		if (LastLaborDicePoint != 0)
		{
			output.WriteRawTag(237, 5);
			output.WriteSFixed32(LastLaborDicePoint);
		}
		mapModeWinCount_.WriteTo(ref output, _map_mapModeWinCount_codec);
		if (OnlineStatus != 0)
		{
			output.WriteRawTag(253, 5);
			output.WriteSFixed32(OnlineStatus);
		}
		if (IsDayPlayLuckyStar)
		{
			output.WriteRawTag(128, 6);
			output.WriteBool(IsDayPlayLuckyStar);
		}
		if (IsHarmony)
		{
			output.WriteRawTag(136, 6);
			output.WriteBool(IsHarmony);
		}
		altArtCards_.WriteTo(ref output, _map_altArtCards_codec);
		if (creditInfo_ != null)
		{
			output.WriteRawTag(154, 6);
			output.WriteMessage(CreditInfo);
		}
		if (sportsMeetInfo_ != null)
		{
			output.WriteRawTag(162, 6);
			output.WriteMessage(SportsMeetInfo);
		}
		if (returnInfo_ != null)
		{
			output.WriteRawTag(170, 6);
			output.WriteMessage(ReturnInfo);
		}
		if (UpdateTime != 0L)
		{
			output.WriteRawTag(177, 6);
			output.WriteSFixed64(UpdateTime);
		}
		if (MuteTime != 0L)
		{
			output.WriteRawTag(185, 6);
			output.WriteSFixed64(MuteTime);
		}
		flipCard_.WriteTo(ref output, _map_flipCard_codec);
		if (HarmonyType != 0)
		{
			output.WriteRawTag(205, 6);
			output.WriteSFixed32(HarmonyType);
		}
		questionInfo_.WriteTo(ref output, _map_questionInfo_codec);
		if (guildInfo_ != null)
		{
			output.WriteRawTag(202, 12);
			output.WriteMessage(GuildInfo);
		}
		if (GameServerId != 0)
		{
			output.WriteRawTag(205, 62);
			output.WriteSFixed32(GameServerId);
		}
		if (RoomServerId != 0)
		{
			output.WriteRawTag(213, 62);
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
		if (Nick.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Nick);
		}
		if (WaitOffLine)
		{
			num += 2;
		}
		if (OffLine)
		{
			num += 2;
		}
		if (RoomId != 0L)
		{
			num += 9;
		}
		if (Slot != 0)
		{
			num += 5;
		}
		if (ChangeSlot != 0)
		{
			num += 5;
		}
		if (Progress != 0)
		{
			num += 5;
		}
		if (Token.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Token);
		}
		if (hero_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Hero);
		}
		num += bagItems_.CalculateSize(_repeated_bagItems_codec);
		if (shopInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ShopInfo);
		}
		if (NextOverDayTime != 0L)
		{
			num += 9;
		}
		num += fashionPlan_.CalculateSize(_repeated_fashionPlan_codec);
		if (UsePlan != 0)
		{
			num += 5;
		}
		num += gachaCount_.CalculateSize(_map_gachaCount_codec);
		if (NextOverMonthTime != 0L)
		{
			num += 10;
		}
		if (NextOverWeekTime != 0L)
		{
			num += 10;
		}
		num += roleCard_.CalculateSize(_map_roleCard_codec);
		if (IsBot)
		{
			num += 3;
		}
		if (task_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Task);
		}
		if (IsUpPlayer)
		{
			num += 3;
		}
		if (SaveTime != 0L)
		{
			num += 2 + CodedOutputStream.ComputeSInt64Size(SaveTime);
		}
		if (RoomReady)
		{
			num += 3;
		}
		if (Level != 0)
		{
			num += 6;
		}
		if (Exp != 0)
		{
			num += 6;
		}
		num += recharge_.CalculateSize(_map_recharge_codec);
		if (CreateTime != 0L)
		{
			num += 2 + CodedOutputStream.ComputeSInt64Size(CreateTime);
		}
		num += cdkGift_.CalculateSize(_repeated_cdkGift_codec);
		num += mails_.CalculateSize(_map_mails_codec);
		if (MailGenId != 0)
		{
			num += 6;
		}
		if (LastServerAllMail != 0L)
		{
			num += 10;
		}
		num += activityTask_.CalculateSize(_repeated_activityTask_codec);
		if (IsDayPlayGame)
		{
			num += 3;
		}
		if (IsDayLogin)
		{
			num += 3;
		}
		if (showPlayer_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ShowPlayer);
		}
		num += gachaRecords_.CalculateSize(_repeated_gachaRecords_codec);
		num += day7_.CalculateSize(_map_day7_codec);
		num += canPraiseMap_.CalculateSize(_map_canPraiseMap_codec);
		if (MonthlyCardRemDays != 0)
		{
			num += 6;
		}
		if (clientData_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ClientData);
		}
		if (friends_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Friends);
		}
		if (OfflineTime != 0L)
		{
			num += 10;
		}
		num += roomInvite_.CalculateSize(_repeated_roomInvite_codec);
		if (LoginCheck)
		{
			num += 3;
		}
		if (LoginTime != 0L)
		{
			num += 10;
		}
		num += scratchCard_.CalculateSize(_map_scratchCard_codec);
		if (WatchRoomId != 0L)
		{
			num += 10;
		}
		if (battlePass_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BattlePass);
		}
		num += msgInfos_.CalculateSize(_map_msgInfos_codec);
		if (IsDayPlayPVE)
		{
			num += 3;
		}
		num += weeklyLimits_.CalculateSize(_map_weeklyLimits_codec);
		if (MatchingMode != 0)
		{
			num += 6;
		}
		if (PunishmentTime != 0L)
		{
			num += 10;
		}
		num += campaignPass_.CalculateSize(_map_campaignPass_codec);
		num += signInReward_.CalculateSize(_map_signInReward_codec);
		if (payAmountInfo_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(PayAmountInfo);
		}
		if (ServerId.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(ServerId);
		}
		if (IsDelete)
		{
			num += 3;
		}
		num += lightGift_.CalculateSize(_map_lightGift_codec);
		if (inviteInfo_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(InviteInfo);
		}
		if (ChannelType != 0)
		{
			num += 6;
		}
		num += payStarDisc_.CalculateSize(_map_payStarDisc_codec);
		if (RechargeSum != 0)
		{
			num += 6;
		}
		if (WinCount != 0)
		{
			num += 6;
		}
		if (NextChangeNameTime != 0L)
		{
			num += 10;
		}
		if (missionMod_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MissionMod);
		}
		if (DeviceId.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(DeviceId);
		}
		num += campScore_.CalculateSize(_map_campScore_codec);
		if (UnLockDifficulty != 0)
		{
			num += 6;
		}
		num += activityTasks_.CalculateSize(_repeated_activityTasks_codec);
		num += recoupBagItems_.CalculateSize(_repeated_recoupBagItems_codec);
		if (IsDayPlayPVP)
		{
			num += 3;
		}
		if (DailyPraiseCount != 0)
		{
			num += 6;
		}
		num += mapModeCount_.CalculateSize(_map_mapModeCount_codec);
		num += winMap_.CalculateSize(_map_winMap_codec);
		if (MatchTeamId != 0L)
		{
			num += 10;
		}
		if (SteamId.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(SteamId);
		}
		if (MergeFlag != 0)
		{
			num += 6;
		}
		if (Plat.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Plat);
		}
		if (singleInfo_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SingleInfo);
		}
		num += activityPass_.CalculateSize(_map_activityPass_codec);
		if (FriendsCdTime != 0L)
		{
			num += 10;
		}
		if (NearFriendsCdTime != 0L)
		{
			num += 10;
		}
		if (FriendBlackCdTime != 0L)
		{
			num += 10;
		}
		if (FriendInviteCdTime != 0L)
		{
			num += 10;
		}
		if (LastLaborDicePoint != 0)
		{
			num += 6;
		}
		num += mapModeWinCount_.CalculateSize(_map_mapModeWinCount_codec);
		if (OnlineStatus != 0)
		{
			num += 6;
		}
		if (IsDayPlayLuckyStar)
		{
			num += 3;
		}
		if (IsHarmony)
		{
			num += 3;
		}
		num += altArtCards_.CalculateSize(_map_altArtCards_codec);
		if (creditInfo_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CreditInfo);
		}
		if (sportsMeetInfo_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SportsMeetInfo);
		}
		if (returnInfo_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ReturnInfo);
		}
		if (UpdateTime != 0L)
		{
			num += 10;
		}
		if (MuteTime != 0L)
		{
			num += 10;
		}
		num += flipCard_.CalculateSize(_map_flipCard_codec);
		if (HarmonyType != 0)
		{
			num += 6;
		}
		num += questionInfo_.CalculateSize(_map_questionInfo_codec);
		if (guildInfo_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GuildInfo);
		}
		if (GameServerId != 0)
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
	public void MergeFrom(Player other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0L)
		{
			Id = other.Id;
		}
		if (other.Nick.Length != 0)
		{
			Nick = other.Nick;
		}
		if (other.WaitOffLine)
		{
			WaitOffLine = other.WaitOffLine;
		}
		if (other.OffLine)
		{
			OffLine = other.OffLine;
		}
		if (other.RoomId != 0L)
		{
			RoomId = other.RoomId;
		}
		if (other.Slot != 0)
		{
			Slot = other.Slot;
		}
		if (other.ChangeSlot != 0)
		{
			ChangeSlot = other.ChangeSlot;
		}
		if (other.Progress != 0)
		{
			Progress = other.Progress;
		}
		if (other.Token.Length != 0)
		{
			Token = other.Token;
		}
		if (other.hero_ != null)
		{
			if (hero_ == null)
			{
				Hero = new Hero();
			}
			Hero.MergeFrom(other.Hero);
		}
		bagItems_.Add(other.bagItems_);
		if (other.shopInfo_ != null)
		{
			if (shopInfo_ == null)
			{
				ShopInfo = new PlayerShopInfo();
			}
			ShopInfo.MergeFrom(other.ShopInfo);
		}
		if (other.NextOverDayTime != 0L)
		{
			NextOverDayTime = other.NextOverDayTime;
		}
		fashionPlan_.Add(other.fashionPlan_);
		if (other.UsePlan != 0)
		{
			UsePlan = other.UsePlan;
		}
		gachaCount_.MergeFrom(other.gachaCount_);
		if (other.NextOverMonthTime != 0L)
		{
			NextOverMonthTime = other.NextOverMonthTime;
		}
		if (other.NextOverWeekTime != 0L)
		{
			NextOverWeekTime = other.NextOverWeekTime;
		}
		roleCard_.MergeFrom(other.roleCard_);
		if (other.IsBot)
		{
			IsBot = other.IsBot;
		}
		if (other.task_ != null)
		{
			if (task_ == null)
			{
				Task = new TaskInfo();
			}
			Task.MergeFrom(other.Task);
		}
		if (other.IsUpPlayer)
		{
			IsUpPlayer = other.IsUpPlayer;
		}
		if (other.SaveTime != 0L)
		{
			SaveTime = other.SaveTime;
		}
		if (other.RoomReady)
		{
			RoomReady = other.RoomReady;
		}
		if (other.Level != 0)
		{
			Level = other.Level;
		}
		if (other.Exp != 0)
		{
			Exp = other.Exp;
		}
		recharge_.MergeFrom(other.recharge_);
		if (other.CreateTime != 0L)
		{
			CreateTime = other.CreateTime;
		}
		cdkGift_.Add(other.cdkGift_);
		mails_.MergeFrom(other.mails_);
		if (other.MailGenId != 0)
		{
			MailGenId = other.MailGenId;
		}
		if (other.LastServerAllMail != 0L)
		{
			LastServerAllMail = other.LastServerAllMail;
		}
		activityTask_.Add(other.activityTask_);
		if (other.IsDayPlayGame)
		{
			IsDayPlayGame = other.IsDayPlayGame;
		}
		if (other.IsDayLogin)
		{
			IsDayLogin = other.IsDayLogin;
		}
		if (other.showPlayer_ != null)
		{
			if (showPlayer_ == null)
			{
				ShowPlayer = new ShowPlayerInfo();
			}
			ShowPlayer.MergeFrom(other.ShowPlayer);
		}
		gachaRecords_.Add(other.gachaRecords_);
		day7_.MergeFrom(other.day7_);
		canPraiseMap_.MergeFrom(other.canPraiseMap_);
		if (other.MonthlyCardRemDays != 0)
		{
			MonthlyCardRemDays = other.MonthlyCardRemDays;
		}
		if (other.clientData_ != null)
		{
			if (clientData_ == null)
			{
				ClientData = new ClientData();
			}
			ClientData.MergeFrom(other.ClientData);
		}
		if (other.friends_ != null)
		{
			if (friends_ == null)
			{
				Friends = new FriendList();
			}
			Friends.MergeFrom(other.Friends);
		}
		if (other.OfflineTime != 0L)
		{
			OfflineTime = other.OfflineTime;
		}
		roomInvite_.Add(other.roomInvite_);
		if (other.LoginCheck)
		{
			LoginCheck = other.LoginCheck;
		}
		if (other.LoginTime != 0L)
		{
			LoginTime = other.LoginTime;
		}
		scratchCard_.MergeFrom(other.scratchCard_);
		if (other.WatchRoomId != 0L)
		{
			WatchRoomId = other.WatchRoomId;
		}
		if (other.battlePass_ != null)
		{
			if (battlePass_ == null)
			{
				BattlePass = new BattlePass();
			}
			BattlePass.MergeFrom(other.BattlePass);
		}
		msgInfos_.MergeFrom(other.msgInfos_);
		if (other.IsDayPlayPVE)
		{
			IsDayPlayPVE = other.IsDayPlayPVE;
		}
		weeklyLimits_.MergeFrom(other.weeklyLimits_);
		if (other.MatchingMode != 0)
		{
			MatchingMode = other.MatchingMode;
		}
		if (other.PunishmentTime != 0L)
		{
			PunishmentTime = other.PunishmentTime;
		}
		campaignPass_.MergeFrom(other.campaignPass_);
		signInReward_.MergeFrom(other.signInReward_);
		if (other.payAmountInfo_ != null)
		{
			if (payAmountInfo_ == null)
			{
				PayAmountInfo = new PayAmountInfo();
			}
			PayAmountInfo.MergeFrom(other.PayAmountInfo);
		}
		if (other.ServerId.Length != 0)
		{
			ServerId = other.ServerId;
		}
		if (other.IsDelete)
		{
			IsDelete = other.IsDelete;
		}
		lightGift_.MergeFrom(other.lightGift_);
		if (other.inviteInfo_ != null)
		{
			if (inviteInfo_ == null)
			{
				InviteInfo = new InviteInfo();
			}
			InviteInfo.MergeFrom(other.InviteInfo);
		}
		if (other.ChannelType != 0)
		{
			ChannelType = other.ChannelType;
		}
		payStarDisc_.MergeFrom(other.payStarDisc_);
		if (other.RechargeSum != 0)
		{
			RechargeSum = other.RechargeSum;
		}
		if (other.WinCount != 0)
		{
			WinCount = other.WinCount;
		}
		if (other.NextChangeNameTime != 0L)
		{
			NextChangeNameTime = other.NextChangeNameTime;
		}
		if (other.missionMod_ != null)
		{
			if (missionMod_ == null)
			{
				MissionMod = new MissionMod();
			}
			MissionMod.MergeFrom(other.MissionMod);
		}
		if (other.DeviceId.Length != 0)
		{
			DeviceId = other.DeviceId;
		}
		campScore_.MergeFrom(other.campScore_);
		if (other.UnLockDifficulty != 0)
		{
			UnLockDifficulty = other.UnLockDifficulty;
		}
		activityTasks_.Add(other.activityTasks_);
		recoupBagItems_.Add(other.recoupBagItems_);
		if (other.IsDayPlayPVP)
		{
			IsDayPlayPVP = other.IsDayPlayPVP;
		}
		if (other.DailyPraiseCount != 0)
		{
			DailyPraiseCount = other.DailyPraiseCount;
		}
		mapModeCount_.MergeFrom(other.mapModeCount_);
		winMap_.MergeFrom(other.winMap_);
		if (other.MatchTeamId != 0L)
		{
			MatchTeamId = other.MatchTeamId;
		}
		if (other.SteamId.Length != 0)
		{
			SteamId = other.SteamId;
		}
		if (other.MergeFlag != 0)
		{
			MergeFlag = other.MergeFlag;
		}
		if (other.Plat.Length != 0)
		{
			Plat = other.Plat;
		}
		if (other.singleInfo_ != null)
		{
			if (singleInfo_ == null)
			{
				SingleInfo = new SingleInfo();
			}
			SingleInfo.MergeFrom(other.SingleInfo);
		}
		activityPass_.MergeFrom(other.activityPass_);
		if (other.FriendsCdTime != 0L)
		{
			FriendsCdTime = other.FriendsCdTime;
		}
		if (other.NearFriendsCdTime != 0L)
		{
			NearFriendsCdTime = other.NearFriendsCdTime;
		}
		if (other.FriendBlackCdTime != 0L)
		{
			FriendBlackCdTime = other.FriendBlackCdTime;
		}
		if (other.FriendInviteCdTime != 0L)
		{
			FriendInviteCdTime = other.FriendInviteCdTime;
		}
		if (other.LastLaborDicePoint != 0)
		{
			LastLaborDicePoint = other.LastLaborDicePoint;
		}
		mapModeWinCount_.MergeFrom(other.mapModeWinCount_);
		if (other.OnlineStatus != 0)
		{
			OnlineStatus = other.OnlineStatus;
		}
		if (other.IsDayPlayLuckyStar)
		{
			IsDayPlayLuckyStar = other.IsDayPlayLuckyStar;
		}
		if (other.IsHarmony)
		{
			IsHarmony = other.IsHarmony;
		}
		altArtCards_.MergeFrom(other.altArtCards_);
		if (other.creditInfo_ != null)
		{
			if (creditInfo_ == null)
			{
				CreditInfo = new CreditInfo();
			}
			CreditInfo.MergeFrom(other.CreditInfo);
		}
		if (other.sportsMeetInfo_ != null)
		{
			if (sportsMeetInfo_ == null)
			{
				SportsMeetInfo = new SportsMeetInfo();
			}
			SportsMeetInfo.MergeFrom(other.SportsMeetInfo);
		}
		if (other.returnInfo_ != null)
		{
			if (returnInfo_ == null)
			{
				ReturnInfo = new ReturnInfo();
			}
			ReturnInfo.MergeFrom(other.ReturnInfo);
		}
		if (other.UpdateTime != 0L)
		{
			UpdateTime = other.UpdateTime;
		}
		if (other.MuteTime != 0L)
		{
			MuteTime = other.MuteTime;
		}
		flipCard_.MergeFrom(other.flipCard_);
		if (other.HarmonyType != 0)
		{
			HarmonyType = other.HarmonyType;
		}
		questionInfo_.MergeFrom(other.questionInfo_);
		if (other.guildInfo_ != null)
		{
			if (guildInfo_ == null)
			{
				GuildInfo = new PlayerGuildInfo();
			}
			GuildInfo.MergeFrom(other.GuildInfo);
		}
		if (other.GameServerId != 0)
		{
			GameServerId = other.GameServerId;
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
				Nick = input.ReadString();
				break;
			case 24u:
				WaitOffLine = input.ReadBool();
				break;
			case 32u:
				OffLine = input.ReadBool();
				break;
			case 41u:
				RoomId = input.ReadSFixed64();
				break;
			case 53u:
				Slot = input.ReadSFixed32();
				break;
			case 61u:
				ChangeSlot = input.ReadSFixed32();
				break;
			case 69u:
				Progress = input.ReadSFixed32();
				break;
			case 74u:
				Token = input.ReadString();
				break;
			case 82u:
				if (hero_ == null)
				{
					Hero = new Hero();
				}
				input.ReadMessage(Hero);
				break;
			case 90u:
				bagItems_.AddEntriesFrom(ref input, _repeated_bagItems_codec);
				break;
			case 98u:
				if (shopInfo_ == null)
				{
					ShopInfo = new PlayerShopInfo();
				}
				input.ReadMessage(ShopInfo);
				break;
			case 105u:
				NextOverDayTime = input.ReadSFixed64();
				break;
			case 114u:
				fashionPlan_.AddEntriesFrom(ref input, _repeated_fashionPlan_codec);
				break;
			case 125u:
				UsePlan = input.ReadSFixed32();
				break;
			case 130u:
				gachaCount_.AddEntriesFrom(ref input, _map_gachaCount_codec);
				break;
			case 137u:
				NextOverMonthTime = input.ReadSFixed64();
				break;
			case 145u:
				NextOverWeekTime = input.ReadSFixed64();
				break;
			case 154u:
				roleCard_.AddEntriesFrom(ref input, _map_roleCard_codec);
				break;
			case 160u:
				IsBot = input.ReadBool();
				break;
			case 170u:
				if (task_ == null)
				{
					Task = new TaskInfo();
				}
				input.ReadMessage(Task);
				break;
			case 176u:
				IsUpPlayer = input.ReadBool();
				break;
			case 184u:
				SaveTime = input.ReadSInt64();
				break;
			case 192u:
				RoomReady = input.ReadBool();
				break;
			case 205u:
				Level = input.ReadSFixed32();
				break;
			case 213u:
				Exp = input.ReadSFixed32();
				break;
			case 218u:
				recharge_.AddEntriesFrom(ref input, _map_recharge_codec);
				break;
			case 224u:
				CreateTime = input.ReadSInt64();
				break;
			case 234u:
			case 237u:
				cdkGift_.AddEntriesFrom(ref input, _repeated_cdkGift_codec);
				break;
			case 242u:
				mails_.AddEntriesFrom(ref input, _map_mails_codec);
				break;
			case 253u:
				MailGenId = input.ReadSFixed32();
				break;
			case 257u:
				LastServerAllMail = input.ReadSFixed64();
				break;
			case 274u:
				activityTask_.AddEntriesFrom(ref input, _repeated_activityTask_codec);
				break;
			case 280u:
				IsDayPlayGame = input.ReadBool();
				break;
			case 288u:
				IsDayLogin = input.ReadBool();
				break;
			case 298u:
				if (showPlayer_ == null)
				{
					ShowPlayer = new ShowPlayerInfo();
				}
				input.ReadMessage(ShowPlayer);
				break;
			case 306u:
				gachaRecords_.AddEntriesFrom(ref input, _repeated_gachaRecords_codec);
				break;
			case 314u:
				day7_.AddEntriesFrom(ref input, _map_day7_codec);
				break;
			case 322u:
				canPraiseMap_.AddEntriesFrom(ref input, _map_canPraiseMap_codec);
				break;
			case 333u:
				MonthlyCardRemDays = input.ReadSFixed32();
				break;
			case 338u:
				if (clientData_ == null)
				{
					ClientData = new ClientData();
				}
				input.ReadMessage(ClientData);
				break;
			case 346u:
				if (friends_ == null)
				{
					Friends = new FriendList();
				}
				input.ReadMessage(Friends);
				break;
			case 353u:
				OfflineTime = input.ReadSFixed64();
				break;
			case 362u:
				roomInvite_.AddEntriesFrom(ref input, _repeated_roomInvite_codec);
				break;
			case 368u:
				LoginCheck = input.ReadBool();
				break;
			case 377u:
				LoginTime = input.ReadSFixed64();
				break;
			case 386u:
				scratchCard_.AddEntriesFrom(ref input, _map_scratchCard_codec);
				break;
			case 393u:
				WatchRoomId = input.ReadSFixed64();
				break;
			case 402u:
				if (battlePass_ == null)
				{
					BattlePass = new BattlePass();
				}
				input.ReadMessage(BattlePass);
				break;
			case 410u:
				msgInfos_.AddEntriesFrom(ref input, _map_msgInfos_codec);
				break;
			case 416u:
				IsDayPlayPVE = input.ReadBool();
				break;
			case 426u:
				weeklyLimits_.AddEntriesFrom(ref input, _map_weeklyLimits_codec);
				break;
			case 437u:
				MatchingMode = input.ReadSFixed32();
				break;
			case 441u:
				PunishmentTime = input.ReadSFixed64();
				break;
			case 450u:
				campaignPass_.AddEntriesFrom(ref input, _map_campaignPass_codec);
				break;
			case 458u:
				signInReward_.AddEntriesFrom(ref input, _map_signInReward_codec);
				break;
			case 466u:
				if (payAmountInfo_ == null)
				{
					PayAmountInfo = new PayAmountInfo();
				}
				input.ReadMessage(PayAmountInfo);
				break;
			case 474u:
				ServerId = input.ReadString();
				break;
			case 480u:
				IsDelete = input.ReadBool();
				break;
			case 490u:
				lightGift_.AddEntriesFrom(ref input, _map_lightGift_codec);
				break;
			case 498u:
				if (inviteInfo_ == null)
				{
					InviteInfo = new InviteInfo();
				}
				input.ReadMessage(InviteInfo);
				break;
			case 509u:
				ChannelType = input.ReadSFixed32();
				break;
			case 514u:
				payStarDisc_.AddEntriesFrom(ref input, _map_payStarDisc_codec);
				break;
			case 525u:
				RechargeSum = input.ReadSFixed32();
				break;
			case 533u:
				WinCount = input.ReadSFixed32();
				break;
			case 537u:
				NextChangeNameTime = input.ReadSFixed64();
				break;
			case 546u:
				if (missionMod_ == null)
				{
					MissionMod = new MissionMod();
				}
				input.ReadMessage(MissionMod);
				break;
			case 554u:
				DeviceId = input.ReadString();
				break;
			case 562u:
				campScore_.AddEntriesFrom(ref input, _map_campScore_codec);
				break;
			case 573u:
				UnLockDifficulty = input.ReadSFixed32();
				break;
			case 578u:
				activityTasks_.AddEntriesFrom(ref input, _repeated_activityTasks_codec);
				break;
			case 586u:
				recoupBagItems_.AddEntriesFrom(ref input, _repeated_recoupBagItems_codec);
				break;
			case 592u:
				IsDayPlayPVP = input.ReadBool();
				break;
			case 605u:
				DailyPraiseCount = input.ReadSFixed32();
				break;
			case 634u:
				mapModeCount_.AddEntriesFrom(ref input, _map_mapModeCount_codec);
				break;
			case 642u:
				winMap_.AddEntriesFrom(ref input, _map_winMap_codec);
				break;
			case 649u:
				MatchTeamId = input.ReadSFixed64();
				break;
			case 658u:
				SteamId = input.ReadString();
				break;
			case 669u:
				MergeFlag = input.ReadSFixed32();
				break;
			case 674u:
				Plat = input.ReadString();
				break;
			case 682u:
				if (singleInfo_ == null)
				{
					SingleInfo = new SingleInfo();
				}
				input.ReadMessage(SingleInfo);
				break;
			case 690u:
				activityPass_.AddEntriesFrom(ref input, _map_activityPass_codec);
				break;
			case 705u:
				FriendsCdTime = input.ReadSFixed64();
				break;
			case 713u:
				NearFriendsCdTime = input.ReadSFixed64();
				break;
			case 721u:
				FriendBlackCdTime = input.ReadSFixed64();
				break;
			case 729u:
				FriendInviteCdTime = input.ReadSFixed64();
				break;
			case 749u:
				LastLaborDicePoint = input.ReadSFixed32();
				break;
			case 754u:
				mapModeWinCount_.AddEntriesFrom(ref input, _map_mapModeWinCount_codec);
				break;
			case 765u:
				OnlineStatus = input.ReadSFixed32();
				break;
			case 768u:
				IsDayPlayLuckyStar = input.ReadBool();
				break;
			case 776u:
				IsHarmony = input.ReadBool();
				break;
			case 786u:
				altArtCards_.AddEntriesFrom(ref input, _map_altArtCards_codec);
				break;
			case 794u:
				if (creditInfo_ == null)
				{
					CreditInfo = new CreditInfo();
				}
				input.ReadMessage(CreditInfo);
				break;
			case 802u:
				if (sportsMeetInfo_ == null)
				{
					SportsMeetInfo = new SportsMeetInfo();
				}
				input.ReadMessage(SportsMeetInfo);
				break;
			case 810u:
				if (returnInfo_ == null)
				{
					ReturnInfo = new ReturnInfo();
				}
				input.ReadMessage(ReturnInfo);
				break;
			case 817u:
				UpdateTime = input.ReadSFixed64();
				break;
			case 825u:
				MuteTime = input.ReadSFixed64();
				break;
			case 834u:
				flipCard_.AddEntriesFrom(ref input, _map_flipCard_codec);
				break;
			case 845u:
				HarmonyType = input.ReadSFixed32();
				break;
			case 850u:
				questionInfo_.AddEntriesFrom(ref input, _map_questionInfo_codec);
				break;
			case 1610u:
				if (guildInfo_ == null)
				{
					GuildInfo = new PlayerGuildInfo();
				}
				input.ReadMessage(GuildInfo);
				break;
			case 8013u:
				GameServerId = input.ReadSFixed32();
				break;
			case 8021u:
				RoomServerId = input.ReadSFixed32();
				break;
			}
		}
	}
}
