using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandDivinationWindow : GComponent
{
	public Controller Progress;

	public Controller select;

	public Controller HideUI;

	public Controller swf;

	public GLoader loader_Paint;

	public GTextField txt_Tip;

	public GGroup group_Tip;

	public UILandDivination_Button_DivinationCard btn_Divination_1;

	public UILandDivination_Button_DivinationCard btn_Divination_2;

	public Transition cut_in;

	public const string URL = "ui://dngx84d5rum70";

	public static UILandDivinationWindow CreateInstance()
	{
		BindAll();
		return (UILandDivinationWindow)UIPackage.CreateObject("LandDivination", "LandDivinationWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://dngx84d5rum70", typeof(UILandDivinationWindow));
		UIObjectFactory.SetPackageItemExtension("ui://dngx84d5rum74", typeof(UILandDivination_Button_DivinationCard));
		UIObjectFactory.SetPackageItemExtension("ui://dngx84d5rum75", typeof(UILandDivination_Com_DivinationTarget));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Progress = GetControllerAt(0);
		select = GetControllerAt(1);
		HideUI = GetControllerAt(2);
		swf = GetControllerAt(3);
		loader_Paint = (GLoader)GetChildAt(1);
		txt_Tip = (GTextField)GetChildAt(8);
		group_Tip = (GGroup)GetChildAt(9);
		btn_Divination_1 = (UILandDivination_Button_DivinationCard)GetChildAt(11);
		btn_Divination_2 = (UILandDivination_Button_DivinationCard)GetChildAt(12);
		cut_in = GetTransitionAt(0);
	}
}
