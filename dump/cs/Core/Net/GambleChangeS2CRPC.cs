using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GambleChangeS2CRPC
{
	public delegate UniTask OnGambleChangeS2CServerDelegate(GambleChangeS2C model, int errId, bool isDispatch);

	public OnGambleChangeS2CServerDelegate OnGambleChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushGambleChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGambleChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GambleChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GambleChangeS2C model = param.ReadObject<GambleChangeS2C>();
		await OnGambleChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
