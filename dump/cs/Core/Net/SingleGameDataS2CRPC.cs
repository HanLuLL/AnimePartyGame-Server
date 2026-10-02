using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class SingleGameDataS2CRPC
{
	public delegate UniTask OnSingleGameDataS2CServerDelegate(SingleGameDataS2C model, int errId, bool isDispatch);

	public OnSingleGameDataS2CServerDelegate OnSingleGameDataS2CServerCallBackAsync;

	internal virtual async UniTask PushSingleGameDataS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnSingleGameDataS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议SingleGameDataS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		SingleGameDataS2C model = param.ReadObject<SingleGameDataS2C>();
		await OnSingleGameDataS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
