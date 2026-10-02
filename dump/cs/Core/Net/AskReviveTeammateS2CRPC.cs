using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class AskReviveTeammateS2CRPC
{
	public delegate UniTask OnAskReviveTeammateS2CServerDelegate(AskReviveTeammateS2C model, int errId, bool isDispatch);

	public OnAskReviveTeammateS2CServerDelegate OnAskReviveTeammateS2CServerCallBackAsync;

	internal virtual async UniTask PushAskReviveTeammateS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnAskReviveTeammateS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议AskReviveTeammateS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		AskReviveTeammateS2C model = param.ReadObject<AskReviveTeammateS2C>();
		await OnAskReviveTeammateS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
