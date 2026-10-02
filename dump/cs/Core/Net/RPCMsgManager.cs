using System;
using System.Collections.Generic;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Core.Net;

public class RPCMsgManager
{
	private readonly object _lockObj = new object();

	private readonly List<OnPushRPCMsgData> _OnPushRPCMsg = new List<OnPushRPCMsgData>();

	private readonly List<OnPushRPCMsgData> _ReconnectCacheRPCMsg = new List<OnPushRPCMsgData>();

	private OnPushRPCMsgData _SyncRoomRPCMsg;

	private readonly Dictionary<long, RPCAsyncResult> callBackList = new Dictionary<long, RPCAsyncResult>(300);

	private static long sessionId;

	public const int UPSNStartID = 100;

	private static int UPSNGenerater;

	public const int TIMEOUT = 15000;

	public KickS2CRPC KickS2C = new KickS2CRPC();

	public PredictActionS2CRPC PredictActionS2C = new PredictActionS2CRPC();

	public RunningGameS2CRPC RunningGameS2C = new RunningGameS2CRPC();

	public BattleS2CRPC BattleS2C = new BattleS2CRPC();

	public LotteryDrawS2CRPC LotteryDrawS2C = new LotteryDrawS2CRPC();

	public LandBuffsS2CRPC LandBuffsS2C = new LandBuffsS2CRPC();

	public RoundStartS2CRPC RoundStartS2C = new RoundStartS2CRPC();

	public GameFinishS2CRPC GameFinishS2C = new GameFinishS2CRPC();

	public MonsterRefreshS2CRPC MonsterRefreshS2C = new MonsterRefreshS2CRPC();

	public MovePointBuffS2CRPC MovePointBuffS2C = new MovePointBuffS2CRPC();

	public ChangeDirS2CRPC ChangeDirS2C = new ChangeDirS2CRPC();

	public GambleChangeS2CRPC GambleChangeS2C = new GambleChangeS2CRPC();

	public HeroBarBoxChangeS2CRPC HeroBarBoxChangeS2C = new HeroBarBoxChangeS2CRPC();

	public RoomNotifyS2CRPC RoomNotifyS2C = new RoomNotifyS2CRPC();

	public ActionStartNotifyS2CRPC ActionStartNotifyS2C = new ActionStartNotifyS2CRPC();

	public NoGambleNotifyS2CRPC NoGambleNotifyS2C = new NoGambleNotifyS2CRPC();

	public GambleObServeS2CRPC GambleObServeS2C = new GambleObServeS2CRPC();

	public UpdateHeroAttrS2CRPC UpdateHeroAttrS2C = new UpdateHeroAttrS2CRPC();

	public ChangePlayerSlotS2CRPC ChangePlayerSlotS2C = new ChangePlayerSlotS2CRPC();

	public BossSleepS2CRPC BossSleepS2C = new BossSleepS2CRPC();

	public RefMallS2CRPC RefMallS2C = new RefMallS2CRPC();

	public BagItemChangeS2CRPC BagItemChangeS2C = new BagItemChangeS2CRPC();

	public RoleCardChangeS2CRPC RoleCardChangeS2C = new RoleCardChangeS2CRPC();

	public TaskConditionS2CRPC TaskConditionS2C = new TaskConditionS2CRPC();

	public TaskInfoS2CRPC TaskInfoS2C = new TaskInfoS2CRPC();

	public PlayerOnlineS2CRPC PlayerOnlineS2C = new PlayerOnlineS2CRPC();

	public ChangeExpS2CRPC ChangeExpS2C = new ChangeExpS2CRPC();

	public MailAddS2CRPC MailAddS2C = new MailAddS2CRPC();

	public OnlineSyncRoomIdS2CRPC OnlineSyncRoomIdS2C = new OnlineSyncRoomIdS2CRPC();

	public NoticeS2CRPC NoticeS2C = new NoticeS2CRPC();

	public ActivityTaskConditionS2CRPC ActivityTaskConditionS2C = new ActivityTaskConditionS2CRPC();

	public MapEventS2CRPC MapEventS2C = new MapEventS2CRPC();

	public Day7RewardS2CRPC Day7RewardS2C = new Day7RewardS2CRPC();

	public MapEventTrainS2CRPC MapEventTrainS2C = new MapEventTrainS2CRPC();

	public ChangePraiseNumS2CRPC ChangePraiseNumS2C = new ChangePraiseNumS2CRPC();

	public MonthlyCardS2CRPC MonthlyCardS2C = new MonthlyCardS2CRPC();

	public MailDelS2CRPC MailDelS2C = new MailDelS2CRPC();

	public FriendNotifyS2CRPC FriendNotifyS2C = new FriendNotifyS2CRPC();

	public FriendListChangeS2CRPC FriendListChangeS2C = new FriendListChangeS2CRPC();

	public FriendInviteNotifyS2CRPC FriendInviteNotifyS2C = new FriendInviteNotifyS2CRPC();

	public LoopNoticeS2CRPC LoopNoticeS2C = new LoopNoticeS2CRPC();

	public BattlePassLvS2CRPC BattlePassLvS2C = new BattlePassLvS2CRPC();

	public BattlePassTaskInfoS2CRPC BattlePassTaskInfoS2C = new BattlePassTaskInfoS2CRPC();

	public BattlePassUpdateTaskS2CRPC BattlePassUpdateTaskS2C = new BattlePassUpdateTaskS2CRPC();

	public BattlePassBuyS2CRPC BattlePassBuyS2C = new BattlePassBuyS2CRPC();

	public BattlePassInfoS2CRPC BattlePassInfoS2C = new BattlePassInfoS2CRPC();

	public FriendsChatMsgS2CRPC FriendsChatMsgS2C = new FriendsChatMsgS2CRPC();

	public GameProgressChangeS2CRPC GameProgressChangeS2C = new GameProgressChangeS2CRPC();

	public MapMissionNotifyS2CRPC MapMissionNotifyS2C = new MapMissionNotifyS2CRPC();

	public ChangeItemLimitS2CRPC ChangeItemLimitS2C = new ChangeItemLimitS2CRPC();

	public CleanItemLimitS2CRPC CleanItemLimitS2C = new CleanItemLimitS2CRPC();

	public CampaignPassS2CRPC CampaignPassS2C = new CampaignPassS2CRPC();

	public CampaignNotifyS2CRPC CampaignNotifyS2C = new CampaignNotifyS2CRPC();

	public KillMessageS2CRPC KillMessageS2C = new KillMessageS2CRPC();

	public GameScoreChangeS2CRPC GameScoreChangeS2C = new GameScoreChangeS2CRPC();

	public SignInRewardS2CRPC SignInRewardS2C = new SignInRewardS2CRPC();

	public PayResultS2CRPC PayResultS2C = new PayResultS2CRPC();

	public PayInfoChangeS2CRPC PayInfoChangeS2C = new PayInfoChangeS2CRPC();

	public InviteSuccessS2CRPC InviteSuccessS2C = new InviteSuccessS2CRPC();

	public InviteInfoNotifyS2CRPC InviteInfoNotifyS2C = new InviteInfoNotifyS2CRPC();

	public MapStatusChangeS2CRPC MapStatusChangeS2C = new MapStatusChangeS2CRPC();

	public GachaCountS2CRPC GachaCountS2C = new GachaCountS2CRPC();

	public MapIndexChangeS2CRPC MapIndexChangeS2C = new MapIndexChangeS2CRPC();

	public SurrenderPunishS2CRPC SurrenderPunishS2C = new SurrenderPunishS2CRPC();

	public GamePassMapSuccessS2CRPC GamePassMapSuccessS2C = new GamePassMapSuccessS2CRPC();

	public FriendDelNotifyS2CRPC FriendDelNotifyS2C = new FriendDelNotifyS2CRPC();

	public BagExpiredTransformNotifyRPC BagExpiredTransformNotify = new BagExpiredTransformNotifyRPC();

	public MapEventCrabS2CRPC MapEventCrabS2C = new MapEventCrabS2CRPC();

	public PkAfterVoteS2CRPC PkAfterVoteS2C = new PkAfterVoteS2CRPC();

	public PlayerTaskNotifyS2CRPC PlayerTaskNotifyS2C = new PlayerTaskNotifyS2CRPC();

	public UnLockDifficultyS2CRPC UnLockDifficultyS2C = new UnLockDifficultyS2CRPC();

	public HeroSkillMoveEffectS2CRPC HeroSkillMoveEffectS2C = new HeroSkillMoveEffectS2CRPC();

	public TimeOutKickPlayerS2CRPC TimeOutKickPlayerS2C = new TimeOutKickPlayerS2CRPC();

	public SayPhraseNotifyS2CRPC SayPhraseNotifyS2C = new SayPhraseNotifyS2CRPC();

	public MatchTeamInviteNotifyRPC MatchTeamInviteNotify = new MatchTeamInviteNotifyRPC();

	public RefreshMatchTeamStateNotifyRPC RefreshMatchTeamStateNotify = new RefreshMatchTeamStateNotifyRPC();

	public ActivityPassGearChangeS2CRPC ActivityPassGearChangeS2C = new ActivityPassGearChangeS2CRPC();

	public SingleGameScoreChangeS2CRPC SingleGameScoreChangeS2C = new SingleGameScoreChangeS2CRPC();

	public DelayProgressMapEventS2CRPC DelayProgressMapEventS2C = new DelayProgressMapEventS2CRPC();

	public LuckyStarMissionChangeS2CRPC LuckyStarMissionChangeS2C = new LuckyStarMissionChangeS2CRPC();

	public MatchPunishmentS2CRPC MatchPunishmentS2C = new MatchPunishmentS2CRPC();

	public ChallengeDataChangeS2CRPC ChallengeDataChangeS2C = new ChallengeDataChangeS2CRPC();

	public GmUnlockRoleInfoS2CRPC GmUnlockRoleInfoS2C = new GmUnlockRoleInfoS2CRPC();

	public SyncPlayerCreditInfoS2CRPC SyncPlayerCreditInfoS2C = new SyncPlayerCreditInfoS2CRPC();

	public RoomHeroCardChangeS2CRPC RoomHeroCardChangeS2C = new RoomHeroCardChangeS2CRPC();

	public RoomRoundAddTermS2CRPC RoomRoundAddTermS2C = new RoomRoundAddTermS2CRPC();

	public ReturnInfoS2CRPC ReturnInfoS2C = new ReturnInfoS2CRPC();

	public SyncRelicsS2CRPC SyncRelicsS2C = new SyncRelicsS2CRPC();

	public ReplaySnapshotS2CRPC ReplaySnapshotS2C = new ReplaySnapshotS2CRPC();

	public ClueNotifyS2CRPC ClueNotifyS2C = new ClueNotifyS2CRPC();

	public ReplayDieS2CRPC ReplayDieS2C = new ReplayDieS2CRPC();

	public GuildTaskNotifyS2CRPC GuildTaskNotifyS2C = new GuildTaskNotifyS2CRPC();

