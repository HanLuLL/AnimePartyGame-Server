using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using SinglePlayer;
using SinglePlayer.GamePlay;
using SinglePlayer.Scene;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class SinglePlayerStartPanel : BasePanel<UISinglePlayerStartPanel>
{
	private const int REFRESH_CD_SECONDS = 60;

	private float _lastRequestFriendScoreTimeSeconds;

	private int currentLevelId = -1;

	private List<EventCallback0> levelClilckEevnts = new List<EventCallback0>();

	private List<UISinglePlayer_Button_Level> levelButtons = new List<UISinglePlayer_Button_Level>();

	private List<int> levels = new List<int>();

	private Model_SinglePlayer_SelectTag selectTagModel;

	private Model_SinglePlayer_Book model_book;

	private const int defaultChapterId = 10000;

	public SinglePlayerStartPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UISinglePlayerStartPanel.CreateInstance();
		base.Create();
		selectTagModel = new Model_SinglePlayer_SelectTag(base.ui.com_selectTag);
		model_book = new Model_SinglePlayer_Book(base.ui.com_book);
	}

	protected override void InitData(params object[] objs)
	{
		levels.Clear();
		if (StaticConfigure.SinglePlayer.ChapterDict.TryGetValue(10000, out var value))
		{
			levels.AddRange(value.Levels);
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		if (SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.IsReStartGame)
		{
			SwitchToSeletLevel();
			SinglePlayerLevelSelectInfo playerSelectInfoOnGameRestart = SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.PlayerSelectInfoOnGameRestart;
			if (playerSelectInfoOnGameRestart.IsLoadOnGameStart)
			{
				playerSelectInfoOnGameRestart.IsLoadOnGameStart = false;
				Game.GetModel<GameData>().SetSelectedCardPacks(playerSelectInfoOnGameRestart.SelectedCardPacks);
				Game.GetModel<GameData>().SetLeveId(playerSelectInfoOnGameRestart.LevelId);
				Game.GetModel<GameData>().UpdateLevelConfigBeforeGameStart();
				Game.GetController<SinglePlayerSceneController>().StartGame().Forget();
				SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.UploadLogData(SinglePlayerLogType.Start);
			}
		}
		else
		{
			SwitchToStartGame();
		}
	}

	protected override void InitComponents()
	{
		base.InitComponents();
	}

	public override void Refresh()
	{
		base.Refresh();
		SingleGameData serverGameData = SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.ServerGameData;
		if (serverGameData == null)
		{
			base.ui.state.selectedIndex = 0;
		}
		else
		{
			base.ui.state.selectedIndex = ((serverGameData.LevelId != 0) ? 1 : 0);
		}
		RefreshLevelButtons();
		if (StaticConfigure.SinglePlayer.ChapterDict.TryGetValue(10000, out var value))
		{
			base.ui.btn_title.text = value.NameID.GetLocal(UIStringType.SinglePlayer);
		}
	}

	private bool IsBtnUnlock(int btnIdx)
	{
		int maxLevelIdPassed = Game.GetModel<GameData>().MaxLevelIdPassed;
		if (levels.GetSafeByIndex(btnIdx) > maxLevelIdPassed + 1)
		{
			return btnIdx == 0;
		}
		return true;
	}

	private void RefreshLevelButtons()
	{
		for (int i = 0; i < levelButtons.Count; i++)
		{
			UISinglePlayer_Button_Level uISinglePlayer_Button_Level = levelButtons[i];
			bool flag = IsBtnUnlock(i);
			uISinglePlayer_Button_Level.type.selectedIndex = (flag ? 1 : 0);
		}
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Start.onClick.Add(OnStartClick);
		base.ui.btn_Continue.onClick.Add(OnContinueClick);
		base.ui.btn_Reset.onClick.Add(OnResetClick);
		base.ui.btn_FriendRankList.onClick.Add(OnFriendRankListClick);
		base.ui.btn_Back.onClick.Add(OnBackClick);
		base.ui.btn_book.onClick.Add(OnBookClick);
		levelButtons.Clear();
		levelButtons.Add(base.ui.btn_level_1);
		levelButtons.Add(base.ui.btn_level_2);
		levelButtons.Add(base.ui.btn_level_3);
		int count = levelButtons.Count;
		levelClilckEevnts.Clear();
		levelClilckEevnts.AddRange(new EventCallback0[count]);
		for (int i = 0; i < count; i++)
		{
			int btnIdx = i;
			UISinglePlayer_Button_Level uISinglePlayer_Button_Level = levelButtons[i];
			if (uISinglePlayer_Button_Level != null)
			{
				levelClilckEevnts[i] = delegate
				{
					OnLevelBtnClick(btnIdx);
				};
				uISinglePlayer_Button_Level.onClick.Add(levelClilckEevnts[i]);
			}
		}
		base.ui.btn_pack.onClick.Add(OnSelectPackBtnClick);
		base.ui.btn_exit.onClick.Add(OnExitClick);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Start.onClick.Remove(OnStartClick);
		base.ui.btn_Continue.onClick.Remove(OnContinueClick);
		base.ui.btn_Reset.onClick.Remove(OnResetClick);
		base.ui.btn_FriendRankList.onClick.Remove(OnFriendRankListClick);
		base.ui.btn_Back.onClick.Remove(OnBackClick);
		base.ui.btn_book.onClick.Remove(OnBookClick);
		int count = levelButtons.Count;
		for (int i = 0; i < count; i++)
		{
			levelButtons[i]?.onClick.Remove(levelClilckEevnts[i]);
		}
		base.ui.btn_pack.onClick.Remove(OnSelectPackBtnClick);
		base.ui.btn_exit.onClick.Remove(OnExitClick);
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void OnStartClick()
	{
		SwitchToSeletLevel();
	}

	private void OnBookClick()
	{
		model_book.Show();
	}

	private void OnExitClick()
	{
		SwitchToStartGame();
	}

	private void SwitchToStartGame()
	{
		base.ui.type.selectedIndex = 0;
	}

	private void SwitchToSeletLevel()
	{
		base.ui.type.selectedIndex = 1;
	}

	private void OnLevelBtnClick(int btnIdx)
	{
		if (IsBtnUnlock(btnIdx))
		{
			int safeByIndex = levels.GetSafeByIndex(btnIdx);
			currentLevelId = safeByIndex;
			StartGame();
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(11028);
		}
	}

	private void StartGame()
	{
		Game.GetModel<GameData>().SetLeveId(currentLevelId);
		Game.GetModel<GameData>().UpdateLevelConfigBeforeGameStart();
		Game.GetController<SinglePlayerSceneController>().StartGame().Forget();
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.UploadLogData(SinglePlayerLogType.Start);
	}

	private void OnSelectPackBtnClick()
	{
		selectTagModel.Show();
	}

	private void OnContinueClick()
	{
		Game.GetController<SinglePlayerSceneController>().StartGame().Forget();
	}

	private void OnResetClick()
	{
		string local = 1040005.GetLocal(UIStringType.GUI);
		base.ui.btn_Reset.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(local, delegate
		{
			SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.ResetSinglePlayer();
			SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.UploadLogData(SinglePlayerLogType.Restart);
			base.ui.btn_Reset.onClick.Release();
		}, delegate
		{
			base.ui.btn_Reset.onClick.Release();
		}).Forget();
	}

	private void OnFriendRankListClick()
	{
		base.ui.btn_FriendRankList.onClick.Retain();
		if (_lastRequestFriendScoreTimeSeconds == 0f || Time.time - _lastRequestFriendScoreTimeSeconds >= 60f)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendListC2S().OnFinished.AddOnce(delegate(RPCAsyncResult _)
			{
				if (_.errId == 0)
				{
					SimpleSingletonProvider<UIManager>.inst.FriendLeaderboard.Show();
					base.ui.BtnGroups.visible = false;
					base.ui.btn_FriendRankList.onClick.Release();
				}
				else
				{
					base.ui.btn_FriendRankList.onClick.Release();
				}
			});
			_lastRequestFriendScoreTimeSeconds = Time.time;
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.FriendLeaderboard.Show();
			base.ui.BtnGroups.visible = false;
			base.ui.btn_FriendRankList.onClick.Release();
		}
		SimpleSingletonProvider<UIManager>.inst.FriendLeaderboard.onClose.AddOnce(delegate
		{
			base.ui.BtnGroups.visible = true;
			base.ui.Cut_in.Play();
		});
	}

	private void OnBackClick()
	{
		base.ui.btn_Reset.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.ExitSinglePlayer();
		base.ui.btn_Reset.onClick.Release();
	}
}
