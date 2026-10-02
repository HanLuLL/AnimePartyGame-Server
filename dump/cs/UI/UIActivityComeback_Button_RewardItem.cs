using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Button_RewardItem : GButton
{
	public Controller isLock;

	public Controller isClaimed;

	public Controller taskComplete;

	public Controller itemType;

	public GLoader loader_Item;

	public GTextField txt_ItemFreeNum;

	public Transition Get;

	public Transition Lock;

	public const string URL = "ui://hconmwfcvjqj1f";

	public static UIActivityComeback_Button_RewardItem CreateInstance()
	{
		return (UIActivityComeback_Button_RewardItem)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Button_RewardItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isLock = GetControllerAt(1);
		isClaimed = GetControllerAt(2);
		taskComplete = GetControllerAt(3);
		itemType = GetControllerAt(4);
		loader_Item = (GLoader)GetChildAt(2);
		txt_ItemFreeNum = (GTextField)GetChildAt(3);
		Get = GetTransitionAt(0);
		Lock = GetTransitionAt(1);
	}
}
