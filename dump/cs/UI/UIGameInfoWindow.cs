using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGameInfoWindow : GComponent
{
	public Controller showPing;

	public UIGameInfo_Ping com_Ping;

	public const string URL = "ui://mttqkyletu3p0";

	public static UIGameInfoWindow CreateInstance()
	{
		BindAll();
		return (UIGameInfoWindow)UIPackage.CreateObject("GameInfo", "GameInfoWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://mttqkyletu3p0", typeof(UIGameInfoWindow));
		UIObjectFactory.SetPackageItemExtension("ui://mttqkyletu3p1", typeof(UIGameInfo_Ping));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showPing = GetControllerAt(0);
		com_Ping = (UIGameInfo_Ping)GetChildAt(0);
	}
}
