using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICardWin_Com_Loader_Lit : GComponent
{
	public Controller playerOrder;

	public GLoader loader;

	public const string URL = "ui://bi8fdi6nqgrb1v";

	public static UICardWin_Com_Loader_Lit CreateInstance()
	{
		return (UICardWin_Com_Loader_Lit)UIPackage.CreateObject("Card", "CardWin_Com_Loader_Lit");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		playerOrder = GetControllerAt(0);
		loader = (GLoader)GetChildAt(5);
	}
}
