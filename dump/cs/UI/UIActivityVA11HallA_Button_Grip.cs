using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityVA11HallA_Button_Grip : GButton
{
	public GImage grip;

	public const string URL = "ui://zlysd2gupgzf4u";

	public static UIActivityVA11HallA_Button_Grip CreateInstance()
	{
		return (UIActivityVA11HallA_Button_Grip)UIPackage.CreateObject("ActivityVA11HallA", "ActivityVA11HallA_Button_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GImage)GetChildAt(0);
	}
}
