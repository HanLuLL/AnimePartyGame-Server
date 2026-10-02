using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Move : GButton
{
	public Controller ShowDice;

	public Controller diceCount;

	public GRichTextField txt_Progress;

	public UISinglePlayer_Com_DiceInfo com_Dice;

	public const string URL = "ui://mi9vm3w0oazx1";

	public static UISinglePlayer_Button_Move CreateInstance()
	{
		return (UISinglePlayer_Button_Move)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Button_Move");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		ShowDice = GetControllerAt(1);
		diceCount = GetControllerAt(2);
		txt_Progress = (GRichTextField)GetChildAt(4);
		com_Dice = (UISinglePlayer_Com_DiceInfo)GetChildAt(6);
	}
}
