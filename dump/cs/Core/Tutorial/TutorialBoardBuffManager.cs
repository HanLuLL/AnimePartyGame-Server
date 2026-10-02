using System.Collections.Generic;
using System.Linq;
using Core.Tutorial.Buff;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace Core.Tutorial;

public class TutorialBoardBuffManager
{
	public void Initialize()
	{
	}

	public void Dispose()
	{
	}

	public async UniTask OnRoundStart()
	{
		List<BattlePlayerData> player = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		for (int i = 0; i < player.Count; i++)
		{
			if (player[i].Property.HP.Value <= 0)
			{
				continue;
			}
			MapField<long, party.model.Buff> buffs = player[i].player.serverPlayer.Hero.Buffs;
			if (buffs.Count == 0)
			{
				continue;
			}
			List<party.model.Buff> list = buffs.Values.ToList();
			foreach (party.model.Buff item in list)
			{
				item.BuffData.OnRoundStart?.Apply(item);
			}
			await DealBuffRound(player[i].player.Id, list, BuffRoundCountType.Action);
		}
	}

	public async UniTask ActionStart(long playerId)
	{
		MapField<long, party.model.Buff> buffMap = GetBuffMap(playerId);
		if (buffMap == null)
		{
			return;
		}
		List<party.model.Buff> list = buffMap.Values.ToList();
		foreach (party.model.Buff item in list)
		{
			item.BuffData.OnActionStart?.Apply(item);
		}
		await DealBuffRound(playerId, list, BuffRoundCountType.Action);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		int value = playerDataById.Property.CureCount.property.Value;
		if (value > 0)
		{
			HeroAttrEffect hpUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetHpUpdate(playerDataById.player.Id, value);
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				Cause = new CauseOrigin(),
				PlayerId = playerDataById.player.Id,
				EffectDatas = { hpUpdate }
			});
		}
	}

	public async UniTask ActionEnd(long playerId)
	{
		MapField<long, party.model.Buff> buffMap = GetBuffMap(playerId);
		if (buffMap == null)
		{
			return;
		}
		List<party.model.Buff> list = buffMap.Values.ToList();
		foreach (party.model.Buff item in list)
		{
			item.BuffData.OnActionEnd?.Apply(item);
		}
		await DealBuffRound(playerId, list, BuffRoundCountType.CurrentAction);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		int value = playerDataById.Property.CureCount.property.Value;
		if (value > 0)
		{
			int changeCure = -Mathf.CeilToInt((float)value * 0.5f);
			HeroAttrEffect cureUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCureUpdate(playerDataById.player.Id, changeCure);
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				Cause = new CauseOrigin(),
				PlayerId = playerDataById.player.Id,
				EffectDatas = { cureUpdate }
			});
		}
	}

	private async UniTask DealBuffRound(long playerId, List<party.model.Buff> buffMap, BuffRoundCountType round)
	{
		foreach (party.model.Buff item in buffMap)
		{
			if (item.BuffData.Config.BuffRoundCountType == round && item.KeepRound != 0)
			{
				item.KeepRound--;
				if (item.KeepRound <= 0)
				{
					await RemoveBuff(playerId, item);
				}
			}
		}
	}

	private MapField<long, party.model.Buff> GetBuffMap(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById.Property.HP.Value <= 0)
		{
			return null;
		}
		MapField<long, party.model.Buff> buffs = playerDataById.player.serverPlayer.Hero.Buffs;
		if (buffs.Count == 0)
		{
			return null;
		}
		return buffs;
	}

	public async UniTask DealDeadEvent(long player)
	{
		await ClearBuff(player);
	}

	public async UniTask OnPKStart(long attackId, long defenceId)
	{
		await OnPkPlayerStart(attackId, isAtk: true);
		await OnPkPlayerStart(defenceId, isAtk: false);
	}

	private async UniTask OnPkPlayerStart(long playerId, bool isAtk)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById.Property.HP.Value <= 0)
		{
			return;
		}
		MapField<long, party.model.Buff> buffs = playerDataById.player.serverPlayer.Hero.Buffs;
		if (buffs.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<long, party.model.Buff> item in buffs)
		{
			BaseBuffModule baseBuffModule = (isAtk ? item.Value.BuffData.OnPKAttackStart : item.Value.BuffData.OnPKDefendStart);
			if (baseBuffModule != null)
			{
				await baseBuffModule.Apply(item.Value);
			}
		}
	}

	public async UniTask OnPKEnd(long attackId, long defenceId)
	{
		await OnPkPlayerEnd(attackId, isAtk: true);
		await OnPkPlayerEnd(defenceId, isAtk: false);
	}

	private async UniTask OnPkPlayerEnd(long playerId, bool isAtk)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById.Property.HP.Value <= 0)
		{
			return;
		}
		MapField<long, party.model.Buff> buffs = playerDataById.player.serverPlayer.Hero.Buffs;
		if (buffs.Count == 0)
		{
			return;
		}
		List<party.model.Buff> pkBuffs = new List<party.model.Buff>();
		foreach (KeyValuePair<long, party.model.Buff> item in buffs)
		{
			BaseBuffModule baseBuffModule = (isAtk ? item.Value.BuffData.OnPkAttackEnd : item.Value.BuffData.OnPkDefendEnd);
			if (baseBuffModule != null)
			{
				pkBuffs.Add(item.Value);
				await baseBuffModule.Apply(item.Value);
			}
		}
		foreach (party.model.Buff item2 in pkBuffs)
		{
			if (item2.KeepRound < 0)
			{
				await RemoveBuff(playerId, item2);
			}
		}
	}

	public async UniTask OnThrowDice(long playerId)
	{
		MapField<long, party.model.Buff> buffMap = GetBuffMap(playerId);
		if (buffMap == null)
		{
			return;
		}
		foreach (KeyValuePair<long, party.model.Buff> item in buffMap)
		{
			if (item.Value.BuffData.OnThrowDice != null)
			{
				await item.Value.BuffData.OnThrowDice.Apply(item.Value);
			}
		}
	}

	public async UniTask OnHealthStateChange(long playerId)
	{
		MapField<long, party.model.Buff> buffMap = GetBuffMap(playerId);
		if (buffMap == null)
		{
			return;
		}
		foreach (KeyValuePair<long, party.model.Buff> item in buffMap)
		{
			if (item.Value.BuffData.OnHealthStateChange != null)
			{
				await item.Value.BuffData.OnHealthStateChange.Apply(item.Value);
			}
		}
	}

	public async UniTask AddBuff(long playerId, party.model.Buff buff)
	{
		BattlePlayerData curPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		MapField<long, party.model.Buff> buffs = curPlayer.player.serverPlayer.Hero.Buffs;
		party.model.Buff buff2 = null;
		foreach (KeyValuePair<long, party.model.Buff> item in buffs)
		{
			if (item.Value.BuffId == buff.BuffId)
			{
				buff2 = item.Value;
				break;
			}
		}
		if (buff2 != null)
		{
			if (buff.BuffData.Config.BuffConflictManagementType == BuffConflictManagementType.Reject)
			{
				return;
			}
			if (buff.BuffData.Config.BuffConflictManagementType == BuffConflictManagementType.Replace)
			{
				await RemoveBuff(playerId, buff2);
			}
		}
		UpdateHeroAttrS2C attrS2C = new UpdateHeroAttrS2C
		{
			PlayerId = playerId,
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Unknown,
				Id = 0L
			},
			EffectDatas = 
			{
				new HeroAttrEffect
				{
					PlayerId = playerId,
					Buff = new HeroBuffChangeS2C
					{
						PlayerId = playerId,
						Buff = buff,
						Op = HeroBuffChangeS2C.Types.Oper.Insert
					}
				}
			}
		};
		curPlayer.player.serverPlayer.Hero.Buffs.Add(buff.UniqueId, buff);
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(attrS2C);
		if (buff.BuffData.OnCreate != null)
		{
			await buff.BuffData.OnCreate.Apply(buff);
		}
	}

	private async UniTask RemoveBuff(long playerId, party.model.Buff buff)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId).player.serverPlayer.Hero.Buffs.Remove(buff.UniqueId))
		{
			UpdateHeroAttrS2C attrS2C = new UpdateHeroAttrS2C
			{
				PlayerId = playerId,
				Cause = new CauseOrigin
				{
					S = CauseOrigin.Types.source.Unknown,
					Id = 0L
				},
				EffectDatas = 
				{
					new HeroAttrEffect
					{
						PlayerId = playerId,
						Buff = new HeroBuffChangeS2C
						{
							PlayerId = playerId,
							Buff = buff,
							Op = HeroBuffChangeS2C.Types.Oper.Delete
						}
					}
				}
			};
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(attrS2C);
			if (buff.BuffData.OnRemove != null)
			{
				await buff.BuffData.OnRemove.Apply(buff);
			}
		}
		else
		{
			Debug.LogError($"无法删除玩家：{playerId} 身上的buffUID:{buff.UniqueId} buffId:{buff.BuffId}");
		}
	}

	public async UniTask ClearBuff(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById.characterType != CharacterType.Hero)
		{
			return;
		}
		int value = playerDataById.Property.CureCount.property.Value;
		HeroAttrEffect heroAttrEffect = null;
		if (value > 0)
		{
			heroAttrEffect = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCureUpdate(playerId, -value);
		}
		MapField<long, party.model.Buff> buffMap = playerDataById.player.serverPlayer.Hero.Buffs;
		UpdateHeroAttrS2C updateHeroAttrS2C = new UpdateHeroAttrS2C
		{
			PlayerId = playerId,
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Unknown,
				Id = 0L
			},
			EffectDatas = 
			{
				new HeroAttrEffect
				{
					PlayerId = playerId,
					Buff = new HeroBuffChangeS2C
					{
						PlayerId = playerId,
						Op = HeroBuffChangeS2C.Types.Oper.Noop
					}
				}
			}
		};
		if (heroAttrEffect != null)
		{
			updateHeroAttrS2C.EffectDatas.Add(heroAttrEffect);
		}
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(updateHeroAttrS2C);
		List<party.model.Buff> list = buffMap.Values.ToList();
		buffMap.Clear();
		foreach (party.model.Buff item in list)
		{
			if (item.BuffData.OnRemove != null)
			{
				await item.BuffData.OnRemove.Apply(item);
			}
		}
	}
}
