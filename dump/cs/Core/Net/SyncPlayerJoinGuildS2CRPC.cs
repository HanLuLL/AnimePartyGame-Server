using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SyncPlayerJoinGuildS2CRPC
{
	public delegate UniTask OnSyncPlayerJoinGuildS2CServerDelegate(SyncPlayerJoinGuildS2C model, int errId, bool isDispatch);

	public OnSyncPlayerJoinGuildS2CServerDelegate OnSyncPlayerJoinGuildS2CServerCallBackAsync;

	internal virtual async UniTask PushSyncPlayerJoinGuildS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSyncPlayerJoinGuildS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SyncPlayerJoinGuildS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SyncPlayerJoinGuildS2C model = param.ReadObject<SyncPlayerJoinGuildS2C>();
		await OnSyncPlayerJoinGuildS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
