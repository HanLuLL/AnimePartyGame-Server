using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRelicWindow : GComponent
{
	public Controller Hide;

	public GTextField txt_HideTip;

	public GTextField txt_Probability;

	public GList list_Relic;

	public UIRelic_button_Rese btn_Reset;

	public UIRelic_button_Select btn_Select;

	public const string URL = "ui://kjm6oxy2l6o60";

	public static UIRelicWindow CreateInstance()
	{
		BindAll();
		return (UIRelicWindow)UIPackage.CreateObject("Relic", "RelicWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://kjm6oxy2iorl3", typeof(UIRelic_button_Select));
		UIObjectFactory.SetPackageItemExtension("ui://kjm6oxy2l6o60", typeof(UIRelicWindow));
		UIObjectFactory.SetPackageItemExtension("ui://kjm6oxy2l6o61", typeof(UIRelic_Button_Item));
		UIObjectFactory.SetPackageItemExtension("ui://kjm6oxy2o8128", typeof(UIRelic_button_Rese));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Hide = GetControllerAt(0);
		txt_HideTip = (GTextField)GetChildAt(0);
		txt_Probability = (GTextField)GetChildAt(5);
		list_Relic = (GList)GetChildAt(6);
		btn_Reset = (UIRelic_button_Rese)GetChildAt(7);
		btn_Select = (UIRelic_button_Select)GetChildAt(8);
	}
}
