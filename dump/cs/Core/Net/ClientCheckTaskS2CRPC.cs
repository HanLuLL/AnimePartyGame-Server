using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ClientCheckTaskS2CRPC
{
	public delegate UniTask OnClientCheckTaskS2CServerDelegate(ClientCheckTaskS2C model, int errId, bool isDispatch);

	public OnClientCheckTaskS2CServerDelegate OnClientCheckTaskS2CServerCallBackAsync;

	internal virtual async UniTask PushClientCheckTaskS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnClientCheckTaskS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ClientCheckTaskS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ClientCheckTaskS2C model = param.ReadObject<ClientCheckTaskS2C>();
		await OnClientCheckTaskS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
