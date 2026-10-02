using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ClientHarmonyS2CRPC
{
	public delegate UniTask OnClientHarmonyS2CServerDelegate(ClientHarmonyS2C model, int errId, bool isDispatch);

	public OnClientHarmonyS2CServerDelegate OnClientHarmonyS2CServerCallBackAsync;

	internal virtual async UniTask PushClientHarmonyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnClientHarmonyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ClientHarmonyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ClientHarmonyS2C model = param.ReadObject<ClientHarmonyS2C>();
		await OnClientHarmonyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
