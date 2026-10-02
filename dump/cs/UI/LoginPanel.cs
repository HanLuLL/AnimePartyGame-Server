using System.IO;
using Core;
using CriWare.Assets;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class LoginPanel : BasePanel<UILoginPanel>
{
	public ILoginComponent ILoginComponent { get; private set; }

	public LoginPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UILoginPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
	}

	public override void Refresh()
	{
		base.Refresh();
		TryShowActionVideo().Forget();
		TryShowBackground().Forget();
		base.ui.aMoive_Loading.visible = true;
		base.ui.logoVersion.selectedIndex = ((GameSettings.languageType != LanguageType.SimplifiedChinese) ? 1 : 0);
		base.ui.angelMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
		base.ui.country.selectedIndex = GameSettings.COUNTRY;
	}

	protected override void AddEvent()
	{
		base.AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		ILoginComponent?.RemoveEvent();
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
		CriManaMovieControllerForAsset component;
		if (base.ui.graph_DynamicBg.displayObject is GoWrapper goWrapper)
		{
			Object.Destroy(goWrapper.wrapTarget);
		}
		else if (base.ui.graph_DynamicBg.shape.gameObject.TryGetComponent<CriManaMovieControllerForAsset>(out component))
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(base.ui.graph_DynamicBg);
		}
		ILoginComponent?.DisposeUI();
		base.Dispose();
	}

	public override void AdultMode(bool inAdultMode)
	{
		base.ui.angelMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
	}

	private void ShowInteractionCom(GComponent com)
	{
		com.AddRelation(base.ui, FairyGUI.RelationType.Height);
		com.AddRelation(base.ui, FairyGUI.RelationType.Center_Center);
		int num = Mathf.CeilToInt(Screen.safeArea.width / GRoot.contentScaleFactor);
		com.SetSize(num, base.ui.height);
		com.Center();
		base.ui.AddChild(com);
		base.ui.aMoive_Loading.visible = false;
	}

	public void ShowInteraction()
	{
		ILoginComponent = UILogin_Com_Interaction_CN.CreateInstance();
		ShowInteractionCom((GComponent)ILoginComponent);
		ILoginComponent.Show(this);
	}

	public bool IsInvalidForServer()
	{
		if (!LoginServiceHelper.CheckServerLicense())
		{
			return true;
		}
		var (flag, text) = LoginServiceHelper.IsInvalidForServer();
		if (flag)
		{
			if (string.IsNullOrEmpty(text))
			{
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(10).Forget();
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(text).Forget();
			}
			return true;
		}
		return false;
	}

	public void ShowEffect(GGraph graph)
	{
		if (graph != null)
		{
			EffectInfoConfigure effectDataConfigure = 1000.GetEffectDataConfigure();
			SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure.EffectName, graph).Forget();
		}
	}

	public async void ShowNotice(EventContext context)
	{
		EventDispatcher sender = context.sender;
		if (sender is GButton button)
		{
			button.onClick.Retain();
			await SimpleSingletonProvider<WebServerManager>.inst.ShowNoticeWindow();
			button.onClick.Release();
		}
	}

	public async void CleanCache(EventContext context)
	{
		EventDispatcher sender = context.sender;
		GButton button = sender as GButton;
		if (button == null)
		{
			return;
		}
		button.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(8, delegate
		{
			button.onClick.Release();
			try
			{
				if (Directory.Exists(Application.persistentDataPath + "/Temp"))
				{
					Directory.Delete(Application.persistentDataPath + "/Temp", recursive: true);
				}
				if (Directory.Exists(Application.persistentDataPath + "/com.unity.addressables"))
				{
					Directory.Delete(Application.persistentDataPath + "/com.unity.addressables", recursive: true);
				}
				Caching.ClearCache();
			}
			finally
			{
				Application.Quit();
			}
		}, delegate
		{
			button.onClick.Release();
		});
	}

	private async UniTask TryShowActionVideo()
	{
		string actionVideoName = SimpleSingletonProvider<GameManager>.inst.ActionVideoName;
		if (string.IsNullOrEmpty(actionVideoName))
		{
			base.ui.graph_DynamicBg.visible = false;
			return;
		}
		base.ui.graph_DynamicBg.FullScreen();
		await SimpleSingletonProvider<CriMovieManager>.inst.Play(actionVideoName, base.ui.graph_DynamicBg);
	}

	private async UniTask TryShowBackground()
	{
		if (SimpleSingletonProvider<GameManager>.inst.needLoadBg)
		{
			Texture texture = await UGUIDynamicBackground.Load();
			base.ui.loader_bg.texture = new NTexture(texture);
		}
	}

	public void TryShowCanvas(GameObject dynamicCanvas)
	{
		RectTransform component = dynamicCanvas.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(base.ui.width, base.ui.height);
		component.localScale = Vector3.one;
		base.ui.graph_DynamicBg.visible = true;
		base.ui.graph_DynamicBg.SetNativeObject(new GoWrapper(dynamicCanvas));
		dynamicCanvas.GetComponentInChildren<UGUIBackground>().FitScreen();
	}

	protected override void FullScreen()
	{
		base.FullScreen();
		if (base.ui != null && base.ui.graph_DynamicBg.displayObject is GoWrapper goWrapper && goWrapper.wrapTarget != null)
		{
			goWrapper.wrapTarget.GetComponent<RectTransform>().sizeDelta = new Vector2(base.ui.width, base.ui.height);
		}
	}

	internal void ChangeSDKLoginStatus(bool status)
	{
		if (ILoginComponent is UILogin_Com_Interaction_CN uILogin_Com_Interaction_CN)
		{
			uILogin_Com_Interaction_CN.btn_Logout.visible = status;
		}
	}
}
