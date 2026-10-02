using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core;
using Core.MapEvents;
using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class ActionLogic : IRPCSync
{
	public struct CardSuggestData
	{
		public int CardId;

		public int SuggestScore;
	}

	public ActionSignal signal = new ActionSignal();

	private PlayerActionEnum _playerAction;

	public RepeatedField<int> UsableCards;

	public bool NotMove;

	public long throwDiceSn;

	public long CardSN;

	private readonly ConcurrentDictionary<long, HeroSkillMoveEffectS2C> _MoveAttrDict = new ConcurrentDictionary<long, HeroSkillMoveEffectS2C>();

	private readonly Dictionary<int, Skill> SkillInstanceDict = new Dictionary<int, Skill>();

	public PlayerActionEnum playerAction
	{
		get
		{
			return _playerAction;
		}
		set
		{
			_playerAction = value;
		}
	}

	public void Connect()
	{
		Connect_Attr();
		MonoSingletonProvider<NetManager>.inst.RPC.TimeWastingS2C.OnTimeWastingS2CServerCallBackAsync = OnTimeWastingS2CServerCallBack;
	}

	public void Disconnect()
	{
		Disconnect_Attr();
		MonoSingletonProvider<NetManager>.inst.RPC.TimeWastingS2C.OnTimeWastingS2CServerCallBackAsync = null;
	}

	public void DealThrowDice(party.model.Action action)
	{
		signal.notifyAllPlayer.Dispatch(action.PlayerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			UsableCards = null;
			throwDiceSn = action.Sn;
			ThrowDiceC2S throwDiceC2S = ByteBuf.ReadObject<ThrowDiceC2S>(action.Data.ToByteArray());
			if (throwDiceC2S.IsNoOper || throwDiceC2S.IsMoveNow)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.RequestThrowDiceC2S(throwDiceSn);
				return;
			}
			playerAction = PlayerActionEnum.MOVE;
		}
		signal.dealThrowDice.Dispatch(action.PlayerId);
	}

	public async UniTask DealMove(party.model.Action action)
	{
		signal.notifyAllPlayer.Dispatch(action.PlayerId);
		MoveC2S moveC2S = ByteBuf.ReadObject<MoveC2S>(action.Data.ToByteArray());
		await SimpleSingletonProvider<MoveArrowManager>.inst.DealMove(action.PlayerId, action.Sn, moveC2S.ForceDir);
	}

	public void DealUseEffectCard(party.model.Action action)
	{
		signal.notifyAllPlayer.Dispatch(action.PlayerId);
		UsableCards = null;
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			CardSN = action.Sn;
			UseEffectCardC2S useEffectCardC2S = ByteBuf.ReadObject<UseEffectCardC2S>(action.Data.ToByteArray());
			if (useEffectCardC2S.CardId != 0)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.card.RequestUseEffectCardC2S(CardSN, useEffectCardC2S.CardId);
				return;
			}
			UsableCards = useEffectCardC2S.CanUseCardIds;
			NotMove = useEffectCardC2S.NotMove;
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(action.PlayerId);
			if (playerDataById?.CharacterInst != null)
			{
				playerDataById.CharacterInst.ServerSkillNotVailStatus = useEffectCardC2S.NotUseSkill;
			}
			playerAction = PlayerActionEnum.CARD;
		}
		signal.dealEffectCard.Dispatch(action.PlayerId);
	}

	public async void DealAbandonCard(party.model.Action action)
	{
		signal.notifyAllPlayer.Dispatch(action.PlayerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			await SimpleSingletonProvider<UIManager>.inst.loseCard.ShowLoseCard(action.Sn);
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(action.PlayerId, 11014);
		}
	}

	public async void DealQuickCard(party.model.Action action)
	{
		signal.notifyAllPlayer.Dispatch(action.PlayerId);
		UseQuickCardC2S useQuickCardC2S = ByteBuf.ReadObject<UseQuickCardC2S>(action.Data.ToByteArray());
		RepeatedField<int> canUseQuickCard = SimpleSingletonProvider<GameLogicManager>.inst.card.GetCanUseQuickCard(useQuickCardC2S.PrevCardId, useQuickCardC2S.PrevPlayerId);
		int preCardId = useQuickCardC2S.PrevCardId;
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			CardSN = action.Sn;
			UsableCards = canUseQuickCard;
		}
		if (!SimpleSingletonProvider<UIManager>.inst.cardWindow.isShowing)
		{
			CardWindow cardWindow = await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCard();
			QuickCardStack cardStack = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.CardStack;
			if (cardStack != null)
			{
				cardWindow.ResetQuickCardData(cardStack.OriginalCardId, cardStack.OriginalPlayerId, cardStack.OriginalTargetIds, cardStack.History);
				SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.ClearClientCardStack();
			}
		}
		signal.dealQuickCard.Dispatch(action.PlayerId, preCardId);
	}

	public async void DealBombThrowDice(party.model.Action action)
	{
		signal.notifyAllPlayer.Dispatch(action.PlayerId);
		await SimpleSingletonProvider<UIManager>.inst.tips.ShowTipsAndWait(10004.GetLocal(UIStringType.Message), 2f);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.card.RequestBombThrowDice(action.Sn);
		}
	}

	public void RequestTimeWastingC2S(int time)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.TimeWastingC2S.TimeWastingC2SCall(new TimeWastingC2S
		{
			Time = time
		});
	}

	private async UniTask OnTimeWastingS2CServerCallBack(TimeWastingS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && isdispatch)
		{
			if (errid != 0 || model.Time == 0)
			{
				OperationTimer.CancelShowPlayerTimer();
			}
			else
			{
				BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
				string headUrl = ((playerDataById != null) ? playerDataById.GetCharacterHeadUrl() : "");
				OperationTimer.StartShowPlayerTimer(model.Time, headUrl);
			}
			await UniTask.CompletedTask;
		}
	}

	public int TryGetCardSuggestCardId(IList<int> usableCards)
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null || !curRoomInfo.IsPVE())
		{
			return 0;
		}
		if (usableCards == null || usableCards.Count <= 0)
		{
			return 0;
		}
		Dictionary<int, Card> cardActions = SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions;
		List<CardSuggestData> list = new List<CardSuggestData>();
		for (int i = 0; i < usableCards.Count; i++)
		{
			if (cardActions.TryGetValue(usableCards[i], out var value) && value.GetRecommendScore() > 0)
			{
				list.Add(new CardSuggestData
				{
					CardId = usableCards[i],
					SuggestScore = value.GetRecommendScore()
				});
			}
		}
		if (list.Count == 0)
		{
			return 0;
		}
		list.Sort((CardSuggestData x, CardSuggestData y) => -x.SuggestScore.CompareTo(y.SuggestScore));
		return list.Select((CardSuggestData s) => s.CardId).ToList().GetSafeByIndex(0);
	}

	public void Dispose()
	{
		CardSN = 0L;
	}

	public void DealMoveAgain(party.model.Action action)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequsetMoveAgainC2S(action.Sn);
		}
	}

	private async UniTask BombDieyShowAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		await SummonShow.SummonTriggerShow_2006(model);
	}

	private async UniTask HeroBuffShowAction(UpdateHeroAttrS2C model)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById == null)
		{
			return;
		}
		Buff buffById = playerDataById.buffContainer.GetBuffById(model.Cause.Id);
		if (buffById == null)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		MethodInfo method = typeof(BuffShow).GetMethod("BuffShow_" + buffById.BuffId);
		if (!(method != null))
		{
			await BuffShow.AttrChangeShow(model, buffById);
		}
		else
		{
			await (UniTask)method.Invoke(null, new object[2] { model, buffById });
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}

	private async UniTask LandBuffShowAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.TryGetLandBuff(model.Cause.Id, out var _LandBuff) && _LandBuff.buffData.Source.S == buff_source.Types.source.Summon)
		{
			if (_LandBuff.buffData.Chain.Count > 1 && _LandBuff.buffData.Chain[1].S == buff_source.Types.source.Card)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions[_LandBuff.buffData.Chain[1].Id].ActiveSummon(model.PlayerId);
			}
			MethodInfo method = typeof(SummonShow).GetMethod("SummonTriggerShow_" + _LandBuff.buffData.Source.Id);
			if (!(method != null))
			{
				await SummonShow.CommonSummonShow(_LandBuff.buffData.Source.Id, model, _LandBuff);
			}
			else
			{
				await (UniTask)method.Invoke(null, new object[2] { model, _LandBuff });
			}
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}

	public async UniTask CardShowAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue((int)model.Cause.Id, out var value))
		{
			await value.CardAttrShow(model);
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}

	private async UniTask DestinyShowAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		if (StaticConfigure.Destiny.InfoDict.TryGetValue(Convert.ToInt32(model.Cause.Id), out var value))
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, value.Perform, "命运");
		}
	}

	private async UniTask DivinationShowAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		if (!StaticConfigure.Divination.InfoDict.TryGetValue(Convert.ToInt32(model.Cause.Id), out var _config))
		{
			return;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, _config.Perform, "占卜");
			if (perform.isCancel)
			{
				break;
			}
		}
	}

	private async UniTask EventShowAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		MethodInfo method = typeof(LandEventShow).GetMethod("LandEventShow_" + model.Cause.Id);
		if (method != null)
		{
			await (UniTask)method.Invoke(null, new object[1] { model });
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}

	private async UniTask LandShowAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		UnitLand land = SimpleSingletonProvider<LandManager>.inst.GetLandById(Convert.ToInt32(model.Cause.Id));
		LandType landType = land.LandType;
		if (landType == LandType.Pursuit || landType == LandType.Shop || landType == LandType.Lottery || landType == LandType.MoveAgain || landType == LandType.Gamble || landType == LandType.Event || landType == LandType.OfferAreward || landType == LandType.Divination || landType == LandType.Destiny)
		{
			return;
		}
		LandInfoConfigure _config = StaticConfigure.Land.InfoDict[(int)land.LandType];
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		landType = land.LandType;
		if (landType == LandType.Born || landType == LandType.FillingStation || landType == LandType.DrawCard || landType == LandType.RollGold || landType == LandType.BloodLoss || landType == LandType.Heal || landType == LandType.Gift || landType == LandType.Relic)
		{
			await perform.PlayPlayerShow(model.PlayerId, _config.Perform1, $"地图格{model.Cause.Id}，{land.LandType}——格");
			if (perform.isCancel)
			{
				return;
			}
		}
		if (land.LandType == LandType.Battery)
		{
			for (int i = 0; i < model.EffectDatas.Count; i++)
			{
				await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, _config.Perform1, $"地图格{model.Cause.Id}，炮台");
				if (perform.isCancel)
				{
					return;
				}
			}
		}
		if (land.LandType == LandType.Hospital)
		{
			int performId = (SimpleSingletonProvider<GameLogicManager>.inst.land.InHospital ? _config.Perform1 : _config.Perform2);
			await perform.PlayPlayerShow(model.PlayerId, performId, $"地图格{model.Cause.Id}，住院格");
			if (perform.isCancel)
			{
				return;
			}
		}
		landType = land.LandType;
		if (landType == LandType.Portal || landType == LandType.Jump)
		{
			for (int i = 0; i < model.EffectDatas.Count; i++)
			{
				HeroPlaceChangeS2C place = model.EffectDatas[i].Place;
				if (place != null)
				{
					BattlePlayerData sendPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(place.PlayerId);
					SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(sendPlayer.CharacterInst, willMove: true);
					await perform.PlayPlayerShow(place.PlayerId, _config.Perform1, "传送门或飞门开始传送");
					if (perform.isCancel)
					{
						return;
					}
					SimpleSingletonProvider<GameLogicManager>.inst.battle.TakeOutHeroAttr(model.EffectDatas[i].PlayerId);
					sendPlayer.CharacterInst.SendCharacter(model.EffectDatas[i].Place.Place.NodeId, model.EffectDatas[i].Place.Place.FrontNodeIds);
					await perform.PlayPlayerShow(place.PlayerId, _config.Perform2, "传送门或飞门结束传送");
					if (perform.isCancel)
					{
						return;
					}
					SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(sendPlayer.CharacterInst, willMove: false);
				}
			}
		}
		if (land.LandType == LandType.Gimmick)
		{
			for (int i = 0; i < model.EffectDatas.Count; i++)
			{
				HeroPlaceChangeS2C place2 = model.EffectDatas[i].Place;
				if (place2 != null)
				{
					BattlePlayerData sendPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(place2.PlayerId);
					sendPlayer.StartSend();
					sendPlayer.CharacterInst.SendCharacter(model.EffectDatas[i].Place.Place.NodeId, model.EffectDatas[i].Place.Place.FrontNodeIds);
					await perform.PlayPlayerShow(place2.PlayerId, 8000301, "传送门或飞门结束传送");
					if (perform.isCancel)
					{
						return;
					}
					SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(sendPlayer.CharacterInst, willMove: false);
				}
			}
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}

	private async UniTask MapEventAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		Type type = Type.GetType($"Core.MapEvent_{model.Cause.Id}");
		if (type != null && Activator.CreateInstance(type) is MapEvent mapEvent)
		{
			await mapEvent.MapEventShowByAttr(model);
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}

	private async UniTask RoundAwardShowAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		UniTask[] array = new UniTask[4];
		List<long> changeAttrPlayerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < changeAttrPlayerIds.Count; i++)
		{
			array[i] = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(changeAttrPlayerIds[i], 2, "轮次奖励");
		}
		await UniTask.WhenAll(array);
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}

	private async UniTask ShopBuyAction(UpdateHeroAttrS2C model)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			return;
		}
		LandInfoConfigure landInfoConfigure = StaticConfigure.Land.InfoDict[7];
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, landInfoConfigure.Perform1, "轮次奖励");
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
		}
		else
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr().Forget();
		}
	}

	private async UniTask ShopOpenAction(UpdateHeroAttrS2C model)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			return;
		}
		LandInfoConfigure landInfoConfigure = StaticConfigure.Land.InfoDict[7];
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, landInfoConfigure.Perform2, "轮次奖励");
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}

	public void Connect_Attr()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.UpdateHeroAttrS2C.OnUpdateHeroAttrS2CServerCallBackAsync = OnUpdateHeroAttrS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.HeroSkillMoveEffectS2C.OnHeroSkillMoveEffectS2CServerCallBackAsync = OnHeroSkillMoveEffectS2CServerCallBack;
	}

	public void Disconnect_Attr()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.UpdateHeroAttrS2C.OnUpdateHeroAttrS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.HeroSkillMoveEffectS2C.OnHeroSkillMoveEffectS2CServerCallBackAsync = null;
	}

	private async UniTask OnUpdateHeroAttrS2CServerCallBack(UpdateHeroAttrS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errId != 0)
		{
			return;
		}
		switch (model.Cause.S)
		{
		case CauseOrigin.Types.source.Unknown:
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			break;
		case CauseOrigin.Types.source.Battle:
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas, isFight: true);
			break;
		case CauseOrigin.Types.source.Skill:
			await SkillShowAction(model);
			break;
		case CauseOrigin.Types.source.Card:
			await CardShowAction(model);
			break;
		case CauseOrigin.Types.source.Event:
			await EventShowAction(model);
			break;
		case CauseOrigin.Types.source.BombDie:
			await BombDieyShowAction(model);
			break;
		case CauseOrigin.Types.source.LandBuff:
			await LandBuffShowAction(model);
			break;
		case CauseOrigin.Types.source.HeroBuff:
			await HeroBuffShowAction(model);
			break;
		case CauseOrigin.Types.source.Land:
			await LandShowAction(model);
			break;
		case CauseOrigin.Types.source.Destiny:
			await DestinyShowAction(model);
			break;
		case CauseOrigin.Types.source.Divination:
			await DivinationShowAction(model);
			break;
		case CauseOrigin.Types.source.RoundAward:
			await RoundAwardShowAction(model);
			break;
		case CauseOrigin.Types.source.ShopBuy:
			await ShopBuyAction(model);
			break;
		case CauseOrigin.Types.source.ShopOpen:
			await ShopOpenAction(model);
			break;
		case CauseOrigin.Types.source.MapEvent:
			await MapEventAction(model);
			break;
		case CauseOrigin.Types.source.RoundEnd:
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			break;
		case CauseOrigin.Types.source.SelectRelic:
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			break;
		case CauseOrigin.Types.source.Mission:
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			break;
		case CauseOrigin.Types.source.BountyKill:
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			for (int i = 0; i < model.EffectDatas.Count; i++)
			{
				HeroGoldChangeS2C gold2 = model.EffectDatas[i].Gold;
				if (gold2 != null && gold2.ChangeGold != 0 && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.EffectDatas[i].PlayerId))
				{
					await SimpleSingletonProvider<UIManager>.inst.tips.ShowChosenOne(11017.GetLocal(UIStringType.Message), $"+{gold2.ChangeGold}", 1.5f);
				}
			}
			break;
		}
		case CauseOrigin.Types.source.BountyDefend:
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			for (int i = 0; i < model.EffectDatas.Count; i++)
			{
				HeroGoldChangeS2C gold = model.EffectDatas[i].Gold;
				if (gold != null && gold.ChangeGold != 0 && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.EffectDatas[i].PlayerId))
				{
					await SimpleSingletonProvider<UIManager>.inst.tips.ShowChosenOne(11018.GetLocal(UIStringType.Message), $"+{gold.ChangeGold}", 1.5f);
				}
			}
			break;
		}
		case CauseOrigin.Types.source.PveBossCombine:
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.OnMonsterCombine(model.EffectDatas);
			break;
		case CauseOrigin.Types.source.LuckyStar:
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			break;
		case CauseOrigin.Types.source.RoomTerms:
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
			break;
		case (CauseOrigin.Types.source)4:
			break;
		}
	}

	private async UniTask OnHeroSkillMoveEffectS2CServerCallBack(HeroSkillMoveEffectS2C model, int errId, bool isDispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errId != 0 || model == null || model.EffectData.Count == 0)
		{
			return;
		}
		if (_MoveAttrDict.Count != 0)
		{
			foreach (var (playerId, _) in _MoveAttrDict)
			{
				await HandleOtherMoveSkillAttr(playerId);
			}
			_MoveAttrDict.Clear();
		}
		_MoveAttrDict.TryAdd(model.PlayerId, model);
		await UniTask.CompletedTask;
	}

	public async UniTask TryShowMoveSkillEffect(long playerId, int landId)
	{
		if (!_MoveAttrDict.TryGetValue(playerId, out var value) || !value.EffectData.Remove(landId, out var value2) || value2 == null)
		{
			return;
		}
		foreach (UpdateHeroAttrS2C effect in value2.Effects)
		{
			await OnUpdateHeroAttrS2CServerCallBack(effect, 0, isdispatch: true);
		}
	}

	public async UniTask HandleOtherMoveSkillAttr(long playerId)
	{
		if (_MoveAttrDict == null || _MoveAttrDict.Count == 0 || !_MoveAttrDict.TryRemove(playerId, out var value) || value.EffectData.Count == 0)
		{
			return;
		}
		List<HeroSkillMoveEffect> list = value.EffectData.Values.ToList();
		_MoveAttrDict.Clear();
		if (list.Count <= 0)
		{
			return;
		}
		foreach (HeroSkillMoveEffect item in list)
		{
			foreach (UpdateHeroAttrS2C effect in item.Effects)
			{
				await OnUpdateHeroAttrS2CServerCallBack(effect, 0, isdispatch: true);
			}
		}
	}

	public async UniTask SkillShowAction(UpdateHeroAttrS2C model)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		Skill skill = ((!(playerDataById?.CharacterInst == null) && playerDataById.CharacterInst.skill != null && playerDataById.CharacterInst.skill.skillId == (int)model.Cause.Id) ? playerDataById.CharacterInst.skill : CreateSkillInst((int)model.Cause.Id));
		if (skill != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
			await skill.SkillAttrChange(model);
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
		}
		else
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAllHeroAttr(model.EffectDatas);
		}
	}

	public Skill CreateSkillInst(int skillId)
	{
		if (!SkillInstanceDict.TryGetValue(skillId, out var value))
		{
			Type type = Type.GetType($"GameLogic.Skill_{skillId}");
			if (type != null)
			{
				value = Activator.CreateInstance(type) as Skill;
			}
			else
			{
				Debug.LogWarning($"<color=#ff0000>skill:{skillId} 无法找到实例， 请确认是否需要</color>");
			}
			SkillInstanceDict.TryAdd(skillId, value);
		}
		return value;
	}

	private async UniTask SummonAction(UpdateHeroAttrS2C model)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordHeroAttr(model.EffectDatas);
		MethodInfo method = typeof(SummonShow).GetMethod("SummonTriggerShow_" + model.Cause.Id);
		if (method != null)
		{
			await (UniTask)method.Invoke(null, new object[1] { model });
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateOtherHeroAttr();
	}
}
