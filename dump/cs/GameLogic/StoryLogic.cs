using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class StoryLogic : IRPCSync
{
	public StorySignal signal = new StorySignal();

	private readonly List<long> WatchStoryPlayerIds = new List<long>();

	private long _storyActionSn;

	public void InitFromServer()
	{
		_storyActionSn = 0L;
	}

	public async UniTask OpenStoryByServer(party.model.Action action)
	{
		if (!WatchStoryPlayerIds.Contains(action.PlayerId))
		{
			WatchStoryPlayerIds.Add(action.PlayerId);
		}
		Debug.Log($"通知剧情：Sn:{action.Sn} PlayerId:{action.PlayerId}");
		if (_storyActionSn == action.Sn)
		{
			return;
		}
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData == null || selfPlayerData.player.Id != action.PlayerId)
		{
			return;
		}
		_storyActionSn = action.Sn;
		NotifyStoryC2S _storyServerData = ByteBuf.ReadObject<NotifyStoryC2S>(action.Data.ToByteArray());
		Debug.Log($"通知剧情：_storyServerData.Index:{_storyServerData.Index}");
		if (StaticConfigure.Story.StoryDict.TryGetValue(_storyServerData.Index, out var storyConfig))
		{
			if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager015 mapGimmickManager && await mapGimmickManager.TryShowArtifactByStoryId(storyConfig.ID))
			{
				return;
			}
			Debug.Log($"通知剧情：{SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId)}");
			if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
			{
				return;
			}
			StoryData storyData = await SimpleSingletonProvider<InternalAssetManager>.inst.GetStoryDataAsset(storyConfig.StoryData);
			if (storyData != null && storyData.Root != null)
			{
				await TryShowStory(storyData, delegate
				{
					RequestNotifyStoryC2S(_storyServerData.Index, action.Sn);
				});
			}
			else
			{
				RequestNotifyStoryC2S(_storyServerData.Index, action.Sn);
				Debug.LogError("无法通过key:" + storyConfig.StoryData + " 在资源组中找到相应的资产");
			}
		}
		else
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
			{
				RequestNotifyStoryC2S(_storyServerData.Index, action.Sn);
			}
			Debug.LogError($"无法通过id:{_storyServerData.Index} 在Story.StoryDict中找到相应的配置");
		}
	}

	public async UniTask TryShowStory(StoryData storyDataAsset, System.Action finishAction)
	{
		StoryWindow story = SimpleSingletonProvider<UIManager>.inst.story;
		if (story.CurrentStoryRoot != null && story.CurrentStoryRoot.Guid == storyDataAsset.Root.Guid)
		{
			Debug.LogWarning("当前正在显示剧情GUID:" + storyDataAsset.Root.Guid + "，不需要在重新打开");
			return;
		}
		Debug.Log("通知剧情：" + storyDataAsset.name);
		await SimpleSingletonProvider<UIManager>.inst.story.TryShowStory(storyDataAsset, finishAction);
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.NotifyStoryS2C.OnNotifyStoryS2CServerCallBackAsync = OnNotifyStoryS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.NotifyStoryS2C.OnNotifyStoryS2CServerCallBackAsync = null;
	}

	private RPCAsyncResult RequestNotifyStoryC2S(int storyId, long actionSn)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.NotifyStoryC2S.NotifyStoryC2SCall(new NotifyStoryC2S
		{
			Info = new ActionInfo
			{
				Sn = actionSn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Index = storyId
		});
	}

	private async UniTask OnNotifyStoryS2CServerCallBack(NotifyStoryS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
		{
			return;
		}
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (roomController.roomStateType != RoomStateType.RUNNING)
		{
			return;
		}
		if (WatchStoryPlayerIds.Contains(model.PlayerId))
		{
			SimpleSingletonProvider<UIManager>.inst.tips.HideMultiplePlayerThink(model.PlayerId, 11022);
			WatchStoryPlayerIds.Remove(model.PlayerId);
		}
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null && !WatchStoryPlayerIds.Contains(selfPlayerData.player.Id) && WatchStoryPlayerIds.Count > 0)
		{
			_storyActionSn = 0L;
			for (int i = 0; i < WatchStoryPlayerIds.Count; i++)
			{
				await SimpleSingletonProvider<UIManager>.inst.tips.ShowMultiplePlayerThink(WatchStoryPlayerIds[i], 11022);
			}
		}
		if (model.Index != 0 && roomController.localRoom != null)
		{
			roomController.localRoom.StoryId = model.Index;
		}
		await UniTask.CompletedTask;
	}
}
