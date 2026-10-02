using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICard_Com_RoundCard : GButton
{
	public GButton com_card;

	public GGraph downEffect;

	public const string URL = "ui://335msnc1kxm50";

	public static UICard_Com_RoundCard CreateInstance()
	{
		return (UICard_Com_RoundCard)UIPackage.CreateObject("ChooseRoundCard", "Card_Com_RoundCard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_card = (GButton)GetChildAt(0);
		downEffect = (GGraph)GetChildAt(1);
	}
}
