using System;
using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIGuild_MemberChange_Com : GComponent
{
	private readonly List<GuildMemberChangeMsg> _renderableMessages = new List<GuildMemberChangeMsg>();

	private bool _initialized;

	private bool _isOpen;

	private int _openVersion;

	public System.Action OnRequestClose;

	public GLabel bottom;

	public GList tips_item_list;

	public const string URL = "ui://w5bj58pzhtd12c";

	public void Init()
	{
		if (!_initialized)
		{
			if (tips_item_list != null)
			{
				tips_item_list.itemRenderer = RenderMemberChangeMessage;
			}
			if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
			{
				uICom_PopUpWindow_Bottom.btnsState.selectedIndex = 0;
			}
			_initialized = true;
		}
	}

	public void Open(int openVersion)
	{
		_openVersion = openVersion;
		_isOpen = true;
		RefreshMemberMessages();
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.RequestMemberChangeMessages();
	}

	public void AddEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Add(OnClickClose);
		}
	}

	public void RemoveEvent()
	{
		if (bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Remove(OnClickClose);
		}
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.memberChangeMessagesChanged.AddListener(OnMemberMessagesChanged);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.memberChangeMessagesChanged.RemoveListener(OnMemberMessagesChanged);
	}

	public void ClearData()
	{
		_openVersion++;
		_isOpen = false;
		_renderableMessages.Clear();
		OnRequestClose = null;
	}

	private void RefreshMemberMessages()
	{
		_renderableMessages.Clear();
		IReadOnlyList<GuildMemberChangeMsg> readOnlyList = SimpleSingletonProvider<GameLogicManager>.inst.guild?.GuildMemberChangeMessages;
		if (readOnlyList != null)
		{
			for (int i = 0; i < readOnlyList.Count; i++)
			{
				GuildMemberChangeMsg guildMemberChangeMsg = readOnlyList[i];
				if (guildMemberChangeMsg != null && guildMemberChangeMsg.NotificationId >= 101 && guildMemberChangeMsg.NotificationId <= 109 && TryFormatMemberChangeMessage(guildMemberChangeMsg, out var _))
				{
					_renderableMessages.Add(guildMemberChangeMsg);
				}
			}
		}
		if (_renderableMessages.Count == 0)
		{
			_renderableMessages.Add(null);
		}
		if (tips_item_list != null)
		{
			tips_item_list.numItems = _renderableMessages.Count;
		}
	}

	private void RenderMemberChangeMessage(int index, GObject item)
	{
		if (item is UIGuild_Tips_Item uIGuild_Tips_Item && index >= 0 && index < _renderableMessages.Count)
		{
			GuildMemberChangeMsg guildMemberChangeMsg = _renderableMessages[index];
			if (guildMemberChangeMsg == null)
			{
				uIGuild_Tips_Item.tips.text = GuildText.Get(3141);
			}
			else
			{
				uIGuild_Tips_Item.tips.text = (TryFormatMemberChangeMessage(guildMemberChangeMsg, out var text) ? text : string.Empty);
			}
		}
	}

	private static bool TryFormatMemberChangeMessage(GuildMemberChangeMsg message, out string text)
	{
		text = string.Empty;
		if (message == null)
		{
			return false;
		}
		string memberChangeTemplate = GuildText.GetMemberChangeTemplate(message.NotificationId);
		if (string.IsNullOrEmpty(memberChangeTemplate))
		{
			return false;
		}
		try
		{
			object[] array = new object[message.Param.Count];
			for (int i = 0; i < message.Param.Count; i++)
			{
				array[i] = message.Param[i];
			}
			text = string.Format(memberChangeTemplate, array);
			return !string.IsNullOrEmpty(text);
		}
		catch (FormatException ex)
		{
			Debug.LogError($"[GuildMemberChange] messageId={message.Id} 参数错误：{ex.Message}");
			return false;
		}
	}

	private void OnMemberMessagesChanged()
	{
		if (_isOpen)
		{
			RefreshMemberMessages();
		}
	}

	private void OnClickClose()
	{
		OnRequestClose?.Invoke();
	}

	public static UIGuild_MemberChange_Com CreateInstance()
	{
		return (UIGuild_MemberChange_Com)UIPackage.CreateObject("Guild", "Guild_MemberChange_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bottom = (GLabel)GetChildAt(0);
		tips_item_list = (GList)GetChildAt(5);
	}
}
