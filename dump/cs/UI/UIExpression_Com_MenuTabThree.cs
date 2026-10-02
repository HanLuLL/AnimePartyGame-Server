using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Com_MenuTabThree : GComponent
{
	public GImage image_Top;

	public GImage image_Left;

	public GImage image_Right;

	public UIExpression_Button_MenuItem btn_Top;

	public UIExpression_Button_MenuItem btn_Left;

	public UIExpression_Button_MenuItem btn_Right;

	public const string URL = "ui://mp1ylwytvkgo1j";

	public static UIExpression_Com_MenuTabThree CreateInstance()
	{
		return (UIExpression_Com_MenuTabThree)UIPackage.CreateObject("Expression", "Expression_Com_MenuTabThree");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		image_Top = (GImage)GetChildAt(0);
		image_Left = (GImage)GetChildAt(1);
		image_Right = (GImage)GetChildAt(2);
		btn_Top = (UIExpression_Button_MenuItem)GetChildAt(10);
		btn_Left = (UIExpression_Button_MenuItem)GetChildAt(11);
		btn_Right = (UIExpression_Button_MenuItem)GetChildAt(12);
	}
}
