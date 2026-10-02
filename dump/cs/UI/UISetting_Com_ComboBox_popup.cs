using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_ComboBox_popup : GComponent
{
	public GList list;

	public const string URL = "ui://iy1joavto1n8i";

	public static UISetting_Com_ComboBox_popup CreateInstance()
	{
		return (UISetting_Com_ComboBox_popup)UIPackage.CreateObject("Setting", "Setting_Com_ComboBox_popup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list = (GList)GetChildAt(1);
	}
}
