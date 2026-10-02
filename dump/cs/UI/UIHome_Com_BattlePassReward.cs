using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Com_BattlePassReward : GComponent
{
	public Controller status;

	public GButton btn_Item;

	public const string URL = "ui://u7xbdcgujz24q2u";

	public static UIHome_Com_BattlePassReward CreateInstance()
	{
		return (UIHome_Com_BattlePassReward)UIPackage.CreateObject("Home", "Home_Com_BattlePassReward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		btn_Item = (GButton)GetChildAt(0);
	}
}
