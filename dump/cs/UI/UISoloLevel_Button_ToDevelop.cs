using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISoloLevel_Button_ToDevelop : GButton
{
	public GTextField des_txt;

	public const string URL = "ui://xuxzg1y1cgxz11";

	public static UISoloLevel_Button_ToDevelop CreateInstance()
	{
		return (UISoloLevel_Button_ToDevelop)UIPackage.CreateObject("SoloLevel", "SoloLevel_Button_ToDevelop");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		des_txt = (GTextField)GetChildAt(1);
	}
}
