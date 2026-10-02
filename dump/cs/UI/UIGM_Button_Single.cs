using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Button_Single : GButton
{
	public GGraph di_0;

	public const string URL = "ui://725vhs9yl4b28";

	public static UIGM_Button_Single CreateInstance()
	{
		return (UIGM_Button_Single)UIPackage.CreateObject("GM", "GM_Button_Single");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di_0 = (GGraph)GetChildAt(0);
	}
}
