using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityBingo_Button_Done : GButton
{
	public GLoader loader_RedPoint;

	public const string URL = "ui://1pgen0em6brro";

	public static UIActivityBingo_Button_Done CreateInstance()
	{
		return (UIActivityBingo_Button_Done)UIPackage.CreateObject("ActivityBingo", "ActivityBingo_Button_Done");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_RedPoint = (GLoader)GetChildAt(2);
	}
}
