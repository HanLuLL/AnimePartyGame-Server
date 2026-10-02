using UnityEngine;

namespace FairyGUI;

public class GTween
{
	public static bool catchCallbackExceptions;

	public static GTweener To(float startValue, float endValue, float duration)
	{
		return TweenManager.CreateTween()._To(startValue, endValue, duration);
	}

	public static GTweener To(Vector2 startValue, Vector2 endValue, float duration)
	{
		return TweenManager.CreateTween()._To(startValue, endValue, duration);
	}

	public static GTweener To(Vector3 startValue, Vector3 endValue, float duration)
	{
		return TweenManager.CreateTween()._To(startValue, endValue, duration);
	}

	public static GTweener To(Vector4 startValue, Vector4 endValue, float duration)
	{
		return TweenManager.CreateTween()._To(startValue, endValue, duration);
	}

	public static GTweener To(Color startValue, Color endValue, float duration)
	{
		return TweenManager.CreateTween()._To(startValue, endValue, duration);
	}

	public static GTweener ToDouble(double startValue, double endValue, float duration)
	{
		return TweenManager.CreateTween()._To(startValue, endValue, duration);
	}

	public static GTweener DelayedCall(float delay)
	{
		return TweenManager.CreateTween().SetDelay(delay);
	}

	public static GTweener Shake(Vector3 startValue, float amplitude, float duration)
	{
		return TweenManager.CreateTween()._Shake(startValue, amplitude, duration);
	}

	public static bool IsTweening(object target)
	{
		return TweenManager.IsTweening(target, TweenPropType.None);
	}

	public static bool IsTweening(object target, TweenPropType propType)
	{
		return TweenManager.IsTweening(target, propType);
	}

	public static void Kill(object target)
	{
		TweenManager.KillTweens(target, TweenPropType.None, completed: false);
	}

	public static void Kill(object target, bool complete)
	{
		TweenManager.KillTweens(target, TweenPropType.None, complete);
	}

	public static void Kill(object target, TweenPropType propType, bool complete)
	{
		TweenManager.KillTweens(target, propType, complete);
	}

	public static GTweener GetTween(object target)
	{
		return TweenManager.GetTween(target, TweenPropType.None);
	}

	public static GTweener GetTween(object target, TweenPropType propType)
	{
		return TweenManager.GetTween(target, propType);
	}

	public static void Clean()
	{
		TweenManager.Clean();
	}
}
