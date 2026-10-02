using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMapPass_Button_RewardItem : GButton
{
	public Controller rewardState;

	public Controller isLock;

	public Controller isClaimed;

	public GGraph paid;

	public GGraph normal;

	public GGroup bg;

	public GLoader loader_Item;

	public GTextField txt_ItemFreeNum;

	public Transition GetLoop;

	public const string URL = "ui://vvv9zaj1qa2x4";

	public static UIActivityMapPass_Button_RewardItem CreateInstance()
	{
		return (UIActivityMapPass_Button_RewardItem)UIPackage.CreateObject("ActivityMapPass", "ActivityMapPass_Button_RewardItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		rewardState = GetControllerAt(1);
		isLock = GetControllerAt(2);
		isClaimed = GetControllerAt(3);
		paid = (GGraph)GetChildAt(2);
		normal = (GGraph)GetChildAt(3);
		bg = (GGroup)GetChildAt(4);
		loader_Item = (GLoader)GetChildAt(5);
		txt_ItemFreeNum = (GTextField)GetChildAt(6);
		GetLoop = GetTransitionAt(0);
	}
}
