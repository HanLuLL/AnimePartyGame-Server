using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChangePraiseNumS2CRPC
{
	public delegate UniTask OnChangePraiseNumS2CServerDelegate(ChangePraiseNumS2C model, int errId, bool isDispatch);

	public OnChangePraiseNumS2CServerDelegate OnChangePraiseNumS2CServerCallBackAsync;

	internal virtual async UniTask PushChangePraiseNumS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChangePraiseNumS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChangePraiseNumS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChangePraiseNumS2C model = param.ReadObject<ChangePraiseNumS2C>();
		await OnChangePraiseNumS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
