using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class CancelMatchS2CRPC
{
	public delegate UniTask OnCancelMatchS2CServerDelegate(CancelMatchS2C model, int errId, bool isDispatch);

	public OnCancelMatchS2CServerDelegate OnCancelMatchS2CServerCallBackAsync;

	internal virtual async UniTask PushCancelMatchS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnCancelMatchS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议CancelMatchS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		CancelMatchS2C model = param.ReadObject<CancelMatchS2C>();
		await OnCancelMatchS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
