using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Icon_Mov : GComponent
{
	public GImage move;

	public const string URL = "ui://xuaw6o8jmmmw43";

	public static UICom_Icon_Mov CreateInstance()
	{
		return (UICom_Icon_Mov)UIPackage.CreateObject("Common", "Com_Icon_Mov");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		move = (GImage)GetChildAt(0);
	}
}
