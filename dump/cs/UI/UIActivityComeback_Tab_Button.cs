using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Tab_Button : GButton
{
	public Controller tabType;

	public Controller redPoint;

	public Transition Cut_in;

	public const string URL = "ui://hconmwfcy9qn4";

	public static UIActivityComeback_Tab_Button CreateInstance()
	{
		return (UIActivityComeback_Tab_Button)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Tab_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tabType = GetControllerAt(1);
		redPoint = GetControllerAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
