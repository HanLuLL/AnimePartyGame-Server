using System;
using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class GuildLogic : IRPCSync
{
	private sealed class PendingGuildSync
	{
		public SyncGuildS2C Guild { get; private set; }

		public SyncGuildMemberS2C Members { get; private set; }

		public SyncGuildMemberExitS2C Exits { get; private set; }

		public static PendingGuildSync ForGuild(SyncGuildS2C model)
		{
			return new PendingGuildSync
			{
				Guild = model
			};
		}

		public static PendingGuildSync ForMembers(SyncGuildMemberS2C model)
		{
			return new PendingGuildSync
			{
				Members = model
			};
		}

		public static PendingGuildSync ForExits(SyncGuildMemberExitS2C model)
		{
			return new PendingGuildSync
			{
				Exits = model
			};
		}
	}

	public readonly GuildSignal signal = new GuildSignal();

	private PlayerGuildInfo _playerGuild;

	public readonly List<Guild> SearchResults = new List<Guild>();

	public readonly Dictionary<long, Guild> GuildCache = new Dictionary<long, Guild>();

	private DateTime _lastSearchServerTime = DateTime.MinValue;

	private readonly Dictionary<long, FriendShowPlayerInfo> _guildMemberInfos = new Dictionary<long, FriendShowPlayerInfo>();

	private readonly List<GuildMemberChangeMsg> _guildMemberChangeMessages = new List<GuildMemberChangeMsg>();

	private readonly List<PendingGuildSync> _pendingGuildSyncs = new List<PendingGuildSync>();

	private Guild _currentGuild;

	private bool _hasCurrentGuildSnapshot;

	private bool _hasMemberInfoSnapshot;

	private bool _isCurrentGuildRequesting;

	private bool _isMemberInfosRequesting;

	private bool _isMemberMessagesRequesting;

	private GuildLeaveCause _expectedLeaveCause;

	private bool _leaveDispatched;

	private readonly List<GuildApplication> _validGuildApplications = new List<GuildApplication>();

	private readonly HashSet<long> _invitationRequests = new HashSet<long>();

	private readonly HashSet<long> _applicationRequests = new HashSet<long>();

	private readonly Dictionary<int, TaskDSO> _guildTasks = new Dictionary<int, TaskDSO>();

	private readonly List<GuildTaskView> _guildTaskViews = new List<GuildTaskView>();

	private readonly HashSet<int> _guildMissionRewardRequests = new HashSet<int>();

	private bool _hasGuildTaskSnapshot;

	public PlayerGuildInfo PlayerGuild => _playerGuild;

	public bool IsJoined
	{
		get
		{
			if (_playerGuild != null)
			{
				return _playerGuild.GuildId != 0;
			}
			return false;
		}
	}

	public bool IsDailyApplyLimitReached
	{
		get
		{
			if (_playerGuild != null)
			{
				return _playerGuild.ApplyCount >= StaticGlobalData.GUILD_APPLICATIONS_DAILYLIMIT;
			}
			return false;
		}
	}

	public bool HasValidApplication
	{
		get
		{
			if (_playerGuild == null)
			{
				return false;
			}
			long num = (long)StaticGlobalData.GUILD_APPLICATIONS_TERM * 24L * 3600;
			long num2 = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds() - num;
			foreach (KeyValuePair<long, long> application in _playerGuild.Applications)
			{
				if (application.Value >= num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool HasValidInvitation
	{
		get
		{
			if (_playerGuild == null)
			{
				return false;
			}
			long num = (long)StaticGlobalData.GUILD_INVITETOGUILD_TERM * 24L * 3600;
			long num2 = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds() - num;
			foreach (KeyValuePair<long, GuildInvitation> receivedInvitation in _playerGuild.ReceivedInvitations)
			{
				if (receivedInvitation.Value != null && receivedInvitation.Value.InvitationTime >= num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool IsSearchEnd { get; private set; }

	public int SearchPage { get; private set; }

	public bool HasMoreSearchPage
	{
		get
		{
			if (SearchPage > 0)
			{
				return !IsSearchEnd;
			}
			return false;
		}
	}

	public bool HasCurrentGuildSnapshot => _hasCurrentGuildSnapshot;

	public bool HasMemberInfoSnapshot => _hasMemberInfoSnapshot;

	public Guild CurrentGuild => _currentGuild;

	public IReadOnlyDictionary<long, FriendShowPlayerInfo> GuildMemberInfos => _guildMemberInfos;

	public IReadOnlyList<GuildMemberChangeMsg> GuildMemberChangeMessages => _guildMemberChangeMessages;

	public long SelfPlayerId => SimpleSingletonProvider<GameLogicManager>.inst.account?.GetPlayerID() ?? 0;

	public GuildMember SelfGuildMember => GetGuildMember(SelfPlayerId);

	public bool CanEditSettings => GetSelfAuthority()?.CanGuildSetting ?? false;

	public bool CanEditInternalAnnouncement => GetSelfAuthority()?.CanInternalAnnouncement ?? false;

	public bool CanInvite => GetSelfAuthority()?.CanInviteToGuild ?? false;

	public bool CanApproveApplications => GetSelfAuthority()?.CanApproveApplications ?? false;

	public bool CanDisbandGuild => GetSelfAuthority()?.CanDisbandGuild ?? false;

	public bool CanExitGuild
	{
		get
		{
			if (IsJoined)
			{
				return SelfGuildMember != null;
			}
			return false;
		}
	}

	public IReadOnlyList<GuildApplication> ValidGuildApplications => _validGuildApplications;

	public bool HasPendingGuildApplications
	{
		get
		{
			if (CanApproveApplications)
			{
				return _validGuildApplications.Count > 0;
			}
			return false;
		}
	}

	public IReadOnlyDictionary<int, TaskDSO> GuildTasks => _guildTasks;

	public IReadOnlyList<GuildTaskView> GuildTaskViews => _guildTaskViews;

	public bool HasGuildTaskSnapshot => _hasGuildTaskSnapshot;

	public bool IsApplicationActive(long guildId)
	{
		if (_playerGuild == null)
		{
			return false;
		}
		long num = (long)StaticGlobalData.GUILD_APPLICATIONS_TERM * 24L * 3600;
		long num2 = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds() - num;
		if (!_playerGuild.Applications.TryGetValue(guildId, out var value))
		{
			return false;
		}
		return value >= num2;
	}

	public bool IsInJoinCooldown()
	{
		if (_playerGuild == null)
		{
			return false;
		}
		if (_playerGuild.GuildId != 0L)
		{
			return false;
		}
		if (_playerGuild.LastJoinTime == 0L)
		{
			return false;
		}
		long num = (long)StaticGlobalData.GUILD_JOIN_CD * 24L * 3600;
		return MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds() - _playerGuild.LastJoinTime < num;
	}

	public bool IsInSearchCooldown()
	{
		int gUILD_SEARCH_CD = StaticGlobalData.GUILD_SEARCH_CD;
		if (gUILD_SEARCH_CD <= 0 || _lastSearchServerTime == DateTime.MinValue)
		{
			return false;
		}
		return (MonoSingletonProvider<NetManager>.inst.ServerTime - _lastSearchServerTime).TotalMilliseconds < (double)gUILD_SEARCH_CD;
	}

	public void Connect()
	{
		RPCMsgManager rPC = MonoSingletonProvider<NetManager>.inst.RPC;
		rPC.SyncPlayerGuildS2C.OnSyncPlayerGuildS2CServerCallBackAsync = OnSyncPlayerGuildS2C;
		rPC.SyncPlayerJoinGuildS2C.OnSyncPlayerJoinGuildS2CServerCallBackAsync = OnSyncPlayerJoinGuildS2C;
		rPC.CreateGuildS2C.OnCreateGuildS2CServerCallBackAsync = OnCreateGuildS2C;
		rPC.SearchGuildS2C.OnSearchGuildS2CServerCallBackAsync = OnSearchGuildS2C;
		rPC.ApplyToGuildS2C.OnApplyToGuildS2CServerCallBackAsync = OnApplyToGuildS2C;
		rPC.ProcessGuildInvitationS2C.OnProcessGuildInvitationS2CServerCallBackAsync = OnProcessGuildInvitationS2C;
		rPC.GetGuildsInfoS2C.OnGetGuildsInfoS2CServerCallBackAsync = OnGetGuildsInfoS2C;
		ConnectInternal();
	}

	public void Disconnect()
	{
		RPCMsgManager rPC = MonoSingletonProvider<NetManager>.inst.RPC;
		rPC.SyncPlayerGuildS2C.OnSyncPlayerGuildS2CServerCallBackAsync = null;
		rPC.SyncPlayerJoinGuildS2C.OnSyncPlayerJoinGuildS2CServerCallBackAsync = null;
		rPC.CreateGuildS2C.OnCreateGuildS2CServerCallBackAsync = null;
		rPC.SearchGuildS2C.OnSearchGuildS2CServerCallBackAsync = null;
		rPC.ApplyToGuildS2C.OnApplyToGuildS2CServerCallBackAsync = null;
		rPC.ProcessGuildInvitationS2C.OnProcessGuildInvitationS2CServerCallBackAsync = null;
		rPC.GetGuildsInfoS2C.OnGetGuildsInfoS2CServerCallBackAsync = null;
		DisconnectInternal();
		ResetInternalConnectionState();
	}

	public void InitFromServer(PlayerGuildInfo info)
	{
		ApplyPlayerGuild(info, GuildJoinedCause.None);
	}

	public void Clear()
	{
		_playerGuild = null;
		SearchResults.Clear();
		GuildCache.Clear();
		IsSearchEnd = false;
		SearchPage = 0;
		ClearInternalState();
	}

	private async UniTask OnSyncPlayerGuildS2C(SyncPlayerGuildS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model?.PlayerGuild != null)
		{
			ApplyPlayerGuild(model.PlayerGuild, GuildJoinedCause.None);
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnSyncPlayerJoinGuildS2C(SyncPlayerJoinGuildS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model?.PlayerGuild != null)
		{
			ApplyPlayerGuild(model.PlayerGuild, GuildJoinedCause.Sync);
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnCreateGuildS2C(CreateGuildS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model != null)
		{
			if (model.Guild != null)
			{
				GuildCache[model.Guild.Id] = model.Guild;
				signal.guildCacheChanged.Dispatch();
			}
			if (model.PlayerGuild != null)
			{
				ApplyPlayerGuild(model.PlayerGuild, GuildJoinedCause.Create);
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnSearchGuildS2C(SearchGuildS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || model == null)
		{
			return;
		}
		IsSearchEnd = model.IsEnd;
		SearchPage++;
		for (int i = 0; i < model.Guilds.Count; i++)
		{
			Guild g = model.Guilds[i];
			if (g != null)
			{
				GuildCache[g.Id] = g;
				if (!SearchResults.Exists((Guild x) => x.Id == g.Id))
				{
					SearchResults.Add(g);
				}
			}
		}
		signal.guildCacheChanged.Dispatch();
		signal.searchResultsChanged.Dispatch();
		await UniTask.CompletedTask;
	}

	private async UniTask OnApplyToGuildS2C(ApplyToGuildS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model != null)
		{
			if (model.PlayerGuild != null)
			{
				ApplyPlayerGuild(model.PlayerGuild, GuildJoinedCause.None);
				signal.searchResultsChanged.Dispatch();
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnProcessGuildInvitationS2C(ProcessGuildInvitationS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model != null)
		{
			if (model.Guild != null)
			{
				GuildCache[model.Guild.Id] = model.Guild;
				signal.guildCacheChanged.Dispatch();
			}
			if (model.PlayerGuild != null)
			{
				GuildJoinedCause cause = ((model.PlayerGuild.GuildId != 0L) ? GuildJoinedCause.AcceptInvitation : GuildJoinedCause.None);
				ApplyPlayerGuild(model.PlayerGuild, cause);
			}
			else
			{
				signal.invitationsChanged.Dispatch();
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnGetGuildsInfoS2C(GetGuildsInfoS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || model == null)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < model.Guilds.Count; i++)
		{
			Guild guild = model.Guilds[i];
			if (guild != null)
			{
				GuildCache[guild.Id] = guild;
				flag = true;
			}
		}
		if (flag)
		{
			signal.guildCacheChanged.Dispatch();
		}
		await UniTask.CompletedTask;
	}

	private void ApplyPlayerGuild(PlayerGuildInfo next, GuildJoinedCause cause)
	{
		if (next == null)
		{
			return;
		}
		long num = _playerGuild?.GuildId ?? 0;
		if (num != 0L && next.GuildId != 0L && num != next.GuildId)
		{
			Debug.LogError($"[GuildLogic] 拒绝非法公会状态跃迁：{num} -> {next.GuildId}。必须先退出当前公会，再加入新公会。");
			return;
		}
		bool num2 = num != 0;
		bool flag = next.GuildId != 0;
		_playerGuild = next;
		HandlePlayerGuildChanged(num, next.GuildId);
		bool num3 = flag && (next.GuildTasks.Count > 0 || (!_hasGuildTaskSnapshot && _guildTasks.Count == 0)) && ApplyGuildTaskSnapshot(next);
		signal.playerGuildUpdated.Dispatch(_playerGuild);
		signal.applicationsChanged.Dispatch();
		signal.invitationsChanged.Dispatch();
		if (num3)
		{
			signal.guildTasksChanged.Dispatch();
		}
		if (!num2 && flag && cause != GuildJoinedCause.None)
		{
			signal.joinedSuccess.Dispatch(cause);
		}
	}

	public void MarkSearchStarted()
	{
		_lastSearchServerTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
	}

	public RPCAsyncResult Search(string keyword, ICollection<int> selectedTagIds, bool resetPage, bool markSearchStarted = true)
	{
		SearchGuildC2S searchGuildC2S = new SearchGuildC2S();
		string text = keyword?.Trim() ?? string.Empty;
		if (!string.IsNullOrEmpty(text))
		{
			searchGuildC2S.Name = text;
		}
		if (selectedTagIds != null)
		{
			foreach (int selectedTagId in selectedTagIds)
			{
				searchGuildC2S.TagIds.Add(selectedTagId);
			}
		}
		if (resetPage)
		{
			SearchResults.Clear();
			SearchPage = 0;
			IsSearchEnd = false;
			if (markSearchStarted)
			{
				MarkSearchStarted();
			}
		}
		searchGuildC2S.Page = SearchPage + 1;
		return MonoSingletonProvider<NetManager>.inst.RPC.SearchGuildC2S.SearchGuildC2SCall(searchGuildC2S);
	}

	public Guild GetCachedGuild(long guildId)
	{
		if (!GuildCache.TryGetValue(guildId, out var value))
		{
			return null;
		}
		return value;
	}

	public void ResetSearch()
	{
		SearchResults.Clear();
		IsSearchEnd = false;
		SearchPage = 0;
	}

	public RPCAsyncResult RequestGuildSummaries(IList<long> guildIds)
	{
		if (guildIds == null || guildIds.Count == 0)
		{
			return null;
		}
		GetGuildsInfoC2S getGuildsInfoC2S = new GetGuildsInfoC2S();
		HashSet<long> hashSet = new HashSet<long>();
		for (int i = 0; i < guildIds.Count; i++)
		{
			long num = guildIds[i];
			if (num != 0L && !GuildCache.ContainsKey(num) && hashSet.Add(num))
			{
				getGuildsInfoC2S.GuildIds.Add(num);
			}
		}
		if (getGuildsInfoC2S.GuildIds.Count == 0)
		{
			return null;
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.GetGuildsInfoC2S.GetGuildsInfoC2SCall(getGuildsInfoC2S);
	}

	public bool AcceptExactSearchHit(long guildId)
	{
		if (guildId == 0L)
		{
			return false;
		}
		Guild cachedGuild = GetCachedGuild(guildId);
		if (cachedGuild == null)
		{
			return false;
		}
		SearchResults.Clear();
		SearchResults.Add(cachedGuild);
		IsSearchEnd = true;
		SearchPage = 1;
		return true;
	}

	public RPCAsyncResult Apply(long guildId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ApplyToGuildC2S.ApplyToGuildC2SCall(new ApplyToGuildC2S
		{
			GuildId = guildId
		});
	}

	public RPCAsyncResult AcceptInvitation(long guildId)
	{
		return SendProcessInvitation(new long[1] { guildId }, ProcessGuildInvitationC2S.Types.Action.Accept);
	}

	public RPCAsyncResult RejectInvitation(long guildId)
	{
		return SendProcessInvitation(new long[1] { guildId }, ProcessGuildInvitationC2S.Types.Action.Reject);
	}

	public RPCAsyncResult RejectInvitations(ICollection<long> guildIds)
	{
		if (guildIds == null)
		{
			return null;
		}
		List<long> list = new List<long>(guildIds.Count);
		foreach (long guildId in guildIds)
		{
			if (!list.Contains(guildId))
			{
				list.Add(guildId);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		return SendProcessInvitation(list, ProcessGuildInvitationC2S.Types.Action.Reject);
	}

	public RPCAsyncResult Create(string name, ICollection<int> tagIds, string exAnnouncement)
	{
		CreateGuildC2S createGuildC2S = new CreateGuildC2S
		{
			Name = (name?.Trim() ?? string.Empty),
			ExAnnouncement = (exAnnouncement ?? string.Empty)
		};
		if (tagIds != null)
		{
			foreach (int tagId in tagIds)
			{
				createGuildC2S.TagIds.Add(tagId);
			}
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.CreateGuildC2S.CreateGuildC2SCall(createGuildC2S);
	}

	private RPCAsyncResult SendProcessInvitation(IList<long> ids, ProcessGuildInvitationC2S.Types.Action action)
	{
		ProcessGuildInvitationC2S processGuildInvitationC2S = new ProcessGuildInvitationC2S
		{
			Action = action
		};
		if (ids != null)
		{
			foreach (long id in ids)
			{
				processGuildInvitationC2S.GuildIds.Add(id);
			}
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.ProcessGuildInvitationC2S.ProcessGuildInvitationC2SCall(processGuildInvitationC2S);
	}

	public static long GetGuildMasterId(Guild guild)
	{
		if (guild?.Members == null)
		{
			return 0L;
		}
		foreach (KeyValuePair<long, GuildMember> member in guild.Members)
		{
			if (member.Value != null && member.Value.Role == 1)
			{
				return member.Key;
			}
		}
		return 0L;
	}

	private void ConnectInternal()
	{
		RPCMsgManager rPC = MonoSingletonProvider<NetManager>.inst.RPC;
		rPC.SyncGuildS2C.OnSyncGuildS2CServerCallBackAsync = OnSyncGuildS2C;
		rPC.SyncGuildMemberS2C.OnSyncGuildMemberS2CServerCallBackAsync = OnSyncGuildMemberS2C;
		rPC.SyncGuildMemberExitS2C.OnSyncGuildMemberExitS2CServerCallBackAsync = OnSyncGuildMemberExitS2C;
		rPC.GetGuildInfoS2C.OnGetGuildInfoS2CServerCallBackAsync = OnGetGuildInfoS2C;
		rPC.UpdateGuildSettingsS2C.OnUpdateGuildSettingsS2CServerCallBackAsync = OnUpdateGuildSettingsS2C;
		rPC.UpdateGuildInAnnouncementS2C.OnUpdateGuildInAnnouncementS2CServerCallBackAsync = OnUpdateGuildInAnnouncementS2C;
		rPC.TransferGuildMasterS2C.OnTransferGuildMasterS2CServerCallBackAsync = OnTransferGuildMasterS2C;
		rPC.ChangeGuildMemberTitleS2C.OnChangeGuildMemberTitleS2CServerCallBackAsync = OnChangeGuildMemberTitleS2C;
		rPC.KickGuildMemberS2C.OnKickGuildMemberS2CServerCallBackAsync = OnKickGuildMemberS2C;
		rPC.ImpeachGuildMasterS2C.OnImpeachGuildMasterS2CServerCallBackAsync = OnImpeachGuildMasterS2C;
		rPC.ExitGuildS2C.OnExitGuildS2CServerCallBackAsync = OnExitGuildS2C;
		rPC.DisbandGuildS2C.OnDisbandGuildS2CServerCallBackAsync = OnDisbandGuildS2C;
		rPC.GetGuildMemberChangeMsgS2C.OnGetGuildMemberChangeMsgS2CServerCallBackAsync = OnGetGuildMemberChangeMsgS2C;
		rPC.GuildMemberS2C.OnGuildMemberS2CServerCallBackAsync = OnGuildMemberS2C;
		ConnectGuildTasks();
		ConnectInvitationApproval();
	}

	private void DisconnectInternal()
	{
		RPCMsgManager rPC = MonoSingletonProvider<NetManager>.inst.RPC;
		rPC.SyncGuildS2C.OnSyncGuildS2CServerCallBackAsync = null;
		rPC.SyncGuildMemberS2C.OnSyncGuildMemberS2CServerCallBackAsync = null;
		rPC.SyncGuildMemberExitS2C.OnSyncGuildMemberExitS2CServerCallBackAsync = null;
		rPC.GetGuildInfoS2C.OnGetGuildInfoS2CServerCallBackAsync = null;
		rPC.UpdateGuildSettingsS2C.OnUpdateGuildSettingsS2CServerCallBackAsync = null;
		rPC.UpdateGuildInAnnouncementS2C.OnUpdateGuildInAnnouncementS2CServerCallBackAsync = null;
		rPC.TransferGuildMasterS2C.OnTransferGuildMasterS2CServerCallBackAsync = null;
		rPC.ChangeGuildMemberTitleS2C.OnChangeGuildMemberTitleS2CServerCallBackAsync = null;
		rPC.KickGuildMemberS2C.OnKickGuildMemberS2CServerCallBackAsync = null;
		rPC.ImpeachGuildMasterS2C.OnImpeachGuildMasterS2CServerCallBackAsync = null;
		rPC.ExitGuildS2C.OnExitGuildS2CServerCallBackAsync = null;
		rPC.DisbandGuildS2C.OnDisbandGuildS2CServerCallBackAsync = null;
		rPC.GetGuildMemberChangeMsgS2C.OnGetGuildMemberChangeMsgS2CServerCallBackAsync = null;
		rPC.GuildMemberS2C.OnGuildMemberS2CServerCallBackAsync = null;
		DisconnectGuildTasks();
		DisconnectInvitationApproval();
	}

	private void ResetInternalConnectionState()
	{
		_pendingGuildSyncs.Clear();
		_hasCurrentGuildSnapshot = false;
		_hasMemberInfoSnapshot = false;
		_isCurrentGuildRequesting = false;
		_isMemberInfosRequesting = false;
		_isMemberMessagesRequesting = false;
		ResetGuildTaskConnectionState();
		ResetInvitationApprovalConnectionState();
	}

	private void ClearInternalState()
	{
		_currentGuild = null;
		_guildMemberInfos.Clear();
		_guildMemberChangeMessages.Clear();
		_pendingGuildSyncs.Clear();
		_hasCurrentGuildSnapshot = false;
		_hasMemberInfoSnapshot = false;
		_isCurrentGuildRequesting = false;
		_isMemberInfosRequesting = false;
		_isMemberMessagesRequesting = false;
		_expectedLeaveCause = GuildLeaveCause.NONE;
		_leaveDispatched = false;
		ClearGuildTaskState();
		ClearInvitationApprovalState();
	}

	private void HandlePlayerGuildChanged(long previousGuildId, long nextGuildId)
	{
		if (previousGuildId != nextGuildId)
		{
			if (previousGuildId != 0L && nextGuildId == 0L)
			{
				ApplyLeftGuild((_expectedLeaveCause == GuildLeaveCause.NONE) ? GuildLeaveCause.SERVER_SYNC : _expectedLeaveCause);
				return;
			}
			ClearGuildBusinessData();
			_leaveDispatched = false;
			_expectedLeaveCause = GuildLeaveCause.NONE;
		}
	}

	private void ApplyLeftGuild(GuildLeaveCause cause)
	{
		ClearGuildBusinessData();
		if (!_leaveDispatched)
		{
			_leaveDispatched = true;
			signal.leftGuild.Dispatch(cause);
		}
	}

	private void ApplySelfMemberExit()
	{
		PlayerGuildInfo playerGuildInfo = PlayerGuild?.Clone() ?? new PlayerGuildInfo();
		playerGuildInfo.GuildId = 0L;
		ApplyPlayerGuild(playerGuildInfo, GuildJoinedCause.None);
	}

	private void ClearGuildBusinessData()
	{
		_currentGuild = null;
		_guildMemberInfos.Clear();
		_guildMemberChangeMessages.Clear();
		_pendingGuildSyncs.Clear();
		_hasCurrentGuildSnapshot = false;
		_hasMemberInfoSnapshot = false;
		_isCurrentGuildRequesting = false;
		_isMemberInfosRequesting = false;
		_isMemberMessagesRequesting = false;
		ClearGuildTaskState();
		ClearInvitationApprovalState();
	}

	public RPCAsyncResult RequestCurrentGuild()
	{
		if (!IsJoined || _isCurrentGuildRequesting)
		{
			return null;
		}
		_isCurrentGuildRequesting = true;
		RPCAsyncResult guildInfoC2SCall = MonoSingletonProvider<NetManager>.inst.RPC.GetGuildInfoC2S.GetGuildInfoC2SCall(new GetGuildInfoC2S());
		if (guildInfoC2SCall == null)
		{
			_isCurrentGuildRequesting = false;
			return null;
		}
		guildInfoC2SCall.OnFinished.AddOnce(delegate
		{
			_isCurrentGuildRequesting = false;
		});
		return guildInfoC2SCall;
	}

	public RPCAsyncResult RequestMemberShowInfos()
	{
		if (!IsJoined || _isMemberInfosRequesting)
		{
			return null;
		}
		_isMemberInfosRequesting = true;
		RPCAsyncResult rPCAsyncResult = MonoSingletonProvider<NetManager>.inst.RPC.GuildMemberC2S.GuildMemberC2SCall(new GuildMemberC2S());
		if (rPCAsyncResult == null)
		{
			_isMemberInfosRequesting = false;
			return null;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate
		{
			_isMemberInfosRequesting = false;
		});
		return rPCAsyncResult;
	}

	public RPCAsyncResult RequestMemberChangeMessages(int msgId = 0)
	{
		if (!IsJoined || _isMemberMessagesRequesting)
		{
			return null;
		}
		_isMemberMessagesRequesting = true;
		RPCAsyncResult guildMemberChangeMsgC2SCall = MonoSingletonProvider<NetManager>.inst.RPC.GetGuildMemberChangeMsgC2S.GetGuildMemberChangeMsgC2SCall(new GetGuildMemberChangeMsgC2S
		{
			MsgId = msgId
		});
		if (guildMemberChangeMsgC2SCall == null)
		{
			_isMemberMessagesRequesting = false;
			return null;
		}
		guildMemberChangeMsgC2SCall.OnFinished.AddOnce(delegate
		{
			_isMemberMessagesRequesting = false;
		});
		return guildMemberChangeMsgC2SCall;
	}

	public static bool TryGetMemberChangeCursor(long messageId, out int cursor)
	{
		if (messageId < int.MinValue || messageId > int.MaxValue)
		{
			cursor = 0;
			return false;
		}
		cursor = (int)messageId;
		return true;
	}

	public RPCAsyncResult UpdateSettings(ICollection<int> tagIds, string exAnnouncement)
	{
		if (!CanEditSettings)
		{
			return null;
		}
		UpdateGuildSettingsC2S updateGuildSettingsC2S = new UpdateGuildSettingsC2S
		{
			ExAnnouncement = (exAnnouncement ?? string.Empty)
		};
		if (tagIds != null)
		{
			updateGuildSettingsC2S.TagIds.Add(tagIds);
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.UpdateGuildSettingsC2S.UpdateGuildSettingsC2SCall(updateGuildSettingsC2S);
	}

	public RPCAsyncResult UpdateInternalAnnouncement(string inAnnouncement)
	{
		if (!CanEditInternalAnnouncement)
		{
			return null;
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.UpdateGuildInAnnouncementC2S.UpdateGuildInAnnouncementC2SCall(new UpdateGuildInAnnouncementC2S
		{
			InAnnouncement = (inAnnouncement ?? string.Empty)
		});
	}

	public RPCAsyncResult TransferMaster(long targetPlayerId)
	{
		if (!CanTransferMaster(targetPlayerId))
		{
			return null;
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.TransferGuildMasterC2S.TransferGuildMasterC2SCall(new TransferGuildMasterC2S
		{
			TargetPlayerId = targetPlayerId
		});
	}

	public RPCAsyncResult ChangeMemberTitle(long targetPlayerId, GuildTitleType targetTitle)
	{
		if (!CanChangeMemberTitle(targetPlayerId, targetTitle))
		{
			return null;
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.ChangeGuildMemberTitleC2S.ChangeGuildMemberTitleC2SCall(new ChangeGuildMemberTitleC2S
		{
			TargetPlayerId = targetPlayerId,
			Role = (int)targetTitle
		});
	}

	public RPCAsyncResult KickMember(long targetPlayerId)
	{
		if (!CanKickMember(targetPlayerId))
		{
			return null;
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.KickGuildMemberC2S.KickGuildMemberC2SCall(new KickGuildMemberC2S
		{
			TargetPlayerId = targetPlayerId
		});
	}

	public RPCAsyncResult ImpeachMaster()
	{
		if (!CanImpeachMaster())
		{
			return null;
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.ImpeachGuildMasterC2S.ImpeachGuildMasterC2SCall(new ImpeachGuildMasterC2S());
	}

	public RPCAsyncResult ExitCurrentGuild()
	{
		if (!CanExitGuild)
		{
			return null;
		}
		_expectedLeaveCause = GuildLeaveCause.EXIT;
		RPCAsyncResult rPCAsyncResult = MonoSingletonProvider<NetManager>.inst.RPC.ExitGuildC2S.ExitGuildC2SCall(new ExitGuildC2S());
		if (rPCAsyncResult == null)
		{
			_expectedLeaveCause = GuildLeaveCause.NONE;
			return null;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			if (rpcResult.errId != 0 && IsJoined)
			{
				_expectedLeaveCause = GuildLeaveCause.NONE;
			}
		});
		return rPCAsyncResult;
	}

	public RPCAsyncResult DisbandCurrentGuild()
	{
		if (!CanDisbandGuild)
		{
			return null;
		}
		_expectedLeaveCause = GuildLeaveCause.DISBAND;
		RPCAsyncResult rPCAsyncResult = MonoSingletonProvider<NetManager>.inst.RPC.DisbandGuildC2S.DisbandGuildC2SCall(new DisbandGuildC2S());
		if (rPCAsyncResult == null)
		{
			_expectedLeaveCause = GuildLeaveCause.NONE;
			return null;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			if (rpcResult.errId != 0 && IsJoined)
			{
				_expectedLeaveCause = GuildLeaveCause.NONE;
			}
		});
		return rPCAsyncResult;
	}

	public GuildMember GetGuildMember(long playerId)
	{
		if (_currentGuild == null || playerId == 0L)
		{
			return null;
		}
		if (!_currentGuild.Members.TryGetValue(playerId, out var value))
		{
			return null;
		}
		return value;
	}

	public FriendShowPlayerInfo GetGuildMemberInfo(long playerId)
	{
		if (!_guildMemberInfos.TryGetValue(playerId, out var value))
		{
			return null;
		}
		return value;
	}

	public string GetGuildMemberName(long playerId)
	{
		FriendShowPlayerInfo guildMemberInfo = GetGuildMemberInfo(playerId);
		if (!string.IsNullOrEmpty(guildMemberInfo?.Name))
		{
			return guildMemberInfo.Name;
		}
		return GetGuildMember(playerId)?.PlayerName ?? string.Empty;
	}

	public bool CanTransferMaster(long targetPlayerId)
	{
		GuildMember selfGuildMember = SelfGuildMember;
		GuildMember guildMember = GetGuildMember(targetPlayerId);
		if (selfGuildMember != null && guildMember != null && targetPlayerId != SelfPlayerId && selfGuildMember.Role == 1)
		{
			return guildMember.Role == 2;
		}
		return false;
	}

	public bool CanPromoteMember(long targetPlayerId, out GuildTitleType targetTitle)
	{
		targetTitle = GuildTitleType.None;
		GuildMember selfGuildMember = SelfGuildMember;
		GuildMember guildMember = GetGuildMember(targetPlayerId);
		if (!CanManageMember(selfGuildMember, guildMember, targetPlayerId))
		{
			return false;
		}
		if (guildMember.Role - selfGuildMember.Role < 2 || guildMember.Role <= 2)
		{
			return false;
		}
		targetTitle = (GuildTitleType)(guildMember.Role - 1);
		if (targetTitle == GuildTitleType.ViceGuildMaster)
		{
			return CountMembersByTitle(GuildTitleType.ViceGuildMaster) < StaticGlobalData.GUILD_VICEGUILDMASTER_LIMIT;
		}
		return true;
	}

	public bool IsPromotionBlockedByViceMasterLimit(long targetPlayerId)
	{
		GuildMember selfGuildMember = SelfGuildMember;
		GuildMember guildMember = GetGuildMember(targetPlayerId);
		if (!CanManageMember(selfGuildMember, guildMember, targetPlayerId))
		{
			return false;
		}
		if (guildMember.Role - selfGuildMember.Role < 2 || guildMember.Role <= 2)
		{
			return false;
		}
		if (guildMember.Role - 1 == 2)
		{
			return CountMembersByTitle(GuildTitleType.ViceGuildMaster) >= StaticGlobalData.GUILD_VICEGUILDMASTER_LIMIT;
		}
		return false;
	}

	public bool CanDemoteMember(long targetPlayerId, out GuildTitleType targetTitle)
	{
		targetTitle = GuildTitleType.None;
		GuildMember selfGuildMember = SelfGuildMember;
		GuildMember guildMember = GetGuildMember(targetPlayerId);
		if (!CanManageMember(selfGuildMember, guildMember, targetPlayerId))
		{
			return false;
		}
		if (guildMember.Role <= selfGuildMember.Role || guildMember.Role >= 4)
		{
			return false;
		}
		targetTitle = (GuildTitleType)(guildMember.Role + 1);
		return true;
	}

	public bool CanChangeMemberTitle(long targetPlayerId, GuildTitleType targetTitle)
	{
		if (CanPromoteMember(targetPlayerId, out var targetTitle2) && targetTitle2 == targetTitle)
		{
			return true;
		}
		if (CanDemoteMember(targetPlayerId, out var targetTitle3))
		{
			return targetTitle3 == targetTitle;
		}
		return false;
	}

	public bool CanKickMember(long targetPlayerId)
	{
		GuildMember selfGuildMember = SelfGuildMember;
		GuildMember guildMember = GetGuildMember(targetPlayerId);
		if (selfGuildMember == null || guildMember == null || targetPlayerId == SelfPlayerId)
		{
			return false;
		}
		GuildAuthorityConfigure authority = GetAuthority((GuildTitleType)selfGuildMember.Role);
		if (authority != null && authority.CanKickFromGuild)
		{
			return guildMember.Role > selfGuildMember.Role;
		}
		return false;
	}

	public bool CanImpeachMaster()
	{
		GuildMember selfGuildMember = SelfGuildMember;
		if (selfGuildMember == null || selfGuildMember.Role == 1)
		{
			return false;
		}
		GuildAuthorityConfigure authority = GetAuthority((GuildTitleType)selfGuildMember.Role);
		if (authority == null || !authority.CanImpeach)
		{
			return false;
		}
		long guildMasterId = GetGuildMasterId(_currentGuild);
		GuildMember guildMember = GetGuildMember(guildMasterId);
		FriendShowPlayerInfo guildMemberInfo = GetGuildMemberInfo(guildMasterId);
		bool flag = guildMemberInfo?.IsOnline ?? guildMember?.Online ?? false;
		if (guildMember == null || flag)
		{
			return false;
		}
		long num = ((guildMemberInfo != null && guildMemberInfo.OfflineTime > 0) ? guildMemberInfo.OfflineTime : guildMember.LastLoginTime);
		long num2 = (long)StaticGlobalData.GUILD_IMPEACH_MASTEROFFLINEDAYS * 24L * 3600;
		long num3 = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds();
		if (num > 0)
		{
			return num3 - num >= num2;
		}
		return false;
	}

	public bool HasAnyMemberOperation(long targetPlayerId)
	{
		if (targetPlayerId == SelfPlayerId)
		{
			return CanExitGuild;
		}
		if (!CanTransferMaster(targetPlayerId) && !CanPromoteMember(targetPlayerId, out var targetTitle) && !CanDemoteMember(targetPlayerId, out targetTitle) && !CanKickMember(targetPlayerId))
		{
			GuildMember guildMember = GetGuildMember(targetPlayerId);
			if (guildMember != null && guildMember.Role == 1)
			{
				return CanImpeachMaster();
			}
			return false;
		}
		return true;
	}

	private bool CanManageMember(GuildMember self, GuildMember target, long targetPlayerId)
	{
		if (self == null || target == null || targetPlayerId == 0L || targetPlayerId == SelfPlayerId)
		{
			return false;
		}
		return GetAuthority((GuildTitleType)self.Role)?.CanPromoteOrDemote ?? false;
	}

	private int CountMembersByTitle(GuildTitleType title)
	{
		if (_currentGuild == null)
		{
			return 0;
		}
		int num = 0;
		foreach (KeyValuePair<long, GuildMember> member in _currentGuild.Members)
		{
			if (member.Value?.Role == (int?)title)
			{
				num++;
			}
		}
		return num;
	}

	private GuildAuthorityConfigure GetSelfAuthority()
	{
		if (SelfGuildMember != null)
		{
			return GetAuthority((GuildTitleType)SelfGuildMember.Role);
		}
		return null;
	}

	private static GuildAuthorityConfigure GetAuthority(GuildTitleType title)
	{
		GuildConfigure guild = StaticConfigure.Guild;
		if (guild == null || !guild.AuthorityDict.TryGetValue((int)title, out var value))
		{
			return null;
		}
		return value;
	}

	private async UniTask OnGetGuildInfoS2C(GetGuildInfoS2C model, int errId, bool isDispatch)
	{
		_isCurrentGuildRequesting = false;
		if (errId == 0 && IsJoined)
		{
			if (model?.Guild == null || model.Guild.Id == 0L)
			{
				ApplySelfMemberExit();
				return;
			}
			if (model.Guild.Id != PlayerGuild.GuildId)
			{
				Debug.LogError($"[GuildLogic] 公会详情 ID 不一致：player={PlayerGuild.GuildId}, detail={model.Guild.Id}");
				return;
			}
			_currentGuild = model.Guild;
			_hasCurrentGuildSnapshot = true;
			CacheCurrentGuild();
			ApplyPendingGuildSyncs();
			RefreshValidGuildApplications();
			signal.currentGuildChanged.Dispatch();
			signal.guildMembersChanged.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnSyncGuildS2C(SyncGuildS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model != null && IsJoined)
		{
			if (!_hasCurrentGuildSnapshot)
			{
				_pendingGuildSyncs.Add(PendingGuildSync.ForGuild(model.Clone()));
				RequestCurrentGuild();
			}
			else
			{
				MergeGuildSync(model);
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnSyncGuildMemberS2C(SyncGuildMemberS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model != null && IsJoined)
		{
			if (!_hasCurrentGuildSnapshot)
			{
				_pendingGuildSyncs.Add(PendingGuildSync.ForMembers(model.Clone()));
				RequestCurrentGuild();
			}
			else
			{
				MergeMemberSync(model);
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnSyncGuildMemberExitS2C(SyncGuildMemberExitS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || model == null || !IsJoined)
		{
			return;
		}
		for (int i = 0; i < model.PlayerIds.Count; i++)
		{
			if (model.PlayerIds[i] == SelfPlayerId)
			{
				ApplySelfMemberExit();
				await UniTask.CompletedTask;
				return;
			}
		}
		if (!_hasCurrentGuildSnapshot)
		{
			_pendingGuildSyncs.Add(PendingGuildSync.ForExits(model.Clone()));
			RequestCurrentGuild();
		}
		else
		{
			MergeMemberExitSync(model);
		}
		await UniTask.CompletedTask;
	}

	private async UniTask OnGuildMemberS2C(GuildMemberS2C model, int errId, bool isDispatch)
	{
		_isMemberInfosRequesting = false;
		if (errId != 0 || model == null || !IsJoined)
		{
			return;
		}
		_guildMemberInfos.Clear();
		for (int i = 0; i < model.Infos.Count; i++)
		{
			FriendShowPlayerInfo friendShowPlayerInfo = model.Infos[i];
			if (friendShowPlayerInfo != null && friendShowPlayerInfo.PlayerId != 0L)
			{
				_guildMemberInfos[friendShowPlayerInfo.PlayerId] = friendShowPlayerInfo;
			}
		}
		_hasMemberInfoSnapshot = true;
		signal.guildMembersChanged.Dispatch();
		await UniTask.CompletedTask;
	}

	private async UniTask OnGetGuildMemberChangeMsgS2C(GetGuildMemberChangeMsgS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || model == null || !IsJoined)
		{
			return;
		}
		Dictionary<long, GuildMemberChangeMsg> dictionary = new Dictionary<long, GuildMemberChangeMsg>();
		for (int i = 0; i < _guildMemberChangeMessages.Count; i++)
		{
			GuildMemberChangeMsg guildMemberChangeMsg = _guildMemberChangeMessages[i];
			if (guildMemberChangeMsg != null)
			{
				dictionary[guildMemberChangeMsg.Id] = guildMemberChangeMsg;
			}
		}
		for (int j = 0; j < model.Messages.Count; j++)
		{
			GuildMemberChangeMsg guildMemberChangeMsg2 = model.Messages[j];
			if (guildMemberChangeMsg2 != null)
			{
				dictionary[guildMemberChangeMsg2.Id] = guildMemberChangeMsg2;
			}
		}
		_guildMemberChangeMessages.Clear();
		_guildMemberChangeMessages.AddRange(dictionary.Values);
		_guildMemberChangeMessages.Sort(delegate(GuildMemberChangeMsg left, GuildMemberChangeMsg right)
		{
			int num = right.Time.CompareTo(left.Time);
			return (num == 0) ? right.Id.CompareTo(left.Id) : num;
		});
		signal.memberChangeMessagesChanged.Dispatch();
		await UniTask.CompletedTask;
	}

	private async UniTask OnUpdateGuildSettingsS2C(UpdateGuildSettingsS2C model, int errId, bool isDispatch)
	{
		await DispatchManagementSuccess(errId, GuildManagementOperation.UPDATE_SETTINGS);
	}

	private async UniTask OnUpdateGuildInAnnouncementS2C(UpdateGuildInAnnouncementS2C model, int errId, bool isDispatch)
	{
		await DispatchManagementSuccess(errId, GuildManagementOperation.UPDATE_INTERNAL_ANNOUNCEMENT);
	}

	private async UniTask OnTransferGuildMasterS2C(TransferGuildMasterS2C model, int errId, bool isDispatch)
	{
		await DispatchManagementSuccess(errId, GuildManagementOperation.TRANSFER_MASTER);
	}

	private async UniTask OnChangeGuildMemberTitleS2C(ChangeGuildMemberTitleS2C model, int errId, bool isDispatch)
	{
		await DispatchManagementSuccess(errId, GuildManagementOperation.CHANGE_MEMBER_TITLE);
	}

	private async UniTask OnKickGuildMemberS2C(KickGuildMemberS2C model, int errId, bool isDispatch)
	{
		await DispatchManagementSuccess(errId, GuildManagementOperation.KICK_MEMBER);
	}

	private async UniTask OnImpeachGuildMasterS2C(ImpeachGuildMasterS2C model, int errId, bool isDispatch)
	{
		await DispatchManagementSuccess(errId, GuildManagementOperation.IMPEACH_MASTER);
	}

	private async UniTask OnExitGuildS2C(ExitGuildS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			_expectedLeaveCause = GuildLeaveCause.EXIT;
			signal.managementSucceeded.Dispatch(GuildManagementOperation.EXIT_GUILD);
		}
		await UniTask.CompletedTask;
	}

	private async UniTask OnDisbandGuildS2C(DisbandGuildS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			_expectedLeaveCause = GuildLeaveCause.DISBAND;
			signal.managementSucceeded.Dispatch(GuildManagementOperation.DISBAND_GUILD);
		}
		await UniTask.CompletedTask;
	}

	private async UniTask DispatchManagementSuccess(int errId, GuildManagementOperation operation)
	{
		if (errId == 0)
		{
			signal.managementSucceeded.Dispatch(operation);
		}
		await UniTask.CompletedTask;
	}

	private void MergeGuildSync(SyncGuildS2C model)
	{
		if (_currentGuild != null)
		{
			_currentGuild.TagIds.Clear();
			_currentGuild.TagIds.Add(model.TagIds);
			_currentGuild.ExAnnouncement = model.ExAnnouncement;
			_currentGuild.LastExternalEditTime = model.LastExternalEditTime;
			_currentGuild.LastExternalEditorId = model.LastExternalEditorId;
			_currentGuild.ExternalEditCount = model.ExternalEditCount;
			_currentGuild.InAnnouncement = model.InAnnouncement;
			_currentGuild.LastInternalEditTime = model.LastInternalEditTime;
			_currentGuild.LastInternalEditorId = model.LastInternalEditorId;
			_currentGuild.InternalEditCount = model.InternalEditCount;
			CacheCurrentGuild();
			signal.currentGuildChanged.Dispatch();
		}
	}

	private void MergeMemberSync(SyncGuildMemberS2C model)
	{
		if (_currentGuild == null)
		{
			return;
		}
		foreach (KeyValuePair<long, GuildMember> member in model.Members)
		{
			_currentGuild.Members[member.Key] = member.Value;
		}
		foreach (KeyValuePair<long, FriendShowPlayerInfo> info in model.Infos)
		{
			_guildMemberInfos[info.Key] = info.Value;
		}
		_currentGuild.MemberCount = _currentGuild.Members.Count;
		CacheCurrentGuild();
		signal.currentGuildChanged.Dispatch();
		signal.guildMembersChanged.Dispatch();
	}

	private void MergeMemberExitSync(SyncGuildMemberExitS2C model)
	{
		if (_currentGuild != null)
		{
			for (int i = 0; i < model.PlayerIds.Count; i++)
			{
				long key = model.PlayerIds[i];
				_currentGuild.Members.Remove(key);
				_guildMemberInfos.Remove(key);
			}
			_currentGuild.MemberCount = _currentGuild.Members.Count;
			CacheCurrentGuild();
			signal.currentGuildChanged.Dispatch();
			signal.guildMembersChanged.Dispatch();
		}
	}

	private void ApplyPendingGuildSyncs()
	{
		if (_currentGuild == null || _pendingGuildSyncs.Count == 0)
		{
			return;
		}
		PendingGuildSync[] array = _pendingGuildSyncs.ToArray();
		_pendingGuildSyncs.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Guild != null)
			{
				MergeGuildSync(array[i].Guild);
			}
			else if (array[i].Members != null)
			{
				MergeMemberSync(array[i].Members);
			}
			else if (array[i].Exits != null)
			{
				MergeMemberExitSync(array[i].Exits);
			}
		}
	}

	private void CacheCurrentGuild()
	{
		if (_currentGuild != null)
		{
			GuildCache[_currentGuild.Id] = _currentGuild;
			signal.guildCacheChanged.Dispatch();
		}
	}

	private void ConnectInvitationApproval()
	{
		RPCMsgManager rPC = MonoSingletonProvider<NetManager>.inst.RPC;
		rPC.ProcessGuildApplicationS2C.OnProcessGuildApplicationS2CServerCallBackAsync = OnProcessGuildApplicationS2C;
		rPC.SendGuildInvitationS2C.OnSendGuildInvitationS2CServerCallBackAsync = OnSendGuildInvitationS2C;
	}

	private void DisconnectInvitationApproval()
	{
		RPCMsgManager rPC = MonoSingletonProvider<NetManager>.inst.RPC;
		rPC.ProcessGuildApplicationS2C.OnProcessGuildApplicationS2CServerCallBackAsync = null;
		rPC.SendGuildInvitationS2C.OnSendGuildInvitationS2CServerCallBackAsync = null;
	}

	private void ResetInvitationApprovalConnectionState()
	{
		_invitationRequests.Clear();
		_applicationRequests.Clear();
	}

	private void ClearInvitationApprovalState()
	{
		_validGuildApplications.Clear();
		_invitationRequests.Clear();
		_applicationRequests.Clear();
	}

	private async UniTask OnProcessGuildApplicationS2C(ProcessGuildApplicationS2C model, int errId, bool isDispatch)
	{
		await UniTask.CompletedTask;
	}

	private async UniTask OnSendGuildInvitationS2C(SendGuildInvitationS2C model, int errId, bool isDispatch)
	{
		await UniTask.CompletedTask;
	}

	public void RefreshValidGuildApplications()
	{
		_validGuildApplications.Clear();
		if (_currentGuild?.Applications != null)
		{
			long num = (long)StaticGlobalData.GUILD_APPLICATIONS_TERM * 24L * 3600;
			long num2 = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds() - num;
			foreach (KeyValuePair<long, GuildApplication> application in _currentGuild.Applications)
			{
				GuildApplication value = application.Value;
				if (value != null && value.PlayerId != 0L && value.ApplyTime >= num2)
				{
					_validGuildApplications.Add(value);
				}
			}
			_validGuildApplications.Sort(delegate(GuildApplication left, GuildApplication right)
			{
				int num3 = right.ApplyTime.CompareTo(left.ApplyTime);
				return (num3 == 0) ? left.PlayerId.CompareTo(right.PlayerId) : num3;
			});
		}
		signal.guildApplicationsChanged.Dispatch();
	}

	private bool HasValidGuildApplication(long playerId)
	{
		for (int i = 0; i < _validGuildApplications.Count; i++)
		{
			if (_validGuildApplications[i].PlayerId == playerId)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsGuildInvitationActive(long playerId)
	{
		if (playerId == 0L || PlayerGuild == null)
		{
			return false;
		}
		if (!PlayerGuild.Invitations.TryGetValue(playerId, out var value))
		{
			return false;
		}
		long num = (long)StaticGlobalData.GUILD_INVITETOGUILD_TERM * 24L * 3600;
		long num2 = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds() - num;
		return value >= num2;
	}

	public RPCAsyncResult SendGuildInvitation(long playerId)
	{
		if (!CanInvite || playerId == 0L || playerId == SelfPlayerId)
		{
			return null;
		}
		if (GetGuildMember(playerId) != null || IsGuildInvitationActive(playerId))
		{
			return null;
		}
		if (!_invitationRequests.Add(playerId))
		{
			return null;
		}
		RPCAsyncResult rPCAsyncResult = MonoSingletonProvider<NetManager>.inst.RPC.SendGuildInvitationC2S.SendGuildInvitationC2SCall(new SendGuildInvitationC2S
		{
			PlayerId = playerId
		});
		if (rPCAsyncResult == null)
		{
			_invitationRequests.Remove(playerId);
			return null;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			_invitationRequests.Remove(playerId);
			if (rpcResult.errId == 0)
			{
				signal.invitationApprovalSucceeded.Dispatch(GuildInvitationApprovalOperation.SEND_INVITATION);
			}
		});
		return rPCAsyncResult;
	}

	public bool IsGuildInvitationRequesting(long playerId)
	{
		return _invitationRequests.Contains(playerId);
	}

	public RPCAsyncResult AcceptGuildApplication(long playerId)
	{
		if (!CanAcceptGuildApplication(playerId))
		{
			return null;
		}
		return SendGuildApplicationRequest(new long[1] { playerId }, ProcessGuildApplicationC2S.Types.Action.Accept, GuildInvitationApprovalOperation.ACCEPT_APPLICATION);
	}

	public RPCAsyncResult RejectGuildApplication(long playerId)
	{
		if (!CanRejectGuildApplication(playerId))
		{
			return null;
		}
		return SendGuildApplicationRequest(new long[1] { playerId }, ProcessGuildApplicationC2S.Types.Action.Reject, GuildInvitationApprovalOperation.REJECT_APPLICATION);
	}

	public RPCAsyncResult RejectAllGuildApplications()
	{
		if (!CanApproveApplications || _validGuildApplications.Count == 0)
		{
			return null;
		}
		List<long> list = new List<long>(_validGuildApplications.Count);
		for (int i = 0; i < _validGuildApplications.Count; i++)
		{
			long playerId = _validGuildApplications[i].PlayerId;
			if (playerId != 0L && !_applicationRequests.Contains(playerId))
			{
				list.Add(playerId);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		return SendGuildApplicationRequest(list, ProcessGuildApplicationC2S.Types.Action.Reject, GuildInvitationApprovalOperation.REJECT_ALL_APPLICATIONS);
	}

	public bool IsGuildApplicationRequesting(long playerId)
	{
		return _applicationRequests.Contains(playerId);
	}

	private bool CanAcceptGuildApplication(long playerId)
	{
		if (!CanRejectGuildApplication(playerId) || _currentGuild == null)
		{
			return false;
		}
		return _currentGuild.MemberCount < StaticGlobalData.GUILD_MEMBER_LIMIT;
	}

	private bool CanRejectGuildApplication(long playerId)
	{
		if (CanApproveApplications && playerId != 0L && !_applicationRequests.Contains(playerId))
		{
			return HasValidGuildApplication(playerId);
		}
		return false;
	}

	private RPCAsyncResult SendGuildApplicationRequest(ICollection<long> playerIds, ProcessGuildApplicationC2S.Types.Action action, GuildInvitationApprovalOperation operation)
	{
		if (playerIds == null || playerIds.Count == 0)
		{
			return null;
		}
		ProcessGuildApplicationC2S request = new ProcessGuildApplicationC2S
		{
			Action = action
		};
		foreach (long playerId in playerIds)
		{
			if (playerId != 0L && _applicationRequests.Add(playerId))
			{
				request.PlayerIds.Add(playerId);
			}
		}
		if (request.PlayerIds.Count == 0)
		{
			return null;
		}
		RPCAsyncResult rPCAsyncResult = MonoSingletonProvider<NetManager>.inst.RPC.ProcessGuildApplicationC2S.ProcessGuildApplicationC2SCall(request);
		if (rPCAsyncResult == null)
		{
			ReleaseGuildApplicationRequests(request.PlayerIds);
			return null;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			ReleaseGuildApplicationRequests(request.PlayerIds);
			if (rpcResult.errId == 0)
			{
				signal.invitationApprovalSucceeded.Dispatch(operation);
			}
		});
		return rPCAsyncResult;
	}

	private void ReleaseGuildApplicationRequests(IEnumerable<long> playerIds)
	{
		foreach (long playerId in playerIds)
		{
			_applicationRequests.Remove(playerId);
		}
	}

	private void ConnectGuildTasks()
	{
		RPCMsgManager rPC = MonoSingletonProvider<NetManager>.inst.RPC;
		rPC.GuildTaskNotifyS2C.OnGuildTaskNotifyS2CServerCallBackAsync = OnGuildTaskNotifyS2C;
		rPC.GuildMissionRewardS2C.OnGuildMissionRewardS2CServerCallBackAsync = OnGuildMissionRewardS2C;
	}

	private void DisconnectGuildTasks()
	{
		RPCMsgManager rPC = MonoSingletonProvider<NetManager>.inst.RPC;
		rPC.GuildTaskNotifyS2C.OnGuildTaskNotifyS2CServerCallBackAsync = null;
		rPC.GuildMissionRewardS2C.OnGuildMissionRewardS2CServerCallBackAsync = null;
	}

	private void ResetGuildTaskConnectionState()
	{
		_guildMissionRewardRequests.Clear();
		_hasGuildTaskSnapshot = false;
	}

	private void ClearGuildTaskState()
	{
		_guildTasks.Clear();
		_guildTaskViews.Clear();
		_guildMissionRewardRequests.Clear();
		_hasGuildTaskSnapshot = false;
	}

	private bool ApplyGuildTaskSnapshot(PlayerGuildInfo playerGuild)
	{
		if (playerGuild == null)
		{
			return false;
		}
		bool flag = _guildTasks.Count != playerGuild.GuildTasks.Count;
		if (!flag)
		{
			foreach (KeyValuePair<int, TaskDSO> guildTask in playerGuild.GuildTasks)
			{
				if (!_guildTasks.TryGetValue(guildTask.Key, out var value) || !object.Equals(value, guildTask.Value))
				{
					flag = true;
					break;
				}
			}
		}
		_guildTasks.Clear();
		foreach (KeyValuePair<int, TaskDSO> guildTask2 in playerGuild.GuildTasks)
		{
			if (guildTask2.Key != 0 && guildTask2.Value != null)
			{
				_guildTasks[guildTask2.Key] = guildTask2.Value.Clone();
			}
		}
		_hasGuildTaskSnapshot = true;
		RebuildGuildTaskViews();
		return flag;
	}

	private async UniTask OnGuildTaskNotifyS2C(GuildTaskNotifyS2C model, int errId, bool isDispatch)
	{
		if (errId != 0 || model == null || !IsJoined)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < model.Tasks.Count; i++)
		{
			TaskDSO taskDSO = model.Tasks[i];
			if (taskDSO != null && taskDSO.Id != 0 && (!_guildTasks.TryGetValue(taskDSO.Id, out var value) || !object.Equals(value, taskDSO)))
			{
				_guildTasks[taskDSO.Id] = taskDSO.Clone();
				flag = true;
			}
		}
		_hasGuildTaskSnapshot = true;
		if (flag)
		{
			RebuildGuildTaskViews();
			signal.guildTasksChanged.Dispatch();
		}
		await UniTask.CompletedTask;
	}

	private async UniTask OnGuildMissionRewardS2C(GuildMissionRewardS2C model, int errId, bool isDispatch)
	{
		if (model != null)
		{
			_guildMissionRewardRequests.Remove(model.TaskId);
		}
		if (errId == 0)
		{
			if (model != null && model.TaskId > 0)
			{
				signal.guildMissionRewardSucceeded.Dispatch(model.TaskId);
			}
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestGuildMissionReward(int taskId)
	{
		if (!IsJoined || taskId <= 0 || _guildMissionRewardRequests.Contains(taskId))
		{
			return null;
		}
		if (!_guildTasks.ContainsKey(taskId))
		{
			return null;
		}
		if (StaticConfigure.Guild == null || !StaticConfigure.Guild.MissionDict.ContainsKey(taskId))
		{
			return null;
		}
		_guildMissionRewardRequests.Add(taskId);
		RPCAsyncResult rPCAsyncResult = MonoSingletonProvider<NetManager>.inst.RPC.GuildMissionRewardC2S.GuildMissionRewardC2SCall(new GuildMissionRewardC2S
		{
			TaskId = taskId
		});
		if (rPCAsyncResult == null)
		{
			_guildMissionRewardRequests.Remove(taskId);
			return null;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate
		{
			_guildMissionRewardRequests.Remove(taskId);
		});
		return rPCAsyncResult;
	}

	public bool IsGuildMissionRewardRequesting(int taskId)
	{
		return _guildMissionRewardRequests.Contains(taskId);
	}

	private void RebuildGuildTaskViews()
	{
		_guildTaskViews.Clear();
		GuildConfigure guild = StaticConfigure.Guild;
		if (guild == null)
		{
			return;
		}
		foreach (KeyValuePair<int, TaskDSO> guildTask in _guildTasks)
		{
			if (guild.MissionDict.TryGetValue(guildTask.Key, out var value) && value != null)
			{
				_guildTaskViews.Add(new GuildTaskView(value, guildTask.Value));
			}
		}
		_guildTaskViews.Sort(delegate(GuildTaskView left, GuildTaskView right)
		{
			int num = left.Configure.GuildMissionType.CompareTo(right.Configure.GuildMissionType);
			if (num != 0)
			{
				return num;
			}
			int num2 = left.Configure.OrderWeight.CompareTo(right.Configure.OrderWeight);
			return (num2 == 0) ? left.Configure.Id.CompareTo(right.Configure.Id) : num2;
		});
	}
}
