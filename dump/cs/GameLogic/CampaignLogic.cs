using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class CampaignLogic : IRPCSync
{
	public CampaignSignal signal = new CampaignSignal();

	private Dictionary<int, bool> _levelPassDict;

	public CampaignData campaignData;

	public readonly Dictionary<int, List<CampaignLevelConfigure>> chapterLevelsMap = new Dictionary<int, List<CampaignLevelConfigure>>();

	private const int CampaignPVETutorialLevelId = 102;

	public CampaignLevelConfigure CampaignLevel
	{
		get
		{
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (curRoomInfo == null)
			{
				return null;
			}
			if (!curRoomInfo.IsCampaign())
			{
				return null;
			}
			if (!StaticConfigure.Campaign.LevelDict.TryGetValue(curRoomInfo.info.LevelId, out var value))
			{
				Debug.LogError($"无法在StaticConfigure.Campaign.LevelDict找到LevelId：{curRoomInfo.info.LevelId}的数据");
				return null;
			}
			return value;
		}
	}

	public void TryInitCampaignData()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null && curRoomInfo.IsCampaign())
		{
			campaignData = new CampaignData(CampaignLevel);
		}
		else
		{
			campaignData = null;
		}
		signal.campaignCondition.Dispatch();
	}

	public void InitFromServer(Player player)
	{
		_levelPassDict = new Dictionary<int, bool>();
		foreach (KeyValuePair<int, bool> item in player.CampaignPass)
		{
			_levelPassDict.Add(item.Key, item.Value);
		}
		chapterLevelsMap.Clear();
		foreach (CampaignChapterConfigure chapter in StaticConfigure.Campaign.Chapters)
		{
			if (!chapterLevelsMap.ContainsKey(chapter.Id))
			{
				List<CampaignLevelConfigure> value = new List<CampaignLevelConfigure>(chapter.Levels.Count);
				chapterLevelsMap.Add(chapter.Id, value);
			}
			foreach (int level in chapter.Levels)
			{
				if (!StaticConfigure.Campaign.LevelDict.TryGetValue(level, out var value2))
				{
					Debug.LogError($"单人战役 ID={level}的关卡不存在！");
					return;
				}
				chapterLevelsMap[chapter.Id].Add(value2);
			}
		}
	}

	public void TryShowTutorial(long playerId, int actionId)
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo == null || !roomInfo.IsCampaign())
		{
			return;
		}
		AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
		if (roomInfo.info.LevelId == 102)
		{
			if (!account.IsSelf(playerId))
			{
				return;
			}
			switch (actionId)
			{
			case 5055:
				if (!account.IsCampaignRecord(CampaignTutorialType.HealTeam))
				{
					if (roomInfo.Round == 1)
					{
						account.UpdateCampaignData(CampaignTutorialType.HealTeam);
						SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(10211).Forget();
					}
				}
				else if (!account.IsCampaignRecord(CampaignTutorialType.PVEMission) && roomInfo.Round == 2)
				{
					account.UpdateCampaignData(CampaignTutorialType.PVEMission);
					SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(10213).Forget();
				}
				break;
			case 5215:
				if (!account.IsCampaignRecord(CampaignTutorialType.TransformGold))
				{
					account.UpdateCampaignData(CampaignTutorialType.TransformGold);
					SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(10212).Forget();
				}
				break;
			}
		}
		else if (actionId == 5077 && roomInfo.MapType == 5 && !account.IsSelf(playerId) && !account.IsCampaignRecord(CampaignTutorialType.PVPUpgrade))
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById != null && playerDataById.Property.level.Value == 2)
			{
				account.UpdateCampaignData(CampaignTutorialType.PVPUpgrade);
				SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(10311).Forget();
			}
		}
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.CampaignPassS2C.OnCampaignPassS2CServerCallBackAsync = OnCampaignPassS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.CampaignNotifyS2C.OnCampaignNotifyS2CServerCallBackAsync = OnCampaignNotifyS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.KillMessageS2C.OnKillMessageS2CServerCallBackAsync = OnKillMessageS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.CampaignPassS2C.OnCampaignPassS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.CampaignNotifyS2C.OnCampaignNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.KillMessageS2C.OnKillMessageS2CServerCallBackAsync = null;
	}

	public bool GetCampaignPass(int levelId)
	{
		return _levelPassDict.GetValueOrDefault(levelId, defaultValue: false);
	}

	private async UniTask OnCampaignPassS2CServerCallBackAsync(CampaignPassS2C model, int errId, bool isDispatch)
	{
		await UniTask.CompletedTask;
		if (errId != 0 || model == null)
		{
			return;
		}
		foreach (KeyValuePair<int, bool> item in model.LevelPass)
		{
			_levelPassDict[item.Key] = item.Value;
			signal.unlockLevel.Dispatch(item.Key, item.Value);
		}
	}

	private async UniTask OnCampaignNotifyS2CServerCallBackAsync(CampaignNotifyS2C model, int errId, bool isDispatch)
	{
		await UniTask.CompletedTask;
		if (errId == 0 && model != null)
		{
			campaignData?.UpdateCampaignTask(model.VictoryCondition);
			signal.campaignCondition.Dispatch();
		}
	}

	private async UniTask OnKillMessageS2CServerCallBack(KillMessageS2C model, int errId, bool isdispatch)
	{
		await UniTask.CompletedTask;
		if (errId == 0 && model != null)
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (account.IsSelf(model.AttackerId))
			{
				campaignData?.TriggerFirstKill();
			}
			if (account.IsSelf(model.DefenderId))
			{
				campaignData?.TriggerFirstDead();
			}
		}
	}

	public (string, bool) GetLevelStatus(int chapterId, MapModeType modeType)
	{
		if (!chapterLevelsMap.TryGetValue(chapterId, out var value))
		{
			return (null, false);
		}
		int num = 0;
		int num2 = 0;
		foreach (CampaignLevelConfigure item in value)
		{
			if (item.MapModeType == modeType)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.campaign.GetCampaignPass(item.Id))
				{
					num++;
				}
				num2++;
			}
		}
		return ($"{num} / {num2}", num == num2);
	}
}
