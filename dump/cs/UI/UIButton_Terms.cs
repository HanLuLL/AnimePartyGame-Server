using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_Terms : GButton
{
	public GComponent bg;

	public const string URL = "ui://xuaw6o8jejjwq49";

	public static UIButton_Terms CreateInstance()
	{
		return (UIButton_Terms)UIPackage.CreateObject("Common", "Button_Terms");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bg = (GComponent)GetChildAt(0);
	}
}
