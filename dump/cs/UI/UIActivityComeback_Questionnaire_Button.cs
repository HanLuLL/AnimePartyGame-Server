using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Questionnaire_Button : GButton
{
	public Controller state;

	public Controller redPoint;

	public GTextField time;

	public const string URL = "ui://hconmwfcy9qn5";

	public static UIActivityComeback_Questionnaire_Button CreateInstance()
	{
		return (UIActivityComeback_Questionnaire_Button)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Questionnaire_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
		redPoint = GetControllerAt(2);
		time = (GTextField)GetChildAt(2);
	}
}
