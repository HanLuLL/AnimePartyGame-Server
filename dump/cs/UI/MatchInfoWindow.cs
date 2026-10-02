using System;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class MatchInfoWindow : BaseWindow
{
	private MatchData _matchData => SimpleSingletonProvider<GameLogicManager>.inst.match.matchData;

	public MatchInfoWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIMatchInfoWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIMatchInfoWindow uIMatchInfoWindow)
		{
			uIMatchInfoWindow.btn_LeaveMatch.onClick.Add(OnRequestExitMatchTeam);
			uIMatchInfoWindow.btn_OpenMatch.onClick.Add(OnOpenMatchPanel);
			SimpleSingletonProvider<GameLogicManager>.inst.match.signal.TeamChange.AddListener(OnSignalTeamChange);
			SimpleSingletonProvider<GameLogicManager>.inst.match.signal.CancelMatch.AddListener(OnSignalCancelMatch);
			SimpleSingletonProvider<GameLogicManager>.inst.match.signal.StartMatch.AddListener(OnSignalStartMatch);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIMatchInfoWindow uIMatchInfoWindow)
		{
			uIMatchInfoWindow.btn_LeaveMatch.onClick.Remove(OnRequestExitMatchTeam);
			uIMatchInfoWindow.btn_OpenMatch.onClick.Remove(OnOpenMatchPanel);
			SimpleSingletonProvider<GameLogicManager>.inst.match.signal.TeamChange.RemoveListener(OnSignalTeamChange);
			SimpleSingletonProvider<GameLogicManager>.inst.match.signal.CancelMatch.RemoveListener(OnSignalCancelMatch);
			SimpleSingletonProvider<GameLogicManager>.inst.match.signal.StartMatch.RemoveListener(OnSignalStartMatch);
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (base.isShowing && base.contentPane is UIMatchInfoWindow uIMatchInfoWindow)
		{
			uIMatchInfoWindow.btn_LeaveMatch.visible = !(SimpleSingletonProvider<UIManager>.inst.currentPanel is MatchPanel) && _matchData.MatchStatus == MatchTeamInfo.Types.State.Matching;
		}
	}

	private void OnRequestExitMatchTeam()
	{
		if (_matchData == null)
		{
			Hide();
			return;
		}
		GComponent gComponent = base.contentPane;
		UIMatchInfoWindow win = gComponent as UIMatchInfoWindow;
		if (win == null)
		{
			return;
		}
		if (_matchData.MatchStatus == MatchTeamInfo.Types.State.Matching)
		{
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1101, delegate
			{
				RequestExitMatchTeam(delegate
				{
					win.btn_LeaveMatch.onClick.Release();
				});
			}, delegate
			{
				win.btn_LeaveMatch.onClick.Release();
			}).Forget();
		}
		else if (_matchData.MatchStatus == MatchTeamInfo.Types.State.Waiting)
		{
			RequestExitMatchTeam(delegate
			{
				win.btn_LeaveMatch.onClick.Release();
			});
		}
	}

	private void RequestExitMatchTeam(System.Action action)
	{
		long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		SimpleSingletonProvider<GameLogicManager>.inst.match.RequestExitMatchTeamC2S(playerID, _matchData.TeamId).OnFinishedOnly.AddOnce(delegate
		{
			StopMatch();
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is MatchPanel matchPanel)
			{
				matchPanel.ReturnToLastPanel();
			}
			action?.Invoke();
		});
	}

	private void OnOpenMatchPanel()
	{
		if (_matchData == null)
		{
			Hide();
		}
		else if (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is MatchPanel) && base.contentPane is UIMatchInfoWindow uIMatchInfoWindow)
		{
			uIMatchInfoWindow.btn_OpenMatch.onClick.Retain();
			if (SimpleSingletonProvider<UIManager>.inst.NewGameLibrary.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.NewGameLibrary.CloseWin();
			}
			if (SimpleSingletonProvider<UIManager>.inst.setting.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.setting.Hide();
			}
			SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Match).Forget();
			uIMatchInfoWindow.btn_OpenMatch.onClick.Release();
		}
	}

	private void OnSignalTeamChange()
	{
		RefreshMatchInfo();
	}

	private void RefreshMatchInfo()
	{
		if (base.contentPane is UIMatchInfoWindow uIMatchInfoWindow)
		{
			int? num = _matchData.TeamPlayers?.Count;
			uIMatchInfoWindow.txt_PlayerCount.text = $"{num}/4";
		}
	}

	private void OnSignalCancelMatch(MatchTeamInfo.Types.State obj)
	{
		if (obj == MatchTeamInfo.Types.State.Matching)
		{
			StopMatch();
			RefreshMatchInfo();
		}
	}

	private void OnSignalStartMatch()
	{
		StartMatch().Forget();
	}

	public async UniTask OpenMatchInfo()
	{
		await TryShowAsync();
		if (base.contentPane is UIMatchInfoWindow uIMatchInfoWindow)
		{
			RefreshMatchInfo();
			uIMatchInfoWindow.matchStatus.selectedIndex = 0;
			uIMatchInfoWindow.Find2Team.Play();
		}
	}

	public void CloseMatchInfo()
	{
		StopMatch();
		Hide();
	}

	public override bool TryHide()
	{
		MatchData matchData = _matchData;
		if (matchData != null && matchData.MatchStatus == MatchTeamInfo.Types.State.Matching)
		{
			return false;
		}
		return base.TryHide();
	}

	public async UniTask StartMatch()
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UIMatchInfoWindow win = gComponent as UIMatchInfoWindow;
		if (win == null)
		{
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.match.StartTimer(300f, delegate
		{
			MapModeType curMapMode = _matchData.GetCurMapMode();
			MapInfoConfigure mapDataConfigure = _matchData.MatchMapId.GetMapDataConfigure();
			if (mapDataConfigure != null && BattleConfig.IsPVE(curMapMode) && (!(mapDataConfigure.BeginTime != null) || !(mapDataConfigure.EndTime != null) || !TimeHelper.ValidityTime(mapDataConfigure.BeginTime, mapDataConfigure.EndTime)))
			{
				win.btn_LeaveMatch.onClick.Call();
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1098.GetLocal(UIStringType.Message), null, delegate
				{
					if (_matchData.MatchStatus == MatchTeamInfo.Types.State.Matching)
					{
						RequestExitMatchTeam(delegate
						{
							win.btn_LeaveMatch.onClick.Release();
						});
					}
				}).Forget();
			}
		}, delegate(float t)
		{
			string text = TimeSpan.FromSeconds(t).ToString("mm\\:ss");
			win.txt_Time.text = text;
		}).Forget();
		win.matchStatus.selectedIndex = 1;
		win.Team2Find.Play();
	}

	public void StopMatch()
	{
		if (base.isShowing && base.contentPane is UIMatchInfoWindow uIMatchInfoWindow && uIMatchInfoWindow.matchStatus.selectedIndex != 0)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.match.StopTimer();
			uIMatchInfoWindow.matchStatus.selectedIndex = 0;
			uIMatchInfoWindow.Find2Team.Play();
		}
	}
}
