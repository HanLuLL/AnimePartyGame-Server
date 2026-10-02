using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Card : GComponent
{
	public Controller showHotZone;

	public UIFight_Com_CardHotZone com_CardHot;

	public UIFight_Com_Cost com_CostLabel;

	public GButton btn_FinishPkCard;

	public const string URL = "ui://8irq146hmjruk";

	public static UIFight_Card CreateInstance()
	{
		return (UIFight_Card)UIPackage.CreateObject("Fight", "Fight_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showHotZone = GetControllerAt(0);
		com_CardHot = (UIFight_Com_CardHotZone)GetChildAt(0);
		com_CostLabel = (UIFight_Com_Cost)GetChildAt(1);
		btn_FinishPkCard = (GButton)GetChildAt(2);
	}
}
