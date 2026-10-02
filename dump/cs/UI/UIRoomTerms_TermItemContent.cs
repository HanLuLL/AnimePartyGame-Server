using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomTerms_TermItemContent : GComponent
{
	public GTextField txt_content;

	public const string URL = "ui://tzpop51di0pfl1e";

	public static UIRoomTerms_TermItemContent CreateInstance()
	{
		return (UIRoomTerms_TermItemContent)UIPackage.CreateObject("RoomTerms", "RoomTerms_TermItemContent");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_content = (GTextField)GetChildAt(0);
	}
}
