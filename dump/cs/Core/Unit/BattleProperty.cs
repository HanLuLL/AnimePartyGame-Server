using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core.Unit;

public class BattleProperty
{
	private readonly long playerId;

	public readonly List<PropertyData<int>> propertyBuffDataList;

	public readonly ReadOnlyReactiveProperty<int> level;

	public readonly ReadOnlyReactiveProperty<int> gold;

	public int maxHP;

	public readonly ReadOnlyReactiveProperty<int> HP;

	public readonly ReadOnlyReactiveProperty<int> ChangeHP;

	public readonly ReadOnlyReactiveProperty<int> ATK;

	public readonly ReadOnlyReactiveProperty<int> DEF;

	public readonly ReadOnlyReactiveProperty<bool> Online;

	public readonly ReadOnlyReactiveProperty<int> Score;

	public readonly ReadOnlyReactiveProperty<int> CardMaxVailUseCount;

	public ReactiveProperty<int> cardUseTimes;

	public Dictionary<int, int> cardUseData;

	public readonly ReadOnlyReactiveProperty<int> activeSkillCD;

	public readonly ReadOnlyReactiveProperty<int> reRelicCount;

	public readonly PropertyData<int> CureCount;

	public readonly PropertyData<int> SalaryCount;

	public readonly PropertyData<int> MarkCount;

	public readonly PropertyData<int> EnergyNum;

	public readonly PropertyData<int> CounterCount;

	public readonly PropertyData<int> ModifyNum;

	public readonly PropertyData<int> UniqueNum;

	public readonly PropertyData<int> CrimeNum;

	public readonly ReadOnlyReactiveProperty<int> CardAddDistance;

	public readonly ReadOnlyReactiveProperty<int> CardAddAttack;

	public readonly ReadOnlyReactiveProperty<bool> Counter;

	public readonly ReadOnlyReactiveProperty<bool> NotSelect;

	public readonly ReadOnlyReactiveProperty<int> ExtraAddMovePoint;

	public readonly ReadOnlyReactiveProperty<int> ExtraMinusMovePoint;

	private int _onHitExtraDamage;

	private HealthState healthState = HealthState.Full;

	public HealthState PreHealthState = HealthState.Full;

