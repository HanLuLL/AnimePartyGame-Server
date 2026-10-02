using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SyncGuildMemberS2CRPC
{
	public delegate UniTask OnSyncGuildMemberS2CServerDelegate(SyncGuildMemberS2C model, int errId, bool isDispatch);

	public OnSyncGuildMemberS2CServerDelegate OnSyncGuildMemberS2CServerCallBackAsync;

	internal virtual async UniTask PushSyncGuildMemberS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSyncGuildMemberS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SyncGuildMemberS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SyncGuildMemberS2C model = param.ReadObject<SyncGuildMemberS2C>();
		await OnSyncGuildMemberS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
