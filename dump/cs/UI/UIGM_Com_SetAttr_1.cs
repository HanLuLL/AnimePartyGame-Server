using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Com_SetAttr_1 : GLabel
{
	public UIGM_Button_Operate btn_Set;

	public GTextInput txt_Set_1;

	public GTextInput txt_Set_2;

	public const string URL = "ui://725vhs9yulwvr";

	public static UIGM_Com_SetAttr_1 CreateInstance()
	{
		return (UIGM_Com_SetAttr_1)UIPackage.CreateObject("GM", "GM_Com_SetAttr_1");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Set = (UIGM_Button_Operate)GetChildAt(2);
		txt_Set_1 = (GTextInput)GetChildAt(4);
		txt_Set_2 = (GTextInput)GetChildAt(6);
	}
}
