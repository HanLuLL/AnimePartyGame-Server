using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetReturnInfoS2CRPC
{
	public delegate UniTask OnGetReturnInfoS2CServerDelegate(GetReturnInfoS2C model, int errId, bool isDispatch);

	public OnGetReturnInfoS2CServerDelegate OnGetReturnInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushGetReturnInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetReturnInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetReturnInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetReturnInfoS2C model = param.ReadObject<GetReturnInfoS2C>();
		await OnGetReturnInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
