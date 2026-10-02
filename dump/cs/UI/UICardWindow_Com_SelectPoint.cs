using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICardWindow_Com_SelectPoint : GComponent
{
	public Controller selectType;

	public GList list_DiceNum;

	public GButton btn_SurePoint;

	public GButton btn_OK;

	public GButton btn_Cancel;

	public const string URL = "ui://bi8fdi6np5xi2t";

	public static UICardWindow_Com_SelectPoint CreateInstance()
	{
		BindAll();
		return (UICardWindow_Com_SelectPoint)UIPackage.CreateObject("Card", "CardWindow_Com_SelectPoint");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://bi8fdi6nhk2a1j", typeof(UICardWin_Com_Loader));
		UIObjectFactory.SetPackageItemExtension("ui://bi8fdi6njlir1b", typeof(UICardWin_Com_UseCard));
		UIObjectFactory.SetPackageItemExtension("ui://bi8fdi6np5xi2t", typeof(UICardWindow_Com_SelectPoint));
		UIObjectFactory.SetPackageItemExtension("ui://bi8fdi6nqgrb1v", typeof(UICardWin_Com_Loader_Lit));
		UIObjectFactory.SetPackageItemExtension("ui://bi8fdi6nqgrb1w", typeof(UICardWin_Com_CardResult));
		UIObjectFactory.SetPackageItemExtension("ui://bi8fdi6nqy402d", typeof(UICard_Button_DiceNumb));
		UIObjectFactory.SetPackageItemExtension("ui://bi8fdi6nu07q2s", typeof(UICard_Com_ChooseCard));
		UIObjectFactory.SetPackageItemExtension("ui://bi8fdi6nuhjc0", typeof(UICardWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		selectType = GetControllerAt(0);
		list_DiceNum = (GList)GetChildAt(0);
		btn_SurePoint = (GButton)GetChildAt(2);
		btn_OK = (GButton)GetChildAt(4);
		btn_Cancel = (GButton)GetChildAt(5);
	}
}
