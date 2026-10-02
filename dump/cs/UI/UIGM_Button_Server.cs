using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Button_Server : GButton
{
	public GImage di_1;

	public const string URL = "ui://725vhs9yo11dt";

	public static UIGM_Button_Server CreateInstance()
	{
		return (UIGM_Button_Server)UIPackage.CreateObject("GM", "GM_Button_Server");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di_1 = (GImage)GetChildAt(2);
	}
}
