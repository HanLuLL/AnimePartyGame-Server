using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Shop_Com_BottomBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://hconmwfcy9qnm";

	public static UIActivityComeback_Shop_Com_BottomBg CreateInstance()
	{
		return (UIActivityComeback_Shop_Com_BottomBg)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Shop_Com_BottomBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
