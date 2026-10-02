using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Com_BattlePass : GComponent
{
	public Controller redStatus;

	public Controller status;

	public GLoader loader_Fold;

	public GTextField txt_Time;

	public GLoader loader_UnFold;

	public GTextField txt_Title;

	public GTextField txt_nextLV;

	public GProgressBar progress_Exp;

	public UIHome_Com_BattlePassReward com_FreeReward;

	public UIHome_Com_BattlePassReward com_NormalReward;

	public UIHome_Com_BattlePassReward com_PremiumReward;

	public UIHome_Btn_BattlePass btn_OpenBattlePass;

	public Transition showDetail;

	public Transition hideDetail;

	public const string URL = "ui://u7xbdcgujz24q2q";

	public static UIHome_Com_BattlePass CreateInstance()
	{
		return (UIHome_Com_BattlePass)UIPackage.CreateObject("Home", "Home_Com_BattlePass");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(0);
		status = GetControllerAt(1);
		loader_Fold = (GLoader)GetChildAt(0);
		txt_Time = (GTextField)GetChildAt(1);
		loader_UnFold = (GLoader)GetChildAt(2);
		txt_Title = (GTextField)GetChildAt(4);
		txt_nextLV = (GTextField)GetChildAt(6);
		progress_Exp = (GProgressBar)GetChildAt(7);
		com_FreeReward = (UIHome_Com_BattlePassReward)GetChildAt(9);
		com_NormalReward = (UIHome_Com_BattlePassReward)GetChildAt(10);
		com_PremiumReward = (UIHome_Com_BattlePassReward)GetChildAt(11);
		btn_OpenBattlePass = (UIHome_Btn_BattlePass)GetChildAt(12);
		showDetail = GetTransitionAt(0);
		hideDetail = GetTransitionAt(1);
	}
}
