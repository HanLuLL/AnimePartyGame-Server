using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class PredictActionS2CRPC
{
	public delegate UniTask OnPredictActionS2CServerDelegate(PredictActionS2C model, int errId, bool isDispatch);

	public OnPredictActionS2CServerDelegate OnPredictActionS2CServerCallBackAsync;

	internal virtual async UniTask PushPredictActionS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnPredictActionS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议PredictActionS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		PredictActionS2C model = param.ReadObject<PredictActionS2C>();
		await OnPredictActionS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
