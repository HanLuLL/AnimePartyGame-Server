using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_RecommendItem : GButton
{
	public Transition RightDisplay;

	public Transition RightDisappear;

	public const string URL = "ui://zyd0rl007h6sqq2z";

	public static UIStore_Button_RecommendItem CreateInstance()
	{
		return (UIStore_Button_RecommendItem)UIPackage.CreateObject("Store", "Store_Button_RecommendItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		RightDisplay = GetTransitionAt(0);
		RightDisappear = GetTransitionAt(1);
	}
}
