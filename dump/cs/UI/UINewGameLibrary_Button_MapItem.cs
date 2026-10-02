using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Button_MapItem : GButton
{
	public Controller status;

	public GTextField txt_tltle;

	public GLoader loader_Icon;

	public Transition Cut_in;

	public const string URL = "ui://mc0y3plupj0z1t";

	public static UINewGameLibrary_Button_MapItem CreateInstance()
	{
		return (UINewGameLibrary_Button_MapItem)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Button_MapItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		txt_tltle = (GTextField)GetChildAt(2);
		loader_Icon = (GLoader)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
