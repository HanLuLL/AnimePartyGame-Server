using System;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using party.model;

namespace UI;

public class UIGuildDiscovery_Com_GuildDetai : GComponent
{
	private Guild _cachedGuild;

	private bool _tagRendererBound;

	public Controller state;

	public GTextField txt_GuildName;

	public GTextField txt_GuildLeader;

	public GTextField txt_GuildId;

	public GTextField txt_GuildMemberCount;

	public GTextField txt_GuildActiveCount;

	public GList list_DetailGuildTags;

	public GTextField txt_GuildAnnouncement;

	public GTextField txt_GuildEditedInfo;

	public const string URL = "ui://gldisc01gd014";

	public long CurrentGuildId { get; private set; }

	public void SetData(Guild guild, int sourceState)
	{
		_cachedGuild = guild;
		CurrentGuildId = guild?.Id ?? 0;
		if (guild == null)
		{
			ClearTemplateVars();
			txt_GuildAnnouncement.text = string.Empty;
			if (list_DetailGuildTags != null)
			{
				list_DetailGuildTags.numItems = 0;
			}
			state.selectedIndex = 0;
			return;
		}
		state.selectedIndex = sourceState;
		txt_GuildName.SetVar("name", guild.Name ?? string.Empty).FlushVars();
		txt_GuildId.SetVar("ID", guild.Id.ToString()).FlushVars();
		long guildMasterId = GuildLogic.GetGuildMasterId(guild);
		string text = ResolveMemberName(guild, guildMasterId);
		txt_GuildLeader.SetVar("leaderName", (!string.IsNullOrEmpty(text)) ? text : ((guildMasterId != 0L) ? guildMasterId.ToString() : string.Empty)).FlushVars();
		if (string.IsNullOrEmpty(text) && guildMasterId != 0L)
		{
			RequestLeaderNameAsync(guildMasterId);
		}
		txt_GuildMemberCount.SetVar("num", guild.MemberCount.ToString()).FlushVars();
		txt_GuildActiveCount.SetVar("activeNum", guild.LastWeekActiveCount.ToString()).FlushVars();
		txt_GuildAnnouncement.text = (string.IsNullOrEmpty(guild.ExAnnouncement) ? string.Empty : guild.ExAnnouncement);
		long lastExternalEditorId = guild.LastExternalEditorId;
		string text2 = ResolveMemberName(guild, lastExternalEditorId);
		if (string.IsNullOrEmpty(text2) && lastExternalEditorId != 0L)
		{
			RequestEditorNameAsync(lastExternalEditorId);
		}
		SetEditedInfoVars(lastExternalEditorId, text2, guild.LastExternalEditTime);
		RenderDetailTags();
	}

	public void SetMissingData(long guildId, int sourceState)
	{
		_cachedGuild = null;
		CurrentGuildId = guildId;
		ClearTemplateVars();
		txt_GuildId.SetVar("ID", (guildId != 0L) ? guildId.ToString() : string.Empty).FlushVars();
		txt_GuildAnnouncement.text = string.Empty;
		if (list_DetailGuildTags != null)
		{
			list_DetailGuildTags.numItems = 0;
		}
		state.selectedIndex = ((guildId != 0L) ? sourceState : 0);
	}

	public void ResetData()
	{
		CurrentGuildId = 0L;
		_cachedGuild = null;
		ClearTemplateVars();
		txt_GuildAnnouncement.text = string.Empty;
		if (list_DetailGuildTags != null)
		{
			list_DetailGuildTags.numItems = 0;
		}
		state.selectedIndex = 0;
	}

	private void ClearTemplateVars()
	{
		txt_GuildName.SetVar("name", string.Empty).FlushVars();
		txt_GuildLeader.SetVar("leaderName", string.Empty).FlushVars();
		txt_GuildId.SetVar("ID", string.Empty).FlushVars();
		txt_GuildMemberCount.SetVar("num", string.Empty).FlushVars();
		txt_GuildActiveCount.SetVar("activeNum", string.Empty).FlushVars();
		txt_GuildEditedInfo.SetVar("name", string.Empty).SetVar("time", string.Empty).FlushVars();
	}

