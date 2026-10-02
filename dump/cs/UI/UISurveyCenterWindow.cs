using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISurveyCenterWindow : GComponent
{
	public GComponent mohu;

	public GLabel bottom;

	public GList list_tab;

	public UISurveyCenter_Com_Content com_Content;

	public GButton btn_confirm;

	public const string URL = "ui://s9p4q57hmyup0";

	public static UISurveyCenterWindow CreateInstance()
	{
		BindAll();
		return (UISurveyCenterWindow)UIPackage.CreateObject("SurveyCenter", "SurveyCenterWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://s9p4q57hmyup0", typeof(UISurveyCenterWindow));
		UIObjectFactory.SetPackageItemExtension("ui://s9p4q57hmyup2", typeof(UISurveyCenter_Com_Content));
		UIObjectFactory.SetPackageItemExtension("ui://s9p4q57hmyup7", typeof(UISurveyCenter_Button_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://s9p4q57hmyup8", typeof(UISurveyCenter_Button_TabItem));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GComponent)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		list_tab = (GList)GetChildAt(2);
		com_Content = (UISurveyCenter_Com_Content)GetChildAt(3);
		btn_confirm = (GButton)GetChildAt(4);
	}
}
