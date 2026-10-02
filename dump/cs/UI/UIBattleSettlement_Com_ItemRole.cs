using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_ItemRole : GComponent
{
	public GLoader loader_Role;

	public Transition Cut_in;

	public Transition CJ_Cut_in;

	public Transition SJ_Cut_in;

	public const string URL = "ui://avgradidqees2p";

	public static UIBattleSettlement_Com_ItemRole CreateInstance()
	{
		return (UIBattleSettlement_Com_ItemRole)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_ItemRole");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Role = (GLoader)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
		CJ_Cut_in = GetTransitionAt(1);
		SJ_Cut_in = GetTransitionAt(2);
	}
}
