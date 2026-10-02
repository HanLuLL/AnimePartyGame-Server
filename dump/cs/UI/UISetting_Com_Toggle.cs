using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_Toggle : GButton
{
	public GLoader bg;

	public GImage open;

	public const string URL = "ui://iy1joavto1n8l";

	public static UISetting_Com_Toggle CreateInstance()
	{
		return (UISetting_Com_Toggle)UIPackage.CreateObject("Setting", "Setting_Com_Toggle");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bg = (GLoader)GetChildAt(1);
		open = (GImage)GetChildAt(2);
	}
}
