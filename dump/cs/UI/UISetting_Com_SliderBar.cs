using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_SliderBar : GComponent
{
	public GImage zheZhao;

	public GImage bg;

	public const string URL = "ui://iy1joavto1n8t";

	public static UISetting_Com_SliderBar CreateInstance()
	{
		return (UISetting_Com_SliderBar)UIPackage.CreateObject("Setting", "Setting_Com_SliderBar");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
		bg = (GImage)GetChildAt(1);
	}
}
