using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetGuildInfoS2CRPC
{
	public delegate UniTask OnGetGuildInfoS2CServerDelegate(GetGuildInfoS2C model, int errId, bool isDispatch);

	public OnGetGuildInfoS2CServerDelegate OnGetGuildInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushGetGuildInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetGuildInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetGuildInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetGuildInfoS2C model = param.ReadObject<GetGuildInfoS2C>();
		await OnGetGuildInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
