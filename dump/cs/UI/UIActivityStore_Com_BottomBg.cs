using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Com_BottomBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://88m1yfwgtik52v";

	public static UIActivityStore_Com_BottomBg CreateInstance()
	{
		return (UIActivityStore_Com_BottomBg)UIPackage.CreateObject("ActivityStore", "ActivityStore_Com_BottomBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
