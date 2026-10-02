using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class MatchLogic : IRPCSync
{
	public readonly MatchData matchData = new MatchData();

	public readonly MatchSignal signal = new MatchSignal();

	private CtsInfo _cts;

	private readonly Dictionary<long, MatchTeamInviteInfo> _matchTeamInviteInfos = new Dictionary<long, MatchTeamInviteInfo>();

	public void InitFromServer(long teamId)
	{
		matchData.TeamId = teamId;
	}

	public bool IsVailDifficulty(int difficulty)
	{
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		int unLockDifficulty = playerInfo.UnLockDifficulty;
		if (playerInfo != null && playerInfo.Level < StaticGlobalData.ROOM_PVELOCK_LEVEL && difficulty >= StaticGlobalData.ROOM_PVELOCK_DIFFICULTY && difficulty > unLockDifficulty)
		{
			return false;
		}
		return true;
	}

	public void CheckRechargeActionForMatch()
	{
		MatchData matchData = this.matchData;
		if (matchData != null && matchData.InTeam)
		{
			long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
			RequestExitMatchTeamC2S(playerID, this.matchData.TeamId).OnFinishedOnly.AddOnce(delegate
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1100.GetLocal(UIStringType.Message));
			});
		}
	}

	public bool CheckOperateForMatch()
	{
		MatchData matchData = this.matchData;
		if (matchData != null && matchData.InTeam)
		{
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1101, delegate
			{
				long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
				RequestExitMatchTeamC2S(playerID, this.matchData.TeamId).OnFinishedOnly.AddOnce(delegate
				{
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1100.GetLocal(UIStringType.Message));
				});
			}).Forget();
			return false;
		}
		return true;
	}

	public async UniTask StartTimer(float intervalSeconds, System.Action onTick, Action<float> onUpdate)
	{
		StopTimer();
		_cts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		await Run(intervalSeconds, onTick, onUpdate, _cts.Token);
	}

	public void StopTimer()
	{
		SimpleSingletonProvider<DelaySignalManager>.inst.CancelTask(_cts);
		_cts = null;
	}

	private async UniTask Run(float intervalSeconds, System.Action onInterval, Action<float> onUpdate, CancellationToken token)
	{
		float lastTime = Time.realtimeSinceStartup;
		float nextTrigger = intervalSeconds;
		float _accumulatedSeconds = 0f;
		while (!token.IsCancellationRequested)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num = realtimeSinceStartup - lastTime;
			lastTime = realtimeSinceStartup;
			_accumulatedSeconds += num;
			onUpdate?.Invoke(_accumulatedSeconds);
			if (_accumulatedSeconds >= nextTrigger)
			{
				onInterval?.Invoke();
				nextTrigger += intervalSeconds;
			}
			await UniTask.Yield(PlayerLoopTiming.Update, token).SuppressCancellationThrow();
		}
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.StartMatchS2C.OnStartMatchS2CServerCallBackAsync = OnStartMatchS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.CancelMatchS2C.OnCancelMatchS2CServerCallBackAsync = OnCancelMatchS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchSuccessS2C.OnMatchSuccessS2CServerCallBackAsync = OnMatchSuccessS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.CreateMatchTeamS2C.OnCreateMatchTeamS2CServerCallBackAsync = OnCreateMatchTeamS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeMatchTeamS2C.OnChangeMatchTeamS2CServerCallBackAsync = OnChangeMatchTeamS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.JoinMatchTeamS2C.OnJoinMatchTeamS2CServerCallBackAsync = OnJoinMatchTeamS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.ExitMatchTeamS2C.OnExitMatchTeamS2CServerCallBackAsync = OnExitMatchTeamS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.RefreshMatchTeamInfoS2C.OnRefreshMatchTeamInfoS2CServerCallBackAsync = OnRefreshMatchTeamInfoS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamInviteS2C.OnMatchTeamInviteS2CServerCallBackAsync = OnMatchTeamInviteS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamInviteNotify.OnMatchTeamInviteNotifyServerCallBackAsync = OnMatchTeamInviteNotifyServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.RefreshMatchTeamStateNotify.OnRefreshMatchTeamStateNotifyServerCallBackAsync = OnRefreshMatchTeamStateNotifyServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamReadyS2C.OnMatchTeamReadyS2CServerCallBackAsync = OnMatchTeamReadyS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamChatS2C.OnMatchTeamChatS2CServerCallBackAsync = OnMatchTeamChatS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchPunishmentS2C.OnMatchPunishmentS2CServerCallBackAsync = OnMatchPunishmentS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.StartMatchS2C.OnStartMatchS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.CancelMatchS2C.OnCancelMatchS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchSuccessS2C.OnMatchSuccessS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.CreateMatchTeamS2C.OnCreateMatchTeamS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeMatchTeamS2C.OnChangeMatchTeamS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.JoinMatchTeamS2C.OnJoinMatchTeamS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ExitMatchTeamS2C.OnExitMatchTeamS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RefreshMatchTeamInfoS2C.OnRefreshMatchTeamInfoS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamInviteS2C.OnMatchTeamInviteS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RefreshMatchTeamStateNotify.OnRefreshMatchTeamStateNotifyServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamReadyS2C.OnMatchTeamReadyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamChatS2C.OnMatchTeamChatS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchPunishmentS2C.OnMatchPunishmentS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamInviteNotify.OnMatchTeamInviteNotifyServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestStartMatchC2S(long teamId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.StartMatchC2S.StartMatchC2SCall(new StartMatchC2S
		{
			TeamId = teamId
		});
	}

	private async UniTask OnStartMatchS2CServerCallBack(StartMatchS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestCancelMatchC2S(long teamId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.CancelMatchC2S.CancelMatchC2SCall(new CancelMatchC2S
		{
			TeamId = teamId
		});
	}

	private async UniTask OnCancelMatchS2CServerCallBack(CancelMatchS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnMatchSuccessS2CServerCallBack(MatchSuccessS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.CreateRoom(model.Room);
			SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.SwitchRoomState(model.Room, RoomStateType.CHOICE);
			signal.CancelMatch.Dispatch(MatchTeamInfo.Types.State.Waiting);
			await SimpleSingletonProvider<UIManager>.inst.matchSuccess.ShowMatchResult();
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
			{
				SimpleSingletonProvider<UIManager>.inst.TryHideWindows();
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.RoomHero);
			}
		}
	}

	public RPCAsyncResult RequestCreateMatchTeamC2S(MapModeType mapMode)
	{
		(int, int) defaultMapInfo = matchData.GetDefaultMapInfo((int)mapMode);
		int item = defaultMapInfo.Item1;
		int item2 = defaultMapInfo.Item2;
		MatchMode matchMode = matchData.GetMatchMode(mapMode);
		return MonoSingletonProvider<NetManager>.inst.RPC.CreateMatchTeamC2S.CreateMatchTeamC2SCall(new CreateMatchTeamC2S
		{
			Mode = matchMode,
			MapId = item,
			Difficulty = item2
		});
	}

	private async UniTask OnCreateMatchTeamS2CServerCallBackAsync(CreateMatchTeamS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			matchData.UpdateMatchInfo(model.Team);
			await SimpleSingletonProvider<UIManager>.inst.MatchInfo.OpenMatchInfo();
		}
	}

	public RPCAsyncResult RequestChangeMatchTeamC2S(MapModeType mapMode, int mapId, int difficulty, long teamId)
	{
		MatchMode matchMode = matchData.GetMatchMode(mapMode);
		return MonoSingletonProvider<NetManager>.inst.RPC.ChangeMatchTeamC2S.ChangeMatchTeamC2SCall(new ChangeMatchTeamC2S
		{
			Mode = matchMode,
			MapId = mapId,
			Difficulty = difficulty,
			TeamId = teamId
		});
	}

	private async UniTask OnChangeMatchTeamS2CServerCallBackAsync(ChangeMatchTeamS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model?.Team != null)
		{
			matchData.UpdateMatchInfo(model.Team);
			signal.TeamChange.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestJoinMatchTeamC2S(long senderInviter, long teamId)
	{
		_matchTeamInviteInfos.Remove(senderInviter);
		return MonoSingletonProvider<NetManager>.inst.RPC.JoinMatchTeamC2S.JoinMatchTeamC2SCall(new JoinMatchTeamC2S
		{
			TeamId = teamId
		});
	}

	private async UniTask OnJoinMatchTeamS2CServerCallBackAsync(JoinMatchTeamS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			return;
		}
		if (!matchData.InTeam)
		{
			matchData.UpdateMatchInfo(model.Team);
			MapModeType curMapMode = matchData.GetCurMapMode();
			matchData.UpdateMapInfo((int)curMapMode);
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Home)
			{
				return;
			}
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Match);
			await SimpleSingletonProvider<UIManager>.inst.MatchInfo.OpenMatchInfo();
		}
		else
		{
			matchData.UpdateMatchInfo(model.Team);
			signal.TeamChange.Dispatch();
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestExitMatchTeamC2S(long playerId, long teamId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ExitMatchTeamC2S.ExitMatchTeamC2SCall(new ExitMatchTeamC2S
		{
			PlayerId = playerId,
			TeamId = teamId
		});
	}

	private async UniTask OnExitMatchTeamS2CServerCallBackAsync(ExitMatchTeamS2C model, int errId, bool isDispatch)
	{
		SimpleSingletonProvider<UIManager>.inst.MatchInfo.CloseMatchInfo();
		if (errId != 0)
		{
			matchData.Dispose();
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.ExitPlayerId))
		{
			matchData.Dispose();
			return;
		}
		matchData.RemoveTeamPlayer(model.ExitPlayerId);
		signal.TeamChange.Dispatch();
		await UniTask.CompletedTask;
	}

	public void RequestRefreshMatchTeamInfoC2S()
	{
		if (matchData != null && matchData.TeamId != 0L)
		{
			MonoSingletonProvider<NetManager>.inst.RPC.RefreshMatchTeamInfoC2S.RefreshMatchTeamInfoC2SCall(new RefreshMatchTeamInfoC2S
			{
				TeamId = matchData.TeamId
			});
		}
	}

	public async UniTask OnRefreshMatchTeamInfoS2CServerCallBackAsync(RefreshMatchTeamInfoS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || model.Team == null || model.Team.Id == 0L)
		{
			matchData.Dispose();
			SimpleSingletonProvider<UIManager>.inst.MatchInfo.CloseMatchInfo();
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is MatchPanel matchPanel)
			{
				matchPanel.ReturnToLastPanel();
			}
			return;
		}
		MatchTeamInfo.Types.State preMatchStatus = matchData.MatchStatus;
		matchData.UpdateMatchInfo(model.Team);
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Home || SimpleSingletonProvider<GameLogicManager>.inst.connectRoomId != 0L)
		{
			return;
		}
		if (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is MatchPanel))
		{
			matchData.UpdateMapInfo((int)matchData.GetCurMapMode());
			if (matchData.MatchStatus != MatchTeamInfo.Types.State.Playing)
			{
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Match);
				if (!SimpleSingletonProvider<UIManager>.inst.MatchInfo.isShowing)
				{
					await SimpleSingletonProvider<UIManager>.inst.MatchInfo.OpenMatchInfo();
				}
			}
		}
		if (matchData.MatchStatus == MatchTeamInfo.Types.State.Waiting)
		{
			signal.CancelMatch.Dispatch(preMatchStatus);
		}
		else if (matchData.MatchStatus == MatchTeamInfo.Types.State.Matching)
		{
			signal.StartMatch.Dispatch();
		}
		signal.TeamChange.Dispatch();
	}

	private async UniTask OnRefreshMatchTeamStateNotifyServerCallBackAsync(RefreshMatchTeamStateNotify model, int errId, bool isDispatch)
	{
		if (errId != 0 || model.TeamId == 0L)
		{
			RequestRefreshMatchTeamInfoC2S();
			return;
		}
		MatchTeamInfo.Types.State matchStatus = matchData.MatchStatus;
		matchData.TeamId = model.TeamId;
		matchData.MatchStatus = model.State;
		if (matchData.MatchStatus == MatchTeamInfo.Types.State.Waiting)
		{
			if (matchStatus == MatchTeamInfo.Types.State.Playing)
			{
				matchData.ClearReadyStatus();
			}
			signal.CancelMatch.Dispatch(matchStatus);
		}
		else if (matchData.MatchStatus == MatchTeamInfo.Types.State.Matching)
		{
			signal.StartMatch.Dispatch();
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestMatchTeamInviteC2S(List<long> inviteIds)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamInviteC2S.MatchTeamInviteC2SCall(new MatchTeamInviteC2S
		{
			PlayerIds = { (IEnumerable<long>)inviteIds }
		});
	}

	private async UniTask OnMatchTeamInviteS2CServerCallBackAsync(MatchTeamInviteS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnMatchTeamInviteNotifyServerCallBackAsync(MatchTeamInviteNotify model, int errId, bool isDispatch)
	{
		if (errId == 0 && !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
		{
			_matchTeamInviteInfos[model.PlayerId] = new MatchTeamInviteInfo(model);
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Home)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowInviteSignal(AstralInviteType.TeamInvite);
			}
			await UniTask.CompletedTask;
		}
	}

	public void ClearMatchTeamInvite()
	{
		_matchTeamInviteInfos.Clear();
	}

	public List<MatchTeamInviteInfo> GetMatchTeamInvite()
	{
		List<MatchTeamInviteInfo> list = _matchTeamInviteInfos.Values.ToList();
		list.Sort((MatchTeamInviteInfo x, MatchTeamInviteInfo y) => x.Time.CompareTo(y.Time));
		return list;
	}

	public RPCAsyncResult RequestMatchTeamReadyC2S(long teamId, long playerId, bool ready)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamReadyC2S.MatchTeamReadyC2SCall(new MatchTeamReadyC2S
		{
			TeamId = teamId,
			PlayerId = playerId,
			IsReady = ready
		});
	}

	private async UniTask OnMatchTeamReadyS2CServerCallBackAsync(MatchTeamReadyS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || model.TeamId != matchData.TeamId)
		{
			RequestRefreshMatchTeamInfoC2S();
			return;
		}
		matchData.UpdatePlayerReady(model.PlayerId, model.IsReady);
		signal.UpdateReady.Dispatch(model.PlayerId);
		await UniTask.CompletedTask;
	}

	private async UniTask OnMatchPunishmentS2CServerCallBackAsync(MatchPunishmentS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.CreditWarning.ShowCreditWarning(model).Forget();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestMatchTeamChatC2S(long teamId, int chatIndex)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.MatchTeamChatC2S.MatchTeamChatC2SCall(new MatchTeamChatC2S
		{
			TeamId = teamId,
			Index = chatIndex
		});
	}

	private async UniTask OnMatchTeamChatS2CServerCallBackAsync(MatchTeamChatS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Home && !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
			{
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.shortChat.Dispatch(model.PlayerId, model.Index);
			}
			await UniTask.CompletedTask;
		}
	}
}
