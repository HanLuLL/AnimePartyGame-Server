using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type2_Main : GComponent
{
	public Controller language;

	public Controller taskValid;

	public GLoader loader_Character;

	public GLoader loader_Title;

	public GLoader loader_NPC;

	public GTextField txt_Time;

	public GList list_Task;

	public GList list_Preview;

	public UIActivity_Button_Toggle btn_TaskToggle;

	public UIActivity_Button_Type2_Scratchoff btn_Game;

	public Transition Cutin;

	public const string URL = "ui://vckl96ksjzxa1g";

	public static UIActivity_Com_Type2_Main CreateInstance()
	{
		return (UIActivity_Com_Type2_Main)UIPackage.CreateObject("Activity", "Activity_Com_Type2_Main");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		taskValid = GetControllerAt(1);
		loader_Character = (GLoader)GetChildAt(0);
		loader_Title = (GLoader)GetChildAt(1);
		loader_NPC = (GLoader)GetChildAt(4);
		txt_Time = (GTextField)GetChildAt(6);
		list_Task = (GList)GetChildAt(7);
		list_Preview = (GList)GetChildAt(12);
		btn_TaskToggle = (UIActivity_Button_Toggle)GetChildAt(13);
		btn_Game = (UIActivity_Button_Type2_Scratchoff)GetChildAt(15);
		Cutin = GetTransitionAt(0);
	}
}
