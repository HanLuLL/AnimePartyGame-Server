using System;
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

public class LandDivinationWindow : BaseWindow
{
	private long divinationSn;

	private BattlePlayerData playerData;

	private UILandDivination_Button_DivinationCard _targetItem;

	private UILandDivination_Button_DivinationCard _disappearItem;

	public LandDivinationWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandDivinationWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<LandDivinationWindow> ShowLand(bool init = true)
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		if (base.contentPane is UILandDivinationWindow uILandDivinationWindow && !init)
		{
			uILandDivinationWindow.btn_Divination_1.touchable = false;
			uILandDivinationWindow.btn_Divination_2.touchable = false;
		}
		return this;
	}

	private void OnHideControllerChange()
	{
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UILandDivinationWindow uILandDivinationWindow)
		{
			uILandDivinationWindow.swf.selectedIndex = (GameSettings.angelMode ? 1 : 0);
			uILandDivinationWindow.btn_Divination_1.stage.selectedIndex = 0;
			uILandDivinationWindow.btn_Divination_2.stage.selectedIndex = 0;
			uILandDivinationWindow.btn_Divination_1.rotationY = 0f;
			uILandDivinationWindow.btn_Divination_2.rotationY = 0f;
			uILandDivinationWindow.btn_Divination_1.effectOutline.visible = false;
			uILandDivinationWindow.btn_Divination_2.effectOutline.visible = false;
			uILandDivinationWindow.HideUI.onChanged.Add(OnHideControllerChange);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		HideDivinationWin();
		if (_disappearItem != null)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(_disappearItem.effectOutline);
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public async UniTask DealLand_Divination(party.model.Action _action)
	{
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (playerData.CharacterInst != null)
		{
			await playerData.CharacterInst.SwitchCamera();
		}
		TriggerDivinationC2S _divinationData = ByteBuf.ReadObject<TriggerDivinationC2S>(_action.Data.ToByteArray());
		divinationSn = _action.Sn;
		await ShowLand();
		RefreshDivinationData(_divinationData.CanChoiceIds, _action.Sn);
	}

	public void RefreshDivinationData(RepeatedField<int> _divinationIds, long _sn)
	{
		GComponent gComponent = base.contentPane;
		UILandDivinationWindow win = gComponent as UILandDivinationWindow;
		if (win == null)
		{
			return;
		}
		win.btn_Divination_1.visible = true;
		win.btn_Divination_2.visible = true;
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id))
		{
			win.HideUI.selectedIndex = 0;
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(playerData.player.Id, 11013);
		}
		else
		{
			win.HideUI.selectedIndex = 1;
			OperationTimer.ActionDownTime(divinationSn, 5069, delegate
			{
				win.btn_Divination_1.onClick.Call();
			});
		}
		RendererDivinationCard(1, _divinationIds[0], win.btn_Divination_1);
		RendererDivinationCard(2, _divinationIds[1], win.btn_Divination_2);
		win.btn_Divination_1.touchable = true;
		win.btn_Divination_2.touchable = true;
		win.Progress.selectedIndex = 0;
		win.txt_Tip.SetVar("color", ColorUtility.ToHtmlStringRGB(GameConfig.slotColor[playerData.player.Slot])).SetVar("playerName", CharacterHandle.GetCharacterName(playerData.player.characterConfig.Id, playerData.player.characterConfig.CharacterType)).FlushVars();
	}

	private void RendererDivinationCard(int selectedIndex, int _divinationId, UILandDivination_Button_DivinationCard btn)
	{
		GComponent gComponent = base.contentPane;
		UILandDivinationWindow win = gComponent as UILandDivinationWindow;
		if (win == null)
		{
			return;
		}
		if (StaticConfigure.Divination.InfoDict.TryGetValue(_divinationId, out var value))
		{
			string cardIndex = ((value.CardNumb == 0) ? "" : value.CardNumb.GetLocal(UIStringType.Divination));
			string cardTips = ((value.CommentId == 0) ? "" : value.CommentId.GetLocal(UIStringType.Divination));
			CommonUIManager.RendererCardInRoom(0L, btn.com_Card as UICom_Card, value.Image, value.NameID.GetLocal(UIStringType.Divination), value.DescId.GetLocal(UIStringType.Divination), cardIndex, cardTips, 0, value.CardType);
		}
		btn.selected = false;
		btn.touchable = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id);
		btn.visible = true;
		btn.data = _divinationId;
		btn.onClick.Set((EventCallback0)delegate
		{
			if (divinationSn != 0L)
			{
				btn.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestTriggerDivinationC2S(divinationSn, _divinationId).OnFinishedOnly.AddOnce(delegate
				{
					win.btn_Divination_1.touchable = false;
					win.btn_Divination_2.touchable = false;
					divinationSn = 0L;
					btn.onClick.Release();
				});
			}
		});
		btn.showEye.onChanged.Set((EventCallback0)delegate
		{
			win.select.selectedIndex = ((btn.showEye.selectedIndex != 0) ? selectedIndex : 0);
		});
	}

	public async UniTask UpdateDivinationDataSelect(long _playerId, int _divinationId, int _divinationTarget, RepeatedField<long> _targetPlayerIds)
	{
		if (_playerId != playerData.CharacterInst.player.Id)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UILandDivinationWindow win))
		{
			return;
		}
		win.HideUI.selectedIndex = 1;
		win.Progress.selectedIndex = 1;
		if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500, base.Hide)))
		{
			_targetItem = (((int)win.btn_Divination_1.data == _divinationId) ? win.btn_Divination_1 : win.btn_Divination_2);
			_disappearItem = (((int)win.btn_Divination_1.data != _divinationId) ? win.btn_Divination_1 : win.btn_Divination_2);
			if (StaticConfigure.Divination.TargetDict.TryGetValue(_divinationTarget, out var value))
			{
				_targetItem.com_Target.loader_FrontCard.url = value.Image;
				_targetItem.com_Target.txt_Name.text = value.NameID.GetLocal(UIStringType.Divination);
				_targetItem.com_Target.txt_CardIndex.text = value.CardNumb.GetLocal(UIStringType.Divination);
			}
			DisappearDivinationCard();
			if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000, base.Hide)))
			{
				Turn();
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2300);
				HideImmediately();
			}
		}
	}

	private async void DisappearDivinationCard()
	{
		if (StaticConfigure.Effect.InfoDict.TryGetValue(23, out var value))
		{
			await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, _disappearItem.effectOutline, 100f);
		}
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(400);
		_disappearItem.visible = false;
	}

	public void Turn(System.Action _onComplete = null)
	{
		if (!GTween.IsTweening(_targetItem))
		{
			_targetItem.rotationY = 0f;
			GTween.To(0f, 180f, 0.8f).SetTarget(_targetItem).SetEase(EaseType.QuadOut)
				.OnUpdate(TurnInTween)
				.OnComplete((GTweenCallback)delegate
				{
					_onComplete?.Invoke();
				});
		}
	}

	private void TurnInTween(GTweener tweener)
	{
		float num = tweener.value.x;
		if (num > 90f)
		{
			_targetItem.rotationY = -180f + num;
			_targetItem.stage.selectedIndex = 1;
		}
		else
		{
			_targetItem.rotationY = num;
			_targetItem.stage.selectedIndex = 0;
		}
	}

	private void HideDivinationWin()
	{
		if (base.contentPane is UILandDivinationWindow uILandDivinationWindow)
		{
			divinationSn = 0L;
			uILandDivinationWindow.Progress.selectedIndex = 0;
			uILandDivinationWindow.HideUI.onChanged.Remove(OnHideControllerChange);
		}
	}
}
