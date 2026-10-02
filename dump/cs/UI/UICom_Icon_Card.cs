using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Icon_Card : GComponent
{
	public GImage card;

	public const string URL = "ui://xuaw6o8jmmmw41";

	public static UICom_Icon_Card CreateInstance()
	{
		return (UICom_Icon_Card)UIPackage.CreateObject("Common", "Com_Icon_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		card = (GImage)GetChildAt(0);
	}
}
