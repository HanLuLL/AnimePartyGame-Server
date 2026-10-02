using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAssistVoteWindow : GComponent
{
	public Controller tipStep;

	public GImage image_Left;

	public GImage image_Right;

	public GLoader loader_Right;

	public UIAssistVote_Com_RelicInfo com_RightRelic;

	public GGroup group_RightRelic;

	public UIAssistVote_Com_NPCInfo com_RightInfo;

	public GButton btn_SelectRight;

	public GLoader loader_Left;

	public UIAssistVote_Com_RelicInfo com_LeftRelic;

	public GGroup group_LeftRelic;

	public UIAssistVote_Com_NPCInfo com_LeftInfo;

	public GButton btn_SelectLeft;

	public GProgressBar progress_Time;

	public GTextField txt_Tip1;

	public GButton btn_SureVote;

	public GTextField txt_Tip2;

	public GTextField txt_Tip3;

	public GTextField txt_Tip4;

	public Transition Cut_in;

	public Transition SwapLi;

	public Transition SwapJiao;

	public Transition PK;

	public Transition PickJiao;

	public Transition PickLi;

	public Transition BackLi;

	public Transition BackJiao;

	public const string URL = "ui://andqlvspec7w0";

	public static UIAssistVoteWindow CreateInstance()
	{
		BindAll();
		return (UIAssistVoteWindow)UIPackage.CreateObject("AssistVote", "AssistVoteWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://andqlvspec7w0", typeof(UIAssistVoteWindow));
		UIObjectFactory.SetPackageItemExtension("ui://andqlvspec7w2", typeof(UIAssistVote_Button_Relic));
		UIObjectFactory.SetPackageItemExtension("ui://andqlvspec7w4", typeof(UIAssistVote_Com_VoteItem));
		UIObjectFactory.SetPackageItemExtension("ui://andqlvspec7wy", typeof(UIAssistVote_Com_NPCInfo));
		UIObjectFactory.SetPackageItemExtension("ui://andqlvspec7wz", typeof(UIAssistVote_Com_RelicInfo));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tipStep = GetControllerAt(0);
		image_Left = (GImage)GetChildAt(0);
		image_Right = (GImage)GetChildAt(1);
		loader_Right = (GLoader)GetChildAt(6);
		com_RightRelic = (UIAssistVote_Com_RelicInfo)GetChildAt(8);
		group_RightRelic = (GGroup)GetChildAt(9);
		com_RightInfo = (UIAssistVote_Com_NPCInfo)GetChildAt(10);
		btn_SelectRight = (GButton)GetChildAt(11);
		loader_Left = (GLoader)GetChildAt(12);
		com_LeftRelic = (UIAssistVote_Com_RelicInfo)GetChildAt(14);
		group_LeftRelic = (GGroup)GetChildAt(15);
		com_LeftInfo = (UIAssistVote_Com_NPCInfo)GetChildAt(16);
		btn_SelectLeft = (GButton)GetChildAt(17);
		progress_Time = (GProgressBar)GetChildAt(18);
		txt_Tip1 = (GTextField)GetChildAt(19);
		btn_SureVote = (GButton)GetChildAt(20);
		txt_Tip2 = (GTextField)GetChildAt(21);
		txt_Tip3 = (GTextField)GetChildAt(22);
		txt_Tip4 = (GTextField)GetChildAt(23);
		Cut_in = GetTransitionAt(0);
		SwapLi = GetTransitionAt(1);
		SwapJiao = GetTransitionAt(2);
		PK = GetTransitionAt(3);
		PickJiao = GetTransitionAt(4);
		PickLi = GetTransitionAt(5);
		BackLi = GetTransitionAt(6);
		BackJiao = GetTransitionAt(7);
	}
}
