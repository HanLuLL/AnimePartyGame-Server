using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type4 : GComponent
{
	public Controller pageType;

	public GGraph loader_BG;

	public UIActivity_Com_Type4_Main com_Task;

	public UIActivity_Com_Type4_Scratchoff com_Scratchoff;

	public const string URL = "ui://c1v285vtpu2i0";

	public static UIActivity_Com_Type4 CreateInstance()
	{
		return (UIActivity_Com_Type4)UIPackage.CreateObject("ActivityNgo", "Activity_Com_Type4");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		pageType = GetControllerAt(0);
		loader_BG = (GGraph)GetChildAt(0);
		com_Task = (UIActivity_Com_Type4_Main)GetChildAt(1);
		com_Scratchoff = (UIActivity_Com_Type4_Scratchoff)GetChildAt(2);
	}
}
