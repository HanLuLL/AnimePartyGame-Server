using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINotice_Com_NoticeImage : GComponent
{
	public Controller showImage;

	public Controller Type;

	public GLoader loader_Banner;

	public const string URL = "ui://bwvbo0x0dx3g3";

	public static UINotice_Com_NoticeImage CreateInstance()
	{
		return (UINotice_Com_NoticeImage)UIPackage.CreateObject("Notice", "Notice_Com_NoticeImage");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showImage = GetControllerAt(0);
		Type = GetControllerAt(1);
		loader_Banner = (GLoader)GetChildAt(0);
	}
}
