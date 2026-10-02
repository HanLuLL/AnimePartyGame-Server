using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class AgeVerifyS2CRPC
{
	public delegate UniTask OnAgeVerifyS2CServerDelegate(AgeVerifyS2C model, int errId, bool isDispatch);

	public OnAgeVerifyS2CServerDelegate OnAgeVerifyS2CServerCallBackAsync;

	internal virtual async UniTask PushAgeVerifyS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnAgeVerifyS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议AgeVerifyS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		AgeVerifyS2C model = param.ReadObject<AgeVerifyS2C>();
		await OnAgeVerifyS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
