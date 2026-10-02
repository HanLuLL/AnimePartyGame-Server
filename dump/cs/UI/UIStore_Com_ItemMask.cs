using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_ItemMask : GComponent
{
	public GImage zhezhao;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00r3jjqq4n";

	public static UIStore_Com_ItemMask CreateInstance()
	{
		return (UIStore_Com_ItemMask)UIPackage.CreateObject("Store", "Store_Com_ItemMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zhezhao = (GImage)GetChildAt(0);
		Cut_in = GetTransitionAt(0);
	}
}
