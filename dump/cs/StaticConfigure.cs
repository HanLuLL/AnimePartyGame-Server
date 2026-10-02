using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class StaticConfigure
{
	private static AchieveConfigure _Achieve;

	private static AcquisitionConfigure _Acquisition;

	private static AudioConfigure _Audio;

	private static BannerConfigure _Banner;

	private static BattlePassConfigure _BattlePass;

	private static BattlePassNewConfigure _BattlePassNew;

	private static BattleResourceConfigure _BattleResource;

	private static BeginTipsConfigure _BeginTips;

	private static BotConfigure _Bot;

	private static BuffConfigure _Buff;

	private static CampaignConfigure _Campaign;

	private static CardConfigure _Card;

	private static CharacterConfigure _Character;

	private static ChatConfigure _Chat;

	private static ChestConfigure _Chest;

	private static ChoosingTimeLimitConfigure _ChoosingTimeLimit;

	private static CollaborationConfigure _Collaboration;

	private static CouponsConfigure _Coupons;

	private static Day7GiftPackageConfigure _Day7GiftPackage;

	private static DestinyConfigure _Destiny;

	private static DeveloperConfigure _Developer;

	private static DiceActivityConfigure _DiceActivity;

	private static DivinationConfigure _Divination;

	private static EffectConfigure _Effect;

	private static EventConfigure _Event;

	private static FactionPointsConfigure _FactionPoints;

	private static FashionConfigure _Fashion;

	private static FavorConfigure _Favor;

	private static FontConfigure _Font;

	private static GameModeConfigure _GameMode;

	private static GlobalConfigure _Global;

	private static GuideConfigure _Guide;

	private static GuildConfigure _Guild;

	private static ImageLocalizationConfigure _ImageLocalization;

	private static ItemConfigure _Item;

	private static LandConfigure _Land;

	private static LuckyStarBattleConfigure _LuckyStarBattle;

	private static MapConfigure _Map;

	private static MapEventConfigure _MapEvent;

	private static MatchConfigure _Match;

	private static MissionConfigure _Mission;

	private static MonsterConfigure _Monster;

	private static MutatorConfigure _Mutator;

	private static PerformConfigure _Perform;

	private static PlayerConfigure _Player;

	private static ProductRecommendationConfigure _ProductRecommendation;

	private static PVEMissionConfigure _PVEMission;

	private static PVENurturanceConfigure _PVENurturance;

	private static RelicConfigure _Relic;

	private static RoundConfigure _Round;

	private static SensitiveWordsConfigure _SensitiveWords;

	private static ServerConfigure _Server;

	private static SignInConfigure _SignIn;

	private static SinglePlayerConfigure _SinglePlayer;

	private static SkillConfigure _Skill;

	private static SkinConfigure _Skin;

	private static StoryConfigure _Story;

	private static SummonConfigure _Summon;

	private static TaskConfigure _Task;

	private static TutorialConfigure _Tutorial;

	private static UIConfigure _UI;

	private static UpgradeConfigure _Upgrade;

	private static VideoConfigure _Video;

	private static WayConfigure _Way;

	private static WelfareConfigure _Welfare;

	private static ActivityConfigure _Activity;

	private static ComebackConfigure _Comeback;

	private static ExchangeStoreConfigure _ExchangeStore;

	private static FixAchieveConfigure _FixAchieve;

	private static FixBannerConfigure _FixBanner;

	private static FixBattlePassConfigure _FixBattlePass;

	private static FixCharacterConfigure _FixCharacter;

	private static FixCollaborationConfigure _FixCollaboration;

	private static FixGameModeConfigure _FixGameMode;

	private static FixGlobalConfigure _FixGlobal;

	private static FixItemConfigure _FixItem;

	private static FixMapConfigure _FixMap;

	private static FixMatchConfigure _FixMatch;

	private static FixMissionConfigure _FixMission;

	private static FixProductRecommendationConfigure _FixProductRecommendation;

	private static FixSignInConfigure _FixSignIn;

	private static GachaConfigure _Gacha;

	private static MonthlyCardConfigure _MonthlyCard;

	private static RechargeRebateConfigure _RechargeRebate;

	private static RechargeStoreConfigure _RechargeStore;

	private static RechargeStoreAdsConfigure _RechargeStoreAds;

	private static RemoveResourceConfigure _RemoveResource;

	private static SettingsConfigure _Settings;

	private static ShopTabConfigure _ShopTab;

	private static SkinSellConfigure _SkinSell;

	private static SurveyConfigure _Survey;

	private static TrialConfigure _Trial;

	private static VersionConfigure _Version;

	private static STRAchieveConfigure _STRAchieve;

	private static STRAcquisitionConfigure _STRAcquisition;

	private static STRActivityConfigure _STRActivity;

	private static STRBannerConfigure _STRBanner;

	private static STRBattlePassConfigure _STRBattlePass;

	private static STRBeginTipsConfigure _STRBeginTips;

	private static STRBotConfigure _STRBot;

	private static STRBuffConfigure _STRBuff;

	private static STRCampaignConfigure _STRCampaign;

	private static STRCardConfigure _STRCard;

	private static STRCharacterConfigure _STRCharacter;

	private static STRChatConfigure _STRChat;

	private static STRChestConfigure _STRChest;

	private static STRChoosingTimeLimitConfigure _STRChoosingTimeLimit;

	private static STRCollaborationConfigure _STRCollaboration;

	private static STRDay7GiftPackageConfigure _STRDay7GiftPackage;

	private static STRDestinyConfigure _STRDestiny;

	private static STRDialogConfigure _STRDialog;

	private static STRDiceActivityConfigure _STRDiceActivity;

	private static STRDivinationConfigure _STRDivination;

	private static STRDynamicConfigure _STRDynamic;

	private static STREventConfigure _STREvent;

	private static STRExchangeStoreConfigure _STRExchangeStore;

	private static STRFriendConfigure _STRFriend;

	private static STRGachaConfigure _STRGacha;

	private static STRGameModeConfigure _STRGameMode;

	private static STRGUIConfigure _STRGUI;

	private static STRGuildConfigure _STRGuild;

	private static STRItemConfigure _STRItem;

	private static STRLandConfigure _STRLand;

	private static STRLuckyStarBattleConfigure _STRLuckyStarBattle;

	private static STRMapConfigure _STRMap;

	private static STRMapEventConfigure _STRMapEvent;

	private static STRMatchConfigure _STRMatch;

	private static STRMessageConfigure _STRMessage;

	private static STRMissionConfigure _STRMission;

	private static STRMonsterConfigure _STRMonster;

	private static STRMonthlyCardConfigure _STRMonthlyCard;

	private static STRMutatorConfigure _STRMutator;

	private static STRPerformConfigure _STRPerform;

	private static STRPlayerConfigure _STRPlayer;

	private static STRProductRecommendationConfigure _STRProductRecommendation;

	private static STRPVEMissionConfigure _STRPVEMission;

	private static STRPVENurturanceConfigure _STRPVENurturance;

	private static STRRechargeStoreConfigure _STRRechargeStore;

	private static STRRechargeStoreAdsConfigure _STRRechargeStoreAds;

	private static STRRelicConfigure _STRRelic;

	private static STRServerConfigure _STRServer;

	private static STRSettingsConfigure _STRSettings;

	private static STRSignInConfigure _STRSignIn;

	private static STRSinglePlayerConfigure _STRSinglePlayer;

	private static STRSkillConfigure _STRSkill;

	private static STRSkinConfigure _STRSkin;

	private static STRSkinSellConfigure _STRSkinSell;

	private static STRSpectateConfigure _STRSpectate;

	private static STRStoryConfigure _STRStory;

	private static STRSummonConfigure _STRSummon;

	private static STRSurveyConfigure _STRSurvey;

	private static STRTaskConfigure _STRTask;

	private static STRTutorialConfigure _STRTutorial;

	private static STRVoiceConfigure _STRVoice;

	private static STRWayConfigure _STRWay;

	private static STRWelfareConfigure _STRWelfare;

	private static AsyncOperationHandle<TextAsset> _sensitiveWordsHandle;

	public static AchieveConfigure Achieve => _Achieve;

	public static AcquisitionConfigure Acquisition => _Acquisition;

	public static AudioConfigure Audio => _Audio;

	public static BannerConfigure Banner => _Banner;

	public static BattlePassConfigure BattlePass => _BattlePass;

	public static BattlePassNewConfigure BattlePassNew => _BattlePassNew;

	public static BattleResourceConfigure BattleResource => _BattleResource;

	public static BeginTipsConfigure BeginTips => _BeginTips;

	public static BotConfigure Bot => _Bot;

	public static BuffConfigure Buff => _Buff;

	public static CampaignConfigure Campaign => _Campaign;

	public static CardConfigure Card => _Card;

	public static CharacterConfigure Character => _Character;

	public static ChatConfigure Chat => _Chat;

	public static ChestConfigure Chest => _Chest;

	public static ChoosingTimeLimitConfigure ChoosingTimeLimit => _ChoosingTimeLimit;

	public static CollaborationConfigure Collaboration => _Collaboration;

	public static CouponsConfigure Coupons => _Coupons;

	public static Day7GiftPackageConfigure Day7GiftPackage => _Day7GiftPackage;

	public static DestinyConfigure Destiny => _Destiny;

	public static DeveloperConfigure Developer => _Developer;

	public static DiceActivityConfigure DiceActivity => _DiceActivity;

	public static DivinationConfigure Divination => _Divination;

	public static EffectConfigure Effect => _Effect;

	public static EventConfigure Event => _Event;

	public static FactionPointsConfigure FactionPoints => _FactionPoints;

	public static FashionConfigure Fashion => _Fashion;

	public static FavorConfigure Favor => _Favor;

	public static FontConfigure Font => _Font;

	public static GameModeConfigure GameMode => _GameMode;

	public static GlobalConfigure Global => _Global;

	public static GuideConfigure Guide => _Guide;

	public static GuildConfigure Guild => _Guild;

	public static ImageLocalizationConfigure ImageLocalization => _ImageLocalization;

	public static ItemConfigure Item => _Item;

	public static LandConfigure Land => _Land;

	public static LuckyStarBattleConfigure LuckyStarBattle => _LuckyStarBattle;

	public static MapConfigure Map => _Map;

	public static MapEventConfigure MapEvent => _MapEvent;

	public static MatchConfigure Match => _Match;

	public static MissionConfigure Mission => _Mission;

	public static MonsterConfigure Monster => _Monster;

	public static MutatorConfigure Mutator => _Mutator;

	public static PerformConfigure Perform => _Perform;

	public static PlayerConfigure Player => _Player;

	public static ProductRecommendationConfigure ProductRecommendation => _ProductRecommendation;

	public static PVEMissionConfigure PVEMission => _PVEMission;

	public static PVENurturanceConfigure PVENurturance => _PVENurturance;

	public static RelicConfigure Relic => _Relic;

	public static RoundConfigure Round => _Round;

	public static SensitiveWordsConfigure SensitiveWords => _SensitiveWords;

	public static ServerConfigure Server => _Server;

	public static SignInConfigure SignIn => _SignIn;

	public static SinglePlayerConfigure SinglePlayer => _SinglePlayer;

	public static SkillConfigure Skill => _Skill;

	public static SkinConfigure Skin => _Skin;

	public static StoryConfigure Story => _Story;

	public static SummonConfigure Summon => _Summon;

	public static TaskConfigure Task => _Task;

	public static TutorialConfigure Tutorial => _Tutorial;

	public static UIConfigure UI => _UI;

	public static UpgradeConfigure Upgrade => _Upgrade;

	public static VideoConfigure Video => _Video;

	public static WayConfigure Way => _Way;

	public static WelfareConfigure Welfare => _Welfare;

	public static ActivityConfigure Activity => _Activity;

	public static ComebackConfigure Comeback => _Comeback;

	public static ExchangeStoreConfigure ExchangeStore => _ExchangeStore;

	public static FixAchieveConfigure FixAchieve => _FixAchieve;

	public static FixBannerConfigure FixBanner => _FixBanner;

	public static FixBattlePassConfigure FixBattlePass => _FixBattlePass;

	public static FixCharacterConfigure FixCharacter => _FixCharacter;

	public static FixCollaborationConfigure FixCollaboration => _FixCollaboration;

	public static FixGameModeConfigure FixGameMode => _FixGameMode;

	public static FixGlobalConfigure FixGlobal => _FixGlobal;

	public static FixItemConfigure FixItem => _FixItem;

	public static FixMapConfigure FixMap => _FixMap;

	public static FixMatchConfigure FixMatch => _FixMatch;

	public static FixMissionConfigure FixMission => _FixMission;

	public static FixProductRecommendationConfigure FixProductRecommendation => _FixProductRecommendation;

	public static FixSignInConfigure FixSignIn => _FixSignIn;

	public static GachaConfigure Gacha => _Gacha;

	public static MonthlyCardConfigure MonthlyCard => _MonthlyCard;

	public static RechargeRebateConfigure RechargeRebate => _RechargeRebate;

	public static RechargeStoreConfigure RechargeStore => _RechargeStore;

	public static RechargeStoreAdsConfigure RechargeStoreAds => _RechargeStoreAds;

	public static RemoveResourceConfigure RemoveResource => _RemoveResource;

	public static SettingsConfigure Settings => _Settings;

	public static ShopTabConfigure ShopTab => _ShopTab;

	public static SkinSellConfigure SkinSell => _SkinSell;

	public static SurveyConfigure Survey => _Survey;

	public static TrialConfigure Trial => _Trial;

	public static VersionConfigure Version => _Version;

	public static STRAchieveConfigure STRAchieve => _STRAchieve;

	public static STRAcquisitionConfigure STRAcquisition => _STRAcquisition;

	public static STRActivityConfigure STRActivity => _STRActivity;

	public static STRBannerConfigure STRBanner => _STRBanner;

	public static STRBattlePassConfigure STRBattlePass => _STRBattlePass;

	public static STRBeginTipsConfigure STRBeginTips => _STRBeginTips;

	public static STRBotConfigure STRBot => _STRBot;

	public static STRBuffConfigure STRBuff => _STRBuff;

	public static STRCampaignConfigure STRCampaign => _STRCampaign;

	public static STRCardConfigure STRCard => _STRCard;

	public static STRCharacterConfigure STRCharacter => _STRCharacter;

	public static STRChatConfigure STRChat => _STRChat;

	public static STRChestConfigure STRChest => _STRChest;

	public static STRChoosingTimeLimitConfigure STRChoosingTimeLimit => _STRChoosingTimeLimit;

	public static STRCollaborationConfigure STRCollaboration => _STRCollaboration;

	public static STRDay7GiftPackageConfigure STRDay7GiftPackage => _STRDay7GiftPackage;

	public static STRDestinyConfigure STRDestiny => _STRDestiny;

	public static STRDialogConfigure STRDialog => _STRDialog;

	public static STRDiceActivityConfigure STRDiceActivity => _STRDiceActivity;

	public static STRDivinationConfigure STRDivination => _STRDivination;

	public static STRDynamicConfigure STRDynamic => _STRDynamic;

	public static STREventConfigure STREvent => _STREvent;

	public static STRExchangeStoreConfigure STRExchangeStore => _STRExchangeStore;

	public static STRFriendConfigure STRFriend => _STRFriend;

	public static STRGachaConfigure STRGacha => _STRGacha;

	public static STRGameModeConfigure STRGameMode => _STRGameMode;

	public static STRGUIConfigure STRGUI => _STRGUI;

	public static STRGuildConfigure STRGuild => _STRGuild;

	public static STRItemConfigure STRItem => _STRItem;

	public static STRLandConfigure STRLand => _STRLand;

	public static STRLuckyStarBattleConfigure STRLuckyStarBattle => _STRLuckyStarBattle;

	public static STRMapConfigure STRMap => _STRMap;

	public static STRMapEventConfigure STRMapEvent => _STRMapEvent;

	public static STRMatchConfigure STRMatch => _STRMatch;

	public static STRMessageConfigure STRMessage => _STRMessage;

	public static STRMissionConfigure STRMission => _STRMission;

	public static STRMonsterConfigure STRMonster => _STRMonster;

	public static STRMonthlyCardConfigure STRMonthlyCard => _STRMonthlyCard;

	public static STRMutatorConfigure STRMutator => _STRMutator;

	public static STRPerformConfigure STRPerform => _STRPerform;

	public static STRPlayerConfigure STRPlayer => _STRPlayer;

	public static STRProductRecommendationConfigure STRProductRecommendation => _STRProductRecommendation;

	public static STRPVEMissionConfigure STRPVEMission => _STRPVEMission;

	public static STRPVENurturanceConfigure STRPVENurturance => _STRPVENurturance;

	public static STRRechargeStoreConfigure STRRechargeStore => _STRRechargeStore;

	public static STRRechargeStoreAdsConfigure STRRechargeStoreAds => _STRRechargeStoreAds;

	public static STRRelicConfigure STRRelic => _STRRelic;

	public static STRServerConfigure STRServer => _STRServer;

	public static STRSettingsConfigure STRSettings => _STRSettings;

	public static STRSignInConfigure STRSignIn => _STRSignIn;

	public static STRSinglePlayerConfigure STRSinglePlayer => _STRSinglePlayer;

	public static STRSkillConfigure STRSkill => _STRSkill;

	public static STRSkinConfigure STRSkin => _STRSkin;

	public static STRSkinSellConfigure STRSkinSell => _STRSkinSell;

	public static STRSpectateConfigure STRSpectate => _STRSpectate;

	public static STRStoryConfigure STRStory => _STRStory;

	public static STRSummonConfigure STRSummon => _STRSummon;

	public static STRSurveyConfigure STRSurvey => _STRSurvey;

	public static STRTaskConfigure STRTask => _STRTask;

	public static STRTutorialConfigure STRTutorial => _STRTutorial;

	public static STRVoiceConfigure STRVoice => _STRVoice;

	public static STRWayConfigure STRWay => _STRWay;

	public static STRWelfareConfigure STRWelfare => _STRWelfare;

	public static async UniTask InitAsync()
	{
		Addressables.Release(await AddressableHelper.LoadAssetsAsync<TextAsset>(new List<string>(1) { "GameData_CN" }, OnConfigureLoaded));
		VerifyStaticConfig.StartFix();
		StaticGlobalData.InitData();
	}

	public static void Clear()
	{
		_Achieve = null;
		_Acquisition = null;
		_Audio = null;
		_Banner = null;
		_BattlePass = null;
		_BattlePassNew = null;
		_BattleResource = null;
		_BeginTips = null;
		_Bot = null;
		_Buff = null;
		_Campaign = null;
		_Card = null;
		_Character = null;
		_Chat = null;
		_Chest = null;
		_ChoosingTimeLimit = null;
		_Collaboration = null;
		_Coupons = null;
		_Day7GiftPackage = null;
		_Destiny = null;
		_Developer = null;
		_DiceActivity = null;
		_Divination = null;
		_Effect = null;
		_Event = null;
		_FactionPoints = null;
		_Fashion = null;
		_Favor = null;
		_Font = null;
		_GameMode = null;
		_Global = null;
		_Guide = null;
		_Guild = null;
		_ImageLocalization = null;
		_Item = null;
		_Land = null;
		_LuckyStarBattle = null;
		_Map = null;
		_MapEvent = null;
		_Match = null;
		_Mission = null;
		_Monster = null;
		_Mutator = null;
		_Perform = null;
		_Player = null;
		_ProductRecommendation = null;
		_PVEMission = null;
		_PVENurturance = null;
		_Relic = null;
		_Round = null;
		_Server = null;
		_SignIn = null;
		_SinglePlayer = null;
		_Skill = null;
		_Skin = null;
		_Story = null;
		_Summon = null;
		_Task = null;
		_Tutorial = null;
		_UI = null;
		_Upgrade = null;
		_Video = null;
		_Way = null;
		_Welfare = null;
		_Activity = null;
		_Comeback = null;
		_ExchangeStore = null;
		_FixAchieve = null;
		_FixBanner = null;
		_FixBattlePass = null;
		_FixCharacter = null;
		_FixCollaboration = null;
		_FixGameMode = null;
		_FixGlobal = null;
		_FixItem = null;
		_FixMap = null;
		_FixMatch = null;
		_FixMission = null;
		_FixProductRecommendation = null;
		_FixSignIn = null;
		_Gacha = null;
		_MonthlyCard = null;
		_RechargeRebate = null;
		_RechargeStore = null;
		_RechargeStoreAds = null;
		_RemoveResource = null;
		_Settings = null;
		_ShopTab = null;
		_SkinSell = null;
		_Survey = null;
		_Trial = null;
		_Version = null;
		_STRAchieve = null;
		_STRAcquisition = null;
		_STRActivity = null;
		_STRBanner = null;
		_STRBattlePass = null;
		_STRBeginTips = null;
		_STRBot = null;
		_STRBuff = null;
		_STRCampaign = null;
		_STRCard = null;
		_STRCharacter = null;
		_STRChat = null;
		_STRChest = null;
		_STRChoosingTimeLimit = null;
		_STRCollaboration = null;
		_STRDay7GiftPackage = null;
		_STRDestiny = null;
		_STRDialog = null;
		_STRDiceActivity = null;
		_STRDivination = null;
		_STRDynamic = null;
		_STREvent = null;
		_STRExchangeStore = null;
		_STRFriend = null;
		_STRGacha = null;
		_STRGameMode = null;
		_STRGUI = null;
		_STRGuild = null;
		_STRItem = null;
		_STRLand = null;
		_STRLuckyStarBattle = null;
		_STRMap = null;
		_STRMapEvent = null;
		_STRMatch = null;
		_STRMessage = null;
		_STRMission = null;
		_STRMonster = null;
		_STRMonthlyCard = null;
		_STRMutator = null;
		_STRPerform = null;
		_STRPlayer = null;
		_STRProductRecommendation = null;
		_STRPVEMission = null;
		_STRPVENurturance = null;
		_STRRechargeStore = null;
		_STRRechargeStoreAds = null;
		_STRRelic = null;
		_STRServer = null;
		_STRSettings = null;
		_STRSignIn = null;
		_STRSinglePlayer = null;
		_STRSkill = null;
		_STRSkin = null;
		_STRSkinSell = null;
		_STRSpectate = null;
		_STRStory = null;
		_STRSummon = null;
		_STRSurvey = null;
		_STRTask = null;
		_STRTutorial = null;
		_STRVoice = null;
		_STRWay = null;
		_STRWelfare = null;
	}

	private static void OnConfigureLoaded(TextAsset asset)
	{
		switch (asset.name)
		{
		case "Achieve":
			_Achieve = AchieveConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Acquisition":
			_Acquisition = AcquisitionConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Audio":
			_Audio = AudioConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Banner":
			_Banner = BannerConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "BattlePass":
			_BattlePass = BattlePassConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "BattlePassNew":
			_BattlePassNew = BattlePassNewConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "BattleResource":
			_BattleResource = BattleResourceConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "BeginTips":
			_BeginTips = BeginTipsConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Bot":
			_Bot = BotConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Buff":
			_Buff = BuffConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Campaign":
			_Campaign = CampaignConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Card":
			_Card = CardConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Character":
			_Character = CharacterConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Chat":
			_Chat = ChatConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Chest":
			_Chest = ChestConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "ChoosingTimeLimit":
			_ChoosingTimeLimit = ChoosingTimeLimitConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Collaboration":
			_Collaboration = CollaborationConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Coupons":
			_Coupons = CouponsConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Day7GiftPackage":
			_Day7GiftPackage = Day7GiftPackageConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Destiny":
			_Destiny = DestinyConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Developer":
			_Developer = DeveloperConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "DiceActivity":
			_DiceActivity = DiceActivityConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Divination":
			_Divination = DivinationConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Effect":
			_Effect = EffectConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Event":
			_Event = EventConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FactionPoints":
			_FactionPoints = FactionPointsConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Fashion":
			_Fashion = FashionConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Favor":
			_Favor = FavorConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Font":
			_Font = FontConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "GameMode":
			_GameMode = GameModeConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Global":
			_Global = GlobalConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Guide":
			_Guide = GuideConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Guild":
			_Guild = GuildConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "ImageLocalization":
			_ImageLocalization = ImageLocalizationConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Item":
			_Item = ItemConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Land":
			_Land = LandConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "LuckyStarBattle":
			_LuckyStarBattle = LuckyStarBattleConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Map":
			_Map = MapConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "MapEvent":
			_MapEvent = MapEventConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Match":
			_Match = MatchConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Mission":
			_Mission = MissionConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Monster":
			_Monster = MonsterConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Mutator":
			_Mutator = MutatorConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Perform":
			_Perform = PerformConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Player":
			_Player = PlayerConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "ProductRecommendation":
			_ProductRecommendation = ProductRecommendationConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "PVEMission":
			_PVEMission = PVEMissionConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "PVENurturance":
			_PVENurturance = PVENurturanceConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Relic":
			_Relic = RelicConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Round":
			_Round = RoundConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Server":
			_Server = ServerConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "SignIn":
			_SignIn = SignInConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "SinglePlayer":
			_SinglePlayer = SinglePlayerConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Skill":
			_Skill = SkillConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Skin":
			_Skin = SkinConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Story":
			_Story = StoryConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Summon":
			_Summon = SummonConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Task":
			_Task = TaskConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Tutorial":
			_Tutorial = TutorialConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "UI":
			_UI = UIConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Upgrade":
			_Upgrade = UpgradeConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Video":
			_Video = VideoConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Way":
			_Way = WayConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Welfare":
			_Welfare = WelfareConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Activity":
			_Activity = ActivityConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Comeback":
			_Comeback = ComebackConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "ExchangeStore":
			_ExchangeStore = ExchangeStoreConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixAchieve":
			_FixAchieve = FixAchieveConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixBanner":
			_FixBanner = FixBannerConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixBattlePass":
			_FixBattlePass = FixBattlePassConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixCharacter":
			_FixCharacter = FixCharacterConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixCollaboration":
			_FixCollaboration = FixCollaborationConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixGameMode":
			_FixGameMode = FixGameModeConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixGlobal":
			_FixGlobal = FixGlobalConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixItem":
			_FixItem = FixItemConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixMap":
			_FixMap = FixMapConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixMatch":
			_FixMatch = FixMatchConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixMission":
			_FixMission = FixMissionConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixProductRecommendation":
			_FixProductRecommendation = FixProductRecommendationConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "FixSignIn":
			_FixSignIn = FixSignInConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Gacha":
			_Gacha = GachaConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "MonthlyCard":
			_MonthlyCard = MonthlyCardConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "RechargeRebate":
			_RechargeRebate = RechargeRebateConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "RechargeStore":
			_RechargeStore = RechargeStoreConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "RechargeStoreAds":
			_RechargeStoreAds = RechargeStoreAdsConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "RemoveResource":
			_RemoveResource = RemoveResourceConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Settings":
			_Settings = SettingsConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "ShopTab":
			_ShopTab = ShopTabConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "SkinSell":
			_SkinSell = SkinSellConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Survey":
			_Survey = SurveyConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Trial":
			_Trial = TrialConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "Version":
			_Version = VersionConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRAchieve":
			_STRAchieve = STRAchieveConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRAcquisition":
			_STRAcquisition = STRAcquisitionConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRActivity":
			_STRActivity = STRActivityConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRBanner":
			_STRBanner = STRBannerConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRBattlePass":
			_STRBattlePass = STRBattlePassConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRBeginTips":
			_STRBeginTips = STRBeginTipsConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRBot":
			_STRBot = STRBotConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRBuff":
			_STRBuff = STRBuffConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRCampaign":
			_STRCampaign = STRCampaignConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRCard":
			_STRCard = STRCardConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRCharacter":
			_STRCharacter = STRCharacterConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRChat":
			_STRChat = STRChatConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRChest":
			_STRChest = STRChestConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRChoosingTimeLimit":
			_STRChoosingTimeLimit = STRChoosingTimeLimitConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRCollaboration":
			_STRCollaboration = STRCollaborationConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRDay7GiftPackage":
			_STRDay7GiftPackage = STRDay7GiftPackageConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRDestiny":
			_STRDestiny = STRDestinyConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRDialog":
			_STRDialog = STRDialogConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRDiceActivity":
			_STRDiceActivity = STRDiceActivityConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRDivination":
			_STRDivination = STRDivinationConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRDynamic":
			_STRDynamic = STRDynamicConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STREvent":
			_STREvent = STREventConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRExchangeStore":
			_STRExchangeStore = STRExchangeStoreConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRFriend":
			_STRFriend = STRFriendConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRGacha":
			_STRGacha = STRGachaConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRGameMode":
			_STRGameMode = STRGameModeConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRGUI":
			_STRGUI = STRGUIConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRGuild":
			_STRGuild = STRGuildConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRItem":
			_STRItem = STRItemConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRLand":
			_STRLand = STRLandConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRLuckyStarBattle":
			_STRLuckyStarBattle = STRLuckyStarBattleConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRMap":
			_STRMap = STRMapConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRMapEvent":
			_STRMapEvent = STRMapEventConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRMatch":
			_STRMatch = STRMatchConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRMessage":
			_STRMessage = STRMessageConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRMission":
			_STRMission = STRMissionConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRMonster":
			_STRMonster = STRMonsterConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRMonthlyCard":
			_STRMonthlyCard = STRMonthlyCardConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRMutator":
			_STRMutator = STRMutatorConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRPerform":
			_STRPerform = STRPerformConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRPlayer":
			_STRPlayer = STRPlayerConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRProductRecommendation":
			_STRProductRecommendation = STRProductRecommendationConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRPVEMission":
			_STRPVEMission = STRPVEMissionConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRPVENurturance":
			_STRPVENurturance = STRPVENurturanceConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRRechargeStore":
			_STRRechargeStore = STRRechargeStoreConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRRechargeStoreAds":
			_STRRechargeStoreAds = STRRechargeStoreAdsConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRRelic":
			_STRRelic = STRRelicConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRServer":
			_STRServer = STRServerConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRSettings":
			_STRSettings = STRSettingsConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRSignIn":
			_STRSignIn = STRSignInConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRSinglePlayer":
			_STRSinglePlayer = STRSinglePlayerConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRSkill":
			_STRSkill = STRSkillConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRSkin":
			_STRSkin = STRSkinConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRSkinSell":
			_STRSkinSell = STRSkinSellConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRSpectate":
			_STRSpectate = STRSpectateConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRStory":
			_STRStory = STRStoryConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRSummon":
			_STRSummon = STRSummonConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRSurvey":
			_STRSurvey = STRSurveyConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRTask":
			_STRTask = STRTaskConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRTutorial":
			_STRTutorial = STRTutorialConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRVoice":
			_STRVoice = STRVoiceConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRWay":
			_STRWay = STRWayConfigure.Parser.ParseFrom(asset.bytes);
			break;
		case "STRWelfare":
			_STRWelfare = STRWelfareConfigure.Parser.ParseFrom(asset.bytes);
			break;
		}
	}

	public static async UniTask LoadSensitiveWords()
	{
		if (_SensitiveWords != null || _sensitiveWordsHandle.IsValid())
		{
			return;
		}
		_sensitiveWordsHandle = await AddressableHelper.LoadAssetAsync<TextAsset>("Assets/GameData/SensitiveWords.bytes");
		_SensitiveWords = SensitiveWordsConfigure.Parser.ParseFrom(_sensitiveWordsHandle.Result.bytes);
		DFAAlgorithm.Init(SensitiveWords.Infos.Count);
		foreach (SensitiveWordsInfoConfigure info in SensitiveWords.Infos)
		{
			DFAAlgorithm.AddFilterWord(info.Word);
		}
	}

	public static string DealSensitiveWord(this string text)
	{
		if (!DFAAlgorithm.IsValid())
		{
			return text;
		}
		return DFAAlgorithm.StringCheckAndReplace(text);
	}

	public static void UnloadSensitiveWords()
	{
		DFAAlgorithm.Clear();
		_SensitiveWords = null;
		Addressables.Release(_sensitiveWordsHandle);
	}
}
