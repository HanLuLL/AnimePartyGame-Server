using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SyncRelicsS2CRPC
{
	public delegate UniTask OnSyncRelicsS2CServerDelegate(SyncRelicsS2C model, int errId, bool isDispatch);

	public OnSyncRelicsS2CServerDelegate OnSyncRelicsS2CServerCallBackAsync;

	internal virtual async UniTask PushSyncRelicsS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSyncRelicsS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SyncRelicsS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SyncRelicsS2C model = param.ReadObject<SyncRelicsS2C>();
		await OnSyncRelicsS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
