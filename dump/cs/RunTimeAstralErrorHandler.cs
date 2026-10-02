using System;
using System.Collections;
using System.Text;
using Tools;
using UnityEngine;
using UnityEngine.Networking;

public class RunTimeAstralErrorHandler : MonoBehaviour
{
	public class ErrorInfo
	{
		public string Device;

		public string CPU;

		public string GPU;

		public ulong UID;

		public string Version;

		public string Log;

		public ErrorInfo()
		{
			Version = Application.version;
			Device = $"{SystemInfo.deviceModel}+{SystemInfo.deviceType}+{SystemInfo.deviceUniqueIdentifier}";
			CPU = $"{SystemInfo.processorType}+{SystemInfo.processorCount}+{SystemInfo.systemMemorySize}";
			GPU = $"{SystemInfo.graphicsDeviceName}+{SystemInfo.graphicsMemorySize}+{SystemInfo.graphicsShaderLevel}";
		}
	}

	private string _url;

	private ErrorInfo _errorInfo;

	private string _platformInfo;

	private string _playerNick;

	private float _uploadCooldown;

	private int _continuousUploadCount;

	private float _lastUploadTime = -10f;

	private void Start()
	{
		_url = ServerConfig.WebProtocolHead + "0.0.0.0:8810";
		_errorInfo = new ErrorInfo();
		_platformInfo = GetPlatformChannelInfo();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void OnEnable()
	{
		Application.logMessageReceived += HandleErrorLog;
	}

	public void OnDisable()
	{
		Application.logMessageReceived -= HandleErrorLog;
	}

	private bool CheckUpload()
	{
		float unscaledTime = Time.unscaledTime;
		if (unscaledTime < _uploadCooldown)
		{
			return false;
		}
		if (unscaledTime - _lastUploadTime > 1f)
		{
			_continuousUploadCount = 0;
		}
		_continuousUploadCount++;
		if (_continuousUploadCount > 5)
		{
			_uploadCooldown = unscaledTime + 60f;
			return false;
		}
		_lastUploadTime = unscaledTime;
		return true;
	}

	private void HandleErrorLog(string logString, string stackTrace, LogType type)
	{
		if (!RunTimeRemoteConfigHandler.IsAuditMode && RunTimeRemoteConfigHandler.EnableLogUpload && _errorInfo != null && (type == LogType.Error || type == LogType.Exception))
		{
			_errorInfo.Log = "Nick--" + _playerNick + " Platform--" + _platformInfo + "; Log--" + logString + "; ST--" + stackTrace;
			string postData = JsonUtility.ToJson(_errorInfo, prettyPrint: false);
			if (CheckUpload())
			{
				StartCoroutine(UnityWebRequestPost(_url, postData));
			}
		}
	}

	private IEnumerator UnityWebRequestPost(string url, string postData)
	{
		UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
		try
		{
			byte[] bytes = Encoding.UTF8.GetBytes(postData);
			webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
			webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			webRequest.SetRequestHeader("Content-Type", "application/json");
			yield return webRequest.SendWebRequest();
		}
		finally
		{
			((IDisposable)webRequest)?.Dispose();
		}
	}

	public void UpdatePlayerId(ulong uid, string playerNick)
	{
		if (_errorInfo != null)
		{
			_errorInfo.UID = uid;
			_playerNick = playerNick;
		}
	}

	private string GetPlatformChannelInfo()
	{
		string text = Application.platform.ToString();
		string text2 = PackageChannel.Get();
		if (!string.IsNullOrEmpty(text2))
		{
			return text + "_" + text2;
		}
		return text;
	}
}
