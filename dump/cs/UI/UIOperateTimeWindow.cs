using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIOperateTimeWindow : GComponent
{
	public GGraph graph_warning;

	public UIOperationTime_Progress progress_OperationTime;

	public const string URL = "ui://vigyylk7mt9n0";

	public static UIOperateTimeWindow CreateInstance()
	{
		BindAll();
		return (UIOperateTimeWindow)UIPackage.CreateObject("OperateTime", "OperateTimeWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://vigyylk7mt9n0", typeof(UIOperateTimeWindow));
		UIObjectFactory.SetPackageItemExtension("ui://vigyylk7mt9n1", typeof(UIOperationTime_Progress));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_warning = (GGraph)GetChildAt(0);
		progress_OperationTime = (UIOperationTime_Progress)GetChildAt(1);
	}
}
