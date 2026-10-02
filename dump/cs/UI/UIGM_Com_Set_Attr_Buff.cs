using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Com_Set_Attr_Buff : GComponent
{
	public UIGM_Button_Operate btn_SetBuff;

	public GTextInput txt_SetSkillSource;

	public GTextInput txt_SetCardSource;

	public GTextInput txt_Set;

	public const string URL = "ui://725vhs9yq2uxw";

	public static UIGM_Com_Set_Attr_Buff CreateInstance()
	{
		return (UIGM_Com_Set_Attr_Buff)UIPackage.CreateObject("GM", "GM_Com_Set_Attr_Buff");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_SetBuff = (UIGM_Button_Operate)GetChildAt(2);
		txt_SetSkillSource = (GTextInput)GetChildAt(4);
		txt_SetCardSource = (GTextInput)GetChildAt(7);
		txt_Set = (GTextInput)GetChildAt(10);
	}
}
