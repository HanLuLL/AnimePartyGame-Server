using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettlement_com_end : GComponent
{
	public Controller NewRecord;

	public Controller NewRanking;

	public Controller language;

	public Controller type;

	public Controller Next;

	public GGraph loadbg;

	public GTextField Score_Txt;

	public GTextField Rank_text;

	public GTextField HistoricalScore_text;

	public GTextField HistoricalRank_text;

	public UISettlement_Com_Info com_info;

	public GButton Friendrank_Btn;

	public GButton Again_Btn;

	public GButton Info_Btn;

	public GButton Next_Btn;

	public Transition Cut_in;

	public const string URL = "ui://wwhrkd30uytml";

	public static UISettlement_com_end CreateInstance()
	{
		return (UISettlement_com_end)UIPackage.CreateObject("SinglePlayerSettlement", "Settlement_com_end");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		NewRecord = GetControllerAt(0);
		NewRanking = GetControllerAt(1);
		language = GetControllerAt(2);
		type = GetControllerAt(3);
		Next = GetControllerAt(4);
		loadbg = (GGraph)GetChildAt(0);
		Score_Txt = (GTextField)GetChildAt(6);
		Rank_text = (GTextField)GetChildAt(7);
		HistoricalScore_text = (GTextField)GetChildAt(13);
		HistoricalRank_text = (GTextField)GetChildAt(14);
		com_info = (UISettlement_Com_Info)GetChildAt(17);
		Friendrank_Btn = (GButton)GetChildAt(19);
		Again_Btn = (GButton)GetChildAt(20);
		Info_Btn = (GButton)GetChildAt(21);
		Next_Btn = (GButton)GetChildAt(22);
		Cut_in = GetTransitionAt(0);
	}
}
