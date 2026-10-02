using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic.Replay;

public class ReplaySession
{
	private const string LOG_PREFIX = "[ReplaySession]";

	public readonly ReplaySignal Signal = new ReplaySignal();

	private ReplayLoadResult _loadResult;

	private int _generation;

	public ReplayState State { get; private set; }

	public ReplayPackage CurrentPackage { get; private set; }

	public RunningGameS2C CurrentRunningGame { get; private set; }

	public GameFinishS2C CurrentGameFinish { get; private set; }

	public ReplayPlayer CurrentPlayer { get; private set; }

	public RoomInfo CurrentRoom => SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;

	public bool IsReplay => State != ReplayState.None;

	public bool IsPlaying => State == ReplayState.Playing;

	public bool IsPaused => State == ReplayState.Paused;

	public bool CanSendC2S => !IsReplay;

	public int PlaybackStartFrameIndex { get; private set; } = -1;

	internal int Generation => _generation;

	public float PlaySpeedMultiplier { get; private set; } = 1f;

	public int RoundCount => (CurrentPackage?.Rounds?.Count).GetValueOrDefault();

	public void SetPlaySpeed(float multiplier)
	{
		if (!Mathf.Approximately(PlaySpeedMultiplier, multiplier))
		{
			PlaySpeedMultiplier = multiplier;
			Signal.speedChanged.Dispatch();
		}
	}

	public void ResetPlaySpeed()
	{
		PlaySpeedMultiplier = 1f;
		Signal.speedChanged.Dispatch();
	}

	public ReplayRoundNode GetRound(int roundIndex)
	{
		List<ReplayRoundNode> list = CurrentPackage?.Rounds;
		if (list == null || roundIndex < 0 || roundIndex >= list.Count)
		{
			return null;
		}
		return list[roundIndex];
	}

	private void SetState(ReplayState newState)
	{
		if (State != newState)
		{
			ReplayState state = State;
			State = newState;
			Signal.stateChanged.Dispatch(state, newState);
		}
	}

	public bool LoadBytesAndRebuildRoom(byte[] bytes, RoomStateType targetState = RoomStateType.RUNNING)
	{
		if (LoadBytes(bytes))
		{
			return RebuildLoadedRoom(targetState);
		}
		return false;
	}

	public bool LoadBytes(byte[] bytes)
	{
		BeginNewSession();
		SetState(ReplayState.Loading);
		ReplayLoadResult replayLoadResult = ReplayLoader.LoadServerPackStream(bytes);
		if (replayLoadResult == null || !replayLoadResult.Success)
		{
			MarkError("LoadFailed", "Replay load failed. code=" + replayLoadResult?.ErrorCode + ", message=" + replayLoadResult?.ErrorMessage);
			return false;
		}
		_loadResult = replayLoadResult;
		CurrentPackage = replayLoadResult.Package;
		CurrentGameFinish = replayLoadResult.GameFinish;
		CurrentRunningGame = replayLoadResult.RunningGame;
		PlaybackStartFrameIndex = replayLoadResult.PlaybackStartFrameIndex;
		return true;
	}

	public bool RebuildLoadedRoom(RoomStateType targetState = RoomStateType.RUNNING)
	{
		if (_loadResult == null || !_loadResult.Success)
		{
			MarkError("LoadResultMissing", "No successful replay load result to rebuild room. Call LoadLocalFile/LoadBytes first.");
			return false;
		}
		return RebuildRoom(_loadResult, targetState);
	}

	public bool RebuildRoom(ReplayLoadResult loadResult, RoomStateType targetState = RoomStateType.RUNNING)
	{
		if (loadResult == null)
		{
			MarkError("LoadResultNull", "Replay load result is null.");
			return false;
		}
		if (!loadResult.Success)
		{
			MarkError("LoadResultFailed", "Replay load result is failed. code=" + loadResult.ErrorCode + ", message=" + loadResult.ErrorMessage);
			return false;
		}
		return RebuildRoom(loadResult.RunningGame, targetState, loadResult.Package, loadResult);
	}

	public bool RebuildRoom(RunningGameS2C runningGame, RoomStateType targetState = RoomStateType.RUNNING, ReplayPackage package = null, ReplayLoadResult loadResult = null)
	{
		SetState(ReplayState.PreparingScene);
		if (runningGame == null)
		{
			MarkError("RunningGameNull", "Replay RunningGameS2C is null.");
			return false;
		}
		if (runningGame.Room == null)
		{
			MarkError("RunningGameRoomNull", "Replay RunningGameS2C.Room is null.");
			return false;
		}
		if (runningGame.Room.State != Room.Types.State.Running)
		{
			MarkError("RunningGameRoomNotRunning", $"Replay RunningGameS2C.Room.State must be Running, actual={runningGame.Room.State}.");
			return false;
		}
		if (!EnsureGameLogicReady())
		{
			MarkError("GameLogicNotReady", "GameLogicManager or RoomLogic is not ready.");
			return false;
		}
		Room room = CloneRoomForReplay(runningGame.Room, targetState);
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		room.WatchPlayers.Add(playerInfo);
		roomController.Dispose();
		roomController.CreateRoom(room);
		roomController.SwitchRoomState(room, targetState);
		SimpleSingletonProvider<GameLogicManager>.inst.watch.SubscribePlayerId.JustSetValue(roomController.localRoom.GetPlayerBySlot(0).Id);
		CurrentPackage = package;
		CurrentRunningGame = runningGame;
		CurrentGameFinish = loadResult?.GameFinish;
		InvalidateCurrentPlayer();
		PlaybackStartFrameIndex = loadResult?.PlaybackStartFrameIndex ?? (-1);
		FillTurnNodeDisplayNames();
		_ = roomController.localRoom;
		return true;
	}

