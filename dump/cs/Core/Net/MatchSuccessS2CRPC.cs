using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class MatchSuccessS2CRPC
{
	public delegate UniTask OnMatchSuccessS2CServerDelegate(MatchSuccessS2C model, int errId, bool isDispatch);

	public OnMatchSuccessS2CServerDelegate OnMatchSuccessS2CServerCallBackAsync;

	internal virtual async UniTask PushMatchSuccessS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnMatchSuccessS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议MatchSuccessS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		MatchSuccessS2C model = param.ReadObject<MatchSuccessS2C>();
		await OnMatchSuccessS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
