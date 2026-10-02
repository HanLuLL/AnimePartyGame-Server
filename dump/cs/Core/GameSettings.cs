using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core;

public static class GameSettings
{
	public static string APP_VERSION;

	public static string RES_VERSION;

	public static bool IsRunningOnSteamDeck;

	public static int VER1;

	public static int VER2;

	public static int VER3;

	public static readonly int SPECIAL_ITEM_STARDISC_FREE = 2;

	public static readonly int SPECIAL_ITEM_STARDISC_PAY = 8;

	public static readonly int CURRENCY_EXCHANGE_RATE = 1;

	public static bool MouseControl;

	public static bool KeyControl;

	public static bool freeCamera;

	public static LanguageType languageType;

	public static bool IncoverMode;

	public static bool angelMode;

	private const byte InternalSettingVersion = 1;

	private const byte AngelModeFlag = 1;

	private const int InternalSettingHeaderLength = 8;

	private static readonly byte[] InternalSettingMagic = new byte[4] { 65, 80, 67, 70 };

	public static GraphicsType graphicsType;

	public static string IP;

	public static int Port;

	public static string LogServerIP;

	public static int LogSeverPort;

	public static Signal<GraphicsType> onGraphicsTypeChanged;

	public static Signal<int> onFrameRateChanged;

	public static Signal<bool> onEnergySavingModeChanged;

	public static bool TipsVibrateControl;

	public static bool MessageVibrateControl;

	public static string ShowVersion => "[CN]APP:" + Application.version + " RES:" + RES_VERSION;

	public static int COUNTRY => 2;

	public static HarmonyType HarmonyType { get; private set; }

	public static bool EnergySavingMode
	{
		get
		{
			return GetEnergySavingState();
		}
		set
		{
			SetEnergySavingState(value);
		}
	}

