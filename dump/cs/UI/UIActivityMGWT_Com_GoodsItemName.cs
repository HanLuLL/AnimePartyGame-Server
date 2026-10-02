using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMGWT_Com_GoodsItemName : GComponent
{
	public GTextField txt_Name;

	public const string URL = "ui://wdl8l4hslwm1z";

	public static UIActivityMGWT_Com_GoodsItemName CreateInstance()
	{
		return (UIActivityMGWT_Com_GoodsItemName)UIPackage.CreateObject("ActivityMGWT", "ActivityMGWT_Com_GoodsItemName");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Name = (GTextField)GetChildAt(0);
	}
}
