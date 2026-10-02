using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Com_SetContent : GLabel
{
	public UIGM_Button_Operate btn_Set;

	public GTextInput txt_Set;

	public const string URL = "ui://725vhs9yfcst2";

	public static UIGM_Com_SetContent CreateInstance()
	{
		return (UIGM_Com_SetContent)UIPackage.CreateObject("GM", "GM_Com_SetContent");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Set = (UIGM_Button_Operate)GetChildAt(2);
		txt_Set = (GTextInput)GetChildAt(4);
	}
}