	public bool RebuildRoomFromSnapshot(ReplaySnapshotS2C snapshot, RoomStateType targetState = RoomStateType.RUNNING)
	{
		if (snapshot?.Room == null)
		{
			MarkError("SnapshotRoomNull", "ReplaySnapshotS2C.Room is null.");
			return false;
		}
		if (!EnsureGameLogicReady())
		{
			MarkError("GameLogicNotReady", "GameLogicManager or RoomLogic is not ready.");
			return false;
		}
		Room room = CloneRoomForReplay(snapshot.Room, targetState);
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		room.WatchPlayers.Add(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo());
		roomController.Dispose();
		roomController.CreateRoom(room);
		roomController.SwitchRoomState(room, targetState);
		SimpleSingletonProvider<GameLogicManager>.inst.watch.SubscribePlayerId.JustSetValue(roomController.localRoom.GetPlayerBySlot(0).Id);
		return true;
	}

	public bool BuildSettlementRoom()
	{
		if (CurrentPackage?.TurnNodes == null || CurrentPackage.TurnNodes.Count == 0)
		{
			MarkError("TurnNodesEmpty", "CurrentPackage.TurnNodes is empty; cannot build settlement room.");
			return false;
		}
		List<ReplayTurnNode> turnNodes = CurrentPackage.TurnNodes;
		ReplaySnapshotS2C snapshot = turnNodes[turnNodes.Count - 1].Snapshot;
		if (snapshot?.Room == null)
		{
			MarkError("SnapshotRoomNull", "Tail ReplaySnapshotS2C.Room is null; cannot build settlement room.");
			return false;
		}
		if (!EnsureGameLogicReady())
		{
			MarkError("GameLogicNotReady", "GameLogicManager or RoomLogic is not ready.");
			return false;
		}
		Room serverRoom = CloneRoomForReplay(snapshot.Room, RoomStateType.NONE);
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		roomController.Dispose();
		roomController.CreateRoom(serverRoom);
		roomController.SwitchRoomState(serverRoom, RoomStateType.NONE);
		return true;
	}

	public void Clear()
	{
		InvalidateCurrentPlayer();
		SetState(ReplayState.None);
		CurrentPackage = null;
		CurrentRunningGame = null;
		CurrentGameFinish = null;
		PlaybackStartFrameIndex = -1;
		_loadResult = null;
		SimpleSingletonProvider<GameLogicManager>.inst?.room?.roomController?.Dispose();
	}

	public UniTask<bool> PlayCurrentPackage(int startFrameIndex = -1)
	{
		if (State == ReplayState.None)
		{
			return UniTask.FromResult(value: false);
		}
		if (CurrentPackage?.Frames == null || CurrentPackage.Frames.Count == 0)
		{
			MarkError("CurrentPackageEmpty", "Replay current package is empty.");
			return UniTask.FromResult(value: false);
		}
		if (startFrameIndex < 0)
		{
			startFrameIndex = ((PlaybackStartFrameIndex >= 0) ? PlaybackStartFrameIndex : 0);
		}
		ReplayPlayer currentPlayer = CurrentPlayer;
		ReplayPlayer replayPlayer = (CurrentPlayer = new ReplayPlayer(this, _generation));
		currentPlayer?.Stop();
		return replayPlayer.PlayAsync(CurrentPackage, startFrameIndex);
	}

	public void PausePlayback()
	{
		CurrentPlayer?.Pause();
	}

	public void ResumePlayback()
	{
		CurrentPlayer?.Resume();
	}

	public void StopPlayback()
	{
		if (IsReplay && SimpleSingletonProvider<DelaySignalManager>.hasInstance)
		{
			SimpleSingletonProvider<DelaySignalManager>.inst.CancelAllTask();
		}
		CurrentPlayer?.Stop();
	}

	internal bool IsCurrentPlayer(ReplayPlayer player, int generation)
	{
		if (generation == _generation)
		{
			return CurrentPlayer == player;
		}
		return false;
	}

	internal bool IsGenerationCurrent(int generation)
	{
		if (State != ReplayState.None)
		{
			return generation == _generation;
		}
		return false;
	}

	internal int BeginAsyncOperation()
	{
		InvalidateCurrentPlayer();
		return _generation;
	}

