using System.Diagnostics;
using UnityEngine;

namespace GameLogic.Replay;

internal static class ReplayLog
{
	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void Log(string message)
	{
		Debug.Log(message);
	}

	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void Warn(string message)
	{
		Debug.LogWarning(message);
	}

	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void DevError(string message)
	{
		Debug.LogError(message);
	}
}
