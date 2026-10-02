using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIDisplayCardWindow : GComponent
{
	public Controller state;

	public GGraph btn_Bg;

	public GButton com_Card;

	public GButton btn_Previous;

	public GButton btn_Next;

	public GButton AltArtCard;

	public GButton Btn_Previous_AltArt;

	public GButton Btn_Next_AltArt;

	public GList list_altCard;

	public UIDisplayCard_Button_Way btn_way;

	public GComponent com_relicKeyword;

	public GButton btn_back;

	public Transition Cut_In;

	public Transition YiHua_Cut_in;

	public const string URL = "ui://893ze0z8cp3s0";

	public static UIDisplayCardWindow CreateInstance()
	{
		BindAll();
		return (UIDisplayCardWindow)UIPackage.CreateObject("DisplayCard", "DisplayCardWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://893ze0z8ays2b", typeof(UIDisplayCard_Button_AltArt));
		UIObjectFactory.SetPackageItemExtension("ui://893ze0z8cp3s0", typeof(UIDisplayCardWindow));
		UIObjectFactory.SetPackageItemExtension("ui://893ze0z8okfee", typeof(UIDisplayCard_Button_Way));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		btn_Bg = (GGraph)GetChildAt(0);
		com_Card = (GButton)GetChildAt(1);
		btn_Previous = (GButton)GetChildAt(2);
		btn_Next = (GButton)GetChildAt(3);
		AltArtCard = (GButton)GetChildAt(4);
		Btn_Previous_AltArt = (GButton)GetChildAt(5);
		Btn_Next_AltArt = (GButton)GetChildAt(6);
		list_altCard = (GList)GetChildAt(10);
		btn_way = (UIDisplayCard_Button_Way)GetChildAt(11);
		com_relicKeyword = (GComponent)GetChildAt(12);
		btn_back = (GButton)GetChildAt(13);
		Cut_In = GetTransitionAt(0);
		YiHua_Cut_in = GetTransitionAt(1);
	}
}
