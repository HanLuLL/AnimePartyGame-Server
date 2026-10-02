using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Com_UpDesc : GComponent
{
	public GTextField txt_UpNick;

	public GTextField txt_UpName;

	public GTextField txt_UpDesc;

	public GButton btn_preview;

	public const string URL = "ui://j90wpcmn11biq21";

	public static UIGacha_Com_UpDesc CreateInstance()
	{
		return (UIGacha_Com_UpDesc)UIPackage.CreateObject("Gacha", "Gacha_Com_UpDesc");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_UpNick = (GTextField)GetChildAt(0);
		txt_UpName = (GTextField)GetChildAt(1);
		txt_UpDesc = (GTextField)GetChildAt(2);
		btn_preview = (GButton)GetChildAt(3);
	}
}
