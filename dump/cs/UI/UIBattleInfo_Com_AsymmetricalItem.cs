using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_AsymmetricalItem : GComponent
{
	public Controller status;

	public const string URL = "ui://fxejlqlfcz379a";

	public static UIBattleInfo_Com_AsymmetricalItem CreateInstance()
	{
		return (UIBattleInfo_Com_AsymmetricalItem)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_AsymmetricalItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
	}
}
