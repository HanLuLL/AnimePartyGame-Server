using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UImail_ScrollBarButton_Grip : GButton
{
	public GLoader grip;

	public const string URL = "ui://91v7tm5ds2pwv";

	public static UImail_ScrollBarButton_Grip CreateInstance()
	{
		return (UImail_ScrollBarButton_Grip)UIPackage.CreateObject("Mail", "mail_ScrollBarButton_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GLoader)GetChildAt(0);
	}
}
