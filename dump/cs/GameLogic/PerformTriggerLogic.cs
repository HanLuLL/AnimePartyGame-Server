using System;
using System.Collections.Generic;
using FairyGUI;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using UnityTimer;
using party.protocol;

namespace GameLogic;

public class PerformTriggerLogic
{
	public PerformTriggerSignal signal = new PerformTriggerSignal();

	private Dictionary<PerformTriggerType, List<PerformTriggerConfigure>> _performTriggers = new Dictionary<PerformTriggerType, List<PerformTriggerConfigure>>();

	private Dictionary<int, Timer> _performTriggerTimers = new Dictionary<int, Timer>();

	private int _curPerformTriggerId;

	public void InitPerformTrigger()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
		{
			return;
		}
		RemoveEventListener();
		AddEventListener();
		ReleaseAllTimer();
		_performTriggers.Clear();
		int mapId = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapId;
		int performTriggerSetId = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapDifficultyId.GetMapGameDifficultyItems()[SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Difficulty].PerformTriggerSetId;
		if (performTriggerSetId == 0)
		{
			Debug.LogError($"在MapGameDifficultyConfigureItem({mapId})表里并没有找到performTriggerSetId");
			return;
		}
		PerformTriggerSetConfigure performTriggerSetConfigure = performTriggerSetId.GetPerformTriggerSetConfigure();
		if (performTriggerSetConfigure == null)
		{
			Debug.LogError($"在Perform.TriggerSetDict表里并没有找到performTriggerSetId：{performTriggerSetId}");
			return;
		}
		foreach (PerformTriggerSetConfigureItem performTriggerSetConfigureItem in performTriggerSetConfigure.PerformTriggerSetConfigureItems)
		{
			PerformTriggerConfigure performTriggerConfigure = performTriggerSetConfigureItem.TriggerId.GetPerformTriggerConfigure();
			if (!_performTriggers.TryGetValue(performTriggerConfigure.PerformTriggerType, out var value))
			{
				value = new List<PerformTriggerConfigure>();
				_performTriggers.Add(performTriggerConfigure.PerformTriggerType, value);
			}
			value.Add(performTriggerConfigure);
		}
		foreach (List<PerformTriggerConfigure> value2 in _performTriggers.Values)
		{
			value2.Sort(TriggerSorted);
		}
	}

	private void AddEventListener()
	{
		signal.characterFightHitSignal.AddListener(OnCharacterFightHit);
		signal.heroMovePointsSignal.AddListener(OnHeroMovePoints);
		signal.heroStarUpSignal.AddListener(OnHeroStarUp);
		signal.monsterShowSignal.AddListener(OnMonsterShow);
		signal.refereeJoinBattleSignal.AddListener(OnRefereeJoinBattle);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.playerHpChange.AddListener(OnCharacterHit);
	}

	private void RemoveEventListener()
	{
		signal.characterFightHitSignal.RemoveAllListeners();
		signal.heroMovePointsSignal.RemoveAllListeners();
		signal.heroStarUpSignal.RemoveAllListeners();
		signal.monsterShowSignal.RemoveAllListeners();
		signal.refereeJoinBattleSignal.RemoveAllListeners();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.playerHpChange.RemoveListener(OnCharacterHit);
	}

	private void ReleaseAllTimer()
	{
		foreach (Timer value in _performTriggerTimers.Values)
		{
			if (value != null)
			{
				value.Cancel();
			}
		}
		_performTriggerTimers.Clear();
	}

	private void ReleaseTimer(int performTriggerId)
	{
		if (_performTriggerTimers.TryGetValue(performTriggerId, out var value))
		{
			if (value != null)
			{
				value.Cancel();
			}
			_performTriggerTimers.Remove(performTriggerId);
		}
	}

	private void PlayTriggerPerform()
	{
		if (_curPerformTriggerId == 0)
		{
			Debug.LogError("PlayTriggerPerform 失败 _curPerformTriggerId 为 0");
			return;
		}
		PerformTriggerConfigure curPerformTrigger = _curPerformTriggerId.GetPerformTriggerConfigure();
		if (curPerformTrigger == null)
		{
			Debug.LogError($"PlayTriggerPerform 失败 _curPerformTriggerId：{_curPerformTriggerId} 不存在");
			return;
		}
		if (!_performTriggerTimers.ContainsKey(curPerformTrigger.Id))
		{
			_performTriggerTimers.Add(curPerformTrigger.Id, Timer.Register(0f, (float)curPerformTrigger.TimeDelay / 1000f, (Action)delegate
			{
				ReleaseTimer(curPerformTrigger.Id);
				PlayTriggerPerformAction(curPerformTrigger);
			}, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, (Action)null, false, -1f, false, (GameObject)null));
		}
		MapField<int, int> audio = curPerformTrigger.Audio;
		if (audio != null && audio.Count > 0)
		{
			PlayTriggerPerformAudio(curPerformTrigger);
		}
		_curPerformTriggerId = 0;
	}

	private void PlayTriggerPerformAction(PerformTriggerConfigure curPerformTrigger)
	{
		if (curPerformTrigger != null)
		{
			PerformTriggerActionType performTriggerActionType = curPerformTrigger.PerformTriggerActionType;
			if (performTriggerActionType != PerformTriggerActionType.None && performTriggerActionType == PerformTriggerActionType.RefereeCall)
			{
				PlayTriggerPerformAction_RefereeCall(curPerformTrigger);
			}
		}
	}

	private void PlayTriggerPerformAction_RefereeCall(PerformTriggerConfigure curPerformTrigger)
	{
		BattlePlayerData battlePlayerData = null;
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (playerData.player.characterConfig.Id == 1061)
			{
				battlePlayerData = playerData;
				break;
			}
		}
		if (battlePlayerData == null || battlePlayerData.CharacterInst == null)
		{
			Debug.Log($"PlayTriggerPerformAction 失败 未找到裁判玩家,performTriggerId：{curPerformTrigger.Id}");
		}
		else if (curPerformTrigger.PerformTriggerActionParams != null && curPerformTrigger.PerformTriggerActionParams.Count > 1)
		{
			string text = null;
			int index = UnityEngine.Random.Range(1, curPerformTrigger.PerformTriggerActionParams.Count);
			text = curPerformTrigger.PerformTriggerActionParams[index].GetLocal(UIStringType.Perform);
			if (!string.IsNullOrEmpty(text))
			{
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.Signal.battleMessage.Dispatch(new BattleMessage(battlePlayerData, text, MessageType.REFEREE));
			}
			int performId = curPerformTrigger.PerformTriggerActionParams[0];
			SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(battlePlayerData.player.Id, performId, "触发Perform Trigger, 裁判演出");
		}
	}

	private void PlayTriggerPerformAudio(PerformTriggerConfigure curPerformTrigger)
	{
		switch (curPerformTrigger.PerformTriggerActionType)
		{
		case PerformTriggerActionType.None:
			PlayPerformAudio(curPerformTrigger);
			break;
		case PerformTriggerActionType.RefereeCall:
		{
			BattlePlayerData battlePlayerData = null;
			foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
			{
				if (playerData.player.characterConfig.Id == 1061)
				{
					battlePlayerData = playerData;
					break;
				}
			}
			if (battlePlayerData == null || battlePlayerData.CharacterInst == null)
			{
				Debug.Log($"PlayTriggerPerformAction 失败 未找到裁判玩家,performTriggerId：{curPerformTrigger.Id}");
			}
			else
			{
				PlayPerformAudio(curPerformTrigger);
			}
			break;
		}
		}
	}

	private void PlayPerformAudio(PerformTriggerConfigure curPerformTrigger)
	{
		foreach (int audioKey in curPerformTrigger.Audio.Keys)
		{
			if (!_performTriggerTimers.ContainsKey(audioKey))
			{
				_performTriggerTimers.Add(audioKey, Timer.Register(0f, (float)curPerformTrigger.Audio[audioKey] / 1000f, (Action)delegate
				{
					ReleaseTimer(audioKey);
					Stage.inst.PlayOneShotSound(audioKey);
				}, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, (Action)null, false, -1f, false, (GameObject)null));
			}
		}
	}

	private void OnCharacterHit(HeroHpChangeS2C hpChange, bool isFight)
	{
		if (isFight || hpChange.RealChangeHp > 0)
		{
			return;
		}
		int damageType = hpChange.DamageType;
		if ((damageType == 2 || damageType == 1) && hpChange.Killer > 0)
		{
			OnTriggerCharacterHit(hpChange.PlayerId, hpChange.Killer, hpChange.RealChangeHp * -1, hpChange.CurrHp > 0, isFight: false);
		}
		else if (hpChange.CurrHp <= 0)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(hpChange.PlayerId);
			if (playerDataById != null && playerDataById.characterType == CharacterType.Hero)
			{
				OnTriggerCharacterHit(hpChange.PlayerId, hpChange.Killer, hpChange.RealChangeHp * -1, isAlive: false, isFight: false);
			}
		}
	}

	private void OnCharacterFightHit(long defenderId, long attackerId, int changeHp, bool isAlive)
	{
		int num = changeHp * -1;
		if (num >= 0)
		{
			OnTriggerCharacterHit(defenderId, attackerId, num, isAlive, isFight: true);
		}
	}

	private void OnHeroMovePoints(long playerId, int movePoint)
	{
		OnTriggerHeroMovePoints(playerId, movePoint);
	}

	private void OnHeroStarUp(long playerId, int starLevel)
	{
		OnTriggerHeroStarUp(playerId, starLevel);
	}

	private void OnMonsterShow(int monsterConfigId)
	{
		OnTriggerMonsterShow(monsterConfigId);
	}

	private void OnRefereeJoinBattle()
	{
		OnTriggerRefereeJoinBattle();
	}

	private void OnTriggerCharacterHit(long defenderId, long attackerId, int hitHp, bool isAlive, bool isFight)
	{
		if (defenderId == 0L)
		{
			Debug.LogError("OnTriggerCharacterHit 触发 defenderId为0");
		}
		else
		{
			if (!_performTriggers.TryGetValue(PerformTriggerType.TakeDamage, out var value))
			{
				return;
			}
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(defenderId);
			BattlePlayerData playerDataById2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attackerId);
			if (playerDataById != null && playerDataById2 != null && playerDataById2.characterType == playerDataById.characterType)
			{
				return;
			}
			foreach (PerformTriggerConfigure item in value)
			{
				if (CheckTakeDamageTrigger(item, playerDataById, playerDataById2, hitHp, isAlive, isFight))
				{
					_curPerformTriggerId = item.Id;
				}
			}
			if (_curPerformTriggerId != 0)
			{
				PlayTriggerPerform();
			}
		}
	}

	private void OnTriggerHeroMovePoints(long playerId, int movePoint)
	{
		if (playerId == 0L)
		{
			Debug.LogError("OnTriggerHeroMovePoints 触发 playerId为0");
		}
		else if (movePoint == 0)
		{
			Debug.Log("OnTriggerHeroMovePoints 触发 movePoint为0");
		}
		else
		{
			if (!_performTriggers.TryGetValue(PerformTriggerType.HeroMoveFinalPoints, out var value))
			{
				return;
			}
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			foreach (PerformTriggerConfigure item in value)
			{
				if (CheckHeroMovePointsTrigger(item, playerDataById, movePoint))
				{
					_curPerformTriggerId = item.Id;
				}
			}
			if (_curPerformTriggerId != 0)
			{
				PlayTriggerPerform();
			}
		}
	}

	private void OnTriggerHeroStarUp(long playerId, int starLevel)
	{
		if (playerId == 0L)
		{
			Debug.LogError("OnTriggerHeroStarUp 触发 playerId为0");
		}
		else if (starLevel <= 0)
		{
			Debug.Log("OnTriggerHeroStarUp 触发 starLevel为无效值");
		}
		else
		{
			if (!_performTriggers.TryGetValue(PerformTriggerType.HeroStarUp, out var value))
			{
				return;
			}
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			foreach (PerformTriggerConfigure item in value)
			{
				if (CheckHeroStarUpTrigger(item, playerDataById, starLevel))
				{
					_curPerformTriggerId = item.Id;
				}
			}
			if (_curPerformTriggerId != 0)
			{
				PlayTriggerPerform();
			}
		}
	}

	private void OnTriggerMonsterShow(int monsterConfigId)
	{
		if (monsterConfigId <= 0)
		{
			Debug.LogError("OnTriggerMonsterShow 触发 monsterConfigId为无效值");
		}
		else
		{
			if (!_performTriggers.TryGetValue(PerformTriggerType.MonsterShow, out var value))
			{
				return;
			}
			foreach (PerformTriggerConfigure item in value)
			{
				if (CheckMonsterShowTrigger(item, monsterConfigId))
				{
					_curPerformTriggerId = item.Id;
				}
			}
			if (_curPerformTriggerId != 0)
			{
				PlayTriggerPerform();
			}
		}
	}

	private void OnTriggerRefereeJoinBattle()
	{
		if (!_performTriggers.TryGetValue(PerformTriggerType.RefereeJoinBattle, out var value))
		{
			return;
		}
		foreach (PerformTriggerConfigure item in value)
		{
			if (CheckRefereeJoinBattleTrigger(item))
			{
				_curPerformTriggerId = item.Id;
			}
		}
		if (_curPerformTriggerId != 0)
		{
			PlayTriggerPerform();
		}
	}

	private bool CheckTakeDamageTrigger(PerformTriggerConfigure trigger, BattlePlayerData defender, BattlePlayerData attacker, int hitHp, bool isAlive, bool isFight)
	{
		if (!CheckTriggerPriority(trigger))
		{
			return false;
		}
		int num = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 0, 0);
		int num2 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 1, 0);
		int num3 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 2, 0);
		int num4 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 3, -1);
		int num5 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 4, -1);
		int num6 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 5, 0);
		int num7 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 6, 0);
		int num8 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 7, 0);
		if (num != 0)
		{
			if (num == 1 && !isFight)
			{
				return false;
			}
			if (num == 2 && isFight)
			{
				return false;
			}
		}
		if (num3 != 0)
		{
			if (defender == null)
			{
				return false;
			}
			if (num3 != defender.player.characterConfig.Id)
			{
				return false;
			}
		}
		if (num8 != 0 && (attacker == null || num8 != attacker.player.characterConfig.Id))
		{
			return false;
		}
		if (num6 != 0)
		{
			if (num6 == 1 && isAlive)
			{
				return false;
			}
			if (num6 == 2 && !isAlive)
			{
				return false;
			}
		}
		if (num2 != 0)
		{
			if (defender == null)
			{
				return false;
			}
			if (defender.player.characterType == CharacterType.Monster && num2 == 1)
			{
				return false;
			}
			if (defender.player.characterType == CharacterType.Hero && num2 == 2)
			{
				return false;
			}
		}
		if (num4 >= 0 && hitHp < num4)
		{
			return false;
		}
		if (num5 >= 0 && hitHp > num5)
		{
			return false;
		}
		if (num7 > 0)
		{
			if (defender == null)
			{
				return false;
			}
			float num9 = (float)defender.Property.maxHP * ((float)num7 / 1000f);
			if ((float)hitHp < num9)
			{
				return false;
			}
		}
		if (trigger.ApplyProbability >= 1000)
		{
			return true;
		}
		return trigger.ApplyProbability >= UnityEngine.Random.Range(0, 1000);
	}

	private bool CheckHeroMovePointsTrigger(PerformTriggerConfigure trigger, BattlePlayerData player, int movePoint)
	{
		if (!CheckTriggerPriority(trigger))
		{
			return false;
		}
		int num = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 0, -1);
		int num2 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 1, -1);
		int num3 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 2, 0);
		if (num != -1 && movePoint < num)
		{
			return false;
		}
		if (num2 != -1 && movePoint > num2)
		{
			return false;
		}
		if (num3 != 0)
		{
			if (player == null)
			{
				return false;
			}
			if (num3 != player.player.characterConfig.Id)
			{
				return false;
			}
		}
		if (trigger.ApplyProbability >= 1000)
		{
			return true;
		}
		return trigger.ApplyProbability >= UnityEngine.Random.Range(0, 1000);
	}

	private bool CheckHeroStarUpTrigger(PerformTriggerConfigure trigger, BattlePlayerData player, int starLevel)
	{
		if (!CheckTriggerPriority(trigger))
		{
			return false;
		}
		int num = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 0, 0);
		int num2 = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 1, 0);
		if (num2 != 0)
		{
			if (player == null)
			{
				return false;
			}
			if (num2 != player.player.characterConfig.Id)
			{
				return false;
			}
		}
		if (num == 0)
		{
			Debug.LogError($"HeroStarUp 参数错误, triggerId: {trigger.Id}");
			return false;
		}
		if (num != starLevel)
		{
			return false;
		}
		if (trigger.ApplyProbability >= 1000)
		{
			return true;
		}
		return trigger.ApplyProbability >= UnityEngine.Random.Range(0, 1000);
	}

	private bool CheckMonsterShowTrigger(PerformTriggerConfigure trigger, int monsterConfigId)
	{
		if (!CheckTriggerPriority(trigger))
		{
			return false;
		}
		int num = TryGetPerformTriggerParam(trigger.PerformTriggerParams, 0, 0);
		if (num == 0)
		{
			Debug.LogError($"MonsterShow 参数错误, triggerId: {trigger.Id}");
			return false;
		}
		if (num != monsterConfigId)
		{
			return false;
		}
		if (trigger.ApplyProbability >= 1000)
		{
			return true;
		}
		return trigger.ApplyProbability >= UnityEngine.Random.Range(0, 1000);
	}

	private bool CheckRefereeJoinBattleTrigger(PerformTriggerConfigure trigger)
	{
		if (!CheckTriggerPriority(trigger))
		{
			return false;
		}
		if (trigger.ApplyProbability >= 1000)
		{
			return true;
		}
		return trigger.ApplyProbability >= UnityEngine.Random.Range(0, 1000);
	}

	private bool CheckTriggerPriority(PerformTriggerConfigure trigger)
	{
		if (_curPerformTriggerId == 0)
		{
			return true;
		}
		return false;
	}

	private int TryGetPerformTriggerParam(RepeatedField<int> trigger, int index, int defaultValue)
	{
		if (index >= 0 && index < trigger.Count)
		{
			return trigger[index];
		}
		return defaultValue;
	}

	private static int TriggerSorted(PerformTriggerConfigure x, PerformTriggerConfigure y)
	{
		return y.Priority.CompareTo(x.Priority);
	}
}
