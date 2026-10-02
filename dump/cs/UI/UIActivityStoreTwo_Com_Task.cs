using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreTwo_Com_Task : GComponent
{
	public Controller taskValid;

	public GLoader loader_Task_Character;

	public GLoader loader_Title;

	public GTextField txt_Task_Timetype1;

	public GTextField txt_Task_Time_Covertype1;

	public GList list_Taskstype1;

	public GTextField txt_NextTime;

	public GButton btn_Preview;

	public Transition Cut_in;

	public const string URL = "ui://6dt5s4htqw8h7";

	public static UIActivityStoreTwo_Com_Task CreateInstance()
	{
		return (UIActivityStoreTwo_Com_Task)UIPackage.CreateObject("ActivityStoreTwo", "ActivityStoreTwo_Com_Task");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		taskValid = GetControllerAt(0);
		loader_Task_Character = (GLoader)GetChildAt(0);
		loader_Title = (GLoader)GetChildAt(1);
		txt_Task_Timetype1 = (GTextField)GetChildAt(5);
		txt_Task_Time_Covertype1 = (GTextField)GetChildAt(6);
		list_Taskstype1 = (GList)GetChildAt(7);
		txt_NextTime = (GTextField)GetChildAt(10);
		btn_Preview = (GButton)GetChildAt(12);
		Cut_in = GetTransitionAt(0);
	}
}
