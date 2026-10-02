using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Icon_Hp : GComponent
{
	public GImage hp;

	public const string URL = "ui://xuaw6o8jmmmw42";

	public static UICom_Icon_Hp CreateInstance()
	{
		return (UICom_Icon_Hp)UIPackage.CreateObject("Common", "Com_Icon_Hp");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		hp = (GImage)GetChildAt(0);
	}
}
