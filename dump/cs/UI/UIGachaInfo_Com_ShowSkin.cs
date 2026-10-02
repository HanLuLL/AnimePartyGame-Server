using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGachaInfo_Com_ShowSkin : GComponent
{
	public GGraph loader_BG;

	public GLoader loader_Line;

	public GLoader loader_Skin;

	public GButton btn_SkipSkin;

	public GRichTextField txt_Dialog;

	public GGraph graph_Speaker;

	public GTextField txt_Speaker;

	public GLoader loader_Speaker;

	public Transition Cut_in;

	public Transition Loop;

	public const string URL = "ui://egrtucyhnu3za";

	public static UIGachaInfo_Com_ShowSkin CreateInstance()
	{
		return (UIGachaInfo_Com_ShowSkin)UIPackage.CreateObject("GachaInfo", "GachaInfo_Com_ShowSkin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_BG = (GGraph)GetChildAt(0);
		loader_Line = (GLoader)GetChildAt(1);
		loader_Skin = (GLoader)GetChildAt(5);
		btn_SkipSkin = (GButton)GetChildAt(17);
		txt_Dialog = (GRichTextField)GetChildAt(19);
		graph_Speaker = (GGraph)GetChildAt(20);
		txt_Speaker = (GTextField)GetChildAt(21);
		loader_Speaker = (GLoader)GetChildAt(22);
		Cut_in = GetTransitionAt(0);
		Loop = GetTransitionAt(1);
	}
}
