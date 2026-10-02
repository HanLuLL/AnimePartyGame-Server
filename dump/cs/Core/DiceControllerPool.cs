using Tools;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public class DiceControllerPool : BasePool<DiceController>
{
	public DiceControllerPool(AsyncOperationHandle<GameObject> _prefabHandle)
		: base(_prefabHandle, collectionCheck: true)
	{
	}

	public override DiceController OnCreatePoolItem()
	{
		return Object.Instantiate(prefab).GetComponent<DiceController>();
	}

	public override void OnGetPoolItem(DiceController obj)
	{
		obj.gameObject.SetActiveEx(active: true);
		base.OnGetPoolItem(obj);
	}

	public override void OnReleasePoolItem(DiceController obj)
	{
		if (obj != null)
		{
			obj.gameObject.SetActiveEx(active: false);
		}
		base.OnReleasePoolItem(obj);
	}

	public override void OnDestroyPoolItem(DiceController obj)
	{
		if (obj != null)
		{
			Object.Destroy(obj.gameObject);
		}
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
	}
}
