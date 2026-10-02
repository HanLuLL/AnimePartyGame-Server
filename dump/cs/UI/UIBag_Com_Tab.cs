using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBag_Com_Tab : GComponent
{
	public GList list;

	public GTextField txt_Select;

	public Transition hi;

	public const string URL = "ui://p0iy0chjja019";

	public static UIBag_Com_Tab CreateInstance()
	{
		return (UIBag_Com_Tab)UIPackage.CreateObject("Bag", "Bag_Com_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list = (GList)GetChildAt(2);
		txt_Select = (GTextField)GetChildAt(7);
		hi = GetTransitionAt(0);
	}
}
