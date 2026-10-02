using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UILogin_Com_Interaction : GComponent, ILoginComponent
{
	private LoginPanel loginPanel;

	public Controller version;

	public Controller isCn;

	public GGraph graph_Effect;

	public GButton btn_StartGame;

	public GTextInput Input;

	public GTextField txt_Version;

	public GTextField txt_Version_01;

	public GButton btn_Quit_PC;

	public GButton btn_Quit_Mobile;

	public UILogin_Button_ShowNotice btn_ShowNotice;

	public UILogin_Button_CleanCache btn_CleanCache;

	public GButton btn_InspectionLogin;

	public GButton btn_Logout;

	public UILogin_Button_AgeTip btn_AgeTip;

	public GRichTextField txt_Version_02;

	public Transition t0;

	public const string URL = "ui://tgr7xz465g1r0";

	public void Show(LoginPanel panel)
	{
		loginPanel = panel;
		btn_Quit_PC.visible = false;
		btn_Quit_Mobile.visible = false;
		btn_StartGame.visible = false;
		btn_InspectionLogin.visible = false;
		AddEvent();
		Refresh();
		btn_Quit_Mobile.visible = true;
		btn_StartGame.visible = true;
		version.selectedIndex = 0;
		isCn.selectedIndex = 0;
		panel.ShowEffect(graph_Effect);
		txt_Version_02.richTextField.htmlParseOptions.linkUnderline = false;
	}

	public void Refresh()
	{
		Input.text = PlayerPrefs.GetString("Account");
		txt_Version.text = GameSettings.ShowVersion;
		btn_StartGame.onClick.Release();
		btn_StartGame.title = ((LoginServiceHelper._LoginStatus == LoginStatus.Queue) ? 25 : 24).GetLocal(UIStringType.Message);
	}

	public void AddEvent()
	{
		btn_InspectionLogin.onClick.Add(ShowInput);
		btn_StartGame.onClick.Add(OnLogin);
		btn_Quit_PC.onClick.Add(OnQuit);
		btn_Quit_Mobile.onClick.Add(OnQuit);
		btn_ShowNotice.onClick.Add(loginPanel.ShowNotice);
		btn_CleanCache.onClick.Add(loginPanel.CleanCache);
		txt_Version_02.onClickLink.Add(OpenIcpUrl);
	}

	public void RemoveEvent()
	{
		btn_InspectionLogin.onClick.Remove(ShowInput);
		btn_StartGame.onClick.Remove(OnLogin);
		btn_Quit_PC.onClick.Remove(OnQuit);
		btn_Quit_Mobile.onClick.Remove(OnQuit);
		btn_ShowNotice.onClick.Remove(loginPanel.ShowNotice);
		btn_CleanCache.onClick.Remove(loginPanel.CleanCache);
		txt_Version_02.onClickLink.Remove(OpenIcpUrl);
	}

	public void DisposeUI()
	{
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(graph_Effect);
		Dispose();
	}

	private void ShowInput(EventContext context)
	{
		btn_InspectionLogin.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.input.OpenInspectionLogin(Connect);
		btn_InspectionLogin.onClick.Release();
	}

	private void OnQuit()
	{
		btn_Quit_PC.onClick.Retain();
		btn_Quit_Mobile.onClick.Retain();
		SimpleSingletonProvider<GameManager>.inst.CloseGame();
		btn_Quit_PC.onClick.Release();
		btn_Quit_Mobile.onClick.Release();
	}

	public void OnLogin()
	{
		RetainLoginButton();
		if (loginPanel.IsInvalidForServer())
		{
			ReleaseLoginButton(null);
			return;
		}
		string text = Input.text;
		if (string.IsNullOrEmpty(text))
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1);
			ReleaseLoginButton(null);
		}
		else
		{
			PlayerPrefs.SetString("Account", text);
			Connect(text);
		}
	}

	private async void Connect(string userName)
	{
		if (!MonoSingletonProvider<NetManager>.inst.IsConnected)
		{
			await MonoSingletonProvider<NetManager>.inst.Connect(GameSettings.IP, GameSettings.Port, isAsync: true, delegate(bool success)
			{
				if (success)
				{
					LoginServiceHelper.RequestConnect(userName).OnFinished.AddOnce(ReleaseLoginButton);
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
			LoginServiceHelper.RequestConnect(userName).OnFinished.AddOnce(ReleaseLoginButton);
		}
	}

	private void RetainLoginButton()
	{
		btn_StartGame.onClick.Retain();
	}

	private void ReleaseLoginButton(RPCAsyncResult result)
	{
		if (result == null || result.errId != 0)
		{
			btn_StartGame.onClick.Release();
		}
	}

	private void OpenIcpUrl(EventContext context)
	{
		Application.OpenURL("https://beian.miit.gov.cn/");
	}

	public static UILogin_Com_Interaction CreateInstance()
	{
		return (UILogin_Com_Interaction)UIPackage.CreateObject("Login", "Login_Com_Interaction");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		version = GetControllerAt(0);
		isCn = GetControllerAt(1);
		graph_Effect = (GGraph)GetChildAt(0);
		btn_StartGame = (GButton)GetChildAt(1);
		Input = (GTextInput)GetChildAt(5);
		txt_Version = (GTextField)GetChildAt(7);
		txt_Version_01 = (GTextField)GetChildAt(8);
		btn_Quit_PC = (GButton)GetChildAt(9);
		btn_Quit_Mobile = (GButton)GetChildAt(10);
		btn_ShowNotice = (UILogin_Button_ShowNotice)GetChildAt(11);
		btn_CleanCache = (UILogin_Button_CleanCache)GetChildAt(12);
		btn_InspectionLogin = (GButton)GetChildAt(14);
		btn_Logout = (GButton)GetChildAt(15);
		btn_AgeTip = (UILogin_Button_AgeTip)GetChildAt(16);
		txt_Version_02 = (GRichTextField)GetChildAt(17);
		t0 = GetTransitionAt(0);
	}
}
