using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_PlayerLabelItem : GComponent
{
	public GComponent com_PlayerLabel;

	public GTextField txt_Title;

	public const string URL = "ui://ssf8xg9njz2423";

	public static UIBattlePass_Com_PlayerLabelItem CreateInstance()
	{
		return (UIBattlePass_Com_PlayerLabelItem)UIPackage.CreateObject("BattlePass", "BattlePass_Com_PlayerLabelItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_PlayerLabel = (GComponent)GetChildAt(0);
		txt_Title = (GTextField)GetChildAt(1);
	}
}
