using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_LitItem : GButton
{
	public Controller qualityType;

	public Controller isShowNum;

	public GLoader loader_Icon;

	public GTextField txt_Symbol;

	public GTextField txt_itemNum;

	public GLoader loader_replaceItem;

	public GGraph graph_ReplaceEffect;

	public GTextField txt_Symbol_Replace;

	public GTextField txt_itemNum_Replace;

	public GGroup group_Replace;

	public Transition Cut_in;

	public Transition showReplace;

	public Transition LoopReplace;

	public const string URL = "ui://m6sn3r22jz24ao";

	public static UICom_LitItem CreateInstance()
	{
		return (UICom_LitItem)UIPackage.CreateObject("Common_External", "Com_LitItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		qualityType = GetControllerAt(0);
		isShowNum = GetControllerAt(2);
		loader_Icon = (GLoader)GetChildAt(4);
		txt_Symbol = (GTextField)GetChildAt(6);
		txt_itemNum = (GTextField)GetChildAt(7);
		loader_replaceItem = (GLoader)GetChildAt(8);
		graph_ReplaceEffect = (GGraph)GetChildAt(9);
		txt_Symbol_Replace = (GTextField)GetChildAt(11);
		txt_itemNum_Replace = (GTextField)GetChildAt(12);
		group_Replace = (GGroup)GetChildAt(15);
		Cut_in = GetTransitionAt(0);
		showReplace = GetTransitionAt(1);
		LoopReplace = GetTransitionAt(2);
	}
}
