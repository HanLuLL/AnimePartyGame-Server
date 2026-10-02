using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePlayerInfoWindow : GComponent
{
	public Controller type;

	public UIBattlePlayerInfo_Com_HeroInfo com_Hero;

	public GComponent com_Monster;

	public Transition Cut_in;

	public const string URL = "ui://qzmgh1v9ef7v80";

	public static UIBattlePlayerInfoWindow CreateInstance()
	{
		BindAll();
		return (UIBattlePlayerInfoWindow)UIPackage.CreateObject("BattlePlayerInfo", "BattlePlayerInfoWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://qzmgh1v9ef7v80", typeof(UIBattlePlayerInfoWindow));
		UIObjectFactory.SetPackageItemExtension("ui://qzmgh1v9m7gv87", typeof(UIBattlePlayerInfo_Button_GameMode));
		UIObjectFactory.SetPackageItemExtension("ui://qzmgh1v9m7gv8g", typeof(UIBattlePlayerInfo_Button_Relic));
		UIObjectFactory.SetPackageItemExtension("ui://qzmgh1v9m7gv8i", typeof(UIBattlePlayerInfo_Button_Arrow));
		UIObjectFactory.SetPackageItemExtension("ui://qzmgh1v9m7gv8l", typeof(UIBattlePlayerInfo_Button_RelicChat));
		UIObjectFactory.SetPackageItemExtension("ui://qzmgh1v9nbg68o", typeof(UIBattlePlayerInfo_Com_HeroInfo));
		UIObjectFactory.SetPackageItemExtension("ui://qzmgh1v9nbg68q", typeof(UIBattlePlayerInfo_Button_BuffItem));
		UIObjectFactory.SetPackageItemExtension("ui://qzmgh1v9nbg68r", typeof(UIBattlePlayerInfo_Com_BattleData));
		UIObjectFactory.SetPackageItemExtension("ui://qzmgh1v9nbg68t", typeof(UIBattlePlayerInfo_Com_Skill));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		com_Hero = (UIBattlePlayerInfo_Com_HeroInfo)GetChildAt(0);
		com_Monster = (GComponent)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
	}
}
