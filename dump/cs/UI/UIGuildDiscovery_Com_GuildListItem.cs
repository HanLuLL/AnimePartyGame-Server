using FairyGUI;
using FairyGUI.Utils;
using Google.Protobuf.Collections;
using party.model;

namespace UI;

public class UIGuildDiscovery_Com_GuildListItem : GButton
{
	private Guild _cachedGuild;

	private bool _tagRendererBound;

	private int _baseShowType;

	public Controller showType;

	public GTextField txt_GuildName;

	public GTextField txt_Time;

	public GList list_GuildTags;

	public GTextField txt_GuildMember;

	public GTextField txt_GuildActiveMember;

	public const string URL = "ui://gldisc01gd005";

	public long GuildId { get; private set; }

	public bool IsSelected { get; private set; }

	public bool IsApplied { get; private set; }

	public void SetData(Guild guild, bool isApplied, int showTypeIndex = 0, long fallbackGuildId = 0L)
	{
		_cachedGuild = guild;
		GuildId = guild?.Id ?? fallbackGuildId;
		_baseShowType = showTypeIndex;
		IsSelected = false;
		base.selected = false;
		base.touchable = GuildId != 0;
		txt_Time.text = string.Empty;
		if (guild == null)
		{
			txt_GuildName.text = ((fallbackGuildId != 0L) ? fallbackGuildId.ToString() : string.Empty);
			txt_GuildMember.SetVar("num", string.Empty).FlushVars();
			txt_GuildActiveMember.SetVar("num", string.Empty).FlushVars();
			if (list_GuildTags != null)
			{
				list_GuildTags.numItems = 0;
			}
			SetApplied(applied: false);
		}
		else
		{
			txt_GuildName.text = guild.Name;
			txt_GuildMember.SetVar("num", guild.MemberCount.ToString()).FlushVars();
			txt_GuildActiveMember.SetVar("num", guild.LastWeekActiveCount.ToString()).FlushVars();
			RenderGuildTags();
			SetApplied(isApplied);
		}
	}

	public void ApplySelection(bool selectedValue)
	{
		IsSelected = selectedValue;
		base.selected = selectedValue;
	}

	public void SetApplied(bool applied)
	{
		IsApplied = applied;
		if (showType != null)
		{
			showType.selectedIndex = (applied ? 2 : _baseShowType);
		}
	}

	private void RenderGuildTags()
	{
		GList gList = list_GuildTags;
		if (gList != null)
		{
			if (!_tagRendererBound)
			{
				gList.itemRenderer = OnRenderGuildTag;
				_tagRendererBound = true;
			}
			gList.numItems = (_cachedGuild?.TagIds?.Count).GetValueOrDefault();
		}
	}

	private void OnRenderGuildTag(int index, GObject item)
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

	public static UIGuildDiscovery_Com_GuildListItem CreateInstance()
	{
		return (UIGuildDiscovery_Com_GuildListItem)UIPackage.CreateObject("GuildDiscovery", "GuildDiscovery_Com_GuildListItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showType = GetControllerAt(0);
		txt_GuildName = (GTextField)GetChildAt(1);
		txt_Time = (GTextField)GetChildAt(4);
		list_GuildTags = (GList)GetChildAt(5);
		txt_GuildMember = (GTextField)GetChildAt(7);
		txt_GuildActiveMember = (GTextField)GetChildAt(9);
	}
}
