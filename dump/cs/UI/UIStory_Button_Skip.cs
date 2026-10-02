using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStory_Button_Skip : GButton
{
	public GTextField txt_Title;

	public const string URL = "ui://abmw5cfohkbbc";

	public static UIStory_Button_Skip CreateInstance()
	{
		return (UIStory_Button_Skip)UIPackage.CreateObject("Story", "Story_Button_Skip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(2);
	}
}
