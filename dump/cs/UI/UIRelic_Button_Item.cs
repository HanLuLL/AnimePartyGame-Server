using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRelic_Button_Item : GButton
{
	public Controller status;

	public Controller RecommendType;

	public GComponent loader_Frame;

	public GLoader loader_Icon;

	public GTextField txt_tltle;

	public GRichTextField txt_Desc;

	public GComponent com_Keyword;

	public GImage txt_Recommend;

	public GImage txt_ParticularlyRecommend;

	public GGraph graph_Gold;

	public Transition cutIn;

	public Transition Starloop;

	public Transition StarCutin;

	public const string URL = "ui://kjm6oxy2l6o61";

	public static UIRelic_Button_Item CreateInstance()
	{
		return (UIRelic_Button_Item)UIPackage.CreateObject("Relic", "Relic_Button_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		RecommendType = GetControllerAt(2);
		loader_Frame = (GComponent)GetChildAt(0);
		loader_Icon = (GLoader)GetChildAt(1);
		txt_tltle = (GTextField)GetChildAt(2);
		txt_Desc = (GRichTextField)GetChildAt(3);
		com_Keyword = (GComponent)GetChildAt(5);
		txt_Recommend = (GImage)GetChildAt(6);
		txt_ParticularlyRecommend = (GImage)GetChildAt(7);
		graph_Gold = (GGraph)GetChildAt(19);
		cutIn = GetTransitionAt(0);
		Starloop = GetTransitionAt(1);
		StarCutin = GetTransitionAt(2);
	}
}
