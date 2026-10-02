using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_TaskLabel : GComponent
{
	public Controller Status;

	public GButton btn_Reward;

	public GTextField txt_Title;

	public GProgressBar progress_task;

	public UIBattlePass_Button_TaskStatus btn_taskStatus;

	public const string URL = "ui://ssf8xg9njz241a";

	public static UIBattlePass_Com_TaskLabel CreateInstance()
	{
		return (UIBattlePass_Com_TaskLabel)UIPackage.CreateObject("BattlePass", "BattlePass_Com_TaskLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		btn_Reward = (GButton)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(2);
		progress_task = (GProgressBar)GetChildAt(3);
		btn_taskStatus = (UIBattlePass_Button_TaskStatus)GetChildAt(5);
	}
}
