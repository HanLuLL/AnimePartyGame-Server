using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Com_SetAttr : GLabel
{
	public UIGM_Button_Operate btn_Set;

	public GTextInput txt_Set;

	public const string URL = "ui://725vhs9ylmo03";

	public static UIGM_Com_SetAttr CreateInstance()
	{
		return (UIGM_Com_SetAttr)UIPackage.CreateObject("GM", "GM_Com_SetAttr");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Set = (UIGM_Button_Operate)GetChildAt(2);
		txt_Set = (GTextInput)GetChildAt(4);
	}
}
