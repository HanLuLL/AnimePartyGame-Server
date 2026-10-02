using System.Collections.Generic;
using Core;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class RoomController
{
	private RoomInfo _LocalRoom;

	private Room ServerRoom;

	public RoomStateType roomStateType;

	public readonly List<ApplySlotData> ChangeSlotData = new List<ApplySlotData>();

	public RoomInfo localRoom => _LocalRoom;

	public bool RoomValid
	{
		get
		{
			if (_LocalRoom != null)
			{
				return roomStateType != RoomStateType.NONE;
			}
			return false;
		}
	}

	public bool NeedUnloadResource
	{
		get
		{
			if (_LocalRoom != null)
			{
				RoomStateType roomStateType = this.roomStateType;
				return roomStateType == RoomStateType.READY || roomStateType == RoomStateType.RUNNING;
			}
			return false;
		}
	}

	public int initRound => ServerRoom.Round;

	public void CreateRoom(Room _serverRoom)
	{
		if (_serverRoom == null)
		{
			Debug.LogError("当前玩家收到来自服务器的房间数据为null");
		}
		ServerRoom = _serverRoom;
		_LocalRoom = new RoomInfo(_serverRoom);
		roomStateType = RoomStateType.WAIT;
		ChangeSlotData.Clear();
	}

	public void UpdateRoomSetting(Room _serverRoom)
	{
		if (_serverRoom == null)
		{
			Debug.LogError("当前玩家收到来自服务器的房间数据为null");
		}
		ServerRoom = _serverRoom;
		if (_serverRoom?.Players != null && _serverRoom.Difficulty != _LocalRoom.Difficulty)
		{
			_LocalRoom.UpdateRoomPlayer(_serverRoom.Players);
			if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_serverRoom.MasterId))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1083);
			}
			SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.Dispatch();
		}
		_LocalRoom.UpdateBaseInfo(_serverRoom);
		SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomSettingRefresh.Dispatch();
	}

	public void UpdateRoomByJoin(Room _serverRoom)
	{
		if (_serverRoom == null)
		{
			Debug.LogError("当前玩家收到来自服务器的房间数据为null");
		}
		ServerRoom = _serverRoom;
		_LocalRoom.UpdateRoomPlayer(ServerRoom.Players);
	}

	public async void UpdateRoomByExit(long masterId, long playerId, bool Dissolve)
	{
		_LocalRoom.UpdateMasterId(masterId);
		_LocalRoom.RemovePlayerInfo(playerId);
		if (roomStateType == RoomStateType.WAIT)
		{
			if (Dissolve)
			{
				roomStateType = RoomStateType.NONE;
				await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.RoomList, null, UIPanelType.MatchEntrance);
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.room.signal.roomPlayerChange.Dispatch();
			}
		}
		else if (roomStateType == RoomStateType.READY)
		{
			if (!_LocalRoom.IsMatchRoom || Dissolve || SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
			{
				await SimpleSingletonProvider<InternalAssetManager>.inst.Dispose();
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.room.RequestExitRoomC2S(ExitRoomC2S.Types.ForceExitType.None);
			}
		}
		else
		{
			if (roomStateType != RoomStateType.CHOICE)
			{
				return;
			}
			SimpleSingletonProvider<CharacterAssetManager>.inst.Dispose();
			if (_LocalRoom.IsMatchRoom)
			{
				if (!Dissolve && !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerId))
				{
					roomStateType = RoomStateType.NONE;
					SimpleSingletonProvider<GameLogicManager>.inst.room.RequestExitRoomC2S(ExitRoomC2S.Types.ForceExitType.None).OnFinishedOnly.AddOnce(ReturnMatchTeam);
				}
				return;
			}
			long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
			roomStateType = RoomStateType.NONE;
			if (_LocalRoom.RoomActorDict.ContainsKey(playerID))
			{
				SimpleSingletonProvider<GameLogicManager>.inst.room.RequestSyncRoomC2S(_LocalRoom.Id);
				return;
			}
			await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.RoomList, null, UIPanelType.MatchEntrance);
		}
	}

	private async void ReturnMatchTeam()
	{
		await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.MatchEntrance, null);
		SimpleSingletonProvider<GameLogicManager>.inst.match.RequestRefreshMatchTeamInfoC2S();
	}

	public void SwitchRoomState(Room _serverRoom, RoomStateType stateType)
	{
		if (_serverRoom != null && _LocalRoom != null)
		{
			ServerRoom = _serverRoom;
			_LocalRoom.UpdateBaseInfo(_serverRoom);
			_LocalRoom.UpdateRoomPlayer(_serverRoom.Players);
			_LocalRoom.UpdateRoomMonster(_serverRoom.Monsters);
			_LocalRoom.UpdateBattleCache(_serverRoom);
			_LocalRoom.UpdateProgress(_serverRoom);
			_LocalRoom.UpdateGameModeData(_serverRoom);
		}
		if (stateType != roomStateType)
		{
			roomStateType = stateType;
			switch (stateType)
			{
			case RoomStateType.READY:
				ChangeSlotData.Clear();
				SimpleSingletonProvider<GameLogicManager>.inst.room.signal.applyChangeSlot.Dispatch();
				break;
			case RoomStateType.CHOICE:
				ChangeSlotData.Clear();
				break;
			}
		}
	}

	public bool IsBattle()
	{
		if (roomStateType == RoomStateType.RUNNING)
		{
			return localRoom != null;
		}
		return false;
	}

	public void Dispose()
	{
		if (_LocalRoom != null)
		{
			_LocalRoom.Dispose();
		}
		ClearRoom();
	}

	public void ClearRoom()
	{
		_LocalRoom = null;
	}
}
