using System.Collections.Generic;
using Core;
using SinglePlayer;
using Tools;
using party.model;
using party.protocol;

namespace GameLogic;

public class GameLogicManager : SimpleSingletonProvider<GameLogicManager>, IReadPoint
{
	public bool initialize;

	public Signal noticeUpdate = new Signal();

	public List<long> lockExpressionPlayerID = new List<long>();

	public RoomListLogic roomList { get; private set; }

	public RoomLogic room { get; private set; }

	public BattleLogic battle { get; private set; }

	public CardLogic card { get; private set; }

	public BuffLogic buff { get; private set; }

	public FightLogic fight { get; private set; }

	public LandLogic land { get; private set; }

	public BattleResultLogic battleResult { get; private set; }

	public ReplayLogic replay { get; private set; }

	public GameModePlayLogic gameModePlay { get; private set; }

	public RelicLogic relic { get; private set; }

	public MatchLogic match { get; private set; }

	public MapLogic map { get; private set; }

	public StoryLogic story { get; private set; }

	public AssistVoteLogic assistVote { get; private set; }

	public AltArtCardLogic altArtCardLogic { get; private set; }

	public PerformTriggerLogic performTriggerLogic { get; private set; }

	public ActionLogic action { get; private set; }

	public GuideLogic guide { get; private set; }

	public TutorialLogic tutorial { get; private set; }

	public HomeLogic home { get; private set; }

	public BagLogic bag { get; private set; }

	public MailLogic mail { get; private set; }

	public StoreLogic store { get; private set; }

	public FashionLogic fashion { get; private set; }

	public HeroCardLogic heroCard { get; private set; }

	public GachaLogic gacha { get; private set; }

	public TaskLogic task { get; private set; }

	public ActivityLogic activity { get; private set; }

	public CommunicateLogic communicate { get; private set; }

	public FriendLogic friend { get; private set; }

	public WatchLogic watch { get; private set; }

	public BattlePassLogic battlePass { get; private set; }

	public ActivityPassLogic activityPass { get; private set; }

	public CampaignLogic campaign { get; private set; }

	public CollaborateLogic collaborate { get; private set; }

	public SignInLogic signIn { get; private set; }

	public ComebackLogic comeback { get; private set; }

	public BnSdkLogic bnSdk { get; private set; }

	public AcquisitionLogic acquisition { get; private set; }

	public GuildLogic guild { get; private set; }

	public SurveyLogic survey { get; private set; }

	public OnlineLogic online { get; private set; }

	public AccountLogic account { get; private set; }

	public SinglePlayerLogic singlePlayer { get; private set; }

	public long connectRoomId { get; private set; }

	public bool InitShowTime => connectRoomId == 0;

	protected override void InstanceInit()
	{
		initialize = false;
	}

	public void InitLogics()
	{
		initialize = true;
		home = new HomeLogic();
		account = new AccountLogic();
		online = new OnlineLogic();
		room = new RoomLogic();
		land = new LandLogic();
		card = new CardLogic();
		buff = new BuffLogic();
		fight = new FightLogic();
		action = new ActionLogic();
		battleResult = new BattleResultLogic();
		replay = new ReplayLogic();
		bag = new BagLogic();
		store = new StoreLogic();
		fashion = new FashionLogic();
		gacha = new GachaLogic();
		roomList = new RoomListLogic();
		guide = new GuideLogic();
		heroCard = new HeroCardLogic();
		task = new TaskLogic();
		mail = new MailLogic();
		battle = new BattleLogic();
		activity = new ActivityLogic();
		communicate = new CommunicateLogic();
		gameModePlay = new GameModePlayLogic();
		friend = new FriendLogic();
		watch = new WatchLogic();
		battlePass = new BattlePassLogic();
		activityPass = new ActivityPassLogic();
		relic = new RelicLogic();
		match = new MatchLogic();
		campaign = new CampaignLogic();
		collaborate = new CollaborateLogic();
		signIn = new SignInLogic();
		comeback = new ComebackLogic();
		bnSdk = new BnSdkLogic();
		acquisition = new AcquisitionLogic();
		map = new MapLogic();
		story = new StoryLogic();
		assistVote = new AssistVoteLogic();
		singlePlayer = new SinglePlayerLogic();
		tutorial = new TutorialLogic();
		altArtCardLogic = new AltArtCardLogic();
		performTriggerLogic = new PerformTriggerLogic();
		guild = new GuildLogic();
		survey = new SurveyLogic();
	}

