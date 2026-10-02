using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIcom_RewardContent_Type1 : GComponent
{
	public Controller type;

	public GLoader img_tip;

	public GTextField txt_content1;

	public GTextField txt_content2;

	public GList list_LiveRewrad;

	public const string URL = "ui://mve1x02chfc02";

	public static UIcom_RewardContent_Type1 CreateInstance()
	{
		return (UIcom_RewardContent_Type1)UIPackage.CreateObject("DouYinReward", "com_RewardContent_Type1");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		img_tip = (GLoader)GetChildAt(0);
		txt_content1 = (GTextField)GetChildAt(1);
		txt_content2 = (GTextField)GetChildAt(2);
		list_LiveRewrad = (GList)GetChildAt(3);
	}
}
