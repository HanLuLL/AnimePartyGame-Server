using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Card_Name : GComponent
{
	public Controller CardType;

	public const string URL = "ui://xuaw6o8jgj393m";

	public static UICom_Card_Name CreateInstance()
	{
		return (UICom_Card_Name)UIPackage.CreateObject("Common", "Com_Card_Name");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		CardType = GetControllerAt(0);
	}
}
