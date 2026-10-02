using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_Gacha : GComponent
{
	public GLoader loader_Gacha_BG;

	public GTextField txt_Gacha_SkinName;

	public GTextField txt_Gacha_timeTip;

	public GGraph loader_UpAnimation;

	public GButton btn_Gacha_PreviewSkin;

	public GButton btn_Gacha_Detail;

	public GButton btn_Gacha_Recoard;

	public UIActivityStoreSeason_Button_Gacha btn_Gahca_Once;

	public UIActivityStoreSeason_Button_Gacha btn_Gahca_Multi;

	public UIActivityStoreSeason_Button_AddGift btn_AddGift;

	public GLoader loader_Element1;

	public GLoader loader_Element2;

	public const string URL = "ui://begz6gfv7vwm13";

	public static UIActivityStoreSeason_Com_Gacha CreateInstance()
	{
		return (UIActivityStoreSeason_Com_Gacha)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_Gacha");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Gacha_BG = (GLoader)GetChildAt(0);
		txt_Gacha_SkinName = (GTextField)GetChildAt(1);
		txt_Gacha_timeTip = (GTextField)GetChildAt(2);
		loader_UpAnimation = (GGraph)GetChildAt(3);
		btn_Gacha_PreviewSkin = (GButton)GetChildAt(4);
		btn_Gacha_Detail = (GButton)GetChildAt(5);
		btn_Gacha_Recoard = (GButton)GetChildAt(6);
		btn_Gahca_Once = (UIActivityStoreSeason_Button_Gacha)GetChildAt(7);
		btn_Gahca_Multi = (UIActivityStoreSeason_Button_Gacha)GetChildAt(8);
		btn_AddGift = (UIActivityStoreSeason_Button_AddGift)GetChildAt(9);
		loader_Element1 = (GLoader)GetChildAt(10);
		loader_Element2 = (GLoader)GetChildAt(11);
	}
}
