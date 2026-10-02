using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class RollGoldS2CRPC
{
	public delegate UniTask OnRollGoldS2CServerDelegate(RollGoldS2C model, int errId, bool isDispatch);

	public OnRollGoldS2CServerDelegate OnRollGoldS2CServerCallBackAsync;

	internal virtual async UniTask PushRollGoldS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnRollGoldS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议RollGoldS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		RollGoldS2C model = param.ReadObject<RollGoldS2C>();
		await OnRollGoldS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
