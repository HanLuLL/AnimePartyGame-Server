using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class SettingWindow : BaseWindow
{
	private List<SettingsLanguageConfigure> _languageConfigures;

	private string[] ResolutionDesc;

	private readonly List<SettingsResolutionConfigure> ResolutionsConfig = new List<SettingsResolutionConfigure>();

	private string[] QualityDesc;

	private string[] DisplayModeDesc;

	private string[] _frameRateDesc;

	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private Color[] scoreColors = new Color[5]
	{
		new Color(1f, 0.16862746f, 29f / 51f),
		new Color(1f, 0.34509805f, 0.38431373f),
		new Color(1f, 0.6431373f, 7f / 85f),
		new Color(56f / 85f, 0.85490197f, 0.08627451f),
		new Color(73f / 85f, 0.8235294f, 0.7490196f)
	};

	private Vector2Int[] scoreRanges = new Vector2Int[5]
	{
		new Vector2Int(90, 100),
		new Vector2Int(80, 89),
		new Vector2Int(70, 79),
		new Vector2Int(60, 69),
		new Vector2Int(0, 59)
	};

	private int[] scoreDescIndices = new int[5] { 101, 102, 103, 104, 105 };

	private int[] msgIndices = new int[6] { 1010, 1020, 1030, 1040, 1050, 9990 };

	private void LiveMode_Refresh()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_LiveMode.com_CoverSwither.selected = GameSettings.IncoverMode;
			uISettingWindow.com_LiveMode.com_AdultSwither.selected = GameSettings.angelMode;
			uISettingWindow.com_LiveMode.com_CardSuggestSwither.title = 1010003.GetLocal(UIStringType.GUI);
			uISettingWindow.com_LiveMode.com_RoadSuggestSwither.title = 1010004.GetLocal(UIStringType.GUI);
			uISettingWindow.com_LiveMode.com_RelicSuggestSwither.title = 1010005.GetLocal(UIStringType.GUI);
			uISettingWindow.com_LiveMode.com_ActionSuggestSwither.title = 1010008.GetLocal(UIStringType.GUI);
			uISettingWindow.com_LiveMode.com_CardSuggestSwither.selected = GameSettings.CardSuggest;
			uISettingWindow.com_LiveMode.com_RoadSuggestSwither.selected = GameSettings.RoadSuggest;
			uISettingWindow.com_LiveMode.com_RelicSuggestSwither.selected = GameSettings.RelicSuggest;
			uISettingWindow.com_LiveMode.com_AutoShortChatSwither.selected = GameSettings.AutoShortChat;
			uISettingWindow.com_LiveMode.com_ActionSuggestSwither.selected = GameSettings.ActionSuggest;
			uISettingWindow.com_LiveMode.isAuditMode.selectedIndex = 0;
		}
	}

	private void LiveMode_AddEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_LiveMode.com_CoverSwither.onChanged.Add(OnCoverSwitch);
			uISettingWindow.com_LiveMode.com_AdultSwither.onChanged.Add(OnAdultSwitch);
			uISettingWindow.com_LiveMode.com_CardSuggestSwither.onChanged.Add(OnChangeCardSuggestSetting);
			uISettingWindow.com_LiveMode.com_RoadSuggestSwither.onChanged.Add(OnChangeRoadSuggestSetting);
			uISettingWindow.com_LiveMode.com_RelicSuggestSwither.onChanged.Add(OnChangeRelicSuggestSetting);
			uISettingWindow.com_LiveMode.com_AutoShortChatSwither.onChanged.Add(OnChangeAutoShortChatSetting);
			uISettingWindow.com_LiveMode.com_ActionSuggestSwither.onChanged.Add(OnChangeActionSuggestSetting);
		}
	}

	private void LiveMode_RemoveEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_LiveMode.com_CoverSwither.onChanged.Remove(OnCoverSwitch);
			uISettingWindow.com_LiveMode.com_AdultSwither.onChanged.Remove(OnAdultSwitch);
			uISettingWindow.com_LiveMode.com_CardSuggestSwither.onChanged.Remove(OnChangeCardSuggestSetting);
			uISettingWindow.com_LiveMode.com_RoadSuggestSwither.onChanged.Remove(OnChangeRoadSuggestSetting);
			uISettingWindow.com_LiveMode.com_RelicSuggestSwither.onChanged.Remove(OnChangeRelicSuggestSetting);
			uISettingWindow.com_LiveMode.com_AutoShortChatSwither.onChanged.Remove(OnChangeAutoShortChatSetting);
			uISettingWindow.com_LiveMode.com_ActionSuggestSwither.onChanged.Remove(OnChangeActionSuggestSetting);
		}
	}

	private void OnCoverSwitch()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			bool selected = uISettingWindow.com_LiveMode.com_CoverSwither.selected;
			if (selected != GameSettings.IncoverMode)
			{
				GameSettings.IncoverMode = selected;
				GameSettings.UpdateCoverModeState(GameSettings.IncoverMode);
				SimpleSingletonProvider<UIManager>.inst.SwitchCoverMode(GameSettings.IncoverMode);
			}
		}
	}

	private void OnAdultSwitch(EventContext context)
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			bool selected = uISettingWindow.com_LiveMode.com_AdultSwither.selected;
			if (selected != GameSettings.angelMode)
			{
				GameSettings.angelMode = selected;
				GameSettings.UpdateAdultModeState(GameSettings.angelMode);
				SimpleSingletonProvider<UIManager>.inst.SwitchAdultMode(GameSettings.angelMode);
			}
		}
	}

	private void OnChangeCardSuggestSetting(EventContext context)
	{
		int setting = ((!GameSettings.CardSuggest) ? 1 : 0);
		SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateSetting(ClientData_SettingType.CardSuggest, setting);
	}

	private void OnChangeRoadSuggestSetting(EventContext context)
	{
		int setting = ((!GameSettings.RoadSuggest) ? 1 : 0);
		SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateSetting(ClientData_SettingType.RoadSuggest, setting);
	}

	private void OnChangeRelicSuggestSetting(EventContext context)
	{
		int setting = ((!GameSettings.RelicSuggest) ? 1 : 0);
		SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateSetting(ClientData_SettingType.RelicSuggest, setting);
	}

	private void OnChangeAutoShortChatSetting()
	{
		int setting = ((!GameSettings.AutoShortChat) ? 1 : 0);
		SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateSetting(ClientData_SettingType.AutoShortChat, setting);
	}

	private void OnChangeActionSuggestSetting()
	{
		int setting = ((!GameSettings.ActionSuggest) ? 1 : 0);
		SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateSetting(ClientData_SettingType.ActionSuggest, setting);
	}

	private void Screen_InitComponent()
	{
	}

	private void Screen_Refresh()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			if (GameSettings.languageType == LanguageType.Japanese)
			{
				uISettingWindow.com_Screen.Screen_Refresh_Item.lable_title.scaleX = 0.9f;
			}
			ReadyFrameRateDesc();
			uISettingWindow.com_Screen.com_FrameRate.items = _frameRateDesc;
			uISettingWindow.com_Screen.isMobile.selectedIndex = 1;
			uISettingWindow.com_Screen.com_camera.isMobile.selectedIndex = 1;
			ReadyQualityDesc();
			uISettingWindow.com_Screen.com_Quality.items = QualityDesc;
			uISettingWindow.com_Screen.com_Quality.selectedIndex = GetQualitySelectedIndex();
			uISettingWindow.com_Screen.com_Quality.GetTextField().text = QualityDesc[uISettingWindow.com_Screen.com_Quality.selectedIndex];
			uISettingWindow.com_Screen.com_FrameRate.visible = true;
			uISettingWindow.com_Screen.com_FrameRate.touchable = true;
			uISettingWindow.com_Screen.com_FrameRate.grayed = false;
			RefreshFrameRate(GameSettings.GetFramesIndex(), vSyncSwitchStatus: false);
			uISettingWindow.com_Screen.com_energySaving.com_energySaving.selected = GameSettings.EnergySavingMode;
			uISettingWindow.com_Screen.com_camera.com_FreeCameraSwither.selected = GameSettings.freeCamera;
			uISettingWindow.com_Screen.country.selectedIndex = GameSettings.COUNTRY;
			RefreshLanguage(uISettingWindow);
		}
	}

	private void RefreshLanguage(UISettingWindow win)
	{
		_languageConfigures = StaticConfigure.Settings.Languages.ToList();
		win.com_Screen.com_Language.items = _languageConfigures.Select((SettingsLanguageConfigure info) => info.DescriptionID.GetLocal(UIStringType.Settings)).ToArray();
		LanguageType curLanguageType = GameSettings.GetLanguagesType();
		int num = _languageConfigures.FindIndex((SettingsLanguageConfigure info) => info.LanguageType == curLanguageType);
		if (num != -1)
		{
			win.com_Screen.com_Language.selectedIndex = num;
			win.com_Screen.com_Language.GetTextField().text = _languageConfigures[num].DescriptionID.GetLocal(UIStringType.Settings);
			win.com_Screen.com_Language.visible = true;
		}
		else
		{
			win.com_Screen.com_Language.visible = false;
		}
	}

	private void RefreshFrameRate(int index, bool vSyncSwitchStatus)
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_Screen.com_FrameRate.selectedIndex = index;
			uISettingWindow.com_Screen.com_FrameRate.GetTextField().text = _frameRateDesc[index];
			uISettingWindow.com_Screen.com_FrameRate.grayed = vSyncSwitchStatus;
			uISettingWindow.com_Screen.com_FrameRate.touchable = !vSyncSwitchStatus;
		}
	}

	private void Screen_AddEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_Screen.com_Quality.onChanged.Add(ChangeQuality);
			uISettingWindow.com_Screen.com_energySaving.com_energySaving.onChanged.Add(OnChangeEnergySaving);
			uISettingWindow.com_Screen.com_camera.com_FreeCameraSwither.onChanged.Add(OnFreeCameraSwitch);
			uISettingWindow.com_Screen.com_FrameRate.onChanged.Add(SelectFrame);
			uISettingWindow.com_Screen.com_Language.onChanged.Add(ChangeLanguage);
			GameSettings.onGraphicsTypeChanged.AddListener(OnGraphicsTypeChanged);
			GameSettings.onFrameRateChanged.AddListener(OnFrameRateChanged);
			GameSettings.onEnergySavingModeChanged.AddListener(OnEnergySavingModeChanged);
		}
	}

	private void Screen_RemoveEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_Screen.com_Quality.onChanged.Remove(ChangeQuality);
			uISettingWindow.com_Screen.com_energySaving.com_energySaving.onChanged.Remove(OnChangeEnergySaving);
			uISettingWindow.com_Screen.com_camera.com_FreeCameraSwither.onChanged.Remove(OnFreeCameraSwitch);
			uISettingWindow.com_Screen.com_FrameRate.onChanged.Remove(SelectFrame);
			uISettingWindow.com_Screen.com_Language.onChanged.Remove(ChangeLanguage);
			GameSettings.onGraphicsTypeChanged.RemoveListener(OnGraphicsTypeChanged);
			GameSettings.onFrameRateChanged.RemoveListener(OnFrameRateChanged);
			GameSettings.onEnergySavingModeChanged.RemoveListener(OnEnergySavingModeChanged);
		}
	}

	private void ChangeLanguage(EventContext context)
	{
		GComponent gComponent = base.contentPane;
		UISettingWindow win = gComponent as UISettingWindow;
		if (win == null)
		{
			return;
		}
		int curIndex = win.com_Screen.com_Language.selectedIndex;
		if (GameSettings.GetLanguagesType() == _languageConfigures[curIndex].LanguageType)
		{
			return;
		}
		win.com_Screen.com_Language.onChanged.Retain();
		SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(34, delegate
		{
			if (_languageConfigures.Count > curIndex)
			{
				GameSettings.Update_LanguagesType((int)_languageConfigures[curIndex].LanguageType);
				win.com_Screen.com_Language.onChanged.Release();
				SimpleSingletonProvider<GameManager>.inst.ReLogin().Forget();
			}
		}, delegate
		{
			RefreshLanguage(win);
			win.com_Screen.com_Language.onChanged.Release();
		}).Forget();
	}

	private void ReadyResolutionDesc()
	{
		int systemHeight = Display.main.systemHeight;
		ResolutionsConfig.Clear();
		for (int i = 0; i < StaticConfigure.Settings.Resolutions.Count; i++)
		{
			SettingsResolutionConfigure settingsResolutionConfigure = StaticConfigure.Settings.Resolutions[i];
			if (settingsResolutionConfigure.Id == 1 || settingsResolutionConfigure.Height <= systemHeight)
			{
				ResolutionsConfig.Add(StaticConfigure.Settings.Resolutions[i]);
			}
		}
		ResolutionDesc = new string[ResolutionsConfig.Count];
		for (int j = 0; j < ResolutionsConfig.Count; j++)
		{
			ResolutionDesc[j] = ResolutionsConfig[j].DescriptionID.GetLocal(UIStringType.Settings);
		}
	}

	private async void ChangeResolution(EventContext context)
	{
		await ChangeScreen();
	}

	private async UniTask ChangeScreen()
	{
		if (!(base.contentPane is UISettingWindow uISettingWindow))
		{
			return;
		}
		GameSettings.Update_ResolutionConfigId(ResolutionsConfig[uISettingWindow.com_Screen.com_Resolution.selectedIndex].Id);
		GameSettings.InitScreenSetting();
		await UniTask.DelayFrame(2);
		foreach (GObject child in GRoot.inst.children)
		{
			child.MakeFullScreen();
		}
	}

	private int GetResolutionSelectedIndex()
	{
		int resolutionConfigId = GameSettings.GetResolutionConfigId();
		for (int i = 0; i < ResolutionsConfig.Count; i++)
		{
			if (ResolutionsConfig[i].Id == resolutionConfigId)
			{
				return i;
			}
		}
		return 0;
	}

	private void ReadyQualityDesc()
	{
		RepeatedField<SettingsQualityConfigure> qualitys = StaticConfigure.Settings.Qualitys;
		QualityDesc = new string[qualitys.Count];
		for (int i = 0; i < qualitys.Count; i++)
		{
			int descriptionID = qualitys[i].DescriptionID;
			int num = qualitys[i].Id;
			if (StaticConfigure.STRSettings == null)
			{
				QualityDesc[i] = ((GraphicsType)(num - 1)/*cast due to .constrained prefix*/).ToString();
			}
			else if (!StaticConfigure.STRSettings.LocalDict.ContainsKey(descriptionID))
			{
				QualityDesc[i] = ((GraphicsType)(num - 1)/*cast due to .constrained prefix*/).ToString();
			}
			else
			{
				QualityDesc[i] = descriptionID.GetLocal(UIStringType.Settings);
			}
		}
	}

	private int GetQualitySelectedIndex()
	{
		RepeatedField<SettingsQualityConfigure> qualitys = StaticConfigure.Settings.Qualitys;
		int qualityGraphicsType = (int)GameSettings.GetQualityGraphicsType();
		for (int i = 0; i < qualitys.Count; i++)
		{
			if (qualitys[i].Id == qualityGraphicsType + 1)
			{
				return i;
			}
		}
		return 0;
	}

	private void ChangeQuality()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			RepeatedField<SettingsQualityConfigure> qualitys = StaticConfigure.Settings.Qualitys;
			GraphicsType qualityGraphicsType = GameSettings.GetQualityGraphicsType();
			int num = qualitys[uISettingWindow.com_Screen.com_Quality.selectedIndex].Id - 1;
			if (qualityGraphicsType != (GraphicsType)num)
			{
				GameSettings.SetScreenQuality((GraphicsType)num);
			}
		}
	}

	private void OnGraphicsTypeChanged(GraphicsType type)
	{
		if (type != GraphicsType.Low)
		{
			GameSettings.EnergySavingMode = false;
		}
	}

	private void ReadyDisplayModeDesc()
	{
		DisplayModeDesc = new string[StaticConfigure.Settings.DisplayModes.Count];
		for (int i = 0; i < StaticConfigure.Settings.DisplayModes.Count; i++)
		{
			DisplayModeDesc[i] = StaticConfigure.Settings.DisplayModes[i].DescriptionID.GetLocal(UIStringType.Settings);
		}
	}

	private async void ChangeDisplayMode(EventContext context)
	{
		await ChangeDisplayMode();
	}

	private async UniTask ChangeDisplayMode()
	{
		if (base.contentPane is UISettingWindow uISettingWindow && GameSettings.GetWindowTypeIndex() != uISettingWindow.com_Screen.com_Window.selectedIndex)
		{
			GameSettings.UpdateIndex_WindowType(uISettingWindow.com_Screen.com_Window.selectedIndex);
			await ChangeScreen();
		}
	}

	private void ReadyFrameRateDesc()
	{
		RepeatedField<SettingsRefreshRateConfigure> refreshRates = StaticConfigure.Settings.RefreshRates;
		_frameRateDesc = new string[refreshRates.Count];
		for (int i = 0; i < refreshRates.Count; i++)
		{
			_frameRateDesc[i] = refreshRates[i].DescriptionID.GetLocal(UIStringType.Settings);
		}
	}

	private void SelectFrame()
	{
		if (base.contentPane is UISettingWindow uISettingWindow && GameSettings.GetFramesIndex() != uISettingWindow.com_Screen.com_FrameRate.selectedIndex)
		{
			GameSettings.UpdateIndex_Frames(uISettingWindow.com_Screen.com_FrameRate.selectedIndex);
			Application.targetFrameRate = GameSettings.GetFrames();
		}
	}

	private void OnFrameRateChanged(int index)
	{
		if (base.contentPane is UISettingWindow && index != 0)
		{
			GameSettings.EnergySavingMode = false;
		}
	}

	private void OnMouseSwitch()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			bool selected = uISettingWindow.com_Screen.com_camera.com_MouseSwither.selected;
			if (selected != GameSettings.MouseControl)
			{
				GameSettings.MouseControl = selected;
				GameSettings.UpdateMouseControlState(GameSettings.MouseControl);
			}
		}
	}

	private void OnKeySwitch()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			bool selected = uISettingWindow.com_Screen.com_camera.com_KeyBoardSwither.selected;
			if (selected != GameSettings.KeyControl)
			{
				GameSettings.KeyControl = selected;
				GameSettings.UpdateKeyControlState(GameSettings.KeyControl);
			}
		}
	}

	private void OnFreeCameraSwitch()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			bool selected = uISettingWindow.com_Screen.com_camera.com_FreeCameraSwither.selected;
			if (selected != GameSettings.freeCamera)
			{
				GameSettings.freeCamera = selected;
				GameSettings.UpdateFreeCameraState(GameSettings.freeCamera);
			}
		}
	}

	private void OnChangeEnergySaving()
	{
		if (!(base.contentPane is UISettingWindow uISettingWindow))
		{
			return;
		}
		bool selected = uISettingWindow.com_Screen.com_energySaving.com_energySaving.selected;
		if (selected != GameSettings.EnergySavingMode)
		{
			GameSettings.EnergySavingMode = selected;
			if (selected)
			{
				EnergySavingMode(0);
				return;
			}
			int defaultGraphicsType = (int)GameSettings.GetDefaultGraphicsType();
			EnergySavingMode(defaultGraphicsType);
		}
	}

	private void EnergySavingMode(int value)
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_Screen.com_Quality.selectedIndex = value;
			ChangeQuality();
		}
	}

	private void OnEnergySavingModeChanged(bool isOn)
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_Screen.com_energySaving.com_energySaving.selected = isOn;
		}
	}

	private void User_InitComponent()
	{
	}

	private void User_Refresh()
	{
		if (!(base.contentPane is UISettingWindow uISettingWindow))
		{
			return;
		}
		uISettingWindow.com_User.txtField_CDK.title.text = "";
		uISettingWindow.com_User.btn_QuitGame.visible = SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Home;
		uISettingWindow.com_User.iosAuditMode.selectedIndex = 1;
		if (RunTimeRemoteConfigHandler.IsAuditMode)
		{
			uISettingWindow.com_User.platformState.selectedIndex = 4;
			return;
		}
		uISettingWindow.com_User.platformState.selectedIndex = 3;
		uISettingWindow.com_User.txt_mail.text = "3156108220@qq.com";
		int childIndex = uISettingWindow.com_User.GetChildIndex(uISettingWindow.com_User.btn_ContactService);
		GObject childAt = uISettingWindow.com_User.GetChildAt(childIndex - 1);
		uISettingWindow.com_User.btn_ContactService.visible = RunTimeRemoteConfigHandler.EnableContact;
		childAt.visible = RunTimeRemoteConfigHandler.EnableContact;
		bool flag = RunTimeRemoteConfigHandler.EnableContact;
		if (!flag)
		{
			flag = BnSdkInit.Instance.AppID != "110001939";
		}
		int childIndex2 = uISettingWindow.com_User.GetChildIndex(uISettingWindow.com_User.btn_QuitGame);
		GObject childAt2 = uISettingWindow.com_User.GetChildAt(childIndex2 - 2);
		uISettingWindow.com_User.btn_QuitGame.visible = flag;
		childAt2.visible = flag;
	}

	private void User_AddEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_User.btn_CDK.onClick.Add(RequestCDK);
			uISettingWindow.com_User.btn_UserAgreement.onClick.Add(ShowUserAgreements);
			uISettingWindow.com_User.btn_Policy.onClick.Add(ShowPrivacyPolicys);
			uISettingWindow.com_User.btn_QuitGame.onClick.Add(ReturnLogin);
			uISettingWindow.com_User.btn_AccountCenter.onClick.Add(ShowAccountCenter);
			uISettingWindow.com_User.btn_ContactService.onClick.Add(ShowContactService);
			uISettingWindow.com_User.btn_Developers.onClick.Add(ShowDeveloper);
			uISettingWindow.com_User.btn_Gratitude.onClick.Add(ShowThanks);
		}
	}

	private void User_RemoveEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_User.btn_CDK.onClick.Remove(RequestCDK);
			uISettingWindow.com_User.btn_UserAgreement.onClick.Remove(ShowUserAgreements);
			uISettingWindow.com_User.btn_Policy.onClick.Remove(ShowPrivacyPolicys);
			uISettingWindow.com_User.btn_QuitGame.onClick.Remove(ReturnLogin);
			uISettingWindow.com_User.btn_AccountCenter.onClick.Remove(ShowAccountCenter);
			uISettingWindow.com_User.btn_ContactService.onClick.Remove(ShowContactService);
			uISettingWindow.com_User.btn_Developers.onClick.Remove(ShowDeveloper);
			uISettingWindow.com_User.btn_Gratitude.onClick.Remove(ShowThanks);
		}
	}

	private async void ReturnLogin(EventContext context)
	{
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UISettingWindow win))
		{
			return;
		}
		win.com_User.btn_QuitGame.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(12, delegate
		{
			Debug.Log("[SettingPanel_User] 调用国服SDK OnLogout接口");
			if (BnSdkManager.Instance != null)
			{
				BnSdkManager.Instance.OnLogout();
			}
			else
			{
				Debug.LogError("[SettingPanel_User] BnSdkManager.Instance为空，无法调用SDK Logout接口");
				SimpleSingletonProvider<GameManager>.inst.ReLogin().Forget();
			}
		});
		win.com_User.btn_QuitGame.onClick.Release();
	}

	private void RequestCDK(EventContext context)
	{
		GComponent gComponent = base.contentPane;
		UISettingWindow win = gComponent as UISettingWindow;
		if (win != null && !string.IsNullOrEmpty(win.com_User.txtField_CDK.title.text))
		{
			win.com_User.btn_CDK.onClick.Retain();
			CDKServiceHelper.RequestGiftCdkC2S(win.com_User.txtField_CDK.title.text).OnFinishedOnly.AddOnce(delegate
			{
				win.com_User.btn_CDK.onClick.Release();
			});
		}
	}

	private void ShowUserAgreements()
	{
		if (base.contentPane is UISettingWindow)
		{
			string text = "";
			text = BnSdkInit.Instance.AppID;
			Application.OpenURL("https://0.0.0.0/index/agreements?game_id=120000182&app_id=110001949&game_version=1.0.0&os=windows&sdk_version=1.0.0.3&type=agreement&app_id=" + text);
		}
	}

	private void ShowPrivacyPolicys()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			string text = "";
			text = BnSdkInit.Instance.AppID;
			Application.OpenURL("https://0.0.0.0/index/agreements?game_id=120000182&app_id=110001949&game_version=1.0.0&os=windows&sdk_version=1.0.0.3&type=privacy&app_id=" + text);
			uISettingWindow.com_User.btn_Policy.onClick.Release();
		}
	}

	private void ShowAccountCenter()
	{
	}

	private void ShowContactService()
	{
		if (!(base.contentPane is UISettingWindow uISettingWindow))
		{
			return;
		}
		uISettingWindow.com_User.btn_ContactService.onClick.Retain();
		try
		{
			string sDKSid = LoginServiceHelper.GetSDKSid();
			string sDKAppId = LoginServiceHelper.GetSDKAppId();
			string text = PackageChannel.Get();
			string area = LoginServiceHelper.GetArea();
			string text2 = "1";
			string text3 = "se";
			string text4 = SimpleSingletonProvider<GameLogicManager>.inst.account.GetAccountId().ToString() ?? "";
			string value = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName() ?? "";
			string dataForLanguage = GameSettings.GetDataForLanguage("en-US", "ja-JP", "zh-CN", "zh-TW", "ko-KR");
			string sign = ContactService.CreateCustomerServiceSign(new Dictionary<string, string>
			{
				["access_token"] = sDKSid,
				["user_id"] = text4,
				["nickname"] = value,
				["game_id"] = text3,
				["area_id"] = area,
				["zone_id"] = text2,
				["channel"] = text,
				["language"] = dataForLanguage,
				["sdk_app_id"] = sDKAppId
			});
			OpenContactServiceUrl(sDKSid, text4, value, area, text2, text, text3, sDKAppId, dataForLanguage, sign);
			uISettingWindow.com_User.btn_ContactService.onClick.Release();
		}
		catch (Exception ex)
		{
			Debug.LogError("[SettingPanel_User] 构建联系客服URL异常: " + ex.Message + "\n" + ex.StackTrace);
			uISettingWindow.com_User.btn_ContactService.onClick.Release();
		}
	}

	private void OpenContactServiceUrl(string token, string userID, string name, string area, string zone, string channel, string gid, string sdkAppId, string locale, string sign)
	{
		try
		{
			string text = "0.0.0.0";
			string text2 = "https://" + text + "/?token=" + Uri.EscapeDataString(token) + "&userID=" + Uri.EscapeDataString(userID) + "&name=" + Uri.EscapeDataString(name) + "&area=" + Uri.EscapeDataString(area) + "&zone=" + Uri.EscapeDataString(zone) + "&channel=" + Uri.EscapeDataString(channel) + "&gid=" + Uri.EscapeDataString(gid) + "&sdkAppId=" + Uri.EscapeDataString(sdkAppId) + "&locale=" + Uri.EscapeDataString(locale) + "&sign=" + Uri.EscapeDataString(sign);
			Debug.Log("[SettingPanel_User] 联系客服URL: " + text2);
			Application.OpenURL(text2);
		}
		catch (Exception ex)
		{
			Debug.LogError("[SettingPanel_User] 打开联系客服URL异常: " + ex.Message + "\n" + ex.StackTrace);
		}
	}

	private async void ShowThanks()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UISettingWindow win)
		{
			win.com_User.btn_Gratitude.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.settingList.ShowList(SettingListWindow.SettingListType.Thanks);
			win.com_User.btn_Gratitude.onClick.Release();
		}
	}

	private async void ShowDeveloper()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UISettingWindow win)
		{
			win.com_User.btn_Developers.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.settingList.ShowList(SettingListWindow.SettingListType.Developer);
			win.com_User.btn_Developers.onClick.Release();
		}
	}

	private void Volume_InitComponent()
	{
	}

	private void Volume_Refresh()
	{
		if (!(base.contentPane is UISettingWindow uISettingWindow))
		{
			return;
		}
		if (GameSettings.languageType == LanguageType.Japanese)
		{
			uISettingWindow.com_Volume.Character_Voice_Settings.lable_title.scaleX = 0.75f;
		}
		uISettingWindow.com_Volume.slider_MasterVolume.value = GameSettings.GetMasterVolume();
		uISettingWindow.com_Volume.slider_BGMVolume.value = GameSettings.GetBGMVolume();
		uISettingWindow.com_Volume.slider_SFXVolume.value = GameSettings.GetSFXVolume();
		uISettingWindow.com_Volume.slider_VoiceVolume.value = GameSettings.GetVoiceVolume();
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		uISettingWindow.com_Volume.gameState.selectedIndex = ((roomController == null || roomController.roomStateType != RoomStateType.RUNNING) ? 1 : 0);
		LanguageType voiceLanguage = GameSettings.GetVoiceLanguage();
		int num = 0;
		string[] array = new string[StaticConfigure.Settings.VoiceLanguages.Count];
		for (int i = 0; i < StaticConfigure.Settings.VoiceLanguages.Count; i++)
		{
			SettingsVoiceLanguageConfigure settingsVoiceLanguageConfigure = StaticConfigure.Settings.VoiceLanguages[i];
			array[i] = settingsVoiceLanguageConfigure.DescriptionID.GetLocal(UIStringType.Settings);
			if (voiceLanguage == settingsVoiceLanguageConfigure.LanguageType)
			{
				num = i;
			}
		}
		uISettingWindow.com_Volume.com_VoiceLanguages.items = array;
		uISettingWindow.com_Volume.com_VoiceLanguages.selectedIndex = StaticConfigure.Settings.VoiceLanguages[num].DataIndex;
		uISettingWindow.com_Volume.com_VoiceLanguages.GetTextField().text = array[num];
		uISettingWindow.com_Volume.isMobile.selectedIndex = 1;
		uISettingWindow.com_Volume.com_vibrate.com_tipsvibrate.selected = GameSettings.TipsVibrateControl;
		uISettingWindow.com_Volume.com_vibrate.com_messagevibrate.selected = GameSettings.MessageVibrateControl;
	}

	private void Volume_AddEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_Volume.slider_MasterVolume.onChanged.Add(SetMasterVolume);
			uISettingWindow.com_Volume.slider_BGMVolume.onChanged.Add(SetBGMVolume);
			uISettingWindow.com_Volume.slider_SFXVolume.onChanged.Add(SetSFXVolume);
			uISettingWindow.com_Volume.slider_VoiceVolume.onChanged.Add(SetVoiceVolume);
			uISettingWindow.com_Volume.com_VoiceLanguages.onChanged.Add(ChangeVoiceLanguage);
			uISettingWindow.com_Volume.com_vibrate.com_tipsvibrate.onChanged.Add(OnTipVibrateSwitch);
			uISettingWindow.com_Volume.com_vibrate.com_messagevibrate.onChanged.Add(OnMessageVibrateSwitch);
		}
	}

	private void Volume_RemoveEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_Volume.slider_MasterVolume.onChanged.Remove(SetMasterVolume);
			uISettingWindow.com_Volume.slider_BGMVolume.onChanged.Remove(SetBGMVolume);
			uISettingWindow.com_Volume.slider_SFXVolume.onChanged.Remove(SetSFXVolume);
			uISettingWindow.com_Volume.slider_VoiceVolume.onChanged.Remove(SetVoiceVolume);
			uISettingWindow.com_Volume.com_VoiceLanguages.onChanged.Remove(ChangeVoiceLanguage);
			uISettingWindow.com_Volume.com_vibrate.com_tipsvibrate.onChanged.Remove(OnTipVibrateSwitch);
			uISettingWindow.com_Volume.com_vibrate.com_messagevibrate.onChanged.Remove(OnMessageVibrateSwitch);
		}
	}

	private void SetMasterVolume()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			GameSettings.UpdateMasterVolume((float)uISettingWindow.com_Volume.slider_MasterVolume.value);
			SetVolume("MasterVolume", (float)uISettingWindow.com_Volume.slider_MasterVolume.value);
		}
	}

	private void SetBGMVolume()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			GameSettings.UpdateBGMVolume((float)uISettingWindow.com_Volume.slider_BGMVolume.value);
			SetVolume("BGMVolume", (float)uISettingWindow.com_Volume.slider_BGMVolume.value);
		}
	}

	private void SetSFXVolume()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			GameSettings.UpdateSFXVolume((float)uISettingWindow.com_Volume.slider_SFXVolume.value);
			SetVolume("SFXVolume", (float)uISettingWindow.com_Volume.slider_SFXVolume.value);
		}
	}

	private void SetVoiceVolume()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			GameSettings.UpdateVoiceVolume((float)uISettingWindow.com_Volume.slider_VoiceVolume.value);
			SetVolume("VoiceVolume", (float)uISettingWindow.com_Volume.slider_VoiceVolume.value);
		}
	}

	private void SetVolume(string volumeType, float value)
	{
		SimpleSingletonProvider<AudioManager>.inst.SetRTPCValue(volumeType, value);
	}

	private void ChangeVoiceLanguage()
	{
		GComponent gComponent = base.contentPane;
		UISettingWindow win = gComponent as UISettingWindow;
		if (win == null)
		{
			return;
		}
		win.com_Volume.com_VoiceLanguages.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(34, delegate
		{
			int selectedIndex = win.com_Volume.com_VoiceLanguages.selectedIndex;
			if (selectedIndex >= 0 && selectedIndex < StaticConfigure.Settings.VoiceLanguages.Count)
			{
				GameSettings.UpdateVoiceLanguage(StaticConfigure.Settings.VoiceLanguages[selectedIndex].LanguageType);
				SimpleSingletonProvider<GameManager>.inst.ReLogin().Forget();
			}
			win.com_Screen.com_Language.onChanged.Release();
		}, delegate
		{
			Volume_Refresh();
			win.com_Screen.com_Language.onChanged.Release();
		}).Forget();
	}

	private void OnTipVibrateSwitch()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			bool selected = uISettingWindow.com_Volume.com_vibrate.com_tipsvibrate.selected;
			if (selected != GameSettings.TipsVibrateControl)
			{
				GameSettings.TipsVibrateControl = selected;
				GameSettings.UpdateTipsVibrateStatus(GameSettings.TipsVibrateControl);
			}
		}
	}

	private void OnMessageVibrateSwitch()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			bool selected = uISettingWindow.com_Volume.com_vibrate.com_messagevibrate.selected;
			if (selected != GameSettings.MessageVibrateControl)
			{
				GameSettings.MessageVibrateControl = selected;
				GameSettings.UpdateMessageVibrateStatus(GameSettings.MessageVibrateControl);
			}
		}
	}

	private int GetScoreIndex(int score)
	{
		int result = 0;
		int num = scoreRanges.Length;
		for (int i = 0; i < num; i++)
		{
			Vector2Int vector2Int = scoreRanges[i];
			if (score >= vector2Int.x && score <= vector2Int.y)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	private void CreditScore_AddEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_Score.btn_check.onClick.Add(OnBtnCheckClick);
			uISettingWindow.com_Detail.btn_ok.onClick.Add(OnBtnOKClick);
			uISettingWindow.com_Detail.btn_quit.onClick.Add(OnBtnQuitClick);
			uISettingWindow.loader_bg_detail.onClick.Add(OnBtnQuitClick);
		}
	}

	private void CreditScore_RemoveEvent()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.com_Score.btn_check.onClick.Remove(OnBtnCheckClick);
			uISettingWindow.com_Detail.btn_ok.onClick.Remove(OnBtnOKClick);
			uISettingWindow.com_Detail.btn_quit.onClick.Remove(OnBtnQuitClick);
			uISettingWindow.loader_bg_detail.onClick.Remove(OnBtnQuitClick);
		}
	}

	private void OnBtnCheckClick()
	{
		if (base.contentPane is UISettingWindow)
		{
			ShowDetail();
		}
	}

	private void OnBtnOKClick()
	{
		if (base.contentPane is UISettingWindow)
		{
			HideDetail();
		}
	}

	private void OnBtnQuitClick()
	{
		if (base.contentPane is UISettingWindow)
		{
			HideDetail();
		}
	}

	private async void ShowDetail()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UISettingWindow win)
		{
			win.com_Score.btn_check.onClick.Retain();
			await blurBgCtrl.CreateBlurTex((GameCameraFlag)2);
			blurBgCtrl.OnShown(win.loader_bg_detail);
			win.loader_bg_detail.visible = true;
			win.com_Detail.visible = true;
			win.RoleCut_in.Play();
			win.com_Score.btn_check.onClick.Release();
		}
	}

	private void HideDetail()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			blurBgCtrl.OnHide();
			uISettingWindow.loader_bg_detail.visible = false;
			uISettingWindow.com_Detail.visible = false;
			uISettingWindow.com_Detail.com_desc.scrollPane.ScrollTop(ani: false);
		}
	}

	private void CreditScore_Refresh()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			int creditScore = SimpleSingletonProvider<GameLogicManager>.inst.account.CreditScore;
			int scoreIndex = GetScoreIndex(creditScore);
			Color color = scoreColors[scoreIndex];
			uISettingWindow.com_Score.txt_score.text = $"{creditScore}";
			uISettingWindow.com_Score.txt_score.textFormat.outlineColor = color;
			uISettingWindow.com_Score.txt_score.textFormat.shadowColor = color;
			uISettingWindow.com_Score.txt_desc.text = scoreDescIndices[scoreIndex].GetLocal(UIStringType.Match);
			GTextField[] array = new GTextField[6]
			{
				uISettingWindow.com_Detail.com_desc.txt_1,
				uISettingWindow.com_Detail.com_desc.txt_2,
				uISettingWindow.com_Detail.com_desc.txt_3,
				uISettingWindow.com_Detail.com_desc.txt_4,
				uISettingWindow.com_Detail.com_desc.txt_5,
				uISettingWindow.com_Detail.com_desc.txt_6
			};
			for (int i = 0; i < msgIndices.Length; i++)
			{
				array[i].text = msgIndices[i].GetLocal(UIStringType.Match);
			}
			uISettingWindow.com_Detail.visible = false;
			blurBgCtrl.OnHide();
			uISettingWindow.loader_bg_detail.visible = false;
		}
	}

	public SettingWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UISettingWindow.CreateInstance();
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
		Volume_InitComponent();
		Screen_InitComponent();
		User_InitComponent();
	}

	public async UniTask ShowSetting()
	{
		await TryShowAsync();
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.tab.selectedIndex = 0;
			SwitchTab();
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			uISettingWindow.SetCut_in.Play();
			uISettingWindow.tab.onChanged.Add(SwitchTab);
			Volume_AddEvent();
			Screen_AddEvent();
			User_AddEvent();
			LiveMode_AddEvent();
			CreditScore_AddEvent();
			uISettingWindow.country.selectedIndex = GameSettings.COUNTRY;
			uISettingWindow.btn_Return.onClick.Add(ReturnPanel);
			long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
			uISettingWindow.btn_UID.data = playerID;
			uISettingWindow.btn_UID.txt_Uid.text = playerID.ToString();
			uISettingWindow.btn_UID.onClick.Add(CopyUID);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			uISettingWindow.tab.onChanged.Remove(SwitchTab);
			Volume_RemoveEvent();
			Screen_RemoveEvent();
			User_RemoveEvent();
			LiveMode_RemoveEvent();
			CreditScore_RemoveEvent();
			uISettingWindow.btn_UID.onClick.Remove(CopyUID);
			uISettingWindow.btn_Return.onClick.Remove(ReturnPanel);
		}
	}

	private void SwitchTab()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			if (uISettingWindow.tab.selectedIndex == 0)
			{
				Screen_Refresh();
				uISettingWindow.HuaMianCut_In.Play();
			}
			else if (uISettingWindow.tab.selectedIndex == 1)
			{
				Volume_Refresh();
				uISettingWindow.ShengYinCut_in.Play();
			}
			else if (uISettingWindow.tab.selectedIndex == 2)
			{
				User_Refresh();
				uISettingWindow.YongHuCut_in.Play();
			}
			else if (uISettingWindow.tab.selectedIndex == 3)
			{
				LiveMode_Refresh();
				uISettingWindow.GongNengCut_in.Play();
			}
			else if (uISettingWindow.tab.selectedIndex == 4)
			{
				CreditScore_Refresh();
				uISettingWindow.XinYuFenCut_in.Play();
			}
		}
	}

	private void ReturnPanel()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.btn_Return.onClick.Retain();
			Hide();
			uISettingWindow.btn_Return.onClick.Release();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (context.inputEvent.keyCode == KeyCode.Escape)
		{
			ReturnPanel();
		}
	}

	private void CopyUID()
	{
		if (base.contentPane is UISettingWindow uISettingWindow)
		{
			uISettingWindow.btn_UID.onClick.Retain();
			if (uISettingWindow.btn_UID.data is long num)
			{
				GUIUtility.systemCopyBuffer = num.ToString();
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1009.GetLocal(UIStringType.Spectate));
			}
			uISettingWindow.btn_UID.onClick.Release();
		}
	}
}