	private void SetEditedInfoVars(long editorId, string editorName, long editTimeSec)
	{
		string value = ((editorId == 0L) ? string.Empty : ((!string.IsNullOrEmpty(editorName)) ? editorName : editorId.ToString()));
		string obj;
		if (editTimeSec <= 0)
		{
			obj = string.Empty;
		}
		else
		{
			DateTime originUtcDT = TimeUtils.OriginUtcDT;
			obj = originUtcDT.AddSeconds(editTimeSec).ToString("yyyy.MM.dd");
		}
		string value2 = obj;
		txt_GuildEditedInfo.SetVar("name", value).SetVar("time", value2).FlushVars();
	}

	private static string ResolveMemberName(Guild guild, long playerId)
	{
		if (guild?.Members == null || playerId == 0L)
		{
			return null;
		}
		if (!guild.Members.TryGetValue(playerId, out var value) || value == null)
		{
			return null;
		}
		string playerName = value.PlayerName;
		if (!string.IsNullOrEmpty(playerName))
		{
			return playerName;
		}
		return null;
	}

	private void RequestLeaderNameAsync(long leaderId)
	{
		FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
		if (friend == null)
		{
			return;
		}
		long capturedGuildId = CurrentGuildId;
		friend.RequestGetPlayerSimpleC2S(leaderId, delegate(RPCAsyncResult result)
		{
			if (CurrentGuildId == capturedGuildId && result.errId == 0)
			{
				FriendData friendData = friend.GetFriendData(leaderId);
				if (friendData != null && !string.IsNullOrEmpty(friendData.Nick) && txt_GuildLeader != null)
				{
					txt_GuildLeader.SetVar("leaderName", friendData.Nick).FlushVars();
				}
			}
		});
	}

	private void RequestEditorNameAsync(long editorId)
	{
		FriendLogic friend = SimpleSingletonProvider<GameLogicManager>.inst.friend;
		if (friend == null)
		{
			return;
		}
		long capturedGuildId = CurrentGuildId;
		friend.RequestGetPlayerSimpleC2S(editorId, delegate(RPCAsyncResult result)
		{
			if (CurrentGuildId == capturedGuildId && result.errId == 0)
			{
				FriendData friendData = friend.GetFriendData(editorId);
				if (friendData != null && !string.IsNullOrEmpty(friendData.Nick) && txt_GuildEditedInfo != null && _cachedGuild != null)
				{
					SetEditedInfoVars(editorId, friendData.Nick, _cachedGuild.LastExternalEditTime);
				}
			}
		});
	}

	private void RenderDetailTags()
	{
		GList gList = list_DetailGuildTags;
		if (gList != null)
		{
			if (!_tagRendererBound)
			{
				gList.itemRenderer = OnRenderDetailTag;
				_tagRendererBound = true;
			}
			gList.numItems = (_cachedGuild?.TagIds?.Count).GetValueOrDefault();
		}
	}

	private void OnRenderDetailTag(int index, GObject item)
	{
		if (!(item is GButton gButton) || _cachedGuild == null || index < 0 || index >= _cachedGuild.TagIds.Count)
		{
			return;
		}
		int num = _cachedGuild.TagIds[index];
		if (num != 0)
		{
			MapField<int, GuildTagConfigure> mapField = StaticConfigure.Guild?.TagDict;
			if (mapField != null && mapField.TryGetValue(num, out var value) && value != null)
			{
				gButton.title = value.NameID.GetLocal(UIStringType.Guild);
				gButton.selected = true;
				gButton.touchable = false;
			}
		}
	}

	public static UIGuildDiscovery_Com_GuildDetai CreateInstance()
	{
		return (UIGuildDiscovery_Com_GuildDetai)UIPackage.CreateObject("GuildDiscovery", "GuildDiscovery_Com_GuildDetai");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		txt_GuildName = (GTextField)GetChildAt(1);
		txt_GuildLeader = (GTextField)GetChildAt(2);
		txt_GuildId = (GTextField)GetChildAt(3);
		txt_GuildMemberCount = (GTextField)GetChildAt(4);
		txt_GuildActiveCount = (GTextField)GetChildAt(5);
		list_DetailGuildTags = (GList)GetChildAt(6);
		txt_GuildAnnouncement = (GTextField)GetChildAt(8);
		txt_GuildEditedInfo = (GTextField)GetChildAt(10);
	}
}
