using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Com_Preview : GComponent
{
	public GTextField txt_Tip;

	public const string URL = "ui://mp1ylwytwfpy1x";

	public static UIExpression_Com_Preview CreateInstance()
	{
		return (UIExpression_Com_Preview)UIPackage.CreateObject("Expression", "Expression_Com_Preview");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Tip = (GTextField)GetChildAt(1);
	}
}
