using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Button_Operate : GButton
{
	public GGroup normal;

	public Transition cut_in;

	public Transition Switchin;

	public Transition Switchout;

	public const string URL = "ui://iy1joavto1n81f";

	public static UISetting_Button_Operate CreateInstance()
	{
		return (UISetting_Button_Operate)UIPackage.CreateObject("Setting", "Setting_Button_Operate");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		normal = (GGroup)GetChildAt(3);
		cut_in = GetTransitionAt(0);
		Switchin = GetTransitionAt(1);
		Switchout = GetTransitionAt(2);
	}
}
