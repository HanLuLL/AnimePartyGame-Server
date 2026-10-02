using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_Common : GComponent
{
	public GTextField txt_Content;

	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8mine4s7p";

	public static UITips_Com_Common CreateInstance()
	{
		return (UITips_Com_Common)UIPackage.CreateObject("Tips", "Tips_Com_Common");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Content = (GTextField)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
	}
}
