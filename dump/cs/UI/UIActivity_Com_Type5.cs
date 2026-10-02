using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5 : GComponent
{
	public Controller changge;

	public UIActivity_Com_Type5_Lin com_Lin;

	public UIActivity_Com_Type5_TaiDao com_TaiDao;

	public GTextField txt_Time;

	public Transition Cut_in;

	public Transition Reward_Loop;

	public const string URL = "ui://vckl96kslw7ydi";

	public static UIActivity_Com_Type5 CreateInstance()
	{
		return (UIActivity_Com_Type5)UIPackage.CreateObject("Activity", "Activity_Com_Type5");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		changge = GetControllerAt(0);
		com_Lin = (UIActivity_Com_Type5_Lin)GetChildAt(0);
		com_TaiDao = (UIActivity_Com_Type5_TaiDao)GetChildAt(1);
		txt_Time = (GTextField)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
		Reward_Loop = GetTransitionAt(1);
	}
}
