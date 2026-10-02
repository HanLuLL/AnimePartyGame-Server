using System;
using System.Collections.Generic;
using Core;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI;

public class UIManager : SimpleSingletonProvider<UIManager>
{
	public readonly UISignal signal = new UISignal();

	private readonly Dictionary<UIPanelType, IBasePanel> _cachedPanelDict = new Dictionary<UIPanelType, IBasePanel>(EnumComparerRef.uiPanelTypeComparer);

	private readonly Stack<IBasePanel> _panelStack = new Stack<IBasePanel>();

	public Stack<BaseWindow> propUpWindows = new Stack<BaseWindow>();

	private List<BaseWindow> _unclearedWindows = new List<BaseWindow>();

	private SurveyCenterWindow _cachedSurveyCenterWindow;

	private ReplayWindow _cachedReplayWindow;

	private ExpressionListWindow _cachedExpressionListWindow;

	private CreditWarningWindow _cachedCreditWarningWindow;

	private ChooseRoundCardWindow _cachedChooseRoundCardWindow;

	private RoomTermsWindow _cachedRoomTermsWindow;

	private BattleLandTipWindow _cachedBattleLandTipWindow;

	private LuckyStarMissionWindow _cachedLuckyStarMissionWindow;

	private BattleVideoWindow _cachedBattleVideoWindow;

	private NewGameLibraryWindow _cachedNewGameLibraryWindow;

	private Anniversary_2ndWindow _cachedAnniversary_2ndWindow;

	private DouYinRewardWindow _cachedDouYinRewardWindow;

	private FriendLeaderboardWindow _cachedFriendLeaderboardWindow;

	private RookieTaskInfoWindow _cachedRookieTaskInfoWindow;

	private TutorialWindow _cachedTutorialWindow;

	private MatchInfoWindow _cachedMatchInfoWindow;

	private GachaRookieRewardWindow _cachedGachaRookieRewardWindow;

	private AssistVoteS7Window _cachedAssistVoteS7Window;

	private SettingListWindow _cachedSettingList;

	private SettingInBattleWindow _cachedSettingInBattle;

	private SinglePlayerSettingInBattleWindow _cachedSinglePlayerSettingInBattleWindow;

	private FightWindow _cachedFightWindow;

	private CardWindow _cachedCardWindow;

	private SkillWindow _cacheSkillWindow;

	private LoseCardWindow _cacheCardWindow;

	private BattlePlayerInfoWindow _cachedBattlePlayerInfoWindow;

	private LandBatteryWindow _cachedLandBatteryWindow;

	private LandDivinationWindow _cachedLandDivinationWindow;

	private LandEventWindow _cachedLandEventWindow;

	private LandFillingStationWindow _cachedLandFillingStationWindow;

	private LandGambleWindow _cachedLandGambleWindow;

	private LandHospitalWindow _cachedLandHospitalWindow;

	private LandPursuitWindow _cachedLandPursuitWindow;

	private LandRollGoldWindow _cachedLandRollGoldWindow;

	private LandShopWindow _cachedLandShopWindow;

	private LandLotteryWindow _cachedLandLotteryWindow;

	private LandPrayWindow _cacheLandPrayWindow;

	private LandRelicWindow _cacheLandRelicWindow;

	private LandVendorWindow _cacheLandVendorWindow;

	private ExpressionWindow _cachedExpressionWindow;

	private UpgradeWindow _cachedUpgradeWindow;

	private ExplainWindow _cachedExplainWindow;

	private RelicWindow _cachedRelicWindow;

	private BattleSelectMonsterWindow _cachedBattleSelectMonsterWindow;

	private BattlePreMonsterWindow _cachedBattlePreMonsterWindow;

	private ATMWindow _cachedATMWindow;

	private BattleRelicInfoWindow _cachedBattleRelicInfoWindow;

	private OperateTimeWindow _cachedOperateTimeWindow;

	private StoryWindow _cachedStoryWindow;

	private AssistVoteWindow _cachedAssistVoteWindow;

	private BattleHintWindow _cachedBattleHintWindow;

	private SettingWindow _cachedSettingWindow;

	private NoticeWindow _cachedNoticeWindow;

	private SystemTipsWindow _cachedSystemTipsWindow;

	private GameInfoWindow _cachedGameInfoWindow;

	private GMWindow _cachedGMWindow;

	private TipsWindow _cachedTipsWindow;

	private LoadingTipWindow _cachedLoadingTipWindow;

	private MessageBoxWindow _cachedMessageBoxWindow;

	private PropDetailWindow _cachedPropDetailWindow;

	private PurchaseWindow _cachedPurchaseWindow;

	private BoxPropWindow _cachedBoxPropWindow;

	private RechargeTipWindow _cachedRechargeTipWindow;

	private AccountInfoWindow _cachedAccountInfoWindow;

	private RewardWindow _cachedRewardWindow;

	private ShowSkinWindow _cachedShowSkinWindow;

	private RuleWindow _cachedRuleWindow;

	private InviteWindow _cachedInviteWindow;

	private InputWindow _cachedInputWindow;

	private ChatWindow _cachedChatWindow;

	private ActivityPopupWindow _cachedActivityPopupWindow;

	private GachaInfoWindow _cachedGachaInfoWindow;

	private GuideWindow _cachedGuideWindow;

	private SelectTutorialWindow _cachedSelectTutorialWindow;

	private InfoWindow _cachedInfoWindow;

	private RoomFilterWindow _cachedRoomFilterWindow;

	private NGOStoreWindow _cachedNGOStoreWindow;

	private WitchWeaponWindow _cachedWitchWeaponWindow;

	private MGWTStoreWindow _cachedMGWTStoreWindow;

	private SignInWindow _cachedSignInWindow;

	private PlayerRenameWindow _playerRenameWindow;

	private DiscountWindow _cachedDiscountWindow;

	private LoadingWindow _cachedLoadingWindow;

	private DisplayCardWindow _cachedDisplayCardWindow;

	private SkinSellWindow _cachedSkinSellWindow;

	private VA11HallAWindow _cachedVA11HallAWindow;

	private MatchSuccessWindow _cachedMatchSuccessWindow;

	public IBasePanel backgroundPanel { get; private set; }

	public IBasePanel bottomMenuPanel { get; private set; }

	public IBasePanel activityHubPanel { get; private set; }

	public IBasePanel currentPanel { get; private set; }

	public SurveyCenterWindow SurveyCenter => _cachedSurveyCenterWindow ?? (_cachedSurveyCenterWindow = new SurveyCenterWindow(UIWindowType.SurveyCenter));