	public GameRoundChangeS2CRPC GameRoundChangeS2C = new GameRoundChangeS2CRPC();

	public NotifyQuestionS2CRPC NotifyQuestionS2C = new NotifyQuestionS2CRPC();

	public ConnectC2SRPC ConnectC2S = new ConnectC2SRPC();

	public ConnectS2CRPC ConnectS2C = new ConnectS2CRPC();

	public HeartbeatC2SRPC HeartbeatC2S = new HeartbeatC2SRPC();

	public HeartbeatS2CRPC HeartbeatS2C = new HeartbeatS2CRPC();

	public CreateRoomC2SRPC CreateRoomC2S = new CreateRoomC2SRPC();

	public CreateRoomS2CRPC CreateRoomS2C = new CreateRoomS2CRPC();

	public SyncRoomC2SRPC SyncRoomC2S = new SyncRoomC2SRPC();

	public SyncRoomS2CRPC SyncRoomS2C = new SyncRoomS2CRPC();

	public JoinRoomC2SRPC JoinRoomC2S = new JoinRoomC2SRPC();

	public JoinRoomS2CRPC JoinRoomS2C = new JoinRoomS2CRPC();

	public ExitRoomC2SRPC ExitRoomC2S = new ExitRoomC2SRPC();

	public ExitRoomS2CRPC ExitRoomS2C = new ExitRoomS2CRPC();

	public QueryRoomC2SRPC QueryRoomC2S = new QueryRoomC2SRPC();

	public QueryRoomS2CRPC QueryRoomS2C = new QueryRoomS2CRPC();

	public RefreshRoomStateC2SRPC RefreshRoomStateC2S = new RefreshRoomStateC2SRPC();

	public RefreshRoomStateS2CRPC RefreshRoomStateS2C = new RefreshRoomStateS2CRPC();

	public StartGameC2SRPC StartGameC2S = new StartGameC2SRPC();

	public StartGameS2CRPC StartGameS2C = new StartGameS2CRPC();

	public ThrowDiceC2SRPC ThrowDiceC2S = new ThrowDiceC2SRPC();

	public ThrowDiceS2CRPC ThrowDiceS2C = new ThrowDiceS2CRPC();

	public ChangeRoomC2SRPC ChangeRoomC2S = new ChangeRoomC2SRPC();

	public ChangeRoomS2CRPC ChangeRoomS2C = new ChangeRoomS2CRPC();

	public MoveC2SRPC MoveC2S = new MoveC2SRPC();

	public MoveS2CRPC MoveS2C = new MoveS2CRPC();

	public ShopBuyC2SRPC ShopBuyC2S = new ShopBuyC2SRPC();

	public ShopBuyS2CRPC ShopBuyS2C = new ShopBuyS2CRPC();

	public PursuitC2SRPC PursuitC2S = new PursuitC2SRPC();

	public PursuitS2CRPC PursuitS2C = new PursuitS2CRPC();

	public BattleUseCardC2SRPC BattleUseCardC2S = new BattleUseCardC2SRPC();

	public BattleUseCardS2CRPC BattleUseCardS2C = new BattleUseCardS2CRPC();

	public BattleThrowDiceC2SRPC BattleThrowDiceC2S = new BattleThrowDiceC2SRPC();

	public BattleThrowDiceS2CRPC BattleThrowDiceS2C = new BattleThrowDiceS2CRPC();

	public BattleChoiceC2SRPC BattleChoiceC2S = new BattleChoiceC2SRPC();

	public BattleChoiceS2CRPC BattleChoiceS2C = new BattleChoiceS2CRPC();

	public LotteryChoiceC2SRPC LotteryChoiceC2S = new LotteryChoiceC2SRPC();

	public LotteryChoiceS2CRPC LotteryChoiceS2C = new LotteryChoiceS2CRPC();

	public MoveAgainC2SRPC MoveAgainC2S = new MoveAgainC2SRPC();

	public MoveAgainS2CRPC MoveAgainS2C = new MoveAgainS2CRPC();

	public AskBattleC2SRPC AskBattleC2S = new AskBattleC2SRPC();

	public AskBattleS2CRPC AskBattleS2C = new AskBattleS2CRPC();

	public RollGoldC2SRPC RollGoldC2S = new RollGoldC2SRPC();

	public RollGoldS2CRPC RollGoldS2C = new RollGoldS2CRPC();

	public EventThrowDiceC2SRPC EventThrowDiceC2S = new EventThrowDiceC2SRPC();

	public EventThrowDiceS2CRPC EventThrowDiceS2C = new EventThrowDiceS2CRPC();

	public TriggerEventC2SRPC TriggerEventC2S = new TriggerEventC2SRPC();

	public TriggerEventS2CRPC TriggerEventS2C = new TriggerEventS2CRPC();

	public UseEffectCardC2SRPC UseEffectCardC2S = new UseEffectCardC2SRPC();

	public UseEffectCardS2CRPC UseEffectCardS2C = new UseEffectCardS2CRPC();

	public BombThrowDiceC2SRPC BombThrowDiceC2S = new BombThrowDiceC2SRPC();

	public BombThrowDiceS2CRPC BombThrowDiceS2C = new BombThrowDiceS2CRPC();

	public ChoiceDirectionC2SRPC ChoiceDirectionC2S = new ChoiceDirectionC2SRPC();

	public ChoiceDirectionS2CRPC ChoiceDirectionS2C = new ChoiceDirectionS2CRPC();

	public LandChoiceTargetC2SRPC LandChoiceTargetC2S = new LandChoiceTargetC2SRPC();

	public LandChoiceTargetS2CRPC LandChoiceTargetS2C = new LandChoiceTargetS2CRPC();

	public ThrowDiceResultC2SRPC ThrowDiceResultC2S = new ThrowDiceResultC2SRPC();

	public ThrowDiceResultS2CRPC ThrowDiceResultS2C = new ThrowDiceResultS2CRPC();

	public TriggerDivinationC2SRPC TriggerDivinationC2S = new TriggerDivinationC2SRPC();

	public TriggerDivinationS2CRPC TriggerDivinationS2C = new TriggerDivinationS2CRPC();

	public TriggerDestinyC2SRPC TriggerDestinyC2S = new TriggerDestinyC2SRPC();

	public TriggerDestinyS2CRPC TriggerDestinyS2C = new TriggerDestinyS2CRPC();

	public UseQuickCardC2SRPC UseQuickCardC2S = new UseQuickCardC2SRPC();

	public UseQuickCardS2CRPC UseQuickCardS2C = new UseQuickCardS2CRPC();

	public AbandonCardC2SRPC AbandonCardC2S = new AbandonCardC2SRPC();

	public AbandonCardS2CRPC AbandonCardS2C = new AbandonCardS2CRPC();

	public StopOrContinueC2SRPC StopOrContinueC2S = new StopOrContinueC2SRPC();

	public StopOrContinueS2CRPC StopOrContinueS2C = new StopOrContinueS2CRPC();

	public StartGambleC2SRPC StartGambleC2S = new StartGambleC2SRPC();

	public StartGambleS2CRPC StartGambleS2C = new StartGambleS2CRPC();

	public GambleThrowDicC2SRPC GambleThrowDicC2S = new GambleThrowDicC2SRPC();

	public GambleThrowDicS2CRPC GambleThrowDicS2C = new GambleThrowDicS2CRPC();

	public ChoiceHeroC2S2RPC ChoiceHeroC2S2 = new ChoiceHeroC2S2RPC();

	public ChoiceHeroS2C2RPC ChoiceHeroS2C2 = new ChoiceHeroS2C2RPC();

	public AffirmHeroC2SRPC AffirmHeroC2S = new AffirmHeroC2SRPC();

	public AffirmHeroS2CRPC AffirmHeroS2C = new AffirmHeroS2CRPC();

	public SearchRoomC2SRPC SearchRoomC2S = new SearchRoomC2SRPC();

	public SearchRoomS2CRPC SearchRoomS2C = new SearchRoomS2CRPC();

	public GmC2SRPC GmC2S = new GmC2SRPC();

	public GmS2CRPC GmS2C = new GmS2CRPC();

	public TriggerHospitalC2SRPC TriggerHospitalC2S = new TriggerHospitalC2SRPC();

	public TriggerHospitalS2CRPC TriggerHospitalS2C = new TriggerHospitalS2CRPC();

	public SendChatC2SRPC SendChatC2S = new SendChatC2SRPC();

	public SendChatS2CRPC SendChatS2C = new SendChatS2CRPC();

	public PlayerShopBuyC2SRPC PlayerShopBuyC2S = new PlayerShopBuyC2SRPC();

	public PlayerShopBuyS2CRPC PlayerShopBuyS2C = new PlayerShopBuyS2CRPC();

	public PlayerUseItemC2SRPC PlayerUseItemC2S = new PlayerUseItemC2SRPC();

	public PlayerUseItemS2CRPC PlayerUseItemS2C = new PlayerUseItemS2CRPC();

	public UseTreasureC2SRPC UseTreasureC2S = new UseTreasureC2SRPC();

	public UseTreasureS2CRPC UseTreasureS2C = new UseTreasureS2CRPC();

	public UseTreasureAutoTransformC2SRPC UseTreasureAutoTransformC2S = new UseTreasureAutoTransformC2SRPC();

	public UseTreasureAutoTransformS2CRPC UseTreasureAutoTransformS2C = new UseTreasureAutoTransformS2CRPC();

	public SetFashionC2SRPC SetFashionC2S = new SetFashionC2SRPC();

	public SetFashionS2CRPC SetFashionS2C = new SetFashionS2CRPC();

	public SelectFashionPlanC2SRPC SelectFashionPlanC2S = new SelectFashionPlanC2SRPC();

	public SelectFashionPlanS2CRPC SelectFashionPlanS2C = new SelectFashionPlanS2CRPC();

	public GachaC2SRPC GachaC2S = new GachaC2SRPC();

	public GachaS2CRPC GachaS2C = new GachaS2CRPC();

	public SteamSearchRoomC2SRPC SteamSearchRoomC2S = new SteamSearchRoomC2SRPC();

	public SteamSearchRoomS2CRPC SteamSearchRoomS2C = new SteamSearchRoomS2CRPC();

	public CheatItemC2SRPC CheatItemC2S = new CheatItemC2SRPC();

	public CheatItemS2CRPC CheatItemS2C = new CheatItemS2CRPC();

	public RoleCardUpLvC2SRPC RoleCardUpLvC2S = new RoleCardUpLvC2SRPC();

	public RoleCardUpLvS2CRPC RoleCardUpLvS2C = new RoleCardUpLvS2CRPC();

	public RoleCardBreakThroughC2SRPC RoleCardBreakThroughC2S = new RoleCardBreakThroughC2SRPC();

	public RoleCardBreakThroughS2CRPC RoleCardBreakThroughS2C = new RoleCardBreakThroughS2CRPC();

