using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityVA11HallA_Button_Toggle : GButton
{
	public GGraph di_0;

	public GImage di_1;

	public const string URL = "ui://zlysd2gupgzf4w";

	public static UIActivityVA11HallA_Button_Toggle CreateInstance()
	{
		return (UIActivityVA11HallA_Button_Toggle)UIPackage.CreateObject("ActivityVA11HallA", "ActivityVA11HallA_Button_Toggle");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di_0 = (GGraph)GetChildAt(0);
		di_1 = (GImage)GetChildAt(1);
	}
}
