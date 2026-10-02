using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class LandPursuitWindow : BaseWindow
{
	private BattlePlayerData playerData;

	private long pursuitSn;

	private readonly List<long> targetPlayerIds = new List<long>();

	private UILandPursuit_Button_RoleHeadshot _selectedTargetItem;

	public LandPursuitWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandPursuitWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<LandPursuitWindow> ShowLand()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		return this;
	}

	private void OnHideControllerChange()
	{
		if (base.contentPane is UILandPursuitWindow uILandPursuitWindow)
		{
			base.BgLoader.visible = uILandPursuitWindow.Hide.selectedIndex == 0;
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UILandPursuitWindow uILandPursuitWindow)
		{
			uILandPursuitWindow.cut_in.Play();
			uILandPursuitWindow.txt_HideTip.text = 11023.GetLocal(UIStringType.Message);
			uILandPursuitWindow.Hide.selectedIndex = 0;
			uILandPursuitWindow.Hide.onChanged.Add(OnHideControllerChange);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: false, UIPanelType.BattlePlayer);
		if (base.contentPane is UILandPursuitWindow uILandPursuitWindow)
		{
			uILandPursuitWindow.btn_Pursuit.onClick.Remove(EnablePursuitChasePlayer);
			uILandPursuitWindow.btn_Stay.onClick.Remove(ClosePursuit);
			uILandPursuitWindow.list_Players.onClickItem.Remove(OnClickTargetListItem);
			uILandPursuitWindow.Hide.onChanged.Remove(OnHideControllerChange);
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public async void DealLand_Pursuit(Action _action)
	{
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			if (playerData.CharacterInst != null)
			{
				await playerData.CharacterInst.SwitchCamera();
			}
			await ShowLand();
			SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: true, UIPanelType.BattlePlayer);
			RefreshPursuitData(_action.Sn);
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11000);
		}
	}

	private void RefreshPursuitData(long _Sn)
	{
		if (base.contentPane is UILandPursuitWindow uILandPursuitWindow)
		{
			pursuitSn = _Sn;
			_selectedTargetItem = null;
			InitAvailablePlayer();
			uILandPursuitWindow.list_Players.selectedIndex = -1;
			uILandPursuitWindow.dialogType.selectedIndex = UnityEngine.Random.Range(0, 3);
			uILandPursuitWindow.list_Players.itemRenderer = RendererTargetPlayers;
			uILandPursuitWindow.list_Players.numItems = targetPlayerIds.Count;
			uILandPursuitWindow.list_Players.onClickItem.Add(OnClickTargetListItem);
			uILandPursuitWindow.btn_Pursuit.onClick.Add(EnablePursuitChasePlayer);
			uILandPursuitWindow.btn_Stay.onClick.Add(ClosePursuit);
			uILandPursuitWindow.btn_Pursuit.touchable = false;
			uILandPursuitWindow.btn_Pursuit.grayed = true;
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id))
			{
				OperationTimer.ActionDownTime(pursuitSn, 5033, ClosePursuit);
			}
		}
	}

	private async void RendererTargetPlayers(int index, GObject item)
	{
		if (!(item is UILandPursuit_Button_RoleHeadshot _item))
		{
			return;
		}
		long num = targetPlayerIds[index];
		if (num > 0)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(num);
			((UICom_PlayerInfo)_item.com_playerInfo).RefreshData(playerDataById);
			_item.com_playerInfo.visible = true;
			if (playerDataById.CharacterInst.standLand.LandType != LandType.Hospital && playerDataById.Property.HP.Value != 0)
			{
				_item.grayed = false;
				_item.touchable = true;
				if (StaticConfigure.Effect.InfoDict.TryGetValue(28, out var value))
				{
					await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, _item.effect, 17f);
				}
				_item.effect.visible = false;
			}
			else
			{
				_item.grayed = true;
				_item.touchable = false;
			}
		}
		else
		{
			_item.com_playerInfo.visible = false;
			_item.grayed = true;
			_item.touchable = false;
		}
		_item.data = index;
	}

	private void InitAvailablePlayer()
	{
		targetPlayerIds.Clear();
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (playerData.characterType == CharacterType.Hero && this.playerData.player.Id != playerData.player.Id && this.playerData.player.TeamId != playerData.player.TeamId && !playerData.Property.NotSelect.Value)
			{
				targetPlayerIds.Add(playerData.player.Id);
			}
		}
		if (targetPlayerIds.Count < 3)
		{
			for (int i = 0; i < 3 - targetPlayerIds.Count; i++)
			{
				targetPlayerIds.Add(-1L);
			}
		}
	}

	private void OnClickTargetListItem(EventContext context)
	{
		if (!(context.data is UILandPursuit_Button_RoleHeadshot uILandPursuit_Button_RoleHeadshot) || !(base.contentPane is UILandPursuitWindow uILandPursuitWindow))
		{
			return;
		}
		int index = (int)uILandPursuit_Button_RoleHeadshot.data;
		if (targetPlayerIds[index] > 0)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(targetPlayerIds[index]).Property.HP.Value > 0)
			{
				uILandPursuitWindow.btn_Pursuit.touchable = true;
				uILandPursuitWindow.btn_Pursuit.grayed = false;
			}
			else
			{
				uILandPursuitWindow.btn_Pursuit.touchable = false;
				uILandPursuitWindow.btn_Pursuit.grayed = true;
			}
			uILandPursuitWindow.dialogType.selectedIndex = 3;
		}
		if (_selectedTargetItem == null || _selectedTargetItem != uILandPursuit_Button_RoleHeadshot)
		{
			if (_selectedTargetItem != null)
			{
				_selectedTargetItem.effect.visible = false;
			}
			uILandPursuit_Button_RoleHeadshot.effect.visible = true;
			_selectedTargetItem = uILandPursuit_Button_RoleHeadshot;
		}
	}

	private void EnablePursuitChasePlayer()
	{
		GComponent gComponent = base.contentPane;
		UILandPursuitWindow win = gComponent as UILandPursuitWindow;
		if (win == null || pursuitSn == 0L || win.list_Players.selectedIndex == -1)
		{
			return;
		}
		long num = targetPlayerIds[win.list_Players.selectedIndex];
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(num);
		if (playerDataById == null || playerDataById.Property.HP.Value == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10012);
			return;
		}
		win.dialogType.selectedIndex = 4;
		win.btn_Pursuit.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.land.RequsetPursuitC2S(pursuitSn, num).OnFinishedOnly.AddOnce(delegate
		{
			pursuitSn = 0L;
			win.btn_Pursuit.onClick.Release();
		});
	}

	private void ClosePursuit()
	{
		GComponent gComponent = base.contentPane;
		UILandPursuitWindow win = gComponent as UILandPursuitWindow;
		if (win != null && pursuitSn != 0L)
		{
			win.btn_Stay.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequsetPursuitC2S(pursuitSn, 0L).OnFinishedOnly.AddOnce(delegate
			{
				pursuitSn = 0L;
				win.btn_Stay.onClick.Release();
			});
		}
	}
}
