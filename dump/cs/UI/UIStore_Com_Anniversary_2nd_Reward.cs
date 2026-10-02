using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_Anniversary_2nd_Reward : GComponent
{
	public Controller stateType;

	public GTextField txt_Day;

	public GLoader Icon;

	public GTextField txt_CurrentCount;

	public GTextField txt_OriginalCount;

	public const string URL = "ui://zyd0rl00c76fqq5v";

	public static UIStore_Com_Anniversary_2nd_Reward CreateInstance()
	{
		return (UIStore_Com_Anniversary_2nd_Reward)UIPackage.CreateObject("Store", "Store_Com_Anniversary_2nd_Reward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		stateType = GetControllerAt(0);
		txt_Day = (GTextField)GetChildAt(2);
		Icon = (GLoader)GetChildAt(3);
		txt_CurrentCount = (GTextField)GetChildAt(4);
		txt_OriginalCount = (GTextField)GetChildAt(5);
	}
}
