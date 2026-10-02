using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_Map : GComponent
{
	public GLoader loader_Map;

	public GTextField txt_MapName;

	public GTextField txt_Difficulty;

	public GTextField txt_Round;

	public GList list_terms;

	public Transition LOOP;

	public const string URL = "ui://avgradidqees2t";

	public static UIBattleSettlement_Com_Map CreateInstance()
	{
		return (UIBattleSettlement_Com_Map)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_Map");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Map = (GLoader)GetChildAt(3);
		txt_MapName = (GTextField)GetChildAt(5);
		txt_Difficulty = (GTextField)GetChildAt(6);
		txt_Round = (GTextField)GetChildAt(8);
		list_terms = (GList)GetChildAt(9);
		LOOP = GetTransitionAt(0);
	}
}
