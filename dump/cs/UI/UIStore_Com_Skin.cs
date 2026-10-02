using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_Skin : GComponent
{
	public UIStore_Com_BottomBg buttom;

	public UIStore_Com_ItemBg bg;

	public GList list_Skin;

	public GList list_SkinTab;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00vs31qq51";

	public static UIStore_Com_Skin CreateInstance()
	{
		return (UIStore_Com_Skin)UIPackage.CreateObject("Store", "Store_Com_Skin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		buttom = (UIStore_Com_BottomBg)GetChildAt(0);
		bg = (UIStore_Com_ItemBg)GetChildAt(2);
		list_Skin = (GList)GetChildAt(4);
		list_SkinTab = (GList)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
