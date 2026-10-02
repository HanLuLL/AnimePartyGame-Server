using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDice_Btn_Throw : GButton
{
	public GRichTextField Txt_Dice_1;

	public GRichTextField Txt_Dice_2;

	public const string URL = "ui://lypih982eo8h4";

	public static UIActivityDice_Btn_Throw CreateInstance()
	{
		return (UIActivityDice_Btn_Throw)UIPackage.CreateObject("ActivityDice", "ActivityDice_Btn_Throw");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Txt_Dice_1 = (GRichTextField)GetChildAt(1);
		Txt_Dice_2 = (GRichTextField)GetChildAt(2);
	}
}
