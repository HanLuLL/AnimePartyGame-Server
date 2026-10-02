using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetGuildsInfoS2CRPC
{
	public delegate UniTask OnGetGuildsInfoS2CServerDelegate(GetGuildsInfoS2C model, int errId, bool isDispatch);

	public OnGetGuildsInfoS2CServerDelegate OnGetGuildsInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushGetGuildsInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetGuildsInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetGuildsInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetGuildsInfoS2C model = param.ReadObject<GetGuildsInfoS2C>();
		await OnGetGuildsInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
