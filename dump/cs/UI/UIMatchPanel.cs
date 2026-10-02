using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMatchPanel : GComponent
{
	public Controller leader;

	public Controller mode;

	public GTextField txt_SelectMap;

	public GList list_Map;

	public GLoader loader_Map;

	public GLoader loader_Detail;

	public GTextField txt_SelectRelation;

	public GRichTextField txt__Advise;

	public GList list_Relation;

	public GComponent com_Player;

	public GButton btn_Ready;

	public GButton btn_ExitTeam;

	public GGroup group_Team;

	public GButton btn_StartMatch;

	public GButton btn_CancelMatch;

	public GGroup group_Operate;

	public GButton btn_Return;

	public Transition CutIn;

	public Transition MapSwitchOut;

	public const string URL = "ui://qxwapsemd828g";

	public static UIMatchPanel CreateInstance()
	{
		BindAll();
		return (UIMatchPanel)UIPackage.CreateObject("Match", "MatchPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://qxwapsemd828g", typeof(UIMatchPanel));
		UIObjectFactory.SetPackageItemExtension("ui://qxwapsemgib2u", typeof(UIMatch_Button_DifficultyItem));
		UIObjectFactory.SetPackageItemExtension("ui://qxwapsemgib2x", typeof(UIMatch_Button_PVEMapItem));
		UIObjectFactory.SetPackageItemExtension("ui://qxwapsemr26s1c", typeof(UIMatch_Com_MapBanner));
		UIObjectFactory.SetPackageItemExtension("ui://qxwapsemr26s1d", typeof(UIMatch_Com_MapItem));
		UIObjectFactory.SetPackageItemExtension("ui://qxwapsemr26s1e", typeof(UIMatch_Com_MapMonsterItem));
		UIObjectFactory.SetPackageItemExtension("ui://qxwapsemr26s1m", typeof(UIMatch_Button_PVPModeItem));
		UIObjectFactory.SetPackageItemExtension("ui://qxwapsemzi0cm", typeof(UIMatch_Button_PVPMapItem));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		leader = GetControllerAt(0);
		mode = GetControllerAt(1);
		txt_SelectMap = (GTextField)GetChildAt(1);
		list_Map = (GList)GetChildAt(4);
		loader_Map = (GLoader)GetChildAt(5);
		loader_Detail = (GLoader)GetChildAt(6);
		txt_SelectRelation = (GTextField)GetChildAt(10);
		txt__Advise = (GRichTextField)GetChildAt(11);
		list_Relation = (GList)GetChildAt(12);
		com_Player = (GComponent)GetChildAt(14);
		btn_Ready = (GButton)GetChildAt(15);
		btn_ExitTeam = (GButton)GetChildAt(16);
		group_Team = (GGroup)GetChildAt(17);
		btn_StartMatch = (GButton)GetChildAt(18);
		btn_CancelMatch = (GButton)GetChildAt(19);
		group_Operate = (GGroup)GetChildAt(20);
		btn_Return = (GButton)GetChildAt(21);
		CutIn = GetTransitionAt(0);
		MapSwitchOut = GetTransitionAt(1);
	}
}
