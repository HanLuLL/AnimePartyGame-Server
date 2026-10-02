using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Com_Value : GComponent
{
	public GList list_atk;

	public GList list_def;

	public UIFight_Com_Value_Player player_atk;

	public UIFight_Com_Value_Player player_def;

	public Transition Cut_in;

	public Transition Cut_out;

	public const string URL = "ui://8irq146hwa8253";

	public static UIFight_Com_Value CreateInstance()
	{
		return (UIFight_Com_Value)UIPackage.CreateObject("Fight", "Fight_Com_Value");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_atk = (GList)GetChildAt(2);
		list_def = (GList)GetChildAt(3);
		player_atk = (UIFight_Com_Value_Player)GetChildAt(4);
		player_def = (UIFight_Com_Value_Player)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
		Cut_out = GetTransitionAt(1);
	}
}
