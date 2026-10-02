using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_GetStatusBottom : GComponent
{
	public Controller status;

	public const string URL = "ui://zyd0rl0011biq1p";

	public static UIStore_Button_GetStatusBottom CreateInstance()
	{
		return (UIStore_Button_GetStatusBottom)UIPackage.CreateObject("Store", "Store_Button_GetStatusBottom");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
	}
}
