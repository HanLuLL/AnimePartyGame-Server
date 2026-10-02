using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICardWin_Com_UseCard : GComponent
{
	public Controller type;

	public GComponent com_ShowCard;

	public GGroup group_1;

	public GList list_targetPlayer;

	public UICardWindow_Com_SelectPoint com_SelectPoint;

	public GGroup group_3;

	public GTextField txt_LandId;

	public GButton btn_Cancel;

	public GButton btn_Sure;

	public GGroup group_4;

	public Transition cardIn;

	public Transition cardOut;

	public const string URL = "ui://bi8fdi6njlir1b";

	public static UICardWin_Com_UseCard CreateInstance()
	{
		return (UICardWin_Com_UseCard)UIPackage.CreateObject("Card", "CardWin_Com_UseCard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		com_ShowCard = (GComponent)GetChildAt(0);
		group_1 = (GGroup)GetChildAt(4);
		list_targetPlayer = (GList)GetChildAt(5);
		com_SelectPoint = (UICardWindow_Com_SelectPoint)GetChildAt(6);
		group_3 = (GGroup)GetChildAt(7);
		txt_LandId = (GTextField)GetChildAt(8);
		btn_Cancel = (GButton)GetChildAt(9);
		btn_Sure = (GButton)GetChildAt(10);
		group_4 = (GGroup)GetChildAt(11);
		cardIn = GetTransitionAt(0);
		cardOut = GetTransitionAt(1);
	}
}