	public RoleCardChoiceResC2SRPC RoleCardChoiceResC2S = new RoleCardChoiceResC2SRPC();

	public RoleCardChoiceResS2CRPC RoleCardChoiceResS2C = new RoleCardChoiceResS2CRPC();

	public TaskRewardC2SRPC TaskRewardC2S = new TaskRewardC2SRPC();

	public TaskRewardS2CRPC TaskRewardS2C = new TaskRewardS2CRPC();

	public TeachingC2SRPC TeachingC2S = new TeachingC2SRPC();

	public TeachingS2CRPC TeachingS2C = new TeachingS2CRPC();

	public QuickJoinRoomC2SRPC QuickJoinRoomC2S = new QuickJoinRoomC2SRPC();

	public QuickJoinRoomS2CRPC QuickJoinRoomS2C = new QuickJoinRoomS2CRPC();

	public RoomKickPlayerC2SRPC RoomKickPlayerC2S = new RoomKickPlayerC2SRPC();

	public RoomKickPlayerS2CRPC RoomKickPlayerS2C = new RoomKickPlayerS2CRPC();

	public RoomAbdicationC2SRPC RoomAbdicationC2S = new RoomAbdicationC2SRPC();

	public RoomAbdicationS2CRPC RoomAbdicationS2C = new RoomAbdicationS2CRPC();

	public RoomReadyC2SRPC RoomReadyC2S = new RoomReadyC2SRPC();

	public RoomReadyS2CRPC RoomReadyS2C = new RoomReadyS2CRPC();

	public ChargeCreateC2SRPC ChargeCreateC2S = new ChargeCreateC2SRPC();

	public ChargeCreateS2CRPC ChargeCreateS2C = new ChargeCreateS2CRPC();

	public ChargeC2SRPC ChargeC2S = new ChargeC2SRPC();

	public ChargeS2CRPC ChargeS2C = new ChargeS2CRPC();

	public GiftCdkC2SRPC GiftCdkC2S = new GiftCdkC2SRPC();

	public GiftCdkS2CRPC GiftCdkS2C = new GiftCdkS2CRPC();

	public MailReadC2SRPC MailReadC2S = new MailReadC2SRPC();

	public MailReadS2CRPC MailReadS2C = new MailReadS2CRPC();

	public MailGetRewardC2SRPC MailGetRewardC2S = new MailGetRewardC2SRPC();

	public MailGetRewardS2CRPC MailGetRewardS2C = new MailGetRewardS2CRPC();

	public MailDelReadC2SRPC MailDelReadC2S = new MailDelReadC2SRPC();

	public MailDelReadS2CRPC MailDelReadS2C = new MailDelReadS2CRPC();

	public GachaRecordC2SRPC GachaRecordC2S = new GachaRecordC2SRPC();

	public GachaRecordS2CRPC GachaRecordS2C = new GachaRecordS2CRPC();

	public ActivityTaskRewardC2SRPC ActivityTaskRewardC2S = new ActivityTaskRewardC2SRPC();

	public ActivityTaskRewardS2CRPC ActivityTaskRewardS2C = new ActivityTaskRewardS2CRPC();

	public RoomShortChatC2SRPC RoomShortChatC2S = new RoomShortChatC2SRPC();

	public RoomShortChatS2CRPC RoomShortChatS2C = new RoomShortChatS2CRPC();

	public SetShowPlayerC2SRPC SetShowPlayerC2S = new SetShowPlayerC2SRPC();

	public SetShowPlayerS2CRPC SetShowPlayerS2C = new SetShowPlayerS2CRPC();

	public GetShowPlayerC2SRPC GetShowPlayerC2S = new GetShowPlayerC2SRPC();

	public GetShowPlayerS2CRPC GetShowPlayerS2C = new GetShowPlayerS2CRPC();

	public GetPlayerFightRecordC2SRPC GetPlayerFightRecordC2S = new GetPlayerFightRecordC2SRPC();

	public GetPlayerFightRecordS2CRPC GetPlayerFightRecordS2C = new GetPlayerFightRecordS2CRPC();

	public GetDay7RewardC2SRPC GetDay7RewardC2S = new GetDay7RewardC2SRPC();

	public GetDay7RewardS2CRPC GetDay7RewardS2C = new GetDay7RewardS2CRPC();

	public PraisePlayerC2SRPC PraisePlayerC2S = new PraisePlayerC2SRPC();

	public PraisePlayerS2CRPC PraisePlayerS2C = new PraisePlayerS2CRPC();

	public ClientDataUploadC2SRPC ClientDataUploadC2S = new ClientDataUploadC2SRPC();

	public ClientDataUploadS2CRPC ClientDataUploadS2C = new ClientDataUploadS2CRPC();

	public FriendListC2SRPC FriendListC2S = new FriendListC2SRPC();

	public FriendListS2CRPC FriendListS2C = new FriendListS2CRPC();

	public FriendApplyC2SRPC FriendApplyC2S = new FriendApplyC2SRPC();

	public FriendApplyS2CRPC FriendApplyS2C = new FriendApplyS2CRPC();

	public FriendApplyListC2SRPC FriendApplyListC2S = new FriendApplyListC2SRPC();

	public FriendApplyListS2CRPC FriendApplyListS2C = new FriendApplyListS2CRPC();

	public FriendApplyOpC2SRPC FriendApplyOpC2S = new FriendApplyOpC2SRPC();

	public FriendApplyOpS2CRPC FriendApplyOpS2C = new FriendApplyOpS2CRPC();

	public FriendOpC2SRPC FriendOpC2S = new FriendOpC2SRPC();

	public FriendOpS2CRPC FriendOpS2C = new FriendOpS2CRPC();

	public FriendInviteC2SRPC FriendInviteC2S = new FriendInviteC2SRPC();

	public FriendInviteS2CRPC FriendInviteS2C = new FriendInviteS2CRPC();

	public FriendInviteListC2SRPC FriendInviteListC2S = new FriendInviteListC2SRPC();

	public FriendInviteListS2CRPC FriendInviteListS2C = new FriendInviteListS2CRPC();

	public FriendInviteCleanC2SRPC FriendInviteCleanC2S = new FriendInviteCleanC2SRPC();

	public FriendInviteCleanS2CRPC FriendInviteCleanS2C = new FriendInviteCleanS2CRPC();

	public FriendBlacksListC2SRPC FriendBlacksListC2S = new FriendBlacksListC2SRPC();

	public FriendBlacksListS2CRPC FriendBlacksListS2C = new FriendBlacksListS2CRPC();

	public NearFightPlayerC2SRPC NearFightPlayerC2S = new NearFightPlayerC2SRPC();

	public NearFightPlayerS2CRPC NearFightPlayerS2C = new NearFightPlayerS2CRPC();

	public SearchPlayerC2SRPC SearchPlayerC2S = new SearchPlayerC2SRPC();

	public SearchPlayerS2CRPC SearchPlayerS2C = new SearchPlayerS2CRPC();

	public ScratchCardC2SRPC ScratchCardC2S = new ScratchCardC2SRPC();

	public ScratchCardS2CRPC ScratchCardS2C = new ScratchCardS2CRPC();

	public NextScratchCardPoolC2SRPC NextScratchCardPoolC2S = new NextScratchCardPoolC2SRPC();

	public NextScratchCardPoolS2CRPC NextScratchCardPoolS2C = new NextScratchCardPoolS2CRPC();

	public WatchJoinRoomC2SRPC WatchJoinRoomC2S = new WatchJoinRoomC2SRPC();

	public WatchJoinRoomS2CRPC WatchJoinRoomS2C = new WatchJoinRoomS2CRPC();

	public WatchRefreshRoomStateC2SRPC WatchRefreshRoomStateC2S = new WatchRefreshRoomStateC2SRPC();

	public WatchRefreshRoomStateS2CRPC WatchRefreshRoomStateS2C = new WatchRefreshRoomStateS2CRPC();

	public WatchExitRoomC2SRPC WatchExitRoomC2S = new WatchExitRoomC2SRPC();

	public WatchExitRoomS2CRPC WatchExitRoomS2C = new WatchExitRoomS2CRPC();

	public BattlePassGetRewardC2SRPC BattlePassGetRewardC2S = new BattlePassGetRewardC2SRPC();

	public BattlePassGetRewardS2CRPC BattlePassGetRewardS2C = new BattlePassGetRewardS2CRPC();

	public BattlePassTaskRewardC2SRPC BattlePassTaskRewardC2S = new BattlePassTaskRewardC2SRPC();

	public BattlePassTaskRewardS2CRPC BattlePassTaskRewardS2C = new BattlePassTaskRewardS2CRPC();

	public BattlePassUpLvC2SRPC BattlePassUpLvC2S = new BattlePassUpLvC2SRPC();

	public BattlePassUpLvS2CRPC BattlePassUpLvS2C = new BattlePassUpLvS2CRPC();

	public FriendSendMsgC2SRPC FriendSendMsgC2S = new FriendSendMsgC2SRPC();

	public FriendSendMsgS2CRPC FriendSendMsgS2C = new FriendSendMsgS2CRPC();

	public GetChatMsgC2SRPC GetChatMsgC2S = new GetChatMsgC2SRPC();

	public GetChatMsgS2CRPC GetChatMsgS2C = new GetChatMsgS2CRPC();

	public ReadChatMsgC2SRPC ReadChatMsgC2S = new ReadChatMsgC2SRPC();

	public ReadChatMsgS2CRPC ReadChatMsgS2C = new ReadChatMsgS2CRPC();

	public DelChatMsgInfoC2SRPC DelChatMsgInfoC2S = new DelChatMsgInfoC2SRPC();

	public DelChatMsgInfoS2CRPC DelChatMsgInfoS2C = new DelChatMsgInfoS2CRPC();

	public SelectRelicC2SRPC SelectRelicC2S = new SelectRelicC2SRPC();

	public SelectRelicS2CRPC SelectRelicS2C = new SelectRelicS2CRPC();

	public MonsterPursuitC2SRPC MonsterPursuitC2S = new MonsterPursuitC2SRPC();

	public MonsterPursuitS2CRPC MonsterPursuitS2C = new MonsterPursuitS2CRPC();

	public PVEShopBuyC2SRPC PVEShopBuyC2S = new PVEShopBuyC2SRPC();

	public PVEShopBuyS2CRPC PVEShopBuyS2C = new PVEShopBuyS2CRPC();

	public ClientCheckTaskC2SRPC ClientCheckTaskC2S = new ClientCheckTaskC2SRPC();

	public ClientCheckTaskS2CRPC ClientCheckTaskS2C = new ClientCheckTaskS2CRPC();

	public PveHeroUpLvC2SRPC PveHeroUpLvC2S = new PveHeroUpLvC2SRPC();

	public PveHeroUpLvS2CRPC PveHeroUpLvS2C = new PveHeroUpLvS2CRPC();

	public StartMatchC2SRPC StartMatchC2S = new StartMatchC2SRPC();

