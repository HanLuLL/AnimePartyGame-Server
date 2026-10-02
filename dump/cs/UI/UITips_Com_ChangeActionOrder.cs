using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_ChangeActionOrder : GComponent
{
	public GTextField txt_Content;

	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8ml8aas83";

	public static UITips_Com_ChangeActionOrder CreateInstance()
	{
		return (UITips_Com_ChangeActionOrder)UIPackage.CreateObject("Tips", "Tips_Com_ChangeActionOrder");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Content = (GTextField)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
	}
}
