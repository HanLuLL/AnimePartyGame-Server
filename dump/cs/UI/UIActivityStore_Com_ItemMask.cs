using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Com_ItemMask : GComponent
{
	public GImage zhezhao;

	public const string URL = "ui://88m1yfwgtik533";

	public static UIActivityStore_Com_ItemMask CreateInstance()
	{
		return (UIActivityStore_Com_ItemMask)UIPackage.CreateObject("ActivityStore", "ActivityStore_Com_ItemMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zhezhao = (GImage)GetChildAt(0);
	}
}
