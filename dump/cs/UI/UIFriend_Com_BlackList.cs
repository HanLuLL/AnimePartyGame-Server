using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Com_BlackList : GComponent
{
	public GTextField txt_blackNum;

	public GList list_Friends;

	public const string URL = "ui://2wec2q8z7h6sw";

	public static UIFriend_Com_BlackList CreateInstance()
	{
		return (UIFriend_Com_BlackList)UIPackage.CreateObject("Friend", "Friend_Com_BlackList");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_blackNum = (GTextField)GetChildAt(3);
		list_Friends = (GList)GetChildAt(7);
	}
}
