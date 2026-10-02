using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_ListItem : GComponent
{
	public Controller day;

	public UIActivityComeback_Button_RewardItem paidItem;

	public UIActivityComeback_Button_RewardItem freeItem;

	public const string URL = "ui://hconmwfcvjqj1a";

	public static UIActivityComeback_ListItem CreateInstance()
	{
		return (UIActivityComeback_ListItem)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_ListItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		day = GetControllerAt(0);
		paidItem = (UIActivityComeback_Button_RewardItem)GetChildAt(0);
		freeItem = (UIActivityComeback_Button_RewardItem)GetChildAt(1);
	}
}
