using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Item_Title : GLabel
{
	public GImage bg;

	public Transition cut_in;

	public const string URL = "ui://iy1joavto1n8d";

	public static UISetting_Item_Title CreateInstance()
	{
		return (UISetting_Item_Title)UIPackage.CreateObject("Setting", "Setting_Item_Title");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bg = (GImage)GetChildAt(0);
		cut_in = GetTransitionAt(0);
	}
}
