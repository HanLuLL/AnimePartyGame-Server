using System;
using System.Threading;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Core.Tutorial;

public class GamePlayManager : ISystem, IInitialize, ITick, IDispose
{
	public GameLoopMode GameLoopMode { get; private set; }

	public IGameLoop GameLoop { get; private set; }

	public CancellationTokenSource CancellationTokenSource { get; private set; }

	public TutorialGlobalSignal Signal { get; private set; }

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
		GameLoopMode = GameLoopMode.Default;
		CancellationTokenSource = new CancellationTokenSource();
		Signal = TutorialGame.GetModel<TutorialGlobalSignal>();
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
		Signal.GameStartBefore.Dispatch();
		await GameStart();
		Signal.GameStartAfter.Dispatch();
		while (!CancellationTokenSource.IsCancellationRequested)
		{
			Signal.RoundStartBefore.Dispatch();
			await RoundStart();
			Signal.RoundStartAfter.Dispatch();
			if (!(await GameLoop.Execute()))
			{
				return;
			}
			Signal.RoundEndBefore.Dispatch();
			await RoundEnd();
			Signal.RoundEndAfter.Dispatch();
		}
		Signal.GameEndBefore.Dispatch();
		await GameEnd();
		Signal.GameEndAfter.Dispatch();
	}

	private async UniTask GameStart()
	{
		Signal.GameStart.Dispatch();
		Debug.Log("游戏开始");
		await UniTask.CompletedTask;
	}

	public async UniTask GameEnd()
	{
		Signal.GameEnd.Dispatch();
		Debug.Log("游戏结束");
		CancellationTokenSource?.Cancel();
		SimpleSingletonProvider<DelaySignalManager>.inst.CancelAllTask();
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial mapGimmickManager_Tutorial)
		{
			await mapGimmickManager_Tutorial.TutorialEnd();
		}
		await UniTask.CompletedTask;
	}

	private async UniTask RoundStart()
	{
		await TutorialGame.GetSystem<TutorialBoardManager>().gameManager.RoundStart();
		await TutorialGame.GetSystem<TutorialBoardManager>().buffManager.OnRoundStart();
		Signal.RoundStart.Dispatch();
	}

	private async UniTask RoundEnd()
	{
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmickManager_Tutorial)
		{
			TutorialBoardGameManager gameManager = TutorialGame.GetSystem<TutorialBoardManager>().gameManager;
			if (gameManager.Round + 1 >= mapGimmickManager_Tutorial.TutorialSceneConfig.MaxProgressLimit)
			{
				CancellationTokenSource?.Cancel();
				gameManager.SetTutorialStatus(TutorialStatus.Failure);
				return;
			}
		}
		Signal.RoundEnd.Dispatch();
		await TutorialGame.GetSystem<TutorialBoardManager>().missionManager.OnRoundEnd();
		await UniTask.CompletedTask;
	}
}
