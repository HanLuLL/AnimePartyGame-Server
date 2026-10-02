using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PlayerOnlineS2CRPC
{
	public delegate UniTask OnPlayerOnlineS2CServerDelegate(PlayerOnlineS2C model, int errId, bool isDispatch);

	public OnPlayerOnlineS2CServerDelegate OnPlayerOnlineS2CServerCallBackAsync;

	internal virtual async UniTask PushPlayerOnlineS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPlayerOnlineS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PlayerOnlineS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PlayerOnlineS2C model = param.ReadObject<PlayerOnlineS2C>();
		await OnPlayerOnlineS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
