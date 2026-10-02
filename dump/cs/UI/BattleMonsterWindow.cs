using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Camera;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using party.model;

namespace UI;

public class BattleMonsterWindow : BaseWindow
{
	private const int BuffPreviewMaxCount = 3;

	protected readonly RepeatedField<UIButton_MonsterItem> selectedButtons = new RepeatedField<UIButton_MonsterItem>();

	protected BattleMonsterWindow(UIWindowType type)
		: base(type)
	{
	}

	protected async UniTask WaitInitialized()
	{
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	private async void SwitchCamera(Character _character)
	{
		if (_character != null)
		{
			await _character.SwitchCamera();
		}
	}

	protected virtual void onScrollItem()
	{
	}

	private void RefreshMonsterItem(UIButton_MonsterItem btn, BattlePlayerData battlePlayerData, bool showDecisionInfo)
	{
		btn.status.selectedIndex = 0;
		if (battlePlayerData?.CharacterInst == null || battlePlayerData.Property == null)
		{
			return;
		}
		btn.txt_Name.SetVar("name", CharacterHandle.GetCharacterName(battlePlayerData.player.Hero.HeroId)).FlushVars();
		btn.txt_Ordin.text = battlePlayerData.player.Hero.MonsterIndex.ToString();
		btn.txt_curLife.text = battlePlayerData.Property.HP.Value.ToString();
		btn.txt_maxLife.SetVar("maxLife", battlePlayerData.Property.maxHP.ToString()).FlushVars();
		btn.loader_Icon.url = battlePlayerData.player.standingPainting.ProfilePhoto;
		btn.txt_Ordin.visible = battlePlayerData.player.characterConfig.MonsterType != MonsterType.Boss;
		GTextField txt_maxLife = btn.txt_maxLife;
		bool flag = (btn.txt_curLife.visible = true);
		txt_maxLife.visible = flag;
		Controller monsterType = btn.monsterType;
		int selectedIndex = (showDecisionInfo ? (battlePlayerData.player.characterConfig.MonsterType switch
		{
			MonsterType.Boss => 1, 
			MonsterType.Elite => 2, 
			_ => 0, 
		}) : 0);
		monsterType.selectedIndex = selectedIndex;
		btn.showCounter.selectedIndex = ((showDecisionInfo && battlePlayerData.Property.Counter.Value) ? 1 : 0);
		btn.attr.visible = showDecisionInfo;
		if (!showDecisionInfo)
		{
			btn.list_buff.numItems = 0;
			btn.list_buff.visible = false;
			btn.omit.selectedIndex = 0;
			btn.btn_showBuff.visible = false;
			btn.btn_showBuff.touchable = false;
			return;
		}
		btn.txt_ATK.text = battlePlayerData.Property.ATK.Value.ToString();
		btn.txt_DEF.text = battlePlayerData.Property.DEF.Value.ToString();
		bool flag3 = RefreshBuffList(btn, battlePlayerData) > 0;
		btn.btn_showBuff.visible = flag3;
		btn.btn_showBuff.touchable = flag3;
		if (flag3)
		{
			btn.btn_showBuff.onClick.Set(delegate(EventContext context)
			{
				OpenBuffInfo(context, btn);
			});
		}
	}

	private int RefreshBuffList(UIButton_MonsterItem btn, BattlePlayerData battlePlayerData)
	{
		(List<Buff>, List<PropertyData<int>>) showBuffs = battlePlayerData.buffContainer.GetShowBuffs(battlePlayerData);
		List<Buff> showBuffs2 = showBuffs.Item1;
		List<PropertyData<int>> propertyBuffList = showBuffs.Item2;
		int num = showBuffs2.Count + propertyBuffList.Count;
		btn.list_buff.touchable = false;
		btn.list_buff.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIButton_Buff uIButton_Buff)
			{
				uIButton_Buff.showframe.selectedIndex = 1;
				if (index < propertyBuffList.Count)
				{
					uIButton_Buff.RefreshProperty(propertyBuffList[index], dynamic: false);
				}
				else
				{
					uIButton_Buff.RefreshBuff(showBuffs2[index - propertyBuffList.Count]);
				}
			}
		};
		btn.list_buff.visible = num > 0;
		btn.list_buff.numItems = Math.Min(num, 3);
		btn.omit.selectedIndex = ((num > 3) ? 1 : 0);
		return num;
	}

	private void OpenBuffInfo(EventContext context, UIButton_MonsterItem btn)
	{
		context.StopPropagation();
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById((long)btn.data);
		if (playerDataById != null)
		{
			var (list, list2) = playerDataById.buffContainer.GetShowBuffs(playerDataById);
			if (list.Count + list2.Count != 0)
			{
				SimpleSingletonProvider<UIManager>.inst.battlePlayerInfo.RefreshMonsterInfo(list, list2, btn.btn_showBuff).Forget();
			}
		}
	}

	protected void RefreshMonster(GList _list, RepeatedField<long> _playerIds, int targetCount, bool showDecisionInfo = false)
	{
		selectedButtons.Clear();
		BattleLogic battle = SimpleSingletonProvider<GameLogicManager>.inst.battle;
		_list.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIButton_MonsterItem uIButton_MonsterItem)
			{
				BattlePlayerData monsterTarget = battle.GetPlayerDataById(_playerIds[index]);
				uIButton_MonsterItem.data = _playerIds[index];
				RefreshMonsterItem(uIButton_MonsterItem, monsterTarget, showDecisionInfo);
				uIButton_MonsterItem.touchable = monsterTarget?.CharacterInst != null && monsterTarget.Property != null;
				uIButton_MonsterItem.onClick.Set(delegate(EventContext content)
				{
					if (targetCount != 0)
					{
						_list.touchable = false;
						ChooseTarget(content, targetCount);
						SwitchCamera(monsterTarget?.CharacterInst);
						_list.touchable = true;
					}
				});
			}
		};
		_list.numItems = _playerIds.Count;
		_list.touchable = targetCount > 0;
		onScrollItem();
	}

	private void ChooseTarget(EventContext context, int targetCount)
	{
		if (!(context.sender is UIButton_MonsterItem uIButton_MonsterItem))
		{
			return;
		}
		uIButton_MonsterItem.onClick.Retain();
		if (selectedButtons.Contains(uIButton_MonsterItem))
		{
			uIButton_MonsterItem.status.selectedIndex = 0;
			selectedButtons.Remove(uIButton_MonsterItem);
		}
		else
		{
			uIButton_MonsterItem.status.selectedIndex = 1;
			if (selectedButtons.Count < targetCount)
			{
				selectedButtons.Add(uIButton_MonsterItem);
			}
			else
			{
				UIButton_MonsterItem uIButton_MonsterItem2 = selectedButtons.FirstOrDefault();
				if (uIButton_MonsterItem2 != null)
				{
					uIButton_MonsterItem2.status.selectedIndex = 0;
					selectedButtons.Remove(uIButton_MonsterItem2);
					selectedButtons.Add(uIButton_MonsterItem);
				}
			}
		}
		uIButton_MonsterItem.onClick.Release();
	}

	private void RefreshSummonItem(UIButton_MonsterItem btn, LandBuffData buffData)
	{
		btn.status.selectedIndex = 0;
		btn.showCounter.selectedIndex = 0;
		btn.monsterType.selectedIndex = 0;
		btn.attr.visible = false;
		btn.list_buff.numItems = 0;
		btn.list_buff.visible = false;
		btn.omit.selectedIndex = 0;
		btn.btn_showBuff.visible = false;
		btn.btn_showBuff.touchable = false;
		Buff buff = buffData?.buffData;
		if (buff?.Source != null && StaticConfigure.Summon.InfoDict.TryGetValue(buff.Source.Id, out var value))
		{
			btn.txt_Name.SetVar("name", value.NameID.GetLocal(UIStringType.Summon)).FlushVars();
			btn.loader_Icon.url = value.SummonProfilePhoto;
			btn.txt_Ordin.text = buff.BuffIndex.ToString();
			btn.txt_Ordin.visible = true;
			GTextField txt_maxLife = btn.txt_maxLife;
			bool flag = (btn.txt_curLife.visible = false);
			txt_maxLife.visible = flag;
		}
	}

	protected void RefreshSummon(GList _list, int summonId, int targetCount)
	{
		List<LandBuffData> summons = SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.GetSummonById(summonId);
		if (summons == null)
		{
			return;
		}
		selectedButtons.Clear();
		_list.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIButton_MonsterItem uIButton_MonsterItem)
			{
				uIButton_MonsterItem.data = summons[index].UniqueId;
				RefreshSummonItem(uIButton_MonsterItem, summons[index]);
				uIButton_MonsterItem.onClick.Set(delegate(EventContext content)
				{
					if (targetCount != 0)
					{
						_list.touchable = false;
						ChooseTarget(content, targetCount);
						UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(summons[index].LandId);
						SimpleSingletonProvider<CameraManager>.inst.EnableShowCamera(landById.transform.position);
						_list.touchable = true;
					}
				});
			}
		};
		_list.numItems = summons.Count;
		_list.touchable = targetCount > 0;
		onScrollItem();
	}
}
