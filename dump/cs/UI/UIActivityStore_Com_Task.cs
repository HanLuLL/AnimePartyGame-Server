using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Com_Task : GComponent
{
	public Controller taskValid;

	public Controller Type;

	public GLoader loader_Task_Character;

	public GLoader loader_Title;

	public GButton btn_Preview;

	public GTextField txt_Task_Time;

	public GTextField txt_Task_Time_Cover;

	public GList list_Tasks;

	public GTextField txt_NextTime;

	public GTextField txt_Task_Timetype1;

	public GTextField txt_Task_Time_Covertype1;

	public GList list_Taskstype1;

	public GTextField tile_task;

	public Transition Cut_in;

	public Transition SOLO_Cut_in;

	public const string URL = "ui://88m1yfwgv1vk16";

	public static UIActivityStore_Com_Task CreateInstance()
	{
		return (UIActivityStore_Com_Task)UIPackage.CreateObject("ActivityStore", "ActivityStore_Com_Task");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		taskValid = GetControllerAt(0);
		Type = GetControllerAt(1);
		loader_Task_Character = (GLoader)GetChildAt(0);
		loader_Title = (GLoader)GetChildAt(2);
		btn_Preview = (GButton)GetChildAt(3);
		txt_Task_Time = (GTextField)GetChildAt(6);
		txt_Task_Time_Cover = (GTextField)GetChildAt(7);
		list_Tasks = (GList)GetChildAt(8);
		txt_NextTime = (GTextField)GetChildAt(11);
		txt_Task_Timetype1 = (GTextField)GetChildAt(16);
		txt_Task_Time_Covertype1 = (GTextField)GetChildAt(17);
		list_Taskstype1 = (GList)GetChildAt(18);
		tile_task = (GTextField)GetChildAt(20);
		Cut_in = GetTransitionAt(0);
		SOLO_Cut_in = GetTransitionAt(1);
	}
}
