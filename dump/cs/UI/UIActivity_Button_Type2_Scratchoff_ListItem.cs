using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type2_Scratchoff_ListItem : GButton
{
	public Controller Status;

	public GLoader loader_Reward;

	public GTextField txt_itemNum;

	public Transition showScratchOff;

	public const string URL = "ui://vckl96ksjzxa1n";

	public static UIActivity_Button_Type2_Scratchoff_ListItem CreateInstance()
	{
		return (UIActivity_Button_Type2_Scratchoff_ListItem)UIPackage.CreateObject("Activity", "Activity_Button_Type2_Scratchoff_ListItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		loader_Reward = (GLoader)GetChildAt(1);
		txt_itemNum = (GTextField)GetChildAt(4);
		showScratchOff = GetTransitionAt(0);
	}
}
