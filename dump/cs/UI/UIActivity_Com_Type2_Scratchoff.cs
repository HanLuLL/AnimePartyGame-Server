using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type2_Scratchoff : GComponent
{
	public GLoader loader_Character_2;

	public GLoader loader_Character_1;

	public GLoader loader_Title;

	public GLoader loader_NPC;

	public GTextField txt_Time;

	public GList list_Scratchoff;

	public UIActivity_Button_Type2_Scratchoff_NextPage btn_Next;

	public GLoader loader_Prop;

	public GTextField txt_NeedNum;

	public GList list_Preview;

	public Transition Cutin;

	public Transition Interaction;

	public const string URL = "ui://vckl96ksjzxa1h";

	public static UIActivity_Com_Type2_Scratchoff CreateInstance()
	{
		return (UIActivity_Com_Type2_Scratchoff)UIPackage.CreateObject("Activity", "Activity_Com_Type2_Scratchoff");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Character_2 = (GLoader)GetChildAt(0);
		loader_Character_1 = (GLoader)GetChildAt(1);
		loader_Title = (GLoader)GetChildAt(2);
		loader_NPC = (GLoader)GetChildAt(5);
		txt_Time = (GTextField)GetChildAt(7);
		list_Scratchoff = (GList)GetChildAt(8);
		btn_Next = (UIActivity_Button_Type2_Scratchoff_NextPage)GetChildAt(9);
		loader_Prop = (GLoader)GetChildAt(10);
		txt_NeedNum = (GTextField)GetChildAt(11);
		list_Preview = (GList)GetChildAt(13);
		Cutin = GetTransitionAt(0);
		Interaction = GetTransitionAt(1);
	}
}
