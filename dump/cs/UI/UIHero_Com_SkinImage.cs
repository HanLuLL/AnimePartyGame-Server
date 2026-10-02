using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_SkinImage : GComponent
{
	public GLoader loader_Skin;

	public GGraph graph_Effect;

	public GComponent com_Qulity;

	public const string URL = "ui://7qkd4lqxq93cq2u";

	public static UIHero_Com_SkinImage CreateInstance()
	{
		return (UIHero_Com_SkinImage)UIPackage.CreateObject("Hero", "Hero_Com_SkinImage");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Skin = (GLoader)GetChildAt(1);
		graph_Effect = (GGraph)GetChildAt(3);
		com_Qulity = (GComponent)GetChildAt(4);
	}
}
