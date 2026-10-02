using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Com_Attacker : GComponent
{
	public Controller ShowUp;

	public Controller showPoint;

	public Controller showReady;

	public Controller showHp;

	public GTextField txt_CardValue;

	public GTextField txt_Life;

	public UIFight_Com_Point com_Point;

	public UIFight_Com_ReadyLabel com_ReadyLabel;

	public Transition ValueChange;

	public const string URL = "ui://8irq146ht4dl2g";

	public static UIFight_Com_Attacker CreateInstance()
	{
		return (UIFight_Com_Attacker)UIPackage.CreateObject("Fight", "Fight_Com_Attacker");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		ShowUp = GetControllerAt(0);
		showPoint = GetControllerAt(1);
		showReady = GetControllerAt(2);
		showHp = GetControllerAt(3);
		txt_CardValue = (GTextField)GetChildAt(2);
		txt_Life = (GTextField)GetChildAt(6);
		com_Point = (UIFight_Com_Point)GetChildAt(8);
		com_ReadyLabel = (UIFight_Com_ReadyLabel)GetChildAt(9);
		ValueChange = GetTransitionAt(0);
	}
}
