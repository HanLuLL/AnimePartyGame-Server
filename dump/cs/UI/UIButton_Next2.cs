using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_Next2 : GButton
{
	public Transition Loop;

	public const string URL = "ui://xuaw6o8ji1yzq2w";

	public static UIButton_Next2 CreateInstance()
	{
		return (UIButton_Next2)UIPackage.CreateObject("Common", "Button_Next2");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Loop = GetTransitionAt(0);
	}
}
