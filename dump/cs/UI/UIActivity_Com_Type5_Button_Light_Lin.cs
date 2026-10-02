using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_Button_Light_Lin : GButton
{
	public GTextField txt_Title;

	public GTextField txt_Desc;

	public const string URL = "ui://vckl96ksxcsllsq7p";

	public static UIActivity_Com_Type5_Button_Light_Lin CreateInstance()
	{
		return (UIActivity_Com_Type5_Button_Light_Lin)UIPackage.CreateObject("Activity", "Activity_Com_Type5_Button_Light_Lin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(1);
		txt_Desc = (GTextField)GetChildAt(2);
	}
}
