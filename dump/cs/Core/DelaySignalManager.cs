using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tools;

namespace Core;

public class DelaySignalManager : SimpleSingletonProvider<DelaySignalManager>
{
	private List<CtsInfo> ctsInfos = new List<CtsInfo>();

	private int Id;

	public CtsInfo CreatCts()
	{
		CancellationTokenSource cts = new CancellationTokenSource();
		CtsInfo ctsInfo = new CtsInfo
		{
			cts = cts,
			id = Id
		};
		Id++;
		ctsInfos.Add(ctsInfo);
		return ctsInfo;
	}

	public void CancelAllTask()
	{
		foreach (CtsInfo item in ctsInfos.ToList())
		{
			item.Cancel();
		}
		ctsInfos.Clear();
	}

	public void CancelTask(int id)
	{
		foreach (CtsInfo ctsInfo in ctsInfos)
		{
			if (ctsInfo.id == id)
			{
				ctsInfo.Cancel();
				break;
			}
		}
	}

	public void CancelTask(CtsInfo ctsInfo)
	{
		ctsInfo?.Cancel();
	}

	public void DisposeCts(CtsInfo ctsInfo)
	{
		ctsInfo.DisposeAfterFinish();
		ctsInfos.Remove(ctsInfo);
	}

	public void Dispose()
	{
		Id = 0;
		OnDestroyInstance();
	}

	protected override void OnDestroyInstance()
	{
		for (int i = 0; i < ctsInfos.Count; i++)
		{
			ctsInfos[i].Dispose();
		}
		ctsInfos.Clear();
		base.OnDestroyInstance();
	}

	public async UniTask<bool> Delay(TimeSpan delayTimeSpan, Action cancelAction = null)
	{
		CtsInfo cts = CreatCts();
		if (cancelAction != null)
		{
			cts.reg = cts.Token.Register(cancelAction);
		}
		bool result = await UniTask.Delay(delayTimeSpan, ignoreTimeScale: false, PlayerLoopTiming.Update, cts.Token).SuppressCancellationThrow();
		DisposeCts(cts);
		return result;
	}

	public async UniTask<bool> Delay(int millisecondsDelay, Action cancelAction = null)
	{
		CtsInfo cts = CreatCts();
		if (cancelAction != null)
		{
			cts.reg = cts.Token.Register(cancelAction);
		}
		bool result = await UniTask.Delay(millisecondsDelay, ignoreTimeScale: false, PlayerLoopTiming.Update, cts.Token).SuppressCancellationThrow();
		DisposeCts(cts);
		return result;
	}

	public async UniTask<bool> WaitWhile(Func<bool> predicate, Action cancelAction = null)
	{
		CtsInfo cts = CreatCts();
		if (cancelAction != null)
		{
			cts.reg = cts.Token.Register(cancelAction);
		}
		bool result = await UniTask.WaitWhile(predicate, PlayerLoopTiming.Update, cts.Token).SuppressCancellationThrow();
		DisposeCts(cts);
		return result;
	}

	public async UniTask<bool> WaitUntil(Func<bool> predicate, Action cancelAction = null)
	{
		CtsInfo cts = CreatCts();
		if (cancelAction != null)
		{
			cts.reg = cts.Token.Register(cancelAction);
		}
		bool result = await UniTask.WaitUntil(predicate, PlayerLoopTiming.Update, cts.Token).SuppressCancellationThrow();
		DisposeCts(cts);
		return result;
	}

	public async UniTask<bool> WhenAll(params UniTask[] tasks)
	{
		CtsInfo cts = CreatCts();
		bool result = await UniTask.WhenAll(tasks).AttachExternalCancellation(cts.Token).SuppressCancellationThrow();
		DisposeCts(cts);
		return result;
	}

	public async UniTask WaitUntilCanceled(CtsInfo tsc)
	{
		if (tsc != null && !tsc.IsCancellationRequested)
		{
			await UniTask.WaitUntilCanceled(tsc.Token);
			DisposeCts(tsc);
		}
	}
}
