using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ReturnSurveyFinishS2CRPC
{
	public delegate UniTask OnReturnSurveyFinishS2CServerDelegate(ReturnSurveyFinishS2C model, int errId, bool isDispatch);

	public OnReturnSurveyFinishS2CServerDelegate OnReturnSurveyFinishS2CServerCallBackAsync;

	internal virtual async UniTask PushReturnSurveyFinishS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnReturnSurveyFinishS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ReturnSurveyFinishS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ReturnSurveyFinishS2C model = param.ReadObject<ReturnSurveyFinishS2C>();
		await OnReturnSurveyFinishS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
