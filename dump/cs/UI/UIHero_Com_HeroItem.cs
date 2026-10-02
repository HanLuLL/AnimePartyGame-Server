using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_HeroItem : GComponent
{
	public Controller Ultimate;

	public GLoader loader_Character;

	public GGraph graph_Effect;

	public GComponent com_Qulity;

	public const string URL = "ui://7qkd4lqxq93cq2w";

	public static UIHero_Com_HeroItem CreateInstance()
	{
		return (UIHero_Com_HeroItem)UIPackage.CreateObject("Hero", "Hero_Com_HeroItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Ultimate = GetControllerAt(0);
		loader_Character = (GLoader)GetChildAt(1);
		graph_Effect = (GGraph)GetChildAt(2);
		com_Qulity = (GComponent)GetChildAt(4);
	}
}
