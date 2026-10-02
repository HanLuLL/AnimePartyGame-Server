using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStory_Com_PerformItem : GComponent
{
	public GLoader loader_Skin;

	public GLoader loader_Expression;

	public const string URL = "ui://abmw5cfoiznzn";

	public static UIStory_Com_PerformItem CreateInstance()
	{
		return (UIStory_Com_PerformItem)UIPackage.CreateObject("Story", "Story_Com_PerformItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Skin = (GLoader)GetChildAt(0);
		loader_Expression = (GLoader)GetChildAt(1);
	}
}
