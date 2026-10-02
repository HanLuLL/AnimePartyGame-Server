using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_LiveMode : GComponent
{
	public Controller isAuditMode;

	public UISetting_Com_Toggle com_CoverSwither;

	public UISetting_Com_Toggle com_AdultSwither;

	public UISetting_Com_Toggle com_CardSuggestSwither;

	public UISetting_Com_Toggle com_RelicSuggestSwither;

	public UISetting_Com_Toggle com_RoadSuggestSwither;

	public UISetting_Com_Toggle com_AutoShortChatSwither;

	public UISetting_Com_Toggle com_ActionSuggestSwither;

	public Transition Cut_in;

	public const string URL = "ui://iy1joavto1n81c";

	public static UISetting_Com_LiveMode CreateInstance()
	{
		return (UISetting_Com_LiveMode)UIPackage.CreateObject("Setting", "Setting_Com_LiveMode");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isAuditMode = GetControllerAt(0);
		com_CoverSwither = (UISetting_Com_Toggle)GetChildAt(1);
		com_AdultSwither = (UISetting_Com_Toggle)GetChildAt(3);
		com_CardSuggestSwither = (UISetting_Com_Toggle)GetChildAt(6);
		com_RelicSuggestSwither = (UISetting_Com_Toggle)GetChildAt(7);
		com_RoadSuggestSwither = (UISetting_Com_Toggle)GetChildAt(8);
		com_AutoShortChatSwither = (UISetting_Com_Toggle)GetChildAt(9);
		com_ActionSuggestSwither = (UISetting_Com_Toggle)GetChildAt(10);
		Cut_in = GetTransitionAt(0);
	}
}
