using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GambleObServeS2CRPC
{
	public delegate UniTask OnGambleObServeS2CServerDelegate(GambleObServeS2C model, int errId, bool isDispatch);

	public OnGambleObServeS2CServerDelegate OnGambleObServeS2CServerCallBackAsync;

	internal virtual async UniTask PushGambleObServeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGambleObServeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GambleObServeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GambleObServeS2C model = param.ReadObject<GambleObServeS2C>();
		await OnGambleObServeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
