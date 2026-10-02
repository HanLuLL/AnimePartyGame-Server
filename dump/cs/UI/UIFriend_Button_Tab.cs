using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Button_Tab : GButton
{
	public Controller redPoint;

	public const string URL = "ui://2wec2q8z7h6sj";

	public static UIFriend_Button_Tab CreateInstance()
	{
		return (UIFriend_Button_Tab)UIPackage.CreateObject("Friend", "Friend_Button_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
	}
}
