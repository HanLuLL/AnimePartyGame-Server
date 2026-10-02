using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISurveyCenter_Button_TabItem : GButton
{
	public Controller redStatus;

	public const string URL = "ui://s9p4q57hmyup8";

	public static UISurveyCenter_Button_TabItem CreateInstance()
	{
		return (UISurveyCenter_Button_TabItem)UIPackage.CreateObject("SurveyCenter", "SurveyCenter_Button_TabItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
	}
}
