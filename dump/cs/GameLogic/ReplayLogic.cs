using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using GameLogic.Replay;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Networking;
using party.protocol;

namespace GameLogic;

public class ReplayLogic : IRPCSync
{
	public enum ReplayLoadStatus
	{
		Success,
		DownloadFailed,
		ReadFailed,
		SaveLimitReached
	}

	private class ReplayReadResult
	{
		public string id;

		public bool success;

		public byte[] bytes;

		public long size;

		public long mtimeUtc;
	}

	private readonly ReplaySession _session = new ReplaySession();

	private readonly ConcurrentDictionary<string, (long mtimeUtc, long size, BattleShortRecord record)> _summaryCache = new ConcurrentDictionary<string, (long, long, BattleShortRecord)>();

	private const string _cdnBaseUrl_INT_DEV = "https://0.0.0.0/dev/";

	private const string _cdnBaseUrl_INT = "https://0.0.0.0/prod/";

	private const string _cdnBaseUrl_CN_DEV = "https://0.0.0.0/dev/";

	private const string _cdnBaseUrl_CN = "https://0.0.0.0/prod/";

	private string _cdnBaseUrl = "https://0.0.0.0/prod/";

	private readonly string _replayRoot = Application.persistentDataPath + "/Temp/Replay";

	private const int SUMMARY_PIPELINE_CONCURRENCY = 4;

	private const int MIN_MASK_DURATION_MS = 1000;

	public ReplaySession Session => _session;

	public void ChangeReplayCDNUrl(int index)
	{
		_cdnBaseUrl = index switch
		{
			0 => "https://0.0.0.0/dev/", 
			1 => "https://0.0.0.0/prod/", 
			2 => "https://0.0.0.0/dev/", 
			3 => "https://0.0.0.0/prod/", 
			_ => _cdnBaseUrl, 
		};
	}

	public async UniTask<(byte[] data, ReplayLoadStatus status)> TryLoadReplayFile(string replayId, bool needSave)
	{
		float maskShowTime = Time.time;
		(byte[], ReplayLoadStatus SaveLimitReached) result;
		try
		{
			if (needSave && !IsReplayCached(replayId) && ListAllReplayIds().Count >= StaticGlobalData.MAX_BATTLE_RECORDS)
			{
				result = (null, SaveLimitReached: ReplayLoadStatus.SaveLimitReached);
			}
			else
			{
				var (extractPath, replayLoadStatus) = await EnsureLocalReplayFileAsync(replayId);
				if (replayLoadStatus != ReplayLoadStatus.Success)
				{
					result = (null, SaveLimitReached: replayLoadStatus);
				}
				else
				{
					string path = Path.Combine(extractPath, replayId);
					byte[] item;
					try
					{
						item = await File.ReadAllBytesAsync(path);
					}
					catch (Exception ex)
					{
						Debug.LogError("目标文件读取失败，将删除失败文件，异常原因: " + ex.Message);
						TryDeleteExtractDir(extractPath);
						result = (null, SaveLimitReached: ReplayLoadStatus.ReadFailed);
						goto end_IL_0040;
					}
					if (!needSave)
					{
						TryDeleteExtractDir(extractPath);
					}
					result = (item, SaveLimitReached: ReplayLoadStatus.Success);
				}
			}
			end_IL_0040:;
		}
		finally
		{
			int num = 1000 - (int)((Time.time - maskShowTime) * 1000f);
			if (num > 0)
			{
				await UniTask.Delay(num, DelayType.UnscaledDeltaTime);
			}
		}
		return result;
	}

	private void TryDeleteExtractDir(string extractPath)
	{
		try
		{
			if (Directory.Exists(extractPath))
			{
				Directory.Delete(extractPath, recursive: true);
			}
		}
		catch (Exception)
		{
		}
	}

