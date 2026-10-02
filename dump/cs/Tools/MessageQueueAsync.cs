using System;
using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tools;

public class MessageQueueAsync<T>
{
	private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();

	private readonly CtsInfo _cts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();

	private bool _isProcessing;

	public void Enqueue(T message)
	{
		_queue.Enqueue(message);
		if (!_isProcessing)
		{
			_isProcessing = true;
			ProcessQueue().Forget();
		}
	}

	public virtual void Stop()
	{
		_cts.Cancel();
		_queue.Clear();
	}

	private async UniTaskVoid ProcessQueue()
	{
		while (_queue.Count > 0 && !_cts.IsCancellationRequested)
		{
			T val = _queue.Dequeue();
			if (val != null)
			{
				try
				{
					await HandleMessage(val, _cts.IsCancellationRequested);
				}
				catch (Exception arg)
				{
					Debug.LogError($"消息处理异常: {arg}");
				}
			}
			await UniTask.Yield();
		}
		_isProcessing = false;
	}

	protected virtual UniTask HandleMessage(T message, bool cancel)
	{
		Debug.Log($"处理消息: {message}");
		return UniTask.CompletedTask;
	}
}
