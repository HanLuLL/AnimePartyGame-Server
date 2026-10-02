using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleHintWindow : GComponent
{
	public Controller hintType;

	public UIBattleHint_Com_DragonPalace com_DragonPalaceTreasure;

	public const string URL = "ui://9vk1z24xidva0";

	public static UIBattleHintWindow CreateInstance()
	{
		BindAll();
		return (UIBattleHintWindow)UIPackage.CreateObject("BattleHint", "BattleHintWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://9vk1z24xidva0", typeof(UIBattleHintWindow));
		UIObjectFactory.SetPackageItemExtension("ui://9vk1z24xidva1", typeof(UIBattleHint_Com_DragonPalace));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		hintType = GetControllerAt(0);
		com_DragonPalaceTreasure = (UIBattleHint_Com_DragonPalace)GetChildAt(0);
	}
}
