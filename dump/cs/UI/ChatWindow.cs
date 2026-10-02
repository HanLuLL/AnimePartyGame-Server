using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class ChatWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private List<SessionData> chatsData;

	private SessionData chattingSession;

	private UIChat_Com_ExpressionPopup _chat_Com_ExpressionPopup;

	private UIChat_Com_SessionOperatePopup _chat_Com_SessionOperatePopup;

	private float _currentSendMsgRefreshTime;

	private const float REFRESH_COOLDOWN = 1f;

	private UIChat_Button_Session SelectedSession;

	private long selfId => SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();

	public ChatWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIChatWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		await StaticConfigure.LoadSensitiveWords();
	}

	protected override void OnShown()
	{
		base.OnShown();
		blurBgCtrl.OnShown(this);
		if (base.contentPane is UIChatWindow uIChatWindow)
		{
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			SimpleSingletonProvider<UIManager>.inst.systemTips.CloseChatSignal();
			uIChatWindow.com_chat.btn_Return.onClick.Add(base.Hide);
			uIChatWindow.mohu.onClick.Add(base.Hide);
			uIChatWindow.com_chat.btn_Send.onClick.Add(SendMsg);
			uIChatWindow.com_chat.Input_Send.onChanged.Add(InputMsg);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.newMessage.AddListener(RefreshChatFromServer);
			uIChatWindow.com_chat.Input_Send.onSubmit.Add(SendMsg);
			uIChatWindow.com_chat.Input_Send.SubmitOnEnter = true;
			uIChatWindow.com_chat.btn_Expression.onClick.Add(OpenExpression);
			RefreshExpressionButtonStatus();
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.newExpression.AddListener(RefreshExpressionButtonStatus);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		blurBgCtrl.OnHide();
		if (base.contentPane is UIChatWindow uIChatWindow)
		{
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			uIChatWindow.com_chat.btn_Return.onClick.Remove(base.Hide);
			uIChatWindow.mohu.onClick.Remove(base.Hide);
			uIChatWindow.com_chat.btn_Send.onClick.Remove(SendMsg);
			uIChatWindow.com_chat.Input_Send.onChanged.Remove(InputMsg);
			uIChatWindow.com_chat.list_Chat.numItems = 0;
			uIChatWindow.com_chat.list_Session.numItems = 0;
			chattingSession = null;
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.newMessage.RemoveListener(RefreshChatFromServer);
			StaticConfigure.UnloadSensitiveWords();
			uIChatWindow.com_chat.Input_Send.onSubmit.Remove(SendMsg);
			uIChatWindow.com_chat.btn_Expression.onClick.Remove(OpenExpression);
			CloseExpression();
			GRoot.inst.HidePopup(_chat_Com_SessionOperatePopup);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.newExpression.RemoveListener(RefreshExpressionButtonStatus);
		}
	}

	public override void Dispose()
	{
		_chat_Com_SessionOperatePopup?.Dispose();
		_chat_Com_SessionOperatePopup = null;
		_chat_Com_ExpressionPopup?.Dispose();
		_chat_Com_ExpressionPopup = null;
		base.Dispose();
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIChatWindow && base.isShowing && context.inputEvent.keyCode == KeyCode.Escape)
		{
			HideImmediately();
		}
	}

	public void TryShowChatWindow(long playerId)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestGetPlayerSimpleC2S(playerId, delegate(RPCAsyncResult result)
		{
			if (result.errId == 0)
			{
				ShowChatWin(playerId).Forget();
			}
		});
	}

	private async UniTask ShowChatWin(long playerId)
	{
		await TryShowAsync();
		if (!(base.contentPane is UIChatWindow uIChatWindow))
		{
			return;
		}
		uIChatWindow.com_chat.list_Chat.SetVirtual();
		uIChatWindow.com_chat.list_Chat.itemProvider = GetChatItemResource;
		uIChatWindow.com_chat.list_Chat.itemRenderer = RendererChatItem;
		uIChatWindow.com_chat.list_Session.itemRenderer = RendererSession;
		SimpleSingletonProvider<GameLogicManager>.inst.communicate.TryGetSessionByPlayerId(playerId);
		chatsData = SimpleSingletonProvider<GameLogicManager>.inst.communicate.GetSessionList();
		uIChatWindow.com_chat.list_Session.numItems = chatsData.Count;
		for (int i = 0; i < chatsData.Count; i++)
		{
			if (playerId == chatsData[i].targetPlayerId)
			{
				if (uIChatWindow.com_chat.list_Session.GetChildAt(i) is UIChat_Button_Session uIChat_Button_Session)
				{
					uIChatWindow.com_chat.list_Session.ScrollToView(i);
					uIChat_Button_Session.com_Session.btn_Select.onClick.Call();
				}
				break;
			}
		}
	}

	private string GetChatItemResource(int index)
	{
		if (chattingSession.messages[index].fromMe)
		{
			return "ui://y0luzhk8b93d6";
		}
		return "ui://y0luzhk8b93d3";
	}

	private void RendererChatItem(int index, GObject item)
	{
		ChatMessage chatMessage = chattingSession.messages[index];
		if (item is UIChat_Com_Left uIChat_Com_Left)
		{
			uIChat_Com_Left.loader_Head.url = chattingSession.TargetHeadURL;
			uIChat_Com_Left.txt_Title.text = chatMessage._FormatTime;
			RendererChatItemInfo(chatMessage, uIChat_Com_Left.txt_Msg, uIChat_Com_Left.state, uIChat_Com_Left.com_Expression);
		}
		if (item is UIChat_Com_Right uIChat_Com_Right)
		{
			uIChat_Com_Right.loader_Head.url = chattingSession.myHeadURL;
			uIChat_Com_Right.txt_Title.text = chatMessage._FormatTime;
			RendererChatItemInfo(chatMessage, uIChat_Com_Right.txt_Msg, uIChat_Com_Right.state, uIChat_Com_Right.com_Expression);
		}
	}

	private void RendererChatItemInfo(ChatMessage msgData, GRichTextField txt_msg, Controller _state, UIChat_Com_ExpressionItem btn_ExpressionItem)
	{
		if (msgData.Type == MessageType.EXPRESSION && int.TryParse(msgData.msg, out var result) && btn_ExpressionItem.PlayExpression(result))
		{
			txt_msg.text = "1\n\n\n";
			_state.selectedIndex = 1;
		}
		else
		{
			_state.selectedIndex = 0;
			txt_msg.text = msgData.msg;
		}
	}

	private void RefreshMsgList()
	{
		if (base.contentPane is UIChatWindow uIChatWindow)
		{
			if (chattingSession == null)
			{
				uIChatWindow.com_chat.txt_PlayerName.text = "";
				uIChatWindow.com_chat.Input_Send.text = "";
				uIChatWindow.com_chat.list_Chat.numItems = 0;
			}
			else
			{
				uIChatWindow.com_chat.Input_Send.text = (string.IsNullOrWhiteSpace(chattingSession.draft) ? "" : chattingSession.draft);
				uIChatWindow.com_chat.list_Chat.numItems = chattingSession.messages.Count;
				uIChatWindow.com_chat.list_Chat.scrollPane.ScrollBottom();
				uIChatWindow.com_chat.btn_Send.onClick.Release();
				uIChatWindow.com_chat.Input_Send.onSubmit.Release();
			}
		}
	}

	private void RefreshChatFromServer(long playerId, bool newSession)
	{
		if (newSession)
		{
			RefreshChatSessionList();
		}
		else if (chattingSession != null && chattingSession.targetPlayerId == playerId)
		{
			RefreshMsgList();
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestReadChatMsgC2S(playerId);
		}
		else
		{
			RefreshChatSessionList();
		}
	}

	private async void SendMsg()
	{
		if (chattingSession == null || !(base.contentPane is UIChatWindow uIChatWindow))
		{
			return;
		}
		uIChatWindow.com_chat.Input_Send.text = RemoveEmoji(uIChatWindow.com_chat.Input_Send.text);
		if (!string.IsNullOrEmpty(uIChatWindow.com_chat.Input_Send.text) && !string.IsNullOrWhiteSpace(uIChatWindow.com_chat.Input_Send.text))
		{
			if (Time.time - _currentSendMsgRefreshTime > 1f)
			{
				uIChatWindow.com_chat.btn_Send.onClick.Retain();
				uIChatWindow.com_chat.Input_Send.onSubmit.Retain();
				chattingSession.draft = "";
				int fRIEND_CHAT_BYTES_LIMIT = StaticGlobalData.FRIEND_CHAT_BYTES_LIMIT;
				Debug.Log("SendMsg=========" + uIChatWindow.com_chat.Input_Send.text);
				uIChatWindow.com_chat.txt_wordLimit.SetVar("current", Mathf.Min(0, fRIEND_CHAT_BYTES_LIMIT).ToString()).SetVar("limit", fRIEND_CHAT_BYTES_LIMIT.ToString()).FlushVars();
				RequestSendMsg(await SensitiveWords.DealSensitiveWord(uIChatWindow.com_chat.Input_Send.text));
				RefreshMsgList();
				_currentSendMsgRefreshTime = Time.time;
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1144);
			}
		}
	}

	private void RequestSendMsg(string msg)
	{
		if (chattingSession != null)
		{
			long targetPlayerId = chattingSession.targetPlayerId;
			int num = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds();
			chattingSession.UpdateChatMsg(selfId, targetPlayerId, msg, num);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestFriendSendMsgC2S(chattingSession.targetPlayerId, msg);
			CloseExpression();
			RefreshMsgList();
		}
	}

	private void InputMsg(EventContext context)
	{
		if (!(base.contentPane is UIChatWindow uIChatWindow))
		{
			return;
		}
		int fRIEND_CHAT_BYTES_LIMIT = StaticGlobalData.FRIEND_CHAT_BYTES_LIMIT;
		string input = uIChatWindow.com_chat.Input_Send.text;
		input = RemoveEmoji(input);
		if (string.IsNullOrEmpty(input))
		{
			uIChatWindow.com_chat.txt_wordLimit.SetVar("current", "0").SetVar("limit", fRIEND_CHAT_BYTES_LIMIT.ToString()).FlushVars();
			return;
		}
		if (input.Length > fRIEND_CHAT_BYTES_LIMIT)
		{
			input = input.Substring(0, fRIEND_CHAT_BYTES_LIMIT);
		}
		uIChatWindow.com_chat.Input_Send.text = input;
		if (chattingSession != null)
		{
			chattingSession.draft = uIChatWindow.com_chat.Input_Send.text;
		}
		uIChatWindow.com_chat.txt_wordLimit.SetVar("current", Mathf.Min(input.Length, fRIEND_CHAT_BYTES_LIMIT).ToString()).SetVar("limit", fRIEND_CHAT_BYTES_LIMIT.ToString()).FlushVars();
	}

	private static string RemoveEmoji(string input)
	{
		string pattern = "[\\uD83C-\\uDBFF\\uDC00-\\uDFFF]+";
		return Regex.Replace(input, pattern, "");
	}

	private void RefreshChatSessionList()
	{
		if (!(base.contentPane is UIChatWindow uIChatWindow))
		{
			return;
		}
		if (chatsData.Count == 0)
		{
			Hide();
			return;
		}
		uIChatWindow.com_chat.list_Session.numItems = chatsData.Count;
		if (SelectedSession != null)
		{
			SelectedSession.com_Session.isChoosed.selectedIndex = 0;
		}
		if (chattingSession == null)
		{
			RefreshMsgList();
			return;
		}
		for (int i = 0; i < chatsData.Count; i++)
		{
			if (chatsData[i].targetPlayerId == chattingSession.targetPlayerId)
			{
				if (uIChatWindow.com_chat.list_Session.GetChildAt(i) is UIChat_Button_Session uIChat_Button_Session)
				{
					uIChat_Button_Session.com_Session.isChoosed.selectedIndex = 1;
					uIChat_Button_Session.com_Session.UpdateColor(selectStatus: true);
					SelectedSession = uIChat_Button_Session;
				}
				break;
			}
		}
	}

	private void RendererSession(int index, GObject item)
	{
		UIChat_Button_Session _session = item as UIChat_Button_Session;
		if (_session == null)
		{
			return;
		}
		_session.visible = false;
		_session.Cut_in.Play(1, 0.01f * (float)index, delegate
		{
			_session.visible = true;
		}, null);
		_session.com_Session.Refresh(chatsData[index]);
		_session.com_Session.btn_Select.onClick.Set((EventCallback0)delegate
		{
			_session.onClick.Retain();
			if (SelectedSession != null && SelectedSession != _session)
			{
				SelectedSession.com_Session.UpdateColor(selectStatus: false);
				SelectedSession.com_Session.isChoosed.selectedIndex = 0;
			}
			SelectedSession = _session;
			SelectedSession.com_Session.isChoosed.selectedIndex = 1;
			if (chattingSession == null || chattingSession.targetPlayerId != chatsData[index].targetPlayerId)
			{
				if (chatsData[index].isHaveUnReadMsg || chatsData[index].messages.Count == 0)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestGetChatMsgC2S(chatsData[index].targetPlayerId).OnFinishedOnly.AddOnce(delegate
					{
						RefreshSessionAndMsg(_session, chatsData[index]);
					});
				}
				else
				{
					RefreshSessionAndMsg(_session, chatsData[index]);
				}
			}
		});
		_session.com_Session.btn_Operate.onClick.Set((EventCallback0)delegate
		{
			_session.com_Session.btn_Operate.onClick.Retain();
			if (_chat_Com_SessionOperatePopup == null)
			{
				_chat_Com_SessionOperatePopup = UIChat_Com_SessionOperatePopup.CreateInstance();
			}
			_chat_Com_SessionOperatePopup.Refresh(delegate
			{
				chattingSession = null;
				GRoot.inst.HidePopup(_chat_Com_SessionOperatePopup);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestDeleteChatC2S(chatsData[index].targetPlayerId).OnFinishedOnly.AddOnce(RefreshChatSessionList);
			});
			GRoot.inst.ShowPopup(_chat_Com_SessionOperatePopup, _session.com_Session.btn_Operate);
			_session.com_Session.btn_Operate.onClick.Release();
		});
	}

	private void RefreshSessionAndMsg(UIChat_Button_Session _session, SessionData newSessionData)
	{
		if (base.contentPane is UIChatWindow uIChatWindow)
		{
			if (SelectedSession != null)
			{
				SelectedSession.com_Session.UpdateColor(selectStatus: true);
				SelectedSession.com_Session.RefreshStatus(newSessionData);
			}
			chattingSession = SimpleSingletonProvider<GameLogicManager>.inst.communicate.TryGetSessionByPlayerId(newSessionData.targetPlayerId);
			_session.com_Session.redStatus.selectedIndex = 0;
			uIChatWindow.com_chat.txt_PlayerName.text = newSessionData.TargetName;
			RefreshMsgList();
			_session.onClick.Release();
		}
	}

	private void OpenExpression()
	{
		if (base.contentPane is UIChatWindow uIChatWindow)
		{
			uIChatWindow.com_chat.btn_Expression.onClick.Retain();
			if (_chat_Com_ExpressionPopup == null)
			{
				_chat_Com_ExpressionPopup = UIChat_Com_ExpressionPopup.CreateInstance();
			}
			GRoot.inst.ShowPopup(_chat_Com_ExpressionPopup, uIChatWindow.com_chat.btn_Expression);
			Vector2 pt = uIChatWindow.com_chat.btn_Expression.LocalToGlobal(Vector2.zero);
			_chat_Com_ExpressionPopup.xy = GRoot.inst.GlobalToLocal(pt);
			_chat_Com_ExpressionPopup.RefreshExpression(RequestSendMsg);
			uIChatWindow.com_chat.btn_Expression.onClick.Release();
		}
	}

	private void CloseExpression()
	{
		GRoot.inst.HidePopup(_chat_Com_ExpressionPopup);
	}

	private void RefreshExpressionButtonStatus()
	{
		if (base.contentPane is UIChatWindow uIChatWindow)
		{
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.communicate.IsNewExpression();
			uIChatWindow.com_chat.btn_Expression.redPoint.selectedIndex = (flag ? 1 : 0);
		}
	}
}
