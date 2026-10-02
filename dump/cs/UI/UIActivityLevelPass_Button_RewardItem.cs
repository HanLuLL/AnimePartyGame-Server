using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityLevelPass_Button_RewardItem : GButton
{
	public Controller isLock;

	public Controller isClaimed;

	public Controller taskComplete;

	public GLoader loader_Item;

	public GTextField txt_ItemFreeNum;

	public Transition Get;

	public Transition Lock;

	public const string URL = "ui://fajmeueeklg7q";

	public static UIActivityLevelPass_Button_RewardItem CreateInstance()
	{
		return (UIActivityLevelPass_Button_RewardItem)UIPackage.CreateObject("ActivityLevelPass", "ActivityLevelPass_Button_RewardItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isLock = GetControllerAt(1);
		isClaimed = GetControllerAt(2);
		taskComplete = GetControllerAt(3);
		loader_Item = (GLoader)GetChildAt(0);
		txt_ItemFreeNum = (GTextField)GetChildAt(1);
		Get = GetTransitionAt(0);
		Lock = GetTransitionAt(1);
	}
}
