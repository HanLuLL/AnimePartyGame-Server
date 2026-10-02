using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityVA11HallAPanel : GComponent
{
	public Controller tab;

	public GLoader Loader_BG;

	public UIActivityVA11HallA_Com_Task com_Task;

	public UIActivityVA11HallA_Com_Store com_Store;

	public GList list_Tabs;

	public GButton btn_Return;

	public Transition Cut_in;

	public const string URL = "ui://zlysd2gupgzf4a";

	public static UIActivityVA11HallAPanel CreateInstance()
	{
		BindAll();
		return (UIActivityVA11HallAPanel)UIPackage.CreateObject("ActivityVA11HallA", "ActivityVA11HallAPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://zlysd2gugu0j76", typeof(UIActivityVA11HallA_Com_Animation));
		UIObjectFactory.SetPackageItemExtension("ui://zlysd2gupgzf4a", typeof(UIActivityVA11HallAPanel));
		UIObjectFactory.SetPackageItemExtension("ui://zlysd2gupgzf4c", typeof(UIActivityVA11HallA_Com_Task));
		UIObjectFactory.SetPackageItemExtension("ui://zlysd2gupgzf4k", typeof(UIActivityVA11HallA_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://zlysd2gupgzf4m", typeof(UIActivityVA11HallA_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://zlysd2gupgzf4u", typeof(UIActivityVA11HallA_Button_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://zlysd2gupgzf4w", typeof(UIActivityVA11HallA_Button_Toggle));
		UIObjectFactory.SetPackageItemExtension("ui://zlysd2gupgzf4y", typeof(UIActivityVA11HallA_Com_Store));
		UIObjectFactory.SetPackageItemExtension("ui://zlysd2gupgzf4z", typeof(UIActivityVA11HallA_Button_Tab));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		Loader_BG = (GLoader)GetChildAt(0);
		com_Task = (UIActivityVA11HallA_Com_Task)GetChildAt(1);
		com_Store = (UIActivityVA11HallA_Com_Store)GetChildAt(2);
		list_Tabs = (GList)GetChildAt(3);
		btn_Return = (GButton)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
