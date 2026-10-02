using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class LaborActDiceS2CRPC
{
	public delegate UniTask OnLaborActDiceS2CServerDelegate(LaborActDiceS2C model, int errId, bool isDispatch);

	public OnLaborActDiceS2CServerDelegate OnLaborActDiceS2CServerCallBackAsync;

	internal virtual async UniTask PushLaborActDiceS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnLaborActDiceS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议LaborActDiceS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		LaborActDiceS2C model = param.ReadObject<LaborActDiceS2C>();
		await OnLaborActDiceS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
