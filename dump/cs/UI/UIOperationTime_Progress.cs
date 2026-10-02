using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIOperationTime_Progress : GProgressBar
{
	public Controller type;

	public GLoader loader_Head;

	public const string URL = "ui://vigyylk7mt9n1";

	public static UIOperationTime_Progress CreateInstance()
	{
		return (UIOperationTime_Progress)UIPackage.CreateObject("OperateTime", "OperationTime_Progress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		loader_Head = (GLoader)GetChildAt(1);
	}
}
