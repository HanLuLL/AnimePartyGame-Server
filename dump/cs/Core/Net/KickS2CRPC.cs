using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class KickS2CRPC
{
	public delegate UniTask OnKickS2CServerDelegate(KickS2C model, int errId, bool isDispatch);

	public OnKickS2CServerDelegate OnKickS2CServerCallBackAsync;

	internal virtual async UniTask PushKickS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnKickS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议KickS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		KickS2C model = param.ReadObject<KickS2C>();
		await OnKickS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
