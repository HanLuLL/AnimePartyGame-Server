using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Skin_Replica : GComponent
{
	public UIGacha_ProgressTab progress_tab;

	public UIGacha_Button_SupportPackage btn_SupportPackage;

	public UIGacha_Com_Replica skin_Desc1;

	public UIGacha_Com_Replica skin_Desc2;

	public UIGacha_Com_UpReplica skin_DescUp;

	public UICacha_Button_Gacha btn_GachaOne;

	public UICacha_Button_Gacha btn_GachaMulti;

	public const string URL = "ui://j90wpcmnvjqjqq3k";

	public static UIGacha_Skin_Replica CreateInstance()
	{
		return (UIGacha_Skin_Replica)UIPackage.CreateObject("Gacha", "Gacha_Skin_Replica");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		progress_tab = (UIGacha_ProgressTab)GetChildAt(0);
		btn_SupportPackage = (UIGacha_Button_SupportPackage)GetChildAt(1);
		skin_Desc1 = (UIGacha_Com_Replica)GetChildAt(2);
		skin_Desc2 = (UIGacha_Com_Replica)GetChildAt(3);
		skin_DescUp = (UIGacha_Com_UpReplica)GetChildAt(4);
		btn_GachaOne = (UICacha_Button_Gacha)GetChildAt(5);
		btn_GachaMulti = (UICacha_Button_Gacha)GetChildAt(6);
	}
}
