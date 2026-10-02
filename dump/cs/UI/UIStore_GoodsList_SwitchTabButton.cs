using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_GoodsList_SwitchTabButton : GButton
{
	public Controller redpoint;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00r3jjqq4t";

	public static UIStore_GoodsList_SwitchTabButton CreateInstance()
	{
		return (UIStore_GoodsList_SwitchTabButton)UIPackage.CreateObject("Store", "Store_GoodsList_SwitchTabButton");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redpoint = GetControllerAt(1);
		Cut_in = GetTransitionAt(0);
	}
}
