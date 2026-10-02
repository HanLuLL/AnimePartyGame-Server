using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMGWT_Com_CommonItem : GComponent
{
	public GLoader loader_Icon;

	public GTextField txt_title;

	public const string URL = "ui://2p754tqkilj51a";

	public static UIMGWT_Com_CommonItem CreateInstance()
	{
		return (UIMGWT_Com_CommonItem)UIPackage.CreateObject("MGWTStore", "MGWT_Com_CommonItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(0);
		txt_title = (GTextField)GetChildAt(1);
	}
}
