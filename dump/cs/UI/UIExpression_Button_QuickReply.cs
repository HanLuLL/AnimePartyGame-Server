using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Button_QuickReply : GButton
{
	public GTextField txt_Tip;

	public const string URL = "ui://mp1ylwytikma10";

	public static UIExpression_Button_QuickReply CreateInstance()
	{
		return (UIExpression_Button_QuickReply)UIPackage.CreateObject("Expression", "Expression_Button_QuickReply");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Tip = (GTextField)GetChildAt(2);
	}
}
