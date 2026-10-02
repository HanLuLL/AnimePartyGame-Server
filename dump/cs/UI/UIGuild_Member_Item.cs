using System;
using FairyGUI;
using FairyGUI.Utils;
using Tools;
using party.model;
using party.protocol;

namespace UI;

public class UIGuild_Member_Item : GComponent
{
	private Action<long> _openMemberSettings;

	public GButton btn_setting;

	public GComponent member_label;

	public GButton btn_tag;

	public GTextField time;

	public GTextField active;

	public const string URL = "ui://w5bj58pzg04j17";

	public void Render(long playerId, GuildMember memberInfo, FriendShowPlayerInfo friendInfo, bool hasOperation, Action<long> openMemberSettings)
	{
		if (memberInfo == null)
		{
			ResetView();
			return;
		}
		data = playerId;
		_openMemberSettings = openMemberSettings;
		if (member_label is UICom_PlayerLabel uICom_PlayerLabel)
		{
			string playerName = (string.IsNullOrEmpty(friendInfo?.Name) ? (memberInfo.PlayerName ?? string.Empty) : friendInfo.Name);
			CommonUIManager.RendererLabelInfo(uICom_PlayerLabel, playerName, friendInfo?.Lv ?? 0);
			CommonUIManager.RendererHeadShot(uICom_PlayerLabel, GetHeadIconUrl(friendInfo?.HeadIcon ?? 0), isVideo: false);
			(string, bool) playerLabel = GetPlayerLabel(friendInfo?.Background ?? 0);
			CommonUIManager.RendererLabel(UIType.Panel, 55, uICom_PlayerLabel, playerLabel.Item1, playerLabel.Item2);
			uICom_PlayerLabel.touchable = false;
		}
		btn_tag.title = GuildText.GetTitle((GuildTitleType)memberInfo.Role);
		btn_tag.touchable = false;
		long num = ((!(friendInfo?.IsOnline ?? memberInfo.Online)) ? (friendInfo?.OfflineTime ?? 0) : (friendInfo?.Time ?? 0));
		long num2 = ((num > 0) ? num : memberInfo.LastLoginTime);
		GTextField gTextField = time;
		string obj;
		if (num2 <= 0)
		{
			obj = string.Empty;
		}
		else
		{
			DateTime originUtcDT = TimeUtils.OriginUtcDT;
			obj = originUtcDT.AddSeconds(num2).ToString("yyyy/MM/dd");
		}
		gTextField.text = obj;
		active.SetVar("active", memberInfo.WeeklyActivity.ToString()).FlushVars();
		btn_setting.visible = hasOperation;
		btn_setting.touchable = hasOperation;
		btn_setting.data = playerId;
		btn_setting.onClick.Set(OnClickSettings);
	}

	private void ResetView()
	{
		data = 0L;
		_openMemberSettings = null;
		if (member_label is UICom_PlayerLabel uICom_PlayerLabel)
		{
			CommonUIManager.RendererLabelInfo(uICom_PlayerLabel, string.Empty, 0L);
			CommonUIManager.RendererHeadShot(uICom_PlayerLabel, string.Empty, isVideo: false);
			CommonUIManager.RendererLabel(UIType.Panel, 55, uICom_PlayerLabel, string.Empty, isVideo: false);
			uICom_PlayerLabel.touchable = false;
		}
		btn_tag.title = string.Empty;
		btn_tag.touchable = false;
		time.text = string.Empty;
		active.SetVar("active", string.Empty).FlushVars();
		btn_setting.visible = false;
		btn_setting.touchable = false;
		btn_setting.data = 0L;
		btn_setting.onClick.Clear();
	}

	private static string GetHeadIconUrl(int headIconId)
	{
		if (headIconId == 0)
		{
			return string.Empty;
		}
		ItemInfoConfigure itemInfoConfigure = headIconId.GetItemInfoConfigure();
		if (itemInfoConfigure != null)
		{
			return itemInfoConfigure.SubMeterID.GetFashionAccountHeadShot();
		}
		return string.Empty;
	}

	private static (string, bool) GetPlayerLabel(int backgroundId)
	{
		if (backgroundId == 0)
		{
			return (string.Empty, false);
		}
		return (backgroundId.GetItemInfoConfigure()?.SubMeterID.GetFashionAccountBackgroundConfigure())?.GetPlayerLabel() ?? (string.Empty, false);
	}

	private void OnClickSettings(EventContext context)
	{
		if (context.sender is GButton { data: var obj } && obj is long num && num != 0L)
		{
			_openMemberSettings?.Invoke(num);
		}
	}

	public static UIGuild_Member_Item CreateInstance()
	{
		return (UIGuild_Member_Item)UIPackage.CreateObject("Guild", "Guild_Member_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_setting = (GButton)GetChildAt(2);
		member_label = (GComponent)GetChildAt(3);
		btn_tag = (GButton)GetChildAt(4);
		time = (GTextField)GetChildAt(5);
		active = (GTextField)GetChildAt(6);
	}
}
