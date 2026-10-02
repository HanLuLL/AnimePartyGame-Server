using Tools;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public class SummonPool : BasePool<GameObject>
{
	public SummonPool(AsyncOperationHandle<GameObject> _prefabHandle)
		: base(_prefabHandle, collectionCheck: true)
	{
	}

	public override GameObject OnCreatePoolItem()
	{
		GameObject gameObject = Object.Instantiate(prefab);
		gameObject.name = prefab.name;
		gameObject.gameObject.SetActiveEx(active: false);
		return gameObject;
	}

	public override void OnGetPoolItem(GameObject _effect)
	{
		_effect.gameObject.SetActiveEx(active: true);
		base.OnGetPoolItem(_effect);
	}

	public override void OnReleasePoolItem(GameObject _effect)
	{
		if (_effect != null)
		{
			_effect.transform.parent = null;
			_effect.gameObject.SetActiveEx(active: false);
			base.OnReleasePoolItem(_effect);
		}
	}

	public override void OnDestroyPoolItem(GameObject _effect)
	{
		if (_effect != null)
		{
			Object.Destroy(_effect);
		}
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
	}
}
