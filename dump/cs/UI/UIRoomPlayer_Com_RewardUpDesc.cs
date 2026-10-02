using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomPlayer_Com_RewardUpDesc : GComponent
{
	public Controller showType;

	public GGraph di;

	public GTextField txt_Desc1;

	public GTextField txt_Title1;

	public GButton up1;

	public GTextField txt_Desc2;

	public GTextField txt_Title2;

	public GButton up2;

	public const string URL = "ui://m6sn3r22rct9q3e";

	public static UIRoomPlayer_Com_RewardUpDesc CreateInstance()
	{
		return (UIRoomPlayer_Com_RewardUpDesc)UIPackage.CreateObject("Common_External", "RoomPlayer_Com_RewardUpDesc");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showType = GetControllerAt(0);
		di = (GGraph)GetChildAt(0);
		txt_Desc1 = (GTextField)GetChildAt(1);
		txt_Title1 = (GTextField)GetChildAt(2);
		up1 = (GButton)GetChildAt(3);
		txt_Desc2 = (GTextField)GetChildAt(4);
		txt_Title2 = (GTextField)GetChildAt(5);
		up2 = (GButton)GetChildAt(6);
	}
}
