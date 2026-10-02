using System;
using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class GamePlayManager : ISystem, IInitialize, ITick, IDispose
{
	public GameLoopMode GameLoopMode { get; private set; }

	public IGameLoop GameLoop { get; private set; }

	public CancellationTokenSource CancellationTokenSource { get; private set; }

	public GlobalSignal Signal { get; private set; }

	public GameData GameData { get; private set; }

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
		GameLoopMode = GameLoopMode.Default;
		CancellationTokenSource = new CancellationTokenSource();
		Signal = Game.GetModel<GlobalSignal>();
		GameData = Game.GetModel<GameData>();
		if (GameLoopMode == GameLoopMode.Default)
		{
			GameLoop = new DefaultGameLoop(this);
			return;
		}
		throw new ArgumentOutOfRangeException($"未处理的GameLoopType: {GameLoopMode}");
	}

	public void Tick()
	{
	}

	public void Dispose()
	{
		GameLoopMode = GameLoopMode.None;
		CancellationTokenSource?.Dispose();
		CancellationTokenSource = null;
		SimpleSingletonProvider<DelaySignalManager>.inst.Dispose();
	}

	public async UniTask Run()
	{
		GameData.SetRoundTiming(RoundTiming.GameStartBefore);
		Signal.GameStartBefore.Dispatch();
		await GameStart();
		GameData.SetRoundTiming(RoundTiming.GameStartAfter);
		Signal.GameStartAfter.Dispatch();
		while (!CancellationTokenSource.IsCancellationRequested)
		{
			GameData.SetRoundTiming(RoundTiming.RoundStartBefore);
			Signal.RoundStartBefore.Dispatch();
			await RoundStart();
			GameData.SetRoundTiming(RoundTiming.RoundStartAfter);
			Signal.RoundStartAfter.Dispatch();
			if (!(await GameLoop.Execute()))
			{
				return;
			}
			GameData.SetRoundTiming(RoundTiming.RoundEndBefore);
			Signal.RoundEndBefore.Dispatch();
			await RoundEnd();
			GameData.SetRoundTiming(RoundTiming.RoundEndAfter);
			Signal.RoundEndAfter.Dispatch();
		}
		GameData.SetRoundTiming(RoundTiming.GameEndBefore);
		Signal.GameEndBefore.Dispatch();
		await GameEnd();
		GameData.SetRoundTiming(RoundTiming.GameEndAfter);
		Signal.GameEndAfter.Dispatch();
	}

	private async UniTask GameStart()
	{
		GameData.SetRoundTiming(RoundTiming.GameStart);
		Signal.GameStart.Dispatch();
		Debug.Log("游戏开始");
		await UniTask.CompletedTask;
	}

	public async UniTask GameEnd()
	{
		GameData.SetRoundTiming(RoundTiming.GameEnd);
		Signal.GameEnd.Dispatch();
		Debug.Log("游戏结束");
		CancellationTokenSource?.Cancel();
		SimpleSingletonProvider<DelaySignalManager>.inst.CancelAllTask();
		await UniTask.CompletedTask;
	}

	private async UniTask RoundStart()
	{
		GameData.SetRoundTiming(RoundTiming.RoundStart);
		await UniTask.CompletedTask;
		if (GameData.RoundTiming == RoundTiming.RoundStart)
		{
			Game.GetModel<GameData>().Round.Value++;
			if (!Game.GetModel<GMData>().LockGameProgress)
			{
				Game.GetModel<GameData>().GameProgress.Value++;
			}
		}
		Signal.RoundStart.Dispatch();
	}

	private async UniTask RoundEnd()
	{
		GameData.SetRoundTiming(RoundTiming.RoundEnd);
		Signal.RoundEnd.Dispatch();
		await UniTask.CompletedTask;
	}
}
