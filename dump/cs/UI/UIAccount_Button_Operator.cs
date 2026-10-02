using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccount_Button_Operator : GButton
{
	public Controller type;

	public const string URL = "ui://iepldke77h6s24";

	public static UIAccount_Button_Operator CreateInstance()
	{
		return (UIAccount_Button_Operator)UIPackage.CreateObject("AccountInfo", "Account_Button_Operator");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
	}
}
