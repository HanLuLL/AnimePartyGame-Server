using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoleCardCollectS2CRPC
{
	public delegate UniTask OnRoleCardCollectS2CServerDelegate(RoleCardCollectS2C model, int errId, bool isDispatch);

	public OnRoleCardCollectS2CServerDelegate OnRoleCardCollectS2CServerCallBackAsync;

	internal virtual async UniTask PushRoleCardCollectS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoleCardCollectS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoleCardCollectS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoleCardCollectS2C model = param.ReadObject<RoleCardCollectS2C>();
		await OnRoleCardCollectS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
