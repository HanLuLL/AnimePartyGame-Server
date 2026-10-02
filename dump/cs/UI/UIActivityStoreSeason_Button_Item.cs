using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Button_Item : GComponent
{
	public Controller newAcquire_;

	public Controller replaceStatus;

	public GComponent com_QualityType;

	public GGraph graph_qualityEffect;

	public GLoader loader_Icon;

	public GGraph graph_DisplayEffect;

	public GGroup group_item;

	public GLoader loader_replaceItem;

	public GGraph graph_ReplaceEffect;

	public GGroup group_Replace;

	public GTextField txt_Count;

	public Transition showReplace;

	public Transition showResult;

	public const string URL = "ui://begz6gfv7vwm1f";

	public static UIActivityStoreSeason_Button_Item CreateInstance()
	{
		return (UIActivityStoreSeason_Button_Item)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Button_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		newAcquire_ = GetControllerAt(0);
		replaceStatus = GetControllerAt(1);
		com_QualityType = (GComponent)GetChildAt(0);
		graph_qualityEffect = (GGraph)GetChildAt(1);
		loader_Icon = (GLoader)GetChildAt(2);
		graph_DisplayEffect = (GGraph)GetChildAt(3);
		group_item = (GGroup)GetChildAt(5);
		loader_replaceItem = (GLoader)GetChildAt(6);
		graph_ReplaceEffect = (GGraph)GetChildAt(7);
		group_Replace = (GGroup)GetChildAt(9);
		txt_Count = (GTextField)GetChildAt(12);
		showReplace = GetTransitionAt(0);
		showResult = GetTransitionAt(1);
	}
}
