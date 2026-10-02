using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class AccountLogic : IRPCSync
{
	public bool online;

	private IAccount accountData;

	public AccountSignal signal = new AccountSignal();

	private Player player;

	private readonly Dictionary<int, int> LocalGuideData = new Dictionary<int, int>();

	private readonly Dictionary<int, int> LocalDateChangeData = new Dictionary<int, int>();

	private readonly Dictionary<int, SongData> songDataDict = new Dictionary<int, SongData>();

	private readonly Dictionary<int, int> _settingDict = new Dictionary<int, int>();

	private readonly List<int> _newItemList = new List<int>();

	private readonly List<int> _campaignTutorialData = new List<int>();

	private MapField<int, int> _passMapList;

	public int CreditScore = 100;

	public const int MaxCreditScore = 100;

	public const int MinCreditScore = 0;

	private PlayerCareer playerCareer;

	public Dictionary<int, SongData> SongDataDict => songDataDict;

	public void InitFromServer(AccountInfo account, Player player, ConnectS2C connectResponse = null)
	{
		online = false;
		accountData = new PCAccountBase(account, player);
		this.player = player;
		LocalGuideData.Clear();
		_newItemList.Clear();
		MapField<int, int> mapField = player?.ClientData?.GuideData;
		if (mapField != null && mapField.Count > 0)
		{
			foreach (KeyValuePair<int, int> item in mapField)
			{
				LocalGuideData.TryAdd(item.Key, item.Value);
			}
		}
		MapField<int, int> mapField2 = player?.ClientData?.DateChangeData;
		if (mapField2 != null && mapField2.Count > 0)
		{
			foreach (KeyValuePair<int, int> item2 in mapField2)
			{
				LocalDateChangeData.TryAdd(item2.Key, item2.Value);
			}
		}
		MapField<int, SongData> mapField3 = player?.ClientData?.SongData;
		if (mapField3 != null && mapField3.Count > 0)
		{
			foreach (KeyValuePair<int, SongData> item3 in mapField3)
			{
				songDataDict.TryAdd(item3.Key, item3.Value);
			}
		}
		MapField<int, int> mapField4 = player?.ClientData?.SettingData;
		if (mapField4 != null && mapField4.Count > 0)
		{
			foreach (KeyValuePair<int, int> item4 in mapField4)
			{
				_settingDict.TryAdd(item4.Key, item4.Value);
			}
		}
		_newItemList.Clear();
		RepeatedField<int> repeatedField = player?.ClientData?.NewItemData;
		if (repeatedField != null && repeatedField.Count > 0)
		{
			foreach (int item5 in repeatedField)
			{
				_newItemList.Add(item5);
			}
		}
		_campaignTutorialData.Clear();
		RepeatedField<int> repeatedField2 = player?.ClientData?.CampaignTutorialData;
		if (repeatedField2 != null && repeatedField2.Count > 0)
		{
			foreach (int item6 in repeatedField2)
			{
				_campaignTutorialData.Add(item6);
			}
		}
		if (player != null)
		{
			_passMapList = player.WinMap;
		}
		if (connectResponse != null)
		{
			try
			{
				LoginServiceHelper.HandleBnSdkLoginResponse(connectResponse);
				Debug.Log("[SDK] BnSdk登录响应处理完成");
			}
			catch (Exception ex)
			{
				Debug.LogError("[SDK] 处理BnSdk登录响应失败: " + ex.Message);
			}
		}
		RoLeInfoBase.UploadRoleInfoToSDK();
		SyncHarmonyStatus();
		if (player?.CreditInfo != null)
		{
			CreditScore = Mathf.Clamp(100 - player.CreditInfo.DeductScore, 0, 100);
			CreditInfo creditInfo = player?.CreditInfo;
			UpdateExpmpteParameter(creditInfo.LastExemptedActionType, creditInfo.LastExemptedActionType);
		}
		else
		{
			CreditScore = 100;
		}
	}

	public IAccount GetAccount()
	{
		return accountData;
	}

	public string GetName()
	{
		return accountData.GetAccountName();
	}

	public long GetPlayerID()
	{
		if (accountData == null)
		{
			return 0L;
		}
		return accountData.GetPlayerInfo().Id;
	}

	public ulong GetAccountId()
	{
		if (accountData == null || !(accountData is AccountBase<ulong> accountBase))
		{
			return 0uL;
		}
		return accountBase.AccountID;
	}

	public Player GetPlayerInfo()
	{
		return accountData.GetPlayerInfo();
	}

	public bool IsSelf(long playerId)
	{
		if (accountData == null)
		{
			Debug.LogError("当前玩家的账户数据是空，请检查");
			return false;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.watch.IsWatcher(playerId))
		{
			return false;
		}
		return accountData.IsSelf(playerId);
	}

	public void SetOnline()
	{
		online = true;
	}

	public bool IsPassMap(int MapId)
	{
		if (_passMapList == null)
		{
			return false;
		}
		return _passMapList.ContainsKey(MapId);
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeExpS2C.OnChangeExpS2CServerCallBackAsync = OnChangeExpS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SetShowPlayerS2C.OnSetShowPlayerS2CServerCallBackAsync = OnSetShowPlayerS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GetShowPlayerS2C.OnGetShowPlayerS2CServerCallBackAsync = OnGetShowPlayerS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GetPlayerFightRecordS2C.OnGetPlayerFightRecordS2CServerCallBackAsync = OnGetPlayerFightRecordS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ClientDataUploadS2C.OnClientDataUploadS2CServerCallBackAsync = OnClientDataUploadS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.AccuseS2C.OnAccuseS2CServerCallBackAsync = OnAccuseS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeNameS2C.OnChangeNameS2CServerCallBackAsync = OnChangeNameS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SurrenderPunishS2C.OnSurrenderPunishS2CServerCallBackAsync = OnSurrenderPunishS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.UnLockDifficultyS2C.OnUnLockDifficultyS2CServerCallBackAsync = OnUnLockDifficultyS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.GamePassMapSuccessS2C.OnGamePassMapSuccessS2CServerCallBackAsync = OnGamePassMapSuccessS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.ClientHarmonyS2C.OnClientHarmonyS2CServerCallBackAsync = OnClientHarmonyS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.SyncPlayerCreditInfoS2C.OnSyncPlayerCreditInfoS2CServerCallBackAsync = OnSyncPlayerCreditInfoS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeExpS2C.OnChangeExpS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SetShowPlayerS2C.OnSetShowPlayerS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GetShowPlayerS2C.OnGetShowPlayerS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GetPlayerFightRecordS2C.OnGetPlayerFightRecordS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ClientDataUploadS2C.OnClientDataUploadS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.AccuseS2C.OnAccuseS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.AgeVerifyS2C.OnAgeVerifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PayInfoChangeS2C.OnPayInfoChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeNameS2C.OnChangeNameS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SurrenderPunishS2C.OnSurrenderPunishS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.UnLockDifficultyS2C.OnUnLockDifficultyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GamePassMapSuccessS2C.OnGamePassMapSuccessS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ClientHarmonyS2C.OnClientHarmonyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SyncPlayerCreditInfoS2C.OnSyncPlayerCreditInfoS2CServerCallBackAsync = null;
	}

	private async UniTask OnGamePassMapSuccessS2CServerCallBackAsync(GamePassMapSuccessS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			_passMapList = model.MapIds;
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnChangeExpS2CServerCallBack(ChangeExpS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			accountData.UpdateLevel(model.Level, model.Exp);
			signal.levelChanged.Dispatch();
			RoLeInfoBase.UpDateRoleInfoToSDK();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestSetShowPlayerC2S(PlayerCareer data)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.SetShowPlayerC2S.SetShowPlayerC2SCall(new SetShowPlayerC2S
		{
			ShowData = new ShowPlayer
			{
				StandingPainting = data.StandingPainting,
				AchieveId = { (IEnumerable<int>)data.acheveIds },
				IsShowData = data.isShowData,
				IsShowFight = data.isShowFight
			}
		});
	}

	private async UniTask OnSetShowPlayerS2CServerCallBack(SetShowPlayerS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public void RequestGetShowPlayerC2S(long playerID, string nick, int lv, string headIcon, (string, bool) labelData, System.Action complete)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GetShowPlayerC2S.GetShowPlayerC2SCall(new GetShowPlayerC2S
		{
			PlayerId = playerID
		}).OnFinished.AddOnce(delegate(RPCAsyncResult _)
		{
			complete?.Invoke();
			if (_.errId == 0)
			{
				ShowAccountInfo(nick, lv, headIcon, labelData);
			}
		});
	}

	private async void ShowAccountInfo(string nick, int lv, string headIcon, (string, bool) labelData)
	{
		playerCareer.UpdateBaseInfo(nick, lv, headIcon, labelData);
		await SimpleSingletonProvider<UIManager>.inst.AccountInfo.TryShowAsync(playerCareer);
	}

	private async UniTask OnGetShowPlayerS2CServerCallBack(GetShowPlayerS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			if (playerCareer == null)
			{
				playerCareer = new PlayerCareer();
			}
			playerCareer.UpdateData(model);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestGetPlayerFightRecordC2S(long playerId, int index)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.GetPlayerFightRecordC2S.GetPlayerFightRecordC2SCall(new GetPlayerFightRecordC2S
		{
			Index = index,
			PlayerId = playerId
		});
	}

	private async UniTask OnGetPlayerFightRecordS2CServerCallBack(GetPlayerFightRecordS2C model, int errid, bool isdispatch)
	{
		if (errid == 0 && playerCareer != null)
		{
			playerCareer.UpdateFightRecordDetail(model.RecordData);
			await UniTask.CompletedTask;
		}
	}

	public void RequestAccuseC2S(long playerId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.AccuseC2S.AccuseC2SCall(new AccuseC2S
		{
			PlayerId = playerId
		});
	}

	private async UniTask OnAccuseS2CServerCallBack(AccuseS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnUnLockDifficultyS2CServerCallBackAsync(UnLockDifficultyS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			Player playerInfo = GetPlayerInfo();
			if (playerInfo != null)
			{
				playerInfo.UnLockDifficulty = model.Difficulty;
				await UniTask.CompletedTask;
			}
		}
	}

	public RPCAsyncResult RequestClientDataUploadS2C()
	{
		return RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.None);
	}

	public RPCAsyncResult RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData ops)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ClientDataUploadC2S.ClientDataUploadC2SCall(new ClientDataUploadC2S
		{
			OpsData = ops,
			Data = BuildClientDataForOps(ops)
		});
	}

	private ClientData BuildClientDataForOps(ClientDataUploadC2S.Types.OpsData ops)
	{
		ClientData clientData = new ClientData();
		switch (ops)
		{
		case ClientDataUploadC2S.Types.OpsData.GuideData:
			clientData.GuideData.Add(LocalGuideData);
			break;
		case ClientDataUploadC2S.Types.OpsData.DateChangeData:
			clientData.DateChangeData.Add(LocalDateChangeData);
			break;
		case ClientDataUploadC2S.Types.OpsData.SongData:
			clientData.SongData.Add(songDataDict);
			break;
		case ClientDataUploadC2S.Types.OpsData.SettingData:
			clientData.SettingData.Add(_settingDict);
			break;
		case ClientDataUploadC2S.Types.OpsData.NewItemData:
			clientData.NewItemData.Add(_newItemList);
			break;
		case ClientDataUploadC2S.Types.OpsData.CampaignTutorialData:
			clientData.CampaignTutorialData.Add(_campaignTutorialData);
			break;
		case ClientDataUploadC2S.Types.OpsData.StarExpression:
			clientData.StarExpression.Add(GetStarExpressionSnapshot());
			break;
		case ClientDataUploadC2S.Types.OpsData.TopExpression:
			clientData.TopExpression.Add(GetTopExpressionSnapshot());
			break;
		default:
			clientData.GuideData.Add(LocalGuideData);
			clientData.DateChangeData.Add(LocalDateChangeData);
			clientData.SongData.Add(songDataDict);
			clientData.SettingData.Add(_settingDict);
			clientData.NewItemData.Add(_newItemList);
			clientData.CampaignTutorialData.Add(_campaignTutorialData);
			clientData.StarExpression.Add(GetStarExpressionSnapshot());
			clientData.TopExpression.Add(GetTopExpressionSnapshot());
			break;
		}
		return clientData;
	}

	private List<int> GetStarExpressionSnapshot()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.communicate?.GetStarExpressionSnapshot() ?? new List<int>();
	}

	private List<int> GetTopExpressionSnapshot()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.communicate?.GetTopExpressionSnapshot() ?? new List<int>();
	}

	private async UniTask OnClientDataUploadS2CServerCallBack(ClientDataUploadS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public void OnTypeToRemoveNewItemData(ItemType itemType)
	{
		_newItemList.RemoveAll((int itemId) => itemId.GetItemInfoConfigure().ItemType == itemType);
		RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.NewItemData);
	}

	public void UpdateNewData(int itemId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItem(itemId) == null)
		{
			_newItemList.Remove(itemId);
			return;
		}
		ItemInfoConfigure itemInfoConfigure = itemId.GetItemInfoConfigure();
		if (((itemInfoConfigure != null && itemInfoConfigure.ItemType == ItemType.Chest) || (itemInfoConfigure != null && itemInfoConfigure.ItemType == ItemType.AccountBackground) || (itemInfoConfigure != null && itemInfoConfigure.ItemType == ItemType.AccountHeadShot) || (itemInfoConfigure != null && itemInfoConfigure.ItemType == ItemType.Dice) || (itemInfoConfigure != null && itemInfoConfigure.ItemType == ItemType.Effect) || (itemInfoConfigure != null && itemInfoConfigure.ItemType == ItemType.CardBack) || (itemInfoConfigure != null && itemInfoConfigure.ItemType == ItemType.Kv)) && !_newItemList.Contains(itemId))
		{
			_newItemList.Add(itemId);
		}
	}

	public void RemoveNewItemById(int itemId)
	{
		_newItemList.Remove(itemId);
	}

	public bool OnTypeGetNewData(ItemType itemType)
	{
		return _newItemList.Exists((int itemId) => itemId.GetItemInfoConfigure().ItemType == itemType);
	}

	public bool OnIDGetNewData(int itemId)
	{
		return _newItemList.Contains(itemId);
	}

	public void UpdateGuidedData(int guideType, int step)
	{
		if (!LocalGuideData.TryAdd(guideType, step))
		{
			LocalGuideData[guideType] = step;
		}
		LocalCache.UpdateTutorialCache(guideType, step);
		RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.GuideData);
	}

	public int GetGuidedRecord(int guideType)
	{
		if (!LocalGuideData.TryGetValue(guideType, out var value) && LocalCache.IsFinishTutorial(guideType))
		{
			value = 1;
			UpdateGuidedData(guideType, value);
		}
		return value;
	}

	public void UpdateDateData(DataChangeType _type, int date)
	{
		if (!LocalDateChangeData.TryAdd((int)_type, date))
		{
			LocalDateChangeData[(int)_type] = date;
		}
		RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.DateChangeData);
	}

	public int GetDateRecord(DataChangeType _type)
	{
		return LocalDateChangeData.GetValueOrDefault((int)_type, 0);
	}

	public int GetSettingFromServer(ClientData_SettingType type, int defaultValue)
	{
		return _settingDict.GetValueOrDefault((int)type, defaultValue);
	}

	public int GetSettingFromServer(int type, int defaultValue)
	{
		return _settingDict.GetValueOrDefault(type, defaultValue);
	}

	public RPCAsyncResult UpdateSetting(ClientData_SettingType type, int setting)
	{
		_settingDict[(int)type] = setting;
		return RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.SettingData);
	}

	public void UpdateSettingsNoResult(params (ClientData_SettingType, int)[] settings)
	{
		for (int i = 0; i < settings.Length; i++)
		{
			(ClientData_SettingType, int) tuple = settings[i];
			_settingDict[(int)tuple.Item1] = tuple.Item2;
		}
		RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.SettingData);
	}

	public RPCAsyncResult UpdateSetting(int type, int setting)
	{
		_settingDict[type] = setting;
		return RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.SettingData);
	}

	public void UpdateCampaignData(CampaignTutorialType _type)
	{
		if (!_campaignTutorialData.Contains((int)_type))
		{
			_campaignTutorialData.Add((int)_type);
			RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.CampaignTutorialData);
		}
	}

	public bool IsCampaignRecord(CampaignTutorialType _type)
	{
		return _campaignTutorialData.Contains((int)_type);
	}

	public void UpdateSongData(int songId, SongData data)
	{
		songDataDict[songId] = data;
	}

	public SongData GetSongData(int songId)
	{
		return songDataDict.GetValueOrDefault(songId);
	}

	private async UniTask OnSurrenderPunishS2CServerCallBack(SurrenderPunishS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			player.PunishmentTime = model.Time;
			await UniTask.CompletedTask;
		}
	}

	public bool StartGameLicense()
	{
		if (player.PunishmentTime == 0L)
		{
			return true;
		}
		TimeSpan timeSpan = (player.PunishmentTime * 1000).StampMillisecondsToDateTime() - MonoSingletonProvider<NetManager>.inst.ServerTime;
		if (timeSpan.TotalMilliseconds > 0.0)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(string.Format(1024.GetLocal(UIStringType.Message), Mathf.FloorToInt((float)timeSpan.TotalSeconds)));
			return false;
		}
		return true;
	}

	public bool CanChangeName()
	{
		DateTime dateTime = (SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().NextChangeNameTime * 1000).StampMillisecondsToDateTime();
		return MonoSingletonProvider<NetManager>.inst.ServerTime > dateTime;
	}

	public RPCAsyncResult ChangeName(string newName)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ChangeNameC2S.ChangeNameC2SCall(new ChangeNameC2S
		{
			Nick = newName
		});
	}

	private async UniTask OnChangeNameS2CServerCallBack(ChangeNameS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			signal.changeName.Dispatch(t: false);
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().NextChangeNameTime = model.NextChangeNameTime;
		player.Nick = model.PlayerName;
		accountData.SetAccountName(model.PlayerName);
		signal.changeName.Dispatch(t: true);
		await UniTask.CompletedTask;
	}

	public void SyncHarmonyStatus()
	{
		if (player != null)
		{
			int harmonyType = (int)GameSettings.HarmonyType;
			if (GameSettings.angelMode != player.IsHarmony || harmonyType != player.HarmonyType)
			{
				player.IsHarmony = GameSettings.angelMode;
				player.HarmonyType = harmonyType;
				RequestClientHarmonyC2S(GameSettings.angelMode, GameSettings.HarmonyType);
			}
		}
	}

	private void RequestClientHarmonyC2S(bool harmony, HarmonyType type)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ClientHarmonyC2S.ClientHarmonyC2SCall(new ClientHarmonyC2S
		{
			IsHarmony = harmony,
			HarmonyType = (int)type
		});
	}

	private async UniTask OnClientHarmonyS2CServerCallBackAsync(ClientHarmonyS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnSyncPlayerCreditInfoS2CServerCallBackAsync(SyncPlayerCreditInfoS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			CreditScore = Mathf.Clamp(100 - model.DeductScore, 0, 100);
			UpdateExpmpteParameter(model.LastExemptedActionType, model.LastExemptedActionType);
			await UniTask.CompletedTask;
		}
	}

	private void UpdateExpmpteParameter(long lastExemptedTime, int lastExemptedActionType)
	{
		if (lastExemptedTime != 0L)
		{
			int num = (int)(lastExemptedTime & 0xFFFFFFFFu);
			if (num != GameSettings.ExemptedTime)
			{
				UpdateSettingsNoResult((ClientData_SettingType.ExemptedTime, num), (ClientData_SettingType.ExemptedActionType, lastExemptedActionType));
			}
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HomePanel homePanel && homePanel.IsOpen())
			{
				SimpleSingletonProvider<UIManager>.inst.CreditWarning.ShowExemptedTip().Forget();
			}
		}
	}
}
