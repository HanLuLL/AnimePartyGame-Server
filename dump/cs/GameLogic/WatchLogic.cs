using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public class WatchLogic : IRPCSync
{
	public ReactiveProperty<long> SubscribePlayerId = new ReactiveProperty<long>();

	public ReactiveProperty<bool> IsFollow = new ReactiveProperty<bool>();

	public void UpdateSubscribePlayer(long playerID)
	{
		SubscribePlayerId.Value = playerID;
	}

	public void SwitchFollow(bool isFollow)
	{
		IsFollow.Value = isFollow;
	}

	public bool PlayerIsWatcher()
	{
		AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
		if (account == null)
		{
			return false;
		}
		return IsWatcher(account.GetPlayerID());
	}

	public bool IsWatcher(long playerId)
	{
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst?.room?.roomController;
		if (roomController != null)
		{
			RoomStateType roomStateType = roomController.roomStateType;
			if (roomStateType == RoomStateType.WAIT || roomStateType == RoomStateType.RUNNING || roomStateType == RoomStateType.SETTLEMENT)
			{
				RepeatedField<Player> repeatedField = roomController.localRoom?.WatchPlayers;
				if (repeatedField == null)
				{
					return false;
				}
				foreach (Player item in repeatedField)
				{
					if (item.Id == playerId)
					{
						return true;
					}
				}
				return false;
			}
		}
		return false;
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.WatchJoinRoomS2C.OnWatchJoinRoomS2CServerCallBackAsync = OnWatchJoinRoomS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.WatchRefreshRoomStateS2C.OnWatchRefreshRoomStateS2CServerCallBackAsync = OnWatchRefreshRoomStateS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.WatchExitRoomS2C.OnWatchExitRoomS2CServerCallBackAsync = OnWatchExitRoomS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.WatchJoinRoomS2C.OnWatchJoinRoomS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.WatchRefreshRoomStateS2C.OnWatchRefreshRoomStateS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.WatchExitRoomS2C.OnWatchExitRoomS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestWatchJoinRoomC2S(string _watchCode)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.WatchJoinRoomC2S.WatchJoinRoomC2SCall(new WatchJoinRoomC2S
		{
			WatchCode = _watchCode
		});
	}

	private async UniTask OnWatchJoinRoomS2CServerCallBack(WatchJoinRoomS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		foreach (Player player in model.Players)
		{
			player.Hero.Affirm = true;
		}
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		roomController.CreateRoom(new Room
		{
			Id = model.RoomId,
			Players = { (IEnumerable<Player>)model.Players },
			MapId = model.MapId,
			State = Room.Types.State.Running,
			MapType = model.MapType,
			Box = model.Box,
			MapIndex = model.MapIndex,
			Difficulty = model.Difficulty,
			MapDifficultyId = model.MapDifficultyId,
			RoomServerId = model.RoomServerId,
			RoomTerms = { (IEnumerable<int>)model.RoomTermIds }
		});
		roomController.localRoom.StartTime = model.StartTime;
		roomController.localRoom.WatchCount = model.WatchCount;
		SubscribePlayerId.JustSetValue(SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GetPlayerBySlot(0).Id);
		IsFollow.JustSetValue(value: true);
		await UniTask.CompletedTask;
	}

	public async void RequestWatchRefreshRoomStateC2S()
	{
		await SimpleSingletonProvider<InternalAssetManager>.inst.PreLoadBattleAsset();
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		await MonoSingletonProvider<NetManager>.inst.RPC.WatchRefreshRoomStateC2S.WatchRefreshRoomStateC2SCall(new WatchRefreshRoomStateC2S
		{
			RoomId = curRoomInfo.Id,
			RoomServerId = curRoomInfo.info.RoomServerId
		});
	}

	private async UniTask OnWatchRefreshRoomStateS2CServerCallBack(WatchRefreshRoomStateS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			await SimpleSingletonProvider<InternalAssetManager>.inst.Dispose();
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.lockExpressionPlayerID.Clear();
		SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.SwitchRoomState(model.Room, RoomStateType.RUNNING);
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleStart(initialization: false);
	}

	public RPCAsyncResult RequestWatchExitRoomC2S()
	{
		SimpleSingletonProvider<UIManager>.inst.loadingTip.ShowNotWait(_delayStaus: false, 10023);
		return MonoSingletonProvider<NetManager>.inst.RPC.WatchExitRoomC2S.WatchExitRoomC2SCall(new WatchExitRoomC2S());
	}

	private async UniTask OnWatchExitRoomS2CServerCallBack(WatchExitRoomS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<UIManager>.inst.loadingTip.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
		}
		if (errid != 0)
		{
			return;
		}
		if (isdispatch)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING)
			{
				await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(1005.GetLocal(UIStringType.Spectate), delegate
				{
					MonoSingletonProvider<NetManager>.inst.RPC.ClearRPC();
					SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
					SimpleSingletonProvider<GameLogicManager>.inst.battleResult.FinishGame();
				});
			}
		}
		else
		{
			MonoSingletonProvider<NetManager>.inst.RPC.ClearRPC();
			SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
			SimpleSingletonProvider<GameLogicManager>.inst.battleResult.FinishGame();
		}
	}
}
