using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChooseSkinS2CRPC
{
	public delegate UniTask OnChooseSkinS2CServerDelegate(ChooseSkinS2C model, int errId, bool isDispatch);

	public OnChooseSkinS2CServerDelegate OnChooseSkinS2CServerCallBackAsync;

	internal virtual async UniTask PushChooseSkinS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChooseSkinS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChooseSkinS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChooseSkinS2C model = param.ReadObject<ChooseSkinS2C>();
		await OnChooseSkinS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
