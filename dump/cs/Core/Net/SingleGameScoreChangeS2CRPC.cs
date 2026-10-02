using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SingleGameScoreChangeS2CRPC
{
	public delegate UniTask OnSingleGameScoreChangeS2CServerDelegate(SingleGameScoreChangeS2C model, int errId, bool isDispatch);

	public OnSingleGameScoreChangeS2CServerDelegate OnSingleGameScoreChangeS2CServerCallBackAsync;

	internal virtual async UniTask PushSingleGameScoreChangeS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSingleGameScoreChangeS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SingleGameScoreChangeS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SingleGameScoreChangeS2C model = param.ReadObject<SingleGameScoreChangeS2C>();
		await OnSingleGameScoreChangeS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
