using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Icon_Def : GComponent
{
	public GImage def;

	public const string URL = "ui://xuaw6o8jmmmw40";

	public static UICom_Icon_Def CreateInstance()
	{
		return (UICom_Icon_Def)UIPackage.CreateObject("Common", "Com_Icon_Def");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		def = (GImage)GetChildAt(0);
	}
}
