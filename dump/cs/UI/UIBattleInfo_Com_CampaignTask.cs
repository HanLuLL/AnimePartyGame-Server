using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_CampaignTask : GComponent
{
	public GTextField txt_Desc;

	public GTextField txt_Task;

	public const string URL = "ui://fxejlqlfkqgj92";

	public static UIBattleInfo_Com_CampaignTask CreateInstance()
	{
		return (UIBattleInfo_Com_CampaignTask)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_CampaignTask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Desc = (GTextField)GetChildAt(1);
		txt_Task = (GTextField)GetChildAt(2);
	}
}