	public void InitAccount(AccountInfo _account, Player player, ConnectS2C connectResponse = null)
	{
		if (!initialize)
		{
			InitLogics();
		}
		account.InitFromServer(_account, player, connectResponse);
		bag.InitFromServer(player.BagItems, player.WeeklyLimits);
		store.InitFromServer(player.ShopInfo, player.Day7, player.MonthlyCardRemDays);
		fashion.InitFromServer(player.UsePlan, player.FashionPlan);
		heroCard.InitFromServer(player.RoleCard, player.SportsMeetInfo);
		task.InitFromServer(player.Task, player.MissionMod);
		mail.InitFromServer(player.Mails);
		activity.InitFromServer(player);
		friend.InitFromServer(player.Friends);
		battlePass.InitFromServer(player.BattlePass);
		activityPass.InitFromServer(player.ActivityPass);
		communicate.InitFromServer(player);
		campaign.InitFromServer(player);
		signIn.InitFromServer(player);
		acquisition.InitFromServer(player.InviteInfo);
		gacha.InitFromServer(player);
		match.InitFromServer(player.MatchTeamId);
		story.InitFromServer();
		altArtCardLogic.InitFromServer(player.AltArtCards);
		guild.InitFromServer(player.GuildInfo);
		survey.InitFromServer(player.QuestionInfo);
		bnSdk.InitFromServer(player);
	}

	public void Connect()
	{
		online.Connect();
		account.Connect();
		room.Connect();
		land.Connect();
		card.Connect();
		buff.Connect();
		fight.Connect();
		action.Connect();
		battleResult.Connect();
		replay.Connect();
		bag.Connect();
		mail.Connect();
		store.Connect();
		fashion.Connect();
		gacha.Connect();
		roomList.Connect();
		heroCard.Connect();
		task.Connect();
		guide.Connect();
		battle.Connect();
		activity.Connect();
		communicate.Connect();
		gameModePlay.Connect();
		friend.Connect();
		watch.Connect();
		battlePass.Connect();
		activityPass.Connect();
		relic.Connect();
		match.Connect();
		campaign.Connect();
		signIn.Connect();
		comeback.Connect();
		bnSdk.Connect();
		acquisition.Connect();
		map.Connect();
		story.Connect();
		assistVote.Connect();
		singlePlayer.Connect();
		tutorial.Connect();
		home.Connect();
		altArtCardLogic.Connect();
		guild.Connect();
		survey.Connect();
	}

	public void Disconnect()
	{
		online?.Disconnect();
		account?.Disconnect();
		room?.Disconnect();
		land?.Disconnect();
		card?.Disconnect();
		buff?.Disconnect();
		fight?.Disconnect();
		action?.Disconnect();
		battleResult?.Disconnect();
		replay?.Disconnect();
		bag?.Disconnect();
		mail?.Disconnect();
		store?.Disconnect();
		fashion?.Disconnect();
		gacha?.Disconnect();
		roomList?.Disconnect();
		heroCard?.Disconnect();
		task?.Disconnect();
		guide?.Disconnect();
		battle?.Disconnect();
		activity?.Disconnect();
		communicate?.Disconnect();
		gameModePlay?.Disconnect();
		friend?.Disconnect();
		watch?.Disconnect();
		battlePass?.Disconnect();
		activityPass?.Disconnect();
		relic?.Disconnect();
		match?.Disconnect();
		campaign?.Disconnect();
		signIn?.Disconnect();
		comeback?.Disconnect();
		bnSdk?.Disconnect();
		acquisition?.Disconnect();
		map?.Disconnect();
		story?.Disconnect();
		assistVote?.Disconnect();
		singlePlayer?.Disconnect();
		tutorial?.Disconnect();
		home?.Disconnect();
		altArtCardLogic?.Disconnect();
		guild?.Disconnect();
		survey?.Disconnect();
	}

	public void Clear()
	{
		replay?.Clear();
		replay = null;
		home = null;
		online = null;
		account = null;
		room = null;
		battle?.Dispose();
		battle = null;
		land = null;
		card = null;
		buff = null;
		fight = null;
		action = null;
		battleResult = null;
		bag = null;
		mail = null;
		store = null;
		fashion?.Dispose();
		fashion = null;
		gacha = null;
		roomList = null;
		guide = null;
		heroCard = null;
		task = null;
		activity = null;
		communicate = null;
		gameModePlay = null;
		friend = null;
		watch = null;
		battlePass = null;
		activityPass = null;
		match = null;
		campaign = null;
		collaborate = null;
		signIn = null;
		comeback = null;
		bnSdk = null;
		acquisition = null;
		map = null;
		story = null;
		assistVote = null;
		singlePlayer = null;
		altArtCardLogic = null;
		performTriggerLogic = null;
		guild?.Clear();
		guild = null;
		survey = null;
	}

	public void Dispose()
	{
		card?.Dispose();
		battle?.Dispose();
		action?.Dispose();
	}

	public void RegisterRed()
	{
		task?.RegisterRed();
		bag?.RegisterRed();
		activity?.RegisterRed();
		store?.RegisterRed();
		fashion?.RegisterRed();
		altArtCardLogic?.RegisterRed();
		comeback?.RegisterRed();
		survey?.RegisterRed();
	}

	public ActionEffectShow CreatePerform()
	{
		return new ActionEffectShow();
	}

	public void SetConnectRoomId(long roomId)
	{
		connectRoomId = roomId;
	}

	public void ResetConnectRoomId()
	{
		connectRoomId = 0L;
	}

	public bool SetLockChatPlayer(long playerId)
	{
		if (!lockExpressionPlayerID.Contains(playerId))
		{
			lockExpressionPlayerID.Add(playerId);
			return true;
		}
		lockExpressionPlayerID.Remove(playerId);
		return false;
	}
}
