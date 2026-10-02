using Tools;
using UnityEngine;

namespace Core;

public class GachaDicePool : BasePool<GachaDice>
{
	public GachaDicePool(GameObject _prefab, bool collectionCheck = true)
		: base(_prefab, collectionCheck)
	{
	}

	public override GachaDice OnCreatePoolItem()
	{
		GameObject gameObject = Object.Instantiate(prefab);
		gameObject.gameObject.SetActiveEx(active: false);
		return gameObject.GetComponent<GachaDice>();
	}

	public override void OnGetPoolItem(GachaDice obj)
	{
		obj.Reset();
		obj.gameObject.SetActiveEx(active: true);
		base.OnGetPoolItem(obj);
	}

	public override void OnReleasePoolItem(GachaDice obj)
	{
		obj.gameObject.SetActiveEx(active: false);
		base.OnReleasePoolItem(obj);
	}

	public override void OnDestroyPoolItem(GachaDice obj)
	{
		if (obj != null)
		{
			Object.Destroy(obj);
		}
	}

	public override void OnDestroy()
	{
		pool?.Clear();
	}
}
