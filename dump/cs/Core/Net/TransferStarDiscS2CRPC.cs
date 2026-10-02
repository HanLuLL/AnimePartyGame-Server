using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class TransferStarDiscS2CRPC
{
	public delegate UniTask OnTransferStarDiscS2CServerDelegate(TransferStarDiscS2C model, int errId, bool isDispatch);

	public OnTransferStarDiscS2CServerDelegate OnTransferStarDiscS2CServerCallBackAsync;

	internal virtual async UniTask PushTransferStarDiscS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnTransferStarDiscS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议TransferStarDiscS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		TransferStarDiscS2C model = param.ReadObject<TransferStarDiscS2C>();
		await OnTransferStarDiscS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
