using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_FinishStatus : GComponent
{
	public Controller status;

	public const string URL = "ui://zyd0rl0011biq1q";

	public static UIStore_Button_FinishStatus CreateInstance()
	{
		return (UIStore_Button_FinishStatus)UIPackage.CreateObject("Store", "Store_Button_FinishStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
	}
}
