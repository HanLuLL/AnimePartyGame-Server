using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class CardLogic : IRPCSync
{
	public readonly CardSignal signal;

	public long prePlayerId;

	public readonly Dictionary<int, Card> cardActions = new Dictionary<int, Card>();

	private HashSet<int> _convertChangedCardIds = new HashSet<int>();

	public CardLogic()
	{
		InitCardMethod();
		signal = new CardSignal();
	}

	public void Dispose()
	{
	}

	public void Connect()
	{
		Connect_SkillCard();
		MonoSingletonProvider<NetManager>.inst.RPC.UseEffectCardS2C.OnUseEffectCardS2CServerCallBackAsync = OnUseEffectCardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.AbandonCardS2C.OnAbandonCardS2CServerCallBackAsync = OnAbandonCardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BombThrowDiceS2C.OnBombThrowDiceS2CServerCallBackAsync = OnBombThrowDiceS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.UseQuickCardS2C.OnUseQuickCardS2CServerCallBackAsync = OnUseQuickCardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ThrowDiceResultS2C.OnThrowDiceResultS2CServerCallBackAsync = OnThrowDiceResultS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SelectRewardCardS2C.OnSelectRewardCardS2CServerCallBackAsync = OnSelectRewardCardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RoomHeroCardChangeS2C.OnRoomHeroCardChangeS2CServerCallBackAsync = OnRoomHeroCardChangeS2CServerCallBack;
	}

	public void Disconnect()
	{
		Disconnect_SkillCard();
		MonoSingletonProvider<NetManager>.inst.RPC.UseEffectCardS2C.OnUseEffectCardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.AbandonCardS2C.OnAbandonCardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BombThrowDiceS2C.OnBombThrowDiceS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.UseQuickCardS2C.OnUseQuickCardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ThrowDiceResultS2C.OnThrowDiceResultS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestUseEffectCardC2S(long _sn, int _cardId, List<long> targetIds = null, List<int> landIds = null, int chooseEffectIndex = 0)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		UpdateUseCardCount(_cardId);
		UseEffectCardC2S useEffectCardC2S = new UseEffectCardC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			CardId = _cardId,
			UseSelectCardIndex = chooseEffectIndex,
			DevPoint = GMConfig.dev_MovePoint
		};
		if (targetIds != null)
		{
			useEffectCardC2S.TargetIds.AddRange(targetIds);
		}
		if (landIds != null)
		{
			useEffectCardC2S.TargetNodeIds.AddRange(landIds);
		}
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10)
		{
			long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
			if (cardActions.TryGetValue(_cardId, out var value))
			{
				value.TutorialCardEffect(playerID, useEffectCardC2S).Forget();
			}
			return null;
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.UseEffectCardC2S.UseEffectCardC2SCall(useEffectCardC2S);
	}

	private async UniTask OnUseEffectCardS2CServerCallBack(UseEffectCardS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errId != 0)
		{
			return;
		}
		if (model.UseSkill)
		{
			await TriggerSkill(model.PlayerId, model.SkillId, model.SkillCds);
		}
		else
		{
			signal.cancelUse.Dispatch();
			if (model.CardId != 0)
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
				if (playerDataById != null && !playerDataById.Property.cardUseData.TryAdd(model.CardId, 1))
				{
					playerDataById.Property.cardUseData[model.CardId]++;
				}
				await cardActions[model.CardId].CardCallBack(model.PlayerId, model.TargetIds, model.EnterReverse, 0);
			}
			else if (SimpleSingletonProvider<UIManager>.inst.cardWindow.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.cardWindow.OnCloseWin();
			}
		}
		SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
	}

	private async UniTask TriggerSkill(long playerId, int skillId, MapField<int, int> SkillCds)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		Skill skill;
		if (playerDataById?.CharacterInst != null && playerDataById.CharacterInst.skill != null && playerDataById.CharacterInst.skill.skillId == skillId)
		{
			skill = playerDataById.CharacterInst.skill;
			playerDataById.CharacterInst.UpdateActiveSkillCD(SkillCds);
		}
		else
		{
			Debug.Log($"玩家{playerId}, 无法触发技能{skillId}, 尝试CreateSkillInst");
			skill = SimpleSingletonProvider<GameLogicManager>.inst.action.CreateSkillInst(skillId);
		}
		if (skill != null)
		{
			await skill.SkillTrigger(playerId);
		}
	}

	public void SkillDeleteShow(int skillId, long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById?.CharacterInst != null && playerDataById.CharacterInst.skill != null && playerDataById.CharacterInst.skill.skillId == skillId)
		{
			playerDataById.CharacterInst.skill.SkillDelete(playerId);
		}
		else
		{
			Debug.LogWarning($"玩家{playerId}, 无法触发技能{skillId}, 请检查");
		}
	}

	public void UpdateUseCardCount(int _cardId)
	{
		if (_cardId != 0)
		{
			UpdateUseCardCount();
		}
	}

	public void UpdateUseCardCount()
	{
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null)
		{
			selfPlayerData.Property.cardUseTimes.Value++;
		}
	}

	public RPCAsyncResult RequestUseQuickCardC2S(long _sn, int _cardId, long targetId)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.UseQuickCardC2S.UseQuickCardC2SCall(new UseQuickCardC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			CardId = _cardId,
			TargetId = targetId
		});
	}

	private async UniTask OnUseQuickCardS2CServerCallBack(UseQuickCardS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			signal.cancelUse.Dispatch();
			if (model.CardId != 0)
			{
				await cardActions[model.CardId].CardCallBack(model.PlayerId, new RepeatedField<long> { model.TargetId }, reverse: false, model.OriginalCardId);
			}
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
		}
	}

	public RepeatedField<int> GetCanUseQuickCard(int _cardId, long _prePlayerId)
	{
		prePlayerId = _prePlayerId;
		RepeatedField<int> cardIds = _cardId.GetCardConfigure().CardIds;
		List<HandCardData> handCards = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().cardContainer._HandCards;
		RepeatedField<int> repeatedField = new RepeatedField<int>();
		foreach (HandCardData item in handCards)
		{
			if (cardIds.Contains(item.CardId))
			{
				repeatedField.Add(item.CardId);
			}
		}
		return repeatedField;
	}

	public RPCAsyncResult RequestAbandonCardC2S(long _Sn, List<int> cardIds)
	{
		OperationTimer.CancelOperatTimer(_Sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_Sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.AbandonCardC2S.AbandonCardC2SCall(new AbandonCardC2S
		{
			Info = new ActionInfo
			{
				Sn = _Sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			CardUniqueIds = { (IEnumerable<int>)cardIds }
		});
	}

	private async UniTask OnAbandonCardS2CServerCallBack(AbandonCardS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.loseCard.HideImmediately();
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
			Effect effect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(14, Vector3.zero, Quaternion.identity, playerDataById.CharacterInst.EffectContainer);
			if (effect != null)
			{
				await effect.WaitFinishPlay();
			}
		}
	}

	public RPCAsyncResult RequestThrowDiceResultC2S(long _sn, int point)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.ThrowDiceResultC2S.ThrowDiceResultC2SCall(new ThrowDiceResultC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Point = point
		});
	}

	private async UniTask OnThrowDiceResultS2CServerCallBack(ThrowDiceResultS2C data, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public void RequestBombThrowDice(long _Sn)
	{
		OperationTimer.CancelOperatTimer(_Sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_Sn);
		MonoSingletonProvider<NetManager>.inst.RPC.BombThrowDiceC2S.BombThrowDiceC2SCall(new BombThrowDiceC2S
		{
			Info = new ActionInfo
			{
				Sn = _Sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			DevPoint = GMConfig.dev_BombPoint
		});
	}

	private async UniTask OnBombThrowDiceS2CServerCallBack(BombThrowDiceS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.landEvent.ShowEventThrowDice(model.PlayerId, model.Point);
		}
	}

	private void InitCardMethod()
	{
		foreach (CardInfoConfigure info in StaticConfigure.Card.Infos)
		{
			if (!cardActions.ContainsKey(info.Id))
			{
				Type type = Type.GetType($"GameLogic.Card_{info.Id}");
				if (type != null)
				{
					Card value = Activator.CreateInstance(type) as Card;
					cardActions.Add(info.Id, value);
				}
			}
		}
	}

	public void OnCardChanged(BattlePlayerData playerData, RepeatedField<CardInfo> attrCards, bool show = false)
	{
		if (playerData == null || playerData.CharacterInst == null || attrCards == null)
		{
			return;
		}
		int cardCount = playerData.cardContainer.CardCount;
		if (show)
		{
			if (attrCards.Count != cardCount)
			{
				playerData.CharacterInst.signal.attrChange.Dispatch((attrCards.Count, cardCount, attrCards.Count - cardCount, 3), "");
			}
			playerData.cardContainer.UpdateCardCount(attrCards.Count);
		}
		bool flag = true;
		if (attrCards.Count > 0)
		{
			flag = attrCards[0].CardId >= 0;
		}
		if (flag)
		{
			playerData.cardContainer.UpdateHandCards(attrCards);
			if (!SimpleSingletonProvider<UIManager>.inst.Fight.isShowing)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetSelfCardList.Dispatch(playerData.player.Id);
			}
		}
	}

	public async UniTask OnCardConvertChanged(BattlePlayerData playerData, RepeatedField<CardInfo> convertCards)
	{
		if (playerData == null || playerData.CharacterInst == null || convertCards == null || !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id))
		{
			return;
		}
		_convertChangedCardIds.Clear();
		foreach (CardInfo convertCard in convertCards)
		{
			HandCardData handCardData = playerData.cardContainer.CardConvertChanged(convertCard);
			if (handCardData != null)
			{
				_convertChangedCardIds.Add(handCardData.Guid);
			}
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.handCard.OnCardConvertChanged(_convertChangedCardIds);
	}

	private async UniTask OnRoomHeroCardChangeS2CServerCallBack(RoomHeroCardChangeS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
			if (playerDataById != null)
			{
				OnCardChanged(playerDataById, model.Cards);
				await UniTask.CompletedTask;
			}
		}
	}

	public void RequestRoundCard(long _sn, RepeatedField<int> cardIds, int selectedCardIndex)
	{
		OperationTimer.CancelOperatTimer(_sn);
		MonoSingletonProvider<NetManager>.inst.RPC.SelectRewardCardC2S.SelectRewardCardC2SCall(new SelectRewardCardC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			CardIds = { (IEnumerable<int>)cardIds },
			Idx = selectedCardIndex
		});
	}

	private async UniTask OnSelectRewardCardS2CServerCallBack(SelectRewardCardS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			if (SimpleSingletonProvider<UIManager>.inst.ChooseRoundCard.isShowing && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
			{
				SimpleSingletonProvider<UIManager>.inst.ChooseRoundCard.Hide();
			}
			SimpleSingletonProvider<UIManager>.inst.tips.HideMultiplePlayerThink(model.PlayerId, 11032);
			await UniTask.CompletedTask;
		}
	}

	public void Connect_SkillCard()
	{
	}

	public void Disconnect_SkillCard()
	{
	}

	public RPCAsyncResult RequestReleaseSkillC2S(long _sn, int _skillId, List<long> players = null, List<int> bestowCardIds = null, List<long> targetSummons = null, int dicePoint = 0, List<int> landIds = null)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		UseEffectCardC2S useEffectCardC2S = new UseEffectCardC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			UseSkill = true,
			SkillId = _skillId,
			DicePoint = dicePoint
		};
		if (players != null)
		{
			useEffectCardC2S.TargetIds.AddRange(players);
		}
		if (bestowCardIds != null)
		{
			useEffectCardC2S.CardUniqueIds.AddRange(bestowCardIds);
		}
		if (targetSummons != null)
		{
			useEffectCardC2S.LandBuffUniqueIds.AddRange(targetSummons);
		}
		if (landIds != null)
		{
			useEffectCardC2S.TargetNodeIds.AddRange(landIds);
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.UseEffectCardC2S.UseEffectCardC2SCall(useEffectCardC2S);
	}
}
