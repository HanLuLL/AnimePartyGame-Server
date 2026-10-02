using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomSetting_Button_Info : GButton
{
	public Controller type;

	public Controller isLock;

	public GTextField txt_Content_Map;

	public UICom_MapTag com_mapTag;

	public GTextField txt_Content_Condition;

	public GRichTextField txt_Content_Time;

	public GTextField txt_Content_Multiple;

	public GRichTextField txt_Content_Difficulty;

	public const string URL = "ui://m6sn3r22ucnq9u";

	public static UIRoomSetting_Button_Info CreateInstance()
	{
		return (UIRoomSetting_Button_Info)UIPackage.CreateObject("Common_External", "RoomSetting_Button_Info");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		isLock = GetControllerAt(1);
		txt_Content_Map = (GTextField)GetChildAt(1);
		com_mapTag = (UICom_MapTag)GetChildAt(2);
		txt_Content_Condition = (GTextField)GetChildAt(3);
		txt_Content_Time = (GRichTextField)GetChildAt(6);
		txt_Content_Multiple = (GTextField)GetChildAt(7);
		txt_Content_Difficulty = (GRichTextField)GetChildAt(10);
	}
}
