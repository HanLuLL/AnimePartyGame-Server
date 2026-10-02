using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIDouYinRewardWindow : GComponent
{
	public Controller type;

	public GComponent mohu;

	public GLabel bottom;

	public UIDouYinReward_Type0_Content com_RewardContent_Type0;

	public UIcom_RewardContent_Type1 com_RewardContent_Type1;

	public const string URL = "ui://mve1x02cl83j0";

	public static UIDouYinRewardWindow CreateInstance()
	{
		BindAll();
		return (UIDouYinRewardWindow)UIPackage.CreateObject("DouYinReward", "DouYinRewardWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://mve1x02chfc02", typeof(UIcom_RewardContent_Type1));
		UIObjectFactory.SetPackageItemExtension("ui://mve1x02cl83j0", typeof(UIDouYinRewardWindow));
		UIObjectFactory.SetPackageItemExtension("ui://mve1x02cl83j1", typeof(UIDouYinReward_Type0_Content));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		mohu = (GComponent)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		com_RewardContent_Type0 = (UIDouYinReward_Type0_Content)GetChildAt(2);
		com_RewardContent_Type1 = (UIcom_RewardContent_Type1)GetChildAt(3);
	}
}
