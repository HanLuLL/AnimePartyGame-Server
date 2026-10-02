using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type2 : GComponent
{
	public Controller pageType;

	public GLoader loader_BG;

	public UIActivity_Com_Type2_Main com_Task;

	public UIActivity_Com_Type2_Scratchoff com_Scratchoff;

	public const string URL = "ui://vckl96ksjzxa1d";

	public static UIActivity_Com_Type2 CreateInstance()
	{
		return (UIActivity_Com_Type2)UIPackage.CreateObject("Activity", "Activity_Com_Type2");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		pageType = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		com_Task = (UIActivity_Com_Type2_Main)GetChildAt(1);
		com_Scratchoff = (UIActivity_Com_Type2_Scratchoff)GetChildAt(2);
	}
}
