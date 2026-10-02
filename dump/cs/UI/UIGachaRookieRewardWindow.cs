using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGachaRookieRewardWindow : GComponent
{
	public GComponent mohu;

	public GList heroList;

	public UICachaReward_Button_Receive btn_Confirm;

	public GButton btn_Cancel;

	public const string URL = "ui://q1a022ssm48i0";

	public static UIGachaRookieRewardWindow CreateInstance()
	{
		BindAll();
		return (UIGachaRookieRewardWindow)UIPackage.CreateObject("GachaRookieReward", "GachaRookieRewardWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://q1a022ssjrp5qq38", typeof(UICachaReward_Button_Receive));
		UIObjectFactory.SetPackageItemExtension("ui://q1a022ssm48i0", typeof(UIGachaRookieRewardWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GComponent)GetChildAt(0);
		heroList = (GList)GetChildAt(3);
		btn_Confirm = (UICachaReward_Button_Receive)GetChildAt(4);
		btn_Cancel = (GButton)GetChildAt(5);
	}
}
