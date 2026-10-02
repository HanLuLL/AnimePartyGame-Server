using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GachaS2CRPC
{
	public delegate UniTask OnGachaS2CServerDelegate(GachaS2C model, int errId, bool isDispatch);

	public OnGachaS2CServerDelegate OnGachaS2CServerCallBackAsync;

	internal virtual async UniTask PushGachaS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGachaS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GachaS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GachaS2C model = param.ReadObject<GachaS2C>();
		await OnGachaS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
