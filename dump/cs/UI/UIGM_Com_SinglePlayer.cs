using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Com_SinglePlayer : GComponent
{
	public UIGM_Com_SetAttr com_DicePoint;

	public UIGM_Com_SetAttr com_AddCard;

	public UIGM_Com_SetAttr com_AddGold;

	public UIGM_Com_SetAttr com_AddProgress;

	public UIGM_Com_SetAttr com_AddRelic;

	public UIGM_Button_Operate btn_CardProbability;

	public UIGM_Button_SingleContent btn_LockProgress;

	public UIGM_Com_SetAttr com_GameOver;

	public const string URL = "ui://725vhs9yvvbqu";

	public static UIGM_Com_SinglePlayer CreateInstance()
	{
		return (UIGM_Com_SinglePlayer)UIPackage.CreateObject("GM", "GM_Com_SinglePlayer");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_DicePoint = (UIGM_Com_SetAttr)GetChildAt(0);
		com_AddCard = (UIGM_Com_SetAttr)GetChildAt(1);
		com_AddGold = (UIGM_Com_SetAttr)GetChildAt(2);
		com_AddProgress = (UIGM_Com_SetAttr)GetChildAt(3);
		com_AddRelic = (UIGM_Com_SetAttr)GetChildAt(4);
		btn_CardProbability = (UIGM_Button_Operate)GetChildAt(5);
		btn_LockProgress = (UIGM_Button_SingleContent)GetChildAt(7);
		com_GameOver = (UIGM_Com_SetAttr)GetChildAt(9);
	}
}
