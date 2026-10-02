using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Button_Victory : GButton
{
	public GTextField txt_Title;

	public const string URL = "ui://fxejlqlfg8sb83";

	public static UIBattleInfo_Button_Victory CreateInstance()
	{
		return (UIBattleInfo_Button_Victory)UIPackage.CreateObject("BattleInfo", "BattleInfo_Button_Victory");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(2);
	}
}
