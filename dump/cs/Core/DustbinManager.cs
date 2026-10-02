using System;
using System.Collections.Generic;
using Core.Camera;
using Core.Unit;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core;

public class DustbinManager : SimpleSingletonProvider<DustbinManager>
{
	private Character attacker;

	private Character victimer;

	public float _height = 10f;

	public float _duration = 0.2f;

	private Vector3 initOffset;

	private readonly List<Vector3> paths = new List<Vector3>();

	private readonly Dictionary<int, PathNode> recordPathNodes = new Dictionary<int, PathNode>();

	public Queue<PathNode> searchQueue = new Queue<PathNode>();

	public async UniTask TriggerDustbin(long _attackerId, long _victimId, Vector3 _initOffset)
	{
		initOffset = _initOffset;
		attacker = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_attackerId).CharacterInst;
		victimer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_victimId).CharacterInst;
		CreatePath();
		await StartDustbin();
	}

	private void LimitCamera(bool state)
	{
		SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.limitCameraControl.Dispatch(state);
	}

	private async UniTask StartDustbin()
	{
		LimitCamera(state: true);
		int index = Convert.ToInt32(Mathf.Floor(paths.Count / 2));
		SimpleSingletonProvider<CameraManager>.inst.EnableShowCamera(paths[index] - Vector3.up * _height);
		GameObject dustbin = await Addressables.InstantiateAsync(2000.GetEffectDataConfigure().EffectName);
		Transform head = dustbin.transform.GetChild(0);
		LineRenderer lineRenderer = dustbin.GetComponentInChildren<LineRenderer>();
		await Protrude(head, lineRenderer);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(victimer, willMove: true);
		await Shrink(head, lineRenderer);
		UnityEngine.Object.Destroy(dustbin);
		LimitCamera(state: false);
	}

	private async UniTask Protrude(Transform head, LineRenderer lineRenderer)
	{
		lineRenderer.positionCount = 1;
		Vector3 pathPos = GetPathPos(0);
		lineRenderer.SetPosition(0, pathPos);
		head.position = pathPos;
		TweenAwaiter val2 = default(TweenAwaiter);
		for (int i = 1; i < paths.Count; i++)
		{
			lineRenderer.positionCount++;
			lineRenderer.SetPosition(i, head.transform.position);
			Vector3 pathPos2 = GetPathPos(i);
			head.forward = pathPos2 - head.position;
			int lineIndex = i;
			TweenAwaiter val = DOTweenAsyncExtensions.GetAwaiter((Tween)head.DOMove(pathPos2, _duration).OnUpdate(delegate
			{
				lineRenderer.SetPosition(lineIndex, head.transform.position);
			}).SetEase(Ease.Linear));
			if (!((TweenAwaiter)(ref val)).IsCompleted)
			{
				await val;
				val = val2;
				val2 = default(TweenAwaiter);
			}
			((TweenAwaiter)(ref val)).GetResult();
		}
	}

	private async UniTask Shrink(Transform head, LineRenderer lineRenderer)
	{
		TweenAwaiter val2 = default(TweenAwaiter);
		for (int i = paths.Count - 1; i > 0; i--)
		{
			Vector3 pathPos = GetPathPos(i - 1);
			head.forward = head.position - pathPos;
			int lineIndex = i;
			TweenAwaiter val = DOTweenAsyncExtensions.GetAwaiter((Tween)head.DOMove(pathPos, _duration).OnUpdate(delegate
			{
				lineRenderer.SetPosition(lineIndex, head.transform.position);
				Vector3 position = new Vector3(head.transform.position.x, victimer.transform.position.y, head.transform.position.z);
				victimer.transform.position = position;
			}).SetEase(Ease.Linear));
			if (!((TweenAwaiter)(ref val)).IsCompleted)
			{
				await val;
				val = val2;
				val2 = default(TweenAwaiter);
			}
			((TweenAwaiter)(ref val)).GetResult();
			lineRenderer.positionCount--;
		}
		victimer.transform.position = attacker.transform.position;
	}

	private Vector3 GetPathPos(int index)
	{
		return new Vector3(paths[index].x, paths[index].y, paths[index].z);
	}

	private void CreatePath()
	{
		paths.Clear();
		Vector3 vector = Vector3.up * _height;
		if (attacker.standLand.Id == victimer.standLand.Id)
		{
			paths.Add(victimer.transform.position + vector);
			paths.Add(attacker.transform.position + initOffset);
		}
		else
		{
			PathNode pathNode = BFS();
			paths.Add(victimer.transform.position + vector);
			pathNode = recordPathNodes[pathNode.preNodeId];
			while (pathNode.curNodeId != attacker.standLand.Id)
			{
				Vector3 item = SimpleSingletonProvider<LandManager>.inst.GetLandById(pathNode.curNodeId).transform.position + vector;
				paths.Add(item);
				pathNode = recordPathNodes[pathNode.preNodeId];
			}
			paths.Add(attacker.transform.position + initOffset);
		}
		paths.Reverse();
	}

	public PathNode BFS()
	{
		recordPathNodes.Clear();
		searchQueue.Clear();
		searchQueue.Enqueue(GetRecordPathNode(attacker.standLand.Id, -1));
		while (searchQueue.Count > 0)
		{
			PathNode pathNode = searchQueue.Dequeue();
			if (pathNode.curNodeId == victimer.standLand.Id)
			{
				return pathNode;
			}
			ExploreAround(pathNode);
		}
		return null;
	}

	private void ExploreAround(PathNode searchCenter)
	{
		foreach (int item in SimpleSingletonProvider<LandManager>.inst.GetLandById(searchCenter.curNodeId).CanSelectedLandId(searchCenter.preNodeId))
		{
			PathNode recordPathNode = GetRecordPathNode(item, searchCenter.curNodeId);
			if (!searchQueue.Contains(recordPathNode))
			{
				searchQueue.Enqueue(recordPathNode);
			}
		}
	}

	public PathNode GetRecordPathNode(int curNodeId, int preNodeId)
	{
		PathNode pathNode = new PathNode(curNodeId, preNodeId);
		recordPathNodes.TryAdd(pathNode.curNodeId, pathNode);
		return pathNode;
	}
}
