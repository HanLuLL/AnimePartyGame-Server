using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class LotteryChoiceS2CRPC
{
	public delegate UniTask OnLotteryChoiceS2CServerDelegate(LotteryChoiceS2C model, int errId, bool isDispatch);

	public OnLotteryChoiceS2CServerDelegate OnLotteryChoiceS2CServerCallBackAsync;

	internal virtual async UniTask PushLotteryChoiceS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnLotteryChoiceS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议LotteryChoiceS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		LotteryChoiceS2C model = param.ReadObject<LotteryChoiceS2C>();
		await OnLotteryChoiceS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
