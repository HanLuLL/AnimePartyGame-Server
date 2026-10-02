using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Button_GameMode : GButton
{
	public Controller GameMode;

	public GLoader loader_Icon_Up;

	public GLoader loader_Icon_Down;

	public GTextField txt_Title;

	public GTextField txt_Explain;

	public const string URL = "ui://mc0y3plupj0z20";

	public static UINewGameLibrary_Button_GameMode CreateInstance()
	{
		return (UINewGameLibrary_Button_GameMode)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Button_GameMode");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		GameMode = GetControllerAt(1);
		loader_Icon_Up = (GLoader)GetChildAt(0);
		loader_Icon_Down = (GLoader)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(5);
		txt_Explain = (GTextField)GetChildAt(6);
	}
}
