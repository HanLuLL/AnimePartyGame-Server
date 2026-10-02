using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandFillingStation_Button_Stop : GButton
{
	public GTextField txt_Title;

	public GTextField txt_Desc;

	public Transition Loop;

	public const string URL = "ui://dhoi6vx2rum74";

	public static UILandFillingStation_Button_Stop CreateInstance()
	{
		return (UILandFillingStation_Button_Stop)UIPackage.CreateObject("LandFillingStation", "LandFillingStation_Button_Stop");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(2);
		txt_Desc = (GTextField)GetChildAt(3);
		Loop = GetTransitionAt(0);
	}
}
