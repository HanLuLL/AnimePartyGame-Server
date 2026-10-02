using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_DiceInfo : GComponent
{
	public Controller diceCount;

	public UISinglePlayer_Com_Point com_Point;

	public UISinglePlayer_Com_Point com_Point_1;

	public UISinglePlayer_Com_Point com_Point_2;

	public const string URL = "ui://mi9vm3w0kvexq49";

	public static UISinglePlayer_Com_DiceInfo CreateInstance()
	{
		return (UISinglePlayer_Com_DiceInfo)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_DiceInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		diceCount = GetControllerAt(0);
		com_Point = (UISinglePlayer_Com_Point)GetChildAt(0);
		com_Point_1 = (UISinglePlayer_Com_Point)GetChildAt(1);
		com_Point_2 = (UISinglePlayer_Com_Point)GetChildAt(2);
	}
}
