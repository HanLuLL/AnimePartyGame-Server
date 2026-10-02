using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMatch_Com_MapMonsterItem : GComponent
{
	public GLoader loader_Boss;

	public const string URL = "ui://qxwapsemr26s1e";

	public static UIMatch_Com_MapMonsterItem CreateInstance()
	{
		return (UIMatch_Com_MapMonsterItem)UIPackage.CreateObject("Match", "Match_Com_MapMonsterItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Boss = (GLoader)GetChildAt(0);
	}
}
