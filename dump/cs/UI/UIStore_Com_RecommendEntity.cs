using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_RecommendEntity : GComponent
{
	public UIStore_Button_RecommendItem btn_Third;

	public UIStore_Button_RecommendItem btn_Second;

	public UIStore_Button_RecommendItem btn_First;

	public Transition Reset;

	public Transition ShowDown;

	public const string URL = "ui://zyd0rl007h6sqq35";

	public static UIStore_Com_RecommendEntity CreateInstance()
	{
		return (UIStore_Com_RecommendEntity)UIPackage.CreateObject("Store", "Store_Com_RecommendEntity");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Third = (UIStore_Button_RecommendItem)GetChildAt(0);
		btn_Second = (UIStore_Button_RecommendItem)GetChildAt(1);
		btn_First = (UIStore_Button_RecommendItem)GetChildAt(2);
		Reset = GetTransitionAt(0);
		ShowDown = GetTransitionAt(1);
	}
}
