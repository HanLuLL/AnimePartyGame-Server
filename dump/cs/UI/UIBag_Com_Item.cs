using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBag_Com_Item : GComponent
{
	public Controller isDeleteByTime;

	public Controller isNew;

	public Controller isEmpty;

	public Controller recycle;

	public GButton btn_item;

	public const string URL = "ui://p0iy0chj73il5";

	public static UIBag_Com_Item CreateInstance()
	{
		return (UIBag_Com_Item)UIPackage.CreateObject("Bag", "Bag_Com_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isDeleteByTime = GetControllerAt(0);
		isNew = GetControllerAt(1);
		isEmpty = GetControllerAt(2);
		recycle = GetControllerAt(3);
		btn_item = (GButton)GetChildAt(0);
	}
}
