using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReward_PropItem : GButton
{
	public GButton btn_Item;

	public const string URL = "ui://r5u0087t929s4";

	public static UIReward_PropItem CreateInstance()
	{
		return (UIReward_PropItem)UIPackage.CreateObject("Reward", "Reward_PropItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Item = (GButton)GetChildAt(0);
	}
}
