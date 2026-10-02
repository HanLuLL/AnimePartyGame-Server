using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_BossTips : GComponent
{
	public GTextField txt_Tutorial;

	public Transition Loop;

	public const string URL = "ui://1ov1i0v9iu434p";

	public static UICom_BossTips CreateInstance()
	{
		return (UICom_BossTips)UIPackage.CreateObject("Common_Internal", "Com_BossTips");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Tutorial = (GTextField)GetChildAt(2);
		Loop = GetTransitionAt(0);
	}
}
