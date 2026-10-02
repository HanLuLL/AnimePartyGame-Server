using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMapPass_ListItem : GComponent
{
	public Controller taskState;

	public GTextField txt_complete;

	public GGroup State_complete;

	public GTextField txt_undone;

	public GGroup State_undone;

	public UIActivityMapPass_MapInfo mapInfo;

	public UIActivityMapPass_Button_RewardItem paidItem;

	public UIActivityMapPass_Button_RewardItem freeItem;

	public Transition Cutin;

	public const string URL = "ui://vvv9zaj1qa2x3";

	public static UIActivityMapPass_ListItem CreateInstance()
	{
		return (UIActivityMapPass_ListItem)UIPackage.CreateObject("ActivityMapPass", "ActivityMapPass_ListItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		taskState = GetControllerAt(0);
		txt_complete = (GTextField)GetChildAt(1);
		State_complete = (GGroup)GetChildAt(2);
		txt_undone = (GTextField)GetChildAt(4);
		State_undone = (GGroup)GetChildAt(5);
		mapInfo = (UIActivityMapPass_MapInfo)GetChildAt(8);
		paidItem = (UIActivityMapPass_Button_RewardItem)GetChildAt(9);
		freeItem = (UIActivityMapPass_Button_RewardItem)GetChildAt(10);
		Cutin = GetTransitionAt(0);
	}
}