	public ReplayWindow Replay => _cachedReplayWindow ?? (_cachedReplayWindow = new ReplayWindow(UIWindowType.Replay));

	public ExpressionListWindow ExpressionList => _cachedExpressionListWindow ?? (_cachedExpressionListWindow = new ExpressionListWindow(UIWindowType.ExpressionList));

	public CreditWarningWindow CreditWarning => _cachedCreditWarningWindow ?? (_cachedCreditWarningWindow = new CreditWarningWindow(UIWindowType.CreditWarning));

	public ChooseRoundCardWindow ChooseRoundCard => _cachedChooseRoundCardWindow ?? (_cachedChooseRoundCardWindow = new ChooseRoundCardWindow(UIWindowType.ChooseRoundCard));

	public RoomTermsWindow RoomTerms => _cachedRoomTermsWindow ?? (_cachedRoomTermsWindow = new RoomTermsWindow(UIWindowType.RoomTerms));

	public BattleLandTipWindow BattleLandTip => _cachedBattleLandTipWindow ?? (_cachedBattleLandTipWindow = new BattleLandTipWindow(UIWindowType.BattleLandTip));

	public LuckyStarMissionWindow LuckyStarMission => _cachedLuckyStarMissionWindow ?? (_cachedLuckyStarMissionWindow = new LuckyStarMissionWindow(UIWindowType.LuckyStarMission));

	public BattleVideoWindow BattleVideo => _cachedBattleVideoWindow ?? (_cachedBattleVideoWindow = new BattleVideoWindow(UIWindowType.BattleVideo));

	public NewGameLibraryWindow NewGameLibrary => _cachedNewGameLibraryWindow ?? (_cachedNewGameLibraryWindow = new NewGameLibraryWindow(UIWindowType.NewGameLibrary));

	public Anniversary_2ndWindow Anniversary_2nd => _cachedAnniversary_2ndWindow ?? (_cachedAnniversary_2ndWindow = new Anniversary_2ndWindow(UIWindowType.Anniversary2Nd));

	public DouYinRewardWindow DouYinReward => _cachedDouYinRewardWindow ?? (_cachedDouYinRewardWindow = new DouYinRewardWindow(UIWindowType.DouYinReward));

	public FriendLeaderboardWindow FriendLeaderboard => _cachedFriendLeaderboardWindow ?? (_cachedFriendLeaderboardWindow = new FriendLeaderboardWindow(UIWindowType.FriendLeaderboard));

	public RookieTaskInfoWindow RookieTaskInfo => _cachedRookieTaskInfoWindow ?? (_cachedRookieTaskInfoWindow = new RookieTaskInfoWindow(UIWindowType.RookieTaskInfo));

	public TutorialWindow tutorial => _cachedTutorialWindow ?? (_cachedTutorialWindow = new TutorialWindow(UIWindowType.Tutorial));

	public MatchInfoWindow MatchInfo => _cachedMatchInfoWindow ?? (_cachedMatchInfoWindow = new MatchInfoWindow(UIWindowType.MatchInfo));

	public GachaRookieRewardWindow GachaRookieReward => _cachedGachaRookieRewardWindow ?? (_cachedGachaRookieRewardWindow = new GachaRookieRewardWindow(UIWindowType.GachaRookieReward));

	public AssistVoteS7Window AssistVoteS7 => _cachedAssistVoteS7Window ?? (_cachedAssistVoteS7Window = new AssistVoteS7Window(UIWindowType.AssistVoteS7));

	public SettingListWindow settingList => _cachedSettingList ?? (_cachedSettingList = new SettingListWindow(UIWindowType.SettingList));

	public SettingInBattleWindow settingInBattle => _cachedSettingInBattle ?? (_cachedSettingInBattle = new SettingInBattleWindow(UIWindowType.SettingInBattle));

	public SinglePlayerSettingInBattleWindow SinglePlayerSettingInBattle => _cachedSinglePlayerSettingInBattleWindow ?? (_cachedSinglePlayerSettingInBattleWindow = new SinglePlayerSettingInBattleWindow(UIWindowType.SinglePlayerSettingInBattle));

	public FightWindow Fight => _cachedFightWindow ?? (_cachedFightWindow = new FightWindow(UIWindowType.Fight));

	public CardWindow cardWindow => _cachedCardWindow ?? (_cachedCardWindow = new CardWindow(UIWindowType.Card));

	public SkillWindow skill => _cacheSkillWindow ?? (_cacheSkillWindow = new SkillWindow(UIWindowType.Skill));

	public LoseCardWindow loseCard => _cacheCardWindow ?? (_cacheCardWindow = new LoseCardWindow(UIWindowType.LoseCard));

	public BattlePlayerInfoWindow battlePlayerInfo => _cachedBattlePlayerInfoWindow ?? (_cachedBattlePlayerInfoWindow = new BattlePlayerInfoWindow(UIWindowType.BattlePlayerInfo));

	public LandBatteryWindow landBattery => _cachedLandBatteryWindow ?? (_cachedLandBatteryWindow = new LandBatteryWindow(UIWindowType.LandBattery));

	public LandDivinationWindow landDivination => _cachedLandDivinationWindow ?? (_cachedLandDivinationWindow = new LandDivinationWindow(UIWindowType.LandDivination));

	public LandEventWindow landEvent => _cachedLandEventWindow ?? (_cachedLandEventWindow = new LandEventWindow(UIWindowType.LandEvent));

	public LandFillingStationWindow landFillingStation => _cachedLandFillingStationWindow ?? (_cachedLandFillingStationWindow = new LandFillingStationWindow(UIWindowType.LandFillingStation));

	public LandGambleWindow landGamble => _cachedLandGambleWindow ?? (_cachedLandGambleWindow = new LandGambleWindow(UIWindowType.LandGamble));

	public LandHospitalWindow landHospital => _cachedLandHospitalWindow ?? (_cachedLandHospitalWindow = new LandHospitalWindow(UIWindowType.LandHospital));

	public LandPursuitWindow landPursuit => _cachedLandPursuitWindow ?? (_cachedLandPursuitWindow = new LandPursuitWindow(UIWindowType.LandPursuit));

	public LandRollGoldWindow landRollGold => _cachedLandRollGoldWindow ?? (_cachedLandRollGoldWindow = new LandRollGoldWindow(UIWindowType.LandRollGold));

	public LandShopWindow landShop => _cachedLandShopWindow ?? (_cachedLandShopWindow = new LandShopWindow(UIWindowType.LandShop));

