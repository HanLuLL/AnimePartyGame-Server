using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_NGOCounter : GComponent
{
	public GTextField txt_Counter;

	public GGroup group_Counter;

	public const string URL = "ui://1ov1i0v9qle6c0";

	public static UICom_NGOCounter CreateInstance()
	{
		return (UICom_NGOCounter)UIPackage.CreateObject("Common_Internal", "Com_NGOCounter");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Counter = (GTextField)GetChildAt(1);
		group_Counter = (GGroup)GetChildAt(2);
	}
}
