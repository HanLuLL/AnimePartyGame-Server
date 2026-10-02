using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMatchInfoWindow : GComponent
{
	public Controller matchStatus;

	public GTextField txt_Time;

	public GGroup matching;

	public GTextField txt_PlayerCount;

	public GGroup waiting;

	public GGraph btn_OpenMatch;

	public GButton btn_LeaveMatch;

	public Transition Find2Team;

	public Transition Team2Find;

	public const string URL = "ui://6fn6dj8bq2ux0";

	public static UIMatchInfoWindow CreateInstance()
	{
		BindAll();
		return (UIMatchInfoWindow)UIPackage.CreateObject("MatchInfo", "MatchInfoWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://6fn6dj8bq2ux0", typeof(UIMatchInfoWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		matchStatus = GetControllerAt(0);
		txt_Time = (GTextField)GetChildAt(2);
		matching = (GGroup)GetChildAt(3);
		txt_PlayerCount = (GTextField)GetChildAt(6);
		waiting = (GGroup)GetChildAt(7);
		btn_OpenMatch = (GGraph)GetChildAt(8);
		btn_LeaveMatch = (GButton)GetChildAt(9);
		Find2Team = GetTransitionAt(0);
		Team2Find = GetTransitionAt(1);
	}
}