	public StartMatchS2CRPC StartMatchS2C = new StartMatchS2CRPC();

	public CancelMatchC2SRPC CancelMatchC2S = new CancelMatchC2SRPC();

	public CancelMatchS2CRPC CancelMatchS2C = new CancelMatchS2CRPC();

	public MatchSuccessC2SRPC MatchSuccessC2S = new MatchSuccessC2SRPC();

	public MatchSuccessS2CRPC MatchSuccessS2C = new MatchSuccessS2CRPC();

	public AccuseC2SRPC AccuseC2S = new AccuseC2SRPC();

	public AccuseS2CRPC AccuseS2C = new AccuseS2CRPC();

	public SingleCampaignC2SRPC SingleCampaignC2S = new SingleCampaignC2SRPC();

	public SingleCampaignS2CRPC SingleCampaignS2C = new SingleCampaignS2CRPC();

	public DevChargeC2SRPC DevChargeC2S = new DevChargeC2SRPC();

	public DevChargeS2CRPC DevChargeS2C = new DevChargeS2CRPC();

	public AskReviveTeammateC2SRPC AskReviveTeammateC2S = new AskReviveTeammateC2SRPC();

	public AskReviveTeammateS2CRPC AskReviveTeammateS2C = new AskReviveTeammateS2CRPC();

	public GetSignInRewardC2SRPC GetSignInRewardC2S = new GetSignInRewardC2SRPC();

	public GetSignInRewardS2CRPC GetSignInRewardS2C = new GetSignInRewardS2CRPC();

	public ChatMapMarkersC2SRPC ChatMapMarkersC2S = new ChatMapMarkersC2SRPC();

	public ChatMapMarkersS2CRPC ChatMapMarkersS2C = new ChatMapMarkersS2CRPC();

	public AbroadCreateOrderC2SRPC AbroadCreateOrderC2S = new AbroadCreateOrderC2SRPC();

	public AbroadCreateOrderS2CRPC AbroadCreateOrderS2C = new AbroadCreateOrderS2CRPC();

	public AgeVerifyC2SRPC AgeVerifyC2S = new AgeVerifyC2SRPC();

	public AgeVerifyS2CRPC AgeVerifyS2C = new AgeVerifyS2CRPC();

	public ChangeNameC2SRPC ChangeNameC2S = new ChangeNameC2SRPC();

	public ChangeNameS2CRPC ChangeNameS2C = new ChangeNameS2CRPC();

	public ClientClickConfirmTaskC2SRPC ClientClickConfirmTaskC2S = new ClientClickConfirmTaskC2SRPC();

	public ClientClickConfirmTaskS2CRPC ClientClickConfirmTaskS2C = new ClientClickConfirmTaskS2CRPC();

	public BuyRelicC2SRPC BuyRelicC2S = new BuyRelicC2SRPC();

	public BuyRelicS2CRPC BuyRelicS2C = new BuyRelicS2CRPC();

	public BuyLightGiftC2SRPC BuyLightGiftC2S = new BuyLightGiftC2SRPC();

	public BuyLightGiftS2CRPC BuyLightGiftS2C = new BuyLightGiftS2CRPC();

	public LightGiftC2SRPC LightGiftC2S = new LightGiftC2SRPC();

	public LightGiftS2CRPC LightGiftS2C = new LightGiftS2CRPC();

	public AcquisitionC2SRPC AcquisitionC2S = new AcquisitionC2SRPC();

	public AcquisitionS2CRPC AcquisitionS2C = new AcquisitionS2CRPC();

	public AcquisitionRewardC2SRPC AcquisitionRewardC2S = new AcquisitionRewardC2SRPC();

	public AcquisitionRewardS2CRPC AcquisitionRewardS2C = new AcquisitionRewardS2CRPC();

	public SelectMechanismC2SRPC SelectMechanismC2S = new SelectMechanismC2SRPC();

	public SelectMechanismS2CRPC SelectMechanismS2C = new SelectMechanismS2CRPC();

	public GachaCountRewardC2SRPC GachaCountRewardC2S = new GachaCountRewardC2SRPC();

	public GachaCountRewardS2CRPC GachaCountRewardS2C = new GachaCountRewardS2CRPC();

	public GetPlayerSimpleC2SRPC GetPlayerSimpleC2S = new GetPlayerSimpleC2SRPC();

	public GetPlayerSimpleS2CRPC GetPlayerSimpleS2C = new GetPlayerSimpleS2CRPC();

	public RoleCardCollectC2SRPC RoleCardCollectC2S = new RoleCardCollectC2SRPC();

	public RoleCardCollectS2CRPC RoleCardCollectS2C = new RoleCardCollectS2CRPC();

	public GMPlayerSettingC2SRPC GMPlayerSettingC2S = new GMPlayerSettingC2SRPC();

	public GMPlayerSettingS2CRPC GMPlayerSettingS2C = new GMPlayerSettingS2CRPC();

	public SetFriendNoteC2SRPC SetFriendNoteC2S = new SetFriendNoteC2SRPC();

	public SetFriendNoteS2CRPC SetFriendNoteS2C = new SetFriendNoteS2CRPC();

	public SetOnlineStatusC2SRPC SetOnlineStatusC2S = new SetOnlineStatusC2SRPC();

	public SetOnlineStatusS2CRPC SetOnlineStatusS2C = new SetOnlineStatusS2CRPC();

	public ChooseSkinC2SRPC ChooseSkinC2S = new ChooseSkinC2SRPC();

	public ChooseSkinS2CRPC ChooseSkinS2C = new ChooseSkinS2CRPC();

	public TimeWastingC2SRPC TimeWastingC2S = new TimeWastingC2SRPC();

	public TimeWastingS2CRPC TimeWastingS2C = new TimeWastingS2CRPC();

	public VoteC2SRPC VoteC2S = new VoteC2SRPC();

	public VoteS2CRPC VoteS2C = new VoteS2CRPC();

	public VoteSelectC2SRPC VoteSelectC2S = new VoteSelectC2SRPC();

	public VoteSelectS2CRPC VoteSelectS2C = new VoteSelectS2CRPC();

	public NotifyStoryC2SRPC NotifyStoryC2S = new NotifyStoryC2SRPC();

	public NotifyStoryS2CRPC NotifyStoryS2C = new NotifyStoryS2CRPC();

	public PveHeroTalentUpC2SRPC PveHeroTalentUpC2S = new PveHeroTalentUpC2SRPC();

	public PveHeroTalentUpS2CRPC PveHeroTalentUpS2C = new PveHeroTalentUpS2CRPC();

	public SelectEventC2SRPC SelectEventC2S = new SelectEventC2SRPC();

	public SelectEventS2CRPC SelectEventS2C = new SelectEventS2CRPC();

	public CampScoreC2SRPC CampScoreC2S = new CampScoreC2SRPC();

	public CampScoreS2CRPC CampScoreS2C = new CampScoreS2CRPC();

	public ActivityMissionRewardC2SRPC ActivityMissionRewardC2S = new ActivityMissionRewardC2SRPC();

	public ActivityMissionRewardS2CRPC ActivityMissionRewardS2C = new ActivityMissionRewardS2CRPC();

	public VendorBuyCardC2SRPC VendorBuyCardC2S = new VendorBuyCardC2SRPC();

	public VendorBuyCardS2CRPC VendorBuyCardS2C = new VendorBuyCardS2CRPC();

	public TransferStarDiscC2SRPC TransferStarDiscC2S = new TransferStarDiscC2SRPC();

	public TransferStarDiscS2CRPC TransferStarDiscS2C = new TransferStarDiscS2CRPC();

	public GetHeroInfoC2SRPC GetHeroInfoC2S = new GetHeroInfoC2SRPC();

	public GetHeroInfoS2CRPC GetHeroInfoS2C = new GetHeroInfoS2CRPC();

	public CreateMatchTeamC2SRPC CreateMatchTeamC2S = new CreateMatchTeamC2SRPC();

	public CreateMatchTeamS2CRPC CreateMatchTeamS2C = new CreateMatchTeamS2CRPC();

	public ChangeMatchTeamC2SRPC ChangeMatchTeamC2S = new ChangeMatchTeamC2SRPC();

	public ChangeMatchTeamS2CRPC ChangeMatchTeamS2C = new ChangeMatchTeamS2CRPC();

	public JoinMatchTeamC2SRPC JoinMatchTeamC2S = new JoinMatchTeamC2SRPC();

	public JoinMatchTeamS2CRPC JoinMatchTeamS2C = new JoinMatchTeamS2CRPC();

	public ExitMatchTeamC2SRPC ExitMatchTeamC2S = new ExitMatchTeamC2SRPC();

	public ExitMatchTeamS2CRPC ExitMatchTeamS2C = new ExitMatchTeamS2CRPC();

	public RefreshMatchTeamInfoC2SRPC RefreshMatchTeamInfoC2S = new RefreshMatchTeamInfoC2SRPC();

	public RefreshMatchTeamInfoS2CRPC RefreshMatchTeamInfoS2C = new RefreshMatchTeamInfoS2CRPC();

	public ChinaCreateOrderC2SRPC ChinaCreateOrderC2S = new ChinaCreateOrderC2SRPC();

	public ChinaCreateOrderS2CRPC ChinaCreateOrderS2C = new ChinaCreateOrderS2CRPC();

	public MatchTeamInviteC2SRPC MatchTeamInviteC2S = new MatchTeamInviteC2SRPC();

	public MatchTeamInviteS2CRPC MatchTeamInviteS2C = new MatchTeamInviteS2CRPC();

	public MatchTeamChatC2SRPC MatchTeamChatC2S = new MatchTeamChatC2SRPC();

	public MatchTeamChatS2CRPC MatchTeamChatS2C = new MatchTeamChatS2CRPC();

	public MatchTeamReadyC2SRPC MatchTeamReadyC2S = new MatchTeamReadyC2SRPC();

	public MatchTeamReadyS2CRPC MatchTeamReadyS2C = new MatchTeamReadyS2CRPC();

	public PlayerChatC2SRPC PlayerChatC2S = new PlayerChatC2SRPC();

	public PlayerChatS2CRPC PlayerChatS2C = new PlayerChatS2CRPC();

	public SyncSingleGameDataC2SRPC SyncSingleGameDataC2S = new SyncSingleGameDataC2SRPC();

	public SyncSingleGameDataS2CRPC SyncSingleGameDataS2C = new SyncSingleGameDataS2CRPC();

	public SingleGameDataC2SRPC SingleGameDataC2S = new SingleGameDataC2SRPC();

	public SingleGameDataS2CRPC SingleGameDataS2C = new SingleGameDataS2CRPC();

	public GetActivityPassRewardC2SRPC GetActivityPassRewardC2S = new GetActivityPassRewardC2SRPC();

	public GetActivityPassRewardS2CRPC GetActivityPassRewardS2C = new GetActivityPassRewardS2CRPC();

	public ApplyChangeSlotC2SRPC ApplyChangeSlotC2S = new ApplyChangeSlotC2SRPC();

