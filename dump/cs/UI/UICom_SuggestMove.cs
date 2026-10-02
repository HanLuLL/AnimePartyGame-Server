using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_SuggestMove : GComponent
{
	public GTextField txt_Tutorial;

	public Transition Loop;

	public const string URL = "ui://1ov1i0v9iu434o";

	public static UICom_SuggestMove CreateInstance()
	{
		return (UICom_SuggestMove)UIPackage.CreateObject("Common_Internal", "Com_SuggestMove");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Tutorial = (GTextField)GetChildAt(2);
		Loop = GetTransitionAt(0);
	}
}
