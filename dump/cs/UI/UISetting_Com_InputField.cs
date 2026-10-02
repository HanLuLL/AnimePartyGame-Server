using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_InputField : GComponent
{
	public GImage bg;

	public GTextInput title;

	public const string URL = "ui://iy1joavto1n810";

	public static UISetting_Com_InputField CreateInstance()
	{
		return (UISetting_Com_InputField)UIPackage.CreateObject("Setting", "Setting_Com_InputField");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bg = (GImage)GetChildAt(0);
		title = (GTextInput)GetChildAt(1);
	}
}
