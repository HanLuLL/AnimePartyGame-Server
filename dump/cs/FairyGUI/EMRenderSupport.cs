using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class EMRenderSupport
{
	public static bool orderChanged;

	private static UpdateContext _updateContext;

	private static List<EMRenderTarget> _targets = new List<EMRenderTarget>();

	public static bool packageListReady { get; private set; }

	public static bool hasTarget => _targets.Count > 0;

	public static void Add(EMRenderTarget value)
	{
		if (!_targets.Contains(value))
		{
			_targets.Add(value);
		}
		orderChanged = true;
	}

	public static void Remove(EMRenderTarget value)
	{
		_targets.Remove(value);
	}

	public static void Update()
	{
		if (Application.isPlaying)
		{
			return;
		}
		if (_updateContext == null)
		{
			_updateContext = new UpdateContext();
		}
		if (orderChanged)
		{
			_targets.Sort(CompareDepth);
			orderChanged = false;
		}
		int count = _targets.Count;
		for (int i = 0; i < count; i++)
		{
			_targets[i].EM_BeforeUpdate();
		}
		if (packageListReady)
		{
			_updateContext.Begin();
			for (int j = 0; j < count; j++)
			{
				_targets[j].EM_Update(_updateContext);
			}
			_updateContext.End();
		}
	}

	public static void Reload()
	{
		if (!Application.isPlaying)
		{
			UIConfig.ClearResourceRefs();
			UIConfig[] array = Object.FindObjectsOfType<UIConfig>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Load();
			}
			packageListReady = true;
			int count = _targets.Count;
			for (int j = 0; j < count; j++)
			{
				_targets[j].EM_Reload();
			}
		}
	}

	private static int CompareDepth(EMRenderTarget c1, EMRenderTarget c2)
	{
		return c1.EM_sortingOrder - c2.EM_sortingOrder;
	}
}
