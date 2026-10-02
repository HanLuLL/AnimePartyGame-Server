using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class UIGuildDiscovery_Com_ApplicationView : GComponent
{
	private struct ApplicationSnapshot
	{
		public long GuildId;

		public long TimestampSec;
	}

	private readonly List<ApplicationSnapshot> _snapshots = new List<ApplicationSnapshot>();

	private long _selectedGuildId;

	private bool _listRendererBound;

	private bool _isFetchingSummary;

	private readonly HashSet<long> _pendingSummaryIds = new HashSet<long>();

	public GList list_Applications;

	public UIGuildDiscovery_Com_GuildDetai com_DetailPanel;

	public const string URL = "ui://gldisc01gd007";

	public bool HasSelection => _selectedGuildId != 0;

	public void Init()
	{
		_listRendererBound = false;
		BindApplicationsList();
	}

	public void OnShow()
	{
		_selectedGuildId = 0L;
		RebuildSnapshots();
		RefreshList();
		RefreshDetail();
		FetchMissingSummariesIfNeeded();
	}

	public void AddEvent()
	{
		list_Applications.onClickItem.Add(OnApplicationListItemClicked);
	}

	public void RemoveEvent()
	{
		list_Applications.onClickItem.Remove(OnApplicationListItemClicked);
	}

	public void AddListener()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			guild.signal.playerGuildUpdated.AddListener(OnPlayerGuildUpdated);
			guild.signal.applicationsChanged.AddListener(OnApplicationsChanged);
			guild.signal.guildCacheChanged.AddListener(OnGuildCacheChanged);
		}
	}

	public void RemoveListener()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			guild.signal.playerGuildUpdated.RemoveListener(OnPlayerGuildUpdated);
			guild.signal.applicationsChanged.RemoveListener(OnApplicationsChanged);
			guild.signal.guildCacheChanged.RemoveListener(OnGuildCacheChanged);
		}
	}

	public void ClearData()
	{
		_snapshots.Clear();
		_selectedGuildId = 0L;
		_isFetchingSummary = false;
		_pendingSummaryIds.Clear();
	}

	public void RefreshRedPoints()
	{
	}

	private void BindApplicationsList()
	{
		GList gList = list_Applications;
		if (gList != null)
		{
			if (!_listRendererBound)
			{
				gList.itemRenderer = OnRenderApplicationItem;
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
		if (playerGuild == null || playerGuild.Applications == null || playerGuild.Applications.Count == 0)
		{
			_selectedGuildId = 0L;
			return;
		}
		long num = (long)StaticGlobalData.GUILD_APPLICATIONS_TERM * 24L * 3600;
		long num2 = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds() - num;
		foreach (KeyValuePair<long, long> application in playerGuild.Applications)
		{
			if (application.Key != 0L && application.Value >= num2)
			{
				_snapshots.Add(new ApplicationSnapshot
				{
					GuildId = application.Key,
					TimestampSec = application.Value
				});
			}
		}
		_snapshots.Sort(delegate(ApplicationSnapshot a, ApplicationSnapshot b)
		{
			int num3 = b.TimestampSec.CompareTo(a.TimestampSec);
			return (num3 != 0) ? num3 : a.GuildId.CompareTo(b.GuildId);
		});
		if (_selectedGuildId != 0L && !_snapshots.Exists((ApplicationSnapshot s) => s.GuildId == _selectedGuildId))
		{
			_selectedGuildId = 0L;
		}
	}

	private void RefreshList()
	{
		if (list_Applications != null)
		{
			list_Applications.numItems = _snapshots.Count;
		}
	}

	private void OnRenderApplicationItem(int index, GObject item)
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
		ApplicationSnapshot applicationSnapshot = _snapshots[index];
		Guild guild = SimpleSingletonProvider<GameLogicManager>.inst.guild?.GetCachedGuild(applicationSnapshot.GuildId);
		uIGuildDiscovery_Com_GuildListItem.SetData(guild, isApplied: false, 1, applicationSnapshot.GuildId);
		uIGuildDiscovery_Com_GuildListItem.ApplySelection(applicationSnapshot.GuildId == _selectedGuildId);
		string text = string.Empty;
		if (applicationSnapshot.TimestampSec > 0)
		{
			DateTime originUtcDT = TimeUtils.OriginUtcDT;
			text = originUtcDT.AddSeconds(applicationSnapshot.TimestampSec).ToString("yyyy.MM.dd");
		}
		uIGuildDiscovery_Com_GuildListItem.txt_Time.text = text;
	}

	private void OnApplicationListItemClicked(EventContext context)
	{
		if (context.data is UIGuildDiscovery_Com_GuildListItem uIGuildDiscovery_Com_GuildListItem)
		{
			OnApplicationItemClicked(uIGuildDiscovery_Com_GuildListItem.GuildId);
		}
	}

	private void OnApplicationItemClicked(long guildId)
	{
		if (guildId != 0L)
		{
			_selectedGuildId = guildId;
			RefreshList();
			RefreshDetail();
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
			com_DetailPanel.SetData(guild, 2);
		}
		else
		{
			com_DetailPanel.SetMissingData(_selectedGuildId, 2);
		}
	}

	private void OnPlayerGuildUpdated(PlayerGuildInfo _)
	{
		RebuildSnapshots();
		RefreshList();
		RefreshDetail();
		FetchMissingSummariesIfNeeded();
	}

	private void OnApplicationsChanged()
	{
		RebuildSnapshots();
		RefreshList();
		RefreshDetail();
		FetchMissingSummariesIfNeeded();
	}

	private void OnGuildCacheChanged()
	{
		if (list_Applications != null)
		{
			list_Applications.numItems = _snapshots.Count;
			RefreshDetail();
		}
	}

	public static UIGuildDiscovery_Com_ApplicationView CreateInstance()
	{
		return (UIGuildDiscovery_Com_ApplicationView)UIPackage.CreateObject("GuildDiscovery", "GuildDiscovery_Com_ApplicationView");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Applications = (GList)GetChildAt(4);
		com_DetailPanel = (UIGuildDiscovery_Com_GuildDetai)GetChildAt(5);
	}
}
