using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Com_AddFriend : GComponent
{
	public Controller type;

	public Controller showResult;

	public GTextField txt_Times;

	public GTextInput txtField_Search;

	public GButton btn_SearchPlayer;

	public GList list_LastPlayer;

	public GList list_SearchPlayer;

	public const string URL = "ui://2wec2q8z7h6s9";

	public static UIFriend_Com_AddFriend CreateInstance()
	{
		return (UIFriend_Com_AddFriend)UIPackage.CreateObject("Friend", "Friend_Com_AddFriend");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		showResult = GetControllerAt(1);
		txt_Times = (GTextField)GetChildAt(5);
		txtField_Search = (GTextInput)GetChildAt(14);
		btn_SearchPlayer = (GButton)GetChildAt(15);
		list_LastPlayer = (GList)GetChildAt(19);
		list_SearchPlayer = (GList)GetChildAt(20);
	}
}
