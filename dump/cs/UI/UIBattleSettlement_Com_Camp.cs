using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_Camp : GComponent
{
	public Controller camp;

	public GTextField txt_AddPoint;

	public const string URL = "ui://avgradidch0q141";

	public static UIBattleSettlement_Com_Camp CreateInstance()
	{
		return (UIBattleSettlement_Com_Camp)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_Camp");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		camp = GetControllerAt(0);
		txt_AddPoint = (GTextField)GetChildAt(8);
	}
}
