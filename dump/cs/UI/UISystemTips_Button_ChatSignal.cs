using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISystemTips_Button_ChatSignal : GButton
{
	public Transition loop;

	public const string URL = "ui://jrqrz0oqljlpd";

	public static UISystemTips_Button_ChatSignal CreateInstance()
	{
		return (UISystemTips_Button_ChatSignal)UIPackage.CreateObject("SystemTips", "SystemTips_Button_ChatSignal");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loop = GetTransitionAt(0);
	}
}
