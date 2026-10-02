using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSelectMonsterWindow : GComponent
{
	public Controller Hide;

	public GTextField txt_HideTip;

	public GTextField txt_Title;

	public GList list_Monster;

	public UIBattleMonster_Com_Arrow com_RightArrow;

	public UIBattleMonster_Com_Arrow com_LeftArrow;

	public GButton btn_Cancel;

	public GButton btn_Sure;

	public const string URL = "ui://nxg5t93dkqgj0";

	public static UIBattleSelectMonsterWindow CreateInstance()
	{
		BindAll();
		return (UIBattleSelectMonsterWindow)UIPackage.CreateObject("BattleSelectMonster", "BattleSelectMonsterWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://nxg5t93dkqgj0", typeof(UIBattleSelectMonsterWindow));
		UIObjectFactory.SetPackageItemExtension("ui://nxg5t93dkqgj2", typeof(UIBattleMonster_Com_Arrow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Hide = GetControllerAt(0);
		txt_HideTip = (GTextField)GetChildAt(0);
		txt_Title = (GTextField)GetChildAt(4);
		list_Monster = (GList)GetChildAt(5);
		com_RightArrow = (UIBattleMonster_Com_Arrow)GetChildAt(6);
		com_LeftArrow = (UIBattleMonster_Com_Arrow)GetChildAt(7);
		btn_Cancel = (GButton)GetChildAt(8);
		btn_Sure = (GButton)GetChildAt(9);
	}
}
