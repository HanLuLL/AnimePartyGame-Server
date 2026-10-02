using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_XRPreRevard : GComponent
{
	public Controller status;

	public GButton btn_Item;

	public const string URL = "ui://vckl96ksize7sq4v";

	public static UIActivity_Com_Type5_XRPreRevard CreateInstance()
	{
		return (UIActivity_Com_Type5_XRPreRevard)UIPackage.CreateObject("Activity", "Activity_Com_Type5_XRPreRevard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		btn_Item = (GButton)GetChildAt(0);
	}
}
