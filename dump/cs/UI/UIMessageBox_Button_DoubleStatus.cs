using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMessageBox_Button_DoubleStatus : GButton
{
	public Controller selectedStatus;

	public GGraph di_0;

	public GImage di_1;

	public const string URL = "ui://fvoxvgwstiv3d";

	public static UIMessageBox_Button_DoubleStatus CreateInstance()
	{
		return (UIMessageBox_Button_DoubleStatus)UIPackage.CreateObject("MessageBox", "MessageBox_Button_DoubleStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		selectedStatus = GetControllerAt(1);
		di_0 = (GGraph)GetChildAt(0);
		di_1 = (GImage)GetChildAt(1);
	}
}
