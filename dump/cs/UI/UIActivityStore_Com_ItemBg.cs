using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Com_ItemBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://88m1yfwgtik531";

	public static UIActivityStore_Com_ItemBg CreateInstance()
	{
		return (UIActivityStore_Com_ItemBg)UIPackage.CreateObject("ActivityStore", "ActivityStore_Com_ItemBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
