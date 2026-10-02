using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class NoticeS2CRPC
{
	public delegate UniTask OnNoticeS2CServerDelegate(NoticeS2C model, int errId, bool isDispatch);

	public OnNoticeS2CServerDelegate OnNoticeS2CServerCallBackAsync;

	internal virtual async UniTask PushNoticeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnNoticeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议NoticeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		NoticeS2C model = param.ReadObject<NoticeS2C>();
		await OnNoticeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
