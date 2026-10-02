using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomTermsWindow : GComponent
{
	public Controller showType;

	public UIRoomTerms_FullScreenTip com_FullScreenTip;

	public UIRoomTrems_WindowScreen com_WinScreen;

	public UIRoomTrems_AddTremWindow com_AddTermWindow;

	public const string URL = "ui://tzpop51do3tt0";

	public static UIRoomTermsWindow CreateInstance()
	{
		BindAll();
		return (UIRoomTermsWindow)UIPackage.CreateObject("RoomTerms", "RoomTermsWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://tzpop51d105pl18", typeof(UIRoomTrems_AddTremWindow));
		UIObjectFactory.SetPackageItemExtension("ui://tzpop51dejjwb", typeof(UIRoomTerms_FullScreenTip));
		UIObjectFactory.SetPackageItemExtension("ui://tzpop51dejjwj", typeof(UIRoomTerms_ScrollBar_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://tzpop51dejjwr", typeof(UIRoomTrems_WinScreen_CloseBtn));
		UIObjectFactory.SetPackageItemExtension("ui://tzpop51dejjwu", typeof(UIRoomTrems_WindowScreen));
		UIObjectFactory.SetPackageItemExtension("ui://tzpop51di0pfl1e", typeof(UIRoomTerms_TermItemContent));
		UIObjectFactory.SetPackageItemExtension("ui://tzpop51do3tt0", typeof(UIRoomTermsWindow));
		UIObjectFactory.SetPackageItemExtension("ui://tzpop51do3tt1", typeof(UIRoomTerms_TermItem));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showType = GetControllerAt(0);
		com_FullScreenTip = (UIRoomTerms_FullScreenTip)GetChildAt(0);
		com_WinScreen = (UIRoomTrems_WindowScreen)GetChildAt(1);
		com_AddTermWindow = (UIRoomTrems_AddTremWindow)GetChildAt(2);
	}
}
