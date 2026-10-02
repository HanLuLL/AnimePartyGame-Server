using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Toggle : GButton
{
	public GGraph di_0;

	public GImage di_1;

	public const string URL = "ui://vckl96ksru20ib";

	public static UIActivity_Button_Toggle CreateInstance()
	{
		return (UIActivity_Button_Toggle)UIPackage.CreateObject("Activity", "Activity_Button_Toggle");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di_0 = (GGraph)GetChildAt(0);
		di_1 = (GImage)GetChildAt(1);
	}
}
