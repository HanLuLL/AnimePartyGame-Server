using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Com_Defenser : GComponent
{
	public Controller ShowUp;

	public Controller showPoint;

	public Controller showReady;

	public Controller changeState;

	public Controller showResult;

	public Controller showHp;

	public GTextField txt_CardValue;

	public GTextField txt_Life;

	public UIFight_Com_Point com_Point;

	public UIFight_Com_ReadyLabel com_ReadyLabel;

	public GTextField txt_HitValue;

	public Transition ValueChange;

	public Transition DamgeFloat;

	public Transition HugeDamage;

	public const string URL = "ui://8irq146hwvm91a";

	public static UIFight_Com_Defenser CreateInstance()
	{
		return (UIFight_Com_Defenser)UIPackage.CreateObject("Fight", "Fight_Com_Defenser");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		ShowUp = GetControllerAt(0);
		showPoint = GetControllerAt(1);
		showReady = GetControllerAt(2);
		changeState = GetControllerAt(3);
		showResult = GetControllerAt(4);
		showHp = GetControllerAt(5);
		txt_CardValue = (GTextField)GetChildAt(3);
		txt_Life = (GTextField)GetChildAt(7);
		com_Point = (UIFight_Com_Point)GetChildAt(11);
		com_ReadyLabel = (UIFight_Com_ReadyLabel)GetChildAt(13);
		txt_HitValue = (GTextField)GetChildAt(14);
		ValueChange = GetTransitionAt(0);
		DamgeFloat = GetTransitionAt(1);
		HugeDamage = GetTransitionAt(2);
	}
}
