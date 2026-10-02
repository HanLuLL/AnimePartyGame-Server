using AK.Wwise.Unity.WwiseAddressables;
using App;
using Core.Net;
using CriWare;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scene;

public class LoginSceneController : BaseSceneController
{
	private static bool _hasInitedGame;

	protected override void Awake()
	{
		if (!_hasInitedGame)
		{
			AkAddressableBankManager.Instance.ReloadAllBanks();
		}
		base.Awake();
	}

	protected override async void Start()
	{
		base.Start();
		await InitGameBeforeShowLoginUI();
		await LoadAudioBank();
		NetManager.IsReturningToLogin = false;
		await SimpleSingletonProvider<UIManager>.inst.AsyncInit();
		IBasePanel panel = await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Login);
		if (UGUIController.IsNeedOpenNoticeWindow())
		{
			await SimpleSingletonProvider<WebServerManager>.inst.ShowNoticeWindow();
		}
		GameObject gameObject = GameObject.Find("AppLauncher");
		if (gameObject != null)
		{
			Object.Destroy(gameObject);
		}
		GameObject gameObject2 = GameObject.Find("BackgroundCanvas");
		if (gameObject2 != null)
		{
			CriManaMovieControllerForUI componentInChildren = gameObject2.GetComponentInChildren<CriManaMovieControllerForUI>();
			if ((Object)(object)componentInChildren != null && ((Behaviour)(object)componentInChildren).enabled)
			{
				SimpleSingletonProvider<GameManager>.inst.ActionVideoName = ((Component)(object)componentInChildren).gameObject.name;
				if (gameObject2.TryGetComponent<Canvas>(out var component))
				{
					component.renderMode = (RenderMode)2;
				}
				if (gameObject2.TryGetComponent<CanvasScaler>(out var component2))
				{
					Object.Destroy((Object)(object)component2);
				}
				if (panel is LoginPanel loginPanel)
				{
					loginPanel.TryShowCanvas(gameObject2);
				}
			}
			else
			{
				if (gameObject2.TryGetComponent<Canvas>(out var component3))
				{
					component3.renderMode = (RenderMode)2;
				}
				if (gameObject2.TryGetComponent<CanvasScaler>(out var component4))
				{
					Object.Destroy((Object)(object)component4);
				}
				if (panel is LoginPanel loginPanel2)
				{
					loginPanel2.TryShowCanvas(gameObject2);
				}
			}
		}
		StartLoading();
		TimeProtector.Create();
		AstralErrorHandler val = Object.FindObjectOfType<AstralErrorHandler>();
		if ((Object)(object)val != null)
		{
			Object.Destroy(((Component)(object)val).gameObject);
			new GameObject("RunTimeAstralErrorHandler").AddComponent<RunTimeAstralErrorHandler>();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		UnloadAudioBank();
	}

	private async UniTask InitGameBeforeShowLoginUI()
	{
		if (!_hasInitedGame)
		{
			_hasInitedGame = true;
			await RunTimeRemoteConfigHandler.TryInit();
			InitServerConfig();
			await StaticConfigure.InitAsync();
			SimpleSingletonProvider<AudioManager>.inst.Init();
			GameSettings.InitSetting();
			InitSDKs();
		}
	}

	private async UniTask LoadAudioBank()
	{
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_BGM_LOGIN);
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_UI);
	}

	private void UnloadAudioBank()
	{
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_BGM_LOGIN);
	}

	private void InitServerConfig()
	{
		GameSettings.IP = WebServerConfig.ServerIp;
		GameSettings.LogServerIP = "0.0.0.0";
		GameSettings.Port = 8800;
		GameSettings.LogSeverPort = 9999;
		GameSettings.Port = WebServerConfig.ServerPort;
	}

	private void StartLoading()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is LoginPanel loginPanel)
		{
			loginPanel.ShowInteraction();
		}
		Debug.Log("初始化已完成，开始调用BnSdk登录接口");
		if (BnSdkManager.Instance == null)
		{
			GameObject obj = new GameObject("BnSdkManager");
			obj.AddComponent<BnSdkManager>();
			Object.DontDestroyOnLoad(obj);
		}
		BnSdkManager.Instance.LoginSDK();
	}

	protected override SceneType GetSceneType()
	{
		return SceneType.Login;
	}

	public void InitSDKs()
	{
	}
}
