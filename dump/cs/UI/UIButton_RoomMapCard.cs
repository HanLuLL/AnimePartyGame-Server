using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_RoomMapCard : GButton
{
	public Controller showInfo;

	public GButton com_card;

	public const string URL = "ui://m6sn3r22r2u9j8s";

	public static UIButton_RoomMapCard CreateInstance()
	{
		return (UIButton_RoomMapCard)UIPackage.CreateObject("Common_External", "Button_RoomMapCard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showInfo = GetControllerAt(0);
		com_card = (GButton)GetChildAt(1);
	}
}
