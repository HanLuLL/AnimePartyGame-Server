using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Btn_BattlePass : GButton
{
	public GGraph btn_OpenBattlePass;

	public const string URL = "ui://u7xbdcguejpwq5l";

	public static UIHome_Btn_BattlePass CreateInstance()
	{
		return (UIHome_Btn_BattlePass)UIPackage.CreateObject("Home", "Home_Btn_BattlePass");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_OpenBattlePass = (GGraph)GetChildAt(0);
	}
}
