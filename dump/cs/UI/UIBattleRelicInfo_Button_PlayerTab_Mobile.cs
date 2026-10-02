using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleRelicInfo_Button_PlayerTab_Mobile : GButton
{
	public GImage down;

	public GImage over;

	public GImage up;

	public const string URL = "ui://ethkhr1hfcnul";

	public static UIBattleRelicInfo_Button_PlayerTab_Mobile CreateInstance()
	{
		return (UIBattleRelicInfo_Button_PlayerTab_Mobile)UIPackage.CreateObject("BattleRelicInfo", "BattleRelicInfo_Button_PlayerTab_Mobile");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		down = (GImage)GetChildAt(0);
		over = (GImage)GetChildAt(1);
		up = (GImage)GetChildAt(2);
	}
}
