using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoleCardChangeS2CRPC
{
	public delegate UniTask OnRoleCardChangeS2CServerDelegate(RoleCardChangeS2C model, int errId, bool isDispatch);

	public OnRoleCardChangeS2CServerDelegate OnRoleCardChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushRoleCardChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoleCardChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoleCardChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoleCardChangeS2C model = param.ReadObject<RoleCardChangeS2C>();
		await OnRoleCardChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
