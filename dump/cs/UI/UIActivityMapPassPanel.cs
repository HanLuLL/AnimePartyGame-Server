using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMapPassPanel : GComponent
{
	public GLoader bg;

	public UIActivityMapPass_Button_Buy btn_buy;

	public GImage listBG;

	public GList passItemList;

	public Transition CutIn;

	public const string URL = "ui://vvv9zaj1qa2x0";

	public static UIActivityMapPassPanel CreateInstance()
	{
		BindAll();
		return (UIActivityMapPassPanel)UIPackage.CreateObject("ActivityMapPass", "ActivityMapPassPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://vvv9zaj1qa2x0", typeof(UIActivityMapPassPanel));
		UIObjectFactory.SetPackageItemExtension("ui://vvv9zaj1qa2x2", typeof(UIActivityMapPass_MapInfo));
		UIObjectFactory.SetPackageItemExtension("ui://vvv9zaj1qa2x3", typeof(UIActivityMapPass_ListItem));
		UIObjectFactory.SetPackageItemExtension("ui://vvv9zaj1qa2x4", typeof(UIActivityMapPass_Button_RewardItem));
		UIObjectFactory.SetPackageItemExtension("ui://vvv9zaj1u0xak", typeof(UIActivityMapPass_Button_Buy));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bg = (GLoader)GetChildAt(0);
		btn_buy = (UIActivityMapPass_Button_Buy)GetChildAt(4);
		listBG = (GImage)GetChildAt(5);
		passItemList = (GList)GetChildAt(6);
		CutIn = GetTransitionAt(0);
	}
}
