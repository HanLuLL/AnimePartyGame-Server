using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_SwitchTab : GComponent
{
	public Controller storeType;

	public GList list_tab;

	public GButton up_btn;

	public GButton down_btn;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00r3jjqq3s";

	public static UIStore_SwitchTab CreateInstance()
	{
		return (UIStore_SwitchTab)UIPackage.CreateObject("Store", "Store_SwitchTab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		storeType = GetControllerAt(0);
		list_tab = (GList)GetChildAt(3);
		up_btn = (GButton)GetChildAt(4);
		down_btn = (GButton)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
