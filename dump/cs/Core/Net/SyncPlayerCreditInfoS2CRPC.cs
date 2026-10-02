using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SyncPlayerCreditInfoS2CRPC
{
	public delegate UniTask OnSyncPlayerCreditInfoS2CServerDelegate(SyncPlayerCreditInfoS2C model, int errId, bool isDispatch);

	public OnSyncPlayerCreditInfoS2CServerDelegate OnSyncPlayerCreditInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushSyncPlayerCreditInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSyncPlayerCreditInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SyncPlayerCreditInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SyncPlayerCreditInfoS2C model = param.ReadObject<SyncPlayerCreditInfoS2C>();
		await OnSyncPlayerCreditInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
