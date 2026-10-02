using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Button_Purchase : GButton
{
	public Controller type;

	public GLoader loader_BG;

	public GLoader loader_HeroIcon;

	public GTextField txt_Countdown;

	public GTextField txt_Topic;

	public GRichTextField txt_Price;

	public const string URL = "ui://ssf8xg9njz2411";

	public static UIBattlePass_Button_Purchase CreateInstance()
	{
		return (UIBattlePass_Button_Purchase)UIPackage.CreateObject("BattlePass", "BattlePass_Button_Purchase");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
		loader_BG = (GLoader)GetChildAt(0);
		loader_HeroIcon = (GLoader)GetChildAt(1);
		txt_Countdown = (GTextField)GetChildAt(3);
		txt_Topic = (GTextField)GetChildAt(4);
		txt_Price = (GRichTextField)GetChildAt(6);
	}
}
