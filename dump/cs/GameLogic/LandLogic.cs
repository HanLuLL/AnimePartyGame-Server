using System.Collections.Generic;
using Core;
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

public class LandLogic : IRPCSync
{
	public int land_Point;

	public bool InHospital;

	public readonly LandSignal signal = new LandSignal();

	public Action CardShopAction;

	public PVEShopBuyC2S PVEShopData;

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ShopBuyS2C.OnShopBuyS2CServerCallBackAsync = OnShopBuyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.PursuitS2C.OnPursuitS2CServerCallBackAsync = OnPursuitS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.LotteryChoiceS2C.OnLotteryChoiceS2CServerCallBackAsync = OnLotteryChoiceS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MoveAgainS2C.OnMoveAgainS2CServerCallBackAsync = OnMoveAgainS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RollGoldS2C.OnRollGoldS2CServerCallBackAsync = OnRollGoldS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.TriggerEventS2C.OnTriggerEventS2CServerCallBackAsync = OnTriggerEventS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.EventThrowDiceS2C.OnEventThrowDiceS2CServerCallBackAsync = OnEventThrowDiceS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.LotteryDrawS2C.OnLotteryDrawS2CServerCallBackAsync = OnLotteryDrawS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.LandChoiceTargetS2C.OnLandChoiceTargetS2CServerCallBackAsync = OnLandChoiceTargetS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.TriggerDestinyS2C.OnTriggerDestinyS2CServerCallBackAsync = OnTriggerDestinyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.TriggerDivinationS2C.OnTriggerDivinationS2CServerCallBackAsync = OnTriggerDivinationS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.StopOrContinueS2C.OnStopOrContinueS2CServerCallBackAsync = OnStopOrContinueS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.StartGambleS2C.OnStartGambleS2CServerCallBackAsync = OnStartGambleS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GambleThrowDicS2C.OnGambleThrowDicS2CServerCallBackAsync = OnGambleThrowDicS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GambleChangeS2C.OnGambleChangeS2CServerCallBackAsync = OnGambleChangeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.NoGambleNotifyS2C.OnNoGambleNotifyS2CServerCallBackAsync = OnNoGambleNotifyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GambleObServeS2C.OnGambleObServeS2CServerCallBackAsync = OnGambleObServeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.TriggerHospitalS2C.OnTriggerHospitalS2CServerCallBackAsync = OnTriggerHospitalS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MonsterPursuitS2C.OnMonsterPursuitS2CServerCallBackAsync = OnMonsterPursuitS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.PVEShopBuyS2C.OnPVEShopBuyS2CServerCallBackAsync = OnPVEShopBuyS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.AskReviveTeammateS2C.OnAskReviveTeammateS2CServerCallBackAsync = OnAskReviveTeammateS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BuyRelicS2C.OnBuyRelicS2CServerCallBackAsync = OnBuyRelicS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SelectMechanismS2C.OnSelectMechanismS2CServerCallBackAsync = OnSelectMechanismS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SelectEventS2C.OnSelectEventS2CServerCallBackAsync = OnSelectEventS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.VendorBuyCardS2C.OnVendorBuyCardS2CServerCallBackAsync = OnVendorBuyCardS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ShopBuyS2C.OnShopBuyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PursuitS2C.OnPursuitS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.LotteryChoiceS2C.OnLotteryChoiceS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MoveAgainS2C.OnMoveAgainS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RollGoldS2C.OnRollGoldS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.TriggerEventS2C.OnTriggerEventS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.EventThrowDiceS2C.OnEventThrowDiceS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.LotteryDrawS2C.OnLotteryDrawS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.LandChoiceTargetS2C.OnLandChoiceTargetS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.TriggerDestinyS2C.OnTriggerDestinyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.TriggerDivinationS2C.OnTriggerDivinationS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.StopOrContinueS2C.OnStopOrContinueS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.StartGambleS2C.OnStartGambleS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GambleThrowDicS2C.OnGambleThrowDicS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GambleChangeS2C.OnGambleChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GambleChangeS2C.OnGambleChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.TriggerHospitalS2C.OnTriggerHospitalS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MonsterPursuitS2C.OnMonsterPursuitS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PVEShopBuyS2C.OnPVEShopBuyS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.AskReviveTeammateS2C.OnAskReviveTeammateS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BuyRelicS2C.OnBuyRelicS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SelectMechanismS2C.OnSelectMechanismS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SelectEventS2C.OnSelectEventS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.VendorBuyCardS2C.OnVendorBuyCardS2CServerCallBackAsync = null;
	}

	public async void DealPVPCardShop(Action _action)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			if (playerDataById.CharacterInst != null)
			{
				await playerDataById.CharacterInst.SwitchCamera();
			}
			CardShopAction = _action;
			ShopBuyC2S pVPShopData = ByteBuf.ReadObject<ShopBuyC2S>(_action.Data.ToByteArray());
			SimpleSingletonProvider<UIManager>.inst.landShop.OpenPVPCardShop(pVPShopData);
			OperationTimer.ActionDownTime(_action.Sn, 5029, delegate
			{
				if (CardShopAction != null && CardShopAction.Sn != 0L)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.land.RequestShopBuyC2S(_action.Sn, new List<int>());
				}
			});
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11003);
		}
	}

	public RPCAsyncResult RequestShopBuyC2S(long _sn, List<int> indexList)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		CardShopAction.Sn = 0L;
		return MonoSingletonProvider<NetManager>.inst.RPC.ShopBuyC2S.ShopBuyC2SCall(new ShopBuyC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			BuyCards = { (IEnumerable<int>)indexList }
		});
	}

	private async UniTask OnShopBuyS2CServerCallBack(ShopBuyS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			if (model.BuyCards.Count == 0)
			{
				SimpleSingletonProvider<UIManager>.inst.landShop.Hide();
				SimpleSingletonProvider<UIManager>.inst.atm.Hide();
				CardShopAction = null;
			}
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
			if (SimpleSingletonProvider<UIManager>.inst.landShop.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.landShop.FinishShopBuy(model.PlayerId, model.BuyCards);
			}
			await UniTask.CompletedTask;
		}
	}

	public async void DealPVECardShop(Action _action)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			if (playerDataById.CharacterInst != null)
			{
				await playerDataById.CharacterInst.SwitchCamera();
			}
			CardShopAction = _action;
			PVEShopData = ByteBuf.ReadObject<PVEShopBuyC2S>(_action.Data.ToByteArray());
			if (SimpleSingletonProvider<UIManager>.inst.atm.isShowing)
			{
				await SimpleSingletonProvider<UIManager>.inst.atm.ShowATM();
			}
			if (!SimpleSingletonProvider<UIManager>.inst.atm.isShowing)
			{
				await SimpleSingletonProvider<UIManager>.inst.landShop.OpenPVECardShop();
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.landShop.UpdatePVECardShopData();
			}
			OperationTimer.ActionDownTime(_action.Sn, 5215, delegate
			{
				if (CardShopAction != null && CardShopAction.Sn != 0L)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.land.RequestPVEShopBuyC2S(_action.Sn, new List<int>(), 0L, isClose: true);
				}
			});
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11003);
		}
	}

	public RPCAsyncResult RequestPVEShopBuyC2S(long _sn, List<int> indexList, long assistPlayer, bool isClose = false)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		CardShopAction.Sn = 0L;
		return MonoSingletonProvider<NetManager>.inst.RPC.PVEShopBuyC2S.PVEShopBuyC2SCall(new PVEShopBuyC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			BuyCards = { (IEnumerable<int>)indexList },
			AssistPlayer = assistPlayer,
			IsClose = isClose
		});
	}

	private async UniTask OnPVEShopBuyS2CServerCallBack(PVEShopBuyS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			if (model.IsClose)
			{
				CardShopAction = null;
				SimpleSingletonProvider<UIManager>.inst.landShop.Hide();
				SimpleSingletonProvider<UIManager>.inst.atm.Hide();
			}
			if (SimpleSingletonProvider<UIManager>.inst.landShop.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.landShop.FinishShopBuy(model.PlayerId, model.BuyCards);
			}
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequsetPursuitC2S(long _sn, long player_Id)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.PursuitC2S.PursuitC2SCall(new PursuitC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			SelectPlayerId = player_Id
		});
	}

	private async UniTask OnPursuitS2CServerCallBack(PursuitS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
			{
				SimpleSingletonProvider<UIManager>.inst.landPursuit.HideImmediately();
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
			}
			if (!model.Exit)
			{
				LandInfoConfigure landInfoConfigure = StaticConfigure.Land.InfoDict[4];
				await ShowSendPlayer(model.PlayerId, landInfoConfigure.Perform1, landInfoConfigure.Perform2, model.NodeId, model.FrontIds);
			}
		}
	}

	private async UniTask ShowSendPlayer(long playerId, int perform1, int perform2, int nodeId, RepeatedField<int> frontIds)
	{
		ActionEffectShow perform3 = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerData.CharacterInst, willMove: false);
		if (playerData.CharacterInst != null)
		{
			await playerData.CharacterInst.SwitchCamera();
		}
		await perform3.PlayPlayerShow(playerId, perform1, "飞门开始传送");
		if (!perform3.isCancel)
		{
			if (playerData.CharacterInst != null)
			{
				playerData.CharacterInst.SendCharacter(nodeId, frontIds);
			}
			await perform3.PlayPlayerShow(playerId, perform2, "飞门结束传送");
			if (!perform3.isCancel)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerData.CharacterInst, willMove: false);
			}
		}
	}

	public void OnLotteryChanged(BattlePlayerData playerData, MapField<int, bool> Lotterys)
	{
		if (Lotterys == null || Lotterys.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<int, bool> Lottery in Lotterys)
		{
			if (!playerData.player.Hero.Lotterys.TryAdd(Lottery.Key, Lottery.Value))
			{
				playerData.player.Hero.Lotterys[Lottery.Key] = Lottery.Value;
			}
		}
	}

	public RPCAsyncResult RequsetLotteryChoiceC2S(long _sn, List<int> selectedLotterys)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.LotteryChoiceC2S.LotteryChoiceC2SCall(new LotteryChoiceC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Vals = { (IEnumerable<int>)selectedLotterys }
		});
	}

	private async UniTask OnLotteryChoiceS2CServerCallBack(LotteryChoiceS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.tips.HideMultiplePlayerThink(model.PlayerId, 11005);
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
			{
				SimpleSingletonProvider<UIManager>.inst.landLottery.CloseLotteryWin();
			}
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnLotteryDrawS2CServerCallBack(LotteryDrawS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0 && await SimpleSingletonProvider<UIManager>.inst.tips.ShowLottery(2f))
		{
			await (await SimpleSingletonProvider<UIManager>.inst.landLottery.ShowLand(2)).RefreshLotteryResultWin(model.PlayerIds, model.Val, model.AwardGold);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.CLearLottery();
		}
	}

	public RPCAsyncResult RequsetMoveAgainC2S(long _sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.MoveAgainC2S.MoveAgainC2SCall(new MoveAgainC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			DevPoint = GMConfig.dev_MoveAgainPoint
		});
	}

	private async UniTask OnMoveAgainS2CServerCallBack(MoveAgainS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			Character character = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId).CharacterInst;
			character.ResetStep(model.MovePoint);
			if (await character.SwitchCamera())
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.ThrowDice.Dispatch(model.PlayerId, model.MovePoint, model.MovePoint, t4: false);
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
				await SimpleSingletonProvider<RoadLineManager>.inst.GeneratePath(character, model.MovePoint);
			}
		}
	}

	public RPCAsyncResult RequsetRollGoldC2S(long _sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.RollGoldC2S.RollGoldC2SCall(new RollGoldC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			DevPoint = GMConfig.dev_RollGoldPoint
		});
	}

	private async UniTask OnRollGoldS2CServerCallBack(RollGoldS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			await (await SimpleSingletonProvider<UIManager>.inst.landRollGold.ShowLand()).RefreshRollGoldDice(model.Point);
		}
	}

	public RPCAsyncResult RequestTriggerEvent(long _sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.TriggerEventC2S.TriggerEventC2SCall(new TriggerEventC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			}
		});
	}

	private async UniTask OnEventThrowDiceS2CServerCallBack(EventThrowDiceS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0 && model.EventId != 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.landEvent.ShowLand();
			land_Point = model.Point;
		}
	}

	private async UniTask OnTriggerEventS2CServerCallBack(TriggerEventS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0 && model.EventId != 0)
		{
			await (await SimpleSingletonProvider<UIManager>.inst.landEvent.ShowLand()).RefreshEventData(model.PlayerId, model.EventId);
		}
	}

	public RPCAsyncResult RequestLandChoiceTargetC2S(long _sn, RepeatedField<long> targetPlayers)
	{
		Debug.Log("#炮台选择# 炮台请求");
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.LandChoiceTargetC2S.LandChoiceTargetC2SCall(new LandChoiceTargetC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			TargetIds = { (IEnumerable<long>)targetPlayers }
		});
	}

	public RPCAsyncResult RequestBatteryLeave(long _sn, bool exit)
	{
		Debug.Log("#炮台选择# 炮台请求");
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.LandChoiceTargetC2S.LandChoiceTargetC2SCall(new LandChoiceTargetC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Exit = exit
		});
	}

	private async UniTask OnLandChoiceTargetS2CServerCallBack(LandChoiceTargetS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
			SimpleSingletonProvider<UIManager>.inst.landBattery.AttackTargetPlayer(model.PlayerId, model.TargetIds);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestTriggerDivinationC2S(long _sn, int _id)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.TriggerDivinationC2S.TriggerDivinationC2SCall(new TriggerDivinationC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Id = _id
		});
	}

	private async UniTask OnTriggerDivinationS2CServerCallBack(TriggerDivinationS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
			await (await SimpleSingletonProvider<UIManager>.inst.landDivination.ShowLand(init: false)).UpdateDivinationDataSelect(model.PlayerId, model.Id, model.TargetType, model.TargetIds);
			SimpleSingletonProvider<UIManager>.inst.landDivination.HideImmediately();
		}
	}

	public RPCAsyncResult RequestTriggerDestinyC2S(long _sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.TriggerDestinyC2S.TriggerDestinyC2SCall(new TriggerDestinyC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			}
		});
	}

	private async UniTask OnTriggerDestinyS2CServerCallBack(TriggerDestinyS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			await (await SimpleSingletonProvider<UIManager>.inst.landEvent.ShowLand()).RefreshDestinyData(model.PlayerId, model.Id);
		}
	}

	public RPCAsyncResult RequestTriggerHospitalC2S(long _sn)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.TriggerHospitalC2S.TriggerHospitalC2SCall(new TriggerHospitalC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			}
		});
	}

	private async UniTask OnTriggerHospitalS2CServerCallBack(TriggerHospitalS2C data, int errId, bool isDispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			InHospital = data.InHospital;
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(data.PlayerId))
			{
				SimpleSingletonProvider<UIManager>.inst.landHospital.SwitchCheckingView(data.InHospital);
			}
			if (!(await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(3000)) && data.InHospital)
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(data.PlayerId, 11011);
				await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(2000);
			}
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(data.PlayerId);
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(data.PlayerId))
			{
				SimpleSingletonProvider<UIManager>.inst.landHospital.HideImmediately();
			}
		}
	}

	public RPCAsyncResult RequestStopOrContinueC2S(long _sn, bool stop)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.StopOrContinueC2S.StopOrContinueC2SCall(new StopOrContinueC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Stop = stop
		});
	}

	private async UniTask OnStopOrContinueS2CServerCallBack(StopOrContinueS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			await (await SimpleSingletonProvider<UIManager>.inst.landFillingStation.ShowLand()).UpdateBornWin(model.PlayerId, model.Stop);
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
		}
	}

	public RPCAsyncResult RequestStartGambleC2S(long _sn, bool isExec, int guess)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.StartGambleC2S.StartGambleC2SCall(new StartGambleC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			IsExec = isExec,
			GuessCode = guess
		});
	}

	public RPCAsyncResult RequestGambleThrowDicC2S(long _sn)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.GambleThrowDicC2S.GambleThrowDicC2SCall(new GambleThrowDicC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			DevPoint = GMConfig.dev_GamblePoint
		});
	}

	private async UniTask OnStartGambleS2CServerCallBack(StartGambleS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnGambleThrowDicS2CServerCallBack(GambleThrowDicS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnGambleChangeS2CServerCallBack(GambleChangeS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0 && SimpleSingletonProvider<UIManager>.inst.landGamble.isShowing)
		{
			await (await SimpleSingletonProvider<UIManager>.inst.landGamble.ShowLand()).RefreshGambleData(model.Hall);
		}
	}

	private async UniTask OnNoGambleNotifyS2CServerCallBack(NoGambleNotifyS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowTipsAndWait(10007.GetLocal(UIStringType.Message));
		}
	}

	private async UniTask OnGambleObServeS2CServerCallBack(GambleObServeS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			await (await SimpleSingletonProvider<UIManager>.inst.landGamble.ShowLand()).InitAsync(0L, model.Hall, enableJoin: false);
		}
	}

	public RPCAsyncResult RequestMonsterPursuitC2S(long _sn, long _monsterId)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.MonsterPursuitC2S.MonsterPursuitC2SCall(new MonsterPursuitC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			SelectId = _monsterId
		});
	}

	private async UniTask OnMonsterPursuitS2CServerCallBack(MonsterPursuitS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING)
		{
			if (SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.Hide();
			}
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
			if (errid == 0 && !model.Exit)
			{
				LandInfoConfigure landInfoConfigure = StaticConfigure.Land.InfoDict[4];
				await ShowSendPlayer(model.PlayerId, landInfoConfigure.Perform1, landInfoConfigure.Perform2, model.NodeId, model.FrontIds);
			}
		}
	}

	public async UniTask DealMonsterPursuit(Action action)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(action.PlayerId);
		if (playerDataById.CharacterInst != null)
		{
			await playerDataById.CharacterInst.SwitchCamera();
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			RepeatedField<long> vailPursuitMonster = GetVailPursuitMonster();
			await SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowMonsterPursuit(action, vailPursuitMonster);
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(action.PlayerId, 11000);
		}
	}

	private RepeatedField<long> GetVailPursuitMonster()
	{
		RepeatedField<long> repeatedField = new RepeatedField<long>();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData == null)
		{
			return repeatedField;
		}
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (playerData.characterType == CharacterType.Monster && !playerData.Property.NotSelect.Value && playerData.Property.HP.Value > 0 && !(playerData.CharacterInst == null) && playerData.CharacterInst.standLand.LandType != LandType.Hospital && selfPlayerData.player.TeamId != playerData.player.TeamId)
			{
				repeatedField.Add(playerData.player.Id);
			}
		}
		return repeatedField;
	}

	public async UniTask DealAskReviveTeammate(Action _action)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (playerDataById.CharacterInst != null)
		{
			await playerDataById.CharacterInst.SwitchCamera();
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11000);
		}
		else
		{
			(await SimpleSingletonProvider<UIManager>.inst.landFillingStation.ShowLand()).ShowAskReviveTeammate(_action);
		}
	}

	public RPCAsyncResult RequestAskReviveTeammateC2S(long _sn, bool _IsRevive)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.AskReviveTeammateC2S.AskReviveTeammateC2SCall(new AskReviveTeammateC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			IsRevive = _IsRevive
		});
	}

	private async UniTask OnAskReviveTeammateS2CServerCallBack(AskReviveTeammateS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0)
		{
			if (SimpleSingletonProvider<UIManager>.inst.landFillingStation.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.landFillingStation.Hide();
			}
			SimpleSingletonProvider<UIManager>.inst.tips.HideThinkingTip(model.PlayerId);
			await UniTask.CompletedTask;
		}
	}

	public async UniTask DealAskPurchaseRelic(Action _action)
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (playerData.CharacterInst != null)
		{
			await playerData.CharacterInst.SwitchCamera();
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11000);
		}
		else
		{
			await SimpleSingletonProvider<UIManager>.inst.landRelic.ShowPurchase(playerData, _action);
		}
	}

	public RPCAsyncResult RequestBuyRelicC2S(long _sn, int _type, bool _Exit = true)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.BuyRelicC2S.BuyRelicC2SCall(new BuyRelicC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Select = _type,
			Exit = _Exit
		});
	}

	private async UniTask OnBuyRelicS2CServerCallBack(BuyRelicS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.landRelic.Hide();
			await UniTask.CompletedTask;
		}
	}

	public async UniTask DealAskSelectMechanism(Action _action)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (playerDataById.CharacterInst != null)
		{
			await playerDataById.CharacterInst.SwitchCamera();
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestSelectMechanismC2S(_action.Sn, select: true);
		}
	}

	public RPCAsyncResult RequestSelectMechanismC2S(long _sn, bool select)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.SelectMechanismC2S.SelectMechanismC2SCall(new SelectMechanismC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Select = select
		});
	}

	private async UniTask OnSelectMechanismS2CServerCallBack(SelectMechanismS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.landFillingStation.Hide();
			await UniTask.CompletedTask;
		}
	}

	public async UniTask DealSelectEventC2S(Action action)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(action.PlayerId);
		if (playerDataById.CharacterInst != null)
		{
			await playerDataById.CharacterInst.SwitchCamera();
		}
		await SimpleSingletonProvider<UIManager>.inst.landEvent.ShowSkill10202(action);
	}

	public RPCAsyncResult RequestSelectEventC2S(SelectEventC2S message)
	{
		message.Info.UseTime = OperationTimer.GetExtraTime();
		return MonoSingletonProvider<NetManager>.inst.RPC.SelectEventC2S.SelectEventC2SCall(message);
	}

	private async UniTask OnSelectEventS2CServerCallBack(SelectEventS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public async UniTask DealAskVendorBuyCard(Action _action)
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (playerData.CharacterInst != null)
		{
			await playerData.CharacterInst.SwitchCamera();
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11000);
		}
		else
		{
			await SimpleSingletonProvider<UIManager>.inst.landVendor.ShowVendorBuyCard(playerData, _action);
		}
	}

	public RPCAsyncResult RequestVendorBuyCardC2S(long _sn, bool isBuy)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		return MonoSingletonProvider<NetManager>.inst.RPC.VendorBuyCardC2S.VendorBuyCardC2SCall(new VendorBuyCardC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			IsBuy = isBuy
		});
	}

	private async UniTask OnVendorBuyCardS2CServerCallBack(VendorBuyCardS2C model, int errId, bool isDispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.landVendor.Hide();
			await UniTask.CompletedTask;
		}
	}

	public void DealLand_EventTigger(Action action)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestTriggerEvent(action.Sn);
		}
	}
}
