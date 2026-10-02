using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandDivination_Com_DivinationTarget : GComponent
{
	public Controller Rarity;

	public GLoader loader_FrontCard;

	public GTextField txt_Name;

	public GTextField txt_CardIndex;

	public GTextField txt_CardTips;

	public const string URL = "ui://dngx84d5rum75";

	public static UILandDivination_Com_DivinationTarget CreateInstance()
	{
		return (UILandDivination_Com_DivinationTarget)UIPackage.CreateObject("LandDivination", "LandDivination_Com_DivinationTarget");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Rarity = GetControllerAt(0);
		loader_FrontCard = (GLoader)GetChildAt(0);
		txt_Name = (GTextField)GetChildAt(4);
		txt_CardIndex = (GTextField)GetChildAt(5);
		txt_CardTips = (GTextField)GetChildAt(6);
	}
}
