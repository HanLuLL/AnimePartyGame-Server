using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleRelicInfo_Button_Chat : GButton
{
	public GLoader btn_Chat;

	public const string URL = "ui://ethkhr1hrmeyh";

	public static UIBattleRelicInfo_Button_Chat CreateInstance()
	{
		return (UIBattleRelicInfo_Button_Chat)UIPackage.CreateObject("BattleRelicInfo", "BattleRelicInfo_Button_Chat");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Chat = (GLoader)GetChildAt(0);
	}
}
