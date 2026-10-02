using System;
using Core;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using UnityTimer;
using party.protocol;

namespace GameLogic;

public class OnlineLogic : IRPCSync
{
	private Timer MarqueeTimer;

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.OnlineSyncRoomIdS2C.OnOnlineSyncRoomIdS2CServerCallBackAsync = OnOnlineSyncRoomIdS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.NoticeS2C.OnNoticeS2CServerCallBackAsync = OnNoticeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.LoopNoticeS2C.OnLoopNoticeS2CServerCallBackAsync = OnLoopNoticeS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.TimeOutKickPlayerS2C.OnTimeOutKickPlayerS2CServerCallBackAsync = OnTimeOutKickPlayerS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.OnlineSyncRoomIdS2C.OnOnlineSyncRoomIdS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.NoticeS2C.OnNoticeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.LoopNoticeS2C.OnLoopNoticeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.TimeOutKickPlayerS2C.OnTimeOutKickPlayerS2CServerCallBackAsync = null;
	}

	private async UniTask OnTimeOutKickPlayerS2CServerCallBackAsync(TimeOutKickPlayerS2C model, int errId, bool isDispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
		{
			return;
		}
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (roomController != null && roomController.localRoom != null && roomController.roomStateType == RoomStateType.RUNNING)
		{
			if (roomController.localRoom.Id != model.RoomId)
			{
				Debug.LogError($"When timeOut, server roomId:{model.RoomId}, but local roomId:{roomController.localRoom.Id}");
			}
			else if (model.IsExit)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.room.GiveUpGame(null, ExitRoomC2S.Types.ForceExitType.TimeOut);
				Debug.LogError("When timeOut, server notify current client giveUpGame");
			}
			else
			{
				MonoSingletonProvider<NetManager>.inst.CloseServerByKick();
				await SimpleSingletonProvider<GameManager>.inst.ReLogin();
				Debug.LogError("When timeOut, server kick current client");
			}
		}
	}

	private async UniTask OnOnlineSyncRoomIdS2CServerCallBack(OnlineSyncRoomIdS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Home)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.SetConnectRoomId(model.RoomId);
			SimpleSingletonProvider<GameLogicManager>.inst.account.SetOnline();
			IBasePanel currentPanel = SimpleSingletonProvider<UIManager>.inst.currentPanel;
			if (currentPanel == null || !currentPanel.IsOpen())
			{
				return;
			}
			if (currentPanel.config.PanelType == UIPanelType.Home)
			{
				if (currentPanel is HomePanel homePanel)
				{
					homePanel.RefreshSystemStatus();
				}
			}
			else if (currentPanel.config.PanelType == UIPanelType.RoomHero)
			{
				RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
				if (roomInfo != null && roomInfo.MapType == 10)
				{
					return;
				}
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(5, delegate
				{
					ReturnHome(model);
				}).Forget();
			}
			else if (currentPanel.config.PanelType == UIPanelType.RoomWait)
			{
				if (model.RoomId == 0L)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
					await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
				}
				else
				{
					SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType = RoomStateType.NONE;
					SimpleSingletonProvider<GameLogicManager>.inst.room.RequestSyncRoomC2S(model.RoomId);
				}
			}
			else if (currentPanel.config.PanelType == UIPanelType.Store)
			{
				if (currentPanel is StorePanel storePanel)
				{
					storePanel.RefreshCurShelf();
				}
			}
			else
			{
				currentPanel.Refresh();
			}
			TryRefreshMatchInfo();
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel.config.NeedBottomMenu && SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel != null && !SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel.IsOpen())
			{
				SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel.Show();
				SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel.Refresh();
			}
		}
		else
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
			{
				return;
			}
			if (model.RoomId != 0L)
			{
				IBasePanel currentPanel2 = SimpleSingletonProvider<UIManager>.inst.currentPanel;
				if (currentPanel2 == null || currentPanel2.config.PanelType != UIPanelType.BattleSettlement)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType = RoomStateType.NONE;
					SimpleSingletonProvider<GameLogicManager>.inst.room.RequestSyncRoomC2S(model.RoomId);
				}
				return;
			}
			IBasePanel currentPanel3 = SimpleSingletonProvider<UIManager>.inst.currentPanel;
			if (currentPanel3 != null && currentPanel3.config.PanelType == UIPanelType.BattleSettlement)
			{
				return;
			}
			RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController?.localRoom;
			if (roomInfo == null || roomInfo.MapType != 10)
			{
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(5, delegate
				{
					MonoSingletonProvider<NetManager>.inst.RPC.ClearRPC();
					SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
					SimpleSingletonProvider<GameLogicManager>.inst.battleResult.FinishGame();
				}).Forget();
			}
		}
	}

	private async void ReturnHome(OnlineSyncRoomIdS2C model)
	{
		await SimpleSingletonProvider<InternalAssetManager>.inst.Dispose();
		if (model.RoomId == 0L)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType = RoomStateType.NONE;
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestSyncRoomC2S(model.RoomId);
	}

	private async UniTask OnLoopNoticeS2CServerCallBackAsync(LoopNoticeS2C model, int errid, bool isdispatch)
	{
		DateTime startDateTime = (model.StartTime * 1000).StampMillisecondsToDateTime();
		DateTime endDateTime = (model.EndTime * 1000).StampMillisecondsToDateTime();
		Timer marqueeTimer = MarqueeTimer;
		if (marqueeTimer != null)
		{
			marqueeTimer.Cancel();
		}
		TimeSpan timeSpan = endDateTime - MonoSingletonProvider<NetManager>.inst.ServerTime;
		if (timeSpan.TotalSeconds <= 0.0)
		{
			return;
		}
		string MarqueeContent = null;
		try
		{
			MarqueeContent = GetLocal(JsonUtility.FromJson<InfoByServer>(model.Context));
		}
		catch (Exception ex)
		{
			MarqueeContent = model.Context;
			Debug.LogError("跑马灯文本格式错误，无法解析" + ex);
		}
		if (string.IsNullOrEmpty(MarqueeContent))
		{
			return;
		}
		float _interval = Mathf.Max((float)(MarqueeContent.Length + 2) * 0.25f, model.Interval);
		float preShowMarqueeTime = 0f - _interval;
		MarqueeTimer = Timer.Register(0f, (float)timeSpan.TotalSeconds, (Action)null, (Action)null, (Action)delegate
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.HideMarquee();
		}, (Action)null, (Action)null, (Action<float>)delegate(float _)
		{
			if (TimeHelper.ValidityTime(startDateTime, endDateTime) && _ - preShowMarqueeTime > _interval)
			{
				preShowMarqueeTime = _;
				SimpleSingletonProvider<UIManager>.inst.systemTips.showMarquee(MarqueeContent);
			}
		}, (Action)null, false, -1f, false, (GameObject)null);
		await UniTask.CompletedTask;
	}

	private async UniTask OnNoticeS2CServerCallBack(NoticeS2C model, int errid, bool isdispatch)
	{
		string content2;
		try
		{
			InfoByServer content = JsonUtility.FromJson<InfoByServer>(model.Notice);
			content2 = GetLocal(content);
		}
		catch (Exception ex)
		{
			content2 = model.Notice;
			Debug.LogError("跑马灯文本格式错误，无法解析" + ex);
		}
		SimpleSingletonProvider<UIManager>.inst.systemTips.showMarquee(content2);
		await UniTask.CompletedTask;
	}

	private string GetLocal(InfoByServer content)
	{
		return GameSettings.GetDataForLanguage(content.English, content.Japanese, content.Simplified, content.Traditional);
	}

	private static void TryRefreshMatchInfo()
	{
		MatchData matchData = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData;
		if (matchData != null)
		{
			if (matchData.TeamId == 0L)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.match.OnRefreshMatchTeamInfoS2CServerCallBackAsync(new RefreshMatchTeamInfoS2C(), 0, isDispatch: false).Forget();
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.match.RequestRefreshMatchTeamInfoC2S();
			}
		}
	}
}
