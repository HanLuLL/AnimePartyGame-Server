using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreTwo_Button_Tab : GButton
{
	public Controller redStatus;

	public const string URL = "ui://6dt5s4htqw8h2";

	public static UIActivityStoreTwo_Button_Tab CreateInstance()
	{
		return (UIActivityStoreTwo_Button_Tab)UIPackage.CreateObject("ActivityStoreTwo", "ActivityStoreTwo_Button_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
	}
}
