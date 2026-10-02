using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://ssf8xg9njz241d";

	public static UIBattlePass_Button_TaskStatus CreateInstance()
	{
		return (UIBattlePass_Button_TaskStatus)UIPackage.CreateObject("BattlePass", "BattlePass_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
