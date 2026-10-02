using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICardWin_Com_Loader : GButton
{
	public GLoader loader;

	public const string URL = "ui://bi8fdi6nhk2a1j";

	public static UICardWin_Com_Loader CreateInstance()
	{
		return (UICardWin_Com_Loader)UIPackage.CreateObject("Card", "CardWin_Com_Loader");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader = (GLoader)GetChildAt(1);
	}
}
