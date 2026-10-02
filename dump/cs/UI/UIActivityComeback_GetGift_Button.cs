using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_GetGift_Button : GButton
{
	public Controller getType;

	public Controller redPoint;

	public const string URL = "ui://hconmwfcy9qnw";

	public static UIActivityComeback_GetGift_Button CreateInstance()
	{
		return (UIActivityComeback_GetGift_Button)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_GetGift_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		getType = GetControllerAt(1);
		redPoint = GetControllerAt(2);
	}
}
