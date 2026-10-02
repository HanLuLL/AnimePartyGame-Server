using Cysharp.Threading.Tasks;
using UnityEngine;
using party.protocol;

namespace Core.Net;

public class ChangeItemLimitS2CRPC
{
	public delegate UniTask OnChangeItemLimitS2CServerDelegate(ChangeItemLimitS2C model, int errId, bool isDispatch);

	public OnChangeItemLimitS2CServerDelegate OnChangeItemLimitS2CServerCallBackAsync;

	internal virtual async UniTask PushChangeItemLimitS2CCallBack(ByteBuf param, int errId, bool isDispatch)
	{
		if (OnChangeItemLimitS2CServerCallBackAsync == null)
		{
			Debug.LogError("协议ChangeItemLimitS2C的委托事件为空，需要添加监听！");
			return;
		}
		param.ReaderIndex(35);
		ChangeItemLimitS2C model = param.ReadObject<ChangeItemLimitS2C>();
		await OnChangeItemLimitS2CServerCallBackAsync(model, errId, isDispatch);
	}
}
