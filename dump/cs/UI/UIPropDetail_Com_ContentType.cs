using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPropDetail_Com_ContentType : GComponent
{
	public GTextField txt_ContentType;

	public const string URL = "ui://307oxfbqhg7x1p";

	public static UIPropDetail_Com_ContentType CreateInstance()
	{
		return (UIPropDetail_Com_ContentType)UIPackage.CreateObject("PropDetail", "PropDetail_Com_ContentType");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_ContentType = (GTextField)GetChildAt(2);
	}
}
