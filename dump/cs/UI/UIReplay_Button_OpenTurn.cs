using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReplay_Button_OpenTurn : GButton
{
	public GTextField txt_Round;

	public GTextField txt_Desc;

	public GLoader loader_Role;

	public const string URL = "ui://dw3tmgbem0hx5";

	public static UIReplay_Button_OpenTurn CreateInstance()
	{
		return (UIReplay_Button_OpenTurn)UIPackage.CreateObject("Replay", "Replay_Button_OpenTurn");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Round = (GTextField)GetChildAt(3);
		txt_Desc = (GTextField)GetChildAt(4);
		loader_Role = (GLoader)GetChildAt(5);
	}
}
