using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class BagItemChangeS2CRPC
{
	public delegate UniTask OnBagItemChangeS2CServerDelegate(BagItemChangeS2C model, int errId, bool isDispatch);

	public OnBagItemChangeS2CServerDelegate OnBagItemChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushBagItemChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnBagItemChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议BagItemChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		BagItemChangeS2C model = param.ReadObject<BagItemChangeS2C>();
		await OnBagItemChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
