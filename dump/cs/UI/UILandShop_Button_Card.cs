using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandShop_Button_Card : GButton
{
	public Controller isSold;

	public Controller isFree;

	public GComponent com_Card;

	public GGraph downEffect;

	public GLoader loader_FreeLogo;

	public Transition cut_in;

	public Transition Loop;

	public const string URL = "ui://d5ngzgeurum72";

	public static UILandShop_Button_Card CreateInstance()
	{
		return (UILandShop_Button_Card)UIPackage.CreateObject("LandShop", "LandShop_Button_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isSold = GetControllerAt(1);
		isFree = GetControllerAt(2);
		com_Card = (GComponent)GetChildAt(0);
		downEffect = (GGraph)GetChildAt(1);
		loader_FreeLogo = (GLoader)GetChildAt(3);
		cut_in = GetTransitionAt(0);
		Loop = GetTransitionAt(1);
	}
}
