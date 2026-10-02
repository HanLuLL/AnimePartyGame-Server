using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityHub_Com_Tab : GComponent
{
	public GList list;

	public GButton up_btn;

	public GButton down_btn;

	public Transition Cut_in;

	public const string URL = "ui://h82y3ndthe6d1";

	public static UIActivityHub_Com_Tab CreateInstance()
	{
		return (UIActivityHub_Com_Tab)UIPackage.CreateObject("ActivityHub", "ActivityHub_Com_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list = (GList)GetChildAt(1);
		up_btn = (GButton)GetChildAt(2);
		down_btn = (GButton)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
