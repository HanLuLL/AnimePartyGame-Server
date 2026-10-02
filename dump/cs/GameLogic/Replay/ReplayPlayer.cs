using System.Threading;
using Core.Net;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameLogic.Replay;

public sealed class ReplayPlayer
{
	private const string LOG_PREFIX = "[ReplayPlayer]";

	private readonly ReplaySession _session;

	private readonly int _sessionGeneration;

	private readonly CancellationTokenSource _playbackCancellation = new CancellationTokenSource();

	private bool _isPlaying;

	private bool _stopRequested;

	private bool _cancellationDisposed;

	public int CurrentFrameIndex { get; private set; } = -1;

	public int PlayedFrameCount { get; private set; }

	public int TotalFrameCount { get; private set; }

	private bool IsCurrentPlayer => _session.IsCurrentPlayer(this, _sessionGeneration);

	private bool ShouldStop
	{
		get
		{
			if (!_stopRequested && !_playbackCancellation.IsCancellationRequested)
			{
				return !IsCurrentPlayer;
			}
			return true;
		}
	}

	public ReplayPlayer(ReplaySession session, int sessionGeneration)
	{
		_session = session;
		_sessionGeneration = sessionGeneration;
	}

	public async UniTask<bool> PlayAsync(ReplayPackage package, int startFrameIndex = 1)
	{
		if (!IsCurrentPlayer)
		{
			DisposeCancellation();
			return false;
		}
		if (_isPlaying)
		{
			return false;
		}
		if (package?.Frames == null || package.Frames.Count == 0)
		{
			_session.TryMarkError(this, _sessionGeneration, "PackageEmpty", "Replay package has no frames for playback.");
			DisposeCancellation();
			return false;
		}
		_isPlaying = true;
		_stopRequested = false;
		CurrentFrameIndex = -1;
		PlayedFrameCount = 0;
		if (startFrameIndex < 0)
		{
			startFrameIndex = 0;
		}
		if (startFrameIndex > package.Frames.Count)
		{
			startFrameIndex = package.Frames.Count;
		}
		TotalFrameCount = package.Frames.Count - startFrameIndex;
		if (TotalFrameCount == 0)
		{
			_session.TryMarkFinished(this, _sessionGeneration, "没有可播放的帧。");
			_isPlaying = false;
			DisposeCancellation();
			return true;
		}
		if (!_session.TryMarkPlaying(this, _sessionGeneration))
		{
			_isPlaying = false;
			DisposeCancellation();
			return false;
		}
		try
		{
			for (int i = startFrameIndex; i < package.Frames.Count; i++)
			{
				if (ShouldStop)
				{
					_session.TryMarkFinished(this, _sessionGeneration, "已停止。");
					return true;
				}
				if (await WaitWhilePaused())
				{
					_session.TryMarkFinished(this, _sessionGeneration, "暂停期间被取消。");
					return true;
				}
				if (ShouldStop)
				{
					_session.TryMarkFinished(this, _sessionGeneration, "暂停期间被停止。");
					return true;
				}
				ReplayFrame replayFrame = package.Frames[i];
				CurrentFrameIndex = i;
				if (replayFrame.CmdId == 1113)
				{
					ReplayTurnNode node = package.FindTurnNodeByFrameIndex(i);
					_session.NotifyTurnNodeReached(node);
					continue;
				}
				Frame frame = ReplayFrameFactory.CreateS2CFrame(replayFrame);
				if (frame == null)
				{
					_session.TryMarkError(this, _sessionGeneration, "FrameCreateFailed", $"Replay frame create failed. index={i}, cmdId={replayFrame.CmdId}, order={i + 1}/{TotalFrameCount}");
					return true;
				}
				bool flag = await _session.InjectFrame(frame);
				if (ShouldStop)
				{
					_session.TryMarkFinished(this, _sessionGeneration, "帧注入完成后被停止。");
					return true;
				}
				if (!flag)
				{
					_session.TryMarkError(this, _sessionGeneration, "InjectFrameFailed", $"Replay frame injection failed. index={i}, cmdId={frame.CMDID}, order={i + 1}/{TotalFrameCount}");
					return true;
				}
				PlayedFrameCount++;
				if (replayFrame.CmdId == 1016)
				{
					_session.TryMarkFinished(this, _sessionGeneration, "到达 GameFinishS2C。");
					return true;
				}
				if (await UniTask.Delay(Mathf.Max(1, (int)(16f / _session.PlaySpeedMultiplier)), ignoreTimeScale: false, PlayerLoopTiming.Update, _playbackCancellation.Token).SuppressCancellationThrow() || ShouldStop)
				{
					_session.TryMarkFinished(this, _sessionGeneration, "帧间等待期间被取消。");
					return true;
				}
			}
			_session.TryMarkFinished(this, _sessionGeneration, "回放帧序列结束。");
			return true;
		}
		finally
		{
			_isPlaying = false;
			DisposeCancellation();
		}
	}

	public void Pause()
	{
		if (_isPlaying && IsCurrentPlayer && !_session.IsPaused)
		{
			_session.TryMarkPaused(this, _sessionGeneration);
		}
	}

	public void Resume()
	{
		if (_isPlaying && IsCurrentPlayer && _session.IsPaused)
		{
			_session.TryMarkPlaying(this, _sessionGeneration);
		}
	}

	public void Stop()
	{
		if (!_stopRequested)
		{
			_stopRequested = true;
			if (!_cancellationDisposed)
			{
				_playbackCancellation.Cancel();
			}
		}
	}

	private async UniTask<bool> WaitWhilePaused()
	{
		if (!_session.IsPaused || ShouldStop)
		{
			return ShouldStop;
		}
		return await UniTask.WaitWhile(() => _session.IsPaused && !ShouldStop, PlayerLoopTiming.Update, _playbackCancellation.Token).SuppressCancellationThrow() || ShouldStop;
	}

	private void DisposeCancellation()
	{
		if (!_cancellationDisposed)
		{
			_cancellationDisposed = true;
			_playbackCancellation.Dispose();
		}
	}
}
