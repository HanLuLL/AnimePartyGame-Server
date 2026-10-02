using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreTwo_Com_ItemBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://6dt5s4htqw8h18";

	public static UIActivityStoreTwo_Com_ItemBg CreateInstance()
	{
		return (UIActivityStoreTwo_Com_ItemBg)UIPackage.CreateObject("ActivityStoreTwo", "ActivityStoreTwo_Com_ItemBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
