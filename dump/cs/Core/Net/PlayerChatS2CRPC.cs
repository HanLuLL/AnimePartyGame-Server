using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PlayerChatS2CRPC
{
	public delegate UniTask OnPlayerChatS2CServerDelegate(PlayerChatS2C model, int errId, bool isDispatch);

	public OnPlayerChatS2CServerDelegate OnPlayerChatS2CServerCallBackAsync;

	internal virtual async UniTask PushPlayerChatS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPlayerChatS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PlayerChatS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PlayerChatS2C model = param.ReadObject<PlayerChatS2C>();
		await OnPlayerChatS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
