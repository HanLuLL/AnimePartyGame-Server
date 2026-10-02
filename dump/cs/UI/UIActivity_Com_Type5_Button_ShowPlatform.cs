using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_Button_ShowPlatform : GButton
{
	public Transition LoopIn;

	public const string URL = "ui://vckl96kso812e8";

	public static UIActivity_Com_Type5_Button_ShowPlatform CreateInstance()
	{
		return (UIActivity_Com_Type5_Button_ShowPlatform)UIPackage.CreateObject("Activity", "Activity_Com_Type5_Button_ShowPlatform");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		LoopIn = GetTransitionAt(0);
	}
}
