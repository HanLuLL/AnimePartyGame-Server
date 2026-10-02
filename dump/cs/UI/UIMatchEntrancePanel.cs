using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMatchEntrancePanel : GComponent
{
	public Controller pveMatchType;

	public GButton btn_Return;

	public GButton btn_RoomList;

	public GButton btn_Campaign;

	public GButton btn_MatchPVE;

	public GButton btn_MatchPVP;

	public GButton btn_MatchPVE_Short;

	public GButton btn_MatchPVP_Short;

	public GButton btn_MatchSportPVE;

	public Transition Cut_in;

	public const string URL = "ui://lrvlamcafois1";

	public static UIMatchEntrancePanel CreateInstance()
	{
		BindAll();
		return (UIMatchEntrancePanel)UIPackage.CreateObject("MatchEntrance", "MatchEntrancePanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://lrvlamcafois1", typeof(UIMatchEntrancePanel));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		pveMatchType = GetControllerAt(0);
		btn_Return = (GButton)GetChildAt(0);
		btn_RoomList = (GButton)GetChildAt(1);
		btn_Campaign = (GButton)GetChildAt(2);
		btn_MatchPVE = (GButton)GetChildAt(3);
		btn_MatchPVP = (GButton)GetChildAt(4);
		btn_MatchPVE_Short = (GButton)GetChildAt(5);
		btn_MatchPVP_Short = (GButton)GetChildAt(6);
		btn_MatchSportPVE = (GButton)GetChildAt(7);
		Cut_in = GetTransitionAt(0);
	}
}
