using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GmS2CRPC
{
	public delegate UniTask OnGmS2CServerDelegate(GmS2C model, int errId, bool isDispatch);

	public OnGmS2CServerDelegate OnGmS2CServerCallBackAsync;

	internal virtual async UniTask PushGmS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGmS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GmS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GmS2C model = param.ReadObject<GmS2C>();
		await OnGmS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
