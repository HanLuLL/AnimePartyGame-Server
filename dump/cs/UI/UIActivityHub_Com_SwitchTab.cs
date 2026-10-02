using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityHub_Com_SwitchTab : GButton
{
	public Controller redPoint;

	public GTextField txt_selectTitle;

	public GTextField txt_title;

	public Transition Switch_in;

	public Transition Switch_out;

	public Transition RedOFF;

	public Transition Cut_in;

	public const string URL = "ui://h82y3ndtb7mb8";

	public static UIActivityHub_Com_SwitchTab CreateInstance()
	{
		return (UIActivityHub_Com_SwitchTab)UIPackage.CreateObject("ActivityHub", "ActivityHub_Com_SwitchTab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		txt_selectTitle = (GTextField)GetChildAt(1);
		txt_title = (GTextField)GetChildAt(5);
		Switch_in = GetTransitionAt(0);
		Switch_out = GetTransitionAt(1);
		RedOFF = GetTransitionAt(2);
		Cut_in = GetTransitionAt(3);
	}
}
