using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomTerms_FullScreenTip : GComponent
{
	public GList list_terms;

	public Transition Cut_in;

	public Transition Loop;

	public const string URL = "ui://tzpop51dejjwb";

	public static UIRoomTerms_FullScreenTip CreateInstance()
	{
		return (UIRoomTerms_FullScreenTip)UIPackage.CreateObject("RoomTerms", "RoomTerms_FullScreenTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_terms = (GList)GetChildAt(18);
		Cut_in = GetTransitionAt(0);
		Loop = GetTransitionAt(1);
	}
}
