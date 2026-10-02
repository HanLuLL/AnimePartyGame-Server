using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPurchase_Button_BattlePassLevel : GButton
{
	public Controller status;

	public const string URL = "ui://cu17piy4jz241g";

	public static UIPurchase_Button_BattlePassLevel CreateInstance()
	{
		return (UIPurchase_Button_BattlePassLevel)UIPackage.CreateObject("Purchase", "Purchase_Button_BattlePassLevel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
	}
}
