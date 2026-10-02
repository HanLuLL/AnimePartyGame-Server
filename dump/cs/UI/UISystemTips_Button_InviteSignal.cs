using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISystemTips_Button_InviteSignal : GButton
{
	public Transition loop;

	public const string URL = "ui://jrqrz0oq7h6s7";

	public static UISystemTips_Button_InviteSignal CreateInstance()
	{
		return (UISystemTips_Button_InviteSignal)UIPackage.CreateObject("SystemTips", "SystemTips_Button_InviteSignal");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loop = GetTransitionAt(0);
	}
}
