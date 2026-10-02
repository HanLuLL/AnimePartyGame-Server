using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class BattleSelectMonsterWindow : BattleMonsterWindow
{
	public BattleSelectMonsterWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIBattleSelectMonsterWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
	{
		if (!base.isShowing)
		{
			Show();
		}
		await WaitInitialized();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIBattleSelectMonsterWindow uIBattleSelectMonsterWindow)
		{
			uIBattleSelectMonsterWindow.list_Monster.scrollPane.onScroll.Add(onScrollItem);
			uIBattleSelectMonsterWindow.txt_Title.text = 1000007.GetLocal(UIStringType.GUI);
			uIBattleSelectMonsterWindow.txt_HideTip.text = 11023.GetLocal(UIStringType.Message);
			uIBattleSelectMonsterWindow.Hide.selectedIndex = 0;
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIBattleSelectMonsterWindow uIBattleSelectMonsterWindow)
		{
			uIBattleSelectMonsterWindow.list_Monster.scrollPane.onScroll.Remove(onScrollItem);
		}
	}

	protected override void onScrollItem()
	{
		if (base.contentPane is UIBattleSelectMonsterWindow uIBattleSelectMonsterWindow)
		{
			uIBattleSelectMonsterWindow.com_LeftArrow.visible = uIBattleSelectMonsterWindow.list_Monster.numItems > 5 && uIBattleSelectMonsterWindow.list_Monster.scrollPane.percX < 1f;
			uIBattleSelectMonsterWindow.com_RightArrow.visible = uIBattleSelectMonsterWindow.list_Monster.numItems > 5 && uIBattleSelectMonsterWindow.list_Monster.scrollPane.percX > 0f;
		}
	}

	public async UniTask ShowMonsterPursuit(Action action, RepeatedField<long> _monsters)
	{
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIBattleSelectMonsterWindow win = gComponent as UIBattleSelectMonsterWindow;
		if (win == null)
		{
			return;
		}
		RefreshMonster(win.list_Monster, _monsters, 1, showDecisionInfo: true);
		win.btn_Sure.onClick.Release();
		win.btn_Cancel.onClick.Release();
		win.list_Monster.touchable = true;
		win.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			win.btn_Cancel.onClick.Retain();
			win.btn_Sure.onClick.Retain();
			RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
			if (roomInfo != null && roomInfo.MapType == 10)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestMonsterPursuitC2S(action.Sn, 0L);
				win.btn_Cancel.onClick.Release();
				win.btn_Sure.onClick.Release();
			}
			else
			{
				win.list_Monster.touchable = false;
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestMonsterPursuitC2S(action.Sn, 0L).OnFinishedOnly.AddOnce(delegate
				{
					win.list_Monster.touchable = true;
				});
			}
		});
		win.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			if (selectedButtons.Count != 0)
			{
				win.btn_Cancel.onClick.Retain();
				win.btn_Sure.onClick.Retain();
				RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
				if (roomInfo != null && roomInfo.MapType == 10)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestMonsterPursuitC2S(action.Sn, (long)selectedButtons[0].data);
					win.btn_Cancel.onClick.Release();
					win.btn_Sure.onClick.Release();
				}
				else
				{
					win.list_Monster.touchable = false;
					SimpleSingletonProvider<GameLogicManager>.inst.land.RequestMonsterPursuitC2S(action.Sn, (long)selectedButtons[0].data).OnFinishedOnly.AddOnce(delegate
					{
						win.list_Monster.touchable = true;
					});
				}
			}
		});
		OperationTimer.ActionDownTime(action.Sn, 5213, delegate
		{
			win.btn_Cancel.onClick.Call();
		});
	}

	public async void ShowSkillVailMonsterTarget(RepeatedField<long> _playerIds, int _skillId, int targetCount, long _Sn)
	{
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIBattleSelectMonsterWindow win = gComponent as UIBattleSelectMonsterWindow;
		if (win == null)
		{
			return;
		}
		RefreshMonster(win.list_Monster, _playerIds, targetCount, showDecisionInfo: true);
		win.btn_Sure.onClick.Release();
		win.btn_Cancel.onClick.Release();
		win.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			if (selectedButtons.Count != 0 && _Sn != 0L && _skillId != 0)
			{
				win.btn_Sure.onClick.Retain();
				List<long> targetIds = selectedButtons.Select((UIButton_MonsterItem _btn) => (long)_btn.data).ToList();
				BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
				if (selfPlayerData?.CharacterInst == null || selfPlayerData.CharacterInst.skill == null)
				{
					Debug.LogError("主动技能无法获取技能实例, 需要检查");
				}
				else
				{
					selfPlayerData.CharacterInst.skill.RequestReleaseSkillBySelectTarget(_Sn, targetIds);
					Hide();
				}
			}
		});
		win.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			win.btn_Cancel.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetCardList.Dispatch();
			Hide();
		});
	}

	public async void ShowSkillVailSummonTarget(int summonId, int _skillId, int targetCount, long _Sn)
	{
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIBattleSelectMonsterWindow win = gComponent as UIBattleSelectMonsterWindow;
		if (win == null)
		{
			return;
		}
		RefreshSummon(win.list_Monster, summonId, targetCount);
		win.btn_Sure.onClick.Release();
		win.btn_Cancel.onClick.Release();
		win.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			if (selectedButtons.Count != 0 && _Sn != 0L && _skillId != 0)
			{
				win.btn_Sure.onClick.Retain();
				BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
				if (selfPlayerData?.CharacterInst == null || selfPlayerData.CharacterInst.skill == null)
				{
					Debug.LogError("主动技能无法获取技能实例, 需要检查");
				}
				else
				{
					List<long> targetIds = selectedButtons.Select((UIButton_MonsterItem _btn) => (long)_btn.data).ToList();
					selfPlayerData.CharacterInst.skill.RequestReleaseSkillBySelectTarget(_Sn, targetIds);
					Hide();
				}
			}
		});
		win.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			win.btn_Cancel.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetCardList.Dispatch();
			Hide();
		});
	}

	public async void ShowCardVailMonsterTarget(RepeatedField<long> _playerIds, int cardId, int targetCount, long _Sn)
	{
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIBattleSelectMonsterWindow win = gComponent as UIBattleSelectMonsterWindow;
		if (win == null)
		{
			return;
		}
		RefreshMonster(win.list_Monster, _playerIds, targetCount, showDecisionInfo: true);
		win.btn_Sure.onClick.Release();
		win.btn_Cancel.onClick.Release();
		win.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			if (selectedButtons.Count != 0 && _Sn != 0L && cardId != 0)
			{
				win.btn_Sure.onClick.Retain();
				List<long> targetIds = selectedButtons.Select((UIButton_MonsterItem _btn) => (long)_btn.data).ToList();
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseEffectCardC2S(_Sn, cardId, targetIds);
				Hide();
			}
		});
		win.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			win.btn_Cancel.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetCardList.Dispatch();
			Hide();
		});
	}

	public async UniTask ShowCardVailMonsterTargetAfterChooseCard(RepeatedField<long> _playerIds, int chooseCardId, int effectIndex, int targetCount, long _Sn)
	{
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIBattleSelectMonsterWindow win = gComponent as UIBattleSelectMonsterWindow;
		if (win == null)
		{
			return;
		}
		RefreshMonster(win.list_Monster, _playerIds, targetCount, showDecisionInfo: true);
		win.btn_Sure.onClick.Release();
		win.btn_Cancel.onClick.Release();
		win.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			if (selectedButtons.Count != 0 && _Sn != 0L && chooseCardId != 0 && effectIndex != 0)
			{
				win.btn_Sure.onClick.Retain();
				List<long> targetIds = selectedButtons.Select((UIButton_MonsterItem _btn) => (long)_btn.data).ToList();
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseEffectCardC2S(_Sn, chooseCardId, targetIds, null, effectIndex);
				Hide();
			}
		});
		win.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			win.btn_Cancel.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetCardList.Dispatch();
			Hide();
		});
	}
}
