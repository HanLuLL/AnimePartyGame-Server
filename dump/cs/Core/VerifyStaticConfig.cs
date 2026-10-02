namespace Core;

public static class VerifyStaticConfig
{
	public static void StartFix()
	{
		StaticConfigure.Achieve.Fix(StaticConfigure.FixAchieve);
		StaticConfigure.Banner.Fix(StaticConfigure.FixBanner);
		StaticConfigure.BattlePass.Fix(StaticConfigure.FixBattlePass);
		StaticConfigure.Character.Fix(StaticConfigure.FixCharacter);
		StaticConfigure.Collaboration.Fix(StaticConfigure.FixCollaboration);
		StaticConfigure.Fashion.Fix();
		StaticConfigure.GameMode.Fix(StaticConfigure.FixGameMode);
		StaticConfigure.Item.Fix();
		StaticConfigure.ProductRecommendation.Fix(StaticConfigure.FixProductRecommendation);
		StaticConfigure.SignIn.Fix(StaticConfigure.FixSignIn);
		StaticConfigure.Global.Fix(StaticConfigure.FixGlobal);
		StaticConfigure.Skin.Fix();
		StaticConfigure.Match.Fix(StaticConfigure.FixMatch);
		StaticConfigure.MapEvent.Fix();
		StaticConfigure.Mission.Fix(StaticConfigure.FixMission);
		StaticConfigure.Map.Fix(StaticConfigure.FixMap);
		StaticConfigure.ExchangeStore.Fix();
	}
}
