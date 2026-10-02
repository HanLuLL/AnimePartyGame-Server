using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPurchase_Com_ContentType : GComponent
{
	public GTextField txt_ContentType;

	public const string URL = "ui://cu17piy4tb9e18";

	public static UIPurchase_Com_ContentType CreateInstance()
	{
		return (UIPurchase_Com_ContentType)UIPackage.CreateObject("Purchase", "Purchase_Com_ContentType");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_ContentType = (GTextField)GetChildAt(2);
	}
}
