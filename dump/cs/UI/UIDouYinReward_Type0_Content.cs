using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIDouYinReward_Type0_Content : GComponent
{
	public GList list_LiveRewrad;

	public GTextField txt_content;

	public const string URL = "ui://mve1x02cl83j1";

	public static UIDouYinReward_Type0_Content CreateInstance()
	{
		return (UIDouYinReward_Type0_Content)UIPackage.CreateObject("DouYinReward", "DouYinReward_Type0_Content");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_LiveRewrad = (GList)GetChildAt(1);
		txt_content = (GTextField)GetChildAt(3);
	}
}
