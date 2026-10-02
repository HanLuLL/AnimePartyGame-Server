using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Com_FightInfo : GComponent
{
	public UIAccountInfo_Button_Replay btn_ReplaySave;

	public UIAccountInfo_Button_ReplaySaving btn_ReplaySaving;

	public UIAccountInfo_Button_Replay btn_ReplayPlay;

	public UIAccountInfo_Button_RankInfo com_FirstLabel;

	public UIAccountInfo_Button_RankInfo com_SecondLabel;

	public UIAccountInfo_Button_RankInfo com_ThirdLabel;

	public UIAccountInfo_Button_RankInfo com_ForthLabel;

	public GTextField txt_Time;

	public GButton btn_Accuse;

	public GButton btn_Block;

	public GGroup group_Operation;

	public GTextField txt_ReplayId;

	public GButton btn_CopyReplayId;

	public Transition Cut_in;

	public const string URL = "ui://iepldke7zhzti";

	public static UIAccountInfo_Com_FightInfo CreateInstance()
	{
		return (UIAccountInfo_Com_FightInfo)UIPackage.CreateObject("AccountInfo", "AccountInfo_Com_FightInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_ReplaySave = (UIAccountInfo_Button_Replay)GetChildAt(0);
		btn_ReplaySaving = (UIAccountInfo_Button_ReplaySaving)GetChildAt(1);
		btn_ReplayPlay = (UIAccountInfo_Button_Replay)GetChildAt(2);
		com_FirstLabel = (UIAccountInfo_Button_RankInfo)GetChildAt(6);
		com_SecondLabel = (UIAccountInfo_Button_RankInfo)GetChildAt(7);
		com_ThirdLabel = (UIAccountInfo_Button_RankInfo)GetChildAt(8);
		com_ForthLabel = (UIAccountInfo_Button_RankInfo)GetChildAt(9);
		txt_Time = (GTextField)GetChildAt(11);
		btn_Accuse = (GButton)GetChildAt(12);
		btn_Block = (GButton)GetChildAt(13);
		group_Operation = (GGroup)GetChildAt(14);
		txt_ReplayId = (GTextField)GetChildAt(15);
		btn_CopyReplayId = (GButton)GetChildAt(16);
		Cut_in = GetTransitionAt(0);
	}
}
