using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Com_BG : GComponent
{
	public Controller showBg;

	public const string URL = "ui://b96qpoz6iu43q3v";

	public static UITutorial_Com_BG CreateInstance()
	{
		return (UITutorial_Com_BG)UIPackage.CreateObject("Tutorial", "Tutorial_Com_BG");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showBg = GetControllerAt(0);
	}
}
