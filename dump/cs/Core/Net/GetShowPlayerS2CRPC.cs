using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetShowPlayerS2CRPC
{
	public delegate UniTask OnGetShowPlayerS2CServerDelegate(GetShowPlayerS2C model, int errId, bool isDispatch);

	public OnGetShowPlayerS2CServerDelegate OnGetShowPlayerS2CServerCallBackAsync;

	internal virtual async UniTask PushGetShowPlayerS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetShowPlayerS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetShowPlayerS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetShowPlayerS2C model = param.ReadObject<GetShowPlayerS2C>();
		await OnGetShowPlayerS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
