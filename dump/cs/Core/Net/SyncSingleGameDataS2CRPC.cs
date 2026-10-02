using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SyncSingleGameDataS2CRPC
{
	public delegate UniTask OnSyncSingleGameDataS2CServerDelegate(SyncSingleGameDataS2C model, int errId, bool isDispatch);

	public OnSyncSingleGameDataS2CServerDelegate OnSyncSingleGameDataS2CServerCallBackAsync;

	internal virtual async UniTask PushSyncSingleGameDataS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSyncSingleGameDataS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SyncSingleGameDataS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SyncSingleGameDataS2C model = param.ReadObject<SyncSingleGameDataS2C>();
		await OnSyncSingleGameDataS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
