using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

internal static class TweenManager
{
	private class TweenEngine : MonoBehaviour
	{
		private void Update()
		{
			TweenManager.Update();
		}
	}

	private static GTweener[] _activeTweens = new GTweener[30];

	private static List<GTweener> _tweenerPool = new List<GTweener>(30);

	private static int _totalActiveTweens = 0;

	private static bool _inited = false;

	internal static GTweener CreateTween()
	{
		if (!_inited)
		{
			Init();
		}
		int count = _tweenerPool.Count;
		GTweener gTweener;
		if (count > 0)
		{
			gTweener = _tweenerPool[count - 1];
			_tweenerPool.RemoveAt(count - 1);
		}
		else
		{
			gTweener = new GTweener();
		}
		gTweener._Init();
		_activeTweens[_totalActiveTweens++] = gTweener;
		if (_totalActiveTweens == _activeTweens.Length)
		{
			GTweener[] array = new GTweener[_activeTweens.Length + Mathf.CeilToInt((float)_activeTweens.Length * 0.5f)];
			_activeTweens.CopyTo(array, 0);
			_activeTweens = array;
		}
		return gTweener;
	}

	internal static bool IsTweening(object target, TweenPropType propType)
	{
		if (target == null)
		{
			return false;
		}
		bool flag = propType == TweenPropType.None;
		for (int i = 0; i < _totalActiveTweens; i++)
		{
			GTweener gTweener = _activeTweens[i];
			if (gTweener != null && gTweener.target == target && !gTweener._killed && (flag || gTweener._propType == propType))
			{
				return true;
			}
		}
		return false;
	}

	internal static bool KillTweens(object target, TweenPropType propType, bool completed)
	{
		if (target == null)
		{
			return false;
		}
		bool result = false;
		int totalActiveTweens = _totalActiveTweens;
		bool flag = propType == TweenPropType.None;
		for (int i = 0; i < totalActiveTweens; i++)
		{
			GTweener gTweener = _activeTweens[i];
			if (gTweener != null && gTweener.target == target && !gTweener._killed && (flag || gTweener._propType == propType))
			{
				gTweener.Kill(completed);
				result = true;
			}
		}
		return result;
	}

	internal static GTweener GetTween(object target, TweenPropType propType)
	{
		if (target == null)
		{
			return null;
		}
		int totalActiveTweens = _totalActiveTweens;
		bool flag = propType == TweenPropType.None;
		for (int i = 0; i < totalActiveTweens; i++)
		{
			GTweener gTweener = _activeTweens[i];
			if (gTweener != null && gTweener.target == target && !gTweener._killed && (flag || gTweener._propType == propType))
			{
				return gTweener;
			}
		}
		return null;
	}

	internal static void Update()
	{
		int totalActiveTweens = _totalActiveTweens;
		int num = -1;
		for (int i = 0; i < totalActiveTweens; i++)
		{
			GTweener gTweener = _activeTweens[i];
			if (gTweener == null)
			{
				if (num == -1)
				{
					num = i;
				}
				continue;
			}
			if (gTweener._killed)
			{
				gTweener._Reset();
				_tweenerPool.Add(gTweener);
				_activeTweens[i] = null;
				if (num == -1)
				{
					num = i;
				}
				continue;
			}
			if (gTweener._target is GObject && ((GObject)gTweener._target)._disposed)
			{
				gTweener._killed = true;
			}
			else if (!gTweener._paused)
			{
				gTweener._Update();
			}
			if (num != -1)
			{
				_activeTweens[num] = gTweener;
				_activeTweens[i] = null;
				num++;
			}
		}
		if (num < 0)
		{
			return;
		}
		if (_totalActiveTweens != totalActiveTweens)
		{
			int num2 = totalActiveTweens;
			totalActiveTweens = _totalActiveTweens - totalActiveTweens;
			for (int j = 0; j < totalActiveTweens; j++)
			{
				_activeTweens[num++] = _activeTweens[num2];
				_activeTweens[num2] = null;
				num2++;
			}
		}
		_totalActiveTweens = num;
	}

	internal static void Clean()
	{
		_tweenerPool.Clear();
	}

	private static void Init()
	{
		_inited = true;
		if (Application.isPlaying)
		{
			GameObject gameObject = new GameObject("[FairyGUI.TweenManager]");
			gameObject.hideFlags = HideFlags.HideInHierarchy;
			gameObject.SetActive(value: true);
			Object.DontDestroyOnLoad(gameObject);
			gameObject.AddComponent<TweenEngine>();
		}
	}
}
