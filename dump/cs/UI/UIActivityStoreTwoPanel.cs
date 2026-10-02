using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreTwoPanel : GComponent
{
	public Controller tab;

	public GLoader Loader_BG;

	public UIActivityStoreTwo_Button_Tab ActivityStore_btn_act;

	public UIActivityStoreTwo_Button_Tab ActivityStore_btn_str;

	public UIActivityStoreTwo_Com_Task com_Task;

	public UIActivityStoreTwo_Com_Store com_Store;

	public Transition Cut_in;

	public const string URL = "ui://6dt5s4htqw8h0";

	public static UIActivityStoreTwoPanel CreateInstance()
	{
		BindAll();
		return (UIActivityStoreTwoPanel)UIPackage.CreateObject("ActivityStoreTwo", "ActivityStoreTwoPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8h0", typeof(UIActivityStoreTwoPanel));
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8h11", typeof(UIActivityStoreTwo_Com_Store));
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8h12", typeof(UIActivityStoreTwo_Com_BottomBg));
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8h15", typeof(UIActivityStoreTwo_Com_BottomMask));
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8h18", typeof(UIActivityStoreTwo_Com_ItemBg));
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8h1a", typeof(UIActivityStoreTwo_Com_ItemMask));
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8h2", typeof(UIActivityStoreTwo_Button_Tab));
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8h7", typeof(UIActivityStoreTwo_Com_Task));
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8ht", typeof(UIActivityStoretypeTwo_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://6dt5s4htqw8hv", typeof(UIActivityStoretypeTwo_Button_TaskStatus));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		Loader_BG = (GLoader)GetChildAt(0);
		ActivityStore_btn_act = (UIActivityStoreTwo_Button_Tab)GetChildAt(1);
		ActivityStore_btn_str = (UIActivityStoreTwo_Button_Tab)GetChildAt(2);
		com_Task = (UIActivityStoreTwo_Com_Task)GetChildAt(3);
		com_Store = (UIActivityStoreTwo_Com_Store)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
