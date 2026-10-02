using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_Day7Gift_PVE_Reward : GComponent
{
	public Controller stateType;

	public GTextField txt_Day;

	public GLoader Icon;

	public GTextField txt_ItemNum;

	public const string URL = "ui://zyd0rl00ekc0qq3p";

	public static UIStore_Com_Day7Gift_PVE_Reward CreateInstance()
	{
		return (UIStore_Com_Day7Gift_PVE_Reward)UIPackage.CreateObject("Store", "Store_Com_Day7Gift_PVE_Reward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		stateType = GetControllerAt(0);
		txt_Day = (GTextField)GetChildAt(2);
		Icon = (GLoader)GetChildAt(3);
		txt_ItemNum = (GTextField)GetChildAt(4);
	}
}
