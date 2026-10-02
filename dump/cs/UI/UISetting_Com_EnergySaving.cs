using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_EnergySaving : GComponent
{
	public Controller status;

	public UISetting_Com_Toggle com_energySaving;

	public const string URL = "ui://iy1joavto1n8o";

	public static UISetting_Com_EnergySaving CreateInstance()
	{
		return (UISetting_Com_EnergySaving)UIPackage.CreateObject("Setting", "Setting_Com_EnergySaving");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		com_energySaving = (UISetting_Com_Toggle)GetChildAt(1);
	}
}
