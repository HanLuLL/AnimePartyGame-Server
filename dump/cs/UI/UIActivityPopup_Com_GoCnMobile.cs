using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityPopup_Com_GoCnMobile : GComponent
{
	public Controller status;

	public GLoader loader_BG;

	public GTextField txt_time;

	public Transition Cut_in;

	public const string URL = "ui://3tvdl51qfmns3m";

	public static UIActivityPopup_Com_GoCnMobile CreateInstance()
	{
		return (UIActivityPopup_Com_GoCnMobile)UIPackage.CreateObject("ActivityPopup", "ActivityPopup_Com_GoCnMobile");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		txt_time = (GTextField)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
