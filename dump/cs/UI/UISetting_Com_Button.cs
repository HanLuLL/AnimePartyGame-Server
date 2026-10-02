using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_Button : GButton
{
	public GGroup info;

	public const string URL = "ui://iy1joavto1n816";

	public static UISetting_Com_Button CreateInstance()
	{
		return (UISetting_Com_Button)UIPackage.CreateObject("Setting", "Setting_Com_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		info = (GGroup)GetChildAt(6);
	}
}
