using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGachaInfoWindow : GComponent
{
	public Controller type;

	public Controller ShowSkip;

	public GGraph btn_CloseInfo;

	public GLabel bottom;

	public UIGachaInfo_Com_PoolDetailInfo com_Info;

	public UIGachaInfo_Com_Record com_Record;

	public GGraph loader_Movie;

	public GButton btn_Skip;

	public GLoader loader_Character;

	public UIGachaInfo_Com_ShowSkin com_ShowSkin;

	public Transition ShowCharacter;

	public const string URL = "ui://egrtucyhot0w0";

	public static UIGachaInfoWindow CreateInstance()
	{
		BindAll();
		return (UIGachaInfoWindow)UIPackage.CreateObject("GachaInfo", "GachaInfoWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://egrtucyhnu3za", typeof(UIGachaInfo_Com_ShowSkin));
		UIObjectFactory.SetPackageItemExtension("ui://egrtucyhot0w0", typeof(UIGachaInfoWindow));
		UIObjectFactory.SetPackageItemExtension("ui://egrtucyhot0w1", typeof(UIGachaInfo_Com_PoolDetailInfo));
		UIObjectFactory.SetPackageItemExtension("ui://egrtucyhot0w2", typeof(UICachaInfo_Com_GroupInfo));
		UIObjectFactory.SetPackageItemExtension("ui://egrtucyhot0w3", typeof(UIGachaInfo_Button_PropItem));
		UIObjectFactory.SetPackageItemExtension("ui://egrtucyhot0w6", typeof(UIGachaInfo_Com_Record));
		UIObjectFactory.SetPackageItemExtension("ui://egrtucyhot0w7", typeof(UIGachaInfo_Com_RecordItem));
		UIObjectFactory.SetPackageItemExtension("ui://egrtucyhu2w5j", typeof(UIGachaInfo_Com_Flash));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		ShowSkip = GetControllerAt(1);
		btn_CloseInfo = (GGraph)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		com_Info = (UIGachaInfo_Com_PoolDetailInfo)GetChildAt(2);
		com_Record = (UIGachaInfo_Com_Record)GetChildAt(4);
		loader_Movie = (GGraph)GetChildAt(5);
		btn_Skip = (GButton)GetChildAt(6);
		loader_Character = (GLoader)GetChildAt(7);
		com_ShowSkin = (UIGachaInfo_Com_ShowSkin)GetChildAt(9);
		ShowCharacter = GetTransitionAt(0);
	}
}
