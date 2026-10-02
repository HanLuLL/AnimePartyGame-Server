using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_Emoji : GComponent
{
	public GList list_Expression;

	public GTextField txt_SkinProgress;

	public const string URL = "ui://7qkd4lqxq93cq2s";

	public static UIHero_Com_Emoji CreateInstance()
	{
		return (UIHero_Com_Emoji)UIPackage.CreateObject("Hero", "Hero_Com_Emoji");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Expression = (GList)GetChildAt(0);
		txt_SkinProgress = (GTextField)GetChildAt(1);
	}
}
