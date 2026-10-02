using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class GetQuestionUrlS2CRPC
{
	public delegate UniTask OnGetQuestionUrlS2CServerDelegate(GetQuestionUrlS2C model, int errId, bool isDispatch);

	public OnGetQuestionUrlS2CServerDelegate OnGetQuestionUrlS2CServerCallBackAsync;

	internal virtual async UniTask PushGetQuestionUrlS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnGetQuestionUrlS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议GetQuestionUrlS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		GetQuestionUrlS2C model = param.ReadObject<GetQuestionUrlS2C>();
		await OnGetQuestionUrlS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
