using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_SwitchTab_Button : GButton
{
	public Controller redpoint;

	public Transition Cut_in;

	public Transition Switch_in;

	public Transition Switch_Out;

	public const string URL = "ui://zyd0rl00r3jjqq4g";

	public static UIStore_SwitchTab_Button CreateInstance()
	{
		return (UIStore_SwitchTab_Button)UIPackage.CreateObject("Store", "Store_SwitchTab_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redpoint = GetControllerAt(1);
		Cut_in = GetTransitionAt(0);
		Switch_in = GetTransitionAt(1);
		Switch_Out = GetTransitionAt(2);
	}
}
