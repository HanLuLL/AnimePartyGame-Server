using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Com_ApplyItem : GButton
{
	public GComponent com_PlayerLabel;

	public GTextField txt_ApplyTime;

	public const string URL = "ui://2wec2q8z7h6sx";

	public static UIFriend_Com_ApplyItem CreateInstance()
	{
		return (UIFriend_Com_ApplyItem)UIPackage.CreateObject("Friend", "Friend_Com_ApplyItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_PlayerLabel = (GComponent)GetChildAt(1);
		txt_ApplyTime = (GTextField)GetChildAt(2);
	}
}
