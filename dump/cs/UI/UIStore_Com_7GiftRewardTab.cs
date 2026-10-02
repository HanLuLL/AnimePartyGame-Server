using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_7GiftRewardTab : GComponent
{
	public Controller day;

	public Controller nextStatus;

	public Controller GetStatus;

	public GTextField txt_Day;

	public GGraph graph_Hot_0;

	public GGraph graph_Hot_1;

	public GGraph graph_Hot_2;

	public GTextField txt_Name_0;

	public GTextField txt_Name_1;

	public GTextField txt_Name_2;

	public const string URL = "ui://zyd0rl0011biqy";

	public static UIStore_Com_7GiftRewardTab CreateInstance()
	{
		return (UIStore_Com_7GiftRewardTab)UIPackage.CreateObject("Store", "Store_Com_7GiftRewardTab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		day = GetControllerAt(0);
		nextStatus = GetControllerAt(1);
		GetStatus = GetControllerAt(2);
		txt_Day = (GTextField)GetChildAt(0);
		graph_Hot_0 = (GGraph)GetChildAt(1);
		graph_Hot_1 = (GGraph)GetChildAt(2);
		graph_Hot_2 = (GGraph)GetChildAt(3);
		txt_Name_0 = (GTextField)GetChildAt(4);
		txt_Name_1 = (GTextField)GetChildAt(5);
		txt_Name_2 = (GTextField)GetChildAt(6);
	}
}
