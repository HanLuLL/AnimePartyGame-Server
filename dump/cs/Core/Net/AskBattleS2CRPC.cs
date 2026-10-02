using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class AskBattleS2CRPC
{
	public delegate UniTask OnAskBattleS2CServerDelegate(AskBattleS2C model, int errId, bool isDispatch);

	public OnAskBattleS2CServerDelegate OnAskBattleS2CServerCallBackAsync;

	internal virtual async UniTask PushAskBattleS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnAskBattleS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议AskBattleS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		AskBattleS2C model = param.ReadObject<AskBattleS2C>();
		await OnAskBattleS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
