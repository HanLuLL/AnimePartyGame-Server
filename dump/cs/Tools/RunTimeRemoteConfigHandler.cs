using System;
using Cysharp.Threading.Tasks;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

namespace Tools;

public static class RunTimeRemoteConfigHandler
{
	private static JSONNode JsonData;

	public static bool IsAuditMode
	{
		get
		{
			if (JsonData == (object)null)
			{
				return true;
			}
			return JSONNode.op_Implicit(JsonData.GetValueOrDefault("IsAuditMode", JSONNode.op_Implicit(true)));
		}
	}

	public static bool EnableHttps
	{
		get
		{
			if (JsonData == (object)null)
			{
				return false;
			}
			return JSONNode.op_Implicit(JsonData.GetValueOrDefault("EnableHttps", JSONNode.op_Implicit(true)));
		}
	}

	public static bool EnableLogUpload
	{
		get
		{
			if (JsonData == (object)null)
			{
				return false;
			}
			return JSONNode.op_Implicit(JsonData.GetValueOrDefault("EnableLogUpload", JSONNode.op_Implicit(true)));
		}
	}

	public static bool IsAngelMode
	{
		get
		{
			if (JsonData == (object)null)
			{
				return true;
			}
			return JSONNode.op_Implicit(JsonData.GetValueOrDefault("IsAngelMode", JSONNode.op_Implicit(true)));
		}
	}

	public static bool EnableContact
	{
		get
		{
			if (JsonData == (object)null)
			{
				return true;
			}
			string appID = BnSdkInit.Instance.AppID;
			string text = JSONNode.op_Implicit(JsonData.GetValueOrDefault("EnableContact", JSONNode.op_Implicit(string.Empty)));
			if (string.IsNullOrEmpty(text))
			{
				return true;
			}
			return !text.Contains(appID);
		}
	}

	public static async UniTask TryInit()
	{
		JsonData = RemoteConfig.JsonData;
		if (JsonData == (object)null && !(await TryGetRemoteConfig(ServerConfig.REMOTECONFIGURL, ServerConfig.REMOTECONFIGURL)))
		{
			await TryGetRemoteConfig(ServerConfig.REMOTECONFIGURL_DEFAULT, ServerConfig.REMOTECONFIGURL_DEFAULT);
		}
	}

	private static async UniTask<bool> TryGetRemoteConfig(string url, string source)
	{
		UnityWebRequest request = null;
		try
		{
			request = UnityWebRequest.Get(url);
			request.timeout = 10;
			await request.SendWebRequest();
			if ((int)request.result != 1)
			{
				Debug.LogError("[RunTimeRemoteConfigHandler] Get Remote Config " + source + " Error : " + request.error);
				return false;
			}
			JsonData = JSON.Parse(request.downloadHandler.text);
			Debug.Log("[RunTimeRemoteConfigHandler] Get Remote Config from " + source + " URL successful");
			return true;
		}
		catch (Exception arg)
		{
			Debug.LogWarning($"[RunTimeRemoteConfigHandler] Get Remote Config {source} Exception :{arg}");
			return false;
		}
		finally
		{
			UnityWebRequest obj = request;
			if (obj != null)
			{
				obj.Dispose();
			}
		}
	}
}
