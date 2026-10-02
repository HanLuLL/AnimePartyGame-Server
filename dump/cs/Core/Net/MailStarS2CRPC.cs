using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MailStarS2CRPC
{
	public delegate UniTask OnMailStarS2CServerDelegate(MailStarS2C model, int errId, bool isDispatch);

	public OnMailStarS2CServerDelegate OnMailStarS2CServerCallBackAsync;

	internal virtual async UniTask PushMailStarS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMailStarS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MailStarS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MailStarS2C model = param.ReadObject<MailStarS2C>();
		await OnMailStarS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
