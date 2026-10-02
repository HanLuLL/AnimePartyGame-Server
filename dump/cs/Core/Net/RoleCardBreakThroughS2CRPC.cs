using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoleCardBreakThroughS2CRPC
{
	public delegate UniTask OnRoleCardBreakThroughS2CServerDelegate(RoleCardBreakThroughS2C model, int errId, bool isDispatch);

	public OnRoleCardBreakThroughS2CServerDelegate OnRoleCardBreakThroughS2CServerCallBackAsync;

	internal virtual async UniTask PushRoleCardBreakThroughS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoleCardBreakThroughS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoleCardBreakThroughS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoleCardBreakThroughS2C model = param.ReadObject<RoleCardBreakThroughS2C>();
		await OnRoleCardBreakThroughS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
