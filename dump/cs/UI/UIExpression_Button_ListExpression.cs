using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Button_ListExpression : GButton
{
	public GComponent com_Expression;

	public const string URL = "ui://mp1ylwytnrib8";

	public static UIExpression_Button_ListExpression CreateInstance()
	{
		return (UIExpression_Button_ListExpression)UIPackage.CreateObject("Expression", "Expression_Button_ListExpression");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Expression = (GComponent)GetChildAt(0);
	}
}
