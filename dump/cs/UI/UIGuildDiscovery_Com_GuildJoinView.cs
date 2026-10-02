using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIGuildDiscovery_Com_GuildJoinView : GComponent
{
	private long _selectedGuildId;

	public Action<bool> OnLoadingMask;

	private readonly List<GuildTagConfigure> _tagConfigs = new List<GuildTagConfigure>();

	private readonly HashSet<int> _selectedTagIds = new HashSet<int>();

	private string _keyword = string.Empty;

	private bool _isSearching;

	private const float PAGINATION_PERCY_THRESHOLD = 0.95f;

	public GTextField txt_SectionTitle;

	public GList list_FilterTags;

	public GTextInput txtField_GuildKeyword;

	public UIGuildDiscovery_Common_Button btn_SearchGuild;

	public GList list_Guilds;

	public UIGuildDiscovery_Com_GuildDetai com_GuildDetailPanel;

	public UIGuildDiscovery_Common_Button btn_ReportGuild;

	public UIGuildDiscovery_Common_Button btn_ApplyJoinGuild;

	public const string URL = "ui://gldisc01gd016";

	public bool HasSelection => _selectedGuildId != 0;

	public void Init()
	{
		CacheTagConfigs();
		BindGuildList();
		BindTagFilterList();
		ResetState();
	}

	public void OnShow()
	{
		ResetState();
	}

	public void AddEvent()
	{
		btn_ReportGuild.onClick.Add(OnClickReportGuild);
		btn_SearchGuild.onClick.Add(OnClickSearchGuild);
		txtField_GuildKeyword.onSubmit.Add(OnSubmitKeyword);
		list_Guilds.scrollPane.onScroll.Add(OnGuildListScroll);
		list_Guilds.onClickItem.Add(OnGuildListItemClicked);
		btn_ApplyJoinGuild.onClick.Add(OnClickApplyGuild);
	}

	public void RemoveEvent()
	{
		btn_ReportGuild.onClick.Remove(OnClickReportGuild);
		btn_SearchGuild.onClick.Remove(OnClickSearchGuild);
		txtField_GuildKeyword.onSubmit.Remove(OnSubmitKeyword);
		list_Guilds.scrollPane.onScroll.Remove(OnGuildListScroll);
		list_Guilds.onClickItem.Remove(OnGuildListItemClicked);
		btn_ApplyJoinGuild.onClick.Remove(OnClickApplyGuild);
	}

	public void AddListener()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			guild.signal.playerGuildUpdated.AddListener(OnPlayerGuildUpdated);
			guild.signal.applicationsChanged.AddListener(OnApplicationsChanged);
			guild.signal.searchResultsChanged.AddListener(OnSearchResultsChanged);
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
			guild.signal.searchResultsChanged.RemoveListener(OnSearchResultsChanged);
			guild.signal.guildCacheChanged.RemoveListener(OnGuildCacheChanged);
		}
	}

	public void ClearData()
	{
		_isSearching = false;
		ResetSearchState();
		_tagConfigs.Clear();
		_selectedTagIds.Clear();
		_selectedGuildId = 0L;
		_keyword = string.Empty;
	}

	public void RefreshRedPoints()
	{
	}

	private void CacheTagConfigs()
	{
		_tagConfigs.Clear();
		GuildConfigure guild = StaticConfigure.Guild;
		if (guild == null)
		{
			return;
		}
		foreach (GuildTagConfigure tag in guild.Tags)
		{
			if (tag != null && tag.GuildTagType != GuildTagType.None)
			{
				_tagConfigs.Add(tag);
			}
		}
	}

	private void BindTagFilterList()
	{
		list_FilterTags.itemRenderer = OnRenderTagItem;
		list_FilterTags.numItems = _tagConfigs.Count;
	}

	private void OnRenderTagItem(int index, GObject item)
	{
		if (item is GButton gButton && index >= 0 && index < _tagConfigs.Count)
		{
			GuildTagConfigure guildTagConfigure = _tagConfigs[index];
			gButton.title = guildTagConfigure.NameID.GetLocal(UIStringType.Guild);
			int tagId = (int)guildTagConfigure.GuildTagType;
			gButton.selected = _selectedTagIds.Contains(tagId);
			gButton.onClick.Set((EventCallback0)delegate
			{
				OnTagClicked(tagId);
			});
		}
	}

	private void OnTagClicked(int tagId)
	{
		if (!_selectedTagIds.Remove(tagId))
		{
			if (_selectedTagIds.Count >= StaticGlobalData.GUILD_TAG_LIMIT)
			{
				return;
			}
			_selectedTagIds.Add(tagId);
		}
		list_FilterTags.numItems = _tagConfigs.Count;
	}

	private void BindGuildList()
	{
		GList gList = list_Guilds;
		gList.SetVirtual();
		gList.itemRenderer = OnRenderGuildItem;
		gList.numItems = 0;
	}

	private void OnRenderGuildItem(int index, GObject item)
	{
		if (item is UIGuildDiscovery_Com_GuildListItem uIGuildDiscovery_Com_GuildListItem)
		{
			GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
			Guild guild2 = null;
			if (guild != null && index >= 0 && index < guild.SearchResults.Count)
			{
				guild2 = guild.SearchResults[index];
			}
			bool isApplied = guild2 != null && guild.IsApplicationActive(guild2.Id);
			uIGuildDiscovery_Com_GuildListItem.SetData(guild2, isApplied, 0, 0L);
			uIGuildDiscovery_Com_GuildListItem.ApplySelection(guild2 != null && guild2.Id == _selectedGuildId);
		}
	}

	private void OnGuildListItemClicked(EventContext context)
	{
		if (context.data is UIGuildDiscovery_Com_GuildListItem uIGuildDiscovery_Com_GuildListItem)
		{
			OnGuildItemClicked(uIGuildDiscovery_Com_GuildListItem.GuildId);
		}
	}

	private void OnGuildItemClicked(long guildId)
	{
		if (guildId == 0L)
		{
			return;
		}
		_selectedGuildId = guildId;
		Guild guild = SimpleSingletonProvider<GameLogicManager>.inst.guild?.GetCachedGuild(guildId);
		com_GuildDetailPanel.SetData(guild, 1);
		for (int i = 0; i < list_Guilds.numChildren; i++)
		{
			if (list_Guilds.GetChildAt(i) is UIGuildDiscovery_Com_GuildListItem uIGuildDiscovery_Com_GuildListItem)
			{
				uIGuildDiscovery_Com_GuildListItem.ApplySelection(uIGuildDiscovery_Com_GuildListItem.GuildId != 0L && uIGuildDiscovery_Com_GuildListItem.GuildId == _selectedGuildId);
			}
		}
		UpdateApplyButton();
	}

	private void ResetState()
	{
		_keyword = string.Empty;
		_selectedTagIds.Clear();
		_selectedGuildId = 0L;
		_isSearching = false;
		if (txtField_GuildKeyword != null)
		{
			txtField_GuildKeyword.text = string.Empty;
		}
		ResetSearchState();
		if (list_Guilds != null)
		{
			list_Guilds.numItems = 0;
		}
		com_GuildDetailPanel?.SetData(null, 0);
		if (list_FilterTags != null)
		{
			list_FilterTags.numItems = _tagConfigs.Count;
		}
		UpdateApplyButton();
	}

	private void ResetSearchState()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.ResetSearch();
	}

	private void UpdateApplyButton()
	{
		bool flag = CanApplyToSelected();
		if (btn_ApplyJoinGuild != null)
		{
			btn_ApplyJoinGuild.grayed = !flag;
			btn_ApplyJoinGuild.touchable = flag;
		}
	}

	private bool CanApplyToSelected()
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
		if (guild.IsInJoinCooldown())
		{
			return false;
		}
		if (guild.IsDailyApplyLimitReached)
		{
			return false;
		}
		if (guild.IsApplicationActive(_selectedGuildId))
		{
			return false;
		}
		Guild cachedGuild = guild.GetCachedGuild(_selectedGuildId);
		if (cachedGuild == null)
		{
			return false;
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

	private void OnClickApplyGuild()
	{
		if (!CanApplyToSelected())
		{
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			long selectedGuildId = _selectedGuildId;
			btn_ApplyJoinGuild?.onClick.Retain();
			OnLoadingMask?.Invoke(obj: true);
			guild.Apply(selectedGuildId).OnFinishedOnly.AddOnce(delegate
			{
				btn_ApplyJoinGuild?.onClick.Release();
				OnLoadingMask?.Invoke(obj: false);
			});
		}
	}

	private void OnClickSearchGuild()
	{
		StartSearch(resetPage: true);
	}

	private void OnSubmitKeyword()
	{
		StartSearch(resetPage: true);
	}

	private void OnGuildListScroll()
	{
		if (!_isSearching && list_Guilds?.scrollPane != null)
		{
			GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
			if (guild != null && guild.HasMoreSearchPage && !(list_Guilds.scrollPane.percY < 0.95f))
			{
				StartSearch(resetPage: false);
			}
		}
	}

	private void StartSearch(bool resetPage)
	{
		if (_isSearching)
		{
			return;
		}
		GuildLogic logic = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (logic == null || (resetPage && logic.IsInSearchCooldown()))
		{
			return;
		}
		_keyword = (txtField_GuildKeyword?.text ?? string.Empty).Trim();
		List<int> tagIds = new List<int>(_selectedTagIds);
		if (tagIds.Count > StaticGlobalData.GUILD_TAG_LIMIT)
		{
			return;
		}
		_isSearching = true;
		OnLoadingMask?.Invoke(obj: true);
		UIGuildDiscovery_Common_Button searchBtn = btn_SearchGuild;
		searchBtn?.onClick.Retain();
		if (resetPage && long.TryParse(_keyword, out var parsedId) && parsedId > 0)
		{
			logic.MarkSearchStarted();
			logic.ResetSearch();
			RPCAsyncResult rPCAsyncResult = logic.RequestGuildSummaries(new long[1] { parsedId });
			if (rPCAsyncResult == null)
			{
				if (logic.AcceptExactSearchHit(parsedId))
				{
					_isSearching = false;
					OnLoadingMask?.Invoke(obj: false);
					searchBtn?.onClick.Release();
					logic.signal.searchResultsChanged.Dispatch();
				}
				else
				{
					FallbackToNameSearch(tagIds);
				}
				return;
			}
			rPCAsyncResult.OnFinishedOnly.AddOnce(delegate
			{
				if (logic.AcceptExactSearchHit(parsedId))
				{
					_isSearching = false;
					OnLoadingMask?.Invoke(obj: false);
					searchBtn?.onClick.Release();
					logic.signal.searchResultsChanged.Dispatch();
				}
				else
				{
					FallbackToNameSearch(tagIds);
				}
			});
		}
		else
		{
			logic.Search(_keyword, tagIds, resetPage).OnFinishedOnly.AddOnce(delegate
			{
				_isSearching = false;
				OnLoadingMask?.Invoke(obj: false);
				searchBtn?.onClick.Release();
			});
		}
	}

	private void FallbackToNameSearch(List<int> tagIds)
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null)
		{
			_isSearching = false;
			OnLoadingMask?.Invoke(obj: false);
			btn_SearchGuild?.onClick.Release();
			return;
		}
		UIGuildDiscovery_Common_Button searchBtn = btn_SearchGuild;
		guild.Search(_keyword, tagIds, resetPage: true, markSearchStarted: false).OnFinishedOnly.AddOnce(delegate
		{
			_isSearching = false;
			OnLoadingMask?.Invoke(obj: false);
			searchBtn?.onClick.Release();
		});
	}

	private void OnPlayerGuildUpdated(PlayerGuildInfo _)
	{
		UpdateApplyButton();
	}

	private void OnApplicationsChanged()
	{
		if (list_Guilds != null)
		{
			list_Guilds.RefreshVirtualList();
			UpdateApplyButton();
		}
	}

	private void OnSearchResultsChanged()
	{
		if (list_Guilds != null)
		{
			int numItems = SimpleSingletonProvider<GameLogicManager>.inst.guild?.SearchResults.Count ?? 0;
			list_Guilds.numItems = numItems;
		}
	}

	private void OnGuildCacheChanged()
	{
		if (list_Guilds != null)
		{
			list_Guilds.RefreshVirtualList();
		}
		Guild guild = SimpleSingletonProvider<GameLogicManager>.inst.guild?.GetCachedGuild(_selectedGuildId);
		com_GuildDetailPanel?.SetData(guild, (guild != null) ? 1 : 0);
		UpdateApplyButton();
	}

	private void OnClickReportGuild()
	{
		Debug.Log("[GuildDiscovery] 举报公会功能暂未实现。");
	}

	public static UIGuildDiscovery_Com_GuildJoinView CreateInstance()
	{
		return (UIGuildDiscovery_Com_GuildJoinView)UIPackage.CreateObject("GuildDiscovery", "GuildDiscovery_Com_GuildJoinView");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_SectionTitle = (GTextField)GetChildAt(2);
		list_FilterTags = (GList)GetChildAt(3);
		txtField_GuildKeyword = (GTextInput)GetChildAt(5);
		btn_SearchGuild = (UIGuildDiscovery_Common_Button)GetChildAt(6);
		list_Guilds = (GList)GetChildAt(7);
		com_GuildDetailPanel = (UIGuildDiscovery_Com_GuildDetai)GetChildAt(8);
		btn_ReportGuild = (UIGuildDiscovery_Common_Button)GetChildAt(9);
		btn_ApplyJoinGuild = (UIGuildDiscovery_Common_Button)GetChildAt(10);
	}
}
