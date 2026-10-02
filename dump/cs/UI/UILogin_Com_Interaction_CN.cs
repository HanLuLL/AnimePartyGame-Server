using System;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UILogin_Com_Interaction_CN : GComponent, ILoginComponent
{
	private LoginPanel loginPanel;

	private UniTaskCompletionSource task;

	public Controller version;

	public GGraph graph_Effect;

	public GButton btn_StartGame;

	public GTextInput Input;

	public GTextField txt_Version;

	public GTextField txt_Version_01;

	public GButton btn_ApplicationQuit;

	public UILogin_Button_ShowNotice btn_ShowNotice;

	public UILogin_Button_CleanCache btn_CleanCache;

	public GButton btn_Logout;

	public GRichTextField txt_Version_02;

	public UILogin_Button_AgeTip btn_AgeTip;

	public Transition t0;

	public const string URL = "ui://tgr7xz46uspw1j";

	public void Show(LoginPanel panel)
	{
		MonoSingletonProvider<NetManager>.inst.onBeforeReconnect = SDKLoginAsync;
		loginPanel = panel;
		btn_StartGame.visible = false;
		btn_Logout.visible = false;
		AddEvent();
		Refresh();
		btn_ApplicationQuit.visible = false;
		btn_StartGame.visible = true;
		version.selectedIndex = 0;
		panel.ShowEffect(graph_Effect);
		txt_Version_02.richTextField.htmlParseOptions.linkUnderline = false;
	}

	public void AddEvent()
	{
		btn_Logout.onClick.Add(OnLogout);
		btn_StartGame.onClick.Add(OnLogin);
		btn_ApplicationQuit.onClick.Add(OnQuit);
		btn_ShowNotice.onClick.Add(loginPanel.ShowNotice);
		btn_CleanCache.onClick.Add(loginPanel.CleanCache);
		btn_AgeTip.onClick.Add(ShowAgeTip);
		txt_Version_02.onClickLink.Add(OpenIcpUrl);
	}

	public void RemoveEvent()
	{
		btn_Logout.onClick.Remove(OnLogout);
		btn_StartGame.onClick.Remove(OnLogin);
		btn_ApplicationQuit.onClick.Remove(OnQuit);
		btn_ShowNotice.onClick.Remove(loginPanel.ShowNotice);
		btn_CleanCache.onClick.Remove(loginPanel.CleanCache);
		btn_AgeTip.onClick.Remove(ShowAgeTip);
		txt_Version_02.onClickLink.Remove(OpenIcpUrl);
	}

	public void DisposeUI()
	{
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(graph_Effect);
		Dispose();
	}

	public void OnLogin()
	{
		Debug.Log("[UILogin_CN] ========== 开始游戏按钮被点击 ==========");
		RetainLoginButton();
		if (loginPanel.IsInvalidForServer())
		{
			Debug.LogWarning("[UILogin_CN] 服务器不可用，终止登录流程");
			ReleaseLoginButton(null);
			return;
		}
		BnSdkManager instance = BnSdkManager.Instance;
		if (instance == null)
		{
			Debug.LogError("[UILogin_CN] BnSdkManager.Instance 为 null，无法进行登录");
			ReleaseLoginButton(null);
			return;
		}
		Debug.Log("[UILogin_CN] 当前Sid状态: Sid=" + (string.IsNullOrEmpty(instance.Sid) ? "空" : instance.Sid) + ", Extra=" + (string.IsNullOrEmpty(instance.Extra) ? "空" : instance.Extra));
		Debug.Log($"[UILogin_CN] UI交互状态: touchable={GRoot.inst.touchable}");
		if (!GRoot.inst.touchable)
		{
			Debug.Log("[UILogin_CN] UI交互被禁用，SDK弹窗正在显示，不触发登录");
			ReleaseLoginButton(null);
		}
		else if (string.IsNullOrEmpty(instance.Sid))
		{
			Debug.Log("[UILogin_CN] Sid为空，先进行SDK登录");
			ReleaseLoginButton(null);
			Debug.Log("[UILogin_CN] 调用 BnSdkManager.Instance.LoginSDK()");
			BnSdkManager.Instance.LoginSDK();
		}
		else
		{
			Debug.Log("[UILogin_CN] Sid已存在，开始连接服务器");
			Connect(null);
		}
	}

	private async void Connect(string userName)
	{
		BnSdkInit bnSdk = BnSdkInit.Instance;
		BnSdkManager bnsdk1 = BnSdkManager.Instance;
		if ((UnityEngine.Object)(object)bnSdk == null)
		{
			Debug.LogError("[UILogin_CN] BnSdkInit.Instance 为 null，无法连接服务器");
			ReleaseLoginButton(null);
		}
		else if (bnsdk1 == null)
		{
			Debug.LogError("[UILogin_CN] BnSdkManager.Instance 为 null，无法连接服务器");
			ReleaseLoginButton(null);
		}
		else if (string.IsNullOrEmpty(bnsdk1.Sid))
		{
			Debug.LogWarning("[UILogin_CN] Sid为空，无法连接服务器");
			ReleaseLoginButton(null);
		}
		else if (!MonoSingletonProvider<NetManager>.inst.IsConnected)
		{
			await MonoSingletonProvider<NetManager>.inst.Connect(GameSettings.IP, GameSettings.Port, isAsync: true, delegate(bool success)
			{
				if (success)
				{
					LoginServiceHelper.RequestConnectWithBnSdk(bnSdk.GameID, bnSdk.ChannelID, bnSdk.AppID, bnsdk1.Sid, bnsdk1.Extra, bnSdk.DeviceId).OnFinished.AddOnce(ReleaseLoginButton);
				}
				else
				{
					SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(5, delegate
					{
						MonoSingletonProvider<NetManager>.inst.Connect(GameSettings.IP, GameSettings.Port);
					}).Forget();
					ReleaseLoginButton(null);
				}
			}).ToUniTask();
		}
		else
		{
			LoginServiceHelper.RequestConnectWithBnSdk(bnSdk.GameID, bnSdk.ChannelID, bnSdk.AppID, bnsdk1.Sid, bnsdk1.Extra, bnSdk.DeviceId).OnFinished.AddOnce(ReleaseLoginButton);
		}
	}

	public void Refresh()
	{
		Input.text = PlayerPrefs.GetString("Account");
		txt_Version.text = GameSettings.ShowVersion;
		btn_StartGame.onClick.Release();
		btn_StartGame.title = ((LoginServiceHelper._LoginStatus == LoginStatus.Queue) ? 25 : 24).GetLocal(UIStringType.Message);
	}

	private void OnLogout()
	{
		if (BnSdkManager.Instance != null)
		{
			BnSdkManager.Instance.OnLogout();
		}
		btn_Logout.visible = false;
	}

	private void OnQuit()
	{
		btn_ApplicationQuit.onClick.Retain();
		if (Application.platform != RuntimePlatform.Android)
		{
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1014, delegate
			{
				SimpleSingletonProvider<GameManager>.inst.CloseGame();
			}).Forget();
		}
		else if (BnSdkManager.Instance != null)
		{
			BnSdkManager.Instance.ExitSdk();
		}
		btn_ApplicationQuit.onClick.Release();
	}

	private void RetainLoginButton()
	{
		btn_StartGame.onClick.Retain();
	}

	private void ReleaseLoginButton(RPCAsyncResult result)
	{
		if (result != null && result.errId == 0)
		{
			Debug.Log("[UILogin_CN] 登录成功，不释放按钮");
			return;
		}
		Debug.Log("[UILogin_CN] 释放登录按钮，允许用户再次点击");
		btn_StartGame.onClick.Release();
	}

	private async UniTask SDKLoginAsync()
	{
		task = new UniTaskCompletionSource();
		SDKLoginImp(delegate
		{
			task.TrySetResult();
		});
		await task.Task;
	}

	public void SDKLoginImp(Action success)
	{
		BnSdkManager.Instance.CheckTimeDifferenceOnReconnect(success);
	}

	private void ShowAgeTip()
	{
		SimpleSingletonProvider<UIManager>.inst.info.ShowLoginAgeTip();
	}

	private void OpenIcpUrl(EventContext context)
	{
		Application.OpenURL("https://beian.miit.gov.cn/");
	}

	public static UILogin_Com_Interaction_CN CreateInstance()
	{
		return (UILogin_Com_Interaction_CN)UIPackage.CreateObject("Login", "Login_Com_Interaction_CN");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		version = GetControllerAt(0);
		graph_Effect = (GGraph)GetChildAt(0);
		btn_StartGame = (GButton)GetChildAt(1);
		Input = (GTextInput)GetChildAt(5);
		txt_Version = (GTextField)GetChildAt(7);
		txt_Version_01 = (GTextField)GetChildAt(8);
		btn_ApplicationQuit = (GButton)GetChildAt(9);
		btn_ShowNotice = (UILogin_Button_ShowNotice)GetChildAt(10);
		btn_CleanCache = (UILogin_Button_CleanCache)GetChildAt(11);
		btn_Logout = (GButton)GetChildAt(13);
		txt_Version_02 = (GRichTextField)GetChildAt(14);
		btn_AgeTip = (UILogin_Button_AgeTip)GetChildAt(15);
		t0 = GetTransitionAt(0);
	}
}
