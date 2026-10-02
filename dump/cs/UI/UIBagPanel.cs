using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBagPanel : GComponent
{
	public Controller sortType;

	public Controller isMoreBelow;

	public GButton btn_Return;

	public UIBag_Com_Tab com_Tab;

	public GList list_Items;

	public GButton btn_SortUp;

	public GButton btn_SortDown;

	public Transition cut_in;

	public Transition BG;

	public const string URL = "ui://p0iy0chjl8aa0";

	public static UIBagPanel CreateInstance()
	{
		BindAll();
		return (UIBagPanel)UIPackage.CreateObject("Bag", "BagPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://p0iy0chj73il5", typeof(UIBag_Com_Item));
		UIObjectFactory.SetPackageItemExtension("ui://p0iy0chjja019", typeof(UIBag_Com_Tab));
		UIObjectFactory.SetPackageItemExtension("ui://p0iy0chjl8aa0", typeof(UIBagPanel));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		sortType = GetControllerAt(0);
		isMoreBelow = GetControllerAt(1);
		btn_Return = (GButton)GetChildAt(5);
		com_Tab = (UIBag_Com_Tab)GetChildAt(6);
		list_Items = (GList)GetChildAt(8);
		btn_SortUp = (GButton)GetChildAt(10);
		btn_SortDown = (GButton)GetChildAt(11);
		cut_in = GetTransitionAt(0);
		BG = GetTransitionAt(1);
	}
}
