using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_Grip3 : GButton
{
	public GGraph grip;

	public const string URL = "ui://m6sn3r22ednmqq46";

	public static UIButton_Grip3 CreateInstance()
	{
		return (UIButton_Grip3)UIPackage.CreateObject("Common_External", "Button_Grip3");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
