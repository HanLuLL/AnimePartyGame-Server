using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Button_Acquisition : GButton
{
	public Controller redStatus;

	public Controller type;

	public GGraph zhezhao;

	public const string URL = "ui://hhpzjcmzh38b4e";

	public static UITask_Button_Acquisition CreateInstance()
	{
		return (UITask_Button_Acquisition)UIPackage.CreateObject("Task", "Task_Button_Acquisition");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
		type = GetControllerAt(2);
		zhezhao = (GGraph)GetChildAt(2);
	}
}
