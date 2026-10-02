using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class LandChoiceTargetS2CRPC
{
	public delegate UniTask OnLandChoiceTargetS2CServerDelegate(LandChoiceTargetS2C model, int errId, bool isDispatch);

	public OnLandChoiceTargetS2CServerDelegate OnLandChoiceTargetS2CServerCallBackAsync;

	internal virtual async UniTask PushLandChoiceTargetS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnLandChoiceTargetS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议LandChoiceTargetS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		LandChoiceTargetS2C model = param.ReadObject<LandChoiceTargetS2C>();
		await OnLandChoiceTargetS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
