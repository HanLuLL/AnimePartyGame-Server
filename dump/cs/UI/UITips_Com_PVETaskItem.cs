using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_PVETaskItem : GComponent
{
	public Controller isComplete;

	public GTextField txt_Title;

	public GTextField txt_Progress;

	public GTextField txt_Desc;

	public GTextField txt_Desc_Failed;

	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8mot0ws87";

	public static UITips_Com_PVETaskItem CreateInstance()
	{
		return (UITips_Com_PVETaskItem)UIPackage.CreateObject("Tips", "Tips_Com_PVETaskItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isComplete = GetControllerAt(0);
		txt_Title = (GTextField)GetChildAt(2);
		txt_Progress = (GTextField)GetChildAt(3);
		txt_Desc = (GTextField)GetChildAt(4);
		txt_Desc_Failed = (GTextField)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
