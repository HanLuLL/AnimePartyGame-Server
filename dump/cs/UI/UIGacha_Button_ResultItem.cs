using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Button_ResultItem : GComponent
{
	public Controller newAcquire_;

	public Controller replaceStatus;

	public Controller showcount;

	public GComponent com_QualityType;

	public GGraph graph_qualityEffect;

	public GLoader loader_Icon;

	public GGraph graph_DisplayEffect;

	public GGroup group_item;

	public GTextField txt_count;

	public GLoader loader_replaceItem;

	public GGraph graph_ReplaceEffect;

	public GTextField txt_replaceNum;

	public GGroup group_Replace;

	public Transition showReplace;

	public Transition showResult;

	public Transition LoopReplace;

	public const string URL = "ui://j90wpcmnabqa1a";

	public static UIGacha_Button_ResultItem CreateInstance()
	{
		return (UIGacha_Button_ResultItem)UIPackage.CreateObject("Gacha", "Gacha_Button_ResultItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		newAcquire_ = GetControllerAt(0);
		replaceStatus = GetControllerAt(1);
		showcount = GetControllerAt(2);
		com_QualityType = (GComponent)GetChildAt(0);
		graph_qualityEffect = (GGraph)GetChildAt(1);
		loader_Icon = (GLoader)GetChildAt(2);
		graph_DisplayEffect = (GGraph)GetChildAt(3);
		group_item = (GGroup)GetChildAt(5);
		txt_count = (GTextField)GetChildAt(8);
		loader_replaceItem = (GLoader)GetChildAt(10);
		graph_ReplaceEffect = (GGraph)GetChildAt(11);
		txt_replaceNum = (GTextField)GetChildAt(14);
		group_Replace = (GGroup)GetChildAt(17);
		showReplace = GetTransitionAt(0);
		showResult = GetTransitionAt(1);
		LoopReplace = GetTransitionAt(2);
	}
}
