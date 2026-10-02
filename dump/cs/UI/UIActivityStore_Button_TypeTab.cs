using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Button_TypeTab : GButton
{
	public Controller redStatus;

	public const string URL = "ui://88m1yfwgjhcl3n";

	public static UIActivityStore_Button_TypeTab CreateInstance()
	{
		return (UIActivityStore_Button_TypeTab)UIPackage.CreateObject("ActivityStore", "ActivityStore_Button_TypeTab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
	}
}
