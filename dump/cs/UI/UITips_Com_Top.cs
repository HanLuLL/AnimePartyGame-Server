using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_Top : GComponent
{
	public GTextField txt_Title;

	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8mgpj2s7n";

	public static UITips_Com_Top CreateInstance()
	{
		return (UITips_Com_Top)UIPackage.CreateObject("Tips", "Tips_Com_Top");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
	}
}
