using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type4_MainReward : GComponent
{
	public GButton btn_Item;

	public const string URL = "ui://c1v285vtpu2iw";

	public static UIActivity_Button_Type4_MainReward CreateInstance()
	{
		return (UIActivity_Button_Type4_MainReward)UIPackage.CreateObject("ActivityNgo", "Activity_Button_Type4_MainReward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Item = (GButton)GetChildAt(1);
	}
}
