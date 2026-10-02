using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISurveyCenter_Button_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://s9p4q57hmyup7";

	public static UISurveyCenter_Button_Grip CreateInstance()
	{
		return (UISurveyCenter_Button_Grip)UIPackage.CreateObject("SurveyCenter", "SurveyCenter_Button_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
