using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Item_Bg : GComponent
{
	public Controller SetColor;

	public Transition cut_in;

	public const string URL = "ui://iy1joavto1n89";

	public static UISetting_Item_Bg CreateInstance()
	{
		return (UISetting_Item_Bg)UIPackage.CreateObject("Setting", "Setting_Item_Bg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
		cut_in = GetTransitionAt(0);
	}
}
