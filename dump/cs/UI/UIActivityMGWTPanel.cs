using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMGWTPanel : GComponent
{
	public Controller page;

	public GLoader loader_BG;

	public UIActivityMGWT_Com_Task com_task;

	public UIActivityMGWT_Com_Store com_store;

	public UIActivityMGWT_Tab_Button tab_task;

	public UIActivityMGWT_Tab_Button tab_store;

	public GButton btn_close;

	public Transition Cut_in;

	public const string URL = "ui://wdl8l4hslwm10";

	public static UIActivityMGWTPanel CreateInstance()
	{
		BindAll();
		return (UIActivityMGWTPanel)UIPackage.CreateObject("ActivityMGWT", "ActivityMGWTPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://wdl8l4hslwm10", typeof(UIActivityMGWTPanel));
		UIObjectFactory.SetPackageItemExtension("ui://wdl8l4hslwm16", typeof(UIActivityMGWT_Com_Task));
		UIObjectFactory.SetPackageItemExtension("ui://wdl8l4hslwm17", typeof(UIActivityMGWT_Com_Store));
		UIObjectFactory.SetPackageItemExtension("ui://wdl8l4hslwm1d", typeof(UIActivityMGWT_Tab_Button));
		UIObjectFactory.SetPackageItemExtension("ui://wdl8l4hslwm1l", typeof(UIActivityMGWT_Task_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://wdl8l4hslwm1o", typeof(UIActivityMGWT_Task_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://wdl8l4hslwm1x", typeof(UIActivityMGWT_Button_GoodsItem));
		UIObjectFactory.SetPackageItemExtension("ui://wdl8l4hslwm1z", typeof(UIActivityMGWT_Com_GoodsItemName));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		com_task = (UIActivityMGWT_Com_Task)GetChildAt(1);
		com_store = (UIActivityMGWT_Com_Store)GetChildAt(2);
		tab_task = (UIActivityMGWT_Tab_Button)GetChildAt(3);
		tab_store = (UIActivityMGWT_Tab_Button)GetChildAt(4);
		btn_close = (GButton)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
