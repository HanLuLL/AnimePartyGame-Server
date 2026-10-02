using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWitchWeapon_Com_Goods : GComponent
{
	public UIWitchWeapon_Com_GoodsItem com_AngelChan;

	public UIWitchWeapon_Com_GoodsItem com_TangTang;

	public UIWitchWeapon_Com_GoodsItem com_Pack;

	public Transition Cut_in;

	public const string URL = "ui://hn2q98k6kqgj14";

	public static UIWitchWeapon_Com_Goods CreateInstance()
	{
		return (UIWitchWeapon_Com_Goods)UIPackage.CreateObject("WitchWeapon", "WitchWeapon_Com_Goods");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_AngelChan = (UIWitchWeapon_Com_GoodsItem)GetChildAt(0);
		com_TangTang = (UIWitchWeapon_Com_GoodsItem)GetChildAt(1);
		com_Pack = (UIWitchWeapon_Com_GoodsItem)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
