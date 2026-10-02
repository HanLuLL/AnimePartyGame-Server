using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIProgress_Reward : GProgressBar
{
	public GProgressBar progress;

	public UIGacha_Button_SliderItem btn_item0;

	public UIGacha_Button_SliderItem btn_item1;

	public UIGacha_Button_SliderItem btn_item2;

	public UIGacha_Button_SliderItem btn_item3;

	public UIGacha_Button_SliderItem btn_item4;

	public UIGacha_Button_SliderItem btn_item5;

	public UIGacha_Button_SliderItem btn_item6;

	public UIGacha_Com_Arrow com_arrow;

	public Transition Cut_in;

	public const string URL = "ui://j90wpcmnfs8hqq2g";

	public static UIProgress_Reward CreateInstance()
	{
		return (UIProgress_Reward)UIPackage.CreateObject("Gacha", "Progress_Reward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		progress = (GProgressBar)GetChildAt(1);
		btn_item0 = (UIGacha_Button_SliderItem)GetChildAt(2);
		btn_item1 = (UIGacha_Button_SliderItem)GetChildAt(3);
		btn_item2 = (UIGacha_Button_SliderItem)GetChildAt(4);
		btn_item3 = (UIGacha_Button_SliderItem)GetChildAt(5);
		btn_item4 = (UIGacha_Button_SliderItem)GetChildAt(6);
		btn_item5 = (UIGacha_Button_SliderItem)GetChildAt(7);
		btn_item6 = (UIGacha_Button_SliderItem)GetChildAt(8);
		com_arrow = (UIGacha_Com_Arrow)GetChildAt(10);
		Cut_in = GetTransitionAt(0);
	}
}
