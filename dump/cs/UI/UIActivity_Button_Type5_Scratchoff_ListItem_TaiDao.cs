using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao : GButton
{
	public Controller Status;

	public Controller selectedStatus;

	public UIActivity_Com_Type5_PreRevard com_Item;

	public UIActivity_Com_Type5_ItemName com_Name;

	public Transition ShowSwitchGold;

	public Transition RewardLoop;

	public const string URL = "ui://vckl96ksyjnjlsq8h";

	public static UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao CreateInstance()
	{
		return (UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao)UIPackage.CreateObject("Activity", "Activity_Button_Type5_Scratchoff_ListItem_TaiDao");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		selectedStatus = GetControllerAt(2);
		com_Item = (UIActivity_Com_Type5_PreRevard)GetChildAt(1);
		com_Name = (UIActivity_Com_Type5_ItemName)GetChildAt(2);
		ShowSwitchGold = GetTransitionAt(0);
		RewardLoop = GetTransitionAt(1);
	}
}
