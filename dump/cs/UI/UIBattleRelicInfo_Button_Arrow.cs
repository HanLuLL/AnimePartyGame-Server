using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleRelicInfo_Button_Arrow : GButton
{
	public Controller vailStatus;

	public const string URL = "ui://ethkhr1hot0wf";

	public static UIBattleRelicInfo_Button_Arrow CreateInstance()
	{
		return (UIBattleRelicInfo_Button_Arrow)UIPackage.CreateObject("BattleRelicInfo", "BattleRelicInfo_Button_Arrow");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		vailStatus = GetControllerAt(1);
	}
}
