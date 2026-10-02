using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_GetStatus : GComponent
{
	public Controller FinishStatus;

	public UIStore_Button_GetStatusBottom com_progress;

	public const string URL = "ui://zyd0rl0011biqq21";

	public static UIStore_Button_GetStatus CreateInstance()
	{
		return (UIStore_Button_GetStatus)UIPackage.CreateObject("Store", "Store_Button_GetStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		FinishStatus = GetControllerAt(0);
		com_progress = (UIStore_Button_GetStatusBottom)GetChildAt(0);
	}
}
