using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandDivination_Button_DivinationCard : GButton
{
	public Controller showEye;

	public Controller stage;

	public UILandDivination_Com_DivinationTarget com_Target;

	public GComponent com_Card;

	public GGraph effectOutline;

	public const string URL = "ui://dngx84d5rum74";

	public static UILandDivination_Button_DivinationCard CreateInstance()
	{
		return (UILandDivination_Button_DivinationCard)UIPackage.CreateObject("LandDivination", "LandDivination_Button_DivinationCard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showEye = GetControllerAt(1);
		stage = GetControllerAt(2);
		com_Target = (UILandDivination_Com_DivinationTarget)GetChildAt(0);
		com_Card = (GComponent)GetChildAt(1);
		effectOutline = (GGraph)GetChildAt(2);
	}
}
