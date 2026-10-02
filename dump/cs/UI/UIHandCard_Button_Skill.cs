using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHandCard_Button_Skill : GButton
{
	public GGraph di_Up;

	public GGraph di_Down;

	public GTextField txt_Name;

	public const string URL = "ui://vflhnh8dqs184n";

	public static UIHandCard_Button_Skill CreateInstance()
	{
		return (UIHandCard_Button_Skill)UIPackage.CreateObject("HandCard", "HandCard_Button_Skill");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di_Up = (GGraph)GetChildAt(0);
		di_Down = (GGraph)GetChildAt(1);
		txt_Name = (GTextField)GetChildAt(2);
	}
}
