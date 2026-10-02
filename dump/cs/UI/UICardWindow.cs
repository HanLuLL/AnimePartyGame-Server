using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICardWindow : GComponent
{
	public Controller showContent;

	public UICardWin_Com_UseCard com_UseCard;

	public UICardWin_Com_CardResult com_CardResult;

	public UICard_Com_ChooseCard com_ChooseCard;

	public const string URL = "ui://bi8fdi6nuhjc0";

	public static UICardWindow CreateInstance()
	{
		BindAll();
		return (UICardWindow)UIPackage.CreateObject("Card", "CardWindow");
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
		showContent = GetControllerAt(0);
		com_UseCard = (UICardWin_Com_UseCard)GetChildAt(0);
		com_CardResult = (UICardWin_Com_CardResult)GetChildAt(1);
		com_ChooseCard = (UICard_Com_ChooseCard)GetChildAt(2);
	}
}
