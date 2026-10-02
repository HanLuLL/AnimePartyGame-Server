using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_Bg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://iy1joavto1n82";

	public static UISetting_Com_Bg CreateInstance()
	{
		return (UISetting_Com_Bg)UIPackage.CreateObject("Setting", "Setting_Com_Bg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
