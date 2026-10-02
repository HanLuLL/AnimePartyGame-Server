using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_Vibrate : GComponent
{
	public Controller isMobile;

	public UISetting_Com_Toggle com_messagevibrate;

	public UISetting_Com_Toggle com_tipsvibrate;

	public Transition cut_in;

	public const string URL = "ui://iy1joavtodf71t";

	public static UISetting_Com_Vibrate CreateInstance()
	{
		return (UISetting_Com_Vibrate)UIPackage.CreateObject("Setting", "Setting_Com_Vibrate");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isMobile = GetControllerAt(0);
		com_messagevibrate = (UISetting_Com_Toggle)GetChildAt(1);
		com_tipsvibrate = (UISetting_Com_Toggle)GetChildAt(2);
		cut_in = GetTransitionAt(0);
	}
}
