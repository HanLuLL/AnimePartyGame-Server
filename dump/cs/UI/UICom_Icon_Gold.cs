using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Icon_Gold : GComponent
{
	public GImage gold;

	public const string URL = "ui://xuaw6o8jmmmw3y";

	public static UICom_Icon_Gold CreateInstance()
	{
		return (UICom_Icon_Gold)UIPackage.CreateObject("Common", "Com_Icon_Gold");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		gold = (GImage)GetChildAt(0);
	}
}
