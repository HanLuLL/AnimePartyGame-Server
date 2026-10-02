using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_SkinItem : GComponent
{
	public GGraph graph_Skin;

	public GTextField txt_title;

	public const string URL = "ui://ssf8xg9njz2422";

	public static UIBattlePass_Com_SkinItem CreateInstance()
	{
		return (UIBattlePass_Com_SkinItem)UIPackage.CreateObject("BattlePass", "BattlePass_Com_SkinItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_Skin = (GGraph)GetChildAt(0);
		txt_title = (GTextField)GetChildAt(1);
	}
}
