using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_ShowRole : GComponent
{
	public GLoader loader_Role_3;

	public GLoader loader_Role_2;

	public GLoader loader_Role_1;

	public Transition Cut_in;

	public const string URL = "ui://avgradidqees24";

	public static UIBattleSettlement_Com_ShowRole CreateInstance()
	{
		return (UIBattleSettlement_Com_ShowRole)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_ShowRole");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Role_3 = (GLoader)GetChildAt(0);
		loader_Role_2 = (GLoader)GetChildAt(1);
		loader_Role_1 = (GLoader)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
