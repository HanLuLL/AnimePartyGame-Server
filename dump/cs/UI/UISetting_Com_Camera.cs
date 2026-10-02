using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_Camera : GComponent
{
	public Controller isMobile;

	public UISetting_Com_Toggle com_MouseSwither;

	public UISetting_Com_Toggle com_FreeCameraSwither;

	public UISetting_Com_Toggle com_KeyBoardSwither;

	public Transition cut_in;

	public const string URL = "ui://iy1joavto1n8k";

	public static UISetting_Com_Camera CreateInstance()
	{
		return (UISetting_Com_Camera)UIPackage.CreateObject("Setting", "Setting_Com_Camera");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isMobile = GetControllerAt(0);
		com_MouseSwither = (UISetting_Com_Toggle)GetChildAt(1);
		com_FreeCameraSwither = (UISetting_Com_Toggle)GetChildAt(2);
		com_KeyBoardSwither = (UISetting_Com_Toggle)GetChildAt(3);
		cut_in = GetTransitionAt(0);
	}
}
