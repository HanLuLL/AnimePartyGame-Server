using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreTwo_Com_BottomBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://6dt5s4htqw8h12";

	public static UIActivityStoreTwo_Com_BottomBg CreateInstance()
	{
		return (UIActivityStoreTwo_Com_BottomBg)UIPackage.CreateObject("ActivityStoreTwo", "ActivityStoreTwo_Com_BottomBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