	public LandLotteryWindow landLottery => _cachedLandLotteryWindow ?? (_cachedLandLotteryWindow = new LandLotteryWindow(UIWindowType.LandLottery));

	public LandPrayWindow landPray => _cacheLandPrayWindow ?? (_cacheLandPrayWindow = new LandPrayWindow(UIWindowType.LandPray));

	public LandRelicWindow landRelic => _cacheLandRelicWindow ?? (_cacheLandRelicWindow = new LandRelicWindow(UIWindowType.LandRelic));

	public LandVendorWindow landVendor => _cacheLandVendorWindow ?? (_cacheLandVendorWindow = new LandVendorWindow(UIWindowType.LandVendor));

	public ExpressionWindow expression => _cachedExpressionWindow ?? (_cachedExpressionWindow = new ExpressionWindow(UIWindowType.Expression));

	public UpgradeWindow upgradeWindow => _cachedUpgradeWindow ?? (_cachedUpgradeWindow = new UpgradeWindow(UIWindowType.Upgrade));

	public ExplainWindow explain => _cachedExplainWindow ?? (_cachedExplainWindow = new ExplainWindow(UIWindowType.Explain));

	public RelicWindow relic => _cachedRelicWindow ?? (_cachedRelicWindow = new RelicWindow(UIWindowType.Relic));

	public BattleSelectMonsterWindow battleSelectMonster => _cachedBattleSelectMonsterWindow ?? (_cachedBattleSelectMonsterWindow = new BattleSelectMonsterWindow(UIWindowType.BattleSelectMonster));

	public BattlePreMonsterWindow battlePreMonster => _cachedBattlePreMonsterWindow ?? (_cachedBattlePreMonsterWindow = new BattlePreMonsterWindow(UIWindowType.BattlePreMonster));

	public ATMWindow atm => _cachedATMWindow ?? (_cachedATMWindow = new ATMWindow(UIWindowType.Atm));

	public BattleRelicInfoWindow battleRelicInfo => _cachedBattleRelicInfoWindow ?? (_cachedBattleRelicInfoWindow = new BattleRelicInfoWindow(UIWindowType.BattleRelicInfo));

	public OperateTimeWindow operateTime => _cachedOperateTimeWindow ?? (_cachedOperateTimeWindow = new OperateTimeWindow(UIWindowType.OperateTime));

	public StoryWindow story => _cachedStoryWindow ?? (_cachedStoryWindow = new StoryWindow(UIWindowType.Story));

	public AssistVoteWindow assistVote => _cachedAssistVoteWindow ?? (_cachedAssistVoteWindow = new AssistVoteWindow(UIWindowType.AssistVote));

	public BattleHintWindow battleHint => _cachedBattleHintWindow ?? (_cachedBattleHintWindow = new BattleHintWindow(UIWindowType.BattleHint));

	public SettingWindow setting => _cachedSettingWindow ?? (_cachedSettingWindow = new SettingWindow(UIWindowType.Setting));

	public NoticeWindow notice => _cachedNoticeWindow ?? (_cachedNoticeWindow = new NoticeWindow(UIWindowType.Notice));

	public SystemTipsWindow systemTips => _cachedSystemTipsWindow ?? (_cachedSystemTipsWindow = new SystemTipsWindow(UIWindowType.SystemTips));

	public GameInfoWindow gameInfo => _cachedGameInfoWindow ?? (_cachedGameInfoWindow = new GameInfoWindow(UIWindowType.GameInfo));

	public GMWindow GM => _cachedGMWindow ?? (_cachedGMWindow = new GMWindow(UIWindowType.Gm));

	public TipsWindow tips => _cachedTipsWindow ?? (_cachedTipsWindow = new TipsWindow(UIWindowType.Tips));

	public LoadingTipWindow loadingTip => _cachedLoadingTipWindow ?? (_cachedLoadingTipWindow = new LoadingTipWindow(UIWindowType.LoadingTip));

	public bool existMessageBox => _cacheCardWindow != null;

	public MessageBoxWindow messageBox => _cachedMessageBoxWindow ?? (_cachedMessageBoxWindow = new MessageBoxWindow(UIWindowType.MessageBox));

	public PropDetailWindow propDetail => _cachedPropDetailWindow ?? (_cachedPropDetailWindow = new PropDetailWindow(UIWindowType.PropDetail));

	public PurchaseWindow purchase => _cachedPurchaseWindow ?? (_cachedPurchaseWindow = new PurchaseWindow(UIWindowType.Purchase));

	public BoxPropWindow boxProp => _cachedBoxPropWindow ?? (_cachedBoxPropWindow = new BoxPropWindow(UIWindowType.BoxProp));

	public RechargeTipWindow rechargeTip => _cachedRechargeTipWindow ?? (_cachedRechargeTipWindow = new RechargeTipWindow(UIWindowType.RechargeTip));

	public AccountInfoWindow AccountInfo => _cachedAccountInfoWindow ?? (_cachedAccountInfoWindow = new AccountInfoWindow(UIWindowType.AccountInfo));

	public RewardWindow reward => _cachedRewardWindow ?? (_cachedRewardWindow = new RewardWindow(UIWindowType.Reward));

	public ShowSkinWindow showSkin => _cachedShowSkinWindow ?? (_cachedShowSkinWindow = new ShowSkinWindow(UIWindowType.SkinShow));

	public RuleWindow rule => _cachedRuleWindow ?? (_cachedRuleWindow = new RuleWindow(UIWindowType.Rule));

	public InviteWindow invite => _cachedInviteWindow ?? (_cachedInviteWindow = new InviteWindow(UIWindowType.Invite));

	public InputWindow input => _cachedInputWindow ?? (_cachedInputWindow = new InputWindow(UIWindowType.Input));

	public ChatWindow chat => _cachedChatWindow ?? (_cachedChatWindow = new ChatWindow(UIWindowType.Chat));

	public ActivityPopupWindow activityPopup => _cachedActivityPopupWindow ?? (_cachedActivityPopupWindow = new ActivityPopupWindow(UIWindowType.ActivityPopup));

	public GachaInfoWindow gachaInfo => _cachedGachaInfoWindow ?? (_cachedGachaInfoWindow = new GachaInfoWindow(UIWindowType.GachaInfo));

	public GuideWindow guide => _cachedGuideWindow ?? (_cachedGuideWindow = new GuideWindow(UIWindowType.Guide));

	public SelectTutorialWindow selectTutorial => _cachedSelectTutorialWindow ?? (_cachedSelectTutorialWindow = new SelectTutorialWindow(UIWindowType.SelectTutorial));

