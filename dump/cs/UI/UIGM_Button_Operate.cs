using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Button_Operate : GButton
{
	public GGraph up;

	public GGraph down;

	public GGraph over;

	public const string URL = "ui://725vhs9ylagsq";

	public static UIGM_Button_Operate CreateInstance()
	{
		return (UIGM_Button_Operate)UIPackage.CreateObject("GM", "GM_Button_Operate");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		up = (GGraph)GetChildAt(0);
		down = (GGraph)GetChildAt(1);
		over = (GGraph)GetChildAt(2);
	}
}
