using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Com_Value_Card : GComponent
{
	public Controller state;

	public GButton card;

	public GTextField title_atk;

	public GTextField title_total;

	public GTextField title_def;

	public Transition Card_Cut_in;

	public const string URL = "ui://8irq146hwa8254";

	public static UIFight_Com_Value_Card CreateInstance()
	{
		return (UIFight_Com_Value_Card)UIPackage.CreateObject("Fight", "Fight_Com_Value_Card");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		card = (GButton)GetChildAt(0);
		title_atk = (GTextField)GetChildAt(2);
		title_total = (GTextField)GetChildAt(6);
		title_def = (GTextField)GetChildAt(9);
		Card_Cut_in = GetTransitionAt(0);
	}
}
