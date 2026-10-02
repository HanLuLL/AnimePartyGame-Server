using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Icon_Counter : GComponent
{
	public GLoader atk;

	public const string URL = "ui://xuaw6o8jbfwlq2r";

	public static UICom_Icon_Counter CreateInstance()
	{
		return (UICom_Icon_Counter)UIPackage.CreateObject("Common", "Com_Icon_Counter");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		atk = (GLoader)GetChildAt(0);
	}
}
