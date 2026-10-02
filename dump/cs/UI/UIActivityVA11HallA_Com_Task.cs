using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityVA11HallA_Com_Task : GComponent
{
	public GLoader loader_Shadow;

	public GLoader loader_Character;

	public GTextField txt_Title;

	public GTextField txt_Task_Time;

	public UIActivityVA11HallA_Com_Animation com_Animation;

	public GTextField txt_AwardDesc;

	public GList list_Tasks;

	public UIActivityVA11HallA_Button_Toggle btn_TaskToggle;

	public GLoader loader_ActivityProp;

	public GTextField txt_ActivityPropCount;

	public Transition Cut_in;

	public const string URL = "ui://zlysd2gupgzf4c";

	public static UIActivityVA11HallA_Com_Task CreateInstance()
	{
		return (UIActivityVA11HallA_Com_Task)UIPackage.CreateObject("ActivityVA11HallA", "ActivityVA11HallA_Com_Task");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Shadow = (GLoader)GetChildAt(0);
		loader_Character = (GLoader)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(4);
		txt_Task_Time = (GTextField)GetChildAt(5);
		com_Animation = (UIActivityVA11HallA_Com_Animation)GetChildAt(7);
		txt_AwardDesc = (GTextField)GetChildAt(9);
		list_Tasks = (GList)GetChildAt(10);
		btn_TaskToggle = (UIActivityVA11HallA_Button_Toggle)GetChildAt(11);
		loader_ActivityProp = (GLoader)GetChildAt(13);
		txt_ActivityPropCount = (GTextField)GetChildAt(14);
		Cut_in = GetTransitionAt(0);
	}
}
