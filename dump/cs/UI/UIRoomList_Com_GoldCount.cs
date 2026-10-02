using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomList_Com_GoldCount : GComponent
{
	public GTextField txt_UpgradePlan;

	public const string URL = "ui://5rtgb50esxcpj9i";

	public static UIRoomList_Com_GoldCount CreateInstance()
	{
		return (UIRoomList_Com_GoldCount)UIPackage.CreateObject("RoomList", "RoomList_Com_GoldCount");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_UpgradePlan = (GTextField)GetChildAt(2);
	}
}
