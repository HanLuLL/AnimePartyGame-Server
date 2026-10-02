using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICampaign_Button_Level : GButton
{
	public Controller state;

	public Controller isReward;

	public GLoader mainImage;

	public GTextField number;

	public GTextField txt_name;

	public GTextField txt_VictoryConditionDesc;

	public GButton RewardItem;

	public GImage completed;

	public GGroup pass;

	public const string URL = "ui://0j78s7jyh0j4p";

	public static UICampaign_Button_Level CreateInstance()
	{
		return (UICampaign_Button_Level)UIPackage.CreateObject("Campaign", "Campaign_Button_Level");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
		isReward = GetControllerAt(2);
		mainImage = (GLoader)GetChildAt(3);
		number = (GTextField)GetChildAt(5);
		txt_name = (GTextField)GetChildAt(6);
		txt_VictoryConditionDesc = (GTextField)GetChildAt(7);
		RewardItem = (GButton)GetChildAt(8);
		completed = (GImage)GetChildAt(10);
		pass = (GGroup)GetChildAt(11);
	}
}
