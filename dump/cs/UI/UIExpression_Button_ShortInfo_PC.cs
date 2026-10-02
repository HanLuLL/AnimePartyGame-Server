using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Button_ShortInfo_PC : GButton
{
	public GImage image;

	public const string URL = "ui://mp1ylwytgbr1r";

	public static UIExpression_Button_ShortInfo_PC CreateInstance()
	{
		return (UIExpression_Button_ShortInfo_PC)UIPackage.CreateObject("Expression", "Expression_Button_ShortInfo_PC");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		image = (GImage)GetChildAt(0);
	}
}
