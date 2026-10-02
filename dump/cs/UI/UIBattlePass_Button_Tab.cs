using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Button_Tab : GButton
{
	public Controller redStatus;

	public const string URL = "ui://ssf8xg9njz24z";

	public static UIBattlePass_Button_Tab CreateInstance()
	{
		return (UIBattlePass_Button_Tab)UIPackage.CreateObject("BattlePass", "BattlePass_Button_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
	}
}
