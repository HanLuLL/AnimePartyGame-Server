using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Gift_Item : GButton
{
	public GLoader gitf_loader;

	public const string URL = "ui://hconmwfcy9qny";

	public static UIActivityComeback_Gift_Item CreateInstance()
	{
		return (UIActivityComeback_Gift_Item)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Gift_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		gitf_loader = (GLoader)GetChildAt(1);
	}
}
