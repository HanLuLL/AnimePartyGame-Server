using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Button_Pin : GButton
{
	public Controller pin;

	public const string URL = "ui://y0luzhk8ednm11";

	public static UIChat_Button_Pin CreateInstance()
	{
		return (UIChat_Button_Pin)UIPackage.CreateObject("Chat", "Chat_Button_Pin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		pin = GetControllerAt(1);
	}
}
