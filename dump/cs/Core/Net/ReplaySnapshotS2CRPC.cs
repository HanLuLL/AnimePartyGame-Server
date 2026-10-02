using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ReplaySnapshotS2CRPC
{
	public delegate UniTask OnReplaySnapshotS2CServerDelegate(ReplaySnapshotS2C model, int errId, bool isDispatch);

	public OnReplaySnapshotS2CServerDelegate OnReplaySnapshotS2CServerCallBackAsync;

	internal virtual async UniTask PushReplaySnapshotS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnReplaySnapshotS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ReplaySnapshotS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ReplaySnapshotS2C model = param.ReadObject<ReplaySnapshotS2C>();
		await OnReplaySnapshotS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
