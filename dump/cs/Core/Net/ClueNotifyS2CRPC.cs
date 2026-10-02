using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ClueNotifyS2CRPC
{
	public delegate UniTask OnClueNotifyS2CServerDelegate(ClueNotifyS2C model, int errId, bool isDispatch);

	public OnClueNotifyS2CServerDelegate OnClueNotifyS2CServerCallBackAsync;

	internal virtual async UniTask PushClueNotifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnClueNotifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ClueNotifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ClueNotifyS2C model = param.ReadObject<ClueNotifyS2C>();
		await OnClueNotifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
