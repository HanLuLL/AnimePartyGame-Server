using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomTrems_AddTremWindow : GComponent
{
	public GLoader bg_btn;

	public GComponent bg;

	public GButton btn_close;

	public UIRoomTerms_TermItem com_term;

	public GTextField txt_desc;

	public Transition Cut_in;

	public Transition Cut_out;

	public const string URL = "ui://tzpop51d105pl18";

	public static UIRoomTrems_AddTremWindow CreateInstance()
	{
		BindAll();
		return (UIRoomTrems_AddTremWindow)UIPackage.CreateObject("RoomTerms", "RoomTrems_AddTremWindow");
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
		btn_close = (GButton)GetChildAt(8);
		com_term = (UIRoomTerms_TermItem)GetChildAt(9);
		txt_desc = (GTextField)GetChildAt(11);
		Cut_in = GetTransitionAt(0);
		Cut_out = GetTransitionAt(1);
	}
}
