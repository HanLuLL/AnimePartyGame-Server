using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Button_ShortInfo_Mobile : GButton
{
	public GImage image;

	public const string URL = "ui://mp1ylwytrct9z";

	public static UIExpression_Button_ShortInfo_Mobile CreateInstance()
	{
		return (UIExpression_Button_ShortInfo_Mobile)UIPackage.CreateObject("Expression", "Expression_Button_ShortInfo_Mobile");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		image = (GImage)GetChildAt(0);
	}
}
