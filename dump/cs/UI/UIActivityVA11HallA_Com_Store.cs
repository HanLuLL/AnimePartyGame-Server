using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityVA11HallA_Com_Store : GComponent
{
	public GList list_Store_Goods;

	public GTextField txt_timeTip;

	public const string URL = "ui://zlysd2gupgzf4y";

	public static UIActivityVA11HallA_Com_Store CreateInstance()
	{
		return (UIActivityVA11HallA_Com_Store)UIPackage.CreateObject("ActivityVA11HallA", "ActivityVA11HallA_Com_Store");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Store_Goods = (GList)GetChildAt(3);
		txt_timeTip = (GTextField)GetChildAt(4);
	}
}
