using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class LotteryDrawS2CRPC
{
	public delegate UniTask OnLotteryDrawS2CServerDelegate(LotteryDrawS2C model, int errId, bool isDispatch);

	public OnLotteryDrawS2CServerDelegate OnLotteryDrawS2CServerCallBackAsync;

	internal virtual async UniTask PushLotteryDrawS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnLotteryDrawS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议LotteryDrawS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		LotteryDrawS2C model = param.ReadObject<LotteryDrawS2C>();
		await OnLotteryDrawS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
