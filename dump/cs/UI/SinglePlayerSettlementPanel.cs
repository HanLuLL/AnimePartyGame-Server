using System.Collections.Generic;
using Core;
using GameLogic;
using SinglePlayer;
using SinglePlayer.GamePlay;
using Tools;
using party.model;

namespace UI;

public class SinglePlayerSettlementPanel : BasePanel<UISinglePlayerSettlementPanel>
{
	private int _curScore;

	private int _curRank;

	private int _historyScoreBase;

	private int _historyRankBase;

	private Model_Settlement_Info settlementInfoModel;

	public SinglePlayerSettlementPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UISinglePlayerSettlementPanel.CreateInstance();
		base.Create();
		settlementInfoModel = new Model_Settlement_Info(base.ui.SingPlayerSettlement_com_end.com_info);
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	private void ShowEnd(GameStatus status)
	{
		base.ui.SingPlayerSettlement_com_end.type.selectedIndex = 0;
		bool flag = status == GameStatus.Victory;
		bool flag2 = !Game.GetModel<GameData>().IsFinalLevel();
		flag2 = flag2 && flag;
		base.ui.SingPlayerSettlement_com_end.Next.selectedIndex = (flag2 ? 1 : 0);
	}

	public override void Refresh()
	{
		base.Refresh();
		settlementInfoModel.Refresh();
	}

	private void RefreshUI()
	{
		base.ui.language.selectedIndex = ((GameSettings.languageType != LanguageType.SimplifiedChinese) ? 1 : 0);
		if (base.ui.SingPlayerSettlement_com_end != null)
		{
			base.ui.SingPlayerSettlement_com_end.language.selectedIndex = base.ui.language.selectedIndex;
		}
		RefreshScoreUI();
		RefreshRankUI();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.SingPlayerSettlement_com_end.loadbg.FullScreen();
	}

	public override void Close()
	{
		base.Close();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		UISettlement_com_end singPlayerSettlement_com_end = base.ui.SingPlayerSettlement_com_end;
		if (singPlayerSettlement_com_end != null)
		{
			singPlayerSettlement_com_end.Info_Btn.onClick.Add(OnBtnInfoClick);
			singPlayerSettlement_com_end.Friendrank_Btn.onClick.Add(OnFriendRank);
			singPlayerSettlement_com_end.Again_Btn.onClick.Add(OnAgain);
			singPlayerSettlement_com_end.Next_Btn.onClick.Add(OnNext);
		}
		base.ui.Exit_Btn.onClick.Add(OnExit);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		UISettlement_com_end singPlayerSettlement_com_end = base.ui.SingPlayerSettlement_com_end;
		if (singPlayerSettlement_com_end != null)
		{
			singPlayerSettlement_com_end.Info_Btn.onClick.Remove(OnBtnInfoClick);
			singPlayerSettlement_com_end.Friendrank_Btn.onClick.Remove(OnFriendRank);
			singPlayerSettlement_com_end.Again_Btn.onClick.Remove(OnAgain);
			singPlayerSettlement_com_end.Next_Btn.onClick.Remove(OnNext);
		}
		base.ui.Exit_Btn.onClick.Remove(OnExit);
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
	}

	private void RefreshScoreUI()
	{
		UISettlement_com_end singPlayerSettlement_com_end = base.ui.SingPlayerSettlement_com_end;
		if (singPlayerSettlement_com_end != null)
		{
			singPlayerSettlement_com_end.Score_Txt.text = ((_curScore == -1) ? "--" : $"{_curScore}");
			singPlayerSettlement_com_end.HistoricalScore_text.text = ((_historyScoreBase > 0) ? $"{_historyScoreBase}" : "000");
			singPlayerSettlement_com_end.NewRecord.selectedIndex = ((_curScore > _historyScoreBase) ? 1 : 0);
		}
	}

