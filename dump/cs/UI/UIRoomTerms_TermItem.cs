using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomTerms_TermItem : GComponent
{
	public Controller mutatorType;

	public GTextField txt_name;

	public UIRoomTerms_TermItemContent com_content;

	public GLoader img_termIcon;

	public Transition IconLoop;

	public Transition Cut_in;

	public const string URL = "ui://tzpop51do3tt1";

	public static UIRoomTerms_TermItem CreateInstance()
	{
		return (UIRoomTerms_TermItem)UIPackage.CreateObject("RoomTerms", "RoomTerms_TermItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mutatorType = GetControllerAt(0);
		txt_name = (GTextField)GetChildAt(5);
		com_content = (UIRoomTerms_TermItemContent)GetChildAt(6);
		img_termIcon = (GLoader)GetChildAt(17);
		IconLoop = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
	}
}
