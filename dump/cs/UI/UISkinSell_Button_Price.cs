using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISkinSell_Button_Price : GButton
{
	public Controller status;

	public Controller type;

	public GTextField txt_Title;

	public GRichTextField txt_OriginalPrice;

	public GRichTextField txt_DisscountPrice;

	public const string URL = "ui://hmljxqy1ub0x4";

	public static UISkinSell_Button_Price CreateInstance()
	{
		return (UISkinSell_Button_Price)UIPackage.CreateObject("SkinSell", "SkinSell_Button_Price");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		type = GetControllerAt(2);
		txt_Title = (GTextField)GetChildAt(4);
		txt_OriginalPrice = (GRichTextField)GetChildAt(6);
		txt_DisscountPrice = (GRichTextField)GetChildAt(7);
	}
}
