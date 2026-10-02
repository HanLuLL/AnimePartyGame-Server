using System.Collections.Generic;
using Core.Net;
using Core.Tutorial.Tools;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core.Tutorial;

public class TutorialBoardCharacterManager
{
	private readonly Dictionary<int, TutorialBaseMonster> _monsters = new Dictionary<int, TutorialBaseMonster>();

	public void Initialize()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return;
		}
		List<int> currentConfigMonsterIds = curRoomInfo.GetCurrentConfigMonsterIds();
		if (currentConfigMonsterIds == null || currentConfigMonsterIds.Count <= 0)
		{
			return;
		}
		foreach (int item in currentConfigMonsterIds)
		{
			TutorialBaseMonster classInstance = Core.Tutorial.Tools.ReflectionHelper.GetClassInstance<TutorialBaseMonster>($"Core.Tutorial.TutorialMonster_{item}");
			if (classInstance != null)
			{
				_monsters.Add(item, classInstance);
			}
		}
	}

	public void Dispose()
	{
	}

	public TutorialBaseMonster GetMonsterById(int monsterId)
	{
		return _monsters.GetValueOrDefault(monsterId);
	}

	public void DealThrowDice(long playerId)
	{
		ActionLogic action = SimpleSingletonProvider<GameLogicManager>.inst.action;
		action.signal.notifyAllPlayer.Dispatch(playerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			action.throwDiceSn = UIDGenerator.NextUID();
			action.playerAction = PlayerActionEnum.MOVE;
		}
		action.signal.dealThrowDice.Dispatch(playerId);
	}

	public void DealUseEffectCard(long playerId, List<int> cardIds)
	{
		ActionLogic action = SimpleSingletonProvider<GameLogicManager>.inst.action;
		action.signal.notifyAllPlayer.Dispatch(playerId);
		action.UsableCards = null;
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
		{
			action.CardSN = UIDGenerator.NextUID();
			action.UsableCards = new RepeatedField<int> { cardIds };
			action.playerAction = PlayerActionEnum.CARD;
		}
		action.signal.dealEffectCard.Dispatch(playerId);
	}

	public HeroAttrEffect GetHpUpdate(long playerId, int changeHp, bool extraDamage = true)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (changeHp < 0 && extraDamage)
		{
			changeHp += playerDataById.player.Property.OnHitExtraDamage;
		}
		int value = playerDataById.player.Property.HP.Value;
		int maxHP = playerDataById.player.Property.maxHP;
		int num = Mathf.Clamp(0, value + changeHp, maxHP);
		int realChangeHp = num - value;
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Hp = new HeroHpChangeS2C
			{
				PlayerId = playerId,
				ChangeHp = changeHp,
				OriHp = value,
				CurrHp = num,
				RealChangeHp = realChangeHp,
				MaxHp = maxHP
			}
		};
	}

	public HeroAttrEffect GetGoldUpdate(long playerId, int changeGold)
	{
		int value = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).Property.gold.Value;
		int currGold = Mathf.Max(value + changeGold, 0);
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Gold = new HeroGoldChangeS2C
			{
				PlayerId = playerId,
				ChangeGold = changeGold,
				OriGold = value,
				CurrGold = currGold
			}
		};
	}

	public HeroAttrEffect GetCureUpdate(long playerId, int changeCure)
	{
		int value = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).Property.CureCount.property.Value;
		int currNum = Mathf.Max(value + changeCure, 0);
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			CureNum = new HeroCureNumChangeS2C
			{
				PlayerId = playerId,
				ChangeNum = changeCure,
				OriNum = value,
				CurrNum = currNum
			}
		};
	}

	public HeroAttrEffect GetCardUpdate(long playerId, List<CardInfo> cardIds)
	{
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Card = new HeroCardChangeS2C
			{
				PlayerId = playerId,
				Cards = { (IEnumerable<CardInfo>)cardIds }
			}
		};
	}

	public HeroAttrEffect GetLVUpdate(long playerId, int _Lv)
	{
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Lv = new HeroUpLvS2C
			{
				PlayerId = playerId,
				CurrLv = _Lv
			}
		};
	}

	public HeroAttrEffect GetAtkUpdate(long playerId, int changeAtk)
	{
		int value = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).Property.ATK.Value;
		int currAtk = Mathf.Max(0, value + changeAtk);
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Atk = new HeroAtkChangeS2C
			{
				PlayerId = playerId,
				CurrAtk = currAtk
			}
		};
	}

	public HeroAttrEffect GetDefUpdate(long playerId, int changeDef)
	{
		int value = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).Property.DEF.Value;
		int currDef = Mathf.Max(0, value + changeDef);
		return new HeroAttrEffect
		{
			PlayerId = playerId,
			Def = new HeroDefChangeS2C
			{
				PlayerId = playerId,
				CurrDef = currDef
			}
		};
	}

	public async UniTask CreateUpdateAttrData(UpdateHeroAttrS2C attrS2C)
	{
		await MonoSingletonProvider<NetManager>.inst.RPC.UpdateHeroAttrS2C.OnUpdateHeroAttrS2CServerCallBackAsync(attrS2C, 0, isDispatch: true);
		foreach (HeroAttrEffect heroAttrEffect in attrS2C.EffectDatas)
		{
			if (heroAttrEffect.Hp != null)
			{
				await OnHPChange(heroAttrEffect);
			}
			if (heroAttrEffect.Lv != null)
			{
				await OnLevelChange(heroAttrEffect);
			}
		}
	}

	private static async UniTask OnLevelChange(HeroAttrEffect heroAttrEffect)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(heroAttrEffect.PlayerId);
		if (playerDataById != null && playerDataById.characterType == CharacterType.Hero)
		{
			List<int> lVRewardLv = TutorialGame.GetSystem<TutorialBoardManager>().relicManager.GetLVRewardLv(heroAttrEffect.Lv.CurrLv);
			await TutorialGame.GetSystem<TutorialBoardManager>().relicManager.DealSelectRelic(playerDataById.player.Id, lVRewardLv);
		}
	}

	private async UniTask OnHPChange(HeroAttrEffect heroAttrEffect)
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(heroAttrEffect.PlayerId);
		int curHp = playerData.Property.HP.Value;
		if (curHp <= 0)
		{
			await TutorialGame.GetSystem<TutorialBoardManager>().DispatchDeadEvent(playerData.player.Id);
		}
		HealthState healthState = playerData.Property.HealthState;
		int maxHP = playerData.Property.maxHP;
		HealthState healthState2 = CalcHealthState(curHp, maxHP);
		if (healthState != healthState2)
		{
			playerData.Property.OnHealthStateChange(healthState2);
			await TutorialGame.GetSystem<TutorialBoardManager>().buffManager.OnHealthStateChange(playerData.player.Id);
		}
	}

	private HealthState CalcHealthState(int hp, int maxHp)
	{
		if (hp <= 0)
		{
			return HealthState.Dead;
		}
		if (hp >= maxHp)
		{
			return HealthState.Full;
		}
		return HealthState.Critical;
	}

	public async UniTask<bool> PlayerStartAction(BattlePlayerData player)
	{
		if (player.player.characterType == CharacterType.Monster)
		{
			return player.player.Property.HP.Value > 0;
		}
		await MonoSingletonProvider<NetManager>.inst.RPC.ActionStartNotifyS2C.OnActionStartNotifyS2CServerCallBackAsync(new ActionStartNotifyS2C
		{
			PlayerId = player.player.Id,
			IsDie = true
		}, 0, isDispatch: true);
		player.Property.cardUseTimes.JustSetValue(0);
		if (player.Property.HP.Value == 0)
		{
			int maxHP = player.Property.maxHP;
			await CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				Cause = new CauseOrigin(),
				PlayerId = player.player.Id,
				EffectDatas = { GetHpUpdate(player.player.Id, maxHP) }
			});
			return false;
		}
		return true;
	}

	public async UniTask PlayActionHandler(BattlePlayerData player)
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial mapGimmickManager_Tutorial)
		{
			await mapGimmickManager_Tutorial.StartRound(player.player.Id);
		}
	}

	public async UniTask PlayStopHandler(BattlePlayerData player)
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial mapGimmickManager_Tutorial)
		{
			await mapGimmickManager_Tutorial.StopRound(player.player.Id);
		}
	}

	public async UniTask OnPKEnd(long actionPlayerId)
	{
		await UniTask.CompletedTask;
	}
}
