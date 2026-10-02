using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICardWin_Com_CardResult : GComponent
{
	public Controller targetState;

	public UICardWin_Com_Loader_Lit com_Self;

	public GList list_Traget;

	public GComponent com_ShowCard;

	public GComponent com_reverseCard;

	public GButton btn_CancelQuickCard;

	public Transition showCard;

	public Transition showWaitReverse;

	public const string URL = "ui://bi8fdi6nqgrb1w";

	public static UICardWin_Com_CardResult CreateInstance()
	{
		return (UICardWin_Com_CardResult)UIPackage.CreateObject("Card", "CardWin_Com_CardResult");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		targetState = GetControllerAt(0);
		com_Self = (UICardWin_Com_Loader_Lit)GetChildAt(0);
		list_Traget = (GList)GetChildAt(2);
		com_ShowCard = (GComponent)GetChildAt(4);
		com_reverseCard = (GComponent)GetChildAt(5);
		btn_CancelQuickCard = (GButton)GetChildAt(6);
		showCard = GetTransitionAt(0);
		showWaitReverse = GetTransitionAt(1);
	}
}
