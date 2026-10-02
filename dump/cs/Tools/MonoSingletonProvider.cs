using UnityEngine;

namespace Tools;

public class MonoSingletonProvider<T> : MonoBehaviour where T : Component
{
	private static T _inst;

	public static T inst
	{
		get
		{
			if (_inst == null)
			{
				GameObject obj = new GameObject();
				_inst = (T)obj.AddComponent(typeof(T));
				obj.name = "---<Singleton>---" + typeof(T).Name;
				obj.hideFlags = HideFlags.DontSave;
				Object.DontDestroyOnLoad(obj);
				(_inst as MonoSingletonProvider<T>)?.InstanceInit();
			}
			return _inst;
		}
	}

	public static bool hasInstance => _inst != null;

	protected virtual void InstanceInit()
	{
	}

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
	{
	}

	protected virtual void OnDestroy()
	{
		_inst = null;
		Resources.UnloadUnusedAssets();
	}

	protected virtual void OnApplicationQuit()
	{
		DestroyInst();
	}

	protected virtual void DestroyInst()
	{
		Object.Destroy(base.gameObject);
	}
}
