using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPurchase_Com_ChestItem : GComponent
{
	public GButton btn_Item;

	public const string URL = "ui://cu17piy4tb9e17";

	public static UIPurchase_Com_ChestItem CreateInstance()
	{
		return (UIPurchase_Com_ChestItem)UIPackage.CreateObject("Purchase", "Purchase_Com_ChestItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Item = (GButton)GetChildAt(0);
	}
}
