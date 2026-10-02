using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_Task : GComponent
{
	public Controller taskValid;

	public GLoader loader_Task_Character;

	public GLoader loader_Title;

	public GButton btn_Preview;

	public GTextField txt_NextTime;

	public GTextField txt_Task_Time;

	public GTextField txt_Task_Time_Cover;

	public GList list_Tasks;

	public UIActivityStoreSeason_Button_Toggle btn_TaskToggle;

	public UIActivityStoreSeason_Com_Token com_Token;

	public Transition Cut_in;

	public const string URL = "ui://begz6gfv7vwmh";

	public static UIActivityStoreSeason_Com_Task CreateInstance()
	{
		return (UIActivityStoreSeason_Com_Task)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_Task");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		taskValid = GetControllerAt(0);
		loader_Task_Character = (GLoader)GetChildAt(0);
		loader_Title = (GLoader)GetChildAt(1);
		btn_Preview = (GButton)GetChildAt(2);
		txt_NextTime = (GTextField)GetChildAt(5);
		txt_Task_Time = (GTextField)GetChildAt(8);
		txt_Task_Time_Cover = (GTextField)GetChildAt(9);
		list_Tasks = (GList)GetChildAt(10);
		btn_TaskToggle = (UIActivityStoreSeason_Button_Toggle)GetChildAt(11);
		com_Token = (UIActivityStoreSeason_Com_Token)GetChildAt(12);
		Cut_in = GetTransitionAt(0);
	}
}
