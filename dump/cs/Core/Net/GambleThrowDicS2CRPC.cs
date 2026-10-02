using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GambleThrowDicS2CRPC
{
	public delegate UniTask OnGambleThrowDicS2CServerDelegate(GambleThrowDicS2C model, int errId, bool isDispatch);

	public OnGambleThrowDicS2CServerDelegate OnGambleThrowDicS2CServerCallBackAsync;

	internal virtual async UniTask PushGambleThrowDicS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGambleThrowDicS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GambleThrowDicS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GambleThrowDicS2C model = param.ReadObject<GambleThrowDicS2C>();
		await OnGambleThrowDicS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
