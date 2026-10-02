using System;
using System.Collections.Generic;
using System.Linq;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using SinglePlayer;
using SinglePlayer.GamePlay;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class FriendLogic : IRPCSync
{
	public readonly FriendSignal signal = new FriendSignal();

	private readonly Dictionary<long, FriendData> playersData = new Dictionary<long, FriendData>();

	private readonly Dictionary<long, FriendSelfApply> selfApplyRecord = new Dictionary<long, FriendSelfApply>();

	public readonly List<long> FriendList = new List<long>();

	private readonly List<long> _BlacksList = new List<long>();

	public readonly List<long> ShowFriendList = new List<long>();

	public readonly List<long> ShowBlacksList = new List<long>();

	public readonly List<long> LastPlayFriends = new List<long>();

	public readonly MapField<long, string> FriendNotes = new MapField<long, string>();

	public readonly List<FriendData> searchFriends = new List<FriendData>();

	private readonly Dictionary<long, FriendData> ApplyDict = new Dictionary<long, FriendData>();

	public readonly List<long> ShowApplyList = new List<long>();

	private int dayApplyCount;

	public readonly ReadOnlyReactiveProperty<bool> applyStatus = new ReadOnlyReactiveProperty<bool>(initialValue: false);

	public readonly List<long> InviteFriends = new List<long>();

	public List<FriendLeaderboardData> friendScoresList = new List<FriendLeaderboardData>();

	private const int FriendS2CThreshold = 3;

	private int FriendListS2CTimes;

	private int NearFightPlayerS2CTimes;

	private int SearchPlayerS2CTimes;

	private int FriendApplyListS2CTimes;

	private int FriendBlacksListS2CTimes;

	private int FriendInviteListS2CTimes;

	private int GetPlayerSimpleS2CTimes;

	public List<FriendData> PlayersList => playersData.Values.ToList();

	public void InitFromServer(FriendList _Friends)
	{
		playersData.Clear();
		UpdateData(_Friends);
	}

	private void UpdateData(FriendList _Friends)
	{
		dayApplyCount = _Friends.DayApplyCount;
		FriendList.Clear();
		FriendList.AddRange(_Friends.FriendIds);
		_BlacksList.Clear();
		_BlacksList.AddRange(_Friends.Blacks);
		UpdateApplyList(_Friends.Apply);
		UpdateSelfApplyRecord(_Friends.SelfApply);
		UpdateFriendNoteList(_Friends.FriendNotes);
	}

	public FriendData GetFriendData(long playerId)
	{
		if (!playersData.TryGetValue(playerId, out var value))
		{
			Debug.LogError($"在好友系统中无法获取玩家{playerId}对应的数据详情");
			return null;
		}
		return value;
	}

	public bool TryGetFriendData(long playerId, out FriendData friendData)
	{
		return playersData.TryGetValue(playerId, out friendData);
	}

	public int GetFriendCount()
	{
		return FriendList.Count;
	}

	public int GetBlackCount()
	{
		return _BlacksList.Count;
	}

	public bool IsFriend(long playerId)
	{
		return FriendList.Contains(playerId);
	}

	private void UpdateFriendData(RepeatedField<FriendInfo> friendInfos)
	{
		ShowFriendList.Clear();
		FriendList.Clear();
		friendScoresList.Clear();
		FriendNotes.Clear();
		for (int i = 0; i < friendInfos.Count; i++)
		{
			long playerId = friendInfos[i].PlayerId;
			UpdatePlayerData(friendInfos[i]);
			FriendList.Add(playerId);
			FriendNotes.TryAdd(playerId, friendInfos[i].Note);
			if (friendInfos[i].IsOnline)
			{
				ShowFriendList.Add(playerId);
			}
			FriendLeaderboardData friendLeaderboardData = GetFriendLeaderboardData(playerId);
			if (friendLeaderboardData.playerId != 0L)
			{
				friendScoresList.Add(friendLeaderboardData);
			}
		}
		FriendLeaderboardData item = AddPlayerData();
		if (item.Score != -1)
		{
			friendScoresList.Add(item);
		}
		friendScoresList.Sort((FriendLeaderboardData a, FriendLeaderboardData b) => b.Score.CompareTo(a.Score));
		ShowFriendList.Sort((long x, long y) => CompareToFriend(playersData[x], playersData[y]));
		FriendList.Sort((long x, long y) => CompareToFriend(playersData[x], playersData[y]));
	}

	private int CompareToFriend(FriendData x, FriendData y)
	{
		if (IsBlack(x.playerId) == IsBlack(y.playerId))
		{
			if (!x.IsOnline.Equals(y.IsOnline))
			{
				if (!x.IsOnline)
				{
					return 1;
				}
				return -1;
			}
			if (x.IsBusy.Equals(y.IsBusy))
			{
				if (x.loginTime - y.loginTime <= 0)
				{
					return 1;
				}
				return -1;
			}
			if (!x.IsBusy)
			{
				return -1;
			}
			return 1;
		}
		if (!IsBlack(x.playerId))
		{
			return -1;
		}
		return 1;
	}

	private void DeleteFriend(long playerId)
	{
		if (FriendList.Contains(playerId))
		{
			FriendList.Remove(playerId);
			ShowFriendList.Remove(playerId);
			FriendNotes.Remove(playerId);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.TryRemoveChatData(playerId);
		}
		signal.refreshFriends.Dispatch();
		DeletedSelfApply(playerId);
	}

	private void AddFriend(long playerId)
	{
		if (!IsFriend(playerId))
		{
			FriendList.Add(playerId);
			FriendNotes.Add(playerId, "");
			signal.refreshFriends.Dispatch();
		}
		DeletedLastPlayer(playerId);
		DeleteApply(playerId);
		DeletedSelfApply(playerId);
	}

	private bool IsCONCERN(long playerId)
	{
		return selfApplyRecord.ContainsKey(playerId);
	}

	private void UpdateSelfApplyRecord(RepeatedField<FriendSelfApply> _records)
	{
		selfApplyRecord.Clear();
		for (int i = 0; i < _records.Count; i++)
		{
			UpdateSelfApplyRecord(_records[i]);
		}
	}

	private void UpdateSelfApplyRecord(FriendSelfApply _record)
	{
		if (!FriendList.Contains(_record.PlayerId) && !selfApplyRecord.TryAdd(_record.PlayerId, _record))
		{
			selfApplyRecord[_record.PlayerId] = _record;
		}
	}

	private void AddSelfApply(long playerId)
	{
		if (!IsCONCERN(playerId))
		{
			selfApplyRecord.Add(playerId, new FriendSelfApply
			{
				PlayerId = playerId,
				Time = MonoSingletonProvider<NetManager>.inst.ServerTime.Millisecond / 1000
			});
		}
	}

	private void DeletedSelfApply(long playerId)
	{
		if (IsCONCERN(playerId))
		{
			selfApplyRecord.Remove(playerId);
		}
	}

	public bool IsAddForMe(long playerId)
	{
		return ApplyDict.ContainsKey(playerId);
	}

	private void UpdateApplyList(RepeatedField<FriendApply> friendsApply)
	{
		ApplyDict.Clear();
		for (int i = 0; i < friendsApply.Count; i++)
		{
			long playerId = friendsApply[i].PlayerId;
			if (!FriendList.Contains(playerId))
			{
				FriendData friendData = new FriendData();
				friendData.UpdateFriendData(friendsApply[i]);
				if (!IsBlack(playerId))
				{
					ApplyDict.TryAdd(playerId, friendData);
				}
			}
		}
		if (ApplyDict.Count > 0)
		{
			applyStatus.SetValue(value: true);
		}
	}

	private void UpdateApplyList(RepeatedField<FriendShowPlayerInfo> friendsApply)
	{
		List<long> list = new List<long>();
		for (int i = 0; i < friendsApply.Count; i++)
		{
			long playerId = friendsApply[i].PlayerId;
			FriendData friendData = UpdatePlayerData(friendsApply[i]);
			friendData.IsOnline = true;
			ApplyDict[playerId] = friendData;
			if (!ShowApplyList.Contains(playerId))
			{
				list.Add(playerId);
			}
		}
		list.Sort(CompareToApply);
		ShowApplyList.AddRange(list);
	}

	private int CompareToApply(long x, long y)
	{
		if (!ApplyDict.TryGetValue(x, out var value))
		{
			return 0;
		}
		if (!ApplyDict.TryGetValue(y, out var value2))
		{
			return 0;
		}
		if (value._Time - value2._Time <= 0)
		{
			return 1;
		}
		return -1;
	}

	public FriendData GetApplyData(long playerId)
	{
		return ApplyDict.GetValueOrDefault(playerId);
	}

	private void DeleteApply(long playerId)
	{
		if (IsAddForMe(playerId))
		{
			if (ApplyDict.ContainsKey(playerId))
			{
				ApplyDict.Remove(playerId);
			}
			if (ShowApplyList.Contains(playerId))
			{
				ShowApplyList.Remove(playerId);
			}
			signal.refreshApplyFriends.Dispatch();
		}
	}

	public FriendData GetApplyInfo(long applyId)
	{
		return ApplyDict.GetValueOrDefault(applyId);
	}

	public bool IsBlack(long playerId)
	{
		return _BlacksList.Contains(playerId);
	}

	private void UpdateBlackFriendData(RepeatedField<FriendShowPlayerInfo> friendInfos)
	{
		_BlacksList.Clear();
		ShowBlacksList.Clear();
		for (int i = 0; i < friendInfos.Count; i++)
		{
			long playerId = friendInfos[i].PlayerId;
			UpdatePlayerData(friendInfos[i]);
			_BlacksList.Add(playerId);
			ShowBlacksList.Add(playerId);
		}
	}

	private void PutInBlack(long playerID)
	{
		if (!IsBlack(playerID))
		{
			_BlacksList.Add(playerID);
			signal.refreshBlackFriends.Dispatch();
		}
		if (IsFriend(playerID))
		{
			signal.refreshFriends.Dispatch();
		}
		DeletedLastPlayer(playerID);
		DeleteApply(playerID);
		DeletedSelfApply(playerID);
	}

	private void PutOutBlack(long playerID)
	{
		if (IsBlack(playerID))
		{
			_BlacksList.Remove(playerID);
			ShowBlacksList.Remove(playerID);
			signal.refreshBlackFriends.Dispatch();
			if (IsFriend(playerID))
			{
				signal.refreshFriends.Dispatch();
			}
		}
	}

	private void DeletedLastPlayer(long playerId)
	{
		if (LastPlayFriends.Remove(playerId))
		{
			signal.refreshLastFriends.Dispatch();
		}
	}

	private void UpdateNearFightPlayerData(RepeatedField<FriendShowPlayerInfo> playerInfos)
	{
		LastPlayFriends.Clear();
		for (int i = 0; i < playerInfos.Count; i++)
		{
			UpdatePlayerData(playerInfos[i]);
			LastPlayFriends.Add(playerInfos[i].PlayerId);
		}
		LastPlayFriends.Sort((long x, long y) => CompareToByTime(playersData[x], playersData[y]));
	}

	private void UpdateFriendInviteData(RepeatedField<FriendInviteInfo> friendInfos)
	{
		InviteFriends.Clear();
		for (int i = 0; i < friendInfos.Count; i++)
		{
			UpdatePlayerData(friendInfos[i]);
			InviteFriends.Add(friendInfos[i].PlayerId);
		}
		InviteFriends.Sort((long x, long y) => CompareToByTime(playersData[x], playersData[y]));
	}

	private void UpdatePlayerData(FriendInfo friendInfo)
	{
		long playerId = friendInfo.PlayerId;
		if (!playersData.ContainsKey(playerId))
		{
			playersData.TryAdd(playerId, new FriendData());
		}
		playersData[playerId].UpdateFriendData(friendInfo);
	}

	private FriendData UpdatePlayerData(FriendShowPlayerInfo friendShowInfo)
	{
		long playerId = friendShowInfo.PlayerId;
		if (!playersData.ContainsKey(playerId))
		{
			playersData.TryAdd(playerId, new FriendData());
		}
		playersData[playerId].UpdateFriendData(friendShowInfo);
		return playersData[playerId];
	}

	private void UpdatePlayerData(FriendInviteInfo friendInviteInfo)
	{
		long playerId = friendInviteInfo.PlayerId;
		if (!playersData.ContainsKey(playerId))
		{
			playersData.TryAdd(playerId, new FriendData());
		}
		playersData[playerId].UpdateFriendData(friendInviteInfo);
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.FriendNotifyS2C.OnFriendNotifyS2CServerCallBackAsync = OnFriendNotifyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendListChangeS2C.OnFriendListChangeS2CServerCallBackAsync = OnFriendListChangeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteNotifyS2C.OnFriendInviteNotifyS2CServerCallBackAsync = OnFriendInviteNotifyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendListS2C.OnFriendListS2CServerCallBackAsync = OnFriendListS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.NearFightPlayerS2C.OnNearFightPlayerS2CServerCallBackAsync = OnNearFightPlayerS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendApplyListS2C.OnFriendApplyListS2CServerCallBackAsync = OnFriendApplyListS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendBlacksListS2C.OnFriendBlacksListS2CServerCallBackAsync = OnFriendBlacksListS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteCleanS2C.OnFriendInviteCleanS2CServerCallBackAsync = OnFriendInviteCleanS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteS2C.OnFriendInviteS2CServerCallBackAsync = OnFriendInviteS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteListS2C.OnFriendInviteListS2CServerCallBackAsync = OnFriendInviteListS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendApplyS2C.OnFriendApplyS2CServerCallBackAsync = OnFriendApplyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendApplyOpS2C.OnFriendApplyOpS2CServerCallBackAsync = OnFriendApplyOpS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendOpS2C.OnFriendOpS2CServerCallBackAsync = OnFriendOpS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SearchPlayerS2C.OnSearchPlayerS2CServerCallBackAsync = OnSearchPlayerS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GetPlayerSimpleS2C.OnGetPlayerSimpleS2CServerCallBackAsync = OnGetPlayerSimpleS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendDelNotifyS2C.OnFriendDelNotifyS2CServerCallBackAsync = OnFriendDelNotifyS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.SetFriendNoteS2C.OnSetFriendNoteS2CServerCallBackAsync = OnFriendSetFriendNoteS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SetOnlineStatusS2C.OnSetOnlineStatusS2CServerCallBackAsync = OnSetOnlineStatusS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.FriendNotifyS2C.OnFriendNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendListChangeS2C.OnFriendListChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteNotifyS2C.OnFriendInviteNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendListS2C.OnFriendListS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.NearFightPlayerS2C.OnNearFightPlayerS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendApplyListS2C.OnFriendApplyListS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendBlacksListS2C.OnFriendBlacksListS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteCleanS2C.OnFriendInviteCleanS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteS2C.OnFriendInviteS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteListS2C.OnFriendInviteListS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendApplyS2C.OnFriendApplyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendApplyOpS2C.OnFriendApplyOpS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendOpS2C.OnFriendOpS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SearchPlayerS2C.OnSearchPlayerS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GetPlayerSimpleS2C.OnGetPlayerSimpleS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FriendDelNotifyS2C.OnFriendDelNotifyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SetFriendNoteS2C.OnSetFriendNoteS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SetOnlineStatusS2C.OnSetOnlineStatusS2CServerCallBackAsync = null;
	}

	private async UniTask OnFriendNotifyS2CServerCallBack(FriendNotifyS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			FriendList.Remove(model.PlayerId);
			FriendNotes.Remove(model.PlayerId);
			if (!ApplyDict.ContainsKey(model.PlayerId))
			{
				ApplyDict.Add(model.PlayerId, new FriendData
				{
					playerId = model.PlayerId
				});
			}
			if (ApplyDict.Count > 0)
			{
				applyStatus.SetValue(value: true);
			}
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is FriendPanel friendPanel)
			{
				friendPanel.TryRefreshApplyList();
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnFriendListChangeS2CServerCallBack(FriendListChangeS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			UpdateData(model.Friend);
			signal.refreshFriends.Dispatch();
			signal.refreshApplyFriends.Dispatch();
			signal.refreshLastFriends.Dispatch();
			SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnFriendInviteNotifyS2CServerCallBack(FriendInviteNotifyS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Home && SimpleSingletonProvider<UIManager>.inst.currentPanel.config.PanelType != UIPanelType.RoomHero)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowInviteSignal(AstralInviteType.RoomInvite);
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnFriendDelNotifyS2CServerCallBackAsync(FriendDelNotifyS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			DeleteFriend(model.PlayerId);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestFriendListC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.FriendListC2S.FriendListC2SCall(new FriendListC2S());
	}

	private async UniTask OnFriendListS2CServerCallBack(FriendListS2C model, int errid, bool isdispatch)
	{
		FriendListS2CTimes++;
		if (errid != 0)
		{
			switch (errid)
			{
			case 12013:
				Debug.Log("好友没有缓存玩家简要信息 重新拉取");
				if (FriendListS2CTimes > 3)
				{
					FriendListS2CTimes = 0;
				}
				else
				{
					RequestFriendListC2S();
				}
				break;
			case 12014:
				FriendListS2CTimes = 0;
				Debug.Log("黑名单列表获取过于频繁");
				signal.refreshFriends.Dispatch();
				break;
			}
			await UniTask.CompletedTask;
		}
		else
		{
			FriendListS2CTimes = 0;
			UpdateFriendData(model.Friends);
			signal.refreshFriends.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestNearFightPlayerC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.NearFightPlayerC2S.NearFightPlayerC2SCall(new NearFightPlayerC2S());
	}

	private async UniTask OnNearFightPlayerS2CServerCallBack(NearFightPlayerS2C model, int errid, bool isdispatch)
	{
		NearFightPlayerS2CTimes++;
		if (errid != 0)
		{
			switch (errid)
			{
			case 12013:
				Debug.Log("好友没有缓存玩家简要信息 重新拉取");
				if (NearFightPlayerS2CTimes > 3)
				{
					NearFightPlayerS2CTimes = 0;
				}
				else
				{
					RequestNearFightPlayerC2S();
				}
				break;
			case 12014:
				Debug.Log("黑名单列表获取过于频繁");
				NearFightPlayerS2CTimes = 0;
				signal.refreshLastFriends.Dispatch();
				break;
			}
			await UniTask.CompletedTask;
		}
		else
		{
			NearFightPlayerS2CTimes = 0;
			UpdateNearFightPlayerData(model.Infos);
			signal.refreshLastFriends.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public void RequestSearchPlayerC2S(long playerId, Action<RPCAsyncResult> onFinished)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SearchPlayerC2S.SearchPlayerC2SCall(new SearchPlayerC2S
		{
			PlayerId = playerId
		}).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (SearchPlayerResponseHandle(result, playerId, onFinished))
			{
				onFinished?.Invoke(result);
			}
		});
	}

	private bool SearchPlayerResponseHandle(RPCAsyncResult result, long playerId, Action<RPCAsyncResult> onFinished)
	{
		if (result.errId == 0)
		{
			return true;
		}
		SearchPlayerS2CTimes++;
		if (result.errId == 12013)
		{
			Debug.Log("好友没有缓存玩家简要信息 重新拉取");
			if (SearchPlayerS2CTimes > 3)
			{
				SearchPlayerS2CTimes = 0;
				return true;
			}
			RequestSearchPlayerC2S(playerId, onFinished);
			return false;
		}
		return true;
	}

	private async UniTask OnSearchPlayerS2CServerCallBack(SearchPlayerS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			await UniTask.CompletedTask;
			return;
		}
		SearchPlayerS2CTimes = 0;
		searchFriends.Clear();
		UpdatePlayerData(model.Info);
		searchFriends.Add(playersData[model.Info.PlayerId]);
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestFriendApplyListC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.FriendApplyListC2S.FriendApplyListC2SCall(new FriendApplyListC2S());
	}

	private async UniTask OnFriendApplyListS2CServerCallBack(FriendApplyListS2C model, int errid, bool isdispatch)
	{
		FriendApplyListS2CTimes++;
		if (errid != 0)
		{
			switch (errid)
			{
			case 12013:
				Debug.Log("好友没有缓存玩家简要信息 重新拉取");
				if (FriendApplyListS2CTimes > 3)
				{
					FriendApplyListS2CTimes = 0;
				}
				else
				{
					RequestFriendApplyListC2S();
				}
				break;
			case 12014:
				Debug.Log("申请玩家信息列表获取过于频繁");
				FriendApplyListS2CTimes = 0;
				signal.refreshApplyFriends.Dispatch();
				break;
			}
			await UniTask.CompletedTask;
		}
		else
		{
			FriendApplyListS2CTimes = 0;
			ShowApplyList.Clear();
			applyStatus.SetValue(value: false);
			UpdateApplyList(model.Apply);
			signal.refreshApplyFriends.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestFriendBlacksListC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.FriendBlacksListC2S.FriendBlacksListC2SCall(new FriendBlacksListC2S());
	}

	private async UniTask OnFriendBlacksListS2CServerCallBack(FriendBlacksListS2C model, int errid, bool isdispatch)
	{
		FriendBlacksListS2CTimes++;
		if (errid != 0)
		{
			switch (errid)
			{
			case 12013:
				Debug.Log("好友没有缓存玩家简要信息 重新拉取");
				if (FriendBlacksListS2CTimes > 3)
				{
					FriendBlacksListS2CTimes = 0;
				}
				else
				{
					RequestFriendBlacksListC2S();
				}
				break;
			case 12014:
				Debug.Log("黑名单列表获取过于频繁");
				FriendBlacksListS2CTimes = 0;
				signal.refreshBlackFriends.Dispatch();
				break;
			}
			await UniTask.CompletedTask;
		}
		else
		{
			FriendBlacksListS2CTimes = 0;
			UpdateBlackFriendData(model.Infos);
			signal.refreshBlackFriends.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public void RequestFriendOpC2S(long playerId, string nick, int opType, System.Action _OnComplete)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.FriendOpC2S.FriendOpC2SCall(new FriendOpC2S
		{
			OpType = opType,
			PlayerId = playerId
		}).OnFinished.AddOnce(delegate(RPCAsyncResult _)
		{
			_OnComplete?.Invoke();
			if (_.errId == 0)
			{
				if (opType == 1)
				{
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(string.Format(105.GetLocal(UIStringType.Friend), nick));
				}
				else if (opType == 2)
				{
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(104.GetLocal(UIStringType.Friend));
				}
				else if (opType == 3)
				{
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(string.Format(106.GetLocal(UIStringType.Friend), nick));
				}
			}
		});
	}

	private async UniTask OnFriendOpS2CServerCallBack(FriendOpS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			if (model.OpType == 1)
			{
				DeleteFriend(model.PlayerId);
			}
			else if (model.OpType == 2)
			{
				PutInBlack(model.PlayerId);
			}
			else if (model.OpType == 3)
			{
				PutOutBlack(model.PlayerId);
			}
			SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public string GetDisplayNick(long playerId, string nick, bool showNote)
	{
		if (!showNote)
		{
			return nick;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			return nick;
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.friend.IsFriend(playerId))
		{
			return nick;
		}
		string friendNote = GetFriendNote(playerId);
		if (string.IsNullOrEmpty(friendNote))
		{
			return nick;
		}
		return string.Concat("[color=#FC1494](" + friendNote + ")[/color]", nick);
	}

	public string GetFriendNote(long friendId)
	{
		FriendNotes.TryGetValue(friendId, out var value);
		return value;
	}

	private void UpdateFriendNoteList(MapField<long, string> friendsFriendNotes)
	{
		FriendNotes.Clear();
		foreach (KeyValuePair<long, string> friendsFriendNote in friendsFriendNotes)
		{
			FriendNotes.Add(friendsFriendNote.Key, friendsFriendNote.Value);
		}
	}

	public RPCAsyncResult SetFriendNoteC2S(long playerId, string note)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.SetFriendNoteC2S.SetFriendNoteC2SCall(new SetFriendNoteC2S
		{
			PlayerId = playerId,
			Note = note
		});
	}

	private async UniTask OnFriendSetFriendNoteS2CServerCallBack(SetFriendNoteS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			FriendNotes[model.PlayerId] = model.Note;
			SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeFriendNote.Dispatch(t: true);
			if (IsBlack(model.PlayerId))
			{
				signal.refreshBlackFriends.Dispatch();
			}
			if (LastPlayFriends.Contains(model.PlayerId))
			{
				signal.refreshLastFriends.Dispatch();
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.Dispatch();
			}
			signal.refreshFriends.Dispatch();
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1011.GetLocal(UIStringType.Friend));
			await UniTask.CompletedTask;
		}
	}

	public void RequestFriendApplyOpC2S(long playerId, int opType, bool allRefuse, System.Action _OnComplete)
	{
		if (opType == 1 && FriendList.Count >= StaticGlobalData.FRIEND_NUMBLIMIT)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1004.GetLocal(UIStringType.Friend));
			_OnComplete?.Invoke();
			return;
		}
		if (opType == 3 && _BlacksList.Count >= StaticGlobalData.FRIEND_BLACKLIST_NUMBLIMIT)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1006.GetLocal(UIStringType.Friend));
			_OnComplete?.Invoke();
			return;
		}
		MonoSingletonProvider<NetManager>.inst.RPC.FriendApplyOpC2S.FriendApplyOpC2SCall(new FriendApplyOpC2S
		{
			OpType = opType,
			AllRefuse = allRefuse,
			PlayerId = playerId
		}).OnFinished.AddOnce(delegate(RPCAsyncResult _)
		{
			_OnComplete?.Invoke();
			if (_.errId == 0 && opType == 1)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(102.GetLocal(UIStringType.Friend));
			}
		});
	}

	private async UniTask OnFriendApplyOpS2CServerCallBack(FriendApplyOpS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			if (model.AllRefuse)
			{
				ApplyDict.Clear();
				signal.refreshApplyFriends.Dispatch();
			}
			if (model.OpType == 1)
			{
				AddFriend(model.PlayerId);
			}
			else if (model.OpType == 2)
			{
				DeleteApply(model.PlayerId);
			}
			else if (model.OpType == 3)
			{
				PutInBlack(model.PlayerId);
			}
			SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public void RequestFriendApplyC2S(long playerId, System.Action _OnComplete = null)
	{
		if (dayApplyCount >= StaticGlobalData.FRIEND_APPLICATION_DAILYLIMIT)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1003.GetLocal(UIStringType.Friend));
			_OnComplete?.Invoke();
			return;
		}
		MonoSingletonProvider<NetManager>.inst.RPC.FriendApplyC2S.FriendApplyC2SCall(new FriendApplyC2S
		{
			PlayerId = playerId
		}).OnFinished.AddOnce(delegate(RPCAsyncResult _)
		{
			_OnComplete?.Invoke();
			if (_.errId == 0)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(101.GetLocal(UIStringType.Friend));
			}
		});
	}

	private async UniTask OnFriendApplyS2CServerCallBack(FriendApplyS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			AddSelfApply(model.PlayerId);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestFriendInviteC2S(List<long> playerIds, string _pwd)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteC2S.FriendInviteC2SCall(new FriendInviteC2S
		{
			PlayerId = { (IEnumerable<long>)playerIds },
			Pwd = _pwd
		});
	}

	private async UniTask OnFriendInviteS2CServerCallBack(FriendInviteS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestFriendInviteListC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteListC2S.FriendInviteListC2SCall(new FriendInviteListC2S());
	}

	private async UniTask OnFriendInviteListS2CServerCallBack(FriendInviteListS2C model, int errid, bool isdispatch)
	{
		FriendInviteListS2CTimes++;
		if (errid != 0)
		{
			switch (errid)
			{
			case 12013:
				Debug.Log("好友没有缓存玩家简要信息 重新拉取");
				if (FriendInviteListS2CTimes > 3)
				{
					FriendInviteListS2CTimes = 0;
				}
				else
				{
					RequestFriendInviteListC2S();
				}
				break;
			case 12014:
				Debug.Log("好友列表获取过于频繁");
				FriendInviteListS2CTimes = 0;
				break;
			}
			await UniTask.CompletedTask;
		}
		else
		{
			FriendInviteListS2CTimes = 0;
			UpdateFriendInviteData(model.Inf);
			await UniTask.CompletedTask;
		}
	}

	private int CompareToByTime(FriendData x, FriendData y)
	{
		if (x._Time - y._Time <= 0)
		{
			return 1;
		}
		return -1;
	}

	public RPCAsyncResult RequestFriendInviteCleanC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.FriendInviteCleanC2S.FriendInviteCleanC2SCall(new FriendInviteCleanC2S());
	}

	private async UniTask OnFriendInviteCleanS2CServerCallBack(FriendInviteCleanS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public void RequestGetPlayerSimpleC2S(long playerId, Action<RPCAsyncResult> onFinished)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GetPlayerSimpleC2S.GetPlayerSimpleC2SCall(new GetPlayerSimpleC2S
		{
			PlayerId = playerId
		}).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (GetPlayerSimpleResponseHandle(result, playerId, onFinished))
			{
				onFinished?.Invoke(result);
			}
		});
	}

	private bool GetPlayerSimpleResponseHandle(RPCAsyncResult result, long playerId, Action<RPCAsyncResult> onFinished)
	{
		if (result.errId == 0)
		{
			return true;
		}
		GetPlayerSimpleS2CTimes++;
		if (result.errId == 12013)
		{
			Debug.Log("好友没有缓存玩家简要信息 重新拉取");
			if (GetPlayerSimpleS2CTimes > 3)
			{
				GetPlayerSimpleS2CTimes = 0;
				return true;
			}
			RequestGetPlayerSimpleC2S(playerId, null);
			return false;
		}
		return true;
	}

	private async UniTask OnGetPlayerSimpleS2CServerCallBack(GetPlayerSimpleS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			await UniTask.CompletedTask;
			return;
		}
		GetPlayerSimpleS2CTimes = 0;
		FriendInfo playerInfo = model.PlayerInfo;
		if (!playersData.TryGetValue(playerInfo.PlayerId, out var value))
		{
			FriendData friendData = new FriendData();
			friendData.UpdateFriendData(playerInfo);
			playersData.TryAdd(playerInfo.PlayerId, friendData);
		}
		else
		{
			value.UpdateFriendData(playerInfo);
		}
		if (!ShowFriendList.Contains(playerInfo.PlayerId))
		{
			ShowFriendList.Add(playerInfo.PlayerId);
		}
		signal.refreshFriends.Dispatch();
		await UniTask.CompletedTask;
	}

	public FriendLeaderboardData GetFriendLeaderboardData(long playerId)
	{
		FriendLeaderboardData result = default(FriendLeaderboardData);
		FriendData friendData = GetFriendData(playerId);
		if (friendData == null || friendData.Score == -1)
		{
			return result;
		}
		result.playerId = friendData.playerId;
		result.Score = friendData.Score;
		result.LV = friendData.LV;
		result.Nick = GetDisplayNick(friendData.playerId, friendData.Nick, showNote: true);
		result.HeadURL = friendData.HeadURL;
		result.Label = friendData.Label;
		return result;
	}

	public FriendLeaderboardData AddPlayerData()
	{
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		ShowingFashion runningFashion = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetRunningFashion();
		(string, bool) playerLabel = runningFashion.labelId.GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
		string fashionAccountHeadShot = runningFashion.headShotId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
		FriendLeaderboardData result = new FriendLeaderboardData
		{
			playerId = playerInfo.Id,
			LV = playerInfo.Level
		};
		if (playerInfo.SingleInfo == null)
		{
			playerInfo.SingleInfo = new SingleInfo();
			playerInfo.SingleInfo.MaxScore = -1;
		}
		result.Score = playerInfo.SingleInfo.MaxScore;
		result.Nick = playerInfo.Nick;
		result.HeadURL = fashionAccountHeadShot;
		result.Label = playerLabel;
		return result;
	}

	public void HandleSinglePlayerDataAfterBattle()
	{
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		int num = Game.GetModel<GameData>().heroProperty.CalculateScore();
		int num2 = friendScoresList.FindIndex((FriendLeaderboardData x) => x.playerId == playerInfo.Id);
		if (num2 >= 0)
		{
			FriendLeaderboardData value = friendScoresList[num2];
			num = Mathf.Max(num, value.Score);
			value.ChangeScore(num);
			friendScoresList[num2] = value;
		}
		else
		{
			FriendLeaderboardData item = AddPlayerData();
			item.ChangeScore(num);
			friendScoresList.Add(item);
		}
		Player player = playerInfo;
		if (player.SingleInfo == null)
		{
			SingleInfo singleInfo = (player.SingleInfo = new SingleInfo());
		}
		playerInfo.SingleInfo.MaxScore = num;
		friendScoresList.Sort((FriendLeaderboardData a, FriendLeaderboardData b) => b.Score - a.Score);
	}

	public RPCAsyncResult SetOnlineStatusC2S(int curStatus)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.SetOnlineStatusC2S.SetOnlineStatusC2SCall(new SetOnlineStatusC2S
		{
			OnlineStatus = curStatus
		});
	}

	private async UniTask OnSetOnlineStatusS2CServerCallBack(SetOnlineStatusS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
			SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().OnlineStatus = model.OnlineStatus;
		}
	}
}
