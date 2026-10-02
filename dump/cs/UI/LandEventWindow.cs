using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace UI;

public class LandEventWindow : BaseWindow
{
	private readonly List<EventInfoConfigure> _eventInfoConfigures = new List<EventInfoConfigure>();

	public LandEventWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandEventWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<LandEventWindow> ShowLand()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		return this;
	}

	protected override void OnShown()
	{
		base.OnShown();
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILandEventWindow uILandEventWindow)
		{
			uILandEventWindow.eventUI.selectedIndex = 0;
		}
	}

	public async void DealLand_Destiny(party.model.Action _action)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (playerDataById.CharacterInst != null)
		{
			await playerDataById.CharacterInst.SwitchCamera();
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.land.RequestTriggerDestinyC2S(_action.Sn);
		}
	}

	public async UniTask RefreshDestinyData(long _player, int _destinyId)
	{
		GComponent gComponent = base.contentPane;
		UILandEventWindow win = gComponent as UILandEventWindow;
		if (win != null)
		{
			win.eventUI.selectedIndex = 0;
			if (StaticConfigure.Destiny.InfoDict.TryGetValue(_destinyId, out var value))
			{
				string cardIndex = ((value.CardNumb == 0) ? "" : value.CardNumb.GetLocal(UIStringType.Destiny));
				string cardTips = ((value.CommentId == 0) ? "" : value.CommentId.GetLocal(UIStringType.Destiny));
				CommonUIManager.RendererCardInRoom(0L, win.com_Card.com_Card as UICom_Card, value.Image, value.NameID.GetLocal(UIStringType.Destiny), value.DescId.GetLocal(UIStringType.Destiny), cardIndex, cardTips, 0, value.CardType);
			}
			win.showCard.Play();
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => win.showCard.playing);
			HideImmediately();
		}
	}

	public async UniTask RefreshEventData(long _playerId, int eventId)
	{
		GComponent gComponent = base.contentPane;
		UILandEventWindow win = gComponent as UILandEventWindow;
		if (win != null)
		{
			win.eventUI.selectedIndex = 0;
			EventInfoConfigure eventConfigure = eventId.GetEventConfigure();
			string cardIndex = ((eventConfigure.CardNumb == 0) ? "" : eventConfigure.CardNumb.GetLocal(UIStringType.Event));
			string cardTips = ((eventConfigure.CommentId == 0) ? "" : eventConfigure.CommentId.GetLocal(UIStringType.Event));
			CommonUIManager.RendererCardInRoom(0L, win.com_Card.com_Card as UICom_Card, eventConfigure.GetImage(), eventConfigure.NameID.GetLocal(UIStringType.Event), eventConfigure.DescId.GetLocal(UIStringType.Event), cardIndex, cardTips, 0, eventConfigure.CardType);
			win.showCard.Play();
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => win.showCard.playing);
			CloseEventWin();
		}
	}

	public async UniTask RefreshMapEventData(int mapCardID)
	{
		GComponent gComponent = base.contentPane;
		UILandEventWindow win = gComponent as UILandEventWindow;
		if (win == null)
		{
			return;
		}
		win.eventUI.selectedIndex = 0;
		if (StaticConfigure.MapEvent.MapEventCardDict.TryGetValue(mapCardID, out var value))
		{
			string cardIndex = ((value.CardNumb == 0) ? "" : value.CardNumb.GetLocal(UIStringType.MapEvent));
			string cardTips = ((value.CommentId == 0) ? "" : value.CommentId.GetLocal(UIStringType.MapEvent));
			CommonUIManager.RendererCardInRoom(0L, win.com_Card.com_Card as UICom_Card, value.GetImage(), value.NameID.GetLocal(UIStringType.MapEvent), value.DescId.GetLocal(UIStringType.MapEvent), cardIndex, cardTips, 0, value.CardType);
			win.showCard.Play();
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => win.showCard.playing);
		}
		CloseEventWin();
	}

	public async UniTask ShowEvent_30001(List<long> playerIds, List<int> goldList, int subPlayerNum)
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UILandEventWindow win && playerIds.Count > 0)
		{
			UniTask[] showTask = new UniTask[playerIds.Count];
			win.eventUI.selectedIndex = 2;
			win.list_AllPlayers.columnCount = ((playerIds.Count == 2) ? 200 : ((playerIds.Count == 3) ? 100 : 10));
			win.list_AllPlayers.itemRenderer = delegate(int index, GObject item)
			{
				if (item is UILandEvent_Graph_Player item2)
				{
					showTask[index] = ShowPlayerByEvent_30001(playerIds, goldList, subPlayerNum, item2, index);
				}
			};
			win.list_AllPlayers.numItems = playerIds.Count;
			await SimpleSingletonProvider<DelaySignalManager>.inst.WhenAll(showTask);
			for (int num = 0; num < playerIds.Count; num++)
			{
				if (win.list_AllPlayers.GetChildAt(num) is UILandEvent_Graph_Player uILandEvent_Graph_Player)
				{
					SimpleSingletonProvider<CharacterAssetManager>.inst.StopAnimation(uILandEvent_Graph_Player.loader_Animation);
				}
			}
		}
		HideImmediately();
	}

	private async UniTask ShowPlayerByEvent_30001(List<long> playerIds, List<int> goldList, int subPlayerNum, UILandEvent_Graph_Player _item, int index)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerIds[index]);
		string animeName = ((index < subPlayerNum) ? "Cry" : "Cheer");
		SimpleSingletonProvider<CharacterAssetManager>.inst.PlayAnimation(playerDataById, animeName, _item.loader_Animation, Vector2.one * 10f).grayed = false;
		_item.loader_Animation.visible = true;
		_item.playerState.selectedIndex = ((index < subPlayerNum) ? 1 : 0);
		_item.txt_AddGold.SetVar("gold", goldList[index].ToString("+#;-#;0")).FlushVars();
		_item.txt_SubGold.SetVar("gold", goldList[index].ToString("+#;-#;0")).FlushVars();
		string effectKey = ((index < subPlayerNum) ? "Gold_Down" : "Gold_UP");
		if (await PlayGoldShow(effectKey, goldList[index], _item.loader_GoldEffect, 10f))
		{
			_item.showGold.Play();
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => _item.showGold.playing);
		}
	}

	private async UniTask<bool> PlayGoldShow(string effectKey, int detal, GGraph graph, float scale = 1f)
	{
		int num = Mathf.Min(Mathf.Abs(detal), 5);
		for (int i = 0; i < num; i++)
		{
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectKey, graph, scale);
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(0.10000000149011612)))
			{
				return false;
			}
		}
		return true;
	}

	public async UniTask ShowSkill10202(party.model.Action action)
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(action.PlayerId, 11001);
			return;
		}
		await ShowLand();
		GComponent gComponent = base.contentPane;
		UILandEventWindow win = gComponent as UILandEventWindow;
		if (win == null)
		{
			return;
		}
		win.eventUI.selectedIndex = 3;
		long actionSn = action.Sn;
		SelectEventC2S selectEvent = ByteBuf.ReadObject<SelectEventC2S>(action.Data.ToByteArray());
		RepeatedField<int> events = selectEvent.Events;
		_eventInfoConfigures.Clear();
		foreach (int @event in selectEvent.Events)
		{
			if (!StaticConfigure.Event.InfoDict.TryGetValue(@event, out var value))
			{
				Debug.LogError($"Event表中不存在ID：{events} 的事件！");
			}
			else
			{
				_eventInfoConfigures.Add(value);
			}
		}
		win.list_Event.itemRenderer = RenderEvent;
		win.list_Event.numItems = _eventInfoConfigures.Count;
		win.list_Event.selectedIndex = -1;
		win.btn_confirm.onClick.Set((EventCallback0)delegate
		{
			if (actionSn != 0L)
			{
				win.btn_confirm.onClick.Retain();
				selectEvent.Idx = win.list_Event.selectedIndex;
				if (selectEvent.Idx == -1)
				{
					selectEvent.Idx = 0;
				}
				selectEvent.Info = new ActionInfo();
				selectEvent.Info.Sn = actionSn;
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestSelectEventC2S(selectEvent).OnFinishedOnly.AddOnce(delegate
				{
					win.btn_confirm.onClick.Release();
					if (base.isShowing)
					{
						CloseEventWin();
					}
				});
				OperationTimer.CancelOperatTimer(actionSn);
				actionSn = 0L;
			}
		});
		OperationTimer.ActionDownTime(actionSn, 5317, delegate
		{
			if (actionSn != 0L)
			{
				win.btn_confirm.onClick.Call();
			}
		});
	}

	private void RenderEvent(int index, GObject item)
	{
		if (base.contentPane is UILandEventWindow uILandEventWindow && item is UILandEvent_Button_Card uILandEvent_Button_Card)
		{
			string cardIndex = ((_eventInfoConfigures[index].CardNumb == 0) ? "" : _eventInfoConfigures[index].CardNumb.GetLocal(UIStringType.Event));
			string cardTips = ((_eventInfoConfigures[index].CommentId == 0) ? "" : _eventInfoConfigures[index].CommentId.GetLocal(UIStringType.Event));
			CommonUIManager.RendererCardInRoom(0L, uILandEvent_Button_Card.com_Card as UICom_Card, _eventInfoConfigures[index].GetImage(), _eventInfoConfigures[index].NameID.GetLocal(UIStringType.Event), _eventInfoConfigures[index].DescId.GetLocal(UIStringType.Event), cardIndex, cardTips, 0, _eventInfoConfigures[index].CardType);
			uILandEvent_Button_Card.selected = index == uILandEventWindow.list_Event.selectedIndex;
		}
	}

	private void CloseEventWin()
	{
		if (base.contentPane is UILandEventWindow uILandEventWindow)
		{
			Hide();
			uILandEventWindow.visible = true;
			uILandEventWindow.com_Dice.visible = false;
			uILandEventWindow.com_Dice.visible = false;
		}
	}

	public async UniTask ShowEventThrowDice(long playerId, int point)
	{
		BattlePlayerData target = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (target == null)
		{
			return;
		}
		await ShowLand();
		if (base.contentPane is UILandEventWindow uILandEventWindow)
		{
			uILandEventWindow.eventUI.selectedIndex = 1;
			UniTaskCompletionSource tcs = new UniTaskCompletionSource();
			uILandEventWindow.com_Dice.visible = true;
			CommonUIManager.PlayDice(target.player.Slot, point, uILandEventWindow.com_Dice as UICom_Point, delegate
			{
				ShowEventThrowDiceResult(tcs).Forget();
			});
			await tcs.Task;
			CloseEventWin();
		}
	}

	private async UniTask ShowEventThrowDiceResult(UniTaskCompletionSource tcs)
	{
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
		tcs.TrySetResult();
	}
}
