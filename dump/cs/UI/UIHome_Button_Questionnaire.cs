using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_Questionnaire : GButton
{
	public Controller redPoint;

	public const string URL = "ui://u7xbdcgumyupq5o";

	public static UIHome_Button_Questionnaire CreateInstance()
	{
		return (UIHome_Button_Questionnaire)UIPackage.CreateObject("Home", "Home_Button_Questionnaire");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
	}
}
