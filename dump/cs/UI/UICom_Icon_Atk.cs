using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Icon_Atk : GComponent
{
	public GImage atk;

	public const string URL = "ui://xuaw6o8jmmmw3z";

	public static UICom_Icon_Atk CreateInstance()
	{
		return (UICom_Icon_Atk)UIPackage.CreateObject("Common", "Com_Icon_Atk");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		atk = (GImage)GetChildAt(0);
	}
}
