using System;
using UnityEngine;

namespace Core;

public static class AndroidUtility
{
	private static readonly AndroidJavaClass pluginClass;

	static AndroidUtility()
	{
		pluginClass = new AndroidJavaClass("com.ecnet.astralparty.AndroidUtility_Java");
	}

	public static int Ping(string host, int timeout)
	{
		try
		{
			return (int)pluginClass.CallStatic<long>("ping", new object[2] { host, timeout });
		}
		catch (Exception arg)
		{
			Debug.LogError($"GetRtt Error:{arg}");
		}
		return 999;
	}

	public static string GetDeviceSocInfo()
	{
		try
		{
			return pluginClass.CallStatic<string>("GetDeviceSoc", Array.Empty<object>());
		}
		catch (Exception arg)
		{
			Debug.LogError($"Get Android Device Error:{arg}");
		}
		return "";
	}
}
