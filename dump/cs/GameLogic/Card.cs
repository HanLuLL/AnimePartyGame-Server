using System.Collections.Generic;
using Core;
using Core.Net;
using Core.Tutorial;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public abstract class Card
{
	protected int cardId;

	protected CardInfoConfigure config;

	public CardInfoConfigure Config => config;

	public virtual async UniTask CardAction(long _Sn)
	{
		(await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCard()).UpdateConfig(config);
	}

	public virtual async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await UniTask.CompletedTask;
	}

	public virtual async UniTask ActiveSummon(long PlayerId)
	{
		await UniTask.CompletedTask;
	}

	public virtual void CardScope(bool state)
	{
	}

	public virtual async UniTask CardAttrShow(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, config.ReleaseDefaultPerform, "卡牌使用者");
		if (perform.isCancel)
		{
			return;
		}
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < playerIds.Count; i++)
		{
			await perform.PlayPlayerShow(playerIds[i], config.TargetDefaultPerform, "卡牌演出2-目标玩家");
			if (perform.isCancel)
			{
				break;
			}
		}
	}

	protected RepeatedField<long> GetTargetPlayers()
	{
		RepeatedField<long> repeatedField = new RepeatedField<long>();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if ((config.IsContainMonster || playerData.characterType != CharacterType.Monster) && (config.CardTargetType != CardTargetType.Target || (!playerData.Property.NotSelect.Value && (config.IsContainPlayer || playerData.characterType != CharacterType.Hero) && (config.IsContainDeadTarget || playerData.Property.HP.Value != 0) && (config.IsContainHospitalPlayer || !(playerData.CharacterInst != null) || playerData.CharacterInst.standLand.LandType != LandType.Hospital) && (config.IsContainSelf || selfPlayerData.player.Id != playerData.player.Id) && ((config.IsFriendly && selfPlayerData.player.TeamId == playerData.player.TeamId) || (config.IsHostile && selfPlayerData.player.TeamId != playerData.player.TeamId)))) && CheckVailDistance(selfPlayerData, playerData))
			{
				repeatedField.Add(playerData.player.Id);
			}
		}
		return repeatedField;
	}

	protected virtual bool CheckVailDistance(BattlePlayerData _self, BattlePlayerData _target)
	{
		return false;
	}

	protected string ChangeCardAttackDescription(BattlePlayerData playerData, string desc, int paramIndex = 2)
	{
		int num = playerData.Property.CardAddAttack.Value;
		if (this is Card_20031)
		{
			Buff buff = playerData.buffContainer.GetBuff(1211201);
			if (buff != null)
			{
				num += buff.Progress;
			}
		}
		if (num <= 0)
		{
			return desc;
		}
		int num2 = -config.Params[paramIndex];
		desc = desc.Replace($"damage={num2}", $"damage=[color=#94FF46]{num2 + num}[/color]");
		return desc;
	}

	public string CardDescription(long playerId, bool replaceDamage)
	{
		string text = CardDescription(playerId);
		if (replaceDamage && text.Contains("{damage="))
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById != null)
			{
				text = ChangeCardAttackDescription(playerDataById, text);
			}
		}
		return text;
	}

	public virtual string CardDescription(long playerId)
	{
		return config.DescId.GetLocal(UIStringType.Card);
	}

	public virtual string CardDescription(long playerId, HandCardData handCardData)
	{
		return CardDescription(playerId, replaceDamage: true);
	}

	public virtual int GetCostValue(long playerId)
	{
		return config.Cost;
	}

	public virtual int GetCostValue(long playerId, HandCardData handCardData)
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10)
		{
			return GetCostValue(playerId);
		}
		return handCardData?.BattleCost ?? GetCostValue(playerId);
	}

	public virtual bool VailStatus()
	{
		return true;
	}

	public virtual int GetRecommendScore()
	{
		if (config.RecomPlus.Count <= 1)
		{
			return config.RecomBase;
		}
		if (1 == config.RecomPlus[0])
		{
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (selfPlayerData.Property.HP.Value * 2 < selfPlayerData.Property.maxHP)
			{
				return config.RecomBase + config.RecomPlus[1];
			}
		}
		else if (3 == config.RecomPlus[0])
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().Property.ModifyNum.property.Value == 1)
			{
				return config.RecomBase + config.RecomPlus[1];
			}
		}
		else if (2 == config.RecomPlus[0] && IsKillableMonster())
		{
			return config.RecomBase + config.RecomPlus[1];
		}
		return config.RecomBase;
	}

	private bool IsKillableMonster()
	{
		if (config.Params.Count != 0)
		{
			RepeatedField<int> repeatedField = config.Params;
			if (repeatedField[repeatedField.Count - 1] <= 0)
			{
				RepeatedField<long> targetPlayers = GetTargetPlayers();
				for (int i = 0; i < targetPlayers.Count; i++)
				{
					int value = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(targetPlayers[i]).Property.HP.Value;
					RepeatedField<int> repeatedField2 = config.Params;
					if (value + repeatedField2[repeatedField2.Count - 1] <= 0)
					{
						return true;
					}
				}
				return false;
			}
		}
		return false;
	}

	public virtual async UniTask<bool> TutorialCardEffect(long actionPlayer, UseEffectCardC2S msg)
	{
		await TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.UseCard);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(actionPlayer);
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(actionPlayer))
		{
			playerDataById.Property.cardUseTimes.Value++;
		}
		List<HandCardData> handCards = playerDataById.cardContainer._HandCards;
		if (handCards.Remove(handCards.Find((HandCardData x) => x.CardId == msg.CardId)))
		{
			HeroAttrEffect cardUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetCardUpdate(actionPlayer, playerDataById.cardContainer._CardInfos);
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
			{
				Cause = new CauseOrigin
				{
					S = CauseOrigin.Types.source.Unknown,
					Id = msg.CardId
				},
				PlayerId = actionPlayer,
				EffectDatas = { cardUpdate }
			});
			await MonoSingletonProvider<NetManager>.inst.RPC.UseEffectCardS2C.OnUseEffectCardS2CServerCallBackAsync(new UseEffectCardS2C
			{
				CardId = msg.CardId,
				PlayerId = actionPlayer,
				TargetIds = { (IEnumerable<long>)msg.TargetIds },
				TargetNodeIds = { (IEnumerable<int>)msg.TargetNodeIds }
			}, 0, isDispatch: true);
			return true;
		}
		Debug.LogError($"在当前玩家{actionPlayer}身上找不到目标卡牌{msg.CardId}");
		await TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.Idle);
		return false;
	}
}
