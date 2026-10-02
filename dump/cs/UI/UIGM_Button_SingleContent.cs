using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Button_SingleContent : GButton
{
	public GGraph di_0;

	public GGraph di_1;

	public const string URL = "ui://725vhs9ypmdy1";

	public static UIGM_Button_SingleContent CreateInstance()
	{
		return (UIGM_Button_SingleContent)UIPackage.CreateObject("GM", "GM_Button_SingleContent");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di_0 = (GGraph)GetChildAt(0);
		di_1 = (GGraph)GetChildAt(1);
	}
}
