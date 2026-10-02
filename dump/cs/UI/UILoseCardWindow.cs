using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILoseCardWindow : GComponent
{
	public Controller type;

	public GList list_Card;

	public GButton btn_Lose;

	public GButton btn_Cancel;

	public GButton btn_Bestow;

	public GButton btn_Mix;

	public GTextField txt_CardNum;

	public const string URL = "ui://vxuz3wboorg70";

	public static UILoseCardWindow CreateInstance()
	{
		BindAll();
		return (UILoseCardWindow)UIPackage.CreateObject("LoseCard", "LoseCardWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://vxuz3wboorg70", typeof(UILoseCardWindow));
		UIObjectFactory.SetPackageItemExtension("ui://vxuz3wboorg71", typeof(UILoseCard_Button_Large));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		list_Card = (GList)GetChildAt(1);
		btn_Lose = (GButton)GetChildAt(2);
		btn_Cancel = (GButton)GetChildAt(3);
		btn_Bestow = (GButton)GetChildAt(4);
		btn_Mix = (GButton)GetChildAt(5);
		txt_CardNum = (GTextField)GetChildAt(6);
	}
}
