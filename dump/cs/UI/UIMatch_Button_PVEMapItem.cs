using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMatch_Button_PVEMapItem : GButton
{
	public Controller status;

	public Controller itemType;

	public Controller signalType;

	public GList list_Monster;

	public GTextField txt_MapTitle;

	public GRichTextField txt_Explain;

	public GTextField txt_NEW;

	public GTextField txt_SignalChange;

	public GComponent com_MapTag;

	public const string URL = "ui://qxwapsemgib2x";

	public static UIMatch_Button_PVEMapItem CreateInstance()
	{
		return (UIMatch_Button_PVEMapItem)UIPackage.CreateObject("Match", "Match_Button_PVEMapItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		itemType = GetControllerAt(2);
		signalType = GetControllerAt(3);
		list_Monster = (GList)GetChildAt(2);
		txt_MapTitle = (GTextField)GetChildAt(6);
		txt_Explain = (GRichTextField)GetChildAt(7);
		txt_NEW = (GTextField)GetChildAt(8);
		txt_SignalChange = (GTextField)GetChildAt(9);
		com_MapTag = (GComponent)GetChildAt(10);
	}
}
