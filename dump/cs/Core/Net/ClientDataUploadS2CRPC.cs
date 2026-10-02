using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ClientDataUploadS2CRPC
{
	public delegate UniTask OnClientDataUploadS2CServerDelegate(ClientDataUploadS2C model, int errId, bool isDispatch);

	public OnClientDataUploadS2CServerDelegate OnClientDataUploadS2CServerCallBackAsync;

	internal virtual async UniTask PushClientDataUploadS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnClientDataUploadS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ClientDataUploadS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ClientDataUploadS2C model = param.ReadObject<ClientDataUploadS2C>();
		await OnClientDataUploadS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
