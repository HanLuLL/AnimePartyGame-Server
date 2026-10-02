using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_CommonItem : GComponent
{
	public GLoader loader_Icon;

	public GTextField txt_title;

	public const string URL = "ui://ssf8xg9njz2424";

	public static UIBattlePass_Com_CommonItem CreateInstance()
	{
		return (UIBattlePass_Com_CommonItem)UIPackage.CreateObject("BattlePass", "BattlePass_Com_CommonItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(0);
		txt_title = (GTextField)GetChildAt(1);
	}
}
