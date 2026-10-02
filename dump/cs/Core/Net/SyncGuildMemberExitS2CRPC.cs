using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SyncGuildMemberExitS2CRPC
{
	public delegate UniTask OnSyncGuildMemberExitS2CServerDelegate(SyncGuildMemberExitS2C model, int errId, bool isDispatch);

	public OnSyncGuildMemberExitS2CServerDelegate OnSyncGuildMemberExitS2CServerCallBackAsync;

	internal virtual async UniTask PushSyncGuildMemberExitS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSyncGuildMemberExitS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SyncGuildMemberExitS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SyncGuildMemberExitS2C model = param.ReadObject<SyncGuildMemberExitS2C>();
		await OnSyncGuildMemberExitS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
