using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Map;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public sealed class AttributeMessageQueueAsync
{
	private readonly ConcurrentQueue<AttributeChangeInfo> _queue = new ConcurrentQueue<AttributeChangeInfo>();

	private readonly CtsInfo _cts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();

	private bool _isProcessing;

	private readonly List<UniTask> _tasks = new List<UniTask>();

	public bool IsProcessing => _isProcessing;

	public void Enqueue(AttributeChangeInfo message)
	{
		_queue.Enqueue(message);
	}

	public async UniTask Start()
	{
		if (!_isProcessing)
		{
			_isProcessing = true;
			await ProcessQueue();
		}
	}

	public void Stop()
	{
		_cts.Cancel();
		_queue.Clear();
		_tasks.Clear();
	}

	private async UniTask ProcessQueue()
	{
		AttributeChangeInfoStatistics statistics = new AttributeChangeInfoStatistics();
		while (_queue.Count > 0 && !_cts.IsCancellationRequested)
		{
			AttributeChangeInfo attributeChangeInfo = _queue.Dequeue();
			if (attributeChangeInfo != null)
			{
				try
				{
					statistics.Combine(attributeChangeInfo);
					UniTask item = HandleMessage(attributeChangeInfo, _cts.IsCancellationRequested);
					_tasks.Add(item);
					await UniTask.WaitForSeconds(0.2f, ignoreTimeScale: false, PlayerLoopTiming.Update, Game.GetSystem<GamePlayManager>().CancellationTokenSource.Token);
				}
				catch (Exception arg)
				{
					Debug.LogError($"消息处理异常: {arg}");
				}
			}
			await UniTask.Yield();
		}
		await UniTask.WhenAll(_tasks);
		int gold = statistics.GetGold();
		if (gold != 0)
		{
			Game.GetSystem<BoardManager>().characterManager.Hero.GoldChange.Dispatch(gold);
		}
		_isProcessing = false;
		_tasks.Clear();
	}

	private void StatisticsMessage(AttributeChangeInfo message, Dictionary<(AttributeChangeTarget type, int id), AttributeChangeInfoStatistics> statistics)
	{
	}

	private async UniTask HandleMessage(AttributeChangeInfo message, bool cancel)
	{
		if (cancel)
		{
			return;
		}
		if (message.Source.type == AttributeChangeSource.Building && message.BuildingShowType != BuildingShowType.Pass)
		{
			await Game.GetModel<GlobalSignal>().BuildingShow(message);
		}
		if (message.Source.type == AttributeChangeSource.DiceLand)
		{
			Land landById = Game.GetModel<GameData>().MapData.GetLandById(message.Source.id);
			if (landById != null && landById.landComponent != null)
			{
				await landById.landComponent.ShowAttributeChange(message);
			}
		}
		if (message.Target.type != AttributeChangeTarget.Building || !Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingById(message.Target.id, out var building))
		{
			return;
		}
		if (message.Source.type == AttributeChangeSource.Building && message.Target.id == message.Source.id)
		{
			building.AddExp(message.Exp);
			return;
		}
		BuildingBase building2 = null;
		if (message.Source.type == AttributeChangeSource.Building && Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingById(message.Source.id, out building2))
		{
			building.AddExp(message.Exp, building2);
		}
		else
		{
			building.AddExp(message.Exp);
		}
	}
}
