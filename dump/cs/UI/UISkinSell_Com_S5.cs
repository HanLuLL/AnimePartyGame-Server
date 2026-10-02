using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISkinSell_Com_S5 : GComponent
{
	public Controller language;

	public GLoader loader_Skin1Shadow;

	public GLoader loader_Skin1;

	public GLoader loader_Skin3Shadow;

	public GLoader loader_Skin3;

	public GLoader loader_Skin2Shadow;

	public GLoader loader_Skin2;

	public GButton btn_ShowSkin1;

	public GButton btn_ShowSkin2;

	public GButton btn_ShowSkin3;

	public GTextField txt_TimeTip;

	public GTextField txt_Explain;

	public UISkinSell_Button_Price btn_ShowDetail;

	public GButton btn_Close;

	public Transition HeroCutin;

	public Transition Cut_in_JP;

	public Transition Cut_In_EN;

	public Transition Cut_In_CN;

	public const string URL = "ui://hmljxqy1kvfs9q";

	public static UISkinSell_Com_S5 CreateInstance()
	{
		return (UISkinSell_Com_S5)UIPackage.CreateObject("SkinSell", "SkinSell_Com_S5");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		loader_Skin1Shadow = (GLoader)GetChildAt(0);
		loader_Skin1 = (GLoader)GetChildAt(1);
		loader_Skin3Shadow = (GLoader)GetChildAt(2);
		loader_Skin3 = (GLoader)GetChildAt(3);
		loader_Skin2Shadow = (GLoader)GetChildAt(4);
		loader_Skin2 = (GLoader)GetChildAt(5);
		btn_ShowSkin1 = (GButton)GetChildAt(63);
		btn_ShowSkin2 = (GButton)GetChildAt(64);
		btn_ShowSkin3 = (GButton)GetChildAt(65);
		txt_TimeTip = (GTextField)GetChildAt(68);
		txt_Explain = (GTextField)GetChildAt(69);
		btn_ShowDetail = (UISkinSell_Button_Price)GetChildAt(70);
		btn_Close = (GButton)GetChildAt(71);
		HeroCutin = GetTransitionAt(0);
		Cut_in_JP = GetTransitionAt(1);
		Cut_In_EN = GetTransitionAt(2);
		Cut_In_CN = GetTransitionAt(3);
	}
}
