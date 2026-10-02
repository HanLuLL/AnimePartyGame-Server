using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class CleanItemLimitS2CRPC
{
	public delegate UniTask OnCleanItemLimitS2CServerDelegate(CleanItemLimitS2C model, int errId, bool isDispatch);

	public OnCleanItemLimitS2CServerDelegate OnCleanItemLimitS2CServerCallBackAsync;

	internal virtual async UniTask PushCleanItemLimitS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnCleanItemLimitS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议CleanItemLimitS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		CleanItemLimitS2C model = param.ReadObject<CleanItemLimitS2C>();
		await OnCleanItemLimitS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
