using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MailDelS2CRPC
{
	public delegate UniTask OnMailDelS2CServerDelegate(MailDelS2C model, int errId, bool isDispatch);

	public OnMailDelS2CServerDelegate OnMailDelS2CServerCallBackAsync;

	internal virtual async UniTask PushMailDelS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMailDelS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MailDelS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MailDelS2C model = param.ReadObject<MailDelS2C>();
		await OnMailDelS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
