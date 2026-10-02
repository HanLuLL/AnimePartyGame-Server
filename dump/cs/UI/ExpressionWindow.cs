using System;
using System.Collections.Generic;
using Core;
using Core.Mark;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class ExpressionWindow : BaseWindow
{
	private readonly List<KeyValuePair<int, ExpressionData>> _selfExpressions = new List<KeyValuePair<int, ExpressionData>>();

	private List<ChatInfoConfigure> ChatConfigs;

	private GList _list_Expression;

	private GList _list_Chat;

	private UIExpression_Button_QuickReply _btn_QuickReply;

	private GButton _btn_OpenExpr;

	private GButton _btn_OpenChat;

	private UIExpression_Button_MapChat _btn_MapChat;

	private Controller _interactStyle;

	private Transition _showQuickReply;

	private float _markInteractTime;

	private const int _markTipRadius = 100;

	private LineRenderer line;

	private TweenerCore<float, float, FloatOptions> _fillAmountTween;

	private CtsInfo quickReplyCts;

	public ExpressionWindow(UIWindowType type)
		: base(type)
	{
	}

	public async UniTask TryShowAsync()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		_list_Expression?.onClickItem.Release();
		_list_Chat?.onClickItem.Release();
		_btn_QuickReply?.onClick.Release();
	}

	protected override void OnInit()
	{
		base.contentPane = UIExpressionWindow.CreateInstance();
		isAdapter = true;
		base.OnInit();
		if (base.contentPane is UIExpressionWindow uIExpressionWindow)
		{
			if (PlatformTarget.IsMobileTarget)
			{
				uIExpressionWindow.com_Interact_PC.visible = false;
				uIExpressionWindow.com_Interact_Mobile.visible = true;
				_list_Expression = uIExpressionWindow.com_Interact_Mobile.list_Expression;
				_list_Chat = uIExpressionWindow.com_Interact_Mobile.list_Chat;
				_btn_QuickReply = uIExpressionWindow.com_Interact_Mobile.btn_QuickReply;
				_btn_OpenExpr = uIExpressionWindow.com_Interact_Mobile.btn_OpenExpr;
				_btn_OpenChat = uIExpressionWindow.com_Interact_Mobile.btn_OpenChat;
				_btn_MapChat = uIExpressionWindow.com_Interact_Mobile.btn_MapChat;
				_interactStyle = uIExpressionWindow.com_Interact_Mobile.style;
				_showQuickReply = uIExpressionWindow.com_Interact_Mobile.showQuickReply;
			}
			else
			{
				uIExpressionWindow.com_Interact_PC.visible = true;
				uIExpressionWindow.com_Interact_Mobile.visible = false;
				_list_Expression = uIExpressionWindow.com_Interact_PC.list_Expression;
				_list_Chat = uIExpressionWindow.com_Interact_PC.list_Chat;
				_btn_QuickReply = uIExpressionWindow.com_Interact_PC.btn_QuickReply;
				_btn_OpenExpr = uIExpressionWindow.com_Interact_PC.btn_OpenExpr;
				_btn_OpenChat = uIExpressionWindow.com_Interact_PC.btn_OpenChat;
				_btn_MapChat = uIExpressionWindow.com_Interact_PC.btn_MapChat;
				_interactStyle = uIExpressionWindow.com_Interact_PC.style;
				_showQuickReply = uIExpressionWindow.com_Interact_PC.showQuickReply;
			}
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		bool flag = roomInfo?.IsNovice() ?? false;
		bool flag2 = roomInfo?.IsPVP() ?? false;
		bool flag3 = SimpleSingletonProvider<GameLogicManager>.inst.watch?.PlayerIsWatcher() ?? false;
		GButton btn_OpenExpr = _btn_OpenExpr;
		bool flag4 = (_btn_OpenChat.visible = !flag3 && !flag);
		btn_OpenExpr.visible = flag4;
		_btn_MapChat.visible = !flag3 && !flag && !flag2;
		_interactStyle.selectedIndex = 0;
		ReadyChatInfo();
		ReadySelfExpressions();
		if (PlatformTarget.IsMobileTarget)
		{
			_btn_MapChat.onTouchBegin.Add(SwitchCursorType);
		}
		else
		{
			_btn_MapChat.onClick.Add(SwitchCursorType);
		}
		Stage.inst.onTouchBegin.Add(CloseChat);
		_btn_QuickReply.txt_Tip.text = 1000006.GetLocal(UIStringType.GUI);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkEnter?.AddListener(MarkModeChange);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkExit?.AddListener(MarkModeChange);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkHoverEnter?.AddListener(MapMarkHoverEnter);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkHoverExit?.AddListener(MapMarkHoverExit);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowWaiting?.AddListener(MapMarkShowWaiting);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.HideWaiting?.AddListener(MapMarkHideWaiting);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowPreview?.AddListener(MapMarkShowPreview);
		Stage.inst.onTouchBegin.Add(TryCloseChatMenu);
		_interactStyle.onChanged.Add(OnChangeInteractStyle);
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIExpressionWindow uIExpressionWindow)
		{
			HideChatInfo();
			HideExpressions();
			if (PlatformTarget.IsMobileTarget)
			{
				_btn_MapChat.onTouchBegin.Remove(SwitchCursorType);
			}
			else
			{
				_btn_MapChat.onClick.Remove(SwitchCursorType);
			}
			Stage.inst.onTouchBegin.Remove(CloseChat);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkEnter?.RemoveListener(MarkModeChange);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkExit?.RemoveListener(MarkModeChange);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkHoverEnter?.RemoveListener(MapMarkHoverEnter);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.MarkHoverExit?.RemoveListener(MapMarkHoverExit);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowWaiting?.RemoveListener(MapMarkShowWaiting);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.HideWaiting?.RemoveListener(MapMarkHideWaiting);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowPreview?.RemoveListener(MapMarkShowPreview);
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uIExpressionWindow.graph_Line);
			Stage.inst.onTouchBegin.Remove(TryCloseChatMenu);
			CursorConfig.SetCursorType(CursorType.Mouse);
			Stage.inst.ActiveCurCursor();
			_interactStyle.onChanged.Remove(OnChangeInteractStyle);
		}
	}

	private void OnChangeInteractStyle(EventContext context)
	{
		Controller interactStyle = _interactStyle;
		if (interactStyle != null && interactStyle.selectedIndex == 1)
		{
			RefreshSelfExpressions(_selfExpressions.Count);
		}
		else
		{
			RefreshSelfExpressions(0);
		}
	}

	private void CloseChat()
	{
		if (GRoot.inst.touchTarget == null)
		{
			_interactStyle.selectedIndex = 0;
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		UpdateCrossHair();
		UpdateMapChatSelectPos();
	}

	private void ReadySelfExpressions()
	{
		_selfExpressions.Clear();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		CharacterExpression expression = SimpleSingletonProvider<CharacterAssetManager>.inst.GetExpression(selfPlayerData.player.Hero.HeroId);
		if (expression != null)
		{
			foreach (KeyValuePair<int, ExpressionData> item in expression.expressionDict)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(item.Key) || item.Value.expressionConfig.IsDefault)
				{
					_selfExpressions.Add(new KeyValuePair<int, ExpressionData>(item.Key, item.Value));
				}
			}
		}
		_btn_OpenExpr.onClick.Add(OnOpenExpressionList);
		_list_Expression.itemRenderer = OnExpressionItem;
		_list_Expression.onClickItem.Add(OnExpressionSelected);
	}

	private void RefreshSelfExpressions(int Count)
	{
		_list_Expression.numItems = Count;
		_list_Expression.ResizeToFit();
	}

	private void OnOpenExpressionList()
	{
		if (_selfExpressions.Count > 0)
		{
			_interactStyle.selectedIndex = 1;
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10016);
		}
	}

	private void OnExpressionItem(int index, GObject item)
	{
		if (_selfExpressions.Count > index && item is UIExpression_Button_ListExpression uIExpression_Button_ListExpression)
		{
			KeyValuePair<int, ExpressionData> keyValuePair = _selfExpressions[index];
			uIExpression_Button_ListExpression.data = keyValuePair.Key;
			if (keyValuePair.Value.isVideo)
			{
				SimpleSingletonProvider<CriMovieManager>.inst.PlaAutoReleaseVideo(keyValuePair.Value.expressionConfig.VideoKey, ((UICom_Expression)uIExpression_Button_ListExpression.com_Expression).loader_Expression_Video).Forget();
				((UICom_Expression)uIExpression_Button_ListExpression.com_Expression).type.selectedIndex = 1;
			}
			else
			{
				((UICom_Expression)uIExpression_Button_ListExpression.com_Expression).loader_Expression_Image.url = keyValuePair.Value.textureUrl;
				((UICom_Expression)uIExpression_Button_ListExpression.com_Expression).type.selectedIndex = 0;
			}
		}
	}

	private void OnExpressionSelected(EventContext context)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.communicate.SendChatLicense() || _selfExpressions == null || _selfExpressions.Count <= _list_Expression.selectedIndex)
		{
			return;
		}
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null)
		{
			_list_Expression.onClickItem.Retain();
			int key = _selfExpressions[_list_Expression.selectedIndex].Key;
			_interactStyle.selectedIndex = 0;
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(new BattleMessage(MessageType.EXPRESSION, selfPlayerData, key));
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestSendChatC2S(key).OnFinishedOnly.AddOnce(delegate
			{
				_list_Expression.onClickItem.Release();
			});
		}
	}

	private void HideExpressions()
	{
		_btn_OpenExpr.onClick.Remove(OnOpenExpressionList);
		_list_Expression.onClickItem.Remove(OnExpressionSelected);
	}

	public float GetOpenExprWidth()
	{
		if (_btn_OpenExpr != null)
		{
			return _btn_OpenExpr.width * _btn_OpenExpr.scale.x;
		}
		if (!PlatformTarget.IsMobileTarget)
		{
			return 80.6f;
		}
		return 124f;
	}

	private void ReadyChatInfo()
	{
		_btn_OpenChat.onClick.Add(OnOpenChatList);
		_list_Chat.onClickItem.Add(OnChatSelected);
		_list_Chat.itemRenderer = OnChatItem;
	}

	private void OnOpenChatList()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is BattleSettlementPanel)
		{
			ChatConfigs = StaticConfigure.Chat.BattleResultChats;
		}
		else
		{
			RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
			ChatConfigs = StaticConfigure.Chat.GetBattleChats((MapModeType)room.curRoomInfo.MapType, room.curRoomInfo.MapId);
		}
		if (ChatConfigs != null && ChatConfigs.Count != 0 && base.contentPane is UIExpressionWindow uIExpressionWindow)
		{
			_list_Chat.numItems = ChatConfigs.Count;
			_list_Chat.height = uIExpressionWindow.height * 0.6f;
			_list_Chat.scrollPane.touchEffect = ChatConfigs.Count > 8;
			_btn_OpenChat.onClick.Retain();
			_interactStyle.selectedIndex = 2;
			_btn_OpenChat.onClick.Release();
		}
	}

	private void OnChatItem(int index, GObject item)
	{
		if (item is GButton gButton)
		{
			gButton.title = ChatConfigs[index].ChatInfo;
		}
	}

	private void OnChatSelected()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.communicate.SendChatLicense() || ChatConfigs == null || ChatConfigs.Count <= _list_Chat.selectedIndex)
		{
			return;
		}
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null)
		{
			_list_Chat.onClickItem.Retain();
			ChatInfoConfigure chatInfoConfigure = ChatConfigs[_list_Chat.selectedIndex];
			_interactStyle.selectedIndex = 0;
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(new BattleMessage(MessageType.SHORTINFO, selfPlayerData, chatInfoConfigure.ChatID));
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestRoomShortChatC2S(chatInfoConfigure.ChatID).OnFinishedOnly.AddOnce(delegate
			{
				_list_Chat.onClickItem.Release();
			});
		}
	}

	private void HideChatInfo()
	{
		_btn_OpenChat.onClick.Remove(OnOpenChatList);
		_list_Chat.itemRenderer = null;
		_list_Chat.numItems = 0;
		_list_Chat.onClickItem.Remove(OnChatSelected);
	}

	private void SwitchCursorType()
	{
		if (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is BattleSettlementPanel))
		{
			_btn_MapChat.onClick.Retain();
			BattleSceneController.inst?.MarkManager.MarkInput.ToggleMode();
			_btn_MapChat.onClick.Release();
		}
	}

	private void UpdateCrossHair()
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow && uIExpressionWindow.com_Mark.visible)
		{
			uIExpressionWindow.com_Mark.xy = uIExpressionWindow.GlobalToLocal(Stage.inst.touchPosition);
			if (line != null)
			{
				line.SetPosition(1, new Vector3(uIExpressionWindow.com_Mark.x, 0f - uIExpressionWindow.com_Mark.y, 0f));
				float num = Vector3.Magnitude(line.GetPosition(0) - line.GetPosition(1));
				line.materials[0].SetTextureScale(Shader.PropertyToID("_MainTex"), new Vector2(num * 0.2f, 0f));
			}
		}
	}

	private void MapMarkHoverEnter()
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow)
		{
			uIExpressionWindow.com_Mark.MapChatColor.selectedIndex = 1;
		}
	}

	private void MapMarkHoverExit()
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow)
		{
			uIExpressionWindow.com_Mark.MapChatColor.selectedIndex = 0;
			uIExpressionWindow.com_Mark.showTip.selectedIndex = 0;
			MapMarkHideWaiting();
		}
	}

	private void MapMarkShowPreview(string msg)
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow && !string.IsNullOrEmpty(msg))
		{
			uIExpressionWindow.com_Mark.com_Preview.txt_Tip.text = msg;
			uIExpressionWindow.com_Mark.showTip.selectedIndex = 1;
		}
	}

	private void MarkModeChange()
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow)
		{
			CursorConfig.SetCursorType((CursorConfig.cursorType == CursorType.CrossHair) ? CursorType.Mouse : CursorType.CrossHair);
			bool flag = CursorConfig.cursorType == CursorType.CrossHair;
			if (!flag)
			{
				Stage.inst.ActiveCurCursor();
			}
			_btn_MapChat.status.selectedIndex = (flag ? 1 : 0);
			uIExpressionWindow.com_Mark.visible = flag;
			MapMarkHoverExit();
			UpdateCrossHair();
			TryShowUILine(flag).Forget();
			if (PlatformTarget.IsMobileTarget)
			{
				TryShowInteractTip(flag);
			}
		}
	}

	private void TryShowInteractTip(bool isCrossHair)
	{
		if (!(base.contentPane is UIExpressionWindow uIExpressionWindow))
		{
			return;
		}
		if (isCrossHair)
		{
			_markInteractTime = Time.unscaledTime;
		}
		else if (_markInteractTime - Time.unscaledTime < 1f)
		{
			Vector2 pt = _btn_MapChat.LocalToGlobal(Vector2.zero);
			Vector2 vector = uIExpressionWindow.GlobalToLocal(pt);
			if (Vector2.SqrMagnitude(uIExpressionWindow.com_Mark.xy - vector) < 10000f)
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(11030);
			}
		}
	}

	private async UniTask TryShowUILine(bool showLine)
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIExpressionWindow win)
		{
			if (line == null)
			{
				line = (await SimpleSingletonProvider<GameObjectManager>.inst.ShowGameObject(1013.GetEffectDataConfigure().EffectName, win.graph_Line)).GetComponent<LineRenderer>();
			}
			Vector2 pt = _btn_MapChat.LocalToGlobal(Vector2.zero);
			Vector2 vector = win.GlobalToLocal(pt);
			line.SetPosition(0, new Vector3(vector.x, 0f - vector.y, 0f));
			line.enabled = showLine;
		}
	}

	private void MapMarkShowWaiting()
	{
		GComponent gComponent = base.contentPane;
		UIExpressionWindow win = gComponent as UIExpressionWindow;
		if (win != null)
		{
			MapMarkHideWaiting();
			win.com_Mark.image_Load.visible = true;
			_fillAmountTween = DOTween.To(() => 1f, delegate(float value)
			{
				win.com_Mark.image_Load.fillAmount = value;
			}, 0f, 0.5f);
		}
	}

	private void MapMarkHideWaiting()
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow)
		{
			uIExpressionWindow.com_Mark.image_Load.visible = false;
			_fillAmountTween?.Kill(complete: true);
		}
	}

	public void CloseMapChat()
	{
		if (base.isShowing && _btn_MapChat != null)
		{
			_btn_MapChat.visible = false;
		}
	}

	public void ShowLandMenu(IMarkTarget target, UnitLand Land)
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow && (object)Land != null)
		{
			uIExpressionWindow.com_ChatMenu.visible = true;
			uIExpressionWindow.com_ChatMenu.ShowLandChat(target, Land);
			UpdateMapChatSelectPos();
		}
	}

	public void ShowPlayerMenu(IMarkTarget target, long playerId)
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow)
		{
			uIExpressionWindow.com_ChatMenu.visible = true;
			uIExpressionWindow.com_ChatMenu.ShowPlayerMenu(target, playerId);
			UpdateMapChatSelectPos();
		}
	}

	private void UpdateMapChatSelectPos()
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow && uIExpressionWindow.com_ChatMenu.visible)
		{
			Vector2 markTargetPos = uIExpressionWindow.com_ChatMenu.GetMarkTargetPos();
			markTargetPos.y = (float)Screen.height - markTargetPos.y;
			Vector2 vector = uIExpressionWindow.GlobalToLocal(markTargetPos);
			float num = uIExpressionWindow.com_ChatMenu.width * 0.5f;
			float num2 = uIExpressionWindow.com_ChatMenu.height * 0.5f;
			float max = ((uIExpressionWindow.width > uIExpressionWindow.com_ChatMenu.width) ? (uIExpressionWindow.width - num) : uIExpressionWindow.width);
			float max2 = ((uIExpressionWindow.height > uIExpressionWindow.com_ChatMenu.height) ? (uIExpressionWindow.height - num2) : uIExpressionWindow.height);
			uIExpressionWindow.com_ChatMenu.x = Math.Clamp(vector.x, num, max);
			uIExpressionWindow.com_ChatMenu.y = Math.Clamp(vector.y, num2, max2);
		}
	}

	private void TryCloseChatMenu()
	{
		if (base.contentPane is UIExpressionWindow uIExpressionWindow && uIExpressionWindow.com_ChatMenu.visible && !uIExpressionWindow.com_ChatMenu.container.IsAncestorOf(Stage.inst.touchTarget))
		{
			uIExpressionWindow.com_ChatMenu.visible = false;
		}
	}

	public async UniTask TryShowQuickReply(int chatId)
	{
		if (!base.isShowing || _btn_QuickReply == null || _showQuickReply == null)
		{
			return;
		}
		quickReplyCts?.Cancel();
		quickReplyCts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		_btn_QuickReply.title = chatId.GetLocal(UIStringType.Chat);
		_btn_QuickReply.visible = true;
		_showQuickReply.Play(delegate
		{
			_btn_QuickReply.visible = true;
		});
		_btn_QuickReply.onClick.Set((EventCallback0)delegate
		{
			_btn_QuickReply.onClick.Retain();
			quickReplyCts?.Cancel();
			_showQuickReply.PlayReverse(delegate
			{
				_btn_QuickReply.visible = false;
			});
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(new BattleMessage(MessageType.SHORTINFO, selfPlayerData, chatId));
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestRoomShortChatC2S(chatId).OnFinishedOnly.AddOnce(delegate
			{
				_btn_QuickReply.onClick.Release();
			});
		});
		if (!(await UniTask.Delay(5000, ignoreTimeScale: false, PlayerLoopTiming.Update, quickReplyCts.Token).SuppressCancellationThrow()))
		{
			_btn_QuickReply.visible = false;
		}
	}
}
