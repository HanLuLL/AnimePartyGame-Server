using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIGuildDiscovery_Com_CreateView : GComponent
{
	private sealed class CreateCost
	{
		public readonly int ItemId;

		public readonly int Count;

		public readonly ItemInfoConfigure ItemConfig;

		public CreateCost(int itemId, int count, ItemInfoConfigure itemConfig)
		{
			ItemId = itemId;
			Count = count;
			ItemConfig = itemConfig;
		}
	}

	private sealed class GuildCreateForm
	{
		public readonly string Name;

		public readonly List<int> TagIds;

		public readonly string Announcement;

		public GuildCreateForm(string name, List<int> tagIds, string announcement)
		{
			Name = name;
			TagIds = tagIds;
			Announcement = announcement;
		}
	}

	private const int CREATE_CONFIRM_MESSAGE_ID = 3002;

	private const int CREATE_RESOURCE_NOT_ENOUGH_MESSAGE_ID = 3003;

	private const int GUILD_NAME_EMPTY_MESSAGE_ID = 3004;

	private const int GUILD_NAME_INVALID_MESSAGE_ID = 3005;

	private const int GUILD_NAME_EXISTS_MESSAGE_ID = 3006;

	private static readonly Regex InvalidGuildNameRegex = new Regex("[^a-zA-Z0-9\\u4e00-\\u9fa5\\u3040-\\u309f\\u30a0-\\u30ff\\u31f0-\\u31ff_-]");

	private readonly List<GuildTagConfigure> _tagConfigs = new List<GuildTagConfigure>();

	private readonly HashSet<int> _selectedTagIds = new HashSet<int>();

	private readonly List<CreateCost> _createCosts = new List<CreateCost>();

	private bool _componentsInitialized;

	private bool _isBusy;

	private bool _isSessionActive;

	private bool _sensitiveWordsLoading;

	private bool _sensitiveWordsReady;

	private int _sessionVersion;

	public Action<bool> OnLoadingMask;

	public GTextInput input_GuildName;

	public GTextField txt_hint1;

	public GTextField txt_hint2;

	public GList list_Tags;

	public GTextField txt_hint3;

	public GTextInput txt_notice;

	public GList cost_list;

	public UIGuildDiscovery_Common_Button btn_Confirm;

	public const string URL = "ui://gldisc01gd009";

	public void Init()
	{
		_sessionVersion++;
		_isSessionActive = true;
		_isBusy = false;
		_sensitiveWordsLoading = false;
		_sensitiveWordsReady = false;
		if (!_componentsInitialized)
		{
			input_GuildName.maxLength = StaticGlobalData.GUILD_NAME_LIMIT;
			input_GuildName.restrict = "[a-zA-Z0-9\\u4e00-\\u9fa5\\u3040-\\u309f\\u30a0-\\u30ff\\u31f0-\\u31ff_-]+";
			txt_notice.maxLength = StaticGlobalData.GUILD_EXTERNALANNOUNCEMENT_LIMIT;
			list_Tags.itemRenderer = RenderTag;
			cost_list.itemRenderer = RenderCost;
			_componentsInitialized = true;
		}
		CacheConfiguration();
		ResetForm();
		RefreshLists();
		UpdateConfirmButton();
	}

	public void OnShow()
	{
		if (_isSessionActive)
		{
			EnsureSensitiveWordsLoaded(_sessionVersion).Forget();
			UpdateConfirmButton();
		}
	}

	public void AddEvent()
	{
		btn_Confirm.onClick.Add(OnConfirmClicked);
	}

	public void RemoveEvent()
	{
		btn_Confirm.onClick.Remove(OnConfirmClicked);
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.playerGuildUpdated.AddListener(OnPlayerGuildUpdated);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.playerGuildUpdated.RemoveListener(OnPlayerGuildUpdated);
	}

	public void ClearData()
	{
		_sessionVersion++;
		_isSessionActive = false;
		EndBusy();
		_tagConfigs.Clear();
		_selectedTagIds.Clear();
		_createCosts.Clear();
		if (_sensitiveWordsReady)
		{
			StaticConfigure.UnloadSensitiveWords();
		}
		_sensitiveWordsLoading = false;
		_sensitiveWordsReady = false;
	}

	public void RefreshRedPoints()
	{
	}

	private void CacheConfiguration()
	{
		_tagConfigs.Clear();
		_createCosts.Clear();
		GuildConfigure guild = StaticConfigure.Guild;
		if (guild == null)
		{
			Debug.LogError("[GuildCreate] Guild 配表未加载。");
			return;
		}
		foreach (GuildTagConfigure tag in guild.Tags)
		{
			if (tag != null && tag.GuildTagType != GuildTagType.None)
			{
				_tagConfigs.Add(tag);
			}
		}
		_tagConfigs.Sort((GuildTagConfigure left, GuildTagConfigure right) => ((int)left.GuildTagType).CompareTo((int)right.GuildTagType));
		if (guild.Creates.Count != 1)
		{
			Debug.LogError($"[GuildCreate] Guild.Create 应有且仅有一条配置，当前为 {guild.Creates.Count} 条。");
			return;
		}
		GuildCreateConfigure guildCreateConfigure = guild.Creates[0];
		if (guildCreateConfigure == null)
		{
			return;
		}
		foreach (KeyValuePair<int, int> item in guildCreateConfigure.Consume)
		{
			ItemInfoConfigure itemInfoConfigure = item.Key.GetItemInfoConfigure();
			if (item.Key <= 0 || item.Value <= 0 || itemInfoConfigure == null)
			{
				Debug.LogError($"[GuildCreate] 非法创建消耗：itemId={item.Key}, count={item.Value}。");
				_createCosts.Clear();
				break;
			}
			_createCosts.Add(new CreateCost(item.Key, item.Value, itemInfoConfigure));
		}
	}

	private void ResetForm()
	{
		input_GuildName.text = string.Empty;
		txt_notice.text = string.Empty;
		_selectedTagIds.Clear();
	}

	private void RefreshLists()
	{
		list_Tags.numItems = _tagConfigs.Count;
		cost_list.numItems = _createCosts.Count;
	}

	private void RenderTag(int index, GObject item)
	{
		if (item is GButton gButton && index >= 0 && index < _tagConfigs.Count)
		{
			GuildTagConfigure guildTagConfigure = _tagConfigs[index];
			int tagId = (int)guildTagConfigure.GuildTagType;
			gButton.title = guildTagConfigure.NameID.GetLocal(UIStringType.Guild);
			gButton.selected = _selectedTagIds.Contains(tagId);
			gButton.onClick.Set((EventCallback0)delegate
			{
				ToggleTag(tagId);
			});
		}
	}

	private void ToggleTag(int tagId)
	{
		if (!_isBusy)
		{
			if (_selectedTagIds.Remove(tagId))
			{
				list_Tags.numItems = _tagConfigs.Count;
			}
			else if (_selectedTagIds.Count < StaticGlobalData.GUILD_TAG_LIMIT)
			{
				_selectedTagIds.Add(tagId);
				list_Tags.numItems = _tagConfigs.Count;
			}
		}
	}

	private void RenderCost(int index, GObject item)
	{
		if (item is UICom_LitItem item2 && index >= 0 && index < _createCosts.Count)
		{
			CreateCost createCost = _createCosts[index];
			CommonUIManager.RendererLitItem(item2, createCost.ItemId, createCost.Count);
		}
	}

	private async UniTask EnsureSensitiveWordsLoaded(int sessionVersion)
	{
		if (_sensitiveWordsReady || _sensitiveWordsLoading)
		{
			return;
		}
		_sensitiveWordsLoading = true;
		try
		{
			await StaticConfigure.LoadSensitiveWords();
			if (IsCurrentSession(sessionVersion))
			{
				_sensitiveWordsReady = true;
			}
		}
		catch (Exception arg)
		{
			Debug.LogError($"[GuildCreate] 敏感词资源加载失败：{arg}");
		}
		finally
		{
			if (IsCurrentSession(sessionVersion))
			{
				_sensitiveWordsLoading = false;
				UpdateConfirmButton();
			}
		}
	}

	private async void OnConfirmClicked()
	{
		if (_isBusy)
		{
			return;
		}
		int sessionVersion = _sessionVersion;
		GuildCreateForm form = CreateFormSnapshot();
		BeginBusy();
		if (!ValidateFormBeforeSensitiveWords(form))
		{
			EndBusy(sessionVersion);
		}
		else if (!(await SensitiveWords.Valid(form.Name)))
		{
			ShowGuildTip(3005);
			FocusNameInput(sessionVersion);
			EndBusy(sessionVersion);
		}
		else
		{
			if (!IsCurrentSession(sessionVersion))
			{
				return;
			}
			if (!ValidateBalances())
			{
				EndBusy(sessionVersion);
				return;
			}
			if (form.Announcement.Length > StaticGlobalData.GUILD_EXTERNALANNOUNCEMENT_LIMIT)
			{
				FocusNoticeInput(sessionVersion);
				EndBusy(sessionVersion);
				return;
			}
			bool flag = !string.IsNullOrEmpty(form.Announcement);
			if (flag)
			{
				flag = !(await SensitiveWords.Valid(form.Announcement));
			}
			if (flag)
			{
				FocusNoticeInput(sessionVersion);
				EndBusy(sessionVersion);
			}
			else
			{
				if (!IsCurrentSession(sessionVersion))
				{
					return;
				}
				CreateCost createCost = _createCosts[0];
				CreateCost createCost2 = _createCosts[1];
				string msg = string.Format(3002.GetLocal(UIStringType.Guild), createCost.ItemConfig.NameID.GetLocal(UIStringType.Item), createCost.Count, createCost2.ItemConfig.NameID.GetLocal(UIStringType.Item), createCost2.Count, form.Name);
				try
				{
					await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(msg, delegate
					{
						SendCreate(form, sessionVersion);
					}, delegate
					{
						EndBusy(sessionVersion);
					});
				}
				catch (Exception arg)
				{
					Debug.LogError($"[GuildCreate] 创建确认框打开失败：{arg}");
					EndBusy(sessionVersion);
				}
			}
		}
	}

	private GuildCreateForm CreateFormSnapshot()
	{
		List<int> list = new List<int>(_selectedTagIds);
		list.Sort();
		return new GuildCreateForm((input_GuildName.text ?? string.Empty).Trim(), list, (txt_notice.text ?? string.Empty).Trim());
	}

	private bool ValidateFormBeforeSensitiveWords(GuildCreateForm form)
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null || guild.IsJoined)
		{
			return false;
		}
		if (!_sensitiveWordsReady)
		{
			return false;
		}
		if (string.IsNullOrEmpty(form.Name))
		{
			ShowGuildTip(3004);
			input_GuildName.RequestFocus();
			return false;
		}
		if (form.Name.Length > StaticGlobalData.GUILD_NAME_LIMIT || InvalidGuildNameRegex.IsMatch(form.Name))
		{
			ShowGuildTip(3005);
			input_GuildName.RequestFocus();
			return false;
		}
		if (form.TagIds.Count > StaticGlobalData.GUILD_TAG_LIMIT)
		{
			return false;
		}
		return true;
	}

	private bool ValidateBalances()
	{
		BagLogic bag = SimpleSingletonProvider<GameLogicManager>.inst.bag;
		if (bag == null)
		{
			return false;
		}
		for (int i = 0; i < _createCosts.Count; i++)
		{
			CreateCost createCost = _createCosts[i];
			if (bag.GetItemCount(createCost.ItemId) < createCost.Count)
			{
				ShowGuildTip(3003);
				return false;
			}
		}
		return true;
	}

	private void SendCreate(GuildCreateForm form, int sessionVersion)
	{
		if (!IsCurrentSession(sessionVersion))
		{
			return;
		}
		if (form.TagIds.Count > StaticGlobalData.GUILD_TAG_LIMIT)
		{
			EndBusy(sessionVersion);
			return;
		}
		if (!ValidateBalances())
		{
			EndBusy(sessionVersion);
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null || guild.IsJoined)
		{
			EndBusy(sessionVersion);
			return;
		}
		RPCAsyncResult rPCAsyncResult = guild.Create(form.Name, form.TagIds, form.Announcement);
		if (rPCAsyncResult == null)
		{
			EndBusy(sessionVersion);
			return;
		}
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult rpcResult)
		{
			if (IsCurrentSession(sessionVersion) && rpcResult.errId == 17003)
			{
				ShowGuildTip(3006);
				FocusNameInput(sessionVersion);
			}
			EndBusy(sessionVersion);
		});
	}

	private static void ShowGuildTip(int messageId)
	{
		SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(messageId.GetLocal(UIStringType.Guild));
	}

	private void BeginBusy()
	{
		_isBusy = true;
		btn_Confirm.onClick.Retain();
		OnLoadingMask?.Invoke(obj: true);
		UpdateConfirmButton();
	}

	private void EndBusy(int sessionVersion)
	{
		if (sessionVersion == _sessionVersion)
		{
			EndBusy();
		}
	}

	private void EndBusy()
	{
		if (_isBusy)
		{
			_isBusy = false;
			btn_Confirm.onClick.Release();
			OnLoadingMask?.Invoke(obj: false);
			UpdateConfirmButton();
		}
	}

	private void UpdateConfirmButton()
	{
		bool flag = _isSessionActive && !_isBusy && _sensitiveWordsReady && !(SimpleSingletonProvider<GameLogicManager>.inst.guild?.IsJoined ?? true);
		btn_Confirm.grayed = !flag;
		btn_Confirm.touchable = flag;
	}

	private void FocusNameInput(int sessionVersion)
	{
		if (IsCurrentSession(sessionVersion))
		{
			input_GuildName.RequestFocus();
		}
	}

	private void FocusNoticeInput(int sessionVersion)
	{
		if (IsCurrentSession(sessionVersion))
		{
			txt_notice.RequestFocus();
		}
	}

	private bool IsCurrentSession(int sessionVersion)
	{
		if (_isSessionActive)
		{
			return sessionVersion == _sessionVersion;
		}
		return false;
	}

	private void OnPlayerGuildUpdated(PlayerGuildInfo _)
	{
		UpdateConfirmButton();
	}

	public static UIGuildDiscovery_Com_CreateView CreateInstance()
	{
		return (UIGuildDiscovery_Com_CreateView)UIPackage.CreateObject("GuildDiscovery", "GuildDiscovery_Com_CreateView");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		input_GuildName = (GTextInput)GetChildAt(4);
		txt_hint1 = (GTextField)GetChildAt(6);
		txt_hint2 = (GTextField)GetChildAt(8);
		list_Tags = (GList)GetChildAt(11);
		txt_hint3 = (GTextField)GetChildAt(13);
		txt_notice = (GTextInput)GetChildAt(16);
		cost_list = (GList)GetChildAt(19);
		btn_Confirm = (UIGuildDiscovery_Common_Button)GetChildAt(20);
	}
}
