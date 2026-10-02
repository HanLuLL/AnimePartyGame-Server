using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINGOStore_Com_Advert : GComponent
{
	public Controller titleType;

	public GLoader loader_AngelChanSkin_Color;

	public GLoader loader_AngelChanSkin;

	public GLoader loader_AngelChan_Color;

	public GLoader loader_AngelChan;

	public GLoader loader_TangTang_Color;

	public GLoader loader_TangTang;

	public GLoader loader_TangTangSkin_Color;

	public GLoader loader_TangTangSkin;

	public GLoader loader_Icon;

	public UINGOStore_Com_CharacterInfo com_AngelChanSkin;

	public UINGOStore_Com_CharacterInfo com_TangTangSkin;

	public UINGOStore_Com_CharacterInfo com_TangTang;

	public UINGOStore_Com_CharacterInfo com_AngelChan;

	public UINGOStore_Button_GoStore btn_GoSkinStore;

	public UINGOStore_Button_GoStore btn_GoStore;

	public UINGOStore_Button_PreviewSkin btn_Preview;

	public UINGOStore_Button_PreviewSkin btn_Preview2;

	public UINGOStore_Button_PreviewSkin btn_Preview3;

	public UINGOStore_Button_PreviewSkin btn_Preview4;

	public Transition Cut_in;

	public const string URL = "ui://na6sy4s6kqgji";

	public static UINGOStore_Com_Advert CreateInstance()
	{
		return (UINGOStore_Com_Advert)UIPackage.CreateObject("NGOStore", "NGOStore_Com_Advert");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		titleType = GetControllerAt(0);
		loader_AngelChanSkin_Color = (GLoader)GetChildAt(35);
		loader_AngelChanSkin = (GLoader)GetChildAt(36);
		loader_AngelChan_Color = (GLoader)GetChildAt(38);
		loader_AngelChan = (GLoader)GetChildAt(39);
		loader_TangTang_Color = (GLoader)GetChildAt(41);
		loader_TangTang = (GLoader)GetChildAt(42);
		loader_TangTangSkin_Color = (GLoader)GetChildAt(44);
		loader_TangTangSkin = (GLoader)GetChildAt(45);
		loader_Icon = (GLoader)GetChildAt(49);
		com_AngelChanSkin = (UINGOStore_Com_CharacterInfo)GetChildAt(54);
		com_TangTangSkin = (UINGOStore_Com_CharacterInfo)GetChildAt(55);
		com_TangTang = (UINGOStore_Com_CharacterInfo)GetChildAt(56);
		com_AngelChan = (UINGOStore_Com_CharacterInfo)GetChildAt(57);
		btn_GoSkinStore = (UINGOStore_Button_GoStore)GetChildAt(58);
		btn_GoStore = (UINGOStore_Button_GoStore)GetChildAt(59);
		btn_Preview = (UINGOStore_Button_PreviewSkin)GetChildAt(60);
		btn_Preview2 = (UINGOStore_Button_PreviewSkin)GetChildAt(61);
		btn_Preview3 = (UINGOStore_Button_PreviewSkin)GetChildAt(62);
		btn_Preview4 = (UINGOStore_Button_PreviewSkin)GetChildAt(63);
		Cut_in = GetTransitionAt(0);
	}
}
