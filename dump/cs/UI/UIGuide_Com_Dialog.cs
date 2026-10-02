using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuide_Com_Dialog : GComponent
{
	public GLoader loader_Icon;

	public GRichTextField txt_Explain;

	public GButton btn_Sure;

	public const string URL = "ui://kogqu0l2hj7w2";

	public static UIGuide_Com_Dialog CreateInstance()
	{
		return (UIGuide_Com_Dialog)UIPackage.CreateObject("Guide", "Guide_Com_Dialog");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(1);
		txt_Explain = (GRichTextField)GetChildAt(2);
		btn_Sure = (GButton)GetChildAt(3);
	}
}
