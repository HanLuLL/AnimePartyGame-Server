using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class LoopNoticeS2CRPC
{
	public delegate UniTask OnLoopNoticeS2CServerDelegate(LoopNoticeS2C model, int errId, bool isDispatch);

	public OnLoopNoticeS2CServerDelegate OnLoopNoticeS2CServerCallBackAsync;

	internal virtual async UniTask PushLoopNoticeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnLoopNoticeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议LoopNoticeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		LoopNoticeS2C model = param.ReadObject<LoopNoticeS2C>();
		await OnLoopNoticeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
