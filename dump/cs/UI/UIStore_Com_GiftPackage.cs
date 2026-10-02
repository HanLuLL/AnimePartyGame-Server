using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_GiftPackage : GComponent
{
	public UIStore_Com_BottomBg buttom;

	public UIStore_Com_ItemBg bg;

	public GList list_GiftPackage;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00vs31qq4w";

	public static UIStore_Com_GiftPackage CreateInstance()
	{
		return (UIStore_Com_GiftPackage)UIPackage.CreateObject("Store", "Store_Com_GiftPackage");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		buttom = (UIStore_Com_BottomBg)GetChildAt(0);
		bg = (UIStore_Com_ItemBg)GetChildAt(2);
		list_GiftPackage = (GList)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
