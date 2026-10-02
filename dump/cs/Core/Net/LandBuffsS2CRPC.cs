using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class LandBuffsS2CRPC
{
	public delegate UniTask OnLandBuffsS2CServerDelegate(LandBuffsS2C model, int errId, bool isDispatch);

	public OnLandBuffsS2CServerDelegate OnLandBuffsS2CServerCallBackAsync;

	internal virtual async UniTask PushLandBuffsS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnLandBuffsS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议LandBuffsS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		LandBuffsS2C model = param.ReadObject<LandBuffsS2C>();
		await OnLandBuffsS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
