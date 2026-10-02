using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Com_ReadyLabel : GComponent
{
	public Controller player;

	public const string URL = "ui://8irq146ht4dl2j";

	public static UIFight_Com_ReadyLabel CreateInstance()
	{
		return (UIFight_Com_ReadyLabel)UIPackage.CreateObject("Fight", "Fight_Com_ReadyLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		player = GetControllerAt(0);
	}
}
