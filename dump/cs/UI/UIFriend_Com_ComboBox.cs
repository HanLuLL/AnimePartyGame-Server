using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Com_ComboBox : GComboBox
{
	public Controller SetArrow;

	public const string URL = "ui://2wec2q8znzp81v";

	public static UIFriend_Com_ComboBox CreateInstance()
	{
		return (UIFriend_Com_ComboBox)UIPackage.CreateObject("Friend", "Friend_Com_ComboBox");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetArrow = GetControllerAt(1);
	}
}
