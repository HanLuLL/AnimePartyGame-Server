using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreTwo_Com_ItemMask : GComponent
{
	public GImage zhezhao;

	public const string URL = "ui://6dt5s4htqw8h1a";

	public static UIActivityStoreTwo_Com_ItemMask CreateInstance()
	{
		return (UIActivityStoreTwo_Com_ItemMask)UIPackage.CreateObject("ActivityStoreTwo", "ActivityStoreTwo_Com_ItemMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zhezhao = (GImage)GetChildAt(0);
	}
}
