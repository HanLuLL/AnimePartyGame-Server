using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_TopicItem : GComponent
{
	public GLoader loader_Theme;

	public GTextField txt_title;

	public GTextField txt_Desc;

	public const string URL = "ui://ssf8xg9njz2426";

	public static UIBattlePass_Com_TopicItem CreateInstance()
	{
		return (UIBattlePass_Com_TopicItem)UIPackage.CreateObject("BattlePass", "BattlePass_Com_TopicItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Theme = (GLoader)GetChildAt(1);
		txt_title = (GTextField)GetChildAt(2);
		txt_Desc = (GTextField)GetChildAt(3);
	}
}
