using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_SkinTab : GButton
{
	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00h0atqq5b";

	public static UIStore_Button_SkinTab CreateInstance()
	{
		return (UIStore_Button_SkinTab)UIPackage.CreateObject("Store", "Store_Button_SkinTab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
