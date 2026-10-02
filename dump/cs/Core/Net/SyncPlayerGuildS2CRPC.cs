using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SyncPlayerGuildS2CRPC
{
	public delegate UniTask OnSyncPlayerGuildS2CServerDelegate(SyncPlayerGuildS2C model, int errId, bool isDispatch);

	public OnSyncPlayerGuildS2CServerDelegate OnSyncPlayerGuildS2CServerCallBackAsync;

	internal virtual async UniTask PushSyncPlayerGuildS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSyncPlayerGuildS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SyncPlayerGuildS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SyncPlayerGuildS2C model = param.ReadObject<SyncPlayerGuildS2C>();
		await OnSyncPlayerGuildS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
