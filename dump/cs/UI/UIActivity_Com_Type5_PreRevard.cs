using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type5_PreRevard : GComponent
{
	public Controller status;

	public GButton btn_Item;

	public const string URL = "ui://vckl96kso812dt";

	public static UIActivity_Com_Type5_PreRevard CreateInstance()
	{
		return (UIActivity_Com_Type5_PreRevard)UIPackage.CreateObject("Activity", "Activity_Com_Type5_PreRevard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		btn_Item = (GButton)GetChildAt(0);
	}
}
