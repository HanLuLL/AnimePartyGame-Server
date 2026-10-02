using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_ItemBg : GComponent
{
	public Controller SetColor;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00r3jjqq4s";

	public static UIStore_Com_ItemBg CreateInstance()
	{
		return (UIStore_Com_ItemBg)UIPackage.CreateObject("Store", "Store_Com_ItemBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
		Cut_in = GetTransitionAt(0);
	}
}
