using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ReturnInfoS2CRPC
{
	public delegate UniTask OnReturnInfoS2CServerDelegate(ReturnInfoS2C model, int errId, bool isDispatch);

	public OnReturnInfoS2CServerDelegate OnReturnInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushReturnInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnReturnInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ReturnInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ReturnInfoS2C model = param.ReadObject<ReturnInfoS2C>();
		await OnReturnInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
