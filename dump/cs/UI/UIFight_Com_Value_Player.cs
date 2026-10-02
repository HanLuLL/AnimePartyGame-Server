using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Com_Value_Player : GComponent
{
	public Controller state;

	public GTextField title;

	public Transition Cut_in;

	public const string URL = "ui://8irq146hdde05b";

	public static UIFight_Com_Value_Player CreateInstance()
	{
		return (UIFight_Com_Value_Player)UIPackage.CreateObject("Fight", "Fight_Com_Value_Player");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		title = (GTextField)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
