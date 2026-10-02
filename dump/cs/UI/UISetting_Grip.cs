using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://iy1joavtodf71y";

	public static UISetting_Grip CreateInstance()
	{
		return (UISetting_Grip)UIPackage.CreateObject("Setting", "Setting_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
