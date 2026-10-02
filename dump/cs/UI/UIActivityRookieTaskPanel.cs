using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityRookieTaskPanel : GComponent
{
	public GLoader loadbg;

	public GTextField txt_title;

	public GLoader loader_finalRewardIocn;

	public GTextField txt_finalRewardName;

	public GTextField txt_rewardLevelTip;

	public GButton btn_finalReward;

	public GList list_Task;

	public GLoader btn_Detail;

	public const string URL = "ui://rx1j3reajos31a";

	public static UIActivityRookieTaskPanel CreateInstance()
	{
		BindAll();
		return (UIActivityRookieTaskPanel)UIPackage.CreateObject("ActivityRookieTask", "ActivityRookieTaskPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://rx1j3readzvf1", typeof(UIRookieTask_Com_AchieveLabel));
		UIObjectFactory.SetPackageItemExtension("ui://rx1j3readzvf4", typeof(UIRookieTask_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://rx1j3readzvfl", typeof(UITask_Button_GoWay));
		UIObjectFactory.SetPackageItemExtension("ui://rx1j3reajos31a", typeof(UIActivityRookieTaskPanel));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loadbg = (GLoader)GetChildAt(0);
		txt_title = (GTextField)GetChildAt(2);
		loader_finalRewardIocn = (GLoader)GetChildAt(3);
		txt_finalRewardName = (GTextField)GetChildAt(4);
		txt_rewardLevelTip = (GTextField)GetChildAt(5);
		btn_finalReward = (GButton)GetChildAt(6);
		list_Task = (GList)GetChildAt(7);
		btn_Detail = (GLoader)GetChildAt(10);
	}
}