	private async UniTask<(string extractPath, ReplayLoadStatus status)> EnsureLocalReplayFileAsync(string replayId, CancellationToken cancellationToken = default(CancellationToken))
	{
		string url = _cdnBaseUrl + replayId;
		string extractPath = Path.Combine(_replayRoot, replayId);
		string text = Path.Combine(extractPath, replayId);
		if (File.Exists(text))
		{
			return (extractPath, ReplayLoadStatus.Success);
		}
		TryDeleteExtractDir(extractPath);
		Directory.CreateDirectory(extractPath);
		if (!(await DownloadFile(url, text, cancellationToken)))
		{
			TryDeleteExtractDir(extractPath);
			return (null, ReplayLoadStatus.DownloadFailed);
		}
		return (extractPath, ReplayLoadStatus.Success);
	}

	private static async UniTask<bool> DownloadFile(string url, string savePath, CancellationToken cancellationToken)
	{
		UnityWebRequest request = UnityWebRequest.Get(url);
		try
		{
			request.downloadHandler = (DownloadHandler)new DownloadHandlerFile(savePath);
			try
			{
				await request.SendWebRequest().ToUniTask(null, PlayerLoopTiming.Update, cancellationToken);
			}
			catch (UnityWebRequestException ex)
			{
				Debug.LogError("回放下载失败：" + ex.Message);
				return false;
			}
			return true;
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	public async UniTask<bool> ShowReplayResultAsync(byte[] bytes)
	{
		if (!_session.LoadBytes(bytes))
		{
			return false;
		}
		int generation = _session.Generation;
		return await ShowLoadedReplayResultAsync(generation);
	}

	private async UniTask<bool> ShowLoadedReplayResultAsync(int generation)
	{
		if (!_session.IsGenerationCurrent(generation))
		{
			return false;
		}
		GameFinishS2C currentGameFinish = _session.CurrentGameFinish;
		if (currentGameFinish == null)
		{
			Debug.LogError("[ReplayLogic] CurrentGameFinish 为 null，无法展示战斗结算。");
			return false;
		}
		if (!_session.BuildSettlementRoom())
		{
			Debug.LogError("[ReplayLogic] 构建结算专用房间失败，无法展示战斗结算。");
			return false;
		}
		List<BattlePlayerData> list = BuildPlayerDatasFromCurrentRoom();
		if (list == null)
		{
			Debug.LogError("[ReplayLogic] 无法从结算专用房间构建玩家数据，无法展示战斗结算。");
			return false;
		}
		BattleSettlement battleSettlement = new BattleSettlement(currentGameFinish, list);
		IBasePanel basePanel = await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.BattleSettlement, battleSettlement);
		if (!_session.IsGenerationCurrent(generation))
		{
			return false;
		}
		if (basePanel is BattleSettlementPanel battleSettlementPanel)
		{
			battleSettlementPanel.ShowAchieveData();
			return true;
		}
		return false;
	}

	private static List<BattlePlayerData> BuildPlayerDatasFromCurrentRoom()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo?.Players == null || roomInfo.Players.Count == 0)
		{
			return null;
		}
		List<BattlePlayerData> list = new List<BattlePlayerData>(roomInfo.Players.Count);
		foreach (RoomPlayer player in roomInfo.Players)
		{
			if (player != null)
			{
				list.Add(new BattlePlayerData(player));
			}
		}
		if (list.Count <= 0)
		{
			return null;
		}
		return list;
	}

	public async UniTask<bool> StartPlaybackAsync(RoomStateType targetState = RoomStateType.RUNNING, int startFrameIndex = -1)
	{
		_session.ResetPlaySpeed();
		if (!_session.RebuildLoadedRoom(targetState))
		{
			return false;
		}
		int generation = _session.Generation;
		if (!(await PrepareReplayBattleScene(generation)))
		{
			return false;
		}
		_session.PlayCurrentPackage(startFrameIndex).Forget();
		return true;
	}

	public void PauseReplay()
	{
		_session.PausePlayback();
	}

	public void ResumeReplay()
	{
		_session.ResumePlayback();
	}

	public void StopReplay()
	{
		_session.StopPlayback();
	}

	public void SetPlaySpeed(float multiplier)
	{
		_session.SetPlaySpeed(multiplier);
	}

	public void ExitReplay()
	{
		_session.StopPlayback();
		_session.Clear();
	}

	public async UniTask<bool> JumpToTurnAsync(ReplayTurnNode turn, RoomStateType targetState = RoomStateType.RUNNING)
	{
		if (turn == null)
		{
			Debug.LogError("[ReplayLogic] JumpToTurnAsync 失败:turn 为 null。");
			return false;
		}
		_session.StopPlayback();
		int generation = _session.BeginAsyncOperation();
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
		if (!_session.IsGenerationCurrent(generation))
		{
			return false;
		}
		if (!_session.RebuildRoomFromSnapshot(turn.Snapshot, targetState))
		{
			Debug.LogError($"[ReplayLogic] JumpToTurnAsync 失败:从快照重建房间失败。turnIndex={turn.TurnIndex}, round={turn.Round}");
			return false;
		}
		_session.NotifyTurnNodeReached(turn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.ReconcilePlayerDatasForReplaySnapshot(SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo);
		if (!(await PrepareReplayBattleScene(generation)))
		{
			return false;
		}
		int millisecondsDelay = Mathf.Max(1, (int)(1000f / _session.PlaySpeedMultiplier));
		if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(millisecondsDelay))
		{
			return true;
		}
		if (!_session.IsGenerationCurrent(generation))
		{
			return false;
		}
		_session.PlayCurrentPackage(turn.PlaybackStartIndex).Forget();
		return true;
	}

	public ReplayTurnNode FindRelativeTurn(int direction)
	{
		if (direction != 1 && direction != -1)
		{
			return null;
		}
		List<ReplayTurnNode> list = _session.CurrentPackage?.TurnNodes;
		if (list == null || list.Count == 0)
		{
			return null;
		}
		int num = _session.CurrentPlayer?.CurrentFrameIndex ?? _session.PlaybackStartFrameIndex;
		int num2 = -1;
		for (int i = 0; i < list.Count && list[i].FrameIndex <= num; i++)
		{
			num2 = i;
		}
		int num3 = num2 + direction;
		if (num3 < 0 || num3 >= list.Count)
		{
			return null;
		}
		return list[num3];
	}

	public ReplayTurnNode FindRelativeRound(int direction)
	{
		if (direction != 1 && direction != -1)
		{
			return null;
		}
		List<ReplayRoundNode> list = _session.CurrentPackage?.Rounds;
		if (list == null || list.Count == 0)
		{
			return null;
		}
		int num = _session.CurrentPlayer?.CurrentFrameIndex ?? _session.PlaybackStartFrameIndex;
		int num2 = -1;
		for (int i = 0; i < list.Count && list[i].FrameIndex <= num; i++)
		{
			num2 = i;
		}
		int num3 = num2 + direction;
		if (num3 < 0 || num3 >= list.Count)
		{
			return null;
		}
		ReplayRoundNode replayRoundNode = list[num3];
		if (replayRoundNode.Turns.Count <= 0)
		{
			return null;
		}
		return replayRoundNode.Turns[0];
	}

	public bool CanNextTurn()
	{
		return FindRelativeTurn(1) != null;
	}

	public bool CanPreTurn()
	{
		return FindRelativeTurn(-1) != null;
	}

	public bool CanNextRound()
	{
		return FindRelativeRound(1) != null;
	}

	public bool CanPreRound()
	{
		return FindRelativeRound(-1) != null;
	}

	public UniTask<bool> JumpToNextTurnAsync()
	{
		return JumpToRelativeAsync(1, byRound: false);
	}

	public UniTask<bool> JumpToPreTurnAsync()
	{
		return JumpToRelativeAsync(-1, byRound: false);
	}

	public UniTask<bool> JumpToNextRoundAsync()
	{
		return JumpToRelativeAsync(1, byRound: true);
	}

	public UniTask<bool> JumpToPreRoundAsync()
	{
		return JumpToRelativeAsync(-1, byRound: true);
	}

	private async UniTask<bool> JumpToRelativeAsync(int direction, bool byRound)
	{
		ReplayTurnNode replayTurnNode = (byRound ? FindRelativeRound(direction) : FindRelativeTurn(direction));
		if (replayTurnNode == null)
		{
			return false;
		}
		return await JumpToTurnAsync(replayTurnNode);
	}

	public UniTask PrepareReplayBattleScene()
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		return room.SyncRunningGame(room.curRoomInfo);
	}

	private async UniTask<bool> PrepareReplayBattleScene(int generation)
	{
		if (!_session.IsGenerationCurrent(generation))
		{
			return false;
		}
		await PrepareReplayBattleScene();
		return _session.IsGenerationCurrent(generation);
	}

	public bool IsReplayCached(string replayId)
	{
		if (string.IsNullOrEmpty(replayId))
		{
			return false;
		}
		return File.Exists(Path.Combine(_replayRoot, replayId, replayId));
	}

	public bool DeleteReplayFile(string replayId)
	{
		if (string.IsNullOrEmpty(replayId))
		{
			return false;
		}
		string path = Path.Combine(_replayRoot, replayId);
		try
		{
			if (Directory.Exists(path))
			{
				Directory.Delete(path, recursive: true);
				_summaryCache.TryRemove(replayId, out (long, long, BattleShortRecord) _);
				return true;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[ReplayLogic] 删除回放文件失败：" + ex.Message);
		}
		return false;
	}

	public async UniTask<List<BattleShortRecord>> ListLocalReplaySummariesAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		List<string> ids = ListAllReplayIds();
		if (ids.Count == 0)
		{
			return new List<BattleShortRecord>();
		}
		await SimpleSingletonProvider<UIManager>.inst.loadingTip.TryShow(_delayStatus: false);
		float maskShowTime = Time.time;
		List<BattleShortRecord> result;
		try
		{
			var (hits, source) = SplitBySummaryCache(ids);
			PruneSummaryCache(ids);
			SemaphoreSlim pipelineSemaphore = new SemaphoreSlim(4, 4);
			BattleShortRecord[] array = await UniTask.WhenAll(source.Select((ReplayReadResult r) => LoadAndParseReplaySummaryAsync(r, pipelineSemaphore, cancellationToken)));
			List<BattleShortRecord> list = new List<BattleShortRecord>(hits.Count + array.Length);
			list.AddRange(hits);
			BattleShortRecord[] array2 = array;
			foreach (BattleShortRecord battleShortRecord in array2)
			{
				if (battleShortRecord != null)
				{
					list.Add(battleShortRecord);
				}
			}
			result = list;
		}
		finally
		{
			await UniTask.SwitchToMainThread();
			int num2 = 1000 - (int)((Time.time - maskShowTime) * 1000f);
			if (num2 > 0)
			{
				await UniTask.Delay(num2, DelayType.UnscaledDeltaTime, PlayerLoopTiming.Update, cancellationToken);
			}
			SimpleSingletonProvider<UIManager>.inst.loadingTip.Hide();
		}
		return result;
	}

	private (List<BattleShortRecord> hits, List<ReplayReadResult> misses) SplitBySummaryCache(List<string> ids)
	{
		List<BattleShortRecord> list = new List<BattleShortRecord>(ids.Count);
		List<ReplayReadResult> list2 = new List<ReplayReadResult>();
		foreach (string id in ids)
		{
			string path = Path.Combine(_replayRoot, id, id);
			long length;
			long ticks;
			try
			{
				if (!File.Exists(path))
				{
					continue;
				}
				using (FileStream fileStream = File.OpenRead(path))
				{
					length = fileStream.Length;
				}
				ticks = File.GetLastWriteTimeUtc(path).Ticks;
				goto IL_007a;
			}
			catch (Exception)
			{
			}
			continue;
			IL_007a:
			if (_summaryCache.TryGetValue(id, out (long, long, BattleShortRecord) value) && value.Item1 == ticks && value.Item2 == length)
			{
				list.Add(value.Item3);
				continue;
			}
			list2.Add(new ReplayReadResult
			{
				id = id,
				size = length,
				mtimeUtc = ticks
			});
		}
		return (hits: list, misses: list2);
	}

	private void PruneSummaryCache(List<string> existingIds)
	{
		HashSet<string> hashSet = new HashSet<string>(existingIds);
		foreach (string key in _summaryCache.Keys)
		{
			if (!hashSet.Contains(key))
			{
				_summaryCache.TryRemove(key, out (long, long, BattleShortRecord) _);
			}
		}
	}

	private async UniTask<BattleShortRecord> LoadAndParseReplaySummaryAsync(ReplayReadResult r, SemaphoreSlim pipelineSemaphore, CancellationToken cancellationToken)
	{
		await pipelineSemaphore.WaitAsync(-1, cancellationToken);
		try
		{
			await LoadReplayFileAsync(r, cancellationToken);
			BattleShortRecord obj = await ParseReplaySummaryAsync(r, cancellationToken);
			r.bytes = null;
			if (obj == null)
			{
				DeleteReplayFile(r.id);
			}
			return obj;
		}
		finally
		{
			pipelineSemaphore.Release();
		}
	}

	private async UniTask LoadReplayFileAsync(ReplayReadResult r, CancellationToken cancellationToken)
	{
		string path = Path.Combine(_replayRoot, r.id, r.id);
		try
		{
			r.bytes = await File.ReadAllBytesAsync(path, cancellationToken);
			r.success = true;
		}
		catch (Exception)
		{
			r.success = false;
		}
	}

	private async UniTask<BattleShortRecord> ParseReplaySummaryAsync(ReplayReadResult r, CancellationToken cancellationToken)
	{
		if (!r.success || r.bytes == null)
		{
			return null;
		}
		BattleShortRecord battleShortRecord = await UniTask.RunOnThreadPool(delegate
		{
			try
			{
				var (gameFinishS2C, lastSnapshot) = ReplayLoader.TryParseReplaySettlementOnly(r.bytes);
				return (gameFinishS2C == null) ? null : BuildBattleShortRecord(r.id, gameFinishS2C, lastSnapshot);
			}
			catch (Exception)
			{
				return (BattleShortRecord)null;
			}
		}, configureAwait: false, cancellationToken);
		if (battleShortRecord != null)
		{
			_summaryCache[r.id] = (r.mtimeUtc, r.size, battleShortRecord);
		}
		return battleShortRecord;
	}

	private List<string> ListAllReplayIds()
	{
		if (!Directory.Exists(_replayRoot))
		{
			return new List<string>();
		}
		List<string> list = new List<string>();
		foreach (string item in Directory.EnumerateDirectories(_replayRoot))
		{
			string fileName = Path.GetFileName(item);
			if (File.Exists(Path.Combine(item, fileName)))
			{
				list.Add(fileName);
			}
		}
		return list;
	}

	private static BattleShortRecord BuildBattleShortRecord(string replayId, GameFinishS2C gf, ReplaySnapshotS2C lastSnapshot = null)
	{
		return new BattleShortRecord(replayId, gf, lastSnapshot, BattleShortRecordType.Replay);
	}

	public void Clear()
	{
		ReplaySession session = Session;
		if (session != null && session.IsReplay)
		{
			ExitReplay();
		}
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ReplayDieS2C.OnReplayDieS2CServerCallBackAsync = OnReplayDieS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ReplayDieS2C.OnReplayDieS2CServerCallBackAsync = null;
	}

	private async UniTask OnReplayDieS2CServerCallBackAsync(ReplayDieS2C model, int errId, bool isDispatch)
	{
		await UniTask.CompletedTask;
	}
}
