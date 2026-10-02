using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICard_Com_ChooseCard : GComponent
{
	public GGraph mohu;

	public GComponent com_LeftCard;

	public GComponent com_RightCard;

	public const string URL = "ui://bi8fdi6nu07q2s";

	public static UICard_Com_ChooseCard CreateInstance()
	{
		return (UICard_Com_ChooseCard)UIPackage.CreateObject("Card", "Card_Com_ChooseCard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		com_LeftCard = (GComponent)GetChildAt(1);
		com_RightCard = (GComponent)GetChildAt(2);
	}
}
