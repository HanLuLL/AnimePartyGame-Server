using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChinaCreateOrderS2CRPC
{
	public delegate UniTask OnChinaCreateOrderS2CServerDelegate(ChinaCreateOrderS2C model, int errId, bool isDispatch);

	public OnChinaCreateOrderS2CServerDelegate OnChinaCreateOrderS2CServerCallBackAsync;

	internal virtual async UniTask PushChinaCreateOrderS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChinaCreateOrderS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChinaCreateOrderS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChinaCreateOrderS2C model = param.ReadObject<ChinaCreateOrderS2C>();
		await OnChinaCreateOrderS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
