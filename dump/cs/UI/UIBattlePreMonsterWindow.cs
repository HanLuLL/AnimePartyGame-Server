using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePreMonsterWindow : GComponent
{
	public GTextField txt_Title;

	public GList list_Monster;

	public Transition Cut_in;

	public const string URL = "ui://nuls6wcokqgj0";

	public static UIBattlePreMonsterWindow CreateInstance()
	{
		BindAll();
		return (UIBattlePreMonsterWindow)UIPackage.CreateObject("BattlePreMonster", "BattlePreMonsterWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://nuls6wcokqgj0", typeof(UIBattlePreMonsterWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(3);
		list_Monster = (GList)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
