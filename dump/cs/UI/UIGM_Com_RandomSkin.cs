using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Com_RandomSkin : GComponent
{
	public UIGM_Com_SetAttr com_Slot1;

	public UIGM_Com_SetAttr com_Slot2;

	public UIGM_Com_SetAttr com_Slot3;

	public UIGM_Com_SetAttr com_Slot4;

	public UIGM_Button_Operate btn_Random;

	public const string URL = "ui://725vhs9y7bvqv";

	public static UIGM_Com_RandomSkin CreateInstance()
	{
		return (UIGM_Com_RandomSkin)UIPackage.CreateObject("GM", "GM_Com_RandomSkin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Slot1 = (UIGM_Com_SetAttr)GetChildAt(0);
		com_Slot2 = (UIGM_Com_SetAttr)GetChildAt(1);
		com_Slot3 = (UIGM_Com_SetAttr)GetChildAt(2);
		com_Slot4 = (UIGM_Com_SetAttr)GetChildAt(3);
		btn_Random = (UIGM_Button_Operate)GetChildAt(4);
	}
}
