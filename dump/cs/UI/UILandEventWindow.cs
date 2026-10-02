using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandEventWindow : GComponent
{
	public Controller eventUI;

	public UILandEvent_Button_Card com_Card;

	public GComponent com_Dice;

	public GList list_AllPlayers;

	public GGroup group_30001;

	public GList list_Event;

	public GButton btn_confirm;

	public Transition showCard;

	public const string URL = "ui://duzt7gzkrum70";

	public static UILandEventWindow CreateInstance()
	{
		BindAll();
		return (UILandEventWindow)UIPackage.CreateObject("LandEvent", "LandEventWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://duzt7gzkp4oi6", typeof(UILandEvent_Graph_Player));
		UIObjectFactory.SetPackageItemExtension("ui://duzt7gzkrum70", typeof(UILandEventWindow));
		UIObjectFactory.SetPackageItemExtension("ui://duzt7gzkrum71", typeof(UILandEvent_Button_Card));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		eventUI = GetControllerAt(0);
		com_Card = (UILandEvent_Button_Card)GetChildAt(0);
		com_Dice = (GComponent)GetChildAt(1);
		list_AllPlayers = (GList)GetChildAt(3);
		group_30001 = (GGroup)GetChildAt(4);
		list_Event = (GList)GetChildAt(5);
		btn_confirm = (GButton)GetChildAt(7);
		showCard = GetTransitionAt(0);
	}
}