	public ApplyChangeSlotS2CRPC ApplyChangeSlotS2C = new ApplyChangeSlotS2CRPC();

	public OpsChangeSlotC2SRPC OpsChangeSlotC2S = new OpsChangeSlotC2SRPC();

	public OpsChangeSlotS2CRPC OpsChangeSlotS2C = new OpsChangeSlotS2CRPC();

	public RookieGachaRewardC2SRPC RookieGachaRewardC2S = new RookieGachaRewardC2SRPC();

	public RookieGachaRewardS2CRPC RookieGachaRewardS2C = new RookieGachaRewardS2CRPC();

	public LiveGiftPackageC2SRPC LiveGiftPackageC2S = new LiveGiftPackageC2SRPC();

	public LiveGiftPackageS2CRPC LiveGiftPackageS2C = new LiveGiftPackageS2CRPC();

	public LaborActDiceC2SRPC LaborActDiceC2S = new LaborActDiceC2SRPC();

	public LaborActDiceS2CRPC LaborActDiceS2C = new LaborActDiceS2CRPC();

	public ActionOverTimeLogC2SRPC ActionOverTimeLogC2S = new ActionOverTimeLogC2SRPC();

	public ActionOverTimeLogS2CRPC ActionOverTimeLogS2C = new ActionOverTimeLogS2CRPC();

	public ClientHarmonyC2SRPC ClientHarmonyC2S = new ClientHarmonyC2SRPC();

	public ClientHarmonyS2CRPC ClientHarmonyS2C = new ClientHarmonyS2CRPC();

	public MailStarC2SRPC MailStarC2S = new MailStarC2SRPC();

	public MailStarS2CRPC MailStarS2C = new MailStarS2CRPC();

	public SetCardAltArtC2SRPC SetCardAltArtC2S = new SetCardAltArtC2SRPC();

	public SetCardAltArtS2CRPC SetCardAltArtS2C = new SetCardAltArtS2CRPC();

	public SelectRewardCardC2SRPC SelectRewardCardC2S = new SelectRewardCardC2SRPC();

	public SelectRewardCardS2CRPC SelectRewardCardS2C = new SelectRewardCardS2CRPC();

	public GetQuestionUrlC2SRPC GetQuestionUrlC2S = new GetQuestionUrlC2SRPC();

	public GetQuestionUrlS2CRPC GetQuestionUrlS2C = new GetQuestionUrlS2CRPC();

	public GetReturnInfoC2SRPC GetReturnInfoC2S = new GetReturnInfoC2SRPC();

	public GetReturnInfoS2CRPC GetReturnInfoS2C = new GetReturnInfoS2CRPC();

	public ReturnGiftClaimC2SRPC ReturnGiftClaimC2S = new ReturnGiftClaimC2SRPC();

	public ReturnGiftClaimS2CRPC ReturnGiftClaimS2C = new ReturnGiftClaimS2CRPC();

	public ReturnSignInClaimC2SRPC ReturnSignInClaimC2S = new ReturnSignInClaimC2SRPC();

	public ReturnSignInClaimS2CRPC ReturnSignInClaimS2C = new ReturnSignInClaimS2CRPC();

	public ReturnSurveyFinishC2SRPC ReturnSurveyFinishC2S = new ReturnSurveyFinishC2SRPC();

	public ReturnSurveyFinishS2CRPC ReturnSurveyFinishS2C = new ReturnSurveyFinishS2CRPC();

	public FlipCardC2SRPC FlipCardC2S = new FlipCardC2SRPC();

	public FlipCardS2CRPC FlipCardS2C = new FlipCardS2CRPC();

	public FlipCardProgressRewardC2SRPC FlipCardProgressRewardC2S = new FlipCardProgressRewardC2SRPC();

	public FlipCardProgressRewardS2CRPC FlipCardProgressRewardS2C = new FlipCardProgressRewardS2CRPC();

	public SyncPlayerGuildS2CRPC SyncPlayerGuildS2C = new SyncPlayerGuildS2CRPC();

	public SyncPlayerJoinGuildS2CRPC SyncPlayerJoinGuildS2C = new SyncPlayerJoinGuildS2CRPC();

	public GuildChatMsgS2CRPC GuildChatMsgS2C = new GuildChatMsgS2CRPC();

	public SyncGuildS2CRPC SyncGuildS2C = new SyncGuildS2CRPC();

	public SyncGuildMemberS2CRPC SyncGuildMemberS2C = new SyncGuildMemberS2CRPC();

	public SyncGuildMemberExitS2CRPC SyncGuildMemberExitS2C = new SyncGuildMemberExitS2CRPC();

	public CreateGuildC2SRPC CreateGuildC2S = new CreateGuildC2SRPC();

	public CreateGuildS2CRPC CreateGuildS2C = new CreateGuildS2CRPC();

	public SearchGuildC2SRPC SearchGuildC2S = new SearchGuildC2SRPC();

	public SearchGuildS2CRPC SearchGuildS2C = new SearchGuildS2CRPC();

	public ApplyToGuildC2SRPC ApplyToGuildC2S = new ApplyToGuildC2SRPC();

	public ApplyToGuildS2CRPC ApplyToGuildS2C = new ApplyToGuildS2CRPC();

	public ProcessGuildApplicationC2SRPC ProcessGuildApplicationC2S = new ProcessGuildApplicationC2SRPC();

	public ProcessGuildApplicationS2CRPC ProcessGuildApplicationS2C = new ProcessGuildApplicationS2CRPC();

	public SendGuildInvitationC2SRPC SendGuildInvitationC2S = new SendGuildInvitationC2SRPC();

	public SendGuildInvitationS2CRPC SendGuildInvitationS2C = new SendGuildInvitationS2CRPC();

	public ProcessGuildInvitationC2SRPC ProcessGuildInvitationC2S = new ProcessGuildInvitationC2SRPC();

	public ProcessGuildInvitationS2CRPC ProcessGuildInvitationS2C = new ProcessGuildInvitationS2CRPC();

	public GetGuildInfoC2SRPC GetGuildInfoC2S = new GetGuildInfoC2SRPC();

	public GetGuildInfoS2CRPC GetGuildInfoS2C = new GetGuildInfoS2CRPC();

	public UpdateGuildSettingsC2SRPC UpdateGuildSettingsC2S = new UpdateGuildSettingsC2SRPC();

	public UpdateGuildSettingsS2CRPC UpdateGuildSettingsS2C = new UpdateGuildSettingsS2CRPC();

	public UpdateGuildInAnnouncementC2SRPC UpdateGuildInAnnouncementC2S = new UpdateGuildInAnnouncementC2SRPC();

	public UpdateGuildInAnnouncementS2CRPC UpdateGuildInAnnouncementS2C = new UpdateGuildInAnnouncementS2CRPC();

	public TransferGuildMasterC2SRPC TransferGuildMasterC2S = new TransferGuildMasterC2SRPC();

	public TransferGuildMasterS2CRPC TransferGuildMasterS2C = new TransferGuildMasterS2CRPC();

	public ChangeGuildMemberTitleC2SRPC ChangeGuildMemberTitleC2S = new ChangeGuildMemberTitleC2SRPC();

	public ChangeGuildMemberTitleS2CRPC ChangeGuildMemberTitleS2C = new ChangeGuildMemberTitleS2CRPC();

	public KickGuildMemberC2SRPC KickGuildMemberC2S = new KickGuildMemberC2SRPC();

	public KickGuildMemberS2CRPC KickGuildMemberS2C = new KickGuildMemberS2CRPC();

	public ImpeachGuildMasterC2SRPC ImpeachGuildMasterC2S = new ImpeachGuildMasterC2SRPC();

	public ImpeachGuildMasterS2CRPC ImpeachGuildMasterS2C = new ImpeachGuildMasterS2CRPC();

	public ExitGuildC2SRPC ExitGuildC2S = new ExitGuildC2SRPC();

	public ExitGuildS2CRPC ExitGuildS2C = new ExitGuildS2CRPC();

	public DisbandGuildC2SRPC DisbandGuildC2S = new DisbandGuildC2SRPC();

	public DisbandGuildS2CRPC DisbandGuildS2C = new DisbandGuildS2CRPC();

	public GuildMissionRewardC2SRPC GuildMissionRewardC2S = new GuildMissionRewardC2SRPC();

	public GuildMissionRewardS2CRPC GuildMissionRewardS2C = new GuildMissionRewardS2CRPC();

	public GetGuildMemberChangeMsgC2SRPC GetGuildMemberChangeMsgC2S = new GetGuildMemberChangeMsgC2SRPC();

	public GetGuildMemberChangeMsgS2CRPC GetGuildMemberChangeMsgS2C = new GetGuildMemberChangeMsgS2CRPC();

	public SendGuildChatMsgC2SRPC SendGuildChatMsgC2S = new SendGuildChatMsgC2SRPC();

	public SendGuildChatMsgS2CRPC SendGuildChatMsgS2C = new SendGuildChatMsgS2CRPC();

	public GetGuildChatMsgC2SRPC GetGuildChatMsgC2S = new GetGuildChatMsgC2SRPC();

	public GetGuildChatMsgS2CRPC GetGuildChatMsgS2C = new GetGuildChatMsgS2CRPC();

	public GuildMemberC2SRPC GuildMemberC2S = new GuildMemberC2SRPC();

	public GuildMemberS2CRPC GuildMemberS2C = new GuildMemberS2CRPC();

	public GetGuildsInfoC2SRPC GetGuildsInfoC2S = new GetGuildsInfoC2SRPC();

	public GetGuildsInfoS2CRPC GetGuildsInfoS2C = new GetGuildsInfoS2CRPC();

