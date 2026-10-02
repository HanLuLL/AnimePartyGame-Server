using GameLogic;
using Tools;

namespace Core;

public static class LocalStore
{
	public static readonly string MasterVolumeLabel = "MasterVolume";

	public static readonly string BGMVolumeLabel = "BGMVolume";

	public static readonly string SFXVolumeLabel = "SFXVolume";

	public static readonly string VoiceVolumeLabel = "VoiceVolume";

	public static readonly string TipsVibrateLabel = "TipsVibrate";

	public static readonly string MessageVibrateLabel = "MessageVibrate";

	public static readonly string ResolutionTypeLabel = "ResolutionTypeV1";

	public static readonly string LanguageTypeLabel = "LanguageType";

	public static readonly string FrameTypeLabel = "FrameType";

	public static readonly string WindowTypeLabel = "WindowType";

	public static readonly string QualityTypeLabel = "QualityTypeLabel";

	public static readonly string EnergySavingLabel = "EnergySavingLabel";

	public static readonly string VSyncCountLabel = "VSyncCountLabel";

	public static readonly string VoiceLanguageLabel = "VoiceLanguageLabel";

	public static readonly string coverModeLabel = "CoverModel";

	public static readonly string angelModeLabel = "AdultModel";

	public static readonly string MouseControlLabel = "MouseControl";

	public static readonly string KeyControlLabel = "KeyControl";

	public static readonly string FreeCameraLabel = "FreeCamera";

	public static readonly string userAgreeLabel = "UserAgree";

	public static readonly string userAgreeVersionLabel = "UserAgreeVersion";

	public static readonly string privacyPolicysVersionLabel = "PrivacyPolicysVersion";

	public static readonly string PVEDifficulty = "PVEDifficultyV3";

	public static readonly string SurveyCache = "SurveyCache_" + GetCurPlayerId();

	public static string StoreGoodsCache => GetCurPlayerName() + "StoreGoodsV1";

	public static string ActivityDoubleStatusCache => GetCurPlayerName() + "ActivityDoubleStatusV1";

	public static string ComebackEndTimeCache => GetCurPlayerName() + "ComebackEndTimeV1";

	public static string HeroSortCache => GetCurPlayerId() + "HeroSortV2";

	public static string HeroSortOrderCache => GetCurPlayerId() + "HeroSortOrderV1";

	public static string GachaCostTip => GetCurPlayerName() + "GachaCostTip";

	public static string GachaSkinRewardTip => GetCurPlayerName() + "GachaSkinRewardTip";

	public static string GachaSkinFinishTip => GetCurPlayerName() + "GachaSkinFinishTip";

	public static string TutorialCache => GetCurPlayerId() + "TutorialV2";

	public static string FashionDefaultKvReadCache => GetCurPlayerId() + "FashionDefaultKvReadV1";

	public static string HomeKvPositionCache => GetCurPlayerId() + "HomeKvPositionV1";

	public static string HomeKvScaleCache => GetCurPlayerId() + "HomeKvScaleV1";

	public static string NewAltArtCardCache => GetCurPlayerId() + "NewAltArtCard";

	public static string NewExpressionReceivePackCache => GetCurPlayerId() + "NewExpressionReceivePack";

	private static string GetCurPlayerName()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account == null)
		{
			return "";
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
	}

	private static string GetCurPlayerId()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account == null)
		{
			return "";
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID().ToString();
	}
}
