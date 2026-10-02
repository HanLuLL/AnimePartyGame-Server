using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MailAddS2CRPC
{
	public delegate UniTask OnMailAddS2CServerDelegate(MailAddS2C model, int errId, bool isDispatch);

	public OnMailAddS2CServerDelegate OnMailAddS2CServerCallBackAsync;

	internal virtual async UniTask PushMailAddS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMailAddS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MailAddS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MailAddS2C model = param.ReadObject<MailAddS2C>();
		await OnMailAddS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
