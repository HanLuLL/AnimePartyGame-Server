using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GachaCountS2CRPC
{
	public delegate UniTask OnGachaCountS2CServerDelegate(GachaCountS2C model, int errId, bool isDispatch);

	public OnGachaCountS2CServerDelegate OnGachaCountS2CServerCallBackAsync;

	internal virtual async UniTask PushGachaCountS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGachaCountS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GachaCountS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GachaCountS2C model = param.ReadObject<GachaCountS2C>();
		await OnGachaCountS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
