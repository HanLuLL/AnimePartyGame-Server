using System;
using System.Collections;
using System.Text;
using Core.Net;
using Cysharp.Threading.Tasks;
using GameLogic;
using SimpleJSON;
using SinglePlayer;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Networking;
using party.model;

namespace Core;

public class WebServerManager : SimpleSingletonProvider<WebServerManager>
{
	public async UniTask ShowNoticeWindow()
	{
		UGUIRoot uGUIRoot = UGUIController.GetUGUIRoot();
		if ((UnityEngine.Object)(object)uGUIRoot != null)
		{
			CommonUIManager.ShowModalWait();
			await uGUIRoot.OpenNoticeWindowInRuntime((System.Action)CommonUIManager.CloseModalWait);
		}
	}

	public void PostGoodsRecord(int goodsId, ShopTabType shopType, LogToServerType logToServerType)
	{
		StoreRecordToServer obj = new StoreRecordToServer
		{
			id = "1",
			uid = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID().ToString(),
			storeAction = logToServerType,
			goods_id = goodsId.ToString()
		};
		int num = (int)shopType;
		obj.shop_id = num.ToString();
		string postData = JsonUtility.ToJson(obj);
		string url = $"{ServerConfig.WebProtocolHead}{GameSettings.LogServerIP}:{GameSettings.LogSeverPort}";
		MonoSingletonProvider<CoroutineManager>.inst.CreateCoroutine(UnityWebRequestPost(url, postData)).Start();
	}

	public void PostGuideRecord(int guideId, int guideStep)
	{
		string postData = JsonUtility.ToJson(new GuideRecordToServer
		{
			id = "1",
			uid = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID(),
			guideId = guideId,
			stepId = guideStep
		});
		string url = $"{ServerConfig.WebProtocolHead}{GameSettings.LogServerIP}:{GameSettings.LogSeverPort}";
		MonoSingletonProvider<CoroutineManager>.inst.CreateCoroutine(UnityWebRequestPost(url, postData)).Start();
	}

	public void PostTutorialRecord(int tutorialId)
	{
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		if (playerInfo != null)
		{
			string postData = JsonUtility.ToJson(new GuideRecordToServer
			{
				id = playerInfo.ServerId,
				uid = playerInfo.Id,
				guideId = tutorialId
			});
			string url = $"{ServerConfig.WebProtocolHead}{GameSettings.LogServerIP}:{GameSettings.LogSeverPort}";
			MonoSingletonProvider<CoroutineManager>.inst.CreateCoroutine(UnityWebRequestPost(url, postData)).Start();
		}
	}

	public void PostSkipLinkRecord(LogToServerType type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		string postData = ((object)new JSONObject
		{
			["uid"] = JSONNode.op_Implicit(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID()),
			["type"] = JSONNode.op_Implicit((int)type),
			["msg"] = JSONNode.op_Implicit("Link")
		}).ToString();
		string url = $"{ServerConfig.WebProtocolHead}{GameSettings.LogServerIP}:{GameSettings.LogSeverPort}";
		MonoSingletonProvider<CoroutineManager>.inst.CreateCoroutine(UnityWebRequestPost(url, postData)).Start();
	}

	public void PostSinglePlayer(SinglePlayerLogType type, bool? pass = null, int? score = null, int? gameProgress = null)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		JSONNode val = (JSONNode)new JSONObject();
		string text = "";
		switch (type)
		{
		case SinglePlayerLogType.Start:
			text = "single_start";
			break;
		case SinglePlayerLogType.Restart:
			text = "single_restart";
			break;
		case SinglePlayerLogType.End:
			text = "single_end";
			break;
		}
		val["msg"] = JSONNode.op_Implicit(text);
		val["time"] = JSONNode.op_Implicit(MonoSingletonProvider<NetManager>.inst.ServerTime.ToString());
		val["serverId"] = JSONNode.op_Implicit(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().ServerId);
		val["uid"] = JSONNode.op_Implicit(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID());
		if (type == SinglePlayerLogType.End)
		{
			bool? flag = pass;
			val["pass"] = (flag.HasValue ? JSONNode.op_Implicit(flag == true) : null);
			int? num = score;
			val["score"] = (num.HasValue ? JSONNode.op_Implicit(num.GetValueOrDefault()) : null);
			num = gameProgress;
			val["gameProgress"] = (num.HasValue ? JSONNode.op_Implicit(num.GetValueOrDefault()) : null);
		}
		string url = $"{ServerConfig.WebProtocolHead}{GameSettings.LogServerIP}:{GameSettings.LogSeverPort}";
		string text2 = ((object)val).ToString();
		Debug.Log("#单人玩法# 埋点 ：" + text2);
		MonoSingletonProvider<CoroutineManager>.inst.CreateCoroutine(UnityWebRequestPost(url, text2)).Start();
	}

	public IEnumerator UnityWebRequestPost(string url, string postData, Action<string> callBack = null)
	{
		UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
		try
		{
			byte[] bytes = Encoding.UTF8.GetBytes(postData);
			webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
			webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			webRequest.SetRequestHeader("Content-Type", "application/json");
			yield return webRequest.SendWebRequest();
			if ((int)webRequest.result == 2)
			{
				Debug.Log(webRequest.error);
				yield break;
			}
			string text = webRequest.downloadHandler.text;
			callBack?.Invoke(text);
		}
		finally
		{
			((IDisposable)webRequest)?.Dispose();
		}
	}
}
