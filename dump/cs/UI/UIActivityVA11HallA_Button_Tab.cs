using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityVA11HallA_Button_Tab : GButton
{
	public Controller redStatus;

	public const string URL = "ui://zlysd2gupgzf4z";

	public static UIActivityVA11HallA_Button_Tab CreateInstance()
	{
		return (UIActivityVA11HallA_Button_Tab)UIPackage.CreateObject("ActivityVA11HallA", "ActivityVA11HallA_Button_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
	}
}
