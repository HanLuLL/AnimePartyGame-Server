using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type4_ScratchoffPreReward : GButton
{
	public Controller status;

	public GButton btn_Item;

	public const string URL = "ui://c1v285vtpu2i17";

	public static UIActivity_Button_Type4_ScratchoffPreReward CreateInstance()
	{
		return (UIActivity_Button_Type4_ScratchoffPreReward)UIPackage.CreateObject("ActivityNgo", "Activity_Button_Type4_ScratchoffPreReward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		btn_Item = (GButton)GetChildAt(1);
	}
}
