using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGameInfo_Ping : GComponent
{
	public Controller ePingState;

	public GTextField txt_Ping;

	public const string URL = "ui://mttqkyletu3p1";

	public static UIGameInfo_Ping CreateInstance()
	{
		return (UIGameInfo_Ping)UIPackage.CreateObject("GameInfo", "GameInfo_Ping");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		ePingState = GetControllerAt(0);
		txt_Ping = (GTextField)GetChildAt(0);
	}
}
