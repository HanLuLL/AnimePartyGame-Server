using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Shop_Com_ItemBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://hconmwfcy9qns";

	public static UIActivityComeback_Shop_Com_ItemBg CreateInstance()
	{
		return (UIActivityComeback_Shop_Com_ItemBg)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Shop_Com_ItemBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