	public InfoWindow info => _cachedInfoWindow ?? (_cachedInfoWindow = new InfoWindow(UIWindowType.Info));

	public RoomFilterWindow roomFilter => _cachedRoomFilterWindow ?? (_cachedRoomFilterWindow = new RoomFilterWindow(UIWindowType.RoomFilter));

	public NGOStoreWindow ngoStore => _cachedNGOStoreWindow ?? (_cachedNGOStoreWindow = new NGOStoreWindow(UIWindowType.Ngostore));

	public WitchWeaponWindow witchWeapon => _cachedWitchWeaponWindow ?? (_cachedWitchWeaponWindow = new WitchWeaponWindow(UIWindowType.WitchWeapon));

	public MGWTStoreWindow mgwtStoreWindow => _cachedMGWTStoreWindow ?? (_cachedMGWTStoreWindow = new MGWTStoreWindow(UIWindowType.Mgwtstore));

	public SignInWindow signInWindow => _cachedSignInWindow ?? (_cachedSignInWindow = new SignInWindow(UIWindowType.SignIn));

	public PlayerRenameWindow PlayerRenameWindow => _playerRenameWindow ?? (_playerRenameWindow = new PlayerRenameWindow(UIWindowType.PlayerRename));

	public DiscountWindow discount => _cachedDiscountWindow ?? (_cachedDiscountWindow = new DiscountWindow(UIWindowType.Discount));

	public LoadingWindow loading => _cachedLoadingWindow ?? (_cachedLoadingWindow = new LoadingWindow(UIWindowType.Loading));

	public DisplayCardWindow displayCard => _cachedDisplayCardWindow ?? (_cachedDisplayCardWindow = new DisplayCardWindow(UIWindowType.DisplayCard));

	public SkinSellWindow skinSell => _cachedSkinSellWindow ?? (_cachedSkinSellWindow = new SkinSellWindow(UIWindowType.SkinSell));

	public VA11HallAWindow VA11HallA => _cachedVA11HallAWindow ?? (_cachedVA11HallAWindow = new VA11HallAWindow(UIWindowType.Va11HallA));

	public MatchSuccessWindow matchSuccess => _cachedMatchSuccessWindow ?? (_cachedMatchSuccessWindow = new MatchSuccessWindow(UIWindowType.MatchSuccess));

