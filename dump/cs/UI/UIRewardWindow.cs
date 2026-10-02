using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRewardWindow : GComponent
{
	public Controller type;

	public GGraph mohu;

	public GTextField Title;

	public GList list_Prop;

	public UIReward_Com_MonthCard com_MonthCard;

	public UIReward_Com_ExpiredTransform com_ExpiredTransform;

	public const string URL = "ui://r5u0087t929s0";

	public static UIRewardWindow CreateInstance()
	{
		BindAll();
		return (UIRewardWindow)UIPackage.CreateObject("Reward", "RewardWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://r5u0087t929s0", typeof(UIRewardWindow));
		UIObjectFactory.SetPackageItemExtension("ui://r5u0087t929s4", typeof(UIReward_PropItem));
		UIObjectFactory.SetPackageItemExtension("ui://r5u0087tm1i7o", typeof(UIReward_Com_MonthCard));
		UIObjectFactory.SetPackageItemExtension("ui://r5u0087tvau3q", typeof(UIReward_Com_ExpiredTransform));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		mohu = (GGraph)GetChildAt(0);
		Title = (GTextField)GetChildAt(3);
		list_Prop = (GList)GetChildAt(6);
		com_MonthCard = (UIReward_Com_MonthCard)GetChildAt(8);
		com_ExpiredTransform = (UIReward_Com_ExpiredTransform)GetChildAt(9);
	}
}
