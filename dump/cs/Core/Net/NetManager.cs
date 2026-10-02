using System;
using System.Collections;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class NetManager : MonoSingletonProvider<NetManager>, IUSocketEventListener
{
	public enum SocketCallBackMsgType
	{
		OnConnect = 1,
		OnIdle,
		OnClose,
		OnTimeOut,
		OnError
	}

	public delegate UniTask AsyncAction();

	public enum ePingState
	{
		GOOD = 80,
		Normal = 210,
		Worse = 300,
		OutOfTime = 2000
	}

	public enum NetRPCMessageBox
	{
		None,
		RetryRPCMessageBox,
		ReconnectFailedBackToLogin
	}

	internal USocketGroup socket;

	private DateTime _serverDateTime = DateTime.UtcNow.AddHours(8.0);

	private int _clientUpdateServerTimeTick = NetHelper.GetEnvironmentTickCount();

	private ConcurrentQueue<Frame> receiveQueue = new ConcurrentQueue<Frame>(100);

	private ConcurrentQueue<Frame> sendQueue = new ConcurrentQueue<Frame>(100);

	private ConcurrentQueue<SocketCallBackMsgType> socketEventMsg = new ConcurrentQueue<SocketCallBackMsgType>(5);

	private Signal<bool> connectCallback = new Signal<bool>();

	public string ClientIP = "";

	public int ClientPort;

	public float TotalSendKB;

	public float TotalSendFrames;

	public float TotalReceiveKB;

	public float TotalReceiveFrames;

	private bool IsInitConnet;

	private float unscaledTime;

	private float _preReconnectingTime;

	public readonly Signal<bool> OnConnect = new Signal<bool>();

	public readonly Signal OnIdle = new Signal();

	public readonly Signal OnClose = new Signal();

	public readonly Signal OnError = new Signal();

	public readonly Signal OnTimeOut = new Signal();

	public CloseType closeType;

	private bool _Reconnecting;

	private int ConnectTimes;

	public AsyncAction onBeforeReconnect;

	public int PingTime;

	private bool isPinging;

	private bool startPing;

	private float pingStartTime;

	private const float maxPingWaitingTime = 2f;

	private const float PingRefreshTime = 5f;

	public static ePingState currentPingState = ePingState.OutOfTime;

	private float MaxWaitingPingTime = 15f;

	public bool IsAutoReconnectNet;

	private int connectCounter;

	private const int connectGuardCheckRate = 50;

	private HeartbeatC2S _cahedHeartbeat;

	public ServerErrorDealType SceneSpecialServerErrorDeal_OnReconnect;

	public RPCMsgManager RPC = new RPCMsgManager();

	private ConcurrentHashSet<long> ackQueue = new ConcurrentHashSet<long>();

	public CTimerSystem TimerSystem = new CTimerSystem();

	public int CurrentSocketWaitTime = 20;

	public static bool IsReturningToLogin = false;

	private ServerErrorDealType _preNetMessageBoxType;

	public bool IsConnected
	{
		get
		{
			if (socket != null)
			{
				return socket.IsAllSocketConnected();
			}
			return false;
		}
	}

	public string IP { get; private set; }

	public int Port { get; private set; }

	public DateTime ServerTime => _serverDateTime.AddMilliseconds(NetHelper.GetEnvironmentTickCount() - _clientUpdateServerTimeTick);

	private HeartbeatC2S heartbeat => _cahedHeartbeat ?? (_cahedHeartbeat = new HeartbeatC2S());

	protected override void Awake()
	{
		base.Awake();
		TimerSystem?.Create();
	}

	protected override void Start()
	{
		base.Start();
	}

	public void InitConnect()
	{
		if (!IsInitConnet)
		{
			IsInitConnet = true;
			RPC.RegisterIRPCSyncConnect();
			RPC.HeartbeatS2C.OnHeartbeatS2CServerCallBack = OnHeartbeatS2CServerCallBack;
			RPC.KickS2C.OnKickS2CServerCallBackAsync = OnReceivePushKickOffline;
		}
		StartPing();
	}

	private void Update()
	{
		unscaledTime = Time.unscaledTime;
		TimerSystem?.UpdateTimer();
		SocketEventUpdate();
		NetUpdate();
		NetPingCheck();
		NetConnectGuard();
	}

	protected override void OnDestroy()
	{
		Close();
		base.OnDestroy();
	}

	private void NetUpdate()
	{
		if (socket == null)
		{
			return;
		}
		while (receiveQueue.Count > 0)
		{
			Frame frame = receiveQueue.Dequeue();
			TotalReceiveKB += frame.FrameLen;
			TotalReceiveFrames += 1f;
			ReceiveRPC(frame);
		}
		if (!IsConnected)
		{
			return;
		}
		while (sendQueue.Count > 0)
		{
			if (sendQueue.Count > 50)
			{
				_ = sendQueue.Count % 50;
			}
			Frame frame2 = sendQueue.Dequeue();
			SocketSend(frame2);
		}
	}

	public void DestroyNetInst()
	{
		Close();
		RPC?.UnRegisterIRPCSyncConnect();
		TimerSystem?.Destroy();
		DestroyInst();
		SimpleSingletonProvider<UIManager>.inst.loadingTip.Hide();
	}

	protected override void OnApplicationQuit()
	{
		DestroyNetInst();
	}

	public NetAsyncResult Connect(string ip, int port, bool isAsync = true, Action<bool> callback = null)
	{
		NetAsyncResult res = new NetAsyncResult();
		try
		{
			if (socket != null && !socket.IsAllSocketClosed())
			{
				Close();
			}
			if (socket == null)
			{
				socket = new USocketGroup();
			}
			if (!IsConnected)
			{
				socket.AddListener(this);
			}
			if (socket.IsAllSocketConnected())
			{
				res.Complete();
				res.isSuccess = true;
				callback?.Invoke(obj: true);
				return res;
			}
			connectCallback.AddListener(delegate(bool b)
			{
				res.isSuccess = b;
				res.Complete();
				callback?.Invoke(b);
			});
			IP = ip;
			Port = port;
			res.isSuccess = socket.Connect(IP, Port, isAsync, 15000);
			res.Complete();
			return res;
		}
		catch (Exception ex)
		{
			res.Complete();
			res.isSuccess = false;
			callback?.Invoke(obj: false);
			Debug.LogError(ex.Message);
		}
		return res;
	}

	public void Close()
	{
		if (socket != null)
		{
			socket.RemoveListener(this);
			socket.AllClose();
		}
	}

	public void Reconnect(Action<bool> callback = null, RPCAsyncResult exceptClearRpcResult = null)
	{
		_ = unscaledTime - _preReconnectingTime;
		_ = 0.1f;
		if (IsConnected)
		{
			return;
		}
		_preReconnectingTime = unscaledTime;
		Connect(IP, Port, isAsync: true, delegate(bool success)
		{
			if (success)
			{
				_preReconnectingTime = unscaledTime;
				RPCAsyncResult exceptClearRpcResult2 = exceptClearRpcResult;
				RPC.ClearRPC(exceptClearRpcResult2);
			}
			else
			{
				callback?.Invoke(obj: false);
				Close();
			}
		});
	}

	public void Clear()
	{
		receiveQueue = new ConcurrentQueue<Frame>(100);
		sendQueue = new ConcurrentQueue<Frame>(100);
		ackQueue = new ConcurrentHashSet<long>();
		socketEventMsg = new ConcurrentQueue<SocketCallBackMsgType>(5);
		connectCallback = new Signal<bool>();
		Close();
		TimerSystem.Destroy();
		RPC.ClearRPC();
		RPC = new RPCMsgManager();
		socket = null;
	}

	private void SocketSend(Frame frame)
	{
		if (frame != null && socket != null)
		{
			socket.Send(frame);
			TotalSendKB += frame.FrameLen;
			TotalSendFrames += 1f;
		}
	}

	public void USocketEvent_OnMessage(USocketGroup us, Frame reader)
	{
		if (reader.UPSN > 0)
		{
			ackQueue.Remove(reader.UPSN);
		}
		receiveQueue.Enqueue(reader);
	}

	public void USocketEvent_OnClose(USocketGroup us, bool fromRemote)
	{
		socketEventMsg.Enqueue(SocketCallBackMsgType.OnClose);
		Debug.LogError("socketEventMsg: OnClose");
		if (us == socket)
		{
			Close();
		}
	}

	public void USocketEvent_OnIdle(USocketGroup us)
	{
		socketEventMsg.Enqueue(SocketCallBackMsgType.OnIdle);
		Debug.LogError("socketEventMsg: OnIdle");
		if (us == socket)
		{
			Close();
		}
	}

	public void USocketEvent_OnConnect(USocketGroup us, bool success)
	{
		if (success && us.IsAllSocketConnected())
		{
			socket = us;
		}
		socketEventMsg.Enqueue(SocketCallBackMsgType.OnConnect);
	}

	public void USocketEvent_OnTimeOut(USocketGroup us)
	{
		socketEventMsg.Enqueue(SocketCallBackMsgType.OnTimeOut);
		Debug.LogError("socketEventMsg: OnTimeOut");
		if (us == socket)
		{
			Close();
		}
	}

	public void USocketEvent_OnError(USocketGroup us, string err)
	{
		socketEventMsg.Enqueue(SocketCallBackMsgType.OnError);
		Debug.LogError("socketEventMsg: OnError");
		if (us == socket)
		{
			Close();
		}
	}

	private void SocketEventUpdate()
	{
		while (socketEventMsg.Count > 0)
		{
			switch (socketEventMsg.Dequeue())
			{
			case SocketCallBackMsgType.OnConnect:
				connectCallback.Dispatch(IsConnected);
				connectCallback.RemoveAllListeners();
				break;
			case SocketCallBackMsgType.OnClose:
			case SocketCallBackMsgType.OnTimeOut:
			case SocketCallBackMsgType.OnError:
				OnClose.Dispatch();
				connectCallback.Dispatch(t: false);
				connectCallback.RemoveAllListeners();
				break;
			}
		}
	}

	public void CloseServer()
	{
		switch (closeType)
		{
		case CloseType.FrequentConnectionFail:
			Clear();
			break;
		case CloseType.ServerKick:
			Clear();
			break;
		case CloseType.None:
			break;
		}
	}

	public RPCAsyncResult RequestLogin(ConnectC2S connectToken)
	{
		RPC.ConnectS2C.OnConnectS2CServerCallBackAsync = OnConnectS2CServerCallBack;
		return RPC.ConnectC2S.ConnectC2SCall(connectToken);
	}

	private async UniTask OnConnectS2CServerCallBack(ConnectS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Login)
			{
				LoginServiceHelper.TryQueueReconnect(model.QueueTime + 1);
				DestroyNetInst();
			}
			if (errId == 10003 && StaticConfigure.Server.ErrorDict.TryGetValue(errId, out var value))
			{
				if (model.BanTime > 0)
				{
					long num = model.BanTime / 60000;
					await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(string.Format(value.DescId.GetLocal(UIStringType.Server), num), ReLogin);
				}
				else
				{
					await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(1106.GetLocal(UIStringType.Message), ReLogin);
				}
			}
			return;
		}
		if (model == null || model.Account == null)
		{
			Debug.LogError("ConnectS2C 没有错误码，出现数据问题——账户数据为空，无法登录");
			return;
		}
		RPC.UpdateSessionId(model.SessionId);
		SimpleSingletonProvider<GameLogicManager>.inst.InitAccount(model.Account, model.Player, model);
		_serverDateTime = Convert.ToInt32(model.NowTime).StampToDateTime();
		InitConnect();
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Login)
		{
			await CommonUIManager.RegisterCommonExternalPackage();
			await SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.Home, "Home");
		}
		else
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
			{
				return;
			}
			RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
			if (room != null)
			{
				RoomInfo curRoomInfo = room.curRoomInfo;
				if (curRoomInfo == null || curRoomInfo.MapType != 10)
				{
					room.roomController.SwitchRoomState(null, RoomStateType.NONE);
				}
			}
		}
	}

	private async void ReconnectServer()
	{
		if (IsConnected)
		{
			SimpleSingletonProvider<UIManager>.inst.UnLoadLoadingTip();
		}
		else
		{
			if (closeType == CloseType.ServerKick || unscaledTime - _preReconnectingTime < 2f || _Reconnecting)
			{
				return;
			}
			_Reconnecting = true;
			ClearInfo();
			_preReconnectingTime = unscaledTime;
			ConnectTimes++;
			await TryShowLoadingTip();
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel != null && SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel.IsOpen())
			{
				SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel.Close();
			}
			await Connect(GameSettings.IP, GameSettings.Port, isAsync: true, delegate(bool success)
			{
				if (success)
				{
					_Reconnecting = false;
					ConnectTimes = 0;
					RequestLogin(LoginServiceHelper.connectToken).OnFinished.AddOnce(delegate(RPCAsyncResult _)
					{
						SimpleSingletonProvider<UIManager>.inst.loadingTip.Hide();
						if (_.errId != 0)
						{
							Close();
							HandleAfterNetworkError(_.errId);
						}
						else if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel != null && SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel.IsOpen())
						{
							SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel.Refresh();
						}
					});
				}
				else if (ConnectTimes >= 5)
				{
					ShowErrMsg();
				}
				else
				{
					_Reconnecting = false;
				}
			});
		}
	}

	private void HandleAfterNetworkError(int errId)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Login && StaticConfigure.Server.ErrorDict.TryGetValue(errId, out var value) && value.ServerErrorDealType == ServerErrorDealType.ReturnToLogin)
		{
			DestroyNetInst();
		}
	}

	private async UniTask TryShowLoadingTip()
	{
		if (!IsInTutorialRoom() && !SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
		{
			await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShowConnect(10000);
		}
	}

	private void ShowErrMsg()
	{
		if (!IsInTutorialRoom())
		{
			SimpleSingletonProvider<UIManager>.inst.loadingTip.Hide();
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(5, delegate
			{
				ConnectTimes = 0;
				_Reconnecting = false;
			}, ReLogin).Forget();
		}
	}

	public void HandleReConnect()
	{
		ConnectTimes = 0;
		_Reconnecting = false;
		StartPing();
	}

	private void ClearInfo()
	{
		RPC.ClearRPC();
		if (!IsInTutorialRoom() && SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.SinglePlayer)
		{
			SimpleSingletonProvider<DelaySignalManager>.inst?.CancelAllTask();
		}
	}

	public async void ReLogin()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room != null)
		{
			RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
			if (roomController.RoomValid && roomController.roomStateType == RoomStateType.READY)
			{
				await SimpleSingletonProvider<InternalAssetManager>.inst.Dispose();
			}
		}
		SimpleSingletonProvider<GameManager>.inst.ReLogin().Forget();
	}

	private bool IsInTutorialRoom()
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room == null)
		{
			return false;
		}
		return room.curRoomInfo?.MapType == 10;
	}

	private void NetPingCheck()
	{
		if (startPing)
		{
			if (!IsConnected)
			{
				UpdatePingState(2000f);
			}
			else if (unscaledTime - pingStartTime > 5f && !isPinging)
			{
				pingStartTime = unscaledTime;
				isPinging = true;
				heartbeat.Client = Convert.ToInt64(NetHelper.GetEnvironmentTickCount());
				RPC.HeartbeatC2S.HeartbeatC2SCall(heartbeat);
			}
			else if (isPinging && unscaledTime - pingStartTime > MaxWaitingPingTime)
			{
				isPinging = false;
				Close();
				UpdatePingState(float.MaxValue);
			}
		}
	}

	private void UpdatePingState(float pingTime)
	{
		ePingState ePingState;
		if (pingTime < 80f)
		{
			ePingState = ePingState.GOOD;
		}
		else if (pingTime < 210f)
		{
			ePingState = ePingState.Normal;
		}
		else if (pingTime < 300f)
		{
			ePingState = ePingState.Worse;
		}
		else
		{
			ePingState = ePingState.OutOfTime;
			isPinging = false;
			ReconnectServer();
		}
		currentPingState = ePingState;
	}

	private void NetConnectGuard()
	{
		connectCounter++;
		if (connectCounter % 50 == 0)
		{
			if (IsAutoReconnectNet && !IsConnected && (socket == null || socket.IsAllSocketClosed()) && !IsReturningToLogin)
			{
				OpenRetryRPCMessageBox(null);
			}
			connectCounter = 0;
		}
	}

	private void StartPing()
	{
		startPing = true;
	}

	public void StopPing()
	{
		startPing = false;
	}

	public void SetConnectSleepMode(bool isSleep)
	{
		if (socket != null)
		{
			socket.ThreadSleepTime = (isSleep ? 150 : CurrentSocketWaitTime);
		}
	}

	public void SetConnectThreadSpeed(bool isFast)
	{
		if (socket != null)
		{
			CurrentSocketWaitTime = (isFast ? 20 : 500);
			socket.ThreadSleepTime = CurrentSocketWaitTime;
		}
	}

	public void SetConnectThreadVeryFast(bool veryFast)
	{
		if (socket != null)
		{
			socket.ThreadSleepTime = 1;
		}
	}

	public NetworkType GetCurrentNetworkType()
	{
		return Application.internetReachability switch
		{
			NetworkReachability.NotReachable => NetworkType.None, 
			NetworkReachability.ReachableViaLocalAreaNetwork => NetworkType.WIFI, 
			NetworkReachability.ReachableViaCarrierDataNetwork => NetworkType.GPRS, 
			_ => NetworkType.None, 
		};
	}

	private void OnHeartbeatS2CServerCallBack(HeartbeatS2C model)
	{
		long num = Convert.ToInt64(NetHelper.GetEnvironmentTickCount());
		PingTime = (int)(num - model.Client);
		_clientUpdateServerTimeTick = NetHelper.GetEnvironmentTickCount();
		_serverDateTime = Convert.ToInt32(model.Server).StampToDateTime();
		UpdatePingState(PingTime);
		isPinging = false;
	}

	public void PRCSendWithAnim(RPCAsyncResult res)
	{
		res.timerID = TimerSystem.CreateTimer(15000u, delegate
		{
			RPCAsyncResult rPCAsyncResult = res;
			if (rPCAsyncResult.isCompleted)
			{
				TimerSystem.DestroyTimer(rPCAsyncResult.timerID);
				rPCAsyncResult.isTimeOut = true;
				rPCAsyncResult.isCompleted = true;
				RPC.DealRPCCallBack(rPCAsyncResult);
				rPCAsyncResult.Finish();
			}
		});
		StartCoroutine(IRPCNetAnim(res));
	}

	private IEnumerator IRPCNetAnim(RPCAsyncResult res)
	{
		if (!IsConnected)
		{
			DoReconnectWithRPCResend(res);
		}
		else
		{
			SendRPCResult(res);
		}
		while (!res.isCompleted)
		{
			yield return new WaitForEndOfFrame();
		}
		yield return null;
	}

	public void Clear_SceneSpecialServerErrorDeal_OnReconnect()
	{
		SceneSpecialServerErrorDeal_OnReconnect = ServerErrorDealType.None;
	}

	private void DoReconnectWithRPCResend(RPCAsyncResult _res)
	{
		if (_res.isCompleted)
		{
			RPC.ResetRPCToUncompleted(_res);
		}
		if (IsConnected)
		{
			return;
		}
		Reconnect(delegate(bool success2)
		{
			RPCAsyncResult rPCAsyncResult = _res;
			if (success2)
			{
				RPC.DealRPCCall(rPCAsyncResult);
			}
			else
			{
				OpenRetryRPCMessageBox(rPCAsyncResult);
			}
		});
	}

	internal void SendRPCResult(RPCAsyncResult res)
	{
		if (res != null)
		{
			Frame frame = res.frame;
			res.sendTime = NetHelper.GetEnvironmentTickCount();
			frame.CMDID = (short)res.CMDID;
			frame.UPSN = res.UPSN;
			frame.IsSendAutoConsumed = res.protoType == ProtolcalType.RealTime || res.protoType == ProtolcalType.none;
			SocketSend(frame);
			ackQueue.Add(frame.UPSN);
		}
	}

	internal void ReceiveRPC(Frame frame)
	{
		RPC.ReceiveRPCCallStatic(frame);
	}

	public async void HandleRPCCustomACKErrorCode(int errorcode)
	{
		if (IsReturningToLogin || !StaticConfigure.Server.ErrorDict.TryGetValue(errorcode, out var cfg))
		{
			return;
		}
		string text = HandleRPCErrorText(cfg);
		if (HandleRPCSpecialError(cfg, text))
		{
			return;
		}
		switch (cfg.ServerErrorShowType)
		{
		case ServerErrorShowType.None:
			HandleRPCACKErrorCodeHelper(cfg.ServerErrorDealType);
			break;
		case ServerErrorShowType.ShowOkwindow:
			await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(text, delegate
			{
				HandleRPCACKErrorCodeHelper(cfg.ServerErrorDealType);
			});
			break;
		case ServerErrorShowType.ShowOkcancelWindow:
			await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(text, delegate
			{
				HandleRPCACKErrorCodeHelper(cfg.ServerErrorDealType);
			});
			break;
		case ServerErrorShowType.ShowTips:
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(text);
			HandleRPCACKErrorCodeHelper(cfg.ServerErrorDealType);
			break;
		}
	}

	private bool HandleRPCSpecialError(ServerErrorConfigure errorCfg, string msg)
	{
		if (errorCfg.Id == 11001)
		{
			if (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is BattleSettlementPanel))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(msg);
			}
			ServerErrorDealType type = ((SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomHeroPanel) ? ServerErrorDealType.ReconnectNet : ServerErrorDealType.None);
			HandleRPCACKErrorCodeHelper(type);
			return true;
		}
		return false;
	}

	private void HandleRPCACKErrorCodeHelper(ServerErrorDealType type)
	{
		switch (type)
		{
		case ServerErrorDealType.ReturnToLogin:
			Close();
			BackToLogin();
			break;
		case ServerErrorDealType.RebootGame:
			SimpleSingletonProvider<GameManager>.inst.RestartGame();
			break;
		case ServerErrorDealType.ReconnectNet:
			Close();
			break;
		case ServerErrorDealType.CloseNetAndResend:
			Close();
			break;
		case ServerErrorDealType.RefreshUi:
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.Refresh();
			break;
		case ServerErrorDealType.QuitGame:
			Application.Quit();
			break;
		case ServerErrorDealType.QueueReconnect:
			LoginServiceHelper._LoginStatus = LoginStatus.Queue;
			break;
		case ServerErrorDealType.ServerBusy:
			LoginServiceHelper._LoginStatus = LoginStatus.Busy;
			break;
		case ServerErrorDealType.None:
			break;
		}
	}

	private string HandleRPCErrorText(ServerErrorConfigure _config)
	{
		return string.Format(6.GetLocal(UIStringType.Message), _config.Id + "\n" + _config.DescId.GetLocal(UIStringType.Server));
	}

	private async UniTask OnReceivePushKickOffline(KickS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			CloseServerByKick();
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(7, delegate
			{
				BackToLogin();
			}).Forget();
			await UniTask.CompletedTask;
		}
	}

	public void CloseServerByKick()
	{
		closeType = CloseType.ServerKick;
		CloseServer();
	}

	private bool PreCheckNetMessageBox(ServerErrorDealType type)
	{
		ServerErrorDealType preNetMessageBoxType = _preNetMessageBoxType;
		if (type <= preNetMessageBoxType)
		{
			return false;
		}
		return true;
	}

	public bool CheckHasNetMessageBox(ServerErrorDealType type)
	{
		return type == _preNetMessageBoxType;
	}

	public void BackToLogin()
	{
		LoginServiceHelper.LogoutSDK();
		SimpleSingletonProvider<GameManager>.inst.ReLogin().Forget();
	}

	public async void OpenRetryRPCMessageBox(RPCAsyncResult res)
	{
		await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(5, delegate
		{
			if (res == null)
			{
				Reconnect(delegate(bool success)
				{
					if (!success)
					{
						ReconnectServer();
					}
				});
			}
			else
			{
				HandleRPCACKErrorCodeHelper(SceneSpecialServerErrorDeal_OnReconnect);
				DoReconnectWithRPCResend(res);
			}
		}, delegate
		{
			SimpleSingletonProvider<GameManager>.inst.ReLogin().Forget();
		});
	}

	public void ReconnectWithMessageBoxUntilSuccess(RPCAsyncResult exceptRpcResult, Action onSuccess)
	{
	}

	private async void OnReconnectCallBack(RPCAsyncResult exceptRpcResult, Action onSuccess, bool success)
	{
		if (success)
		{
			onSuccess();
			return;
		}
		await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(5, delegate
		{
			ReconnectWithMessageBoxUntilSuccess(exceptRpcResult, onSuccess);
		}, delegate
		{
			SimpleSingletonProvider<GameManager>.inst.ReLogin().Forget();
		});
	}
}
