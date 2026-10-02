using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExplain_Com_PVE : GComponent
{
	public GLabel bottom;

	public GLoader loader_Image;

	public GRichTextField txt_Explain;

	public GTextField txt_Page;

	public GButton btn_Left;

	public GButton btn_Right;

	public const string URL = "ui://5wai1nhyl6o6at";

	public static UIExplain_Com_PVE CreateInstance()
	{
		return (UIExplain_Com_PVE)UIPackage.CreateObject("Explain", "Explain_Com_PVE");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bottom = (GLabel)GetChildAt(0);
		loader_Image = (GLoader)GetChildAt(1);
		txt_Explain = (GRichTextField)GetChildAt(2);
		txt_Page = (GTextField)GetChildAt(3);
		btn_Left = (GButton)GetChildAt(4);
		btn_Right = (GButton)GetChildAt(5);
	}
}
