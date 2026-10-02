using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Shop_Com_ItemMask : GComponent
{
	public GImage zhezhao;

	public const string URL = "ui://hconmwfcy9qnu";

	public static UIActivityComeback_Shop_Com_ItemMask CreateInstance()
	{
		return (UIActivityComeback_Shop_Com_ItemMask)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Shop_Com_ItemMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zhezhao = (GImage)GetChildAt(0);
	}
}
