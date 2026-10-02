using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type2_Reward : GButton
{
	public Controller status;

	public GButton btn_Item;

	public const string URL = "ui://vckl96ksjzxa1i";

	public static UIActivity_Button_Type2_Reward CreateInstance()
	{
		return (UIActivity_Button_Type2_Reward)UIPackage.CreateObject("Activity", "Activity_Button_Type2_Reward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		btn_Item = (GButton)GetChildAt(0);
	}
}
