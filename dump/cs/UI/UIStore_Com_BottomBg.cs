using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_BottomBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://zyd0rl00r3jjqq4i";

	public static UIStore_Com_BottomBg CreateInstance()
	{
		return (UIStore_Com_BottomBg)UIPackage.CreateObject("Store", "Store_Com_BottomBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
