using System;
using UnityEngine;

namespace Core.Unit;

[Serializable]
public abstract class Unit : MonoBehaviour
{
	public GameObject cachedGo { get; private set; }

	public Transform cachedTrans { get; private set; }

	protected virtual void Awake()
	{
		cachedGo = base.gameObject;
		cachedTrans = base.transform;
	}

	protected virtual void OnDestroy()
	{
		cachedGo = null;
		cachedTrans = null;
	}
}
