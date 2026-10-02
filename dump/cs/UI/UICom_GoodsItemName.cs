using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_GoodsItemName : GComponent
{
	public GTextField txt_Name;

	public const string URL = "ui://m6sn3r22o1g0c4";

	public static UICom_GoodsItemName CreateInstance()
	{
		return (UICom_GoodsItemName)UIPackage.CreateObject("Common_External", "Com_GoodsItemName");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Name = (GTextField)GetChildAt(0);
	}
}
