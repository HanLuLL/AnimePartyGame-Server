using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_Scratchoff_Lin : GComponent
{
	public Controller language;

	public GList list_Preview;

	public GTextField txt_Desc;

	public GList list_Scratchoff;

	public UIActivity_Com_Type5_Button_Purchase_Lin btn_Purchase;

	public UIActivity_Com_Type5_Button_Light_Lin btn_Light;

	public GButton btn_DetailInfo;

	public const string URL = "ui://vckl96ksxcsllsq76";

	public static UIActivity_Com_Type5_Scratchoff_Lin CreateInstance()
	{
		return (UIActivity_Com_Type5_Scratchoff_Lin)UIPackage.CreateObject("Activity", "Activity_Com_Type5_Scratchoff_Lin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		list_Preview = (GList)GetChildAt(12);
		txt_Desc = (GTextField)GetChildAt(13);
		list_Scratchoff = (GList)GetChildAt(14);
		btn_Purchase = (UIActivity_Com_Type5_Button_Purchase_Lin)GetChildAt(15);
		btn_Light = (UIActivity_Com_Type5_Button_Light_Lin)GetChildAt(16);
		btn_DetailInfo = (GButton)GetChildAt(17);
	}
}
