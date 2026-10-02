using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChangeNameS2CRPC
{
	public delegate UniTask OnChangeNameS2CServerDelegate(ChangeNameS2C model, int errId, bool isDispatch);

	public OnChangeNameS2CServerDelegate OnChangeNameS2CServerCallBackAsync;

	internal virtual async UniTask PushChangeNameS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChangeNameS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChangeNameS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChangeNameS2C model = param.ReadObject<ChangeNameS2C>();
		await OnChangeNameS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
