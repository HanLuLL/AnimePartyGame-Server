using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ClientClickConfirmTaskS2CRPC
{
	public delegate UniTask OnClientClickConfirmTaskS2CServerDelegate(ClientClickConfirmTaskS2C model, int errId, bool isDispatch);

	public OnClientClickConfirmTaskS2CServerDelegate OnClientClickConfirmTaskS2CServerCallBackAsync;

	internal virtual async UniTask PushClientClickConfirmTaskS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnClientClickConfirmTaskS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ClientClickConfirmTaskS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ClientClickConfirmTaskS2C model = param.ReadObject<ClientClickConfirmTaskS2C>();
		await OnClientClickConfirmTaskS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
