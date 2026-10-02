using System;
using System.Collections.Generic;
using Core;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using UnityTimer;
using party.protocol;

namespace GameLogic;

public static class OperationTimer
{
	private static Dictionary<long, Timer> timerDict;

	private static float operationTime;

	private static int operationTimeLimitBase;

	private static int operationTimeLimitExtra;

	private static int operationCardTimeLimit;

	private static RepeatedField<int> punishmentTimeLimit;

	private static float downtime;

	private static bool _AFK;

	private static int _AFKTime;

	private static Timer ShowPlayerTimer;

	private static void ReadyTimer()
	{
		if (timerDict == null)
		{
			_AFK = false;
			timerDict = new Dictionary<long, Timer>();
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (StaticConfigure.ChoosingTimeLimit.InfoDict.TryGetValue(curRoomInfo.TimePlan, out var value))
			{
				operationTimeLimitBase = value.OtherTimeLimit;
				operationCardTimeLimit = value.ChoosingCardTimeLimit;
				operationTimeLimitExtra = value.ExtraTime;
				punishmentTimeLimit = value.PunishmentTimeLimit;
			}
		}
	}

	public static int GetExtraTime()
	{
		return operationTimeLimitExtra;
	}

	public static Timer GetOperateTimer(long actionSn)
	{
		return timerDict?.GetValueOrDefault(actionSn);
	}

	public static void CancelOperatTimer(long actionSn)
	{
		if (timerDict != null && timerDict.TryGetValue(actionSn, out var value))
		{
			if (!value.isCompleted)
			{
				value.Cancel();
			}
			timerDict.Remove(actionSn);
		}
	}

	public static Timer ActionDownTime(long actionSn, int actionProtocolId, Action onComplete = null, Action onCancel = null, Action<float, float> onUpdate = null, bool operateCard = false, bool showTimerToPlayer = true, bool showWarning = true)
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst?.room?.curRoomInfo;
		if (roomInfo == null)
		{
			return null;
		}
		if (roomInfo.IsCampaign() || roomInfo.MapType == 10)
		{
			return null;
		}
		ReadyTimer();
		if (!timerDict.ContainsKey(actionSn))
		{
			downtime = 0f;
			operationTime = 0f;
			if (_AFK)
			{
				if (AdjustAFK())
				{
					return null;
				}
				int val = ((punishmentTimeLimit.Count > _AFKTime - 1) ? (_AFKTime - 1) : (punishmentTimeLimit.Count - 1));
				operationTime = punishmentTimeLimit[Math.Max(val, 0)];
			}
			else
			{
				_AFKTime = 0;
				if (operateCard)
				{
					operationTime = operationCardTimeLimit + operationTimeLimitExtra;
				}
				else
				{
					operationTime = operationTimeLimitBase + operationTimeLimitExtra;
				}
			}
			TryShowTimerToPlayer(showTimerToPlayer, (int)operationTime);
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			string headUrl = ((selfPlayerData != null) ? selfPlayerData.GetCharacterHeadUrl() : "");
			Timer val2 = Timer.Register(0f, operationTime, (Action)delegate
			{
				onComplete?.Invoke();
				_AFK = true;
				TryShowTimerToPlayer(showTimerToPlayer, 0);
				SimpleSingletonProvider<UIManager>.inst.operateTime.UpdateOperationProgress(0, 0f, headUrl, showWarning);
				timerDict.Remove(actionSn);
				PostOperateTimeOverRecord(actionProtocolId);
			}, (Action)null, (Action)delegate
			{
				_AFK = false;
				onCancel?.Invoke();
				TryShowTimerToPlayer(showTimerToPlayer, 0);
				SimpleSingletonProvider<UIManager>.inst.operateTime.UpdateOperationProgress(0, 0f, headUrl, showWarning);
				timerDict.Remove(actionSn);
			}, (Action)null, (Action)null, (Action<float>)delegate(float _time)
			{
				downtime = (int)_time;
				onUpdate?.Invoke(_time, operationTime);
				SimpleSingletonProvider<UIManager>.inst.operateTime.UpdateOperationProgress((int)operationTime, _time, headUrl, showWarning);
			}, (Action)null, false, -1f, false, (GameObject)null);
			timerDict.Add(actionSn, val2);
			SimpleSingletonProvider<GameLogicManager>.inst?.battle?.signal?.curPlayerOperate?.Dispatch();
			return val2;
		}
		return null;
	}

	private static void PostOperateTimeOverRecord(int actionProtocolId)
	{
		SimpleSingletonProvider<GameLogicManager>.inst?.room?.RequestActionOverTimeLogC2S(actionProtocolId);
	}

	private static bool AdjustAFK()
	{
		if (!GMConfig.AFKCheck)
		{
			return false;
		}
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room != null && room.IsInRoom && !room.curRoomInfo.IsSingleGameModel())
		{
			_AFKTime++;
			int afkLimit = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.afkLimit;
			if (_AFKTime > afkLimit)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.room.GiveUpGame(null, ExitRoomC2S.Types.ForceExitType.TimeOut);
				return true;
			}
		}
		return false;
	}

	private static void TryShowTimerToPlayer(bool showTimerToPlayer, int time)
	{
		if (showTimerToPlayer)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.action.RequestTimeWastingC2S(time);
		}
	}

	public static void Dispose()
	{
		if (timerDict != null && timerDict.Count > 0)
		{
			foreach (KeyValuePair<long, Timer> item in timerDict)
			{
				item.Value.Pause();
			}
		}
		timerDict = null;
	}

	public static void SetAtkCount(int count)
	{
		_AFKTime = count;
	}

	public static void CancelShowPlayerTimer()
	{
		Timer showPlayerTimer = ShowPlayerTimer;
		if (showPlayerTimer != null)
		{
			showPlayerTimer.Cancel();
		}
	}

	public static void StartShowPlayerTimer(int time, string headUrl)
	{
		Timer showPlayerTimer = ShowPlayerTimer;
		if (showPlayerTimer != null)
		{
			showPlayerTimer.Cancel();
		}
		ShowPlayerTimer = Timer.Register(0f, (float)time, (Action)delegate
		{
			SimpleSingletonProvider<UIManager>.inst.operateTime.UpdateShowProgress(0, 0f, headUrl);
		}, (Action)null, (Action)delegate
		{
			SimpleSingletonProvider<UIManager>.inst.operateTime.UpdateShowProgress(0, 0f, headUrl);
		}, (Action)null, (Action)null, (Action<float>)delegate(float seconds)
		{
			SimpleSingletonProvider<UIManager>.inst.operateTime.UpdateShowProgress(time, seconds, headUrl);
		}, (Action)null, false, -1f, false, (GameObject)null);
	}
}
