using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPropDetail_Com_ChestItem : GComponent
{
	public GButton btn_Item;

	public const string URL = "ui://307oxfbquins17";

	public static UIPropDetail_Com_ChestItem CreateInstance()
	{
		return (UIPropDetail_Com_ChestItem)UIPackage.CreateObject("PropDetail", "PropDetail_Com_ChestItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Item = (GButton)GetChildAt(0);
	}
}
