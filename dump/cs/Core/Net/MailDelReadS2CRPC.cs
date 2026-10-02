using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MailDelReadS2CRPC
{
	public delegate UniTask OnMailDelReadS2CServerDelegate(MailDelReadS2C model, int errId, bool isDispatch);

	public OnMailDelReadS2CServerDelegate OnMailDelReadS2CServerCallBackAsync;

	internal virtual async UniTask PushMailDelReadS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMailDelReadS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MailDelReadS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MailDelReadS2C model = param.ReadObject<MailDelReadS2C>();
		await OnMailDelReadS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
