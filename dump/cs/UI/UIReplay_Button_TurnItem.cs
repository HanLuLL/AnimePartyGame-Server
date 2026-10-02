using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReplay_Button_TurnItem : GButton
{
	public Controller type;

	public Controller status;

	public GTextField txt_Desc;

	public GLoader loader_Role;

	public GTextField txt_MonsterDesc;

	public GList list_Event;

	public Transition setOpenStatus;

	public Transition setCloseStatus;

	public const string URL = "ui://dw3tmgbem0hx8";

	public static UIReplay_Button_TurnItem CreateInstance()
	{
		return (UIReplay_Button_TurnItem)UIPackage.CreateObject("Replay", "Replay_Button_TurnItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
		status = GetControllerAt(2);
		txt_Desc = (GTextField)GetChildAt(3);
		loader_Role = (GLoader)GetChildAt(4);
		txt_MonsterDesc = (GTextField)GetChildAt(5);
		list_Event = (GList)GetChildAt(6);
		setOpenStatus = GetTransitionAt(0);
		setCloseStatus = GetTransitionAt(1);
	}
}
