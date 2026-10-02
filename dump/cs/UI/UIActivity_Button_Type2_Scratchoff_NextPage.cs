using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type2_Scratchoff_NextPage : GButton
{
	public Controller status;

	public GTextField txt_Progress;

	public Transition showNext;

	public const string URL = "ui://vckl96ksjzxa1p";

	public static UIActivity_Button_Type2_Scratchoff_NextPage CreateInstance()
	{
		return (UIActivity_Button_Type2_Scratchoff_NextPage)UIPackage.CreateObject("Activity", "Activity_Button_Type2_Scratchoff_NextPage");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		txt_Progress = (GTextField)GetChildAt(2);
		showNext = GetTransitionAt(0);
	}
}
