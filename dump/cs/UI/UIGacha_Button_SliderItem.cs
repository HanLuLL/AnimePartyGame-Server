using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Button_SliderItem : GButton
{
	public Controller status;

	public GGraph graph_ReplaceEffect_Bottom;

	public GGraph graph_ReplaceEffect_Top;

	public GLoader loader_icon;

	public GTextField txt_count;

	public GLoader loader_icon2;

	public Transition Cut_in;

	public const string URL = "ui://j90wpcmnfs8hqq2l";

	public static UIGacha_Button_SliderItem CreateInstance()
	{
		return (UIGacha_Button_SliderItem)UIPackage.CreateObject("Gacha", "Gacha_Button_SliderItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		graph_ReplaceEffect_Bottom = (GGraph)GetChildAt(0);
		graph_ReplaceEffect_Top = (GGraph)GetChildAt(3);
		loader_icon = (GLoader)GetChildAt(7);
		txt_count = (GTextField)GetChildAt(8);
		loader_icon2 = (GLoader)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
