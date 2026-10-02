using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Button_MapChat : GButton
{
	public Controller status;

	public const string URL = "ui://mp1ylwytq93cu";

	public static UIExpression_Button_MapChat CreateInstance()
	{
		return (UIExpression_Button_MapChat)UIPackage.CreateObject("Expression", "Expression_Button_MapChat");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
	}
}