	public static bool CardSuggest
	{
		get
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (account == null)
			{
				return false;
			}
			return account.GetSettingFromServer(ClientData_SettingType.CardSuggest, 1) == 1;
		}
	}

	public static bool RoadSuggest
	{
		get
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (account == null)
			{
				return false;
			}
			return account.GetSettingFromServer(ClientData_SettingType.RoadSuggest, 1) == 1;
		}
	}

	public static bool RelicSuggest
	{
		get
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (account == null)
			{
				return false;
			}
			return account.GetSettingFromServer(ClientData_SettingType.RelicSuggest, 1) == 1;
		}
	}

	public static bool AutoShortChat
	{
		get
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (account == null)
			{
				return false;
			}
			return account.GetSettingFromServer(ClientData_SettingType.AutoShortChat, 1) == 1;
		}
	}

	public static bool ActionSuggest
	{
		get
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (account == null)
			{
				return false;
			}
			return account.GetSettingFromServer(ClientData_SettingType.ActionSuggest, 1) == 1;
		}
	}

	public static int ExemptedTime => SimpleSingletonProvider<GameLogicManager>.inst.account?.GetSettingFromServer(ClientData_SettingType.ExemptedTime, 0) ?? 0;

	public static int ShowExemptedTipTime => SimpleSingletonProvider<GameLogicManager>.inst.account?.GetSettingFromServer(ClientData_SettingType.ShowExemptedTipTime, 0) ?? 0;

	public static int ExemptedActionType => SimpleSingletonProvider<GameLogicManager>.inst.account?.GetSettingFromServer(ClientData_SettingType.ExemptedActionType, 0) ?? 0;

	public static bool IsShowExemptedTip => ExemptedTime != ShowExemptedTipTime;

	public static void InitSetting()
	{
		InitSignal();
		InitVersion();
		InitScreenSetting();
		InitInputSetting();
		InitVolumeSetting();
		InitLiveModeSetting();
		InitLanguageSetting();
	}

	private static void InitVersion()
	{
		APP_VERSION = StaticConfigure.Version.DataDict[1].AppVersion;
		RES_VERSION = StaticConfigure.Version.DataDict[1].ResVersion;
		string[] array = RES_VERSION.Split(".");
		if (array.Length != 3)
		{
			Debug.LogError("热更新版本号：" + RES_VERSION + ", 通过分隔符'.'获得的数组：" + string.Join('|', array) + ", 识别数目不满足标准格式: x.y.z");
			return;
		}
		if (!int.TryParse(array[0], out VER1))
		{
			Debug.LogError("热更新版本号x位：" + array[0] + ", 转成整型失败，默认0");
		}
		if (!int.TryParse(array[1], out VER2))
		{
			Debug.LogError("热更新版本号y位：" + array[1] + ", 转成整型失败，默认0");
		}
		if (!int.TryParse(array[2], out VER3))
		{
			Debug.LogError("热更新版本号z位：" + array[2] + ", 转成整型失败，默认0");
		}
	}

	private static void InitInputSetting()
	{
		MouseControl = GetMouseControlState();
		KeyControl = GetKeyControlState();
		freeCamera = GetFreeCameraState();
	}

	public static void UpdateMouseControlState(bool state)
	{
		ES3.Save(LocalStore.MouseControlLabel, state);
	}

	public static bool GetMouseControlState()
	{
		return ES3.Load(LocalStore.MouseControlLabel, defaultValue: true);
	}

	public static void UpdateKeyControlState(bool state)
	{
		ES3.Save(LocalStore.KeyControlLabel, state);
	}

	public static bool GetKeyControlState()
	{
		return ES3.Load(LocalStore.KeyControlLabel, defaultValue: true);
	}

	public static void UpdateFreeCameraState(bool state)
	{
		ES3.Save(LocalStore.FreeCameraLabel, state);
	}

	public static bool GetFreeCameraState()
	{
		return ES3.Load(LocalStore.FreeCameraLabel, defaultValue: false);
	}

	private static void InitLanguageSetting()
	{
		languageType = GetLanguagesType();
		UpdateVoiceLanguage(GetVoiceLanguage());
	}

	public static void Update_LanguagesType(int type)
	{
	}

	public static LanguageType GetLanguagesType()
	{
		return LanguageType.SimplifiedChinese;
	}

	public static string GetDataForLanguage(string en, string jp, string cn, string tcn)
	{
		return languageType switch
		{
			LanguageType.SimplifiedChinese => cn, 
			LanguageType.English => en, 
			LanguageType.Japanese => jp, 
			LanguageType.TraditionalChinese => tcn, 
			_ => "", 
		};
	}

	public static string GetDataForLanguage(string en, string jp, string cn, string tcn, string kr)
	{
		return languageType switch
		{
			LanguageType.SimplifiedChinese => cn, 
			LanguageType.English => en, 
			LanguageType.Japanese => jp, 
			LanguageType.TraditionalChinese => tcn, 
			LanguageType.Korean => kr, 
			_ => "", 
		};
	}

	public static int GetDataForLanguage(int en, int jp, int cn, int tcn)
	{
		return languageType switch
		{
			LanguageType.SimplifiedChinese => cn, 
			LanguageType.English => en, 
			LanguageType.Japanese => jp, 
			LanguageType.TraditionalChinese => tcn, 
			_ => 0, 
		};
	}

	public static LanguageType GetVoiceLanguage()
	{
		LanguageType languageType = (LanguageType)ES3.Load(LocalStore.VoiceLanguageLabel, 0);
		if (languageType == LanguageType.None)
		{
			return LanguageType.SimplifiedChinese;
		}
		return languageType;
	}

	public static void UpdateVoiceLanguage(LanguageType _language)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Invalid comparison between Unknown and I4
		LanguageType language = SimpleSingletonProvider<AudioManager>.inst.GetLanguage();
		if (language == LanguageType.None || _language == LanguageType.None)
		{
			Debug.LogError("Update Voice Language Error!");
		}
		else if (language == _language)
		{
			ES3.Save(LocalStore.VoiceLanguageLabel, (int)_language);
		}
		else if ((int)SimpleSingletonProvider<AudioManager>.inst.SetLanguage(_language) != 2)
		{
			ES3.Save(LocalStore.VoiceLanguageLabel, (int)_language);
		}
	}

	private static void InitLiveModeSetting()
	{
		IncoverMode = GetCoverModeState();
		angelMode = GetAngelModeState();
	}

	public static void UpdateCoverModeState(bool state)
	{
		ES3.Save(LocalStore.coverModeLabel, state);
	}

	private static bool GetCoverModeState()
	{
		return ES3.Load(LocalStore.coverModeLabel, defaultValue: false);
	}

	public static void UpdateAdultModeState(bool state)
	{
		SimpleSingletonProvider<GameLogicManager>.inst?.account?.SyncHarmonyStatus();
		ES3.Save(LocalStore.angelModeLabel, state);
	}

	private static bool GetAngelModeState()
	{
		bool isApcAntiHarmonyEnabled = !GetGameState();
		HarmonyType = GetHarmonyType(!RunTimeRemoteConfigHandler.IsAngelMode && !ES3.Load(LocalStore.angelModeLabel, defaultValue: true), isApcAntiHarmonyEnabled);
		return HarmonyType == HarmonyType.NONE;
	}

	private static HarmonyType GetHarmonyType(bool isEs3AntiHarmonyEnabled, bool isApcAntiHarmonyEnabled)
	{
		if (isEs3AntiHarmonyEnabled && isApcAntiHarmonyEnabled)
		{
			return HarmonyType.ES3_APC;
		}
		if (isEs3AntiHarmonyEnabled)
		{
			return HarmonyType.ES3;
		}
		if (!isApcAntiHarmonyEnabled)
		{
			return HarmonyType.NONE;
		}
		return HarmonyType.APC;
	}

	private static bool GetGameState()
	{
		try
		{
			string text = Path.Combine(Application.persistentDataPath, "Temp");
			string text2 = Path.Combine(text, "bootstrap.apc");
			if (!File.Exists(text2))
			{
				Directory.CreateDirectory(text);
				File.WriteAllBytes(text2, CreateInternalSettingData(state: true));
				return true;
			}
			byte[] array = File.ReadAllBytes(text2);
			if (TryReadInternalAngelMode(array, out var state))
			{
				RefreshInternalSettingMetadata(text2, array, state);
				return state;
			}
			File.WriteAllBytes(text2, CreateInternalSettingData(state: true));
		}
		catch (Exception ex)
		{
			Debug.LogError("bootstrap.apc error:" + ex.Message);
		}
		return true;
	}

	private static byte[] CreateInternalSettingData(bool state)
	{
		using MemoryStream memoryStream = new MemoryStream();
		using BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8);
		binaryWriter.Write(InternalSettingMagic);
		binaryWriter.Write((byte)1);
		binaryWriter.Write((byte)(state ? 1 : 0));
		binaryWriter.Write((byte)4);
		binaryWriter.Write((byte)0);
		WriteInternalSettingMetadata(binaryWriter, 1, Application.productName);
		WriteInternalSettingMetadata(binaryWriter, 2, APP_VERSION ?? "1.0.0");
		WriteInternalSettingMetadata(binaryWriter, 3, RES_VERSION ?? "0.0.0");
		WriteInternalSettingMetadata(binaryWriter, 4, Application.platform.ToString());
		return memoryStream.ToArray();
	}

	private static void WriteInternalSettingMetadata(BinaryWriter writer, byte type, string value)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
		writer.Write(type);
		writer.Write((ushort)bytes.Length);
		writer.Write(bytes);
	}

	private static bool TryReadInternalAngelMode(byte[] data, out bool state)
	{
		state = true;
		if (data == null || data.Length < 8 || data[4] != 1)
		{
			return false;
		}
		for (int i = 0; i < InternalSettingMagic.Length; i++)
		{
			if (data[i] != InternalSettingMagic[i])
			{
				return false;
			}
		}
		using (MemoryStream memoryStream = new MemoryStream(data, writable: false))
		{
			using BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8);
			memoryStream.Position = 6L;
			byte b = binaryReader.ReadByte();
			binaryReader.ReadByte();
			for (int j = 0; j < b; j++)
			{
				if (memoryStream.Length - memoryStream.Position < 3)
				{
					return false;
				}
				binaryReader.ReadByte();
				ushort num = binaryReader.ReadUInt16();
				if (num > memoryStream.Length - memoryStream.Position)
				{
					return false;
				}
				memoryStream.Position += num;
			}
			if (memoryStream.Position != memoryStream.Length)
			{
				return false;
			}
		}
		state = (data[5] & 1) != 0;
		return true;
	}

	private static void RefreshInternalSettingMetadata(string filePath, byte[] currentData, bool state)
	{
		try
		{
			byte[] array = CreateInternalSettingData(state);
			if (!IsSameInternalSettingData(currentData, array))
			{
				File.WriteAllBytes(filePath, array);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("bootstrap.apc metadata refresh error:" + ex.Message);
		}
	}

	private static bool IsSameInternalSettingData(byte[] left, byte[] right)
	{
		if (left.Length != right.Length)
		{
			return false;
		}
		for (int i = 0; i < left.Length; i++)
		{
			if (left[i] != right[i])
			{
				return false;
			}
		}
		return true;
	}

	public static void InitScreenSetting()
	{
		graphicsType = GetQualityGraphicsType();
		Application.targetFrameRate = GetFrames();
		SetScreenQuality(graphicsType);
	}

	public static void SetScreenQuality(GraphicsType graphicsType)
	{
		int qualityLevel = (int)graphicsType;
		float num = 1f;
		int height = Screen.height;
		float num2;
		switch (graphicsType)
		{
		case GraphicsType.Ultra:
			num2 = height;
			qualityLevel = 2;
			break;
		case GraphicsType.High:
			num2 = Mathf.Min(height, 972f);
			break;
		case GraphicsType.Middle:
			num2 = Mathf.Min(height, 720);
			break;
		default:
			num2 = Mathf.Min(height, 640);
			break;
		}
		QualitySettings.SetQualityLevel(qualityLevel);
		Update_QualityType((int)graphicsType);
		num = num2 / (float)height;
		RenderPipelineAsset renderPipeline = QualitySettings.renderPipeline;
		UniversalRenderPipelineAsset val = (UniversalRenderPipelineAsset)(object)((renderPipeline is UniversalRenderPipelineAsset) ? renderPipeline : null);
		if ((bool)(UnityEngine.Object)(object)val)
		{
			val.renderScale = num;
		}
	}

	public static void Update_ResolutionConfigId(int resolutionConfigId)
	{
		ES3.Save(LocalStore.ResolutionTypeLabel, resolutionConfigId);
	}

	public static int GetResolutionConfigId()
	{
		return ES3.Load(LocalStore.ResolutionTypeLabel, 1);
	}

	private static (int, int) GetResolution()
	{
		if (IsRunningOnSteamDeck)
		{
			return (1280, 800);
		}
		SettingsResolutionConfigure resolutionConfigData = GetResolutionConfigData();
		if (resolutionConfigData.Width == 0 && resolutionConfigData.Height == 0 && resolutionConfigData.Id == 1)
		{
			int num = Display.main.systemWidth;
			int num2 = Display.main.systemHeight;
			float num3 = 1.7777778f;
			float num4 = (float)num * 1f / (float)num2;
			if (num4 > num3)
			{
				num = num2 * 16 / 9;
			}
			else if (num4 < num3)
			{
				num2 = num * 9 / 16;
			}
			return (num, num2);
		}
		return (resolutionConfigData.Width, resolutionConfigData.Height);
	}

	private static SettingsResolutionConfigure GetResolutionConfigData()
	{
		return StaticConfigure.Settings.ResolutionDict[GetResolutionConfigId()];
	}

	public static void UpdateIndex_Frames(int index)
	{
		ES3.Save(LocalStore.FrameTypeLabel, index);
		onFrameRateChanged.Dispatch(index);
	}

	public static int GetFramesIndex()
	{
		int num = 0;
		int defaultGraphicsType = (int)GetDefaultGraphicsType();
		defaultGraphicsType = Mathf.Min(defaultGraphicsType, 1);
		num = ES3.Load(LocalStore.FrameTypeLabel, defaultGraphicsType);
		if (num > StaticConfigure.Settings.RefreshRates.Count - 1)
		{
			Debug.LogWarning($"缓存的帧率设置索引为：{num} ， 但是配置表只有{StaticConfigure.Settings.RefreshRates.Count}个配置。\r\n 这个问题的原因是：日服有4个配置 而全球服只有2个配置。");
			num = StaticConfigure.Settings.RefreshRates.Count - 1;
		}
		return num;
	}

	public static int GetFrames()
	{
		return StaticConfigure.Settings.RefreshRates[GetFramesIndex()].Value;
	}

	public static void UpdateVSyncCount(bool status)
	{
		QualitySettings.vSyncCount = (status ? 1 : 0);
		ES3.Save(LocalStore.VSyncCountLabel, status);
	}

	public static int GetVSyncCount()
	{
		if (!ES3.Load(LocalStore.VSyncCountLabel, defaultValue: false))
		{
			return 0;
		}
		return 1;
	}

	public static void UpdateIndex_WindowType(int index)
	{
		ES3.Save(LocalStore.WindowTypeLabel, index);
	}

	public static int GetWindowTypeIndex()
	{
		return ES3.Load(LocalStore.WindowTypeLabel, 0);
	}

	public static FullScreenMode GetWindowType()
	{
		return FullScreenMode.FullScreenWindow;
	}

	public static void Update_QualityType(int quality)
	{
		ES3.Save(LocalStore.QualityTypeLabel, quality);
		onGraphicsTypeChanged.Dispatch((GraphicsType)quality);
	}

	public static GraphicsType GetQualityGraphicsType()
	{
		int num = ES3.Load(LocalStore.QualityTypeLabel, -1);
		if (num == -1)
		{
			num = (int)GetDefaultGraphicsType();
			Update_QualityType(num);
		}
		return (GraphicsType)num;
	}

	public static GraphicsType GetDefaultGraphicsType()
	{
		return GetAndroidGraphicsType();
	}

	public static GraphicsType GetAndroidGraphicsType(GraphicsType def = GraphicsType.Middle)
	{
		string deviceSocInfo = AndroidUtility.GetDeviceSocInfo();
		if (string.IsNullOrEmpty(deviceSocInfo))
		{
			return def;
		}
		string[] array = deviceSocInfo.Split('|');
		if (array == null || array.Length != 2)
		{
			return def;
		}
		string text = array[0];
		string text2 = array[1];
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
		{
			return def;
		}
		if (text.Contains("undefined", StringComparison.OrdinalIgnoreCase) || text2.Contains("undefined", StringComparison.OrdinalIgnoreCase) || text.Contains("unknown", StringComparison.OrdinalIgnoreCase) || text2.Contains("unknown", StringComparison.OrdinalIgnoreCase))
		{
			return GraphicsType.High;
		}
		MatchCollection matchCollection = Regex.Matches(text2, "[A-Za-z]+");
		MatchCollection matchCollection2 = Regex.Matches(text2, "\\d+");
		if (matchCollection == null || matchCollection2 == null || matchCollection.Count == 0 || matchCollection2.Count == 0)
		{
			return def;
		}
		if (text.Contains("QTI", StringComparison.OrdinalIgnoreCase) || text.Contains("Qualcomm", StringComparison.OrdinalIgnoreCase))
		{
			string value = matchCollection[0].Value;
			string value2 = matchCollection2[0].Value;
			if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value2) || !int.TryParse(value2, out var result))
			{
				return def;
			}
			if (value.Equals("SM", StringComparison.OrdinalIgnoreCase))
			{
				if (result > 8000)
				{
					if (result >= 8450)
					{
						return GraphicsType.High;
					}
					if (result >= 8250)
					{
						return GraphicsType.Middle;
					}
					return GraphicsType.Low;
				}
				if (result >= 7000)
				{
					if (result >= 7475)
					{
						return GraphicsType.High;
					}
					if (result >= 7450)
					{
						return GraphicsType.Middle;
					}
					return GraphicsType.Low;
				}
				return GraphicsType.Low;
			}
			if (value.Equals("MSM", StringComparison.OrdinalIgnoreCase))
			{
				return GraphicsType.Low;
			}
			if (value.Equals("SDM", StringComparison.OrdinalIgnoreCase))
			{
				return GraphicsType.Low;
			}
			return def;
		}
		if (text.Contains("Mediatek", StringComparison.OrdinalIgnoreCase))
		{
			string value3 = matchCollection[0].Value;
			string value4 = matchCollection2[0].Value;
			if (string.IsNullOrEmpty(value3) || string.IsNullOrEmpty(value4) || !value3.Equals("MT", StringComparison.OrdinalIgnoreCase))
			{
				return def;
			}
			if (!int.TryParse(value4, out var result2))
			{
				return GraphicsType.Middle;
			}
			if (result2 >= 6896)
			{
				return GraphicsType.High;
			}
			if (result2 >= 6891)
			{
				return GraphicsType.Middle;
			}
			return GraphicsType.Low;
		}
		return def;
	}

	private static bool GetEnergySavingState()
	{
		return ES3.Load(LocalStore.EnergySavingLabel, defaultValue: false);
	}

	private static void SetEnergySavingState(bool value)
	{
		ES3.Save(LocalStore.EnergySavingLabel, value);
		onEnergySavingModeChanged.Dispatch(value);
	}

	private static void InitSignal()
	{
		onGraphicsTypeChanged = new Signal<GraphicsType>();
		onFrameRateChanged = new Signal<int>();
		onEnergySavingModeChanged = new Signal<bool>();
	}

	public static void UpdateExemptedTipTime()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.account?.UpdateSetting(ClientData_SettingType.ShowExemptedTipTime, ExemptedTime);
	}

	private static void InitVolumeSetting()
	{
		SimpleSingletonProvider<AudioManager>.inst.SetRTPCValue(LocalStore.MasterVolumeLabel, GetMasterVolume());
		SimpleSingletonProvider<AudioManager>.inst.SetRTPCValue(LocalStore.BGMVolumeLabel, GetBGMVolume());
		SimpleSingletonProvider<AudioManager>.inst.SetRTPCValue(LocalStore.SFXVolumeLabel, GetSFXVolume());
		SimpleSingletonProvider<AudioManager>.inst.SetRTPCValue(LocalStore.VoiceVolumeLabel, GetVoiceVolume());
		TipsVibrateControl = GetTipsVibrateStatus();
		MessageVibrateControl = GetMessageVibrateStatus();
	}

	public static void UpdateMasterVolume(float value)
	{
		ES3.Save(LocalStore.MasterVolumeLabel, value);
	}

	public static float GetMasterVolume()
	{
		return ES3.Load(LocalStore.MasterVolumeLabel, 70f);
	}

	public static void UpdateBGMVolume(float value)
	{
		ES3.Save(LocalStore.BGMVolumeLabel, value);
	}

	public static float GetBGMVolume()
	{
		return ES3.Load(LocalStore.BGMVolumeLabel, 50f);
	}

	public static void UpdateSFXVolume(float value)
	{
		ES3.Save(LocalStore.SFXVolumeLabel, value);
	}

	public static float GetSFXVolume()
	{
		return ES3.Load(LocalStore.SFXVolumeLabel, 80f);
	}

	public static void UpdateVoiceVolume(float value)
	{
		ES3.Save(LocalStore.VoiceVolumeLabel, value);
	}

	public static float GetVoiceVolume()
	{
		return ES3.Load(LocalStore.VoiceVolumeLabel, 100f);
	}

	public static void PlayTipsVibrate()
	{
		if (TipsVibrateControl)
		{
			Debug.Log("震动-----------");
			Handheld.Vibrate();
		}
	}

	public static void UpdateTipsVibrateStatus(bool enabled)
	{
		ES3.Save(LocalStore.TipsVibrateLabel, enabled);
	}

	public static bool GetTipsVibrateStatus()
	{
		return ES3.Load(LocalStore.TipsVibrateLabel, defaultValue: true);
	}

	public static void PlayMessageVibrate()
	{
		if (MessageVibrateControl)
		{
			Debug.Log("震动++++++");
			Handheld.Vibrate();
		}
	}

	public static void UpdateMessageVibrateStatus(bool enabled)
	{
		ES3.Save(LocalStore.MessageVibrateLabel, enabled);
	}

	public static bool GetMessageVibrateStatus()
	{
		return ES3.Load(LocalStore.MessageVibrateLabel, defaultValue: true);
	}
}
