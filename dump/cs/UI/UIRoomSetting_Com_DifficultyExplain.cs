using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomSetting_Com_DifficultyExplain : GComponent
{
	public GGraph di;

	public GRichTextField txt_Desc;

	public const string URL = "ui://m6sn3r22uws2q3f";

	public static UIRoomSetting_Com_DifficultyExplain CreateInstance()
	{
		return (UIRoomSetting_Com_DifficultyExplain)UIPackage.CreateObject("Common_External", "RoomSetting_Com_DifficultyExplain");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di = (GGraph)GetChildAt(0);
		txt_Desc = (GRichTextField)GetChildAt(1);
	}
}
