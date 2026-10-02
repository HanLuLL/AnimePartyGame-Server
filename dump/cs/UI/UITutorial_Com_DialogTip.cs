using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Com_DialogTip : GComponent
{
	public GLoader loader_Icon;

	public GRichTextField txt_Explain;

	public const string URL = "ui://b96qpoz6iu43q";

	public static UITutorial_Com_DialogTip CreateInstance()
	{
		return (UITutorial_Com_DialogTip)UIPackage.CreateObject("Tutorial", "Tutorial_Com_DialogTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(1);
		txt_Explain = (GRichTextField)GetChildAt(2);
	}
}
