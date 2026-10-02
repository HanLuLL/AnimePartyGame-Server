using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type4_Main : GComponent
{
	public Controller taskValid;

	public Controller language;

	public GLoader loader_Icon_Color;

	public GLoader loader_Icon;

	public GLoader loader_Title;

	public GTextField txt_Time;

	public GList list_Task;

	public GGroup group_Task;

	public GList list_Preview;

	public GGroup group_PreReward;

	public UIActivity_Com_Type4_OpenScratchOff com_ScratchOff;

	public Transition Cut_in;

	public Transition Interaction;

	public Transition WindowFloat;

	public const string URL = "ui://c1v285vtpu2i1";

	public static UIActivity_Com_Type4_Main CreateInstance()
	{
		return (UIActivity_Com_Type4_Main)UIPackage.CreateObject("ActivityNgo", "Activity_Com_Type4_Main");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		taskValid = GetControllerAt(0);
		language = GetControllerAt(1);
		loader_Icon_Color = (GLoader)GetChildAt(35);
		loader_Icon = (GLoader)GetChildAt(36);
		loader_Title = (GLoader)GetChildAt(38);
		txt_Time = (GTextField)GetChildAt(48);
		list_Task = (GList)GetChildAt(51);
		group_Task = (GGroup)GetChildAt(52);
		list_Preview = (GList)GetChildAt(58);
		group_PreReward = (GGroup)GetChildAt(59);
		com_ScratchOff = (UIActivity_Com_Type4_OpenScratchOff)GetChildAt(60);
		Cut_in = GetTransitionAt(0);
		Interaction = GetTransitionAt(1);
		WindowFloat = GetTransitionAt(2);
	}
}
