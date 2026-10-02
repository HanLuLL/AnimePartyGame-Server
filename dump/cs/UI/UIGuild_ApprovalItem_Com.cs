using System;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class UIGuild_ApprovalItem_Com : GComponent
{
	private long _playerId;

	private Action<long> _onView;

	private Action<long> _onReject;

	private Action<long> _onAccept;

	public GComponent member_label;

	public GTextField txt_approvaTime;

	public UIGuild_Common_Button btn_see;

	public UIGuild_Common_Button btn_refuse;

	public UIGuild_Common_Button btn_agree;

	public const string URL = "ui://w5bj58pzhtd135";

	public void Bind(GuildApplication application, FriendData friendData, bool requesting, Action<long> onView, Action<long> onReject, Action<long> onAccept)
	{
		if (application == null || application.PlayerId == 0L)
		{
			ClearRenderedData();
			return;
		}
		_playerId = application.PlayerId;
		_onView = onView;
		_onReject = onReject;
		_onAccept = onAccept;
		RenderApplyTime(application.ApplyTime);
		RenderPlayerLabel(friendData);
		bool flag = friendData != null;
		SetButtonState(btn_see, flag && !requesting);
		SetButtonState(btn_refuse, flag && !requesting);
		SetButtonState(btn_agree, flag && !requesting);
		btn_see?.onClick.Set(OnClickView);
		btn_refuse?.onClick.Set(OnClickReject);
		btn_agree?.onClick.Set(OnClickAccept);
	}

	private void RenderApplyTime(long timestamp)
	{
		if (txt_approvaTime != null)
		{
			string obj;
			if (timestamp <= 0)
			{
				obj = string.Empty;
			}
			else
			{
				DateTime originUtcDT = TimeUtils.OriginUtcDT;
				obj = originUtcDT.AddSeconds(timestamp).ToString("yyyy/MM/dd HH:mm");
			}
			string value = obj;
			txt_approvaTime.SetVar("time", value).FlushVars();
		}
	}

	private void RenderPlayerLabel(FriendData friendData)
	{
		if (member_label != null)
		{
			member_label.visible = friendData != null;
			if (friendData != null && member_label is UICom_PlayerLabel com_Label)
			{
				CommonUIManager.RendererLabelInfo(com_Label, friendData.Nick ?? string.Empty, friendData.LV);
				CommonUIManager.RendererHeadShot(com_Label, friendData.HeadURL, isVideo: false);
				CommonUIManager.RendererLabel(UIType.Panel, 55, com_Label, friendData.Label.Item1, friendData.Label.Item2);
			}
		}
	}

	private static void SetButtonState(GButton button, bool enabled)
	{
		if (button != null)
		{
			button.visible = enabled;
			button.touchable = enabled;
			button.grayed = !enabled;
		}
	}

	private void OnClickView()
	{
		if (_playerId != 0L)
		{
			_onView?.Invoke(_playerId);
		}
	}

	private void OnClickReject()
	{
		if (_playerId != 0L)
		{
			_onReject?.Invoke(_playerId);
		}
	}

	private void OnClickAccept()
	{
		if (_playerId != 0L)
		{
			_onAccept?.Invoke(_playerId);
		}
	}

	private void ClearRenderedData()
	{
		_playerId = 0L;
		_onView = null;
		_onReject = null;
		_onAccept = null;
		if (txt_approvaTime != null)
		{
			txt_approvaTime.SetVar("time", string.Empty).FlushVars();
		}
		if (member_label != null)
		{
			member_label.visible = false;
		}
		SetButtonState(btn_see, enabled: false);
		SetButtonState(btn_refuse, enabled: false);
		SetButtonState(btn_agree, enabled: false);
	}

	public static UIGuild_ApprovalItem_Com CreateInstance()
	{
		return (UIGuild_ApprovalItem_Com)UIPackage.CreateObject("Guild", "Guild_ApprovalItem_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		member_label = (GComponent)GetChildAt(1);
		txt_approvaTime = (GTextField)GetChildAt(2);
		btn_see = (UIGuild_Common_Button)GetChildAt(3);
		btn_refuse = (UIGuild_Common_Button)GetChildAt(4);
		btn_agree = (UIGuild_Common_Button)GetChildAt(5);
	}
}
