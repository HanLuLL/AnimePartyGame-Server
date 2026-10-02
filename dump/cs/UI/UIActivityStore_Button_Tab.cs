using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Button_Tab : GButton
{
	public Controller redStatus;

	public const string URL = "ui://88m1yfwgv1vk1f";

	public static UIActivityStore_Button_Tab CreateInstance()
	{
		return (UIActivityStore_Button_Tab)UIPackage.CreateObject("ActivityStore", "ActivityStore_Button_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
	}
}
