using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityLevelPassPanel : GComponent
{
	public GLoader bg;

	public GList passItemList;

	public UIActivityLevelPass_Button_Buy btn_buy;

	public Transition Cut_in;

	public const string URL = "ui://fajmeueeg85ew";

	public static UIActivityLevelPassPanel CreateInstance()
	{
		BindAll();
		return (UIActivityLevelPassPanel)UIPackage.CreateObject("ActivityLevelPass", "ActivityLevelPassPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://fajmeueeg85ew", typeof(UIActivityLevelPassPanel));
		UIObjectFactory.SetPackageItemExtension("ui://fajmeueeklg7q", typeof(UIActivityLevelPass_Button_RewardItem));
		UIObjectFactory.SetPackageItemExtension("ui://fajmeueeklg7t", typeof(UIActivityLevelPass_ListItem));
		UIObjectFactory.SetPackageItemExtension("ui://fajmeueem48ix", typeof(UIActivityLevelPass_Button_Buy));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bg = (GLoader)GetChildAt(0);
		passItemList = (GList)GetChildAt(9);
		btn_buy = (UIActivityLevelPass_Button_Buy)GetChildAt(10);
		Cut_in = GetTransitionAt(0);
	}
}
