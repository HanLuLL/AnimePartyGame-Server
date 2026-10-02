using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type2_Scratchoff : GButton
{
	public Controller redPoint;

	public GLoader loader_Prop;

	public GTextField txt_Progress;

	public GTextField txt_Num;

	public const string URL = "ui://vckl96ksjzxa1o";

	public static UIActivity_Button_Type2_Scratchoff CreateInstance()
	{
		return (UIActivity_Button_Type2_Scratchoff)UIPackage.CreateObject("Activity", "Activity_Button_Type2_Scratchoff");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		loader_Prop = (GLoader)GetChildAt(1);
		txt_Progress = (GTextField)GetChildAt(4);
		txt_Num = (GTextField)GetChildAt(5);
	}
}