	internal bool TryMarkPlaying(ReplayPlayer player, int generation)
	{
		if (!CanPlayerWriteState(player, generation))
		{
			return false;
		}
		SetState(ReplayState.Playing);
		return true;
	}

	internal bool TryMarkPaused(ReplayPlayer player, int generation)
	{
		if (!CanPlayerWriteState(player, generation))
		{
			return false;
		}
		SetState(ReplayState.Paused);
		return true;
	}

	internal bool TryMarkFinished(ReplayPlayer player, int generation, string reason)
	{
		if (!CanPlayerWriteState(player, generation))
		{
			return false;
		}
		SetState(ReplayState.Finished);
		return true;
	}

	internal bool TryMarkError(ReplayPlayer player, int generation, string code, string message)
	{
		if (!CanPlayerWriteState(player, generation))
		{
			return false;
		}
		MarkError(code, message);
		return true;
	}

	public void MarkError(string code, string message)
	{
		SetState(ReplayState.Error);
		Debug.LogError("[ReplaySession] 回放出错。code=" + code + ", message=" + message);
	}

	private bool CanPlayerWriteState(ReplayPlayer player, int generation)
	{
		if (IsGenerationCurrent(generation))
		{
			return CurrentPlayer == player;
		}
		return false;
	}

	private void BeginNewSession()
	{
		InvalidateCurrentPlayer();
		CurrentPackage = null;
		CurrentRunningGame = null;
		CurrentGameFinish = null;
		PlaybackStartFrameIndex = -1;
		_loadResult = null;
	}

	private void InvalidateCurrentPlayer()
	{
		ReplayPlayer currentPlayer = CurrentPlayer;
		CurrentPlayer = null;
		_generation++;
		currentPlayer?.Stop();
	}

	public async UniTask<bool> InjectFrame(Frame frame)
	{
		if (frame == null)
		{
			Debug.LogError("[ReplaySession] 注入回放帧失败：frame 为 null。");
			return false;
		}
		return await MonoSingletonProvider<NetManager>.inst.RPC.ReceiveReplayS2CFrame(frame);
	}

	public UniTask<bool> InjectFrameAt(int frameIndex)
	{
		if (CurrentPackage?.Frames == null)
		{
			Debug.LogError("[ReplaySession] 注入回放帧失败：CurrentPackage 为 null。");
			return UniTask.FromResult(value: false);
		}
		if (frameIndex < 0 || frameIndex >= CurrentPackage.Frames.Count)
		{
			Debug.LogError(string.Format("{0} 注入回放帧失败。index={1}, frameCount={2}。", "[ReplaySession]", frameIndex, CurrentPackage.FrameCount));
			return UniTask.FromResult(value: false);
		}
		ReplayFrame replayFrame = CurrentPackage.Frames[frameIndex];
		Frame frame = ReplayFrameFactory.CreateS2CFrame(replayFrame);
		if (frame == null)
		{
			Debug.LogError(string.Format("{0} 注入回放帧失败：CreateS2CFrame 返回 null。index={1}, cmdId={2}。", "[ReplaySession]", frameIndex, replayFrame.CmdId));
			return UniTask.FromResult(value: false);
		}
		return InjectFrame(frame);
	}

	public void NotifyTurnNodeReached(ReplayTurnNode node)
	{
		if (node != null)
		{
			Signal.turnNodeReached.Dispatch(node);
		}
	}

	private void FillTurnNodeDisplayNames()
	{
		RoomInfo currentRoom = CurrentRoom;
		if (CurrentPackage?.TurnNodes == null || currentRoom == null)
		{
			return;
		}
		foreach (ReplayTurnNode turnNode in CurrentPackage.TurnNodes)
		{
			turnNode.DisplayName = currentRoom.GetPlayerById(turnNode.PlayerId)?.GetNick() ?? turnNode.PlayerId.ToString();
		}
	}

	private bool EnsureGameLogicReady()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst == null)
		{
			Debug.LogError("[ReplaySession] GameLogicManager.inst 为 null。");
			return false;
		}
		if (!SimpleSingletonProvider<GameLogicManager>.inst.initialize)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.InitLogics();
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.room != null;
	}

	private static Room CloneRoomForReplay(Room sourceRoom, RoomStateType targetState)
	{
		Room room = sourceRoom.Clone();
		room.State = ToServerRoomState(targetState, sourceRoom.State);
		return room;
	}

	private static Room.Types.State ToServerRoomState(RoomStateType targetState, Room.Types.State fallback)
	{
		return targetState switch
		{
			RoomStateType.NONE => Room.Types.State.None, 
			RoomStateType.WAIT => Room.Types.State.Wait, 
			RoomStateType.CHOICE => Room.Types.State.ChoiceHero, 
			RoomStateType.READY => Room.Types.State.Ready1, 
			RoomStateType.RUNNING => Room.Types.State.Running, 
			RoomStateType.SETTLEMENT => Room.Types.State.Finish, 
			_ => fallback, 
		};
	}
}
