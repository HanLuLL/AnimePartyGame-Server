using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core;

public static class ActionCustomShow
{
	private static readonly Dictionary<string, Action<long>> _cache = new Dictionary<string, Action<long>>();

	private static readonly Dictionary<string, Action<SummonBase>> _summonCache = new Dictionary<string, Action<SummonBase>>();

	public static Action<long> Get(string key)
	{
		if (_cache.TryGetValue(key, out var value))
		{
			return value;
		}
		MethodInfo method = typeof(ActionCustomShow).GetMethod(key);
		if (method == null)
		{
			return null;
		}
		value = (Action<long>)Delegate.CreateDelegate(typeof(Action<long>), method);
		_cache[key] = value;
		return value;
	}

	public static void TakeOff(long playerId)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId)?.StartSend();
	}

	public static void ComeBackLand(long playerId)
	{
		Debug.Log($"完成的玩家ID：{playerId}");
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId)?.FinishSend();
	}

	public static void FastTravelByTaxi(long playerId)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId)?.StartSend_Car();
	}

	public static void FlashShow(long playerId)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId)?.FlashShow();
	}

	public static void AbsorbShadows(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById == null || playerDataById.CharacterInst == null || playerDataById.CharacterInst.skill == null)
		{
			return;
		}
		int safeByIndex = playerDataById.CharacterInst.skill.skillConfig.Params.GetSafeByIndex(0);
		List<LandBuffData> summonById = SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.GetSummonById(safeByIndex);
		if (summonById == null)
		{
			return;
		}
		foreach (LandBuffData item in summonById)
		{
			if (item.Summon != null)
			{
				Transform effectTransform = item.Summon.GetEffectTransform();
				SimpleSingletonProvider<EffectManager>.inst.PlayById(11103, effectTransform.position, Quaternion.LookRotation(playerDataById.CharacterInst.transform.position - effectTransform.position)).Forget();
				item.Summon.MoveToRole(playerDataById.CharacterInst.transform.position).Forget();
			}
		}
	}

	public static void VfxCharge128(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null && !(playerDataById.CharacterInst == null) && playerDataById.CharacterInst.skill is Skill_128 skill_)
		{
			skill_.PlayRoleSkillEffect(playerId).Forget();
		}
	}

	public static void VfxChargeHit128(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null && !(playerDataById.CharacterInst == null) && playerDataById.CharacterInst.skill is Skill_128 skill_)
		{
			skill_.PlayBurstEffect(playerId).Forget();
		}
	}

	public static void Talenting128(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null && !(playerDataById.CharacterInst == null) && playerDataById.CharacterInst.skill is Skill_128 skill_)
		{
			skill_.SetSkillAnime(playerId, status: true);
		}
	}

	public static Action<SummonBase> GetSummon(string key)
	{
		if (_summonCache.TryGetValue(key, out var value))
		{
			return value;
		}
		MethodInfo method = typeof(ActionCustomShow).GetMethod(key);
		if (method == null)
		{
			return null;
		}
		value = (Action<SummonBase>)Delegate.CreateDelegate(typeof(Action<SummonBase>), method);
		_summonCache[key] = value;
		return value;
	}

	public static void ShowSummon(SummonBase summon)
	{
		if (!(summon == null))
		{
			summon.ShowObject();
		}
	}
}
