using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWitchWeapon_Com_Advert : GComponent
{
	public GLoader loader_TaiDao;

	public GLoader loader_XiuNv;

	public UIWitchWeapon_Com_CharacterInfo com_TaiDao;

	public UIWitchWeapon_Com_CharacterInfo com_XiuNv;

	public GLoader loader_Title;

	public UIWitchWeapon_Button_GoStore btn_GoStore;

	public Transition Cut_in;

	public const string URL = "ui://hn2q98k6kqgji";

	public static UIWitchWeapon_Com_Advert CreateInstance()
	{
		return (UIWitchWeapon_Com_Advert)UIPackage.CreateObject("WitchWeapon", "WitchWeapon_Com_Advert");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_TaiDao = (GLoader)GetChildAt(1);
		loader_XiuNv = (GLoader)GetChildAt(2);
		com_TaiDao = (UIWitchWeapon_Com_CharacterInfo)GetChildAt(3);
		com_XiuNv = (UIWitchWeapon_Com_CharacterInfo)GetChildAt(4);
		loader_Title = (GLoader)GetChildAt(6);
		btn_GoStore = (UIWitchWeapon_Button_GoStore)GetChildAt(7);
		Cut_in = GetTransitionAt(0);
	}
}