	private void RefreshRankUI()
	{
		UISettlement_com_end singPlayerSettlement_com_end = base.ui.SingPlayerSettlement_com_end;
		if (singPlayerSettlement_com_end == null)
		{
			return;
		}
		string text = "--";
		if (_curScore > _historyScoreBase && (_historyRankBase > 0 || _curRank > 0))
		{
			if (_curRank > 0 && _historyRankBase > 0 && _curRank < _historyRankBase)
			{
				text = $"{_curRank}";
			}
			else if (_historyRankBase > 0)
			{
				text = $"{_historyRankBase}";
			}
			else if (_curRank > 0)
			{
				text = $"{_curRank}";
			}
		}
		singPlayerSettlement_com_end.Rank_text.text = text;
		singPlayerSettlement_com_end.HistoricalRank_text.text = ((_historyRankBase > 0) ? $"{_historyRankBase}" : "000");
		singPlayerSettlement_com_end.NewRanking.selectedIndex = ((_curScore > _historyScoreBase && _curRank > 0 && (_curRank < _historyRankBase || _historyRankBase == 0)) ? 1 : 0);
	}

	private void OnExit()
	{
		base.ui.Exit_Btn.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.ResetSinglePlayer();
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.UploadLogData(SinglePlayerLogType.Start);
		base.ui.Exit_Btn.onClick.Release();
	}

	private void OnBtnInfoClick()
	{
		base.ui.SingPlayerSettlement_com_end.type.selectedIndex = 1 - base.ui.SingPlayerSettlement_com_end.type.selectedIndex;
	}

	private void OnFriendRank()
	{
		base.ui.SingPlayerSettlement_com_end.Friendrank_Btn.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.FriendLeaderboard.Show();
		base.ui.SingPlayerSettlement_com_end.Friendrank_Btn.onClick.Release();
	}

	private void OnAgain()
	{
		base.ui.SingPlayerSettlement_com_end.Again_Btn.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.PlayerSelectInfoOnGameRestart.SetRestartGameInfo(isNextLevel: false);
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.ResetSinglePlayer();
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.UploadLogData(SinglePlayerLogType.Start);
		base.ui.SingPlayerSettlement_com_end.Again_Btn.onClick.Release();
	}

	private void OnNext()
	{
		base.ui.SingPlayerSettlement_com_end.Again_Btn.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.PlayerSelectInfoOnGameRestart.SetRestartGameInfo(isNextLevel: true);
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.ResetSinglePlayer();
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.UploadLogData(SinglePlayerLogType.Start);
		base.ui.SingPlayerSettlement_com_end.Again_Btn.onClick.Release();
	}

	public void ShowResult(GameStatus status)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer?.UploadLogData(status);
		base.ui.status.selectedIndex = ((status != GameStatus.Victory) ? 1 : 0);
		ShowEnd(status);
		if (base.ui.status.selectedIndex == 0)
		{
			base.ui.Win_Cut_in.Play();
		}
		else
		{
			base.ui.Lose_Cut_in.Play();
		}
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		SinglePlayerLogic singlePlayer = SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer;
		_historyScoreBase = singlePlayer.HistoryScoreBase;
		_historyRankBase = singlePlayer.HistoryRankBase;
		int curScore = Game.GetModel<GameData>().heroProperty.CalculateScore();
		_curScore = curScore;
		List<FriendLeaderboardData> friendScoresList = SimpleSingletonProvider<GameLogicManager>.inst.friend.friendScoresList;
		_curRank = CalcRankByScore(friendScoresList, _curScore, playerInfo.Id);
		RefreshUI();
	}

	private int CalcRankByScore(List<FriendLeaderboardData> sortedList, int score, long playerId)
	{
		if (sortedList == null || sortedList.Count == 0)
		{
			return 1;
		}
		int num = 1;
		for (int i = 0; i < sortedList.Count; i++)
		{
			if (sortedList[i].playerId != playerId && sortedList[i].Score > score)
			{
				num++;
			}
		}
		return num;
	}
}
