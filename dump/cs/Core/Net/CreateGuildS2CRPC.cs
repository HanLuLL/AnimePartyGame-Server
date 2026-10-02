using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class CreateGuildS2CRPC
{
	public delegate UniTask OnCreateGuildS2CServerDelegate(CreateGuildS2C model, int errId, bool isDispatch);

	public OnCreateGuildS2CServerDelegate OnCreateGuildS2CServerCallBackAsync;

	internal virtual async UniTask PushCreateGuildS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnCreateGuildS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议CreateGuildS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		CreateGuildS2C model = param.ReadObject<CreateGuildS2C>();
		await OnCreateGuildS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
