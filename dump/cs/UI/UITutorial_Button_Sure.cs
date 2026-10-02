using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Button_Sure : GButton
{
	public Transition Loop;

	public const string URL = "ui://b96qpoz6ia9gg";

	public static UITutorial_Button_Sure CreateInstance()
	{
		return (UITutorial_Button_Sure)UIPackage.CreateObject("Tutorial", "Tutorial_Button_Sure");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Loop = GetTransitionAt(0);
	}
}
