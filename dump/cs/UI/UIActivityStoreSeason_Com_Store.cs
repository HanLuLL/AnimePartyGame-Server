using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_Store : GComponent
{
	public UIActivityStoreSeason_Com_BottomBg buttom;

	public UIActivityStoreSeason_Com_ItemBg bg;

	public GList list_Store_Goods;

	public GTextField txt_timeTip;

	public const string URL = "ui://begz6gfv7vwms";

	public static UIActivityStoreSeason_Com_Store CreateInstance()
	{
		return (UIActivityStoreSeason_Com_Store)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_Store");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		buttom = (UIActivityStoreSeason_Com_BottomBg)GetChildAt(0);
		bg = (UIActivityStoreSeason_Com_ItemBg)GetChildAt(2);
		list_Store_Goods = (GList)GetChildAt(3);
		txt_timeTip = (GTextField)GetChildAt(4);
	}
}
