using System;
using System.Collections.Generic;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core;

public class RoadLineData : IEquatable<RoadLineData>
{
	private readonly int Head;

	public readonly int Tail;

	public readonly List<int> Path;

	public readonly int Step;

	private GameObject _RoadLine;

	private GameObject _Direction;

	public readonly RoadLineData PreLines;

	public RoadLineData(List<int> path, RoadLineData prePathData, int step)
	{
		Path = path;
		Head = path[0];
		Tail = path[path.Count - 1];
		PreLines = prePathData;
		Step = step;
	}

	public async UniTask<bool> InstantiateRoad(Color _color)
	{
		EffectInfoConfigure effectDataConfigure = 2001.GetEffectDataConfigure();
		_RoadLine = await InstantiateLine(effectDataConfigure.EffectName);
		if (_RoadLine == null)
		{
			return false;
		}
		LineRenderer component = _RoadLine.GetComponent<LineRenderer>();
		component.materials[0].SetTextureScale(Shader.PropertyToID("_MainTex"), new Vector2(Path.Count * 2, 1f));
		component.materials[0].SetColor(Shader.PropertyToID("_MainColor"), _color);
		_RoadLine.transform.SetParent(BattleSceneController.inst.SceneUI.transform);
		return true;
	}

	private async UniTask<GameObject> InstantiateLine(string key)
	{
		CtsInfo createRoadCts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		GameObject gameObject = await Addressables.InstantiateAsync(key).WithCancellation(createRoadCts.Token);
		if (createRoadCts.IsCancellationRequested)
		{
			return null;
		}
		SimpleSingletonProvider<DelaySignalManager>.inst.DisposeCts(createRoadCts);
		UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(Path[0]);
		gameObject.transform.InverseTransformPoint(landById.transform.position);
		LineRenderer[] componentsInChildren = gameObject.GetComponentsInChildren<LineRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].positionCount = Path.Count;
			for (int j = 0; j < Path.Count; j++)
			{
				UnitLand landById2 = SimpleSingletonProvider<LandManager>.inst.GetLandById(Path[j]);
				Vector3 position = new Vector3(landById2.transform.position.x, landById2.transform.position.y + 4f, landById2.transform.position.z);
				componentsInChildren[i].SetPosition(j, position);
			}
		}
		gameObject.transform.SetParent(BattleSceneController.inst.SceneUI.transform);
		return gameObject;
	}

	public async UniTask<GameObject> InstantiateDirection(string key)
	{
		_Direction = await InstantiateLine(key);
		LineRenderer[] componentsInChildren = _Direction.GetComponentsInChildren<LineRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			int positionCount = componentsInChildren[i].positionCount;
			componentsInChildren[i].materials[0].SetFloat(Shader.PropertyToID("_Float2"), (float)positionCount * 2f);
		}
		return _Direction;
	}

	public void DestroyRoadLine()
	{
		if (_RoadLine != null)
		{
			Addressables.Release(_RoadLine);
		}
		DestroyDirection();
	}

	public void DestroyDirection()
	{
		if (_Direction != null)
		{
			Addressables.Release(_Direction);
			_Direction = null;
		}
	}

	private bool Equals(int _head, int _tail, List<int> _path)
	{
		if (_path == null || Path == null)
		{
			return false;
		}
		if (Head != _head)
		{
			return false;
		}
		if (Tail != _tail)
		{
			return false;
		}
		if (Path.Count != _path.Count)
		{
			return false;
		}
		for (int i = 0; i < Path.Count; i++)
		{
			if (Path[i] != _path[i])
			{
				return false;
			}
		}
		return true;
	}

	public bool Equals(RoadLineData other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		return Equals(other.Head, other.Tail, other.Path);
	}
}