	protected override void InstanceInit()
	{
		base.InstanceInit();
		UIObjectFactory.SetLoaderExtension(typeof(TextureLoader));
		GRoot.inst.SetContentScaleFactor(1920, 1080, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
		Stage.inst.onTouchBegin.AddCapture(OnGlobalTouchBegin);
	}

	protected override void OnDestroyInstance()
	{
		Stage.inst.onTouchBegin.RemoveCapture(OnGlobalTouchBegin);
		base.OnDestroyInstance();
	}

	private void OnGlobalTouchBegin(EventContext context)
	{
		SimpleSingletonProvider<GameLogicManager>.inst?.room?.TrackInvalidFrequentClick();
	}

	public async UniTask AsyncInit()
	{
		await LoadFairyGUIContent();
		await CommonUIManager.RegisterCommonPackage();
		await AsyncLoadFonts();
	}

	public void TryHideWindows(bool force = false)
	{
		if (force)
		{
			BaseWindow result;
			while (propUpWindows.TryPop(out result))
			{
				result.Hide();
			}
			return;
		}
		_unclearedWindows.Clear();
		BaseWindow result2;
		while (propUpWindows.TryPop(out result2))
		{
			if (!result2.TryHide())
			{
				_unclearedWindows.Add(result2);
			}
		}
		for (int num = _unclearedWindows.Count - 1; num >= 0; num--)
		{
			propUpWindows.Push(_unclearedWindows[num]);
		}
		_unclearedWindows.Clear();
	}

	public async UniTask AsyncLoadFonts()
	{
		UnloadTMPFonts();
		HashSet<string> runFontNames = new HashSet<string>();
		foreach (FontInfoConfigure info in StaticConfigure.Font.Infos)
		{
			string local = GetLocal(info);
			if (info.Name.Contains("_TMP") && GetLocal(info).Contains("_TMP"))
			{
				MyTMPFont TMPFont = new MyTMPFont(info.Name);
				await TMPFont.AsyncLoad(local);
				FontManager.RegisterFont(TMPFont);
			}
			else
			{
				runFontNames.Add(local);
				MyDynamicFont myDynamicFont = new MyDynamicFont(info.Name);
				myDynamicFont.AsyncLoad(local);
				FontManager.RegisterFont(myDynamicFont);
			}
		}
		SystemConfig.ReleaseInvalidFontAsset(runFontNames);
	}

	public string GetLocal(FontInfoConfigure config)
	{
		return GameSettings.GetDataForLanguage(config.LoadedKeyEN, config.LoadedKeyJP, config.LoadedKeyCN, config.LoadedKeyCHT);
	}

	private void UnloadTMPFonts()
	{
		FontManager.Clear();
	}

	private static async UniTask LoadFairyGUIContent()
	{
		if (!StaticConfigure.Settings.LanguageDict.TryGetValue((int)GameSettings.languageType, out var value))
		{
			Debug.LogError("找不到当前语言配置的数据");
			return;
		}
		AsyncOperationHandle<TextAsset> handle = await AddressableHelper.LoadAssetAsync<TextAsset>(value.FileContentKey);
		XML stringsSource = new XML(handle.Result.text);
		Addressables.Release(handle);
		UIPackage.SetStringsSource(stringsSource);
	}

	private async UniTask<IBasePanel> AsyncLoadPanel(UIPanelType panelType)
	{
		if (!_cachedPanelDict.TryGetValue(panelType, out var value))
		{
			UIPanelConfigure config = UIHelper.GetPanelConfig(panelType);
			if (config != null)
			{
				await UIPackage.AddPackageAsync(config.PackageName, string.Empty);
				value = Activator.CreateInstance(Type.GetType("UI." + config.PackageName + "Panel", throwOnError: true), config) as IBasePanel;
				if (value == null)
				{
					throw new Exception("不存在" + panelType.ToString() + "对应的UI脚本:UI." + config.PackageName + "Panel");
				}
				_cachedPanelDict.Add(panelType, value);
			}
		}
		return value;
	}

	public async UniTask<IBasePanel> OpenPanel(UIPanelType panelType, params object[] objs)
	{
		if (currentPanel != null && currentPanel.config.PanelType == panelType)
		{
			return currentPanel;
		}
		if (_cachedPanelDict.TryGetValue(panelType, out var panel))
		{
			await ShowPanel(panel, objs);
		}
		else
		{
			panel = await AsyncLoadPanel(panelType);
			if (panel == null)
			{
				return null;
			}
			await ShowPanel(panel, objs);
		}
		signal.showPanel.Dispatch(panelType);
		return panel;
	}

	private async UniTask ShowPanel(IBasePanel panel, object[] objs)
	{
		if (_panelStack.Count > 0 && panel.config.NeedClosePrevious)
		{
			_panelStack.Peek().Close();
		}
		_panelStack.Push(panel);
		currentPanel = panel;
		await DealPublicPanel(panel, objs);
		panel.Show(objs);
	}

	public async UniTask ReturnToLastPanel(IBasePanel closePanel, params object[] objs)
	{
		if (closePanel != null && currentPanel == closePanel && _panelStack.Count != 0 && !_panelStack.Peek().config.IsRootInScene)
		{
			_panelStack.Pop().Close();
			IBasePanel peekPanel2 = (currentPanel = _panelStack.Peek());
			await DealPublicPanel(peekPanel2, objs);
			peekPanel2.Show(objs);
		}
	}

	public async UniTask ReturnHomePanelDirectly()
	{
		if (_panelStack.Count == 0 || SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Home || currentPanel.config.PanelType == UIPanelType.Home)
		{
			return;
		}
		foreach (IBasePanel item in _panelStack)
		{
			if (item.IsOpen() && item.config.PanelType != UIPanelType.Home)
			{
				item.Close();
			}
		}
		_panelStack.Clear();
		await OpenPanel(UIPanelType.Home);
	}

	public async UniTask ReturnThePanelDirectlyInHomeScene(UIPanelType openType, object[] openParams, params UIPanelType[] loadTypes)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Home)
		{
			return;
		}
		foreach (IBasePanel item3 in _panelStack)
		{
			if (item3.IsOpen() && item3.config.PanelType != UIPanelType.Home)
			{
				item3.Close();
			}
		}
		_panelStack.Clear();
		IBasePanel item = await AsyncLoadPanel(UIPanelType.Home);
		_panelStack.Push(item);
		foreach (UIPanelType panelType in loadTypes)
		{
			IBasePanel item2 = await AsyncLoadPanel(panelType);
			_panelStack.Push(item2);
		}
		await OpenPanel(openType, openParams);
	}

	public async UniTask OpenActivityHub(int activityId)
	{
		if (activityHubPanel == null)
		{
			activityHubPanel = await AsyncLoadPanel(UIPanelType.ActivityHub);
		}
		if (!activityHubPanel.IsOpen())
		{
			activityHubPanel.Show(new RepeatedField<int> { activityId });
		}
	}

	private async UniTask DealPublicPanel(IBasePanel panel, object[] objs)
	{
		if (panel.config.NeedBackgroundUI)
		{
			await TryShowBackground();
		}
		else if (backgroundPanel != null && backgroundPanel.IsOpen())
		{
			backgroundPanel.Close();
		}
		if (panel.config.NeedBottomMenu)
		{
			if (bottomMenuPanel == null)
			{
				bottomMenuPanel = await AsyncLoadPanel(UIPanelType.BottomMenu);
			}
			if (!bottomMenuPanel.IsOpen())
			{
				bottomMenuPanel.Show();
			}
			else
			{
				bottomMenuPanel.Refresh();
			}
		}
		else if (bottomMenuPanel != null && bottomMenuPanel.IsOpen())
		{
			bottomMenuPanel.Close();
		}
		if (panel.config.CloseActivityHub)
		{
			activityHubPanel?.Close();
		}
		else if (panel.config.PanelType != UIPanelType.ActivityHub)
		{
			if (activityHubPanel == null)
			{
				activityHubPanel = await AsyncLoadPanel(UIPanelType.ActivityHub);
			}
			if (!activityHubPanel.IsOpen())
			{
				activityHubPanel.Show(objs);
			}
		}
	}

	public async UniTask TryShowBackground()
	{
		if (backgroundPanel == null)
		{
			backgroundPanel = await AsyncLoadPanel(UIPanelType.Background);
		}
		if (!backgroundPanel.IsOpen())
		{
			backgroundPanel.Show();
		}
	}

	public void UnloadWhenEnterNewScene()
	{
		currentPanel = null;
		UnLoadPanel();
		UnLoadPublicPanel();
		UnLoadWin();
		SimpleSingletonProvider<CriMovieManager>.inst.Clear();
		SimpleSingletonProvider<TextureManager>.inst.Clear();
		SimpleSingletonProvider<GameObjectManager>.inst.Clear();
		SimpleSingletonProvider<RenderTextureManager>.inst.ClearCache();
	}

	public void UnLoadPanel()
	{
		List<UIPanelType> list = new List<UIPanelType>();
		foreach (KeyValuePair<UIPanelType, IBasePanel> item in _cachedPanelDict)
		{
			item.Value.Dispose();
			list.Add(item.Key);
		}
		foreach (UIPanelType item2 in list)
		{
			_cachedPanelDict.Remove(item2);
		}
		_panelStack.Clear();
	}

	private void UnLoadPublicPanel()
	{
		backgroundPanel?.Dispose();
		backgroundPanel = null;
		bottomMenuPanel?.Dispose();
		bottomMenuPanel = null;
		activityHubPanel?.Dispose();
		activityHubPanel = null;
	}

	public void Clear()
	{
		currentPanel = null;
		UnloadWhenEnterNewScene();
		CommonUIManager.RemoveCommonPackage();
		UnloadTMPFonts();
	}

	public void SwitchCoverMode(bool inCoverMode)
	{
		foreach (KeyValuePair<UIPanelType, IBasePanel> item in _cachedPanelDict)
		{
			if (item.Value.IsOpen())
			{
				item.Value.CoverMode(inCoverMode);
			}
		}
	}

	public void SwitchAdultMode(bool inAdultMode)
	{
		foreach (KeyValuePair<UIPanelType, IBasePanel> item in _cachedPanelDict)
		{
			if (item.Value.IsOpen())
			{
				item.Value.AdultMode(inAdultMode);
			}
		}
	}

	public bool GoWayAvailable(int way)
	{
		if (!StaticConfigure.Way.DataDict.TryGetValue(way, out var value))
		{
			return false;
		}
		if (StaticConfigure.Way.InfoDict.TryGetValue((int)value.WayType, out var value2))
		{
			if (value2.PanelType == UIPanelType.None && value2.WindowType == UIWindowType.None)
			{
				return false;
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.activity.CheckWayTypeIsActivity(value2.WayType))
			{
				if (StaticConfigure.Activity.InfoDict.TryGetValue(value.WayParam[0], out var value3))
				{
					return SimpleSingletonProvider<GameLogicManager>.inst.activity.AdjustActivity(value3);
				}
				return false;
			}
			if (value2.WayType == WayType.Anniversary2Nd)
			{
				return SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinComboInfo()?.CheckValidWay(value.WayParam[0]) ?? false;
			}
			if (value2.WayType == WayType.Gacha)
			{
				if (StaticConfigure.Gacha.BackstageDict.TryGetValue(value.WayParam[0], out var value4))
				{
					return SimpleSingletonProvider<GameLogicManager>.inst.gacha.BackstageAvailable(value4);
				}
				return false;
			}
			if (value2.WayType == WayType.BattlePass)
			{
				BattlePassData battlePassData = SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData;
				if (battlePassData != null)
				{
					return battlePassData.BattlePassInfo.Id == value.WayParam[0];
				}
				return false;
			}
			if (value2.WayType == WayType.Collaboration && value.WayParam.Count > 0)
			{
				CollaborationInfoConfigure collaborationInfoConfigure = SimpleSingletonProvider<GameLogicManager>.inst.collaborate.TryGetCollaboration();
				if (collaborationInfoConfigure != null)
				{
					return value.WayParam[0] == collaborationInfoConfigure.Id;
				}
				return false;
			}
			if (value2.WayType == WayType.SkinBundlingSales && value.WayParam.Count > 0)
			{
				return SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinComboInfo()?.CheckValidWay(value.WayParam[0]) ?? false;
			}
			WayType wayType = value2.WayType;
			if ((wayType == WayType.Mall || wayType == WayType.GiftPackageStore) && value.WayParam.Count > 1)
			{
				ShopTabType showType = (ShopTabType)value.WayParam[0];
				int goodsId = value.WayParam[1];
				return SimpleSingletonProvider<GameLogicManager>.inst.store.CheckGoodsPurchaseLicense(showType, goodsId);
			}
			return true;
		}
		return false;
	}

	public async UniTask GoWayPanel(int way)
	{
		if (!StaticConfigure.Way.DataDict.TryGetValue(way, out var value))
		{
			return;
		}
		WayInfoConfigure value2;
		if (value.WayType == WayType.ActivitySinglePlayer)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.EnterSinglePlayer();
		}
		else if (StaticConfigure.Way.InfoDict.TryGetValue((int)value.WayType, out value2))
		{
			if (value2.PanelType != UIPanelType.None)
			{
				await GoTargetPanel(value2.PanelType, value.WayParam, value.WayType);
			}
			else if (value2.WindowType != UIWindowType.None)
			{
				await GoWindow(value2.WindowType, value.WayParam, value.WayType);
			}
		}
	}

	public async UniTask GoTargetPanel(UIPanelType PanelType, RepeatedField<int> WayParam, WayType WayType)
	{
		if (WayType == WayType.BondForged || WayType == WayType.FriendshipLevel || WayType == WayType.CharacterProfile)
		{
			await OpenPanel(PanelType, WayParam, WayType);
		}
		else if (currentPanel.config.PanelType == PanelType)
		{
			if (currentPanel.config.PanelType == UIPanelType.Store && currentPanel is StorePanel storePanel)
			{
				storePanel.SwitchMustShowTab(WayParam);
			}
		}
		else
		{
			await OpenPanel(PanelType, WayParam);
		}
	}

	public async UniTask GoWindow(UIWindowType WindowType, RepeatedField<int> WayParam, WayType WayType)
	{
		switch (WindowType)
		{
		case UIWindowType.ActivityPopup:
			await activityPopup.ShowActivity(WayParam[0]);
			break;
		case UIWindowType.Ngostore:
		{
			CollaborationInfoConfigure collaborationInfoConfigure = WayParam[0].GetCollaborationInfoConfigure();
			await ngoStore.ShowNGOHero(collaborationInfoConfigure);
			break;
		}
		case UIWindowType.Mgwtstore:
		{
			CollaborationInfoConfigure collaborationInfoConfigure3 = WayParam[0].GetCollaborationInfoConfigure();
			await mgwtStoreWindow.ShowMGWTStore(collaborationInfoConfigure3);
			break;
		}
		case UIWindowType.WitchWeapon:
			await witchWeapon.ShowWitchWeaponSkin(WayParam[0]);
			break;
		case UIWindowType.Va11HallA:
		{
			CollaborationInfoConfigure collaborationInfoConfigure2 = WayParam[0].GetCollaborationInfoConfigure();
			await VA11HallA.ShowVA11HallASkin(collaborationInfoConfigure2);
			break;
		}
		case UIWindowType.SkinSell:
		{
			SkinSellData skinComboInfo2 = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinComboInfo();
			if (skinComboInfo2 != null)
			{
				await skinSell.ShowSkinSell(skinComboInfo2);
			}
			break;
		}
		case UIWindowType.Anniversary2Nd:
		{
			SkinSellData skinComboInfo = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinComboInfo();
			if (skinComboInfo != null)
			{
				await Anniversary_2nd.ShowSkinSell(skinComboInfo);
			}
			break;
		}
		}
	}

	public void ShowTipsBeforeGacha(int itemId, int count, Action cb)
	{
		if (!LocalCache.GetGachaTipsStatus())
		{
			cb?.Invoke();
			return;
		}
		ItemInfoConfigure itemInfoConfigure = itemId.GetItemInfoConfigure();
		if (itemInfoConfigure != null)
		{
			messageBox.ShowOKCancel(string.Format(1073.GetLocal(UIStringType.Message), count, itemInfoConfigure.NameID.GetLocal(UIStringType.Item)), 1072, delegate
			{
				LocalCache.UpdateGachaTipsStatus(messageBox.GetDoubleStatus());
				cb?.Invoke();
			}, delegate
			{
				LocalCache.UpdateGachaTipsStatus(messageBox.GetDoubleStatus());
			}).Forget();
		}
	}

	public void ShowTipsBeforeGacha(List<int> itemIds, int count, Action cb)
	{
		if (!LocalCache.GetGachaTipsStatus())
		{
			cb?.Invoke();
		}
		else
		{
			if (itemIds == null || itemIds.Count == 0)
			{
				return;
			}
			if (itemIds.Count == 1)
			{
				ShowTipsBeforeGacha(itemIds[0], count, cb);
				return;
			}
			string text = "";
			for (int i = 0; i < itemIds.Count; i++)
			{
				int num = itemIds[i];
				if (num == 0)
				{
					continue;
				}
				ItemInfoConfigure itemInfoConfigure = num.GetItemInfoConfigure();
				if (itemInfoConfigure == null)
				{
					continue;
				}
				int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(num);
				if (itemCount > 0 || i == itemIds.Count - 1)
				{
					int num2 = ((count < itemCount) ? count : itemCount);
					count -= num2;
					if (i == itemIds.Count - 1 && count > 0)
					{
						num2 += count;
					}
					if (num2 > 0)
					{
						text = ((!(text == "")) ? $"{text}{string.Format(1134.GetLocal(UIStringType.Message), num2, itemInfoConfigure.NameID.GetLocal(UIStringType.Item))}" : $"{text}{string.Format(1133.GetLocal(UIStringType.Message), num2, itemInfoConfigure.NameID.GetLocal(UIStringType.Item))}");
					}
				}
			}
			messageBox.ShowOKCancel(string.Format(1132.GetLocal(UIStringType.Message), text), 1072, delegate
			{
				LocalCache.UpdateGachaTipsStatus(messageBox.GetDoubleStatus());
				cb?.Invoke();
			}, delegate
			{
				LocalCache.UpdateGachaTipsStatus(messageBox.GetDoubleStatus());
			}).Forget();
		}
	}

	private void UnLoadWin()
	{
		propUpWindows.Clear();
		_cachedSurveyCenterWindow?.Dispose();
		_cachedSurveyCenterWindow = null;
		_cachedReplayWindow?.Dispose();
		_cachedReplayWindow = null;
		_cachedExpressionListWindow?.Dispose();
		_cachedExpressionListWindow = null;
		_cachedCreditWarningWindow?.Dispose();
		_cachedCreditWarningWindow = null;
		_cachedChooseRoundCardWindow?.Dispose();
		_cachedChooseRoundCardWindow = null;
		_cachedRoomTermsWindow?.Dispose();
		_cachedRoomTermsWindow = null;
		_cachedBattleLandTipWindow?.Dispose();
		_cachedBattleLandTipWindow = null;
		_cachedLuckyStarMissionWindow?.Dispose();
		_cachedLuckyStarMissionWindow = null;
		_cachedBattleVideoWindow?.Dispose();
		_cachedBattleVideoWindow = null;
		_cachedNewGameLibraryWindow?.Dispose();
		_cachedNewGameLibraryWindow = null;
		_cachedAnniversary_2ndWindow?.Dispose();
		_cachedAnniversary_2ndWindow = null;
		_cachedDouYinRewardWindow?.Dispose();
		_cachedDouYinRewardWindow = null;
		_cachedFriendLeaderboardWindow?.Dispose();
		_cachedFriendLeaderboardWindow = null;
		_cachedRookieTaskInfoWindow?.Dispose();
		_cachedRookieTaskInfoWindow = null;
		_cachedMatchInfoWindow?.Dispose();
		_cachedMatchInfoWindow = null;
		_cachedGachaRookieRewardWindow?.Dispose();
		_cachedGachaRookieRewardWindow = null;
		_cachedSinglePlayerSettingInBattleWindow?.Dispose();
		_cachedSinglePlayerSettingInBattleWindow = null;
		_cachedTipsWindow?.Dispose();
		_cachedTipsWindow = null;
		_cachedMessageBoxWindow?.Dispose();
		_cachedMessageBoxWindow = null;
		_cachedCardWindow?.Dispose();
		_cachedCardWindow = null;
		_cachedBattlePlayerInfoWindow?.Dispose();
		_cachedBattlePlayerInfoWindow = null;
		_cacheSkillWindow?.Dispose();
		_cacheSkillWindow = null;
		_cachedGMWindow?.Dispose();
		_cachedGMWindow = null;
		_cachedGameInfoWindow?.Dispose();
		_cachedGameInfoWindow = null;
		_cachedSystemTipsWindow?.Dispose();
		_cachedSystemTipsWindow = null;
		_cachedFightWindow?.Dispose();
		_cachedFightWindow = null;
		_cacheCardWindow?.Dispose();
		_cacheCardWindow = null;
		_cachedLandBatteryWindow?.Dispose();
		_cachedLandBatteryWindow = null;
		_cachedLandDivinationWindow?.Dispose();
		_cachedLandDivinationWindow = null;
		_cachedLandEventWindow?.Dispose();
		_cachedLandEventWindow = null;
		_cachedLandFillingStationWindow?.Dispose();
		_cachedLandFillingStationWindow = null;
		_cachedLandGambleWindow?.Dispose();
		_cachedLandGambleWindow = null;
		_cachedLandHospitalWindow?.Dispose();
		_cachedLandHospitalWindow = null;
		_cachedLandPursuitWindow?.Dispose();
		_cachedLandPursuitWindow = null;
		_cachedLandRollGoldWindow?.Dispose();
		_cachedLandRollGoldWindow = null;
		_cachedLandShopWindow?.Dispose();
		_cachedLandShopWindow = null;
		_cachedLandLotteryWindow?.Dispose();
		_cachedLandLotteryWindow = null;
		_cachedExpressionWindow?.Dispose();
		_cachedExpressionWindow = null;
		_cachedUpgradeWindow?.Dispose();
		_cachedUpgradeWindow = null;
		_cachedExplainWindow?.Dispose();
		_cachedExplainWindow = null;
		_cachedPropDetailWindow?.Dispose();
		_cachedPropDetailWindow = null;
		_cachedPurchaseWindow?.Dispose();
		_cachedPurchaseWindow = null;
		_cachedBoxPropWindow?.Dispose();
		_cachedBoxPropWindow = null;
		_cachedRechargeTipWindow?.Dispose();
		_cachedRechargeTipWindow = null;
		_cachedAccountInfoWindow?.Dispose();
		_cachedAccountInfoWindow = null;
		_cachedShowSkinWindow?.Dispose();
		_cachedShowSkinWindow = null;
		_cachedRewardWindow?.Dispose();
		_cachedRewardWindow = null;
		_cachedRuleWindow?.Dispose();
		_cachedRuleWindow = null;
		_cachedInputWindow?.Dispose();
		_cachedInputWindow = null;
		_cachedInviteWindow?.Dispose();
		_cachedInviteWindow = null;
		_cachedSettingWindow?.Dispose();
		_cachedSettingWindow = null;
		_cachedChatWindow?.Dispose();
		_cachedChatWindow = null;
		_cachedRelicWindow?.Dispose();
		_cachedRelicWindow = null;
		_cachedATMWindow?.Dispose();
		_cachedATMWindow = null;
		_cachedActivityPopupWindow?.Dispose();
		_cachedActivityPopupWindow = null;
		_cachedBattleRelicInfoWindow?.Dispose();
		_cachedBattleRelicInfoWindow = null;
		_cachedGachaInfoWindow?.Dispose();
		_cachedGachaInfoWindow = null;
		_cachedGuideWindow?.Dispose();
		_cachedGuideWindow = null;
		_cachedSelectTutorialWindow?.Dispose();
		_cachedSelectTutorialWindow = null;
		_cachedInfoWindow?.Dispose();
		_cachedInfoWindow = null;
		_cacheLandPrayWindow?.Dispose();
		_cacheLandPrayWindow = null;
		_cachedRoomFilterWindow?.Dispose();
		_cachedRoomFilterWindow = null;
		_cachedBattleSelectMonsterWindow?.Dispose();
		_cachedBattleSelectMonsterWindow = null;
		_cachedBattlePreMonsterWindow?.Dispose();
		_cachedBattlePreMonsterWindow = null;
		_cachedNGOStoreWindow?.Dispose();
		_cachedNGOStoreWindow = null;
		_cachedWitchWeaponWindow?.Dispose();
		_cachedWitchWeaponWindow = null;
		_cachedMGWTStoreWindow?.Dispose();
		_cachedMGWTStoreWindow = null;
		_cacheLandRelicWindow?.Dispose();
		_cacheLandRelicWindow = null;
		_cachedSignInWindow?.Dispose();
		_cachedSignInWindow = null;
		_playerRenameWindow?.Dispose();
		_playerRenameWindow = null;
		_cachedDiscountWindow?.Dispose();
		_cachedDiscountWindow = null;
		_cachedOperateTimeWindow?.Dispose();
		_cachedOperateTimeWindow = null;
		_cachedDisplayCardWindow?.Dispose();
		_cachedDisplayCardWindow = null;
		_cachedStoryWindow?.Dispose();
		_cachedStoryWindow = null;
		UnLoadBattleHint();
		_cachedSkinSellWindow?.Dispose();
		_cachedSkinSellWindow = null;
		_cachedVA11HallAWindow?.Dispose();
		_cachedVA11HallAWindow = null;
		_cacheLandVendorWindow?.Dispose();
		_cacheLandVendorWindow = null;
		_cachedMatchSuccessWindow?.Dispose();
		_cachedMatchSuccessWindow = null;
		_cachedSettingInBattle?.Dispose();
		_cachedSettingInBattle = null;
		_cachedSettingList?.Dispose();
		_cachedSettingList = null;
		_cachedTutorialWindow?.Dispose();
		_cachedTutorialWindow = null;
		UnLoadAssistVote();
	}

	public void UnLoadAssistVote()
	{
		_cachedAssistVoteWindow?.Dispose();
		_cachedAssistVoteWindow = null;
		_cachedAssistVoteS7Window?.Dispose();
		_cachedAssistVoteS7Window = null;
	}

	public void UnLoadBattleHint()
	{
		_cachedBattleHintWindow?.Dispose();
		_cachedBattleHintWindow = null;
	}

	public void HideNewGameLibrary()
	{
		_cachedNewGameLibraryWindow?.Dispose();
		_cachedNewGameLibraryWindow = null;
	}

	public void UnLoadLoadingTip()
	{
		_cachedLoadingTipWindow?.Dispose();
		_cachedLoadingTipWindow = null;
	}

	public void UnLoadGlobalUI()
	{
		_cachedNoticeWindow?.Dispose();
		_cachedNoticeWindow = null;
		_cachedLoadingWindow?.Dispose();
		_cachedLoadingWindow = null;
		UnLoadLoadingTip();
	}

	public void CloseAllUnFightWin()
	{
		_cachedSurveyCenterWindow?.Hide();
		_cachedCreditWarningWindow?.Hide();
		_cachedChooseRoundCardWindow?.Hide();
		_cachedRoomTermsWindow?.Hide();
		_cachedBattleLandTipWindow?.Hide();
		_cachedLuckyStarMissionWindow?.Hide();
		_cachedBattleVideoWindow?.Hide();
		_cachedAnniversary_2ndWindow?.Hide();
		_cachedDouYinRewardWindow?.Hide();
		_cachedFriendLeaderboardWindow?.Hide();
		_cachedRookieTaskInfoWindow?.Hide();
		_cachedMatchInfoWindow?.Hide();
		_cachedGachaRookieRewardWindow?.Hide();
		_cachedAssistVoteS7Window?.Hide();
		_cachedSinglePlayerSettingInBattleWindow?.Hide();
		_cachedNoticeWindow?.Hide();
		_cachedMessageBoxWindow?.Hide();
		_cachedCardWindow?.Hide();
		_cacheSkillWindow?.Hide();
		_cachedLandBatteryWindow?.Hide();
		_cachedLandDivinationWindow?.Hide();
		_cachedLandEventWindow?.Hide();
		_cachedLandFillingStationWindow?.Hide();
		_cachedLandGambleWindow?.Hide();
		_cachedLandHospitalWindow?.Hide();
		_cachedLandPursuitWindow?.Hide();
		_cachedLandRollGoldWindow?.Hide();
		_cachedLandShopWindow?.Hide();
		_cachedLandLotteryWindow?.Hide();
		_cachedRelicWindow?.Hide();
		_cachedATMWindow?.Hide();
		_cachedChatWindow?.Hide();
		_cachedSettingWindow?.Hide();
		_cachedInviteWindow?.Hide();
		_cachedInputWindow?.Hide();
		_cachedRuleWindow?.Hide();
		_cachedRewardWindow?.Hide();
		_cachedActivityPopupWindow?.Hide();
		_cachedBattleRelicInfoWindow?.Hide();
		_cacheLandPrayWindow?.Hide();
		_cachedRoomFilterWindow?.Hide();
		_cachedBattlePreMonsterWindow?.Hide();
		_cachedBattleSelectMonsterWindow?.Hide();
		_cachedNGOStoreWindow?.Hide();
		_cachedWitchWeaponWindow?.Hide();
		_cachedMGWTStoreWindow?.Hide();
		_cacheLandRelicWindow?.Hide();
		_cachedSignInWindow?.Hide();
		_cachedDiscountWindow?.Hide();
		_cachedDisplayCardWindow?.Hide();
		_cachedStoryWindow?.Hide();
		_cachedAssistVoteWindow?.Hide();
		_playerRenameWindow?.Hide();
		_cachedBattleHintWindow?.Hide();
		_cachedSkinSellWindow?.Hide();
		_cacheLandVendorWindow?.Hide();
	}
}
