using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleHint_Com_DragonPalace : GComponent
{
	public GTextField txt_BattleInfo;

	public Transition Cut_in;

	public const string URL = "ui://9vk1z24xidva1";

	public static UIBattleHint_Com_DragonPalace CreateInstance()
	{
		return (UIBattleHint_Com_DragonPalace)UIPackage.CreateObject("BattleHint", "BattleHint_Com_DragonPalace");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_BattleInfo = (GTextField)GetChildAt(20);
		Cut_in = GetTransitionAt(0);
	}
}
