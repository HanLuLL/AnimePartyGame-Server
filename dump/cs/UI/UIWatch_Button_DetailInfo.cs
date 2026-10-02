using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWatch_Button_DetailInfo : GButton
{
	public GTextField txt_CharaterName;

	public GTextField txt_CharaterNick;

	public GTextField txt_HP;

	public GTextField txt_ATK;

	public GTextField txt_DEF;

	public GTextField txt_CD;

	public GList list_Skill;

	public const string URL = "ui://sv6gwhbej6om4";

	public static UIWatch_Button_DetailInfo CreateInstance()
	{
		return (UIWatch_Button_DetailInfo)UIPackage.CreateObject("Watch", "Watch_Button_DetailInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_CharaterName = (GTextField)GetChildAt(3);
		txt_CharaterNick = (GTextField)GetChildAt(4);
		txt_HP = (GTextField)GetChildAt(8);
		txt_ATK = (GTextField)GetChildAt(9);
		txt_DEF = (GTextField)GetChildAt(10);
		txt_CD = (GTextField)GetChildAt(12);
		list_Skill = (GList)GetChildAt(13);
	}
}
