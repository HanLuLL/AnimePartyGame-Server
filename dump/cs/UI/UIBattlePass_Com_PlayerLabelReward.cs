using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_PlayerLabelReward : GComponent
{
	public GComponent com_PlayerLabel;

	public const string URL = "ui://ssf8xg9njz2420";

	public static UIBattlePass_Com_PlayerLabelReward CreateInstance()
	{
		return (UIBattlePass_Com_PlayerLabelReward)UIPackage.CreateObject("BattlePass", "BattlePass_Com_PlayerLabelReward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_PlayerLabel = (GComponent)GetChildAt(0);
	}
}
