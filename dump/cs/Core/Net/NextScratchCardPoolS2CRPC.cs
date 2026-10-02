using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class NextScratchCardPoolS2CRPC
{
	public delegate UniTask OnNextScratchCardPoolS2CServerDelegate(NextScratchCardPoolS2C model, int errId, bool isDispatch);

	public OnNextScratchCardPoolS2CServerDelegate OnNextScratchCardPoolS2CServerCallBackAsync;

	internal virtual async UniTask PushNextScratchCardPoolS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnNextScratchCardPoolS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议NextScratchCardPoolS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		NextScratchCardPoolS2C model = param.ReadObject<NextScratchCardPoolS2C>();
		await OnNextScratchCardPoolS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
