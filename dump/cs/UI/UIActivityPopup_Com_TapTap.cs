using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityPopup_Com_TapTap : GComponent
{
	public Controller status;

	public GLoader loader_BG;

	public GTextField txt_desc;

	public GButton btn_Item;

	public GButton btn_Go;

	public Transition Cut_in;

	public const string URL = "ui://3tvdl51q8f672n";

	public static UIActivityPopup_Com_TapTap CreateInstance()
	{
		return (UIActivityPopup_Com_TapTap)UIPackage.CreateObject("ActivityPopup", "ActivityPopup_Com_TapTap");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		txt_desc = (GTextField)GetChildAt(1);
		btn_Item = (GButton)GetChildAt(3);
		btn_Go = (GButton)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
