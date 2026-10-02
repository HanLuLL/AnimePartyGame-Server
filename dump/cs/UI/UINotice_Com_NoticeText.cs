using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINotice_Com_NoticeText : GLabel
{
	public GRichTextField txt_title;

	public const string URL = "ui://bwvbo0x0dx3g4";

	public static UINotice_Com_NoticeText CreateInstance()
	{
		return (UINotice_Com_NoticeText)UIPackage.CreateObject("Notice", "Notice_Com_NoticeText");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_title = (GRichTextField)GetChildAt(0);
	}
}