	private BattlePlayerData _PlayerData => SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);

	public int ExtraMovePoint => ExtraAddMovePoint.Value - ExtraMinusMovePoint.Value;

	public int OnHitExtraDamage => _onHitExtraDamage;

	public HealthState HealthState => healthState;

	public BattleProperty(CharacterType characterType, Player player)
	{
		playerId = player.Id;
		level = new ReadOnlyReactiveProperty<int>(player.Hero.Lv);
		gold = new ReadOnlyReactiveProperty<int>(player.Hero.Gold);
		maxHP = player.Hero.MaxHp;
		HP = new ReadOnlyReactiveProperty<int>(player.Hero.Hp);
		ChangeHP = new ReadOnlyReactiveProperty<int>(0);
		ATK = new ReadOnlyReactiveProperty<int>(player.Hero.Attack);
		DEF = new ReadOnlyReactiveProperty<int>(player.Hero.Defense);
		Online = new ReadOnlyReactiveProperty<bool>(!player.OffLine);
		CardMaxVailUseCount = new ReadOnlyReactiveProperty<int>(player.Hero.UseCardMaxNum);
		cardUseTimes = new ReactiveProperty<int>(Mathf.Max(player.Hero.UseCardMaxNum - player.Hero.UserCardNum));
		cardUseData = new Dictionary<int, int>(0);
		activeSkillCD = new ReadOnlyReactiveProperty<int>(0);
		reRelicCount = new ReadOnlyReactiveProperty<int>(player.Hero.ReRollNum);
		Score = new ReadOnlyReactiveProperty<int>(player.Hero.SpecialScore);
		propertyBuffDataList = new List<PropertyData<int>>();
		CureCount = RegisterPropertyBuff(player.Hero.CureNum, 10004);
		SalaryCount = RegisterPropertyBuff(player.Hero.SalaryNum, 10005);
		MarkCount = RegisterPropertyBuff(player.Hero.MarkNum, 10006);
		CounterCount = RegisterPropertyBuff(player.Hero.CounterNum, 10008);
		ModifyNum = RegisterPropertyBuff(player.Hero.ModityNum, 10009);
		UniqueNum = RegisterPropertyBuff(player.Hero.UniqueNum, 10012);
		CrimeNum = RegisterPropertyBuff(player.Hero.CrimeNum, 10013);
		EnergyNum = RegisterPropertyBuff(player.Hero.EnergyNum, 10014);
		CardAddDistance = new ReadOnlyReactiveProperty<int>(player.Hero.CardDistance);
		CardAddAttack = new ReadOnlyReactiveProperty<int>(player.Hero.CardAtk);
		Counter = new ReadOnlyReactiveProperty<bool>(player.Hero.CanCounter);
		NotSelect = new ReadOnlyReactiveProperty<bool>(player.Hero.NotSelect);
		ExtraAddMovePoint = new ReadOnlyReactiveProperty<int>(0);
		ExtraMinusMovePoint = new ReadOnlyReactiveProperty<int>(0);
		UpdateBattleData(characterType, player);
	}

	private PropertyData<int> RegisterPropertyBuff(int value, int buffId)
	{
		PropertyData<int> propertyData = new PropertyData<int>(value, buffId);
		propertyBuffDataList.Add(propertyData);
		return propertyData;
	}

	private void UpdateBattleData(CharacterType characterType, Player player)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		foreach (KeyValuePair<long, AdditionAttribute> additionAttr in player.Hero.AdditionAttrs)
		{
			num += additionAttr.Value.Attack;
			num2 += additionAttr.Value.Defense;
			num3 += additionAttr.Value.CardDistance;
			num4 += additionAttr.Value.CardAtk;
		}
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (characterType == CharacterType.Monster && curRoomInfo != null)
		{
			num += curRoomInfo.info.MonsterAtkAdd;
			num2 += curRoomInfo.info.MonsterDefAdd;
		}
		ATK.SetValue(Mathf.Max(0, player.Hero.Attack + num));
		DEF.SetValue(Mathf.Max(0, player.Hero.Defense + num2));
		CardAddDistance.SetValue(player.Hero.CardDistance + num3);
		CardAddAttack.SetValue(player.Hero.CardAtk + num4);
		level.SetValue(player.Hero.Lv);
		gold.SetValue(player.Hero.Gold);
		HP.SetValue(player.Hero.Hp);
		Online.SetValue(!player.OffLine);
		Score.SetValue(player.Hero.SpecialScore);
	}

	protected virtual void RemoveListeners()
	{
		level?.RemoveAllListeners();
		gold?.RemoveAllListeners();
		HP?.RemoveAllListeners();
		ChangeHP?.RemoveAllListeners();
		ATK?.RemoveAllListeners();
		DEF?.RemoveAllListeners();
		Online?.RemoveAllListeners();
		CardMaxVailUseCount?.RemoveAllListeners();
		cardUseTimes?.RemoveAllListeners();
		activeSkillCD?.RemoveAllListeners();
		reRelicCount?.RemoveAllListeners();
		Score?.RemoveAllListeners();
		CardAddDistance?.RemoveAllListeners();
		foreach (PropertyData<int> propertyBuffData in propertyBuffDataList)
		{
			propertyBuffData.ClearProperty();
		}
	}

	public void Dispose()
	{
		RemoveListeners();
	}

	public async UniTask OnLevelChanged(int _newLevel)
	{
		if (level.Value == _newLevel)
		{
			return;
		}
		if (_PlayerData == null || _PlayerData.CharacterInst == null)
		{
			level.JustSetValue(_newLevel);
		}
		else if (await _PlayerData.CharacterInst.SwitchCamera())
		{
			level.SetValue(_newLevel);
			await SimpleSingletonProvider<UIManager>.inst.upgradeWindow.ShowPVPTip(_PlayerData.player.Id);
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom && !SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.IsPVE())
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(_PlayerData.player.Id, 4, "升级表演");
				SimpleSingletonProvider<GameLogicManager>.inst.performTriggerLogic.signal.heroStarUpSignal.Dispatch(_PlayerData.player.Id, _newLevel);
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_PlayerData.player.Id))
			{
				SimpleSingletonProvider<GameLogicManager>.inst.campaign.campaignData?.TriggerStarLevelUp();
			}
		}
	}

	public async UniTask OnGoldChanged(HeroGoldChangeS2C goldChange)
	{
		int curGold = gold.Value;
		int curChange = curGold + goldChange.ChangeGold;
		gold.SetValue(curChange);
		if (_PlayerData != null && _PlayerData.CharacterInst != null && !SimpleSingletonProvider<UIManager>.inst.landEvent.isShowing && !SimpleSingletonProvider<UIManager>.inst.landShop.isShowing && !SimpleSingletonProvider<UIManager>.inst.landLottery.isShowing && !SimpleSingletonProvider<UIManager>.inst.landGamble.isShowing)
		{
			Transform transform = _PlayerData.CharacterInst.characterObject.transform;
			await SimpleSingletonProvider<EffectManager>.inst.PlayGoldShow(goldChange.CurrGold - curGold, transform.position, transform.localRotation);
			if ((object)_PlayerData.CharacterInst != null)
			{
				_PlayerData.CharacterInst.signal.attrChange.Dispatch((curGold, curChange, goldChange.ChangeGold, 4), "");
			}
		}
	}

	public async UniTask OnLifeChanged(HeroHpChangeS2C HpChange, bool isFight = false)
	{
		int curHp = ((HpChange.RealHp == 0) ? (HP.Value + HpChange.RealChangeHp) : HpChange.RealHp);
		if (_PlayerData == null || _PlayerData.CharacterInst == null)
		{
			maxHP = HpChange.MaxHp;
			HP.JustSetValue(Mathf.Min(Mathf.Max(0, curHp), maxHP));
			return;
		}
		Character _characterInst = _PlayerData.CharacterInst;
		ChangeHP.SetValueAndForceDispath(curHp - HP.Value);
		if ((object)_characterInst != null && (HpChange.RealChangeHp != 0 || !isFight))
		{
			_characterInst.signal.attrChange.Dispatch((HpChange.OriHp, HpChange.CurrHp, HpChange.RealChangeHp, 5), "");
		}
		if (curHp > 0 && HP.Value == 0 && _characterInst != null && _characterInst.showComponent != null)
		{
			await _characterInst.showComponent.UpdateHealthStatus(dead: false);
		}
		maxHP = HpChange.MaxHp;
		HP.SetValueAndForceDispath(Mathf.Min(Mathf.Max(0, curHp), maxHP));
		_PlayerData.CharacterInst.window.Com_AttrInfo?.UpdateHP(HP.Value, maxHP);
		if (HP.Value == 0 && _characterInst != null && _characterInst.showComponent != null)
		{
			await _characterInst.showComponent.UpdateHealthStatus(dead: true);
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.playerHpChange.Dispatch(HpChange, isFight);
	}

	public void OnATKChanged(HeroAtkChangeS2C Atk)
	{
		if (ATK.Value != Atk.CurrAtk)
		{
			if (_PlayerData != null && _PlayerData.CharacterInst != null)
			{
				_PlayerData.CharacterInst.signal.attrChange.Dispatch((ATK.Value, Atk.CurrAtk, Atk.CurrAtk - ATK.Value, 1), "");
				_PlayerData.CharacterInst.window.Com_AttrInfo?.UpdateATK(Atk.CurrAtk);
			}
			ATK.SetValue(Atk.CurrAtk);
		}
	}

	public void OnDEFChanged(HeroDefChangeS2C Def)
	{
		if (DEF.Value != Def.CurrDef)
		{
			if (_PlayerData != null && _PlayerData.CharacterInst != null)
			{
				_PlayerData.CharacterInst.signal.attrChange.Dispatch((DEF.Value, Def.CurrDef, Def.CurrDef - DEF.Value, 2), "");
				_PlayerData.CharacterInst.window.Com_AttrInfo?.UpdateDEF(Def.CurrDef);
			}
			DEF.SetValue(Def.CurrDef);
		}
	}

	public void UpdateSkillCD(int SkillCD)
	{
		activeSkillCD.SetValueAndForceDispath(SkillCD);
	}

	public void OnReRelicCountChanged(HeroReRollChangeS2C ReRoll)
	{
		reRelicCount.SetValue(ReRoll.ReRollNum);
	}

	public void OnScoreChanged(HeroSpecialScoreChangeS2C _Score)
	{
		_ = Score.Value;
		_ = _Score.CurrScore;
		Score.SetValue(_Score.CurrScore);
	}

	public void OnCureCountChanged(HeroCureNumChangeS2C CureNum)
	{
		CureCount.UpdateProperty(CureNum.CurrNum);
	}

	public void OnSalaryCountChanged(HeroSalaryNumChangeS2C SalaryNum)
	{
		SalaryCount.UpdateProperty(SalaryNum.CurrNum);
	}

	public void OnMarkCountChanged(HeroMarkNumChangeS2C MarkNum)
	{
		MarkCount.UpdateProperty(MarkNum.CurrNum);
	}

	public void OnEnergyNumChanged(HeroEnergyNumChangeS2C energyNum)
	{
		EnergyNum.UpdateProperty(energyNum.CurrNum);
	}

	public void OnCounterCountChanged(HeroCounterNumChangeS2C CounterNum)
	{
		CounterCount.UpdateProperty(CounterNum.CurrNum);
	}

	public async UniTask OnModifyNumChanged(HeroModifyNumChangeS2C modifyNum)
	{
		if (modifyNum.IsActionEnd && modifyNum.CurrNum == 0)
		{
			BuffInfoConfigure buffConfigure = ModifyNum.buffId.GetBuffConfigure();
			if (buffConfigure != null)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(playerId, buffConfigure.PerformDestroy, "改造层数清空演出");
			}
		}
		ModifyNum.UpdateProperty(modifyNum.CurrNum);
	}

	public void OnCardCountChanged(HeroUseCardNumChangeS2C CardNum)
	{
		CardMaxVailUseCount.SetValueAndForceDispath(CardNum.UseCardMaxNum);
		cardUseTimes.Value = Mathf.Max(0, CardNum.UseCardMaxNum - CardNum.UseCardNum);
	}

	public void OnCardCountChanged(int MaxVailUseCount)
	{
		CardMaxVailUseCount.SetValueAndForceDispath(MaxVailUseCount);
	}

	public void OnCardDistanceChanged(CardDistanceChangeS2C Distance)
	{
		if (CardAddDistance.Value != Distance.CurrDistance)
		{
			CardAddDistance.SetValue(Distance.CurrDistance);
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetSelfCardList.Dispatch(playerId);
		}
	}

	public void OnCardAtkChanged(CardAtkChangeS2C atk)
	{
		if (CardAddAttack.Value != atk.Atk)
		{
			CardAddAttack.SetValue(atk.Atk);
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetSelfCardList.Dispatch(playerId);
		}
	}

	public void ResetCharacter()
	{
		if (_PlayerData != null && _PlayerData.CharacterInst != null)
		{
			CharacterAnimator characterAnimator = _PlayerData.CharacterInst.characterAnimator;
			bool flag = HP.Value > 0;
			if ((object)characterAnimator != null)
			{
				characterAnimator.grayed = !flag;
				characterAnimator.Die(!flag).Forget();
			}
		}
	}

	public void OnCanCounterChanged(HeroCounterChangeS2C canCounter)
	{
		if (Counter.Value != canCounter.CanCounter)
		{
			Counter.SetValue(canCounter.CanCounter);
		}
	}

	public void OnNotSelectChanged(HeroNotSelectS2C notSelect)
	{
		if (NotSelect.Value != notSelect.NotSelect)
		{
			NotSelect.SetValue(notSelect.NotSelect);
		}
	}

	public void OnChangeExtraMovePoint(int extraAddMovePoint, int extraMinusMovePoint)
	{
		if (ExtraAddMovePoint.Value != extraAddMovePoint)
		{
			ExtraAddMovePoint.SetValue(extraAddMovePoint);
		}
		if (ExtraMinusMovePoint.Value != extraMinusMovePoint)
		{
			ExtraMinusMovePoint.SetValue(extraMinusMovePoint);
		}
		if (_PlayerData != null && _PlayerData.CharacterInst != null)
		{
			_PlayerData.CharacterInst.window.Com_AttrInfo?.UpdateExtraMovePoint(ExtraMovePoint);
		}
	}

	public async UniTask OnUniqueNumChanged(HeroUniqueNumChangeS2C uniqueNum)
	{
		if (uniqueNum.CurrNum == 0)
		{
			BuffInfoConfigure buffConfigure = UniqueNum.buffId.GetBuffConfigure();
			if (buffConfigure != null)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(playerId, buffConfigure.PerformDestroy, "唯一buff层数清空演出");
			}
		}
		UniqueNum.UpdateProperty(uniqueNum.CurrNum);
	}

	public async UniTask OnCrimeNumChanged(HeroCrimeNumChangeS2C crimeNum)
	{
		if (crimeNum.CurrNum == 0)
		{
			BuffInfoConfigure buffConfigure = CrimeNum.buffId.GetBuffConfigure();
			if (buffConfigure != null && buffConfigure.PerformDestroy != 0)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(playerId, buffConfigure.PerformDestroy, "罪证buff层数清空演出");
			}
		}
		CrimeNum.UpdateProperty(crimeNum.CurrNum);
	}

	public void OnHitExtraDamageChange(int changeDamage)
	{
		_onHitExtraDamage = Mathf.Min(0, _onHitExtraDamage + changeDamage);
	}

	public void OnHealthStateChange(HealthState state)
	{
		PreHealthState = healthState;
		healthState = state;
	}
}
