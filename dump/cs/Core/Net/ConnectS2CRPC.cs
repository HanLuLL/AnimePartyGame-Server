using System;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ConnectS2CRPC
{
	public delegate UniTask OnConnectS2CServerDelegate(ConnectS2C model, int errId, bool isDispatch);

	public OnConnectS2CServerDelegate OnConnectS2CServerCallBackAsync;

	internal virtual async UniTask PushConnectS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnConnectS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ConnectS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		try
		{
			ConnectS2C model = param.ReadObject<ConnectS2C>();
			await OnConnectS2CServerCallBackAsync(model, errId, isDispatch);
		}
		catch (Exception)
		{
			MonoSingletonProvider<NetManager>.inst.ReLogin();
		}
	}
}
