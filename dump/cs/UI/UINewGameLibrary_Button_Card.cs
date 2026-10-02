using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Button_Card : GButton
{
	public Controller c1;

	public GButton com_card;

	public Transition Cut_in;

	public const string URL = "ui://mc0y3plupj0z1";

	public static UINewGameLibrary_Button_Card CreateInstance()
	{
		return (UINewGameLibrary_Button_Card)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Button_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		c1 = GetControllerAt(0);
		com_card = (GButton)GetChildAt(0);
		Cut_in = GetTransitionAt(0);
	}
}
