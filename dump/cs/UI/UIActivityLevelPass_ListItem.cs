using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityLevelPass_ListItem : GComponent
{
	public Controller rewardState;

	public GTextField text_ItemLevel;

	public UIActivityLevelPass_Button_RewardItem freeItem;

	public UIActivityLevelPass_Button_RewardItem paidItem;

	public Transition Cut_in;

	public Transition GetLoop;

	public const string URL = "ui://fajmeueeklg7t";

	public static UIActivityLevelPass_ListItem CreateInstance()
	{
		return (UIActivityLevelPass_ListItem)UIPackage.CreateObject("ActivityLevelPass", "ActivityLevelPass_ListItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		rewardState = GetControllerAt(0);
		text_ItemLevel = (GTextField)GetChildAt(4);
		freeItem = (UIActivityLevelPass_Button_RewardItem)GetChildAt(5);
		paidItem = (UIActivityLevelPass_Button_RewardItem)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
		GetLoop = GetTransitionAt(1);
	}
}
