using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RoleCardChoiceResS2CRPC
{
	public delegate UniTask OnRoleCardChoiceResS2CServerDelegate(RoleCardChoiceResS2C model, int errId, bool isDispatch);

	public OnRoleCardChoiceResS2CServerDelegate OnRoleCardChoiceResS2CServerCallBackAsync;

	internal virtual async UniTask PushRoleCardChoiceResS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRoleCardChoiceResS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RoleCardChoiceResS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RoleCardChoiceResS2C model = param.ReadObject<RoleCardChoiceResS2C>();
		await OnRoleCardChoiceResS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
