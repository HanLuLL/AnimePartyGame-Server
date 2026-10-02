using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Com_Dialog : GButton
{
	public GLoader loader_Style;

	public GRichTextField txt_Dialog;

	public Transition Loop;

	public const string URL = "ui://b96qpoz68vxw3";

	public static UITutorial_Com_Dialog CreateInstance()
	{
		return (UITutorial_Com_Dialog)UIPackage.CreateObject("Tutorial", "Tutorial_Com_Dialog");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Style = (GLoader)GetChildAt(1);
		txt_Dialog = (GRichTextField)GetChildAt(2);
		Loop = GetTransitionAt(0);
	}
}
