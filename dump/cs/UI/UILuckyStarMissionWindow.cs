using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILuckyStarMissionWindow : GComponent
{
	public Controller stage;

	public GLoader btn_mohu;

	public GList list_Card;

	public UILuckyStarMission_Com_Card com_ShowFinishMIssion;

	public GLoader loader_FinishPlayer;

	public GTextField txt_Finish;

	public Transition Show_Mission;

	public Transition MissionComplete;

	public const string URL = "ui://pwex29p4u5uz9";

	public static UILuckyStarMissionWindow CreateInstance()
	{
		BindAll();
		return (UILuckyStarMissionWindow)UIPackage.CreateObject("LuckyStarMission", "LuckyStarMissionWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://pwex29p4brvlq", typeof(UILuckyStarMission_Com_Card));
		UIObjectFactory.SetPackageItemExtension("ui://pwex29p4u5uz9", typeof(UILuckyStarMissionWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		stage = GetControllerAt(0);
		btn_mohu = (GLoader)GetChildAt(0);
		list_Card = (GList)GetChildAt(2);
		com_ShowFinishMIssion = (UILuckyStarMission_Com_Card)GetChildAt(5);
		loader_FinishPlayer = (GLoader)GetChildAt(6);
		txt_Finish = (GTextField)GetChildAt(7);
		Show_Mission = GetTransitionAt(0);
		MissionComplete = GetTransitionAt(1);
	}
}
