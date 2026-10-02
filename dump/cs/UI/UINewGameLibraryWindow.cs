using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibraryWindow : GComponent
{
	public Controller label;

	public GGraph loader_BG;

	public GLoader loader_Line;

	public GLoader loader_SystemBG;

	public GButton btn_Return;

	public UINewGameLibrary_Cards com_Cords;

	public UINewGameLibrary_Lands com_Lands;

	public UINewGameLibrary_Relic com_Relic;

	public UINewGameLibrary_Map com_Map;

	public UINewGameLibray_btn_relic Btn_Relic;

	public UINewGameLibray_btn_Map Btn_Map;

	public UINewGameLibray_btn_Event Btn_Event;

	public UINewGameLibray_btn_Land Btn_Land;

	public UINewGameLibray_btn_code Btn_Code;

	public Transition Cut_in;

	public const string URL = "ui://mc0y3plupj0z0";

	public static UINewGameLibraryWindow CreateInstance()
	{
		BindAll();
		return (UINewGameLibraryWindow)UIPackage.CreateObject("NewGameLibrary", "NewGameLibraryWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z0", typeof(UINewGameLibraryWindow));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1", typeof(UINewGameLibrary_Button_Card));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1h", typeof(UINewGameLibray_btn_relic));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1i", typeof(UINewGameLibray_btn_code));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1j", typeof(UINewGameLibray_btn_Land));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1k", typeof(UINewGameLibray_btn_Event));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1l", typeof(UINewGameLibray_btn_Map));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1m", typeof(UINewGameLibrary_Cards));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1n", typeof(UINewGameLibrary_btn_search));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1p", typeof(UINewGameLibrary_Lands));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1q", typeof(UINewGameLibrary_Map));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1r", typeof(UINewGameLibrary_Relic));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z1t", typeof(UINewGameLibrary_Button_MapItem));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z20", typeof(UINewGameLibrary_Button_GameMode));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z25", typeof(UINewGameLibrary_Com_MapMain));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z2b", typeof(UINewGamelibrary_Com_taball));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z2e", typeof(UINewGameLibrary_Com_Banner));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z2i", typeof(UINewGameLibrary_Com_NewMonsterItem));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z2l", typeof(UINewGameLibrary_Btn_MonsterItem));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z2m", typeof(UINewGameLibrary_Com_MapTab));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z4", typeof(UINewGameLibrary_Button_RelicItem));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z6", typeof(UINewGameLibrary_Com_Monster));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plupj0z7", typeof(UINewGameLibrary_Com_Difficulty));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plus8o93n", typeof(UINewGameLibrary_Com_ComboBox));
		UIObjectFactory.SetPackageItemExtension("ui://mc0y3plus8o93q", typeof(UINewGameLibrary_Com_ComboBox_popup));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		label = GetControllerAt(0);
		loader_BG = (GGraph)GetChildAt(1);
		loader_Line = (GLoader)GetChildAt(2);
		loader_SystemBG = (GLoader)GetChildAt(3);
		btn_Return = (GButton)GetChildAt(4);
		com_Cords = (UINewGameLibrary_Cards)GetChildAt(5);
		com_Lands = (UINewGameLibrary_Lands)GetChildAt(6);
		com_Relic = (UINewGameLibrary_Relic)GetChildAt(7);
		com_Map = (UINewGameLibrary_Map)GetChildAt(8);
		Btn_Relic = (UINewGameLibray_btn_relic)GetChildAt(9);
		Btn_Map = (UINewGameLibray_btn_Map)GetChildAt(10);
		Btn_Event = (UINewGameLibray_btn_Event)GetChildAt(11);
		Btn_Land = (UINewGameLibray_btn_Land)GetChildAt(12);
		Btn_Code = (UINewGameLibray_btn_code)GetChildAt(13);
		Cut_in = GetTransitionAt(0);
	}
}
