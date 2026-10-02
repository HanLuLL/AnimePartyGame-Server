using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SyncGuildS2CRPC
{
	public delegate UniTask OnSyncGuildS2CServerDelegate(SyncGuildS2C model, int errId, bool isDispatch);

	public OnSyncGuildS2CServerDelegate OnSyncGuildS2CServerCallBackAsync;

	internal virtual async UniTask PushSyncGuildS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSyncGuildS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SyncGuildS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SyncGuildS2C model = param.ReadObject<SyncGuildS2C>();
		await OnSyncGuildS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
