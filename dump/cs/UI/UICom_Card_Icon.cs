using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Card_Icon : GComponent
{
	public Controller Cost;

	public Controller CardType;

	public Controller TargetType;

	public const string URL = "ui://xuaw6o8jgj393l";

	public static UICom_Card_Icon CreateInstance()
	{
		return (UICom_Card_Icon)UIPackage.CreateObject("Common", "Com_Card_Icon");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cost = GetControllerAt(0);
		CardType = GetControllerAt(1);
		TargetType = GetControllerAt(2);
	}
}
