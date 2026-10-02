using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITipsWindow : GComponent
{
	public UITips_Com_Common common;

	public UITips_Com_RoundReward roundReward;

	public UITips_Com_Top com_Thinking;

	public UITips_Com_PassiveSkillTrigger passiveSkillTrigger;

	public UITips_Com_Top com_RoundStart;

	public UITips_Com_LevelUp levelUp;

	public UITips_Com_YourRoundStart yourRoundStart;

	public UITips_Com_ChangeActionOrder changeActionOrder;

	public UITips_Com_LotteryRound lotteryRound;

	public UITips_Com_PVETaskItem com_PveTaskTip;

	public UITips_Com_ChosenOne com_ChosenOne;

	public UITips_Com_ClueCompleteTip com_PveClueTaskTip;

	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8m103c70";

	public static UITipsWindow CreateInstance()
	{
		BindAll();
		return (UITipsWindow)UIPackage.CreateObject("Tips", "TipsWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8m103c70", typeof(UITipsWindow));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mgpj2s7n", typeof(UITips_Com_Top));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mine4s7p", typeof(UITips_Com_Common));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mine4s7q", typeof(UITips_Com_RoundReward));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mine4s7r", typeof(UITips_Com_PassiveSkillTrigger));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mine4s7t", typeof(UITips_Com_LevelUp));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8ml8aas83", typeof(UITips_Com_ChangeActionOrder));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mmgj002", typeof(UITips_Com_ClueCompleteTip));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mmques86", typeof(UITips_Com_LotteryRound));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mot0ws87", typeof(UITips_Com_PVETaskItem));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mou6os82", typeof(UITips_Com_YourRoundStart));
		UIObjectFactory.SetPackageItemExtension("ui://1jtcsp8mwz8ns8b", typeof(UITips_Com_ChosenOne));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		common = (UITips_Com_Common)GetChildAt(0);
		roundReward = (UITips_Com_RoundReward)GetChildAt(1);
		com_Thinking = (UITips_Com_Top)GetChildAt(2);
		passiveSkillTrigger = (UITips_Com_PassiveSkillTrigger)GetChildAt(3);
		com_RoundStart = (UITips_Com_Top)GetChildAt(4);
		levelUp = (UITips_Com_LevelUp)GetChildAt(5);
		yourRoundStart = (UITips_Com_YourRoundStart)GetChildAt(6);
		changeActionOrder = (UITips_Com_ChangeActionOrder)GetChildAt(7);
		lotteryRound = (UITips_Com_LotteryRound)GetChildAt(8);
		com_PveTaskTip = (UITips_Com_PVETaskItem)GetChildAt(9);
		com_ChosenOne = (UITips_Com_ChosenOne)GetChildAt(10);
		com_PveClueTaskTip = (UITips_Com_ClueCompleteTip)GetChildAt(11);
		Cut_in = GetTransitionAt(0);
	}
}
