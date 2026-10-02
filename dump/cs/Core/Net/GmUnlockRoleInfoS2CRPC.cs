using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GmUnlockRoleInfoS2CRPC
{
	public delegate UniTask OnGmUnlockRoleInfoS2CServerDelegate(GmUnlockRoleInfoS2C model, int errId, bool isDispatch);

	public OnGmUnlockRoleInfoS2CServerDelegate OnGmUnlockRoleInfoS2CServerCallBackAsync;

	internal virtual async UniTask PushGmUnlockRoleInfoS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGmUnlockRoleInfoS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GmUnlockRoleInfoS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GmUnlockRoleInfoS2C model = param.ReadObject<GmUnlockRoleInfoS2C>();
		await OnGmUnlockRoleInfoS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
