using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIUpgrade_Com_PVPLabel : GComponent
{
	public Controller Slot;

	public UIUpgrade_Com_Star com_Level_1;

	public UIUpgrade_Com_Star com_Level_2;

	public UIUpgrade_Com_Star com_Level_3;

	public UIUpgrade_Com_Character loader_Character;

	public GTextField txt_First;

	public GTextField txt_Second;

	public GTextField txt_Thirth;

	public GTextField txt_Forth;

	public Transition Cut_in_Lv1;

	public Transition Cut_in_Lv2;

	public Transition Cut_in_Lv3;

	public const string URL = "ui://6vzgbzmwot0w28";

	public static UIUpgrade_Com_PVPLabel CreateInstance()
	{
		return (UIUpgrade_Com_PVPLabel)UIPackage.CreateObject("Upgrade", "Upgrade_Com_PVPLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Slot = GetControllerAt(0);
		com_Level_1 = (UIUpgrade_Com_Star)GetChildAt(8);
		com_Level_2 = (UIUpgrade_Com_Star)GetChildAt(9);
		com_Level_3 = (UIUpgrade_Com_Star)GetChildAt(10);
		loader_Character = (UIUpgrade_Com_Character)GetChildAt(11);
		txt_First = (GTextField)GetChildAt(22);
		txt_Second = (GTextField)GetChildAt(23);
		txt_Thirth = (GTextField)GetChildAt(24);
		txt_Forth = (GTextField)GetChildAt(25);
		Cut_in_Lv1 = GetTransitionAt(0);
		Cut_in_Lv2 = GetTransitionAt(1);
		Cut_in_Lv3 = GetTransitionAt(2);
	}
}
