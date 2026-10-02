using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Button_MenuItem : GButton
{
	public GImage image_Hover;

	public GImage image_Frame;

	public GLoader loader_Icon;

	public const string URL = "ui://mp1ylwytvkgo17";

	public static UIExpression_Button_MenuItem CreateInstance()
	{
		return (UIExpression_Button_MenuItem)UIPackage.CreateObject("Expression", "Expression_Button_MenuItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		image_Hover = (GImage)GetChildAt(3);
		image_Frame = (GImage)GetChildAt(4);
		loader_Icon = (GLoader)GetChildAt(5);
	}
}
