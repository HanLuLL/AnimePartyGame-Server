using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRuleWindow : GComponent
{
	public GGraph mohu;

	public GTextField txt_Title;

	public UIRule_Com_Content com_Content;

	public GButton btn_Close;

	public const string URL = "ui://232dx96nn9cw0";

	public static UIRuleWindow CreateInstance()
	{
		BindAll();
		return (UIRuleWindow)UIPackage.CreateObject("Rule", "RuleWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://232dx96nn9cw0", typeof(UIRuleWindow));
		UIObjectFactory.SetPackageItemExtension("ui://232dx96nn9cw3", typeof(UIRule_Com_Content));
		UIObjectFactory.SetPackageItemExtension("ui://232dx96no8124", typeof(UIRule_Com_Excel_01));
		UIObjectFactory.SetPackageItemExtension("ui://232dx96no8125", typeof(UIRule_Com_Excel_01_Item));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		txt_Title = (GTextField)GetChildAt(4);
		com_Content = (UIRule_Com_Content)GetChildAt(5);
		btn_Close = (GButton)GetChildAt(6);
	}
}
