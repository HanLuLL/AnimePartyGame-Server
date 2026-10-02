using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_SelectTips : GComponent
{
	public GTextField txt_Tutorial;

	public Transition Loop;

	public const string URL = "ui://1ov1i0v9ul3cq4w";

	public static UICom_SelectTips CreateInstance()
	{
		return (UICom_SelectTips)UIPackage.CreateObject("Common_Internal", "Com_SelectTips");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Tutorial = (GTextField)GetChildAt(2);
		Loop = GetTransitionAt(0);
	}
}
