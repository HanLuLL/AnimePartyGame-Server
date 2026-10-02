using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type5_Scratchoff_ListItem_Lin : GButton
{
	public Controller Status;

	public Controller selectedStatus;

	public UIActivity_Com_Type5_PreRevard com_Item;

	public UIActivity_Com_Type5_ItemName com_Name;

	public Transition ShowSwitchGold;

	public Transition Lantern;

	public Transition NB_Reward_loop;

	public Transition Nomal_Reward_loop;

	public const string URL = "ui://vckl96ksxcsllsq7q";

	public static UIActivity_Button_Type5_Scratchoff_ListItem_Lin CreateInstance()
	{
		return (UIActivity_Button_Type5_Scratchoff_ListItem_Lin)UIPackage.CreateObject("Activity", "Activity_Button_Type5_Scratchoff_ListItem_Lin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		selectedStatus = GetControllerAt(2);
		com_Item = (UIActivity_Com_Type5_PreRevard)GetChildAt(3);
		com_Name = (UIActivity_Com_Type5_ItemName)GetChildAt(4);
		ShowSwitchGold = GetTransitionAt(0);
		Lantern = GetTransitionAt(1);
		NB_Reward_loop = GetTransitionAt(2);
		Nomal_Reward_loop = GetTransitionAt(3);
	}
}
