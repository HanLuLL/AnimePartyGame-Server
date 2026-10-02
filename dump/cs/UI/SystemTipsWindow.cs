using System;
using Core;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class SystemTipsWindow : BaseWindow
{
	public SystemTipsWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UISystemTipsWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
	}

	protected override void OnHide()
	{
	}

	public void ShowTips(int msgId, float duration = 1.5f)
	{
		ShowTips(msgId.GetLocal(UIStringType.Message), duration);
	}

	public async void ShowTips(string content, float duration = 1.5f)
	{
		if (!(duration <= 0f))
		{
			await TryShowAsync();
			GComponent gComponent = base.contentPane;
			if (gComponent is UISystemTipsWindow win)
			{
				win.txt_Content.text = content;
				win.showMessage.selectedIndex = 1;
				await UniTask.Delay(TimeSpan.FromSeconds(duration));
				win.showMessage.selectedIndex = 0;
			}
		}
	}

	public void showMarquee(int msgId)
	{
		showMarquee(msgId.GetLocal(UIStringType.Message));
	}

	public async void showMarquee(string content)
	{
		if (string.IsNullOrEmpty(content))
		{
			return;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UISystemTipsWindow win = gComponent as UISystemTipsWindow;
		if (win != null)
		{
			win.showZoetrope.selectedIndex = 1;
			win.com_Zeotrope.StartShow(content, delegate
			{
				win.showZoetrope.selectedIndex = 0;
			});
		}
	}

	public void HideMarquee()
	{
		if (base.isShowing && base.contentPane is UISystemTipsWindow uISystemTipsWindow)
		{
			uISystemTipsWindow.showZoetrope.selectedIndex = 0;
			uISystemTipsWindow.com_Zeotrope.FinishShow();
		}
	}

	public async void ShowInviteSignal(AstralInviteType type)
	{
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		if (SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.InTeam || SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom || playerInfo.OnlineStatus == 1)
		{
			return;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UISystemTipsWindow win = gComponent as UISystemTipsWindow;
		if (win != null)
		{
			GameSettings.PlayMessageVibrate();
			if (win.chatSignal.selectedIndex == 1)
			{
				win.chatSignal.selectedIndex = 0;
			}
			win.inviteSignal.selectedIndex = 1;
			win.btn_InviteSignal.onClick.Set((EventCallback0)delegate
			{
				win.btn_InviteSignal.onClick.Retain();
				SimpleSingletonProvider<UIManager>.inst.invite.TryShowInvite(type);
				win.inviteSignal.selectedIndex = 0;
				win.btn_InviteSignal.onClick.Release();
			});
		}
	}

	public async void ShowChatSignal(long playerId)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Home || SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom || SimpleSingletonProvider<UIManager>.inst.chat.isShowing || SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.MatchStatus == MatchTeamInfo.Types.State.Playing)
		{
			return;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UISystemTipsWindow win = gComponent as UISystemTipsWindow;
		if (win == null || win.inviteSignal.selectedIndex == 1)
		{
			return;
		}
		win.chatSignal.selectedIndex = 1;
		win.btn_ChatSignal.onClick.Set((EventCallback0)delegate
		{
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Home && !SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
			{
				win.btn_ChatSignal.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendListC2S().OnFinished.AddOnce(delegate(RPCAsyncResult _)
				{
					if (_.errId == 0)
					{
						OnEnableChat(playerId);
						win.chatSignal.selectedIndex = 0;
						win.btn_ChatSignal.onClick.Release();
					}
				});
			}
		});
	}

	private async void OnEnableChat(long playerId)
	{
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Friend);
		SimpleSingletonProvider<UIManager>.inst.chat.TryShowChatWindow(playerId);
	}

	public void CloseChatSignal()
	{
		if (base.contentPane is UISystemTipsWindow uISystemTipsWindow)
		{
			uISystemTipsWindow.chatSignal.selectedIndex = 0;
		}
	}
}
