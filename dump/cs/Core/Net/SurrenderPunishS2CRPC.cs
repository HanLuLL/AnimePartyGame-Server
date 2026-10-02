using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SurrenderPunishS2CRPC
{
	public delegate UniTask OnSurrenderPunishS2CServerDelegate(SurrenderPunishS2C model, int errId, bool isDispatch);

	public OnSurrenderPunishS2CServerDelegate OnSurrenderPunishS2CServerCallBackAsync;

	internal virtual async UniTask PushSurrenderPunishS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSurrenderPunishS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SurrenderPunishS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SurrenderPunishS2C model = param.ReadObject<SurrenderPunishS2C>();
		await OnSurrenderPunishS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
