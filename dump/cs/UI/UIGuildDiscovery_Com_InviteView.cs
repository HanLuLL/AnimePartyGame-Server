using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class UIGuildDiscovery_Com_InviteView : GComponent
{
	private struct InvitationSnapshot
	{
		public long GuildId;

		public long InviterId;

		public long InvitationTime;
	}

	private readonly List<InvitationSnapshot> _snapshots = new List<InvitationSnapshot>();

	private long _selectedGuildId;

	private bool _listRendererBound;

	private bool _isBusy;

	private bool _isFetchingSummary;

	private readonly HashSet<long> _pendingSummaryIds = new HashSet<long>();

	public Action<bool> OnLoadingMask;

	public GList list_ReceivedInvites;

	public UIGuildDiscovery_Com_GuildDetai com_DetailPanel;

	public UIGuildDiscovery_Common_Button btn_AcceptInvite;

	public UIGuildDiscovery_Common_Button btn_RejectInvite;

	public UIGuildDiscovery_Common_Button btn_RejectAllInvites;

	public const string URL = "ui://gldisc01gd008";

	public void Init()
	{
		_listRendererBound = false;
		BindInvitesList();
	}

	public void OnShow()
	{
		_selectedGuildId = 0L;
		_isBusy = false;
		RebuildSnapshots();
		RefreshList();
		RefreshDetail();
		UpdateAllButtons();
		FetchMissingSummariesIfNeeded();
	}

	public void AddEvent()
	{
		list_ReceivedInvites.onClickItem.Add(OnInviteListItemClicked);
		btn_AcceptInvite.onClick.Add(OnClickAccept);
		btn_RejectInvite.onClick.Add(OnClickReject);
		btn_RejectAllInvites.onClick.Add(OnClickRejectAll);
	}

	public void RemoveEvent()
	{
		list_ReceivedInvites.onClickItem.Remove(OnInviteListItemClicked);
		btn_AcceptInvite.onClick.Remove(OnClickAccept);
		btn_RejectInvite.onClick.Remove(OnClickReject);
		btn_RejectAllInvites.onClick.Remove(OnClickRejectAll);
	}

	public void AddListener()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			guild.signal.playerGuildUpdated.AddListener(OnPlayerGuildUpdated);
			guild.signal.invitationsChanged.AddListener(OnInvitationsChanged);
			guild.signal.guildCacheChanged.AddListener(OnGuildCacheChanged);
		}
	}

	public void RemoveListener()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			guild.signal.playerGuildUpdated.RemoveListener(OnPlayerGuildUpdated);
			guild.signal.invitationsChanged.RemoveListener(OnInvitationsChanged);
			guild.signal.guildCacheChanged.RemoveListener(OnGuildCacheChanged);
		}
	}

	public void ClearData()
	{
		_snapshots.Clear();
		_selectedGuildId = 0L;
		EndBusy();
		_isFetchingSummary = false;
		_pendingSummaryIds.Clear();
	}

	public void RefreshRedPoints()
	{
	}

	private void BindInvitesList()
	{
		GList gList = list_ReceivedInvites;
		if (gList != null)
		{
			if (!_listRendererBound)
			{
				gList.itemRenderer = OnRenderInviteItem;
				_listRendererBound = true;
			}
			gList.numItems = 0;
		}
	}

	private void RebuildSnapshots()
	{
		_snapshots.Clear();
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null)
		{
			_selectedGuildId = 0L;
			return;
		}
		PlayerGuildInfo playerGuild = guild.PlayerGuild;
		if (playerGuild == null || playerGuild.ReceivedInvitations == null || playerGuild.ReceivedInvitations.Count == 0)
		{
			_selectedGuildId = 0L;
			return;
		}
		long num = (long)StaticGlobalData.GUILD_INVITETOGUILD_TERM * 24L * 3600;
		long num2 = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds() - num;
		foreach (KeyValuePair<long, GuildInvitation> receivedInvitation in playerGuild.ReceivedInvitations)
		{
			if (receivedInvitation.Key != 0L)
			{
				GuildInvitation value = receivedInvitation.Value;
				if (value != null && value.InvitationTime >= num2)
				{
					_snapshots.Add(new InvitationSnapshot
					{
						GuildId = value.GuildId,
						InviterId = value.InviterId,
						InvitationTime = value.InvitationTime
					});
				}
			}
		}
		_snapshots.Sort(delegate(InvitationSnapshot a, InvitationSnapshot b)
		{
			int num3 = b.InvitationTime.CompareTo(a.InvitationTime);
			return (num3 != 0) ? num3 : a.GuildId.CompareTo(b.GuildId);
		});
		if (_selectedGuildId != 0L && !_snapshots.Exists((InvitationSnapshot s) => s.GuildId == _selectedGuildId))
		{
			_selectedGuildId = 0L;
		}
	}

	private void RefreshList()
	{
		if (list_ReceivedInvites != null)
		{
			list_ReceivedInvites.numItems = _snapshots.Count;
		}
	}

	private void OnRenderInviteItem(int index, GObject item)
	{
		if (!(item is UIGuildDiscovery_Com_GuildListItem uIGuildDiscovery_Com_GuildListItem))
		{
			return;
		}
		if (index < 0 || index >= _snapshots.Count)
		{
			uIGuildDiscovery_Com_GuildListItem.SetData(null, isApplied: false, 1, 0L);
			return;
		}
		InvitationSnapshot invitationSnapshot = _snapshots[index];
		Guild guild = SimpleSingletonProvider<GameLogicManager>.inst.guild?.GetCachedGuild(invitationSnapshot.GuildId);
		uIGuildDiscovery_Com_GuildListItem.SetData(guild, isApplied: false, 1, invitationSnapshot.GuildId);
		uIGuildDiscovery_Com_GuildListItem.ApplySelection(invitationSnapshot.GuildId == _selectedGuildId);
		string text = string.Empty;
		if (invitationSnapshot.InvitationTime > 0)
		{
			DateTime originUtcDT = TimeUtils.OriginUtcDT;
			text = originUtcDT.AddSeconds(invitationSnapshot.InvitationTime).ToString("yyyy.MM.dd");
		}
		uIGuildDiscovery_Com_GuildListItem.txt_Time.text = text;
	}

	private void OnInviteListItemClicked(EventContext context)
	{
		if (context.data is UIGuildDiscovery_Com_GuildListItem uIGuildDiscovery_Com_GuildListItem)
		{
			OnInviteItemClicked(uIGuildDiscovery_Com_GuildListItem.GuildId);
		}
	}

	private void OnInviteItemClicked(long guildId)
	{
		if (guildId != 0L)
		{
			_selectedGuildId = guildId;
			RefreshList();
			RefreshDetail();
			UpdateAllButtons();
		}
	}

	private void FetchMissingSummariesIfNeeded()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null || _isFetchingSummary)
		{
			return;
		}
		List<long> list = new List<long>();
		HashSet<long> currentIds = new HashSet<long>();
		for (int i = 0; i < _snapshots.Count; i++)
		{
			long guildId = _snapshots[i].GuildId;
			if (guildId != 0L)
			{
				currentIds.Add(guildId);
				if (guild.GetCachedGuild(guildId) == null && !_pendingSummaryIds.Contains(guildId))
				{
					list.Add(guildId);
				}
			}
		}
		_pendingSummaryIds.RemoveWhere((long id) => !currentIds.Contains(id));
		if (list.Count == 0)
		{
			return;
		}
		_isFetchingSummary = true;
		foreach (long item in list)
		{
			_pendingSummaryIds.Add(item);
		}
		RPCAsyncResult rPCAsyncResult = guild.RequestGuildSummaries(list);
		if (rPCAsyncResult == null)
		{
			_isFetchingSummary = false;
			_pendingSummaryIds.Clear();
			return;
		}
		rPCAsyncResult.OnFinishedOnly.AddOnce(delegate
		{
			_isFetchingSummary = false;
			_pendingSummaryIds.Clear();
		});
	}

	private void RefreshDetail()
	{
		if (com_DetailPanel == null)
		{
			return;
		}
		if (_selectedGuildId == 0L)
		{
			com_DetailPanel.SetData(null, 0);
			return;
		}
		Guild guild = SimpleSingletonProvider<GameLogicManager>.inst.guild?.GetCachedGuild(_selectedGuildId);
		if (guild != null)
		{
			com_DetailPanel.SetData(guild, 3);
		}
		else
		{
			com_DetailPanel.SetMissingData(_selectedGuildId, 3);
		}
	}

	private bool CanAcceptSelected()
	{
		if (_selectedGuildId == 0L)
		{
			return false;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null)
		{
			return false;
		}
		if (guild.IsJoined)
		{
			return false;
		}
		if (!_snapshots.Exists((InvitationSnapshot s) => s.GuildId == _selectedGuildId))
		{
			return false;
		}
		Guild cachedGuild = guild.GetCachedGuild(_selectedGuildId);
		if (cachedGuild == null)
		{
			return true;
		}
		if (cachedGuild.MemberCount >= StaticGlobalData.GUILD_MEMBER_LIMIT)
		{
			return false;
		}
		if (cachedGuild.Status == 2)
		{
			return false;
		}
		if (cachedGuild.Status == 3)
		{
			return false;
		}
		return true;
	}

	private bool CanRejectSelected()
	{
		if (_selectedGuildId == 0L)
		{
			return false;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.IsJoined)
		{
			return false;
		}
		if (!_snapshots.Exists((InvitationSnapshot s) => s.GuildId == _selectedGuildId))
		{
			return false;
		}
		return true;
	}

	private bool CanRejectAll()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.IsJoined)
		{
			return false;
		}
		return _snapshots.Count > 0;
	}

	private void UpdateAllButtons()
	{
		if (_isBusy)
		{
			SetButtonEnabled(btn_AcceptInvite, isUsable: false);
			SetButtonEnabled(btn_RejectInvite, isUsable: false);
			SetButtonEnabled(btn_RejectAllInvites, isUsable: false);
		}
		else
		{
			SetButtonEnabled(btn_AcceptInvite, CanAcceptSelected());
			SetButtonEnabled(btn_RejectInvite, CanRejectSelected());
			SetButtonEnabled(btn_RejectAllInvites, CanRejectAll());
		}
	}

	private void SetButtonEnabled(UIGuildDiscovery_Common_Button button, bool isUsable)
	{
		if (button != null)
		{
			button.grayed = !isUsable;
			button.touchable = isUsable;
		}
	}

	private void OnClickAccept()
	{
		if (_isBusy || !CanAcceptSelected())
		{
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			long selectedGuildId = _selectedGuildId;
			BeginBusy();
			guild.AcceptInvitation(selectedGuildId).OnFinishedOnly.AddOnce(delegate
			{
				EndBusy();
			});
		}
	}

	private void OnClickReject()
	{
		if (_isBusy || !CanRejectSelected())
		{
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			long selectedGuildId = _selectedGuildId;
			BeginBusy();
			guild.RejectInvitation(selectedGuildId).OnFinishedOnly.AddOnce(delegate
			{
				EndBusy();
			});
		}
	}

	private void OnClickRejectAll()
	{
		if (_isBusy || !CanRejectAll())
		{
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null)
		{
			return;
		}
		List<long> list = new List<long>(_snapshots.Count);
		HashSet<long> hashSet = new HashSet<long>();
		foreach (InvitationSnapshot snapshot in _snapshots)
		{
			if (hashSet.Add(snapshot.GuildId))
			{
				list.Add(snapshot.GuildId);
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		BeginBusy();
		RPCAsyncResult rPCAsyncResult = guild.RejectInvitations(list);
		if (rPCAsyncResult == null)
		{
			EndBusy();
			return;
		}
		rPCAsyncResult.OnFinishedOnly.AddOnce(delegate
		{
			EndBusy();
		});
	}

	private void BeginBusy()
	{
		_isBusy = true;
		btn_AcceptInvite.onClick.Retain();
		btn_RejectInvite.onClick.Retain();
		btn_RejectAllInvites.onClick.Retain();
		SetButtonEnabled(btn_AcceptInvite, isUsable: false);
		SetButtonEnabled(btn_RejectInvite, isUsable: false);
		SetButtonEnabled(btn_RejectAllInvites, isUsable: false);
		OnLoadingMask?.Invoke(obj: true);
	}

	private void EndBusy()
	{
		if (_isBusy)
		{
			_isBusy = false;
			btn_AcceptInvite.onClick.Release();
			btn_RejectInvite.onClick.Release();
			btn_RejectAllInvites.onClick.Release();
			OnLoadingMask?.Invoke(obj: false);
			UpdateAllButtons();
		}
	}

	private void OnPlayerGuildUpdated(PlayerGuildInfo _)
	{
		RebuildSnapshots();
		RefreshList();
		RefreshDetail();
		UpdateAllButtons();
		FetchMissingSummariesIfNeeded();
	}

	private void OnInvitationsChanged()
	{
		RebuildSnapshots();
		RefreshList();
		RefreshDetail();
		UpdateAllButtons();
		FetchMissingSummariesIfNeeded();
	}

	private void OnGuildCacheChanged()
	{
		if (list_ReceivedInvites != null)
		{
			list_ReceivedInvites.numItems = _snapshots.Count;
			RefreshDetail();
			UpdateAllButtons();
		}
	}

	public static UIGuildDiscovery_Com_InviteView CreateInstance()
	{
		return (UIGuildDiscovery_Com_InviteView)UIPackage.CreateObject("GuildDiscovery", "GuildDiscovery_Com_InviteView");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_ReceivedInvites = (GList)GetChildAt(4);
		com_DetailPanel = (UIGuildDiscovery_Com_GuildDetai)GetChildAt(5);
		btn_AcceptInvite = (UIGuildDiscovery_Common_Button)GetChildAt(6);
		btn_RejectInvite = (UIGuildDiscovery_Common_Button)GetChildAt(7);
		btn_RejectAllInvites = (UIGuildDiscovery_Common_Button)GetChildAt(8);
	}
}
