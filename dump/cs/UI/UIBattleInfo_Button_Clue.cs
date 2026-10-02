using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Button_Clue : GButton
{
	public GTextField txt_Title;

	public GTextField txt_Count;

	public const string URL = "ui://fxejlqlfmgj007";

	public static UIBattleInfo_Button_Clue CreateInstance()
	{
		return (UIBattleInfo_Button_Clue)UIPackage.CreateObject("BattleInfo", "BattleInfo_Button_Clue");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(1);
		txt_Count = (GTextField)GetChildAt(2);
	}
}