	private async UniTask DealOnPushRPCMsg(Frame frame, RPCAsyncResult callAction = null)
	{
		OnPushRPCMsgData msg = new OnPushRPCMsgData(frame, callAction);
		if (!ImmediatelyProcessRPCMsg(msg.CMDID))
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.connectRoomId != 0L)
			{
				_ReconnectCacheRPCMsg.Add(msg);
			}
			else
			{
				EnqueueOnPushRPCMsg(msg);
			}
			return;
		}
		if (msg.CMDID == 5008)
		{
			_SyncRoomRPCMsg = msg;
		}
		await DealServerCallback(msg.CMDID, msg.UPSN, msg.errId, msg.PARAM);
		Frame frame2 = msg.frame;
		if (frame2 != null && !frame2.IsConsumed)
		{
			msg.frame.Consumed();
		}
		if (msg.callAction != null)
		{
			msg.callAction.isCompleted = true;
			msg.callAction.Finish();
		}
		if (callBackList.ContainsKey(msg.UPSN))
		{
			callBackList.Remove(msg.UPSN);
		}
	}

	private void EnqueueOnPushRPCMsg(OnPushRPCMsgData msg)
	{
		lock (_lockObj)
		{
			_OnPushRPCMsg.Add(msg);
			if (_OnPushRPCMsg.Count == 1)
			{
				ProcessNextPushRPCMsg().Forget();
			}
		}
	}

	public void DealReconnectCache()
	{
		if (_ReconnectCacheRPCMsg.Count == 0)
		{
			return;
		}
		for (int i = 0; i < _ReconnectCacheRPCMsg.Count; i++)
		{
			if (_SyncRoomRPCMsg != null && _ReconnectCacheRPCMsg[i].DOWNSN > _SyncRoomRPCMsg.DOWNSN)
			{
				EnqueueOnPushRPCMsg(_ReconnectCacheRPCMsg[i]);
			}
		}
		_ReconnectCacheRPCMsg.Clear();
	}

	private async UniTask ProcessNextPushRPCMsg()
	{
		OnPushRPCMsgData currentMessage;
		lock (_lockObj)
		{
			if (_OnPushRPCMsg.Count == 0)
			{
				return;
			}
			currentMessage = _OnPushRPCMsg[0];
		}
		await DealServerCallback(currentMessage.CMDID, currentMessage.UPSN, currentMessage.errId, currentMessage.PARAM);
		Frame frame = currentMessage.frame;
		if (frame != null && !frame.IsConsumed)
		{
			currentMessage.frame.Consumed();
		}
		if (currentMessage.callAction != null)
		{
			currentMessage.callAction.isCompleted = true;
			currentMessage.callAction.Finish();
		}
		if (callBackList.ContainsKey(currentMessage.UPSN))
		{
			callBackList.Remove(currentMessage.UPSN);
		}
		lock (_lockObj)
		{
			if (_OnPushRPCMsg.Count > 0)
			{
				_OnPushRPCMsg.RemoveAt(0);
			}
			if (_OnPushRPCMsg.Count > 0)
			{
				ProcessNextPushRPCMsg().Forget();
			}
		}
	}

	private bool ImmediatelyProcessRPCMsg(int cmdId)
	{
		if (cmdId != 5004 && cmdId != 5096 && cmdId != 5238 && cmdId != 5008 && cmdId != 5150 && cmdId != 5246 && cmdId != 5328 && cmdId != 1001)
		{
			return cmdId == 5012;
		}
		return true;
	}

	private void ClearRPCMsgData()
	{
		lock (_lockObj)
		{
			_OnPushRPCMsg.Clear();
		}
		_ReconnectCacheRPCMsg.Clear();
		_SyncRoomRPCMsg = null;
	}

	public int GetCallBackListCount()
	{
		return callBackList.Count;
	}

	public static int CreateUPSN()
	{
		UPSNGenerater++;
		UPSNGenerater = ((UPSNGenerater == int.MaxValue) ? 100 : UPSNGenerater);
		return UPSNGenerater;
	}

	public void UpdateSessionId(long _sessionId)
	{
		sessionId = _sessionId;
	}

	public static RPCAsyncResult RPCCallStatic<T>(T req, int CMDID, ProtolcalType type = ProtolcalType.Background) where T : IMessage<T>
	{
		RPCAsyncResult rPCAsyncResult = new RPCAsyncResult();
		int num = CreateUPSN();
		int num2 = req.ToByteArray().Length;
		Frame frame = new Frame(num2);
		frame.PutInt(num2);
		frame.PutLong(Convert.ToInt64(sessionId));
		frame.PutShort(CMDID);
		frame.PutByte(1);
		frame.PutByte(0);
		frame.PutByte(0);
		frame.PutLong(num);
		frame.PutLong(0L);
		frame.PutShort(0);
		frame.PutObject(req);
		rPCAsyncResult.CMDID = CMDID;
		rPCAsyncResult.UPSN = num;
		rPCAsyncResult.protoType = type;
		rPCAsyncResult.isTimeOut = false;
		rPCAsyncResult.frame = frame;
		return MonoSingletonProvider<NetManager>.inst.RPC.RPCCall(rPCAsyncResult);
	}

	private RPCAsyncResult RPCCall(RPCAsyncResult rpcResult)
	{
		return DealRPCCall(rpcResult);
	}

	internal RPCAsyncResult DealRPCCall(RPCAsyncResult rpcResult)
	{
		ProtolcalType type = rpcResult.protoType;
		callBackList[rpcResult.UPSN] = rpcResult;
		if (NetManager.IsReturningToLogin)
		{
			SetRpcActionConnectFailed(rpcResult);
			return rpcResult;
		}
		if (!MonoSingletonProvider<NetManager>.inst.IsConnected)
		{
			if (type == ProtolcalType.RealTime)
			{
				SetRpcActionConnectFailed(rpcResult);
				return rpcResult;
			}
			if (SimpleSingletonProvider<GameManager>.inst.IsLogined)
			{
				MonoSingletonProvider<NetManager>.inst.ReconnectWithMessageBoxUntilSuccess(rpcResult, delegate
				{
					if (type == ProtolcalType.Ahead)
					{
						MonoSingletonProvider<NetManager>.inst.PRCSendWithAnim(rpcResult);
					}
					else
					{
						MonoSingletonProvider<NetManager>.inst.SendRPCResult(rpcResult);
					}
				});
			}
			else
			{
				MonoSingletonProvider<NetManager>.inst.ReconnectWithMessageBoxUntilSuccess(rpcResult, delegate
				{
					if (type == ProtolcalType.Ahead)
					{
						MonoSingletonProvider<NetManager>.inst.PRCSendWithAnim(rpcResult);
					}
					else
					{
						MonoSingletonProvider<NetManager>.inst.SendRPCResult(rpcResult);
					}
				});
			}
		}
		else if (type == ProtolcalType.Ahead)
		{
			MonoSingletonProvider<NetManager>.inst.PRCSendWithAnim(rpcResult);
		}
		else
		{
			MonoSingletonProvider<NetManager>.inst.SendRPCResult(rpcResult);
		}
		return rpcResult;
	}

	public void ReceiveRPCCallStatic(Frame frame)
	{
		RPCAsyncResult rPCAsyncResult = null;
		if (frame.UPSN != 0L && callBackList.TryGetValue(frame.UPSN, out var value))
		{
			rPCAsyncResult = value;
			MonoSingletonProvider<NetManager>.inst.TimerSystem.DestroyTimer(rPCAsyncResult.timerID);
		}
		if (SimpleSingletonProvider<Core.Scene.SceneManager>.inst.currentType.Value != SceneType.Battle)
		{
			if (frame.VER1 > GameSettings.VER1)
			{
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(27.GetLocal(UIStringType.Message), delegate
				{
					SimpleSingletonProvider<GameManager>.inst.CloseGame();
				}).Forget();
				return;
			}
			if (frame.VER2 > GameSettings.VER2)
			{
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(27.GetLocal(UIStringType.Message), delegate
				{
					SimpleSingletonProvider<GameManager>.inst.CloseGame();
				}).Forget();
				return;
			}
			_ = frame.VER3;
			_ = GameSettings.VER3;
		}
		if (frame.ERR != 0 && frame.CMDID != 5126)
		{
			MonoSingletonProvider<NetManager>.inst.HandleRPCCustomACKErrorCode(frame.ERR);
		}
		DealOnPushRPCMsg(frame, rPCAsyncResult).Forget();
	}

	private void TryLoadLauncher()
	{
		SimpleSingletonProvider<AudioManager>.inst.StopAll();
		SimpleSingletonProvider<GameManager>.inst.Clear();
		EventSystem val = UnityEngine.Object.FindObjectOfType<EventSystem>();
		if ((UnityEngine.Object)(object)val != null)
		{
			UnityEngine.Object.Destroy(((Component)(object)val).gameObject);
		}
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Launcher");
	}

	internal void ResetRPCToUncompleted(RPCAsyncResult res)
	{
		res.isCompleted = false;
		res.isTimeOut = false;
		res.timerID = 0u;
	}

	public void ClearRPC(RPCAsyncResult exceptClearRpcResult = null)
	{
		List<RPCAsyncResult> list = new List<RPCAsyncResult>(callBackList.Values);
		list.AddRange(callBackList.Values);
		foreach (RPCAsyncResult item in list)
		{
			if (item != exceptClearRpcResult)
			{
				SetRpcActionConnectFailed(item);
			}
		}
		callBackList.Clear();
		if (exceptClearRpcResult != null)
		{
			callBackList[exceptClearRpcResult.UPSN] = exceptClearRpcResult;
		}
		ClearRPCMsgData();
	}

	internal void SetRpcActionConnectFailed(RPCAsyncResult rpcResult)
	{
		rpcResult.isCompleted = true;
		DealRPCCallBack(rpcResult);
		rpcResult.Clear();
	}

	internal void DealRPCCallBack(RPCAsyncResult action)
	{
		callBackList.Remove(action.UPSN);
	}

	private async UniTask DealServerCallback(int cmdID, long upsn, int errId, ByteBuf param)
	{
		param.ReaderIndex(35);
		if (cmdID == 1001)
		{
			await KickS2C.PushKickS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1002)
		{
			await PredictActionS2C.PushPredictActionS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1003)
		{
			await RunningGameS2C.PushRunningGameS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1007)
		{
			await BattleS2C.PushBattleS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1011)
		{
			await LotteryDrawS2C.PushLotteryDrawS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1013)
		{
			await LandBuffsS2C.PushLandBuffsS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1015)
		{
			await RoundStartS2C.PushRoundStartS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1016)
		{
			await GameFinishS2C.PushGameFinishS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1018)
		{
			await MonsterRefreshS2C.PushMonsterRefreshS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1019)
		{
			await MovePointBuffS2C.PushMovePointBuffS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1020)
		{
			await ChangeDirS2C.PushChangeDirS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1022)
		{
			await GambleChangeS2C.PushGambleChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1023)
		{
			await HeroBarBoxChangeS2C.PushHeroBarBoxChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1024)
		{
			await RoomNotifyS2C.PushRoomNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1026)
		{
			await ActionStartNotifyS2C.PushActionStartNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1031)
		{
			await NoGambleNotifyS2C.PushNoGambleNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1034)
		{
			await GambleObServeS2C.PushGambleObServeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1040)
		{
			await UpdateHeroAttrS2C.PushUpdateHeroAttrS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1042)
		{
			await ChangePlayerSlotS2C.PushChangePlayerSlotS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1043)
		{
			await BossSleepS2C.PushBossSleepS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1044)
		{
			await RefMallS2C.PushRefMallS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1045)
		{
			await BagItemChangeS2C.PushBagItemChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1046)
		{
			await RoleCardChangeS2C.PushRoleCardChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1047)
		{
			await TaskConditionS2C.PushTaskConditionS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1048)
		{
			await TaskInfoS2C.PushTaskInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1049)
		{
			await PlayerOnlineS2C.PushPlayerOnlineS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1050)
		{
			await ChangeExpS2C.PushChangeExpS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1051)
		{
			await MailAddS2C.PushMailAddS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1052)
		{
			await OnlineSyncRoomIdS2C.PushOnlineSyncRoomIdS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1053)
		{
			await NoticeS2C.PushNoticeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1054)
		{
			await ActivityTaskConditionS2C.PushActivityTaskConditionS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1055)
		{
			await MapEventS2C.PushMapEventS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1056)
		{
			await Day7RewardS2C.PushDay7RewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1057)
		{
			await MapEventTrainS2C.PushMapEventTrainS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1058)
		{
			await ChangePraiseNumS2C.PushChangePraiseNumS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1059)
		{
			await MonthlyCardS2C.PushMonthlyCardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1060)
		{
			await MailDelS2C.PushMailDelS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1061)
		{
			await FriendNotifyS2C.PushFriendNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1062)
		{
			await FriendListChangeS2C.PushFriendListChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1063)
		{
			await FriendInviteNotifyS2C.PushFriendInviteNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1064)
		{
			await LoopNoticeS2C.PushLoopNoticeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1065)
		{
			await BattlePassLvS2C.PushBattlePassLvS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1066)
		{
			await BattlePassTaskInfoS2C.PushBattlePassTaskInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1067)
		{
			await BattlePassUpdateTaskS2C.PushBattlePassUpdateTaskS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1068)
		{
			await BattlePassBuyS2C.PushBattlePassBuyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1069)
		{
			await BattlePassInfoS2C.PushBattlePassInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1070)
		{
			await FriendsChatMsgS2C.PushFriendsChatMsgS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1071)
		{
			await GameProgressChangeS2C.PushGameProgressChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1072)
		{
			await MapMissionNotifyS2C.PushMapMissionNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1073)
		{
			await ChangeItemLimitS2C.PushChangeItemLimitS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1074)
		{
			await CleanItemLimitS2C.PushCleanItemLimitS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1075)
		{
			await CampaignPassS2C.PushCampaignPassS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1076)
		{
			await CampaignNotifyS2C.PushCampaignNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1077)
		{
			await KillMessageS2C.PushKillMessageS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1078)
		{
			await GameScoreChangeS2C.PushGameScoreChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1079)
		{
			await SignInRewardS2C.PushSignInRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1080)
		{
			await PayResultS2C.PushPayResultS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1081)
		{
			await PayInfoChangeS2C.PushPayInfoChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1082)
		{
			await InviteSuccessS2C.PushInviteSuccessS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1083)
		{
			await InviteInfoNotifyS2C.PushInviteInfoNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1084)
		{
			await MapStatusChangeS2C.PushMapStatusChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1085)
		{
			await GachaCountS2C.PushGachaCountS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1086)
		{
			await MapIndexChangeS2C.PushMapIndexChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1087)
		{
			await SurrenderPunishS2C.PushSurrenderPunishS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1088)
		{
			await GamePassMapSuccessS2C.PushGamePassMapSuccessS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1090)
		{
			await FriendDelNotifyS2C.PushFriendDelNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1091)
		{
			await BagExpiredTransformNotify.PushBagExpiredTransformNotifyCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1092)
		{
			await MapEventCrabS2C.PushMapEventCrabS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1093)
		{
			await PkAfterVoteS2C.PushPkAfterVoteS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1094)
		{
			await PlayerTaskNotifyS2C.PushPlayerTaskNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1095)
		{
			await UnLockDifficultyS2C.PushUnLockDifficultyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1096)
		{
			await HeroSkillMoveEffectS2C.PushHeroSkillMoveEffectS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1097)
		{
			await TimeOutKickPlayerS2C.PushTimeOutKickPlayerS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1098)
		{
			await SayPhraseNotifyS2C.PushSayPhraseNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1099)
		{
			await MatchTeamInviteNotify.PushMatchTeamInviteNotifyCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1100)
		{
			await RefreshMatchTeamStateNotify.PushRefreshMatchTeamStateNotifyCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1101)
		{
			await ActivityPassGearChangeS2C.PushActivityPassGearChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1102)
		{
			await SingleGameScoreChangeS2C.PushSingleGameScoreChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1103)
		{
			await DelayProgressMapEventS2C.PushDelayProgressMapEventS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1104)
		{
			await LuckyStarMissionChangeS2C.PushLuckyStarMissionChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1105)
		{
			await MatchPunishmentS2C.PushMatchPunishmentS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1106)
		{
			await ChallengeDataChangeS2C.PushChallengeDataChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1107)
		{
			await GmUnlockRoleInfoS2C.PushGmUnlockRoleInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1108)
		{
			await SyncPlayerCreditInfoS2C.PushSyncPlayerCreditInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1109)
		{
			await RoomHeroCardChangeS2C.PushRoomHeroCardChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1110)
		{
			await RoomRoundAddTermS2C.PushRoomRoundAddTermS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1111)
		{
			await ReturnInfoS2C.PushReturnInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1112)
		{
			await SyncRelicsS2C.PushSyncRelicsS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1113)
		{
			await ReplaySnapshotS2C.PushReplaySnapshotS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1114)
		{
			await ClueNotifyS2C.PushClueNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1115)
		{
			await ReplayDieS2C.PushReplayDieS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1116)
		{
			await GuildTaskNotifyS2C.PushGuildTaskNotifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1117)
		{
			await GameRoundChangeS2C.PushGameRoundChangeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 1118)
		{
			await NotifyQuestionS2C.PushNotifyQuestionS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5002)
		{
			await ConnectS2C.PushConnectS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5004)
		{
			HeartbeatS2C.PushHeartbeatS2CCallBack(param);
		}
		if (cmdID == 5006)
		{
			await CreateRoomS2C.PushCreateRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5008)
		{
			await SyncRoomS2C.PushSyncRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5010)
		{
			await JoinRoomS2C.PushJoinRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5012)
		{
			await ExitRoomS2C.PushExitRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5014)
		{
			await QueryRoomS2C.PushQueryRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5016)
		{
			await RefreshRoomStateS2C.PushRefreshRoomStateS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5020)
		{
			await StartGameS2C.PushStartGameS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5022)
		{
			await ThrowDiceS2C.PushThrowDiceS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5024)
		{
			await ChangeRoomS2C.PushChangeRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5028)
		{
			await MoveS2C.PushMoveS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5030)
		{
			await ShopBuyS2C.PushShopBuyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5034)
		{
			await PursuitS2C.PushPursuitS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5036)
		{
			await BattleUseCardS2C.PushBattleUseCardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5038)
		{
			await BattleThrowDiceS2C.PushBattleThrowDiceS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5040)
		{
			await BattleChoiceS2C.PushBattleChoiceS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5042)
		{
			await LotteryChoiceS2C.PushLotteryChoiceS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5044)
		{
			await MoveAgainS2C.PushMoveAgainS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5048)
		{
			await AskBattleS2C.PushAskBattleS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5050)
		{
			await RollGoldS2C.PushRollGoldS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5052)
		{
			await EventThrowDiceS2C.PushEventThrowDiceS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5054)
		{
			await TriggerEventS2C.PushTriggerEventS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5056)
		{
			await UseEffectCardS2C.PushUseEffectCardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5060)
		{
			await BombThrowDiceS2C.PushBombThrowDiceS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5062)
		{
			await ChoiceDirectionS2C.PushChoiceDirectionS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5064)
		{
			await LandChoiceTargetS2C.PushLandChoiceTargetS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5068)
		{
			await ThrowDiceResultS2C.PushThrowDiceResultS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5070)
		{
			await TriggerDivinationS2C.PushTriggerDivinationS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5072)
		{
			await TriggerDestinyS2C.PushTriggerDestinyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5074)
		{
			await UseQuickCardS2C.PushUseQuickCardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5076)
		{
			await AbandonCardS2C.PushAbandonCardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5078)
		{
			await StopOrContinueS2C.PushStopOrContinueS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5082)
		{
			await StartGambleS2C.PushStartGambleS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5084)
		{
			await GambleThrowDicS2C.PushGambleThrowDicS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5086)
		{
			await ChoiceHeroS2C2.PushChoiceHeroS2C2CallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5088)
		{
			await AffirmHeroS2C.PushAffirmHeroS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5090)
		{
			await SearchRoomS2C.PushSearchRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5094)
		{
			await TriggerHospitalS2C.PushTriggerHospitalS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5096)
		{
			await SendChatS2C.PushSendChatS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5098)
		{
			await PlayerShopBuyS2C.PushPlayerShopBuyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5100)
		{
			await PlayerUseItemS2C.PushPlayerUseItemS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5102)
		{
			await UseTreasureS2C.PushUseTreasureS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5124)
		{
			await UseTreasureAutoTransformS2C.PushUseTreasureAutoTransformS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5104)
		{
			await SetFashionS2C.PushSetFashionS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5106)
		{
			await SelectFashionPlanS2C.PushSelectFashionPlanS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5108)
		{
			await GachaS2C.PushGachaS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5110)
		{
			await SteamSearchRoomS2C.PushSteamSearchRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5112)
		{
			await CheatItemS2C.PushCheatItemS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5114)
		{
			await RoleCardUpLvS2C.PushRoleCardUpLvS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5116)
		{
			await RoleCardBreakThroughS2C.PushRoleCardBreakThroughS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5118)
		{
			await RoleCardChoiceResS2C.PushRoleCardChoiceResS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5120)
		{
			await TaskRewardS2C.PushTaskRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5122)
		{
			await TeachingS2C.PushTeachingS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5126)
		{
			await QuickJoinRoomS2C.PushQuickJoinRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5128)
		{
			await RoomKickPlayerS2C.PushRoomKickPlayerS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5130)
		{
			await RoomAbdicationS2C.PushRoomAbdicationS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5132)
		{
			await RoomReadyS2C.PushRoomReadyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5134)
		{
			await ChargeCreateS2C.PushChargeCreateS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5136)
		{
			await ChargeS2C.PushChargeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5138)
		{
			await GiftCdkS2C.PushGiftCdkS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5140)
		{
			await MailReadS2C.PushMailReadS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5142)
		{
			await MailGetRewardS2C.PushMailGetRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5144)
		{
			await MailDelReadS2C.PushMailDelReadS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5146)
		{
			await GachaRecordS2C.PushGachaRecordS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5148)
		{
			await ActivityTaskRewardS2C.PushActivityTaskRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5150)
		{
			await RoomShortChatS2C.PushRoomShortChatS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5152)
		{
			await SetShowPlayerS2C.PushSetShowPlayerS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5154)
		{
			await GetShowPlayerS2C.PushGetShowPlayerS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5156)
		{
			await GetPlayerFightRecordS2C.PushGetPlayerFightRecordS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5158)
		{
			await GetDay7RewardS2C.PushGetDay7RewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5160)
		{
			await PraisePlayerS2C.PushPraisePlayerS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5162)
		{
			await ClientDataUploadS2C.PushClientDataUploadS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5164)
		{
			await FriendListS2C.PushFriendListS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5166)
		{
			await FriendApplyS2C.PushFriendApplyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5168)
		{
			await FriendApplyListS2C.PushFriendApplyListS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5170)
		{
			await FriendApplyOpS2C.PushFriendApplyOpS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5172)
		{
			await FriendOpS2C.PushFriendOpS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5174)
		{
			await FriendInviteS2C.PushFriendInviteS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5176)
		{
			await FriendInviteListS2C.PushFriendInviteListS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5180)
		{
			await FriendInviteCleanS2C.PushFriendInviteCleanS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5182)
		{
			await FriendBlacksListS2C.PushFriendBlacksListS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5184)
		{
			await NearFightPlayerS2C.PushNearFightPlayerS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5186)
		{
			await SearchPlayerS2C.PushSearchPlayerS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5188)
		{
			await ScratchCardS2C.PushScratchCardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5190)
		{
			await NextScratchCardPoolS2C.PushNextScratchCardPoolS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5192)
		{
			await WatchJoinRoomS2C.PushWatchJoinRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5194)
		{
			await WatchRefreshRoomStateS2C.PushWatchRefreshRoomStateS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5196)
		{
			await WatchExitRoomS2C.PushWatchExitRoomS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5198)
		{
			await BattlePassGetRewardS2C.PushBattlePassGetRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5200)
		{
			await BattlePassTaskRewardS2C.PushBattlePassTaskRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5202)
		{
			await BattlePassUpLvS2C.PushBattlePassUpLvS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5204)
		{
			await FriendSendMsgS2C.PushFriendSendMsgS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5206)
		{
			await GetChatMsgS2C.PushGetChatMsgS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5208)
		{
			await ReadChatMsgS2C.PushReadChatMsgS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5210)
		{
			await DelChatMsgInfoS2C.PushDelChatMsgInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5212)
		{
			await SelectRelicS2C.PushSelectRelicS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5214)
		{
			await MonsterPursuitS2C.PushMonsterPursuitS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5216)
		{
			await PVEShopBuyS2C.PushPVEShopBuyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5218)
		{
			await ClientCheckTaskS2C.PushClientCheckTaskS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5220)
		{
			await PveHeroUpLvS2C.PushPveHeroUpLvS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5222)
		{
			await StartMatchS2C.PushStartMatchS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5224)
		{
			await CancelMatchS2C.PushCancelMatchS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5226)
		{
			await MatchSuccessS2C.PushMatchSuccessS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5228)
		{
			await AccuseS2C.PushAccuseS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5230)
		{
			await SingleCampaignS2C.PushSingleCampaignS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5232)
		{
			await DevChargeS2C.PushDevChargeS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5234)
		{
			await AskReviveTeammateS2C.PushAskReviveTeammateS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5236)
		{
			await GetSignInRewardS2C.PushGetSignInRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5238)
		{
			await ChatMapMarkersS2C.PushChatMapMarkersS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5240)
		{
			await AbroadCreateOrderS2C.PushAbroadCreateOrderS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5244)
		{
			await AgeVerifyS2C.PushAgeVerifyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5246)
		{
			await ChangeNameS2C.PushChangeNameS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5248)
		{
			await ClientClickConfirmTaskS2C.PushClientClickConfirmTaskS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5250)
		{
			await BuyRelicS2C.PushBuyRelicS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5252)
		{
			await BuyLightGiftS2C.PushBuyLightGiftS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5254)
		{
			await LightGiftS2C.PushLightGiftS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5256)
		{
			await AcquisitionS2C.PushAcquisitionS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5258)
		{
			await AcquisitionRewardS2C.PushAcquisitionRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5260)
		{
			await SelectMechanismS2C.PushSelectMechanismS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5262)
		{
			await GachaCountRewardS2C.PushGachaCountRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5264)
		{
			await GetPlayerSimpleS2C.PushGetPlayerSimpleS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5266)
		{
			await RoleCardCollectS2C.PushRoleCardCollectS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5268)
		{
			await GMPlayerSettingS2C.PushGMPlayerSettingS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5270)
		{
			await SetFriendNoteS2C.PushSetFriendNoteS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5272)
		{
			await SetOnlineStatusS2C.PushSetOnlineStatusS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5306)
		{
			await ChooseSkinS2C.PushChooseSkinS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5308)
		{
			await TimeWastingS2C.PushTimeWastingS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5310)
		{
			await VoteS2C.PushVoteS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5312)
		{
			await VoteSelectS2C.PushVoteSelectS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5314)
		{
			await NotifyStoryS2C.PushNotifyStoryS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5316)
		{
			await PveHeroTalentUpS2C.PushPveHeroTalentUpS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5318)
		{
			await SelectEventS2C.PushSelectEventS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5320)
		{
			await CampScoreS2C.PushCampScoreS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5322)
		{
			await ActivityMissionRewardS2C.PushActivityMissionRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5324)
		{
			await VendorBuyCardS2C.PushVendorBuyCardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5326)
		{
			await TransferStarDiscS2C.PushTransferStarDiscS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5328)
		{
			await GetHeroInfoS2C.PushGetHeroInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5330)
		{
			await CreateMatchTeamS2C.PushCreateMatchTeamS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5332)
		{
			await ChangeMatchTeamS2C.PushChangeMatchTeamS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5334)
		{
			await JoinMatchTeamS2C.PushJoinMatchTeamS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5336)
		{
			await ExitMatchTeamS2C.PushExitMatchTeamS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5338)
		{
			await RefreshMatchTeamInfoS2C.PushRefreshMatchTeamInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5340)
		{
			await ChinaCreateOrderS2C.PushChinaCreateOrderS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5342)
		{
			await MatchTeamInviteS2C.PushMatchTeamInviteS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5344)
		{
			await MatchTeamChatS2C.PushMatchTeamChatS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5346)
		{
			await MatchTeamReadyS2C.PushMatchTeamReadyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5348)
		{
			await PlayerChatS2C.PushPlayerChatS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5350)
		{
			await SyncSingleGameDataS2C.PushSyncSingleGameDataS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5352)
		{
			await SingleGameDataS2C.PushSingleGameDataS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5354)
		{
			await GetActivityPassRewardS2C.PushGetActivityPassRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5356)
		{
			await ApplyChangeSlotS2C.PushApplyChangeSlotS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5358)
		{
			await OpsChangeSlotS2C.PushOpsChangeSlotS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5360)
		{
			await RookieGachaRewardS2C.PushRookieGachaRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5362)
		{
			await LiveGiftPackageS2C.PushLiveGiftPackageS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5364)
		{
			await LaborActDiceS2C.PushLaborActDiceS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5366)
		{
			await ActionOverTimeLogS2C.PushActionOverTimeLogS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5370)
		{
			await ClientHarmonyS2C.PushClientHarmonyS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5372)
		{
			await MailStarS2C.PushMailStarS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5374)
		{
			await SetCardAltArtS2C.PushSetCardAltArtS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5378)
		{
			await SelectRewardCardS2C.PushSelectRewardCardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5396)
		{
			await GetQuestionUrlS2C.PushGetQuestionUrlS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5380)
		{
			await GetReturnInfoS2C.PushGetReturnInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5382)
		{
			await ReturnGiftClaimS2C.PushReturnGiftClaimS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5384)
		{
			await ReturnSignInClaimS2C.PushReturnSignInClaimS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5386)
		{
			await ReturnSurveyFinishS2C.PushReturnSurveyFinishS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5388)
		{
			await FlipCardS2C.PushFlipCardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 5390)
		{
			await FlipCardProgressRewardS2C.PushFlipCardProgressRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 2001)
		{
			await SyncPlayerGuildS2C.PushSyncPlayerGuildS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 2002)
		{
			await SyncPlayerJoinGuildS2C.PushSyncPlayerJoinGuildS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 2003)
		{
			await GuildChatMsgS2C.PushGuildChatMsgS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 2004)
		{
			await SyncGuildS2C.PushSyncGuildS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 2005)
		{
			await SyncGuildMemberS2C.PushSyncGuildMemberS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 2006)
		{
			await SyncGuildMemberExitS2C.PushSyncGuildMemberExitS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9002)
		{
			await CreateGuildS2C.PushCreateGuildS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9004)
		{
			await SearchGuildS2C.PushSearchGuildS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9006)
		{
			await ApplyToGuildS2C.PushApplyToGuildS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9008)
		{
			await ProcessGuildApplicationS2C.PushProcessGuildApplicationS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9010)
		{
			await SendGuildInvitationS2C.PushSendGuildInvitationS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9012)
		{
			await ProcessGuildInvitationS2C.PushProcessGuildInvitationS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9014)
		{
			await GetGuildInfoS2C.PushGetGuildInfoS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9016)
		{
			await UpdateGuildSettingsS2C.PushUpdateGuildSettingsS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9018)
		{
			await UpdateGuildInAnnouncementS2C.PushUpdateGuildInAnnouncementS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9020)
		{
			await TransferGuildMasterS2C.PushTransferGuildMasterS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9022)
		{
			await ChangeGuildMemberTitleS2C.PushChangeGuildMemberTitleS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9024)
		{
			await KickGuildMemberS2C.PushKickGuildMemberS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9026)
		{
			await ImpeachGuildMasterS2C.PushImpeachGuildMasterS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9028)
		{
			await ExitGuildS2C.PushExitGuildS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9030)
		{
			await DisbandGuildS2C.PushDisbandGuildS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9032)
		{
			await GuildMissionRewardS2C.PushGuildMissionRewardS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9034)
		{
			await GetGuildMemberChangeMsgS2C.PushGetGuildMemberChangeMsgS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9036)
		{
			await SendGuildChatMsgS2C.PushSendGuildChatMsgS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9038)
		{
			await GetGuildChatMsgS2C.PushGetGuildChatMsgS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9040)
		{
			await GuildMemberS2C.PushGuildMemberS2CCallBack(param, errId, upsn == 0);
		}
		if (cmdID == 9042)
		{
			await GetGuildsInfoS2C.PushGetGuildsInfoS2CCallBack(param, errId, upsn == 0);
		}
	}

	public async UniTask<bool> ReceiveReplayS2CFrame(Frame frame)
	{
		if (frame == null)
		{
			Debug.LogError("[ReplayRPC] Inject failed. frame is null.");
			return false;
		}
		ByteBuf content = frame.GetContent();
		if (content == null)
		{
			Debug.LogError($"[ReplayRPC] Inject failed. frame content is null. cmdId={frame.CMDID}");
			return false;
		}
		Debug.Log($"[ReplayRPC] Inject S2C frame. cmdId={frame.CMDID}, upSn={frame.UPSN}, downSn={frame.DOWNSN}, err={frame.ERR}, payload={frame.LENGTH}");
		await DealServerCallback(frame.CMDID, frame.UPSN, frame.ERR, content);
		if (!frame.IsConsumed)
		{
			frame.Consumed();
		}
		return true;
	}

	internal void RegisterIRPCSyncConnect()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.Connect();
	}

	internal void UnRegisterIRPCSyncConnect()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.Disconnect();
	}
}
