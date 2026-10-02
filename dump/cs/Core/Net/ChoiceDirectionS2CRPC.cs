using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChoiceDirectionS2CRPC
{
	public delegate UniTask OnChoiceDirectionS2CServerDelegate(ChoiceDirectionS2C model, int errId, bool isDispatch);

	public OnChoiceDirectionS2CServerDelegate OnChoiceDirectionS2CServerCallBackAsync;

	internal virtual async UniTask PushChoiceDirectionS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChoiceDirectionS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChoiceDirectionS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChoiceDirectionS2C model = param.ReadObject<ChoiceDirectionS2C>();
		await OnChoiceDirectionS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
