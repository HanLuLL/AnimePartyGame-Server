using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStorePanel : GComponent
{
	public Controller tab;

	public Controller Type;

	public GLoader Loader_BG;

	public UIActivityStore_Button_Tab ActivityStore_btn_act;

	public UIActivityStore_Button_Tab ActivityStore_btn_str;

	public UIActivityStore_Button_TypeTab ActivityStore_btn_OneTask;

	public UIActivityStore_Button_TypeTab ActivityStore_btn_OneStore;

	public UIActivityStore_Com_Task com_Task;

	public UIActivityStore_Com_Store com_Store;

	public Transition Cut_in;

	public const string URL = "ui://88m1yfwgv1vk15";

	public static UIActivityStorePanel CreateInstance()
	{
		BindAll();
		return (UIActivityStorePanel)UIPackage.CreateObject("ActivityStore", "ActivityStorePanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgeusd2t", typeof(UIActivityStoretype1_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgeusd2u", typeof(UIActivityStoretype1_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgjhcl3n", typeof(UIActivityStore_Button_TypeTab));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgpgrv3x", typeof(UIActivityStore_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgtik52v", typeof(UIActivityStore_Com_BottomBg));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgtik52y", typeof(UIActivityStore_Com_BottomMask));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgtik531", typeof(UIActivityStore_Com_ItemBg));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgtik533", typeof(UIActivityStore_Com_ItemMask));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgv1vk15", typeof(UIActivityStorePanel));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgv1vk16", typeof(UIActivityStore_Com_Task));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgv1vk1e", typeof(UIActivityStore_Com_Store));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgv1vk1f", typeof(UIActivityStore_Button_Tab));
		UIObjectFactory.SetPackageItemExtension("ui://88m1yfwgv1vk1j", typeof(UIActivityStore_Button_TaskStatus));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		Type = GetControllerAt(1);
		Loader_BG = (GLoader)GetChildAt(0);
		ActivityStore_btn_act = (UIActivityStore_Button_Tab)GetChildAt(1);
		ActivityStore_btn_str = (UIActivityStore_Button_Tab)GetChildAt(2);
		ActivityStore_btn_OneTask = (UIActivityStore_Button_TypeTab)GetChildAt(3);
		ActivityStore_btn_OneStore = (UIActivityStore_Button_TypeTab)GetChildAt(4);
		com_Task = (UIActivityStore_Com_Task)GetChildAt(5);
		com_Store = (UIActivityStore_Com_Store)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
