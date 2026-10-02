using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDice_Bg_Mission : GComponent
{
	public Transition Cut_in;

	public const string URL = "ui://lypih982lbjzy";

	public static UIActivityDice_Bg_Mission CreateInstance()
	{
		return (UIActivityDice_Bg_Mission)UIPackage.CreateObject("ActivityDice", "ActivityDice_Bg_Mission");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
