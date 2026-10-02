using System.Collections.Generic;
using FairyGUI;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class FriendLeaderboardWindow : BaseWindow
{
	private UIFirendLeaderBoard_Leaderboard_List _curList;

	private readonly List<FriendLeaderboardData> _curFriendScores = SimpleSingletonProvider<GameLogicManager>.inst.friend.friendScoresList;

	private UIFriendLeaderboardWindow win => base.contentPane as UIFriendLeaderboardWindow;

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public FriendLeaderboardWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIFriendLeaderboardWindow.CreateInstance();
		base.OnInit();
		if (win != null)
		{
			_curList = win.list_FriendLeaderboard;
			if (_curList != null)
			{
				_curList.list_friend.SetVirtual();
				_curList.list_friend.itemRenderer = RenderFriendItem;
			}
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (win != null)
		{
			win.Cut_in.Play();
			win.FriendClose_btn.onClick.Set(base.Hide);
			win.mohu.onClick.Add(base.Hide);
			RefreshUI();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		win.mohu.onClick.Remove(base.Hide);
	}

	private void RefreshUI()
	{
		if (win != null && _curList != null)
		{
			RefreshMeData();
			if (_curFriendScores != null && _curFriendScores.Count > 0)
			{
				_curList.Status.selectedIndex = 1;
				_curList.list_friend.numItems = _curFriendScores.Count;
			}
			else
			{
				_curList.Status.selectedIndex = 0;
				_curList.list_friend.numItems = 0;
			}
		}
	}

	private void RefreshMeData()
	{
		UIFirendLeaderBoard_label uIFirendLeaderBoard_label = win?.Me_Data;
		if (uIFirendLeaderBoard_label == null)
		{
			return;
		}
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		FriendLeaderboardData friendLeaderboardData = default(FriendLeaderboardData);
		int num = -1;
		for (int i = 0; i < _curFriendScores.Count; i++)
		{
			if (_curFriendScores[i].playerId == playerInfo.Id)
			{
				friendLeaderboardData = _curFriendScores[i];
				num = i;
				break;
			}
		}
		if (friendLeaderboardData.Score == -1 || num == -1)
		{
			uIFirendLeaderBoard_label.Ranking_text.text = "";
			uIFirendLeaderBoard_label.Score_text.text = "--";
			uIFirendLeaderBoard_label.Status.selectedIndex = 0;
			if (playerInfo != null)
			{
				SetPlayerIcon(uIFirendLeaderBoard_label, playerInfo);
			}
		}
		else
		{
			uIFirendLeaderBoard_label.Ranking_text.text = CalculateRank(num).ToString();
			uIFirendLeaderBoard_label.Score_text.text = friendLeaderboardData.Score.ToString();
			uIFirendLeaderBoard_label.Status.selectedIndex = 1;
			SetPlayerIcon(uIFirendLeaderBoard_label, playerInfo);
		}
	}

	private void RenderFriendItem(int index, GObject obj)
	{
		if (obj is UIFirendLeaderBoard_label uIFirendLeaderBoard_label && index >= 0 && index < _curFriendScores.Count)
		{
			FriendLeaderboardData friendLeaderboardData = _curFriendScores[index];
			string arg = ((index == 0) ? "[color=#FE4D53]" : "[color=#FFFFFF]");
			uIFirendLeaderBoard_label.Ranking_text.text = $"{arg}{CalculateRank(index)}[/color]";
			uIFirendLeaderBoard_label.Score_text.text = $"{arg}{friendLeaderboardData.Score}[/color]";
			SetFriendIcon(uIFirendLeaderBoard_label, friendLeaderboardData);
			uIFirendLeaderBoard_label.Status.selectedIndex = 1;
		}
	}

	private int CalculateRank(int index)
	{
		if (_curFriendScores == null || _curFriendScores.Count == 0)
		{
			return 0;
		}
		int score = _curFriendScores[index].Score;
		int num = 1;
		for (int i = 0; i < _curFriendScores.Count; i++)
		{
			if (_curFriendScores[i].Score > score)
			{
				num++;
			}
		}
		return num;
	}

	private void SetFriendIcon(UIFirendLeaderBoard_label item, FriendLeaderboardData Data)
	{
		CommonUIManager.RendererLabelInfo((UICom_PlayerLabel)item.com_PlayerLabel, Data.Nick, Data.LV);
		(string, bool) label = Data.Label;
		CommonUIManager.RendererLabel(UIType.Window, 345, (UICom_PlayerLabel)item.com_PlayerLabel, label.Item1, label.Item2);
		CommonUIManager.RendererHeadShot((UICom_PlayerLabel)item.com_PlayerLabel, Data.HeadURL, isVideo: false);
	}

	private void SetPlayerIcon(UIFirendLeaderBoard_label item, Player player)
	{
		ShowingFashion runningFashion = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetRunningFashion();
		(string, bool) playerLabel = runningFashion.labelId.GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
		string fashionAccountHeadShot = runningFashion.headShotId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
		CommonUIManager.RendererLabelInfo((UICom_PlayerLabel)item.com_PlayerLabel, player.Nick, player.Level);
		CommonUIManager.RendererLabel(UIType.Window, 345, (UICom_PlayerLabel)item.com_PlayerLabel, playerLabel.Item1, playerLabel.Item2);
		CommonUIManager.RendererHeadShot((UICom_PlayerLabel)item.com_PlayerLabel, fashionAccountHeadShot, isVideo: false);
	}
}
