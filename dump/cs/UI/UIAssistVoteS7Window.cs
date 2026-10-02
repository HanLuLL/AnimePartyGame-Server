using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAssistVoteS7Window : GComponent
{
	public Controller tipStep;

	public Controller select;

	public UIChengSeYouBian loader_Right;

	public UIAssistVoteS7_Com_RelicInfo com_RightRelic;

	public GGroup group_RightRelic;

	public UIAssistVoteS7_Com_NPCInfo com_RightInfo;

	public GButton btn_SelectRight;

	public UILanSeZuoBian loader_Left;

	public UIAssistVoteS7_Com_RelicInfo com_LeftRelic;

	public GGroup group_LeftRelic;

	public UIAssistVoteS7_Com_NPCInfo com_LeftInfo;

	public GButton btn_SelectLeft;

	public UIAssistVoteS7_Com_NPCInfo com_CenterInfo;

	public GButton btn_SelectCenter;

	public UIAssistVoteS7_ProgressBar progress_Time;

	public GTextField txt_Tip1;

	public GButton btn_SureVote;

	public GTextField txt_Tip2;

	public GTextField txt_Tip3;

	public GTextField txt_Tip4;

	public Transition Cut_in;

	public Transition SwapBlue;

	public Transition SwapRed;

	public Transition BackRed;

	public Transition BackBlue;

	public Transition PickRed;

	public Transition PickBlue;

	public Transition PK;

	public Transition SwapMid;

	public Transition BackMid;

	public Transition PickMid;

	public const string URL = "ui://50xzye56puf30";

	public static UIAssistVoteS7Window CreateInstance()
	{
		BindAll();
		return (UIAssistVoteS7Window)UIPackage.CreateObject("AssistVoteS7", "AssistVoteS7Window");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://50xzye56puf30", typeof(UIAssistVoteS7Window));
		UIObjectFactory.SetPackageItemExtension("ui://50xzye56puf37", typeof(UIAssistVoteS7_Com_RelicInfo));
		UIObjectFactory.SetPackageItemExtension("ui://50xzye56puf3a", typeof(UIAssistVoteS7_Com_NPCInfo));
		UIObjectFactory.SetPackageItemExtension("ui://50xzye56puf3d", typeof(UIAssistVoteS7_Com_VoteItem));
		UIObjectFactory.SetPackageItemExtension("ui://50xzye56puf3p", typeof(UIAssistVoteS7_ProgressBar));
		UIObjectFactory.SetPackageItemExtension("ui://50xzye56r1gk1l", typeof(UILan));
		UIObjectFactory.SetPackageItemExtension("ui://50xzye56r1gk1m", typeof(UICheng));
		UIObjectFactory.SetPackageItemExtension("ui://50xzye56r1gk1p", typeof(UIChengSeYouBian));
		UIObjectFactory.SetPackageItemExtension("ui://50xzye56r1gk1q", typeof(UILanSeZuoBian));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tipStep = GetControllerAt(0);
		select = GetControllerAt(1);
		loader_Right = (UIChengSeYouBian)GetChildAt(3);
		com_RightRelic = (UIAssistVoteS7_Com_RelicInfo)GetChildAt(5);
		group_RightRelic = (GGroup)GetChildAt(6);
		com_RightInfo = (UIAssistVoteS7_Com_NPCInfo)GetChildAt(7);
		btn_SelectRight = (GButton)GetChildAt(8);
		loader_Left = (UILanSeZuoBian)GetChildAt(10);
		com_LeftRelic = (UIAssistVoteS7_Com_RelicInfo)GetChildAt(12);
		group_LeftRelic = (GGroup)GetChildAt(13);
		com_LeftInfo = (UIAssistVoteS7_Com_NPCInfo)GetChildAt(14);
		btn_SelectLeft = (GButton)GetChildAt(15);
		com_CenterInfo = (UIAssistVoteS7_Com_NPCInfo)GetChildAt(21);
		btn_SelectCenter = (GButton)GetChildAt(22);
		progress_Time = (UIAssistVoteS7_ProgressBar)GetChildAt(24);
		txt_Tip1 = (GTextField)GetChildAt(25);
		btn_SureVote = (GButton)GetChildAt(26);
		txt_Tip2 = (GTextField)GetChildAt(27);
		txt_Tip3 = (GTextField)GetChildAt(28);
		txt_Tip4 = (GTextField)GetChildAt(29);
		Cut_in = GetTransitionAt(0);
		SwapBlue = GetTransitionAt(1);
		SwapRed = GetTransitionAt(2);
		BackRed = GetTransitionAt(3);
		BackBlue = GetTransitionAt(4);
		PickRed = GetTransitionAt(5);
		PickBlue = GetTransitionAt(6);
		PK = GetTransitionAt(7);
		SwapMid = GetTransitionAt(8);
		BackMid = GetTransitionAt(9);
		PickMid = GetTransitionAt(10);
	}
}
