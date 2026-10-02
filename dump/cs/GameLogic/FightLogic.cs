using Core;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic.Replay;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class FightLogic : IRPCSync
{
	public FightSignal signal = new FightSignal();

	public FightType fightType;

	public bool fightStatus;

	public BattleFightData battleFightData;

	public BattleRole attackData => battleFightData?.attackerInfo;

	public BattleRole defendData => battleFightData?.defenderInfo;

	public void UpdateFightData(Battle _battleInfo, bool _isReConnect = false)
	{
		if (_battleInfo != null)
		{
			if (battleFightData == null)
			{
				battleFightData = new BattleFightData();
			}
			battleFightData.UpdateData(_battleInfo, _isReConnect);
		}
	}

	public async UniTask AskFight(Action action)
	{
		fightType = FightType.FIGHT_NOTIFY;
		if (OperationTimer.GetOperateTimer(action.Sn) != null)
		{
			return;
		}
		AskBattleC2S _battleData = ByteBuf.ReadObject<AskBattleC2S>(action.Data.ToByteArray());
		FightWindow fightWindow = await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin();
		if (_battleData.FightBack)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
			{
				SimpleSingletonProvider<GameLogicManager>.inst.fight.RequestAskBattleC2S(action.Sn, is_battle: true);
			}
			return;
		}
		fightWindow.OpenChallengeWin(action.PlayerId, _battleData.AskPlayerId, action.Sn);
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(action.PlayerId).characterType == CharacterType.Monster)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(600);
			return;
		}
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst?.replay?.Session;
		if (replaySession != null && replaySession.IsReplay)
		{
			int millisecondsDelay = Mathf.Max(1, (int)(1000f / replaySession.PlaySpeedMultiplier));
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(millisecondsDelay);
		}
	}

	public async UniTask ReadyFightUseCard(Action action)
	{
		FightWindow fightWindow = await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin();
		if (fightType != FightType.FIGHT_CARD_NOTIFY && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			fightWindow.InitPKCard(action.PlayerId);
			fightType = FightType.FIGHT_CARD_NOTIFY;
		}
		fightWindow.RefreshPKCard(action.PlayerId, action.Sn);
	}

	public async UniTask ReadyFightThrowDice(Action action)
	{
		if (fightType != FightType.FIGHT_ATTACK_NOTIFY)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(500);
			BattleSceneController.inst.directorManager.CloseReadyLabel();
			BattleSceneController.inst.directorManager.PlayDetachDirect();
			fightType = FightType.FIGHT_ATTACK_NOTIFY;
			(await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin()).RefreshThrowDice(action.PlayerId, action.Sn);
		}
	}

	public async UniTask ReadyFightChoice(Action action)
	{
		if (fightType != FightType.FIGHT_DEFEND_NOTIFY)
		{
			BattleChoiceC2S _ChoiceData = ByteBuf.ReadObject<BattleChoiceC2S>(action.Data.ToByteArray());
			(await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin()).RefreshDefendReadyChoice(action.Sn, action.PlayerId, _ChoiceData.NoDodge);
			if (battleFightData != null && battleFightData.isReConnect)
			{
				int point = battleFightData.attackerInfo.Point;
				BattleSceneController.inst.directorManager.PlayDetachDirect();
				fightType = FightType.FIGHT_DEFEND_NOTIFY;
				BattleSceneController.inst.directorManager.attacker._UI.RefreshPoint_Attacker(1, point);
				signal.dodgeShow.Dispatch(point);
			}
		}
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.BattleS2C.OnBattleS2CServerCallBackAsync = OnBattleS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.AskBattleS2C.OnAskBattleS2CServerCallBackAsync = OnAskBattleS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BattleUseCardS2C.OnBattleUseCardS2CServerCallBackAsync = OnBattleUseCardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BattleThrowDiceS2C.OnBattleThrowDiceS2CServerCallBackAsync = OnBattleThrowDiceS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BattleChoiceS2C.OnBattleChoiceS2CServerCallBackAsync = OnBattleChoiceS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.BattleS2C.OnBattleS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.AskBattleS2C.OnAskBattleS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattleUseCardS2C.OnBattleUseCardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattleThrowDiceS2C.OnBattleThrowDiceS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BattleChoiceS2C.OnBattleChoiceS2CServerCallBackAsync = null;
	}

	private async UniTask OnBattleS2CServerCallBack(BattleS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errId != 0)
		{
			return;
		}
		UpdateFightData(model.Battle);
		if (!model.Battle.IsEnd)
		{
			if (!SimpleSingletonProvider<UIManager>.inst.Fight.isShowing)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.card.signal.recycleCard.Dispatch();
				await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin();
				await SimpleSingletonProvider<UIManager>.inst.Fight.ReadyFight(model.Battle.Attacker.PlayerId, model.Battle.Defender.PlayerId);
			}
			SimpleSingletonProvider<UIManager>.inst.Fight.RefreshCardWinData();
		}
		else
		{
			await BattleSceneController.inst.directorManager.PlayBattleResult();
			fightType = FightType.FIGHT_RESULT;
		}
	}

	public RPCAsyncResult RequestAskBattleC2S(long actionSn, bool is_battle)
	{
		OperationTimer.CancelOperatTimer(actionSn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(actionSn);
		return MonoSingletonProvider<NetManager>.inst.RPC.AskBattleC2S.AskBattleC2SCall(new AskBattleC2S
		{
			Info = new ActionInfo
			{
				Sn = actionSn,
				UseTime = OperationTimer.GetExtraTime()
			},
			IsBattle = is_battle
		});
	}

	private async UniTask OnAskBattleS2CServerCallBack(AskBattleS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errId != 0)
		{
			return;
		}
		if (model.IsBattle)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.card.signal.recycleCard.Dispatch();
			if (!SimpleSingletonProvider<UIManager>.inst.Fight.isShowing)
			{
				await SimpleSingletonProvider<UIManager>.inst.Fight.ShowWin();
			}
			await SimpleSingletonProvider<UIManager>.inst.Fight.ReadyFight(model.PlayerId, model.AskPlayerId);
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.Fight.HideImmediately();
		}
	}

	public RPCAsyncResult RequestBattleUseCardC2S(long actionSn, int cardId)
	{
		OperationTimer.CancelOperatTimer(actionSn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(actionSn);
		return MonoSingletonProvider<NetManager>.inst.RPC.BattleUseCardC2S.BattleUseCardC2SCall(new BattleUseCardC2S
		{
			Info = new ActionInfo
			{
				Sn = actionSn,
				UseTime = OperationTimer.GetExtraTime()
			},
			CardUid = cardId
		});
	}

	private async UniTask OnBattleUseCardS2CServerCallBack(BattleUseCardS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			if (model.CardId == 0)
			{
				BattleSceneController.inst.directorManager.ShowReadyLabel(model.PlayerId);
			}
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestBattleThrowDiceC2S(long actionSn)
	{
		OperationTimer.CancelOperatTimer(actionSn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(actionSn);
		return MonoSingletonProvider<NetManager>.inst.RPC.BattleThrowDiceC2S.BattleThrowDiceC2SCall(new BattleThrowDiceC2S
		{
			Info = new ActionInfo
			{
				Sn = actionSn,
				UseTime = OperationTimer.GetExtraTime()
			},
			DevPoint = GMConfig.dev_AttackerPoint
		});
	}

	private async UniTask OnBattleThrowDiceS2CServerCallBack(BattleThrowDiceS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.ATK, model.PlayerId);
			await BattleSceneController.inst.directorManager.attacker._UI.RefreshDice_Attacker(1, model.Val);
			signal.dodgeShow.Dispatch(model.Val);
		}
	}

	public RPCAsyncResult RequestBattleChoiceC2S(long actionSn, bool choice)
	{
		OperationTimer.CancelOperatTimer(actionSn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(actionSn);
		return MonoSingletonProvider<NetManager>.inst.RPC.BattleChoiceC2S.BattleChoiceC2SCall(new BattleChoiceC2S
		{
			Info = new ActionInfo
			{
				Sn = actionSn,
				UseTime = OperationTimer.GetExtraTime()
			},
			DevPoint = GMConfig.dev_DefenderPoint,
			Dodge = choice
		});
	}

	private async UniTask OnBattleChoiceS2CServerCallBack(BattleChoiceS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errId == 0)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.DEF, model.PlayerId);
			SimpleSingletonProvider<UIManager>.inst.Fight.ShowChoiceResult(model.Dodge);
			await BattleSceneController.inst.directorManager.RefreshDice_Defender(0, model.Val, model.Dodge, model.ExistFightBack);
			BattleSceneController.inst.directorManager.PlayApproach();
			await BattleSceneController.inst.directorManager.attacker.PlayMove();
		}
	}
}
