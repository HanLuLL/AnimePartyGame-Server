using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITaskPanel : GComponent
{
	public Controller tab;

	public UITask_Com_Seven com_Seven;

	public UITask_Com_Acquisition com_Acquisition;

	public GButton btn_Return;

	public GList list_Menu;

	public GList list_Task;

	public GList list_Achieve;

	public UITask_WeekTaskLiveness com_liveness;

	public GList list_weekTask;

	public Transition CUT_IN;

	public const string URL = "ui://hhpzjcmzthbm2j";

	public static UITaskPanel CreateInstance()
	{
		BindAll();
		return (UITaskPanel)UIPackage.CreateObject("Task", "TaskPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzcif04s", typeof(UIAcquisition_Com_Task));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzh38b48", typeof(UITask_Com_Acquisition));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzh38b4e", typeof(UITask_Button_Acquisition));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzh38b4g", typeof(UITask_Com_Acquisition_AcceptInvite));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzh38b4h", typeof(UITask_Com_Acquisition_Invite));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzkqgj47", typeof(UITask_Com_WeekLabel));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzl6o63u", typeof(UITask_Button_SevenDay));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzl6o63v", typeof(UITask_Com_Seven));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzl6o63w", typeof(UITask_Com_SevenSelectDay));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzthbm2j", typeof(UITaskPanel));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzthbm2n", typeof(UITask_Button_Tab));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzthbm2s", typeof(UITask_Com_NoviceLabel));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzthbm2v", typeof(UITask_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzthbm33", typeof(UITask_Com_AchieveLabel));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzthbm34", typeof(UITask_WeekTaskLiveness));
		UIObjectFactory.SetPackageItemExtension("ui://hhpzjcmzthbm37", typeof(UITask_Button_LivenessBox));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		com_Seven = (UITask_Com_Seven)GetChildAt(4);
		com_Acquisition = (UITask_Com_Acquisition)GetChildAt(5);
		btn_Return = (GButton)GetChildAt(6);
		list_Menu = (GList)GetChildAt(7);
		list_Task = (GList)GetChildAt(8);
		list_Achieve = (GList)GetChildAt(9);
		com_liveness = (UITask_WeekTaskLiveness)GetChildAt(10);
		list_weekTask = (GList)GetChildAt(11);
		CUT_IN = GetTransitionAt(0);
	}
}
