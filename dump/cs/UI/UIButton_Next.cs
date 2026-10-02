using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_Next : GButton
{
	public Controller ArrowColor;

	public Transition Loop;

	public const string URL = "ui://xuaw6o8jpx78b3";

	public static UIButton_Next CreateInstance()
	{
		return (UIButton_Next)UIPackage.CreateObject("Common", "Button_Next");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		ArrowColor = GetControllerAt(1);
		Loop = GetTransitionAt(0);
	}
}
