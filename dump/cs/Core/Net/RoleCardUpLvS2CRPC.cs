using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoleCardUpLvS2CRPC
{
	public delegate UniTask OnRoleCardUpLvS2CServerDelegate(RoleCardUpLvS2C model, int errId, bool isDispatch);

	public OnRoleCardUpLvS2CServerDelegate OnRoleCardUpLvS2CServerCallBackAsync;

	internal virtual async UniTask PushRoleCardUpLvS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoleCardUpLvS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoleCardUpLvS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoleCardUpLvS2C model = param.ReadObject<RoleCardUpLvS2C>();
		await OnRoleCardUpLvS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
