using System;
using System.Collections.Generic;
using AK.Wwise.Unity.WwiseAddressables;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public class AudioManager : SimpleSingletonProvider<AudioManager>
{
	private const string _CV_JAPANESE = "Japanese";

	private const string _CV_CHINESE = "Simplified Chinese";

	private readonly Dictionary<string, AsyncOperationHandle<WwiseAddressableSoundBank>> _cachedWwiseBank = new Dictionary<string, AsyncOperationHandle<WwiseAddressableSoundBank>>();

	protected override void InstanceInit()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		AkSoundEngine.SetCurrentLanguage("Japanese");
		Stage.inst.playSoundCallback = OnPlayUISound;
	}

	public void Init()
	{
	}

	private void OnPlayUISound(int eventID)
	{
		SendEvent(eventID, Stage.inst.gameObject);
	}

	public async UniTask LoadBank(int bankID)
	{
		if (!StaticConfigure.Audio.BankDict.TryGetValue(bankID, out var config))
		{
			if (bankID != 0L)
			{
				Debug.LogError($"未能在Audio.Bank表中找到id:{bankID}");
			}
			return;
		}
		if (!_cachedWwiseBank.TryGetValue(config.LoadKey, out var value))
		{
			value = await AddressableHelper.LoadAssetAsync<WwiseAddressableSoundBank>(config.LoadKey);
			_cachedWwiseBank.Add(config.LoadKey, value);
		}
		AkAddressableBankManager.Instance.LoadBank(value.Result, false, false, true, false);
	}

	public async UniTask LoadBank(string loadKey)
	{
		if (!_cachedWwiseBank.TryGetValue(loadKey, out var value))
		{
			value = await AddressableHelper.LoadAssetAsync<WwiseAddressableSoundBank>(loadKey);
			_cachedWwiseBank.Add(loadKey, value);
		}
		AkAddressableBankManager.Instance.LoadBank(value.Result, false, false, true, false);
	}

	public void UnloadBank(int bankID)
	{
		AsyncOperationHandle<WwiseAddressableSoundBank> value2;
		if (!StaticConfigure.Audio.BankDict.TryGetValue(bankID, out var value))
		{
			if (bankID != 0L)
			{
				Debug.LogError($"未能在Audio.Bank表中找到id:{bankID}");
			}
		}
		else if (value == null)
		{
			Debug.LogError("数据已经消失了，仍然尝试使用");
		}
		else if (_cachedWwiseBank.TryGetValue(value.LoadKey, out value2))
		{
			AkAddressableBankManager.Instance.UnloadBank(value2.Result, true, true);
			Addressables.Release(value2);
			_cachedWwiseBank.Remove(value.LoadKey);
		}
	}

	public void Suspend()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		AkSoundEngine.Suspend(false);
	}

	public void Wakeup()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		AkSoundEngine.WakeupFromSuspend();
		AkSoundEngine.RenderAudio();
	}

	public uint SendEvent(int configId, GameObject attached)
	{
		if (configId == 0)
		{
			return 0u;
		}
		uint result = 0u;
		if (!StaticConfigure.Audio.EventDict.TryGetValue(configId, out var value))
		{
			Debug.LogError($"未能在Audio.Event表中找到id:{configId}");
			return result;
		}
		if (!AudioEventValid(value.IsBlock))
		{
			return 0u;
		}
		return AkSoundEngine.PostEvent(value.EventID, attached);
	}

	public uint SendEvent(int configId, GameObject attached, AkCallbackType markerType, Action<int, int> finishedCallback)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0076: Expected I4, but got Unknown
		uint result = 0u;
		if (!StaticConfigure.Audio.EventDict.TryGetValue(configId, out var value))
		{
			Debug.LogError($"未能在Audio.Event表中找到id:{configId}");
			return result;
		}
		if (!AudioEventValid(value.IsBlock))
		{
			return 0u;
		}
		return AkSoundEngine.PostEvent(value.EventID, attached, (uint)(int)markerType, (EventCallback)delegate(object cookie, AkCallbackType type, AkCallbackInfo info)
		{
			finishedCallback?.Invoke((int)info.gameObjID, configId);
		}, (object)null);
	}

	public uint SendEvent(int configId, GameObject attached, uint markerType, Action<int, int> finishedCallback)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		uint result = 0u;
		if (!StaticConfigure.Audio.EventDict.TryGetValue(configId, out var value))
		{
			Debug.LogError($"未能在Audio.Event表中找到id:{configId}");
			return result;
		}
		if (!AudioEventValid(value.IsBlock))
		{
			return 0u;
		}
		return AkSoundEngine.PostEvent(value.EventID, attached, markerType, (EventCallback)delegate(object cookie, AkCallbackType type, AkCallbackInfo info)
		{
			finishedCallback?.Invoke((int)info.gameObjID, configId);
		}, (object)null);
	}

	public bool SetRTPCValue(uint rtpcID, float value)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		return (int)AkSoundEngine.SetRTPCValue(rtpcID, value) == 1;
	}

	public bool SetRTPCValue(string rtpcName, float value)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		return (int)AkSoundEngine.SetRTPCValue(rtpcName, value) == 1;
	}

	public float GetRTPCValue(uint rtpcID)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		int num = 1;
		float result = default(float);
		AkSoundEngine.GetRTPCValue(rtpcID, (GameObject)null, 0u, ref result, ref num);
		return result;
	}

	public float GetRTPCValue(string in_pszRtpcName)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		int num = 1;
		float result = default(float);
		AkSoundEngine.GetRTPCValue(in_pszRtpcName, (GameObject)null, 0u, ref result, ref num);
		return result;
	}

	public LanguageType GetLanguage()
	{
		string currentLanguage = AkSoundEngine.GetCurrentLanguage();
		if (!(currentLanguage == "Simplified Chinese"))
		{
			if (currentLanguage == "Japanese")
			{
				return LanguageType.Japanese;
			}
			return LanguageType.None;
		}
		return LanguageType.SimplifiedChinese;
	}

	public AKRESULT SetLanguage(LanguageType languageType)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return (AKRESULT)(languageType switch
		{
			LanguageType.SimplifiedChinese => AkSoundEngine.SetCurrentLanguage("Simplified Chinese"), 
			LanguageType.Japanese => AkSoundEngine.SetCurrentLanguage("Japanese"), 
			_ => 2, 
		});
	}

	public void SendSwitch(string switchGroup, string switchState, GameObject attached)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		AkSoundEngine.SetSwitch(switchGroup, switchState, attached);
	}

	public void StopPlayingBGM(uint playingId)
	{
		AkSoundEngine.StopPlayingID(playingId);
	}

	public void StopAll()
	{
		AkSoundEngine.StopAll();
	}

	private bool AudioEventValid(bool isBlock)
	{
		if (GameSettings.angelMode)
		{
			return !isBlock;
		}
		return true;
	}
}
