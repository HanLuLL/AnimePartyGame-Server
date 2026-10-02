using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class AccuseS2CRPC
{
	public delegate UniTask OnAccuseS2CServerDelegate(AccuseS2C model, int errId, bool isDispatch);

	public OnAccuseS2CServerDelegate OnAccuseS2CServerCallBackAsync;

	internal virtual async UniTask PushAccuseS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnAccuseS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议AccuseS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		AccuseS2C model = param.ReadObject<AccuseS2C>();
		await OnAccuseS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
