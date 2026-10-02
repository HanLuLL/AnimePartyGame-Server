using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_Reward7DayGift : GButton
{
	public Controller avalible;

	public GLoader loader_Up;

	public GLoader loader_Down;

	public GTextField txt_Time;

	public const string URL = "ui://zyd0rl0011biq1r";

	public static UIStore_Button_Reward7DayGift CreateInstance()
	{
		return (UIStore_Button_Reward7DayGift)UIPackage.CreateObject("Store", "Store_Button_Reward7DayGift");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		avalible = GetControllerAt(1);
		loader_Up = (GLoader)GetChildAt(0);
		loader_Down = (GLoader)GetChildAt(1);
		txt_Time = (GTextField)GetChildAt(3);
	}
}
