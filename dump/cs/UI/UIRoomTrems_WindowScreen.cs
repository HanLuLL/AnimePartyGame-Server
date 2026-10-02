using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomTrems_WindowScreen : GComponent
{
	public GLoader bg_btn;

	public GComponent bg;

	public GList list_terms;

	public UIRoomTrems_WinScreen_CloseBtn close_btn;

	public GButton btn_prePage;

	public GButton btn_nextPage;

	public Transition Cut_in;

	public Transition Cut_out;

	public const string URL = "ui://tzpop51dejjwu";

	public static UIRoomTrems_WindowScreen CreateInstance()
	{
		BindAll();
		return (UIRoomTrems_WindowScreen)UIPackage.CreateObject("RoomTerms", "RoomTrems_WindowScreen");
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
		bg_btn = (GLoader)GetChildAt(0);
		bg = (GComponent)GetChildAt(1);
		list_terms = (GList)GetChildAt(2);
		close_btn = (UIRoomTrems_WinScreen_CloseBtn)GetChildAt(3);
		btn_prePage = (GButton)GetChildAt(4);
		btn_nextPage = (GButton)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
		Cut_out = GetTransitionAt(1);
	}
}
