using System;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using UnityTimer;
using party.protocol;

namespace GameLogic;

public static class LoginServiceHelper
{
	public static ConnectC2S connectToken;

	public static LoginStatus _LoginStatus;

	private static float _CurrentRequestWebTime;

	private static Timer _QueueTimer;

	private static float _QueueTime;

	public static RPCAsyncResult RequestConnect(string nick)
	{
		nick = nick.TryCutSQLIllegalChars();
		connectToken = new ConnectC2S
		{
			PublicKey = "t4UDM%2Q",
			ClientVer = GameSettings.APP_VERSION,
			Auth = AuthType.Dev,
			Dev = new DevInfo
			{
				Nick = nick
			}
		};
		return MonoSingletonProvider<NetManager>.inst.RequestLogin(connectToken);
	}

	public static (bool, string) IsInvalidForServer()
	{
		if (HackerConfig.IsValid())
		{
			return (false, null);
		}
		NoticesServerData noticesServerData = SimpleSingletonProvider_AppLauncher<NoticesServerManager>.inst.NoticesServerData;
		if (noticesServerData?.server == null)
		{
			return (false, null);
		}
		if (noticesServerData.server.state != 0)
		{
			return (false, null);
		}
		if (Time.time - _CurrentRequestWebTime > (float)StaticGlobalData.CLIENT_REQUEST_SERVERSTATE_CD)
		{
			SimpleSingletonProvider_AppLauncher<NoticesServerManager>.inst.Get().Forget();
			_CurrentRequestWebTime = Time.time;
		}
		string item = noticesServerData.server.hint.Content();
		return (true, item);
	}

	public static void HandleBnSdkLoginResponse(ConnectS2C model)
	{
		if (BnSdkManager.Instance != null)
		{
			try
			{
				BnSdkManager.Instance.HandleBnSdkLoginResponse(model.Data);
				Debug.Log("返回userid给客户端进入对应游戏账号成功");
				return;
			}
			catch (Exception ex)
			{
				Debug.LogError("序列化服务器响应失败: " + ex.Message);
				return;
			}
		}
		Debug.LogWarning("BnSdkManager.Instance为空，无法处理BnSdk登录响应");
	}

	public static void TryQueueReconnect(int QueueTime)
	{
		if (_LoginStatus == LoginStatus.None)
		{
			return;
		}
		_QueueTime = QueueTime;
		Timer queueTimer = _QueueTimer;
		if (queueTimer != null)
		{
			queueTimer.Cancel();
		}
		if (_LoginStatus == LoginStatus.Queue)
		{
			IBasePanel currentPanel = SimpleSingletonProvider<UIManager>.inst.currentPanel;
			LoginPanel panel = currentPanel as LoginPanel;
			if (panel != null && panel.ILoginComponent != null)
			{
				panel.ILoginComponent.Refresh();
				_QueueTimer = Timer.Register(0f, (float)QueueTime, (Action)delegate
				{
					_LoginStatus = LoginStatus.None;
					panel.ILoginComponent.Refresh();
					panel.ILoginComponent.OnLogin();
				}, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)delegate(float time)
				{
					_QueueTime = Mathf.CeilToInt((float)QueueTime - time);
				}, (Action)null, false, -1f, false, (GameObject)null);
			}
		}
		else if (_LoginStatus == LoginStatus.Busy)
		{
			_QueueTimer = Timer.Register(0f, 5f, (Action)delegate
			{
				_LoginStatus = LoginStatus.None;
			}, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)delegate(float time)
			{
				_QueueTime = Mathf.CeilToInt((float)QueueTime - time);
			}, (Action)null, false, -1f, false, (GameObject)null);
		}
		ShowQueueMessageBox();
	}

	public static bool CheckServerLicense()
	{
		if (_LoginStatus != LoginStatus.None)
		{
			ShowQueueMessageBox();
			return false;
		}
		return true;
	}

	private static void ShowQueueMessageBox()
	{
		if (_LoginStatus == LoginStatus.Queue && _QueueTime > 0f)
		{
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowQueueUp(1003.GetLocal(UIStringType.Server), _QueueTime).Forget();
		}
		else if (_LoginStatus == LoginStatus.Busy)
		{
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(1002.GetLocal(UIStringType.Server)).Forget();
		}
	}

	public static void LogoutSDK()
	{
		Debug.Log("[SettingPanel_User] 调用国服SDK OnLogout接口");
		if (BnSdkManager.Instance != null)
		{
			BnSdkManager.Instance.OnLogout();
		}
	}

	public static string GetSDKSid()
	{
		return BnSdkManager.Instance?.Sid ?? "";
	}

	public static string GetSDKAppId()
	{
		return BnSdkInit.Instance.AppID;
	}

	public static string GetArea()
	{
		return "cn";
	}

	public static RPCAsyncResult RequestConnectWithBnSdk(string gameID, string channelID, string appID, string sid, string extra, string deviceid)
	{
		connectToken = new ConnectC2S
		{
			PublicKey = "t4UDM%2Q",
			ClientVer = GameSettings.APP_VERSION,
			Auth = AuthType.China,
			China = new ChinaInfo
			{
				GameId = gameID,
				ChannelId = channelID,
				AppId = appID,
				Sid = sid,
				Extra = extra,
				DeviceId = deviceid
			}
		};
		return MonoSingletonProvider<NetManager>.inst.RequestLogin(connectToken);
	}
}
