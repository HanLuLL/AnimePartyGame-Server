using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PlayerUseItemS2CRPC
{
	public delegate UniTask OnPlayerUseItemS2CServerDelegate(PlayerUseItemS2C model, int errId, bool isDispatch);

	public OnPlayerUseItemS2CServerDelegate OnPlayerUseItemS2CServerCallBackAsync;

	internal virtual async UniTask PushPlayerUseItemS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPlayerUseItemS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PlayerUseItemS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PlayerUseItemS2C model = param.ReadObject<PlayerUseItemS2C>();
		await OnPlayerUseItemS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
