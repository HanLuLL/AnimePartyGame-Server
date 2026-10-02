using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type4_OpenScratchOff : GComponent
{
	public Controller redPoint;

	public GTextField txt_Progress;

	public GTextField txt_ItemCount;

	public GButton btn_Item;

	public GGraph btn_Open;

	public Transition redPointShow;

	public const string URL = "ui://c1v285vtpu2ix";

	public static UIActivity_Com_Type4_OpenScratchOff CreateInstance()
	{
		return (UIActivity_Com_Type4_OpenScratchOff)UIPackage.CreateObject("ActivityNgo", "Activity_Com_Type4_OpenScratchOff");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(0);
		txt_Progress = (GTextField)GetChildAt(5);
		txt_ItemCount = (GTextField)GetChildAt(6);
		btn_Item = (GButton)GetChildAt(8);
		btn_Open = (GGraph)GetChildAt(10);
		redPointShow = GetTransitionAt(0);
	}
}
