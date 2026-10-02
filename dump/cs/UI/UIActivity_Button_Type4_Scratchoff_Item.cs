using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type4_Scratchoff_Item : GButton
{
	public Controller Status;

	public GLoader loader_Reward;

	public GTextField txt_itemNum;

	public Transition showScratchOff;

	public Transition RedPointShow;

	public const string URL = "ui://c1v285vtpu2i19";

	public static UIActivity_Button_Type4_Scratchoff_Item CreateInstance()
	{
		return (UIActivity_Button_Type4_Scratchoff_Item)UIPackage.CreateObject("ActivityNgo", "Activity_Button_Type4_Scratchoff_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
		loader_Reward = (GLoader)GetChildAt(1);
		txt_itemNum = (GTextField)GetChildAt(4);
		showScratchOff = GetTransitionAt(0);
		RedPointShow = GetTransitionAt(1);
	}
}
