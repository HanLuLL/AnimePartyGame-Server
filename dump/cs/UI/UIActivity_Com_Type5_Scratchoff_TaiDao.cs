using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_Scratchoff_TaiDao : GComponent
{
	public Controller language;

	public GList list_Preview;

	public GTextField txt_Desc;

	public GList list_Scratchoff;

	public UIActivity_Com_Type5_Button_Purchase_TaiDao btn_Purchase;

	public UIActivity_Com_Type5_Button_Light_TaiDao btn_Light;

	public GButton btn_DetailInfo;

	public const string URL = "ui://vckl96ksyjnjlsq88";

	public static UIActivity_Com_Type5_Scratchoff_TaiDao CreateInstance()
	{
		return (UIActivity_Com_Type5_Scratchoff_TaiDao)UIPackage.CreateObject("Activity", "Activity_Com_Type5_Scratchoff_TaiDao");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		list_Preview = (GList)GetChildAt(9);
		txt_Desc = (GTextField)GetChildAt(10);
		list_Scratchoff = (GList)GetChildAt(11);
		btn_Purchase = (UIActivity_Com_Type5_Button_Purchase_TaiDao)GetChildAt(12);
		btn_Light = (UIActivity_Com_Type5_Button_Light_TaiDao)GetChildAt(13);
		btn_DetailInfo = (GButton)GetChildAt(14);
	}
}
